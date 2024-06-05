using Asm1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm1.Data.Repository
{
    public interface IRoom
    {
        IEnumerable<RoomInformation> GetAllRoom();
        IEnumerable<RoomInformation> GetAllRoomsByName(String t);
        public void AddRoom(RoomInformation room);
        public void RemoveRoom(int Roomid);
        public void UpdateRoom(RoomInformation room);
    }
}
