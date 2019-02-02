using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace fuyuanhao
{
    public enum GeometryType { None, Point, Line, Circle, Polyline, Polygon, Rectangle }
    public enum SelectType { None, Point, Line, Circle, Polyline, Polygon, Rectangle }

    class Geometry_T
    {
        private static int _Count = 0;
        //public virtual string Info()
        //{
        //    return "This is a Geometry";
        //}
        public virtual int Count()
        {
            return _Count;
        }
        public virtual void Draw(Canvas canvas)
        {

        }
    }
}
