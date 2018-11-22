using System;
using System.Collections.Generic;
using System.Linq;
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

namespace fyh53_7_1
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
        //使用枚举来控制鼠标的状态
        GeometryType _geometryType = GeometryType.None;

        static int count = 10;
        //建立三个数组分别存储点、线、面
        Point_T[] points = new Point_T[count];
        Line_T[] lines = new Line_T[count];
        Circle_T[] circles = new Circle_T[count];

        /*
        private void bnPoint_Click(object sender, RoutedEventArgs e)
        {
            int x1 = int.Parse(tbX1.Text);
            int y1 = int.Parse(tbY1.Text);
            Point_T p1 = new Point_T(x1, y1);
            int x2 = int.Parse(tbX2.Text);
            int y2 = int.Parse(tbY2.Text);
            Point_T p2 = new Point_T(x2, y2);
            int x3 = int.Parse(tbX3.Text);
            int y3 = int.Parse(tbY3.Text);
            Point_T p3 = new Point_T(x3, y3);
            showPoint.Text = Point_T.PointCount().ToString();
            MessageBox.Show(p1.Info() + Environment.NewLine + p2.Info() + Environment.NewLine + p3.Info());
        }

        private void bnDistance_Click(object sender, RoutedEventArgs e)
        {
            int x1 = int.Parse(tbX1.Text);
            int y1 = int.Parse(tbY1.Text);
            Point_T p1 = new Point_T(x1, y1);
            int x2 = int.Parse(tbX2.Text);
            int y2 = int.Parse(tbY2.Text);
            Point_T p2 = new Point_T(x2, y2);
            MessageBox.Show("Distance is " + p1.DistanceTo(p2).ToString());
        }

        private void bnArea_Click(object sender, RoutedEventArgs e)
        {
            int x1 = int.Parse(tbX1.Text);
            int y1 = int.Parse(tbY1.Text);
            Point_T p1 = new Point_T(x1, y1);
            int x2 = int.Parse(tbX2.Text);
            int y2 = int.Parse(tbY2.Text);
            Point_T p2 = new Point_T(x2, y2);
            int x3 = int.Parse(tbX3.Text);
            int y3 = int.Parse(tbY3.Text);
            Point_T p3 = new Point_T(x3, y3);
            MessageBox.Show("Area is " + p1.Area_triangle(p2,p3).ToString());
        }
        */

        private void bnDrawPoint_Click(object sender, RoutedEventArgs e)
        {
            _geometryType = GeometryType.Point;
        }

        private void bnDrawLine_Click(object sender, RoutedEventArgs e)
        {
            _geometryType = GeometryType.Line;
        }

        private void bnDrawCircle_Click(object sender, RoutedEventArgs e)
        {
            _geometryType = GeometryType.Circle;
        }

        private void bnCreate_Click(object sender, RoutedEventArgs e)
        {
            //switch和枚举的搭配，好棒呀
            switch (_geometryType)
            {
                case GeometryType.Point:
                    DrawPoint();
                    break;
                case GeometryType.Line:
                    DrawLine();
                    break;
                case GeometryType.Circle:
                    DrawCircle();
                    break;
                default:
                    break;
            }
        }
        private void DrawPoint()
        {
            int x = int.Parse(tbX1.Text);
            int y = int.Parse(tbY1.Text);
            Point_T p = new Point_T(x, y);
            if(Point_T.PointCount() < count)
            {
                points[Point_T.PointCount() - 1] = p;
                lbInfo.Items.Add(p.Info());
            }                      
        }

        private void DrawLine()
        {
            int x1 = int.Parse(tbX1.Text);
            int y1 = int.Parse(tbY1.Text);
            int x2 = int.Parse(tbX2.Text);
            int y2 = int.Parse(tbY2.Text);
            Line_T line = new Line_T(x1, y1, x2, y2);
            if(Line_T.LineCount() < count)
            {
                lines[Line_T.LineCount() - 1] = line;
                lbInfo.Items.Add(line.Info());
            }
        }
        public void DrawCircle()
        {
            int x = int.Parse(tbX1.Text);
            int y = int.Parse(tbY1.Text);
            double r = int.Parse(tbR1.Text);
            Circle_T cir = new Circle_T(x, y, r);
            if (Circle_T.CircleCount() < count)
            {
                circles[Circle_T.CircleCount() - 1] = cir;
                lbInfo.Items.Add(cir.Info());
            }
        }
        private void btClear_Click(object sender, RoutedEventArgs e)
        {
            lbInfo.Items.Clear();
        }
        //怎么能回退一步
        //怎么能按顺序显示呢？要记录下来_geometry的变化
        private void btShowInfo_Click(object sender, RoutedEventArgs e)
        {
            lbInfo.Items.Clear();
            for (int i = 0; i < Point_T.PointCount(); i++)
            {
                lbInfo.Items.Add(points[i].Info());
            }
            for (int i = 0; i < Line_T.LineCount(); i++)
            {
                lbInfo.Items.Add(lines[i].Info());
            }
            for (int i = 0; i < Circle_T.CircleCount(); i++)
            {
                lbInfo.Items.Add(circles[i].Info());
            }
        }

    }
}
