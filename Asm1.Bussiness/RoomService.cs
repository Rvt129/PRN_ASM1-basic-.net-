using Asm1.Data.Entities;
using Asm1.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm1.Bussiness
{
    public class RoomService
    {
        private RoomDAO roomDAO;
        private static RoomService instance ;

        public RoomService()
        {
            roomDAO = RoomDAO.GetInstance();
        }
       

        public static RoomService GetInstance()
        {
            if (instance == null)
            {
                instance = new RoomService();
            }
            return instance;
        }

        public void CreateRoom(RoomInformation room)
        {
            roomDAO.AddRoom(room);
        }

        public void DeleteRoom(int roomId)
        {
            roomDAO.RemoveRoom(roomId);
        }

        public IEnumerable<RoomInformation> GetAllRoom()
        {
            return roomDAO.GetAllRoom();
        }

        public IEnumerable<RoomInformation> SearchRoomByRoomDetail(string roomdetail)
        {
            return roomDAO.GetAllRoomsByName(roomdetail);
        }

        public void UpdateRoom(RoomInformation tempRoom)
        {
            roomDAO.UpdateRoom(tempRoom);
        }

        public IEnumerable<RoomType> GetAllType()
        {
            return roomDAO.GetAllRoomType();
        }
    }
}
