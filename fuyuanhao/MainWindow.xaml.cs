using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace fuyuanhao
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int clickNumber = 0;
        GeometryType _geometryType = GeometryType.None;
        SelectType _selectType = SelectType.None;

        Point_T myPoint = null;
        Line_T myLine = null;
        Circle_T myCircle = null;
        Rectangle_T myRectangle = null;
        Polyline_T myPolyline = null;
        Polygon_T pg = null;

        List<Geometry_T> geos = new List<Geometry_T>();

        #region 改变状态
        private void bnDrawPoint_Click(object sender, RoutedEventArgs e)
        {
            _geometryType = GeometryType.Point;
            tblDrawStatus.Text = "Point";
        }

        private void bnDrawLine_Click(object sender, RoutedEventArgs e)
        {
            _geometryType = GeometryType.Line;
            tblDrawStatus.Text = "Line";
        }

        private void bnDrawCircle_Click(object sender, RoutedEventArgs e)
        {
            _geometryType = GeometryType.Circle;
            tblDrawStatus.Text = "Circle";
        }

        private void bnDrawRectangle_Click(object sender, RoutedEventArgs e)
        {
            _geometryType = GeometryType.Rectangle;
            tblDrawStatus.Text = "Rectangle";
        }

        private void bnDrawPolyline_Click(object sender, RoutedEventArgs e)
        {
            _geometryType = GeometryType.Polyline;
            tblDrawStatus.Text = "Polyline";
            ///myPolyline = new Polyline_T();
            lbInfo.Items.Add("Polyline");
        }

        private void bnDrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            _geometryType = GeometryType.Polygon;
            tblDrawStatus.Text = "Polygon";
            pg = new Polygon_T();
            string info = "Polygon";
            lbInfo.Items.Add(info);
        }

        private void miSelectPoint_Click(object sender, RoutedEventArgs e)
        {
            _selectType = SelectType.Point;
            _geometryType = GeometryType.None;
            tblDrawStatus.Text = "SelectPoint";
        }

        private void miSelectLine_Click(object sender, RoutedEventArgs e)
        {
            _selectType = SelectType.Line;
            _geometryType = GeometryType.None;
            tblDrawStatus.Text = "SelectLine";
        }

        private void miSelectCircle_Click(object sender, RoutedEventArgs e)
        {
            _selectType = SelectType.Circle;
            _geometryType = GeometryType.None;
            tblDrawStatus.Text = "SelectCircle";
        }

        private void miSelectRectangle_Click(object sender, RoutedEventArgs e)
        {
            _selectType = SelectType.Rectangle;
            _geometryType = GeometryType.None;
            tblDrawStatus.Text = "SelectRectangle";
        }

        private void miSelectPolyline_Click(object sender, RoutedEventArgs e)
        {
            _selectType = SelectType.Polyline;
            _geometryType = GeometryType.None;
            tblDrawStatus.Text = "SelectPolyline";
        }

        #endregion

        private void canvasDraw_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point spt = e.GetPosition((IInputElement)sender);
            Coordinate_T coor = new Coordinate_T();
            coor.X = (int)spt.X;            
            coor.Y = (int)spt.Y;
            switch(_selectType)
            {
                case SelectType.None:
                    break;
                case SelectType.Point:
                    List<Point_T> points = new List<Point_T>();
                    myPoint = new Point_T(coor);
                    //找到geos中的点
                    foreach (Geometry_T geo in geos)
                    {
                        if (geo.GetType() == typeof(Point_T))
                        {                            
                            points.Add((Point_T)geo);
                        }
                    }
                    //依次计算距离,如果小于阈值高亮显示
                    foreach(Point_T point in points)
                    {
                        if(myPoint.DistanceTo(point) < 4)
                        {
                            point.HighLightDraw(canvasDraw);
                        }
                    }                                        
                    break;                
                case SelectType.Line:
                    List<Line_T> lines = new List<Line_T>();
                    //找到geos中的线
                    foreach (Geometry_T geo in geos)
                    {
                        if (geo.GetType() == typeof(Line_T))
                        {
                            lines.Add((Line_T)geo);
                        }
                    }                                        
                    foreach(Line_T line in lines)
                    {
                        //利用外接矩形初步判断
                        if(line.InRectangle(coor))
                        {
                            //计算距离进一步判断
                            if(line.DistanceTo(coor) < 10)
                            {
                                line.HighLightDraw(canvasDraw);
                            }
                        }
                    }
                    break;
                case SelectType.Circle:
                    List<Circle_T> circles = new List<Circle_T>();
                    foreach (Geometry_T geo in geos)
                    {
                        if (geo.GetType() == typeof(Circle_T))
                        {
                            circles.Add((Circle_T)geo);
                        }
                    }
                    foreach (Circle_T circle in circles)
                    {
                        if(circle.InCircle(coor))
                            {
                                circle.HighLightDraw(canvasDraw);
                            }
                    }
                    break;
                case SelectType.Rectangle:
                    List<Rectangle_T> rectangles = new List<Rectangle_T>();
                    foreach (Geometry_T geo in geos)
                    {
                        if (geo.GetType() == typeof(Rectangle_T))
                        {
                            rectangles.Add((Rectangle_T)geo);
                        }
                    }
                    foreach (Rectangle_T rectangle in rectangles)
                    {
                        if (rectangle.InRectangle(coor))
                        {
                            rectangle.HighLightDraw(canvasDraw);
                        }
                    }
                    break;
                case SelectType.Polyline:
                    List<Polyline_T> polylines = new List<Polyline_T>();
                    //找到geos中的线
                    foreach (Geometry_T geo in geos)
                    {
                        if (geo.GetType() == typeof(Polyline_T))
                        {
                            polylines.Add((Polyline_T)geo);
                        }
                    }
                    foreach (Polyline_T polyline in polylines)
                    {
                        if (polyline.DistanceTo(coor) < 10)
                        {
                            polyline.HighLightDraw(canvasDraw);
                        }

                    }
                    break;
                default:
                    break;

            }
            #region Draw
            //switch和枚举的搭配，好棒呀
            switch (_geometryType)
            {
                case GeometryType.None:
                    break;
                case GeometryType.Point:
                    Point_T mypt = new Point_T(coor);
                    //素质三连，存储 显示 画
                    geos.Add(mypt);
                    lbInfo.Items.Add(mypt.ToString());
                    mypt.Draw(canvasDraw);
                    break;
                case GeometryType.Line:                    
                    if (clickNumber == 0)
                    {                       
                        myLine = new Line_T();
                        myLine.Start = new Coordinate_T(coor.X, coor.Y);
                        myLine.End = new Coordinate_T(coor.X, coor.Y);                                           
                        //真正的威力不是这行代码，而是它里面的东西
                        //这行代码是将myLine中的系统线创建并加到画布上
                        //系统的线很厉害，甚至不需要起点终点就能加上去
                        //而且系统的线的起点终点改变时效果随之改变
                        myLine.Draw(canvasDraw);
                        clickNumber++;
                    }
                    else
                    {
                        myLine.End = new Coordinate_T(coor.X, coor.Y);
                        lbInfo.Items.Add(myLine.ToString());
                        geos.Add(myLine);
                        //myLine.Start = myLine.End;
                        clickNumber = 0;
                        myLine = null;
                    }
                    break;
                case GeometryType.Circle:
                    if (clickNumber == 0)
                    {
                        myCircle = new Circle_T();
                        myCircle.Center = new Coordinate_T(coor.X, coor.Y);
                        myCircle.End = new Coordinate_T(coor.X, coor.Y);                        
                        myCircle.Draw(canvasDraw);
                        clickNumber++;                                       
                    }
                    else
                    {                       
                        lbInfo.Items.Add(myCircle.ToString());
                        geos.Add(myCircle);
                        clickNumber = 0;
                        myCircle = null;                       
                    }
                    break;
                case GeometryType.Rectangle:
                    if(clickNumber == 0)
                    {
                        myRectangle = new Rectangle_T();
                        myRectangle.Start = new Coordinate_T(coor.X, coor.Y);
                        myRectangle.End = new Coordinate_T(coor.X, coor.Y);
                        myRectangle.Draw(canvasDraw);
                        clickNumber++;
                    }
                    else
                    {
                        lbInfo.Items.Add(myRectangle.ToString());
                        geos.Add(myRectangle);
                        clickNumber = 0;
                        myRectangle = null;  
                    }
                    break;
                case GeometryType.Polyline:
                    if (e.ClickCount == 1)
                    {
                        if (clickNumber == 0)
                        {                          
                            myPolyline = new Polyline_T();
                            myPolyline.Draw(canvasDraw);
                            myPolyline.Start = coor;

                            myPolyline.Flag = true;
                            myPolyline.End = coor;
                            myPolyline.Flag = false;

                            myPolyline.AddPoint(coor);
                            geos.Add(myPolyline);
                            lbInfo.Items.Add(coor.ToString());
                            clickNumber++;
                        }
                        else
                        {                            
                            //加入新的点
                            geos.RemoveAt(geos.Count - 1);
                            //myPolyline.AddPoint(endCoor);
                            myPolyline.AddPoint(coor);
                            geos.Add(myPolyline);

                            myPolyline.Flag = true;
                            myPolyline.End = coor;
                            myPolyline.Flag = false;

                            //lbInfo.Items.Add(endCoor.ToString());
                            lbInfo.Items.Add(coor.ToString());
                            //把终点换为起点
                            myPolyline.Start = myPolyline.End;
                            
                        }
                    }

                    if (e.ClickCount >= 2)
                    {
                        //geos.RemoveAt(geos.Count - 1);                       
                        //myPolyline.AddPoint(coor);
                        //geos.Add(myPolyline);
                        myPolyline.Draw(canvasDraw);
                        myPolyline = null;
                        lbInfo.Items.Add("End");
                        _geometryType = GeometryType.None;
                        tblDrawStatus.Text = "None";
                        clickNumber = 0;
                    }
                    break;
                default:
                    break;
            }
            #endregion

        }

        private void canvasDraw_MouseMove(object sender, MouseEventArgs e)
        {
            //spt为此时此刻鼠标的位置，他这里应该会有一个时间的阀值，过个零点几秒读取一次
            Point spt = e.GetPosition((IInputElement)sender);
            Coordinate_T coor = new Coordinate_T();
            coor.X = (int)spt.X;
            coor.Y = (int)spt.Y;

            switch (_geometryType)
            {
                case GeometryType.None:
                    break;
                case GeometryType.Point:
                    break;
                case GeometryType.Line:
                    if (myLine != null)
                        myLine.End = new Coordinate_T(coor.X, coor.Y);
                    break;
                case GeometryType.Rectangle:
                    if (myRectangle != null)
                    {
                        myRectangle.End = new Coordinate_T(coor.X, coor.Y);
                        //endCoor = coor;                                                
                    }                        
                    break;
                case GeometryType.Circle:
                    if (myCircle != null)
                    {
                        myCircle.End = new Coordinate_T(coor.X, coor.Y);
                        //endCoor = coor;                        
                    }
                    break;
                case GeometryType.Polyline:                   
                    if(myPolyline != null)
                    {
                        myPolyline.End = new Coordinate_T(coor.X, coor.Y);
                    }
                    break;
                default:
                    break;
            }
            lbPosition.Content = spt.X + ", " + spt.Y;
        }

        #region 写文件
        SaveFileDialog _sfd = new SaveFileDialog();
        private void bnSave_Click(object sender, RoutedEventArgs e)
        {
            _sfd.Filter = "Txt|*.txt|All Files|*.*";

            _sfd.FileOk += saveFileDialogFileOk;

            _sfd.ShowDialog();
        }  
        private void saveFileDialogFileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(_sfd.FileName))
            {
                foreach (Geometry_T geo in geos)
                {
                    sw.WriteLine(geo.ToString());
                }
            }
        }
        #endregion

        #region 读文件

        OpenFileDialog _ofd = new OpenFileDialog();

        private void bnOpen_Click(object sender, RoutedEventArgs e)
        {
            _ofd.Filter = "Txt|*.txt|All Files|*.*";

            _ofd.FileOk += openFileDialogFileOk;

            _ofd.ShowDialog();
        }

        private void openFileDialogFileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            geos.Clear();
            lbInfo.Items.Clear();
            canvasDraw.Children.Clear();

            using (StreamReader sr = new StreamReader(_ofd.FileName))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] value = line.Split(':');
                    string[] number = value[1].Split(',');
                    switch (value[0])
                    {
                        case "Point":
                            int x = int.Parse(number[0]);
                            int y = int.Parse(number[1]);
                            Point_T pt = new Point_T(new Coordinate_T(x, y));
                            geos.Add(pt);
                            break;
                        case "Line":
                            number = value[1].Split(',');
                            int x1 = int.Parse(number[0]);
                            int y1 = int.Parse(number[1]);
                            int x2 = int.Parse(number[2]);
                            int y2 = int.Parse(number[3]);
                            Line_T lineSegment = new Line_T(new Coordinate_T(x1, y1), new Coordinate_T(x2, y2));
                            geos.Add(lineSegment);
                            break;
                        case "Circle":
                            number = value[1].Split(',');
                            x = int.Parse(number[0]);
                            y = int.Parse(number[1]);
                            double r = double.Parse(number[2]);
                            Circle_T circleSegment = new Circle_T(new Coordinate_T(x, y), r);
                            geos.Add(circleSegment);
                            break;
                        case "Rectangle":
                            number = value[1].Split(',');
                            x1 = int.Parse(number[0]);
                            y1 = int.Parse(number[1]);
                            x2 = int.Parse(number[2]);
                            y2 = int.Parse(number[3]);
                            Rectangle_T rectangleSegment = new Rectangle_T(new Coordinate_T(x1, y1), new Coordinate_T(x2, y2));
                            geos.Add(rectangleSegment);
                            break;
                        case "Polyline":
                            ReadPolyline(sr);
                            break;
                        default:
                            break;
                    }
                    line = sr.ReadLine();
                }
            }
            //显示所有形状、及文字
            //bnShowAllGeometry_Click(null, null);
            //bnShowText_Click(null, null);
            foreach (var geo in geos)
            {
                geo.Draw(canvasDraw);
            }
            foreach (Geometry_T geo in geos)
            {
                lbInfo.Items.Add(geo.ToString());
            }
        }

        //读取折线
        private void ReadPolyline(StreamReader sr)
        {
            Polyline_T poly = new Polyline_T();

            string line = sr.ReadLine();

            while (line != "End")
            {
                string[] number = line.Split(',');
                int x = int.Parse(number[0]);
                int y = int.Parse(number[1]);
                poly.AddPoint(new Coordinate_T(x, y));
                line = sr.ReadLine();
            }
            geos.Add(poly);
        }
        #endregion
        private void bnNew_Click(object sender, RoutedEventArgs e)
        {
            geos.Clear();
            lbInfo.Items.Clear();
            canvasDraw.Children.Clear();
        }

        private void miClearText_Click(object sender, RoutedEventArgs e)
        {
            lbInfo.Items.Clear();
        }

        private void miShowAllText_Click(object sender, RoutedEventArgs e)
        {
            lbInfo.Items.Clear();
            foreach (var item in geos)
            {
                lbInfo.Items.Add(item.ToString());
            }
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
                     
    }
}
