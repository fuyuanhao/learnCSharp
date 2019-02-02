using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fuyuanhao
{
    struct Coordinate_T
    {
        public int X, Y;

        public Coordinate_T(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public override string ToString()
        {
            return X.ToString() + ", " + Y.ToString();
        }
    }
}
