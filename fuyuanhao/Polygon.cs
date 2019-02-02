using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fuyuanhao
{
    class Polygon_T : Geometry_T
    {
        //private static int _polygonCount;
        private List<Coordinate_T> coors = new List<Coordinate_T>();

        public Polygon_T()
        {

        }

        public void AddPoint(Coordinate_T var)
        {
            coors.Add(var);
        }
        public Coordinate_T First()
        {
            return coors[0];
        }
        //public override int Count()
        //{
        //    return _polygonCount;
        //}
        public override string ToString()
        {
            //这是个静态的
            string info = "Polygon" + Environment.NewLine;

            foreach (var item in coors)
            {
                info += item.ToString() + Environment.NewLine;
            }
            info += "End";

            return info;
        }
    }
}
