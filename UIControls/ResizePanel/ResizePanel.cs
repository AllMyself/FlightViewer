using System;
using System.Drawing;
using System.Windows.Forms;

namespace UiControls
{
    public partial class ResizePanel : Panel, IScale, ICanResize
    {
        public ResizePanel()
        {
            CanHeightResize = true;
            CanWidthResize = true;
            HeightScale = 1;
            WidthScale = 1;
            
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPrint(e);
            //右边和底下的线在可拉动时不一样
            if (CanHeightResize)
            {
                //Pen pen = new Pen(Color.FromArgb(this.BackColor.R - 20, this.BackColor.G - 20, this.BackColor.B - 20));
                Pen pen = new Pen(Color.Red);
                Rectangle rect=new Rectangle(this.Left, this.Height,this.Width,3);
                e.Graphics.DrawRectangle(pen, rect);
            }
            if (CanWidthResize)
            {
                //Pen pen=new Pen(Color.FromArgb(this.BackColor.R-20,this.BackColor.G-20,this.BackColor.B-20));
                Pen pen = new Pen(Color.Red);
                Rectangle rect = new Rectangle(this.Right, 0, 3, this.Height);
                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

        }

        public double HeightScale { get; private set; }
        public double WidthScale { get; private set; }
        public bool CanHeightResize { get; private set; }
        public bool CanWidthResize { get; private set; }
    }
}
