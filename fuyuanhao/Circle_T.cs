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
    class Circle_T : Geometry_T
    {
        //field
        private Coordinate_T center, end;      
        private double r;
        //private double height, width;
        private static int _circleCount = 0;        
        private Ellipse _circle = null;

        internal double R
        {
            get { return r; }
            set
            {
                r = value;
            }
        }

        internal Ellipse Ellipse
        {
            get { return _circle; }
            set
            {
                _circle = value;
            }
        }

        internal Coordinate_T Center
        {
            get { return center; }
            set
            {
                center = value;

                if (_circle != null)
                {
                    //_line.X1 = center.X;
                    //_line.Y1 = center.Y;
                }
            }
        }
        internal Coordinate_T End
        {
            get { return end; }
            set
            {
                end = value;
                if (_circle != null)
                {
                    this.r = Math.Sqrt((center.X - end.X) * (center.X - end.X) + (center.Y - end.Y) * (center.Y - end.Y));
                    _circle.Height = 2 * this.r;
                    _circle.Width = 2 * this.r;
                    Canvas.SetLeft(_circle, center.X - this.r);
                    Canvas.SetTop(_circle, center.Y - this.r);                   
                }
            }
        }
     
        //constructor
        public Circle_T()
        {
            this.center.X = 0;
            this.center.Y = 0;
            this.r = 0.0;
            _circleCount++;
        }

        public Circle_T(Coordinate_T center, double r)
        {
            this.center.X = center.X;
            this.center.Y = center.Y;
            this.r = r;
            _circleCount++;
        }

        //method1:圆的个数
        public override int Count()
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
        public override string ToString()
        {    
            return "Circle:" + center.ToString() + "," + r.ToString();
        }

        //method5：画圆
        public override void Draw(Canvas canvas)
        {     
            if(_circle == null)
            {
                _circle = new Ellipse();
                _circle.Stroke = System.Windows.Media.Brushes.Black;
                _circle.Fill = System.Windows.Media.Brushes.LightGreen;

                canvas.Children.Add(_circle);
            }

            _circle.Height = 2 * this.r;
            _circle.Width = 2 * this.r;
            Canvas.SetLeft(_circle, this.center.X - this.r);
            Canvas.SetTop(_circle, this.center.Y - this.r);      
          
        }
        
        public bool InCircle(Coordinate_T coor)
        {
            double distance = Math.Sqrt((coor.X - center.X) * (coor.X - center.X) + (coor.Y - center.Y) * (coor.Y - center.Y));
            if (distance < r)
                return true;
            else
                return false;
        }

        public void HighLightDraw(Canvas canvas)
        {
            _circle.Fill = System.Windows.Media.Brushes.Yellow;
        }
    }
}
