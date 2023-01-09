using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ScreenPainter
{
    public partial class TollBox : Form
    {
        public TollBox()
        {
            
            InitializeComponent();
        }
      
        CanvasForm canvasForm = new CanvasForm();
        bool scrnsht=false;
        private void btnPen_Click(object sender, EventArgs e)
        {


            canvasForm.drawCircle = false;
            canvasForm.drawPaint = true;
            canvasForm.drawLine = false;
            canvasForm.drawRectangle = false;
            
            if (scrnsht == false)
            {
                this.Hide();
                Thread.Sleep(500);
                var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
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


                canvasForm.BackgroundImage = bmpScreenshot;
                canvasForm.bitmapTemp = bmpScreenshot.Clone(new Rectangle(0, 0, bmpScreenshot.Width, bmpScreenshot.Height), PixelFormat.Format32bppArgb);
                scrnsht = true;
            }
            canvasForm.Show();
           
            this.TopMost = true;
            this.Show();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
            canvasForm.Clear();
        }

        private void numericPenSize_ValueChanged(object sender, EventArgs e)
        {
            canvasForm.penSize = ((int)numericPenSize.Value);
        }
        public ColorDialog c = new ColorDialog();
        int countForColor=0;
        private void colorDialog_ButtonClick(object sender, EventArgs e)
        {
        
            
            if (c.ShowDialog() == DialogResult.OK)
            {

                button1.BackColor = c.Color;
                canvasForm.penColor = c.Color;
                Brush brush = new SolidBrush(button1.BackColor);
                
                var image1 = new Bitmap(2, 2);
                var gfxScreenshot1 = Graphics.FromImage(image1);
                gfxScreenshot1.FillRectangle(brush, 0, 0, 2, 2);
                Image img = image1;
                colorDialog.DropDownItems.Add(img);
                colorDialog.DropDownItems[countForColor].BackColor = c.Color;
                countForColor++;
            }
        }

        private void colorDialog_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {



            button1.BackgroundImage = e.ClickedItem.Image;
            canvasForm.penColor = e.ClickedItem.BackColor;

        }
        
        private void btnUndo_Click(object sender, EventArgs e)
        {
            //canvasForm.panelBitmap = canvasForm.Undo[0];
            ////canvasForm.g.DrawImage(canvasForm.panelBitmap, canvasForm.panelBitmap.Width, canvasForm.panelBitmap.Height);
            //DrawToBitmap(canvasForm.panelBitmap, new Rectangle(0, 0, canvasForm.panelBitmap.Width, canvasForm.panelBitmap.Height));
            if (canvasForm.currentPointInTheHistoryArray >=0)
            { 
                canvasForm.g.DrawImage(canvasForm.Undo[canvasForm.currentPointInTheHistoryArray],0,0);
                canvasForm.currentPointInTheHistoryArray--;

            }
            else
            {
                canvasForm.Clear();

            }
           
          
            //canvasForm.pnl_Draw.CreateGraphics();
            //canvasForm.pnl_Draw.BackgroundImage = canvasForm.panelBitmap;

            //lock (canvasForm._undoRedoLocker)
            //{
            //    if (canvasForm.UndoStack.Count > 0)
            //    {
            //        canvasForm.RedoStack.Push((Bitmap)canvasForm.myBitmap.Clone());
            //        //canvasForm.myBitmap = canvasForm.UndoStack.Pop();
            //        //canvasForm.g = Graphics.FromImage(canvasForm.myBitmap);
            //        //canvasForm.myBitmap.Dispose();
            //        canvasForm.RedoStack.Push(canvasForm.RedoStack.Pop());
            //        canvasForm.pnl_Draw.BackgroundImage = canvasForm.RedoStack.Peek();
            //        //canvasForm.pnl_Draw.Refresh();
            //    }
            //}
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if(canvasForm.currentPointInTheHistoryArray < canvasForm.Undo.Count - 1)
            {
                canvasForm.currentPointInTheHistoryArray++;
                canvasForm.g.DrawImage(canvasForm.Undo[canvasForm.currentPointInTheHistoryArray], 0, 0);
            }
            
            //lock (canvasForm._undoRedoLocker)
            //{ 
            //     if (canvasForm.RedoStack.Count > 0)
            //     {
            //         canvasForm.UndoStack.Push((Bitmap)canvasForm.myBitmap.Clone());
            //        //canvasForm.myBitmap = canvasForm.UndoStack.Pop();
            //        //canvasForm.g = Graphics.FromImage(canvasForm.myBitmap);
            //        //canvasForm.myBitmap.Dispose();
            //        canvasForm.UndoStack.Push(canvasForm.UndoStack.Pop());
            //         canvasForm.pnl_Draw.BackgroundImage = canvasForm.UndoStack.Peek();

            //     }
            //}
        }

        private void TollBox_Load(object sender, EventArgs e)
        {
            
            btnPen.PerformClick();
            this.SetDesktopLocation(700, 0);

        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            canvasForm.drawCircle = false;
            canvasForm.drawLine = true;
            canvasForm.drawPaint = false;
            canvasForm.drawRectangle = false;

            this.TopMost = true;
    
            this.Show();
        }

       
        private void btnRectangle_Click(object sender, EventArgs e)
        {
            canvasForm.drawCircle = false;
            canvasForm.drawRectangle = true;
            canvasForm.drawLine = false;
            canvasForm.drawPaint = false;

            this.TopMost = true;

            this.Show();

        }

        private void TollBox_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                canvasForm.Hide();
                this.WindowState = FormWindowState.Normal;
                Hide();
                notifyIcon.Visible = true;
                
                

            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
            scrnsht = false;
      

        }

        private void toolStrip1_MouseHover(object sender, EventArgs e)
        {
            this.Focus();
        
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            canvasForm.drawCircle = true;
            canvasForm.drawRectangle = false;
            canvasForm.drawLine = false;
            canvasForm.drawPaint = false;

            this.TopMost = true;

            this.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "Save my new File";
            saveFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                int last = canvasForm.Undo.Count-1;
                int width = canvasForm.Undo[last].Width;
                int height = canvasForm.Undo[last].Height;
             
                using (Bitmap bmp = new Bitmap(width, height)) 
                {
                  
                    canvasForm.Undo[last].Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                }
            }
        }
        static public bool chectFlag = false;
        private void btnZoom_Click(object sender, EventArgs e)
        {
            chectFlag = btnZoom.Checked;
        }
    }

}
