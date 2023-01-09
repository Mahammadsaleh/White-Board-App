using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ScreenPainter
{
    public partial class CanvasForm : Form
    {
        public CanvasForm()
        {
            InitializeComponent();
            g = pnl_Draw.CreateGraphics();

            
        }

        bool startPaint = false;
        public Graphics g;
        public int penSize = 2;
        //nullable int for storing Null value
        int? initX = null;
        int? initY = null;
        public bool drawSquare = false;
        public bool drawRectangle = false;
        public bool drawCircle = false;
        public bool drawLine = false;
        public bool drawPaint = false;
        //public Stack<Bitmap> UndoStack = new Stack<Bitmap>();
        //public Stack<Bitmap> RedoStack = new Stack<Bitmap>();
        public List<Bitmap> Undo = new List<Bitmap>();
        public List<Bitmap> Redo = new List<Bitmap>();
        public readonly object _undoRedoLocker = new object();
        public Bitmap panelBitmap;

        public void pnl_Draw_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = new Point(e.X, e.Y);
            Points.Add(startPoint);
            startPaint = true;
        }
        PointF startPoint;
        List<PointF> Points = new List<PointF>();
        
        PointF[] array;
        List<List<PointF>> LinesHistory = new List<List<PointF>>();
        public Color penColor =Color.Red;
        int counterForHistory = 0;
        private void pnl_Draw_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPaint && drawPaint)
            {
                

                //Setting the Pen BackColor and line Width
                Pen p = new Pen(penColor, penSize);
                //Drawing the line.
                //g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));

                PointF point = new PointF(e.X, e.Y);
                Points.Add(point);
                LinesHistory.Add(Points);
                //array = Points.ToArray();
                array = LinesHistory[counterForHistory].ToArray();
                g.DrawLines(p, array);
                counterForHistory++;    
                //myBitmap = new Bitmap(10,10,g);
                //Image img = Image.FromFile(@"C:\Users\User\Downloads\brush.png");
                //g.DrawImage(img, new Point(initX ?? e.X, initY ?? e.Y));

                initX = e.X;
                initY = e.Y;
                //UndoStack.Push((Bitmap)myBitmap.Clone());
                //RedoStack.Clear();
            }
        }
        public int currentPointInTheHistoryArray;
        Point pointLine = new Point();
        Bitmap bmpScreenshot;
        private void pnl_Draw_MouseUp(object sender, MouseEventArgs e)
        {
            
            if (drawLine)
            {
                Pen p = new Pen(penColor, penSize);
                g.DrawLine(p,startPoint, new Point(initX ?? e.X, initY ?? e.Y) );

               
            }
            if (drawRectangle)
            {
                Pen p = new Pen(penColor, penSize);
                SolidBrush sb = new SolidBrush(penColor);
                //setting the width twice of the height
                if(e.X > startPoint.X && e.Y > startPoint.Y)
                {
                    g.DrawRectangle(p, startPoint.X, startPoint.Y, e.X - startPoint.X, e.Y - startPoint.Y);
                }
                
                if(e.X - startPoint.X < 0 && e.Y > startPoint.Y)
                {
                    g.DrawRectangle(p, e.X ,startPoint.Y, startPoint.X-e.X,e.Y-startPoint.Y);
                }
               
                else if (e.X - startPoint.X > 0 && e.Y - startPoint.Y < 0)
                {
                    g.DrawRectangle(p, startPoint.X, e.Y, e.X - startPoint.X, startPoint.Y-e.Y);
                }
                else if (e.X<startPoint.X && e.Y < startPoint.Y)
                {
                    g.DrawRectangle(p, e.X, e.Y, startPoint.X - e.X, startPoint.Y - e.Y);
                }
               
                
            }
            if (drawCircle)
            {
                Pen p = new Pen(penColor, penSize);
                SolidBrush sb = new SolidBrush(penColor);
                //setting the width twice of the height
                if (e.X > startPoint.X && e.Y > startPoint.Y)
                {
                    g.DrawEllipse(p, startPoint.X, startPoint.Y, e.X - startPoint.X, e.Y - startPoint.Y);
                }

                if (e.X - startPoint.X < 0 && e.Y > startPoint.Y)
                {
                    g.DrawEllipse(p, e.X, startPoint.Y, startPoint.X - e.X, e.Y - startPoint.Y);
                }

                else if (e.X - startPoint.X > 0 && e.Y - startPoint.Y < 0)
                {
                    g.DrawEllipse(p, startPoint.X, e.Y, e.X - startPoint.X, startPoint.Y - e.Y);
                }
                else if (e.X < startPoint.X && e.Y < startPoint.Y)
                {
                    g.DrawEllipse(p, e.X, e.Y, startPoint.X - e.X, startPoint.Y - e.Y);
                }
            }
            //Pen p = new Pen(Color.DodgerBlue, penSize);
            //g.DrawLines(p, LinesHistory[counterForHistory - 1].ToArray());

            currentPointInTheHistoryArray = Undo.Count - 1;
            Points.Clear();
            bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                               Screen.PrimaryScreen.Bounds.Height,
                               PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);
            Undo.Add(bmpScreenshot);

            startPaint = false;
            initX = null;
            initY = null;
        }
        public void Clear()
        {
            //Pen p = new Pen(Color.Red, 100);
            //g.DrawRectangle(p, 30,30, 30, 30);
            //Brush brush = new SolidBrush(Color.White);
            //g.FillRectangle(brush, 0, 0, 2000, 2000);
            //g.Clear(Color.Transparent);
            Bitmap bitmap = new Bitmap(pnl_Draw.Width, pnl_Draw.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            bitmap.MakeTransparent();
            Graphics graph = Graphics.FromImage(bitmap);
            
           
            //  draw stuff here ...

            pnl_Draw.BackgroundImage = bitmap;
            graph.Dispose();

            
        }

        private void pnl_Draw_Paint(object sender, PaintEventArgs e)
        {
         
        }
        public Bitmap bitmapTemp; 
        public void pnl_Draw_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (TollBox.chectFlag)
            {
                if (e.Button == MouseButtons.Left)
                {
                    int width = bmpScreenshot.Width;
                    int height = bmpScreenshot.Height;
                    RectangleF destinationRect = new RectangleF(
                        0,
                        0,
                         width,
                         height);

                    // Draw a portion of the image. Scale that portion of the image
                    // so that it fills the destination rectangle.
                    RectangleF sourceRect = new RectangleF(e.X - width / 6, e.Y - height / 6, width / 2, height / 2);
                    g.DrawImage(
                        bmpScreenshot,
                        destinationRect,
                        sourceRect,
                        GraphicsUnit.Pixel);
                }
                else if (e.Button == MouseButtons.Right)
                {

                    g.DrawImage(bitmapTemp, 0, 0, bitmapTemp.Width, bitmapTemp.Height);

                }

            }
        }

        private void CanvasForm_Load(object sender, EventArgs e)
        {
            
        }
    }

}
