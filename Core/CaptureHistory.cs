using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalCapture
{
    [Serializable]
    internal class CaptureHistory
    {
        public Bitmap? Bitmap { get; set; }
        public Point? Size { get; set; }
    }
}
