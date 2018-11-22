using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fyh53_7_1
{
    class Circle_T
    {
        //field
        private int x, y;
        private double r;
        private static int _circleCount = 0;

        //constructor
        public Circle_T()
        {
            this.x = this.y = 0;
            this.r = 0.0;
            _circleCount++;
        }

        public Circle_T(int x, int y, double r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
            _circleCount++;
        }
        //method1:圆的个数
        public static int CircleCount()
        {
            return _circleCount;
        }
        //method2：圆的面积
        public double Area()
        {
            double circleArea = Math.PI * r * r;
            return circleArea;
        }

        //method3：圆的周长
        public double Length()
        {
            double circleLength = 2.0 * Math.PI * r;
            return circleLength;
        }

        //method4：呈现圆的信息
        public string Info()
        {
            string s1 = string.Format("X = {0}, Y = {1} ,R = {2}", x, y, r);
            //string s2 = string.Format("Area = {0}, Length = {1}", Area(), Length());
            //return s1 + Environment.NewLine + s2;
            return s1;
        }
    }
}
