namespace ScreenPainter
{
    partial class CanvasForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_Draw = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnl_Draw
            // 
            this.pnl_Draw.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Draw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Draw.Location = new System.Drawing.Point(0, 0);
            this.pnl_Draw.MinimumSize = new System.Drawing.Size(2000, 1400);
            this.pnl_Draw.Name = "pnl_Draw";
            this.pnl_Draw.Size = new System.Drawing.Size(2000, 1400);
            this.pnl_Draw.TabIndex = 0;
            this.pnl_Draw.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Draw_Paint);
            this.pnl_Draw.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pnl_Draw_MouseDoubleClick);
            this.pnl_Draw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnl_Draw_MouseDown);
            this.pnl_Draw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnl_Draw_MouseMove);
            this.pnl_Draw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnl_Draw_MouseUp);
            // 
            // CanvasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(619, 191);
            this.Controls.Add(this.pnl_Draw);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CanvasForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Canvas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CanvasForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnl_Draw;
    }
}