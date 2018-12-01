using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    public class AT9mmS : AmmoType
    {
        public AT9mmS()
        {
			this.bulletLength = 3f;
            this.combustable = true;
            this.bulletSpeed = 37f;
        }
    }
}
