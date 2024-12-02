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
        private string fileExtension;
        private string imageName;
        private string imageType;
        private int imageSize;

        public HomeImage(int imageID, byte[] imageData, string imageCaption, 
            string fileExtension, string imageName, string imageType, int imageSize)
        {
            this.imageID = imageID;
            this.imageData = imageData;
            this.imageCaption = imageCaption;
            this.fileExtension = fileExtension;
            this.imageName = imageName;
            this.imageType = imageType;
            this.imageSize = imageSize;
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

        public String ImageCaption
        {
            get { return imageCaption; }
            set { imageCaption = value; }
        }

        public String FileExtension
        {
            get { return fileExtension; }
            set { fileExtension = value; }
        }

        public String ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        public String ImageType
        {
            get { return imageType; }
            set { imageType = value; }
        }

        public int ImageSize
        {
            get { return imageSize; }
            set { imageSize = value; }
        }

    }
}
