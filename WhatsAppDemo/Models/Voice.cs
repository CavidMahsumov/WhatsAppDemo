using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppDemo.Models
{
    public class Voice
    {
        public string VoicePath { get; set; }
        public string ImagePath { get; set; }
        public Voice(string Path,string ImagePath)
        {
            this.VoicePath = Path;
            this.ImagePath = ImagePath;
        }
       
    }
}
