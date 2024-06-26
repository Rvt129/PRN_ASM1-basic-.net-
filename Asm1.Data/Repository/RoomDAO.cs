﻿using Asm1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm1.Data.Repository
{
    public class RoomDAO : IRoom
    {
        private FuminiHotelManagementContext context;

        public RoomDAO()
        {
        }
 
        private static RoomDAO instance;

        public static RoomDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new RoomDAO();
            }
            return instance;
        }
       
        public RoomDAO(FuminiHotelManagementContext _context)
        {
            this.context = _context;
        }

        public void AddRoom(RoomInformation room)
        {
            context = new();
            context.RoomInformations.Add(room);
            context.SaveChanges();
        }

        public IEnumerable<RoomInformation> GetAllRoom()
        {
            context = new();
            return context.RoomInformations.Where(r => r.RoomStatus != 2).ToList();
        }

        public IEnumerable<RoomInformation> GetAllRoomsByName(String detail)
        {
            context = new();
            return context.RoomInformations.Where(r => r.RoomDetailDescription.Contains(detail)).ToList();
        }

        public void RemoveRoom(int Roomid)
        {
            context = new();
            var roomToRemove = context.RoomInformations.FirstOrDefault(r => r.RoomId == Roomid);
            if (roomToRemove != null)
            {
                roomToRemove.RoomStatus = 2;
                context.SaveChanges();
            }

        }

        public void UpdateRoom(RoomInformation room)
        {
            context = new();
            context.Update(room);
            context.SaveChanges();
        }

        public IEnumerable<RoomType> GetAllRoomType()
        {
           context= new();
            return context.RoomTypes.ToList();
        }
    }
}
