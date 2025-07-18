using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Keyper.CustomDesigns
{
    public class Custom_BorderPanel : Panel
    {
        private Color borderColor = Color.White;
        private float borderSize = 0.1f;

        private int topLeftRadius = 20;
        private int topRightRadius = 20;
        private int bottomRightRadius = 20;
        private int bottomLeftRadius = 20;

        public int TopLeftRadius
        {
            get { return topLeftRadius; }
            set { topLeftRadius = value; Invalidate(); }
        }
        public int TopRightRadius
        {
            get { return topRightRadius; }
            set { topRightRadius = value; Invalidate(); }
        }
        public int BottomRightRadius
        {
            get { return bottomRightRadius; }
            set { bottomRightRadius = value; Invalidate(); }
        }
        public int BottomLeftRadius
        {
            get { return bottomLeftRadius; }
            set { bottomLeftRadius = value; Invalidate(); }
        }
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }
        public float BorderSize
        {
            get { return borderSize; }
            set { borderSize = value; Invalidate(); }
        }

        public Custom_BorderPanel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.Opaque |
                        ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.White;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = this.ClientRectangle;
            rect.Inflate(-1, -1);

            using (GraphicsPath path = GetRoundedRectanglePath(rect))
            {
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    g.FillPath(brush, path); // Fill background
                }

                using (Pen pen = new Pen(borderColor, borderSize))
                {
                    g.DrawPath(pen, path); // Draw rounded border
                }
            }
        }
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
        private GraphicsPath GetRoundedRectanglePath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();

            int arcTopLeft = TopLeftRadius * 2;
            int arcTopRight = TopRightRadius * 2;
            int arcBottomRight = BottomRightRadius * 2;
            int arcBottomLeft = BottomLeftRadius * 2;

            // Top Left
            if (TopLeftRadius > 0)
                path.AddArc(rect.X, rect.Y, arcTopLeft, arcTopLeft, 180, 90);
            else
                path.AddLine(rect.X, rect.Y, rect.X, rect.Y);

            // Top Edge
            path.AddLine(rect.X + TopLeftRadius, rect.Y, rect.Right - TopRightRadius, rect.Y);

            // Top Right
            if (TopRightRadius > 0)
                path.AddArc(rect.Right - arcTopRight, rect.Y, arcTopRight, arcTopRight, 270, 90);
            else
                path.AddLine(rect.Right, rect.Y, rect.Right, rect.Y);

            // Right Edge
            path.AddLine(rect.Right, rect.Y + TopRightRadius, rect.Right, rect.Bottom - BottomRightRadius);

            // Bottom Right
            if (BottomRightRadius > 0)
                path.AddArc(rect.Right - arcBottomRight, rect.Bottom - arcBottomRight, arcBottomRight, arcBottomRight, 0, 90);
            else
                path.AddLine(rect.Right, rect.Bottom, rect.Right, rect.Bottom);

            // Bottom Edge
            path.AddLine(rect.Right - BottomRightRadius, rect.Bottom, rect.X + BottomLeftRadius, rect.Bottom);

            // Bottom Left
            if (BottomLeftRadius > 0)
                path.AddArc(rect.X, rect.Bottom - arcBottomLeft, arcBottomLeft, arcBottomLeft, 90, 90);
            else
                path.AddLine(rect.X, rect.Bottom, rect.X, rect.Bottom);

            // Left Edge
            path.AddLine(rect.X, rect.Bottom - BottomLeftRadius, rect.X, rect.Y + TopLeftRadius);

            path.CloseFigure();
            return path;
        }

    }
}
