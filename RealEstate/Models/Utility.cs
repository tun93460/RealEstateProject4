﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project4.Models
{
    public class Utility
    {
        private int utilityID;
        private string utilityType;

        public Utility(int utilityID, string utilityType)
        {
            this.utilityID = utilityID;
            this.utilityType = utilityType;
        }

        public Utility()
        {

        }

        public int UtilityID
        {
            get { return utilityID; }
            set { utilityID = value; }
        }

        public String UtilityType
        {
            get { return utilityType; }
            set { utilityType = value; }
        }

    }
}
