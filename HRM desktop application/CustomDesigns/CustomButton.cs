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
    public class CustomButton : Button
    {
        // Properties for Border
        private int borderRadius = 15;
        private int borderSize = 1;
        private Color borderColor = Color.FromArgb(42, 111, 168);

        //private Color _backgroundColor = ColorTranslator.FromHtml("#14334C");

        [Category("Appearance")]
        [Description("Button Border Radius.")]
        public int BorderRadius
        {
            get => borderRadius;
            set
            {
                if (borderRadius != value)
                {
                    borderRadius = value;
                    Invalidate(); // Redraw the control
                }
            }
        }

        [Category("Appearance")]
        [Description("Button Border Size (Thickness).")]
        public int BorderSize
        {
            get => borderSize;
            set
            {
                if (borderSize != value)
                {
                    borderSize = value;
                    Invalidate(); // Redraw the control
                }
            }
        }

        [Category("Appearance")]
        [Description("Button Border Color.")]
        public Color BorderColor
        {
            get => borderColor;
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    Invalidate(); // Redraw the control
                }
            }
        }

        [Category("Appearance")]
        [Description("Button Back Color.")]      
        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }
        public CustomButton()
        {
             // Set up the UserControl properties
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.Opaque |
                        ControlStyles.AllPaintingInWmPaint, true);
            //this.DoubleBuffered = true;
            this.BackColor = Color.FromArgb(20,51,76);
            this.ForeColor = Color.White;
            this.Size = new Size(500, 60);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseOverBackColor = this.BackColor;
            this.FlatAppearance.MouseDownBackColor = this.BackColor;     
            Cursor = Cursors.Hand;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {

            base.OnPaint(pevent);

            Rectangle rectSurface = this.ClientRectangle;
            int smoothSize = 2;

            if (borderSize > 0)
                smoothSize = borderSize;

            if (borderRadius > 2) //Rounded button
            {
                using (GraphicsPath pathSurface = GetRoundedRectangle(rectSurface, borderRadius))
                using (Pen penSurface = new Pen(borderColor, smoothSize))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    //Button surface
                    this.Region = new Region(pathSurface);
                    //Draw surface border for HD result
                    pevent.Graphics.DrawPath(penSurface, pathSurface);
                }
            }

            // Draw the button text
            TextRenderer.DrawText(pevent.Graphics, Text, Font, ClientRectangle, ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
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

        // Called when the control's handle is created (after it's fully initialized)
        //protected override void OnHandleCreated(EventArgs e)
        //{
        //    base.OnHandleCreated(e);
        //    this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        //}

        // Event handler: called when parent container's background color changes
        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        // Event handler: called when the button is resized
        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }

    }
}
