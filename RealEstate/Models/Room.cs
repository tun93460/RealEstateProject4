using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Models
{
    public class Room
    {
        private int roomID;
        private string roomType;
        private string roomDescription;
        private double roomWidth;
        private double roomLength;

        public Room(int roomID, string roomType, string roomDescription, double roomWidth, double roomLength)
        {
            this.roomID = roomID;
            this.roomType = roomType;
            this.roomDescription = roomDescription;
            this.roomWidth = roomWidth;
            this.roomLength = roomLength;
        }

        public Room()
        {

        }

        public int RoomID
        {
            get { return roomID; }
            set { roomID = value; }
        }

        public String RoomType
        {
            get { return roomType; }
            set { roomType = value; }
        }

        public String RoomDescription
        {
            get { return roomDescription; }
            set { roomDescription = value; }
        }

        public double RoomWidth
        {
            get { return roomWidth; }
            set { roomWidth = value; }
        }

        public double RoomLength
        {
            get { return roomLength; }
            set { roomLength = value; }
        }
    }
}
