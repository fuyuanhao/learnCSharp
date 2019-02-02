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
    class Line_T : Geometry_T
    {
        //field
        //这是一个线的类，线有什么，线有起点和终点
        private Coordinate_T start, end;
        private static int _lineCount = 0;  
        //这里为什么要用系统的线，和Canvas沟通
        Line _line = null;

        internal Coordinate_T Start
        {
            get { return start; }
            set
            {
                //改变我的start
                start = value;
                if (_line != null)
                {
                    //每次我的start一改变，系统的同步改变
                    _line.X1 = start.X;
                    _line.Y1 = start.Y;
                }
            }
        }
        internal Coordinate_T End
        {
            get { return end; }
            set
            {
                end = value;
                if (_line != null)
                {
                    _line.X2 = end.X;
                    _line.Y2 = end.Y;
                }
            }
        }
      
        //constructor
        public Line_T()
        {
            this.start.X = 0;
            this.start.Y = 0;
            this.end.X = 0;
            this.end.Y = 0;
            _lineCount++;
        }

        public Line_T(Coordinate_T coor1, Coordinate_T coor2)
        {
            this.start.X = coor1.X;
            this.start.Y = coor1.Y;
            this.end.X = coor2.X;
            this.end.Y = coor2.Y;
            _lineCount++;
        }

        //method
        public override int Count()
        {
            return _lineCount;
        }

        public double Length(int x1, int x2, int y1, int y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public override string ToString()
        {
            return "Line:" + start.ToString() + "," + end.ToString();
        }

        public override void Draw(Canvas canvas)
        {
            if (_line == null)
            {
                _line = new Line();
                _line.Stroke = new SolidColorBrush(Colors.Blue);
                canvas.Children.Add(_line);
            }

            _line.X1 = start.X;
            _line.Y1 = start.Y;
            _line.X2 = end.X;
            _line.Y2 = end.Y;

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
            else if(Math.Abs(start.X - end.X) < 5 ||
                    Math.Abs(start.Y - end.Y) < 5 ){
                return true;
            }
            else
                return false;
        }
        
        public double DistanceTo(Coordinate_T coor)
        {
            if (start.X == end.X){
                return Math.Abs(coor.X - Start.X);
            }else if (Start.Y == End.Y){
                return Math.Abs(coor.Y - Start.Y);
            }
            else{
                double k = (Double)(end.Y - start.Y) / (end.X - start.X);
                double b = (Double)end.Y - k * end.X;
                double distance = Math.Abs((k * coor.X - coor.Y + b) / Math.Sqrt(k * k + 1));
                return distance;
            }
            
        }

        public void HighLightDraw(Canvas canvas)
        {
            _line.StrokeThickness = 2;
            _line.Stroke = new SolidColorBrush(Colors.Yellow);            
        }
    }
}
