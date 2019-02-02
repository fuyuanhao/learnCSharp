using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace fuyuanhao
{
    class Polyline_T : Geometry_T
    {
        private Coordinate_T start, end;
        private List<Coordinate_T> coors = new List<Coordinate_T>();
        Polyline _polyline = null;
        Line _line = null;
        bool flag = false;

        public bool Flag
        {
            get { return flag; }
            set { flag = value; }
        } 
        
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
                if(flag == true)
                {
                    Point pt = new Point(end.X, end.Y);
                    _polyline.Points.Add(pt);
                }
            }
        }
      
        private static int _polylineCount = 0;
        
        public void AddPoint(Coordinate_T var)
        {
            coors.Add(var);
            //Point pt = new Point(var.X, var.Y);
            //_polyline.Points.Add(pt);
        }

        public override int Count()
        {
            return _polylineCount;
        }
       
        public int PointCount()
        {
            return coors.Count();
        }

        public override string ToString()
        {
            //这是个静态的
            string info = "Polyline:" + Environment.NewLine;

            foreach (var item in coors)
            {
                info += item.ToString() + Environment.NewLine;
            }
            info += "End";

            return info;
        }

        public override void Draw(Canvas canvas)
        {
            if (_polyline == null)
            {
                _line = new Line();

                //系统的线
                _polyline = new Polyline();
                _polyline.Stroke = System.Windows.Media.Brushes.Black;
                _polyline.StrokeThickness = 2;
                //系统的线加到画布上
                canvas.Children.Add(_polyline);

                PointCollection myPointCollection2 = new PointCollection();
                foreach (Coordinate_T coor in coors)
                {
                    Point pt = new Point(coor.X, coor.Y);
                    myPointCollection2.Add(pt);                   
                }
                _polyline.Points = myPointCollection2;

                _line.Stroke = new SolidColorBrush(Colors.Blue);
                canvas.Children.Add(_line);
                
            }

            _line.X1 = start.X;
            _line.Y1 = start.Y;
            _line.X2 = end.X;
            _line.Y2 = end.Y;
            
        }

        public double DistanceTo(Coordinate_T coor)
        {           
            double distance = 100000000.0;
            double temp = 100000000.0;
            for (int i = 0; i < coors.Count - 1; i++)
            {
                start = coors[i];
                end = coors[i + 1];
                //判断是否在小段直线的外接矩形中
                if (Math.Min(start.X, end.X) < coor.X &&
                   coor.X < Math.Max(start.X, end.X) &&
                   Math.Min(start.Y, end.Y) < coor.Y &&
                   coor.Y < Math.Max(start.Y, end.Y))
                {
                    //计算距离
                    double k = (Double)(end.Y - start.Y) / (end.X - start.X);
                    double b = (Double)end.Y - k * end.X;
                    temp = Math.Abs((k * coor.X - coor.Y + b) / Math.Sqrt(k * k + 1));
                }
                //垂直情况
                else if (Math.Abs(start.X - end.X) < 5)
                {
                    temp = Math.Abs(coor.X - start.X);
                }
                //水平情况
                else if (Math.Abs(start.Y - end.Y) < 5)
                {
                    temp = Math.Abs(coor.Y - start.Y);
                }
                if (temp < distance)
                    distance = temp;
            }
            return distance;
        }

        public void HighLightDraw(Canvas canvas)
        {
            _polyline.Stroke = System.Windows.Media.Brushes.Yellow;
        }
       
    }
}
