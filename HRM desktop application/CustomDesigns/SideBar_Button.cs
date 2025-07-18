using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keyper.CustomDesigns
{
    public class SideBar_Button : Button
    {
        // Properties for Border
        private int _borderRadius = 15;
        private int _borderSize = 1;
        private Color _borderColor = Color.White;
        private Color _backgroundColor = ColorTranslator.FromHtml("#14334C");

        [Category("Appearance")]
        [Description("Button Border Radius.")]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                if (_borderRadius != value)
                {
                    _borderRadius = value;
                    Invalidate(); // Redraw the control
                }
            }
        }

        [Category("Appearance")]
        [Description("Button Border Size (Thickness).")]
        public int BorderSize
        {
            get => _borderSize;
            set
            {
                if (_borderSize != value)
                {
                    _borderSize = value;
                    Invalidate(); // Redraw the control
                }
            }
        }

        [Category("Appearance")]
        [Description("Button Border Color.")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                if (_borderColor != value)
                {
                    _borderColor = value;
                    Invalidate(); // Redraw the control
                }
            }
        }

        [Category("Appearance")]
        [Description("Button Back Color.")]
        public Color BackGroundColor
        {
            get => _backgroundColor;
            set
            {
                if (_backgroundColor != value)
                {
                    _backgroundColor = value;
                    Invalidate(); // Redraw the control
                }
            }
        }

        public SideBar_Button()
        {
            this.SetStyle(ControlStyles.Opaque |
                          ControlStyles.SupportsTransparentBackColor |
                          ControlStyles.AllPaintingInWmPaint, true);
           
            this.BackColor = Color.Transparent;
            this.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = Color.White;
            this.Size = new Size(500, 60);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;   
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Draw the gradient background
            Rectangle fillRect = ClientRectangle;
            fillRect.Inflate(-BorderSize, -BorderSize); // Adjust rectangle for gradient fill

            using (GraphicsPath path = GetRoundedRectangle(fillRect, BorderRadius))
            {
                using (SolidBrush brush = new SolidBrush(_backgroundColor))
                {
                    pevent.Graphics.FillPath(brush, path);
                }
            }

            // Draw the border
            if (BorderSize > 0)
            {
                // Create a slightly larger path for the border
                Rectangle borderRect = ClientRectangle;
                borderRect.Inflate(-BorderSize / 2, -BorderSize / 2); // Inflate path to ensure it’s within the button

                using (GraphicsPath borderPath = GetRoundedRectangle(borderRect, BorderRadius))
                {
                    using (Pen pen = new Pen(BorderColor, BorderSize))
                    {
                        // pen.Alignment = PenAlignment.Center; // Ensure the pen is centered
                        pevent.Graphics.DrawPath(pen, borderPath);
                    }
                }
            }
        }

        private static GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            // Add arcs for each corner
            path.AddArc(rect.X + 1, rect.Y + 1.5f, radius * 2, radius * 2, 180, 90); // Top-left

            path.AddArc(rect.Right - radius * 2.1f, rect.Y + 1.5f, radius * 2, radius * 2, 270, 90); // Top-right

            path.AddArc(rect.Right - radius * 2.1f, rect.Bottom - radius * 2.1f, radius * 2, radius * 2, 0, 90); // Bottom-right

            path.AddArc(rect.X + 1.7f, rect.Bottom - radius * 2.1f, radius * 2, radius * 2, 90, 90); // Bottom-left

            path.CloseFigure();

            return path;
        }
    }
}
