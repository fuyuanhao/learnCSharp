using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fyh53_7_1
{
    class Point_T
    {
        //field
        private int x, y;
        private static int _pointCount = 0;

        //constructor
        public Point_T()
        {
            this.x = -1;
            this.y = -1;
            _pointCount++;
        }

        public Point_T(int x, int y)
        {
            this.x = x;
            this.y = y;
            _pointCount++;
        }

        //method1：点的个数
        public static int PointCount()
        {
            return _pointCount;
        }

        //method2：和另一个点的距离
        public double DistanceTo(Point_T other)
        {
            int xDiff = this.x - other.x;
            int yDiff = this.y - other.y;
            double distance = Math.Sqrt((xDiff * xDiff) + (yDiff * yDiff));
            return distance;
        }

        //mdthod3：三点构成的三角形面积
        public double Area_triangle(Point_T one, Point_T two)
        {
            double s = Math.Abs((this.x * one.y + one.x * two.y + two.x * this.y - this.x * two.y - one.x * this.y - two.x * one.y) / 2.0);
            return s;
        }

        //method4：显示点的坐标
        public string Info()
        {
            string s = string.Format("X = {0}, Y = {1}",  this.x,  this.y);
            return s;
        }
    }
}
