using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Project4.Models   
{
    public class HomeImage
    {
        private int imageID;
        private byte[] imageData;
        private string imageCaption;

        public HomeImage(int imageID, byte[] imageData, string imageCaption)
        {
            this.imageID = imageID;
            this.imageData = imageData;
            this.imageCaption = imageCaption;
        }

        public HomeImage()
        {
        }

        public int ImageID
        {
            get { return imageID; }
            set { imageID = value; }
        }

        public byte[] ImageData
        {
            get { return imageData; }
            set { imageData = value; }
        }

        public string ImageCaption
        {
            get { return imageCaption; }
            set { imageCaption = value; }
        }
    }
}
