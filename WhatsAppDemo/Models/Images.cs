﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppDemo.Models
{
    public class Images
    {
        public string ImagePath { get; set; }
        public Images(string imagepath)
        {
            this.ImagePath = imagepath;
        }
    }
}
