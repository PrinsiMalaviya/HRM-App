using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Keyper.CustomDesigns
{
    public class Img_Button : Button
    {
        private int borderSize = 0;
        private int borderRadius = 10;
        private Color borderColor = Color.PaleVioletRed;
        private System.Drawing.Image image_icon;

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
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        [Category("Custom")]
        public System.Drawing.Image Image_Icon
        {
            get => image_icon;
            set
            {
                image_icon = value;
                this.Invalidate(); // Redraw the control
            }
        }

        //Constructor
        public Img_Button()
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
            base.OnPaint(pevent);

            int imagePadding = 15;
            int textPadding = 10;

            Rectangle rectSurface = this.ClientRectangle;

            // Create rounded rectangle path
            using (GraphicsPath path = GetFigurePath(rectSurface, borderRadius))
            {

                //path.AddArc(0, 0, borderRadius, borderRadius, 180, 90);
                //path.AddArc(this.Width - borderRadius, 0, borderRadius, borderRadius, 270, 90);
                //path.AddArc(this.Width - borderRadius   , this.Height - borderRadius, borderRadius, borderRadius, 0, 90);
                //path.AddArc(0, this.Height - borderRadius, borderRadius, borderRadius, 90, 90);
               // path.CloseAllFigures();

                // Apply the rounded region
                this.Region = new Region(path);

                // Clear background using anti-aliasing
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Brush backgroundBrush = new SolidBrush(this.BackColor))
                using (Pen pen = new Pen(borderColor, borderSize))
                {
                    pevent.Graphics.FillPath(backgroundBrush, path);
                    pevent.Graphics.DrawPath(pen, path);
                }

                // Draw the image
                if (image_icon != null)
                {
                    int imgY = (this.Height - image_icon.Height) / 2;
                    pevent.Graphics.DrawImage(image_icon, imagePadding, imgY, image_icon.Width, image_icon.Height);
                }

                // Draw the text
                using (StringFormat sf = new StringFormat()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near
                })
                {
                    Rectangle textRect = new Rectangle(
                        (image_icon != null ? image_icon.Width + imagePadding + textPadding : imagePadding),
                        0,
                        this.Width - (image_icon != null ? image_icon.Width + imagePadding + textPadding : 0),
                        this.Height
                    );

                    using (Brush textBrush = new SolidBrush(this.ForeColor))
                    {
                        pevent.Graphics.DrawString(this.Text, this.Font, textBrush, textRect, sf);
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
