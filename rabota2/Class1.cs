using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabota2
{
    public partial class Tovar
    {
        public override string ToString()
        {
            return nazvanie;
        }
    }

    public partial class VidTovara
    {
        public override string ToString()
        {
            return vid;
        }
    }
}
