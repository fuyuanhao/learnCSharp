using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fyh53_7_1
{
    class Line_T
    {
        //field
        private int x1, y1, x2, y2;
        private static int _lineCount = 0;

        //constructor
        public Line_T()
        {
            this.x1 = 0;
            this.y1 = 0;
            this.x2 = 0;
            this.y2 = 0;
            _lineCount++;
        }
        public Line_T(int x1,int y1,int x2,int y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            _lineCount++;
        }
        //method
        public static int LineCount()
        {
            return _lineCount;
        }
        public double Length(int x1, int x2, int y1, int y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
        public string Info()
        {
            return string.Format("X1 = {0}, Y1 = {1}, X2 = {2}, Y2 = {3}", this.x1, this.y1, this.x2, this.y2);
        }
        
    }
}
