using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace fuyuanhao
{
    class Rectangle_T: Geometry_T
    {
        private Coordinate_T start, end;
        private static int _rectangleCount = 0;
        Rectangle _rectangle = null;

        public Rectangle Rectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        internal Coordinate_T Start
        {
            get { return start; }
            set
            {
                //改变我的start
                start = value;
                if (_rectangle != null)
                {
                    //To do
                }
            }
        }
        
        internal Coordinate_T End
        {
            get { return end; }
            set
            {
                end = value;
                if (_rectangle != null)
                {
                    _rectangle.Height = Math.Abs(this.start.Y - this.end.Y);
                    _rectangle.Width = Math.Abs(this.start.X - this.end.X);
                    Canvas.SetLeft(_rectangle, Math.Min(this.start.X, this.end.X));
                    Canvas.SetTop(_rectangle, Math.Min(this.start.Y, this.end.Y));                
                }
            }
        }
        
        public Rectangle_T()
        {
            this.start.X = 0;
            this.start.Y = 0;
            this.end.X = 0;
            this.end.Y = 0;
            _rectangleCount++;
        }

        public Rectangle_T(Coordinate_T coor1, Coordinate_T coor2)
        {
            this.start.X = coor1.X;
            this.start.Y = coor1.Y;
            this.end.X = coor2.X;
            this.end.Y = coor2.Y;
            _rectangleCount++;
        }

        public override string ToString()
        {
            return "Rectangle:" + start.X.ToString() + "," + start.Y.ToString()
                + "," + end.X.ToString() + "," + end.Y.ToString();
        }

        public override void Draw(Canvas canvas)
        {
            if (_rectangle == null)
            {
                _rectangle = new Rectangle();
                _rectangle.Stroke = System.Windows.Media.Brushes.Black;
                _rectangle.Fill = System.Windows.Media.Brushes.SkyBlue;
                canvas.Children.Add(_rectangle);
            }

            _rectangle.Height = Math.Abs(this.start.Y - this.end.Y);
            _rectangle.Width = Math.Abs(this.start.X - this.end.X);
            Canvas.SetLeft(_rectangle, Math.Min(this.start.X, this.end.X));
            Canvas.SetTop(_rectangle, Math.Min(this.start.Y, this.end.Y));  
        }

        public bool InRectangle(Coordinate_T coor)
        {
            if (Math.Min(start.X, end.X) < coor.X &&
               coor.X < Math.Max(start.X, end.X) &&
               Math.Min(start.Y, end.Y) < coor.Y &&
               coor.Y < Math.Max(start.Y, end.Y))
            {
                return true;
            }
            else
                return false;
        }

        public void HighLightDraw(Canvas canvas)
        {
            _rectangle.Fill = System.Windows.Media.Brushes.Yellow;
        }
    }
}
