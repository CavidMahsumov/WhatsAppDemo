using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppDemo.Models
{
   public  class PDF
    {
        public string ImagePath { get; set; }
        public string FilePath { get; set; }
        public PDF(string img,string fp)
        {
            this.ImagePath = img;
            this.FilePath = fp;
        }
    }
}
