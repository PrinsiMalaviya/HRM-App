using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Keyper.CustomDesigns
{
    public class Custom_ImageButton : Button
    {
        private int borderSize = 0;
        private int borderRadius = 10;
        private Color borderColor = Color.PaleVioletRed;
        private Color backgroundColor = Color.FromArgb(90, 154, 214);

        //Properties    
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; this.Invalidate(); }
        }

        //Constructor
        public Custom_ImageButton()
        {
            this.FlatStyle = FlatStyle.Flat;        
            this.Size = new Size(60, 40);
            //this.BackColor = Color.FromArgb(42, 111, 168);
            this.ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseOverBackColor = this.BackColor;
            this.FlatAppearance.MouseDownBackColor = this.BackColor;
            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent); // Keep base for enabled state
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectSurface = this.ClientRectangle;
            int smoothSize = borderSize > 0 ? borderSize : 2;

            using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
            {
                this.Region = new Region(pathSurface); // Rounded corners

                if (!this.Enabled)
                {
                    // Disabled background
                    using (SolidBrush disabledBrush = new SolidBrush(backgroundColor))
                    using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
                    {
                        pevent.Graphics.FillPath(disabledBrush, pathSurface); // Fill with dark color
                        pevent.Graphics.DrawPath(penSurface, pathSurface);    // Border
                    }

                    // Draw image manually (if available)
                    if (this.Image != null)
                    {
                        int imgX = 10;
                        int imgY = ((this.Height - this.Image.Height) / 2) + 1;

                        // Draw the image grayed out (if you want normal image, remove ImageAttributes)
                        using (ImageAttributes attr = new ImageAttributes())
                        {
                            ColorMatrix matrix = new ColorMatrix
                            {
                                Matrix33 = 1f // 50% transparency (optional)
                            };
                            attr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                            pevent.Graphics.DrawImage(
                                this.Image,
                                new Rectangle(imgX, imgY, this.Image.Width, this.Image.Height),
                                0, 0, this.Image.Width, this.Image.Height,
                                GraphicsUnit.Pixel,
                                attr
                            );
                        }
                    }

                    // Draw text
                    int textX = this.Image != null ? this.Image.Width : 0;
                    Rectangle textRect = new Rectangle(textX - 15, -1, this.Width - textX, this.Height);
                    TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, textRect, Color.White,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                }
                else
                {
                    // Draw border for enabled state
                    using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
                    {
                        pevent.Graphics.DrawPath(penSurface, pathSurface);
                    }
                }
            }
        }
        // Called when the control's handle is created (after it's fully initialized)
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }

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

        //Methods
        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
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

      
    }
}
