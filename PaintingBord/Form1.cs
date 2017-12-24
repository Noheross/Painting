using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace PaintingBord
{
    public partial class Form1 : Form
    {
        private bool mark = false;
        //记录画下一条很短线段的起始点 
        private Point point;
        //画笔颜色
        private Color color = Color.Black;
        //画笔
        private Pen pen; 
        //用来记录1条曲线的所有点。
        private List<Point> Point_List;
        //用来记录每条曲线，既Point_List 
        private List<List<Point>> Line_List;
        private int x, y;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            Line_List = new List<List<Point>>();
            pen = new Pen(Color.Black);
            x = 110;
            y = 110;
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {    //建立数组，记录1条曲线的所有点
                Point_List = new List<Point>();
                point.X = e.X;
                point.Y = e.Y;
                
                mark = true;
                Point_List.Add(point);//记录曲线起点的坐标
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (mark)       //如果鼠标左键已按下
            {   //得到窗体的使用的Graphics类对象 
                
                
                Graphics g = Drawing.CreateGraphics();                                  //画线注意画线起始点和终点
               // g.DrawLine(pen, point.X, point.Y, e.X, e.Y);
                DrawCir(Drawing.BackColor, Point_List[0],point);
                point.X = e.X;  //记录画下一线段的起始点的坐标
                point.Y = e.Y;
                DrawCir(color, Point_List[0], point);
                Point_List.Add(point); //记录曲线中其他点的坐标	


            }
            toolStripStatusLabel1.Text = "X:" + e.X.ToString() + "  Y:" + e.Y.ToString();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mark = false;
            Line_List.Add(Point_List);
        }

        private void Drawing_MouseEnter(object sender, EventArgs e)
        {

        }

        private void Drawing_MouseHover(object sender, EventArgs e)
        {
            

        }
        private void Drawing_Paint(object sender, PaintEventArgs e)
        {
            //最小化最大化时不会图像消失
            Graphics g = e.Graphics;
            Pen pen1 = new Pen(Color.Black);
            Point p1, p2;
            foreach (List<Point> l in Line_List)//取出每条线
            {
                for (int k = 0; k < (l.Count - 1); k++)//重画每条线的点
                {
                    p1 = l[k];
                    p2 = l[k + 1];
                    g.DrawLine(pen1, p1, p2);
                }
            } //运行，在Form窗体拖动鼠标可以画线。
        }



        void DrawCir(Color color,Point startp,Point endp)//参数是画圆的笔和刷子的颜色
        {
            Graphics g = Drawing.CreateGraphics();
            Pen pen1 = new Pen(color);
        //    SolidBrush brush1 = new SolidBrush(fillcolor);
            g.DrawEllipse(pen1, startp.X, startp.Y,endp.X-startp.X,endp.Y-startp.Y);
       //     g.FillEllipse(brush1, startp.X, startp.Y, endp.X, endp.Y);
        }

    }
}
