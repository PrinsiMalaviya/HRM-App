using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Keyper.CustomDesigns
{
    public class Custom_Panel : Panel
    {
        #region --------------------------------- Properties ---------------------------------------
        private int _cornerRadius = 30;
        private Color borderColor = Color.White;
        private float borderSize = 0.1f;

        [Category("Appearance")]
        [Description("Corner radius for rounded corners.")]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = value; Invalidate(); }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }
        public Color BackGroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; Invalidate(); }
        }
        public float BorderSize
        {
            get { return borderSize; }
            set { borderSize = value; Invalidate(); }
        }
        #endregion

        #region --------------------------------- Contructor ---------------------------------------
        public Custom_Panel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.Opaque |
                        ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.White;
            this.Font = new Font("Roboto", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = Color.WhiteSmoke;
            this.Size = new Size(500, 400);
            this.Cursor = Cursors.Hand;
        }
        #endregion

        #region --------------------------------- On Paint ---------------------------------------
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            rect.Inflate(-1, -1);

            using (GraphicsPath path = GetRoundedRectanglePath(rect, _cornerRadius))
            {
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                using (Pen pen = new Pen(borderColor, borderSize))
                {
                    g.FillPath(brush, path); // Fill background
                    g.DrawPath(pen, path); // Draw rounded border
                }
            }
        }
        #endregion
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; // Set WS_EX_LAYERED style to allow transparency
                return cp;
            }
        }
        #region --------------------------------- Draw Rounded Corner ---------------------------------------
        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            // Add arcs for each corner
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        #endregion

    }
}
