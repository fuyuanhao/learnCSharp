using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace fuyuanhao
{
    class Point_T : Geometry_T
    {
        //field
        private Coordinate_T coor;
        private static int _pointCount = 0;

        //constructor
        public Point_T(Coordinate_T coor)
        {
            this.coor = coor;
        }

        //method1：点的个数
        public override int Count()
        {
            return _pointCount;
        }

        //method2：和另一个点的距离
        public double DistanceTo(Point_T other)
        {
            int xDiff = this.coor.X - other.coor.X;
            int yDiff = this.coor.Y - other.coor.Y;
            double distance = Math.Sqrt((xDiff * xDiff) + (yDiff * yDiff));
            return distance;
        }

        //mdthod3：三点构成的三角形面积
        public double Area_triangle(Point_T one, Point_T two)
        {
            double s = Math.Abs((this.coor.X * one.coor.Y + one.coor.X * two.coor.Y + two.coor.X * this.coor.Y - this.coor.X * two.coor.Y - one.coor.X * this.coor.Y - two.coor.X * one.coor.Y) / 2.0);
            return s;
        }

        //method4：显示点的坐标
        public override string ToString()
        {           
            return "Point:" + coor.ToString();
        }
        
        public override void Draw(Canvas canvas)
        {
            Line hline = new Line();

            hline.Stroke = new SolidColorBrush(Colors.Coral);
            hline.X1 = this.coor.X - 3;
            hline.Y1 = this.coor.Y - 3;
            hline.X2 = this.coor.X + 3;
            hline.Y2 = this.coor.Y + 3;

            canvas.Children.Add(hline);

            Line vline = new Line();

            vline.Stroke = new SolidColorBrush(Colors.Black);
            vline.X1 = this.coor.X + 3;
            vline.Y1 = this.coor.Y - 3;
            vline.X2 = this.coor.X - 3;
            vline.Y2 = this.coor.Y + 3;

            canvas.Children.Add(vline);
        }

        public void HighLightDraw(Canvas canvas)
        {
            Line hline = new Line();

            hline.StrokeThickness = 2;
            hline.Stroke = new SolidColorBrush(Colors.Yellow);
            hline.X1 = this.coor.X - 3;
            hline.Y1 = this.coor.Y - 3;
            hline.X2 = this.coor.X + 3;
            hline.Y2 = this.coor.Y + 3;

            canvas.Children.Add(hline);

            Line vline = new Line();

            vline.StrokeThickness = 2;
            vline.Stroke = new SolidColorBrush(Colors.Yellow);
            vline.X1 = this.coor.X + 3;
            vline.Y1 = this.coor.Y - 3;
            vline.X2 = this.coor.X - 3;
            vline.Y2 = this.coor.Y + 3;

            canvas.Children.Add(vline);
        }
    }
}
