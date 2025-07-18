using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Keyper.CustomDesigns
{
    public class CustomTextBoxForLogin : System.Windows.Forms.UserControl
    {
        private TextBox textBox;
        private PictureBox leftIcon;
        private PictureBox rightIcon; // Second image after the TextBox
        private bool isPasswordVisible = false; // Track the visibility state of the password
        private string _placeholderText;
        private bool isPlaceholderVisible = true;
        private int borderRadius = 7;
        private Color borderColor = Color.Silver;
        private float borderSize = 1;
        public Image leftSideImage { get; set; }
        public Image RightSideEyeOpen { get; set; } // Property for the right side "open" eye image
        public Image RightSideEyeClosed { get; set; } // Property for the right side "closed" eye image

        private char _passwordChar = '\0'; // Default value to not mask the characters
        public char PasswordChar
        {
            get { return _passwordChar; }
            set
            {
                _passwordChar = value;
                textBox.PasswordChar = _passwordChar;
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
        public string PlaceholderText
        {
            get { return _placeholderText; }
            set { _placeholderText = value; Invalidate(); }
        }
        public string TextValue
        {
            get { return textBox.Text; }
            set { textBox.Text = value; Invalidate(); }
        }
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate(); // Refresh the control to redraw with new color
            }
        }

        public float BorderSize
        {
            get { return borderSize; }
            set
            {
                if (value >= 0f)
                {
                    borderSize = value;
                    this.Invalidate(); // Redraw the control to reflect border size change
                }
            }
        }
        public CustomTextBoxForLogin()
        {
            // Set up the UserControl properties
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint, true);

            // Set up the UserControl properties
            this.BackColor = Color.White;
            this.Size = new Size(450, 45); // Set a default size for the UserControl
            // Add the TextBox
            textBox = new TextBox
            {
                PasswordChar = '\0',
                BorderStyle = BorderStyle.None, // No border
                BackColor = Color.White,
                Font = new Font("Lucida Sans", 10.5F),
                Location = new Point(30, 5), // Adjust to leave space for the icon
                Width = this.Width - 80, // Adjust to make space for both images
                Name = this.Name,
            };

            this.Controls.Add(textBox);

            textBox.Enter += (s, e) =>
            {
                if (isPlaceholderVisible)
                {
                    if (textBox.Text == "Password")
                        textBox.PasswordChar = '*';

                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                    isPlaceholderVisible = false;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = _placeholderText;
                    textBox.ForeColor = Color.Gray;
                    isPlaceholderVisible = true;
                }
            };


            // Add the eye icon 
            leftIcon = new PictureBox
            {
                Image = leftSideImage, // Set initial image (eye icon hidden)
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(20, 20), // Icon size
                Location = new Point(10, (this.Height - 20) / 2), // Center icon vertically
                Cursor = Cursors.Hand
            };
            this.Controls.Add(leftIcon);

            // Add the right side image 
            rightIcon = new PictureBox
            {
                Image = RightSideEyeClosed, // Set initial image (right side closed eye)
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(20, 20), // Icon size
                Location = new Point(this.Width - 35, (this.Height - 20) / 2), // Position it after the TextBox
                Cursor = Cursors.Hand
            };
            this.Controls.Add(rightIcon);

            // Add click event to toggle password visibility and change the images immediately
            rightIcon.Click += (sender, e) =>
            {
                // Toggle password visibilit
                isPasswordVisible = !isPasswordVisible;

                // Change the TextBox PasswordChar and image immediately
                textBox.PasswordChar = isPasswordVisible ? '\0' : '*';
                rightIcon.Image = isPasswordVisible ? RightSideEyeOpen : RightSideEyeClosed;

            };

            this.Resize += (sender, e) =>
            {
                if (leftIcon.Visible)
                {
                    leftIcon.Location = new Point(15, (this.Height - leftIcon.Height) / 2);
                    leftIcon.Size = new Size(20, 20);
                }

                if (rightIcon.Visible)
                {
                    rightIcon.Width = 21;
                    rightIcon.Height = 21;
                    rightIcon.Location = new Point(this.Width - rightIcon.Width - 15, (this.Height - rightIcon.Height) / 2);
                }

                AdjustTextBoxLayout(); // Apply layout changes
            };
            textBox.BorderStyle = BorderStyle.None;
            this.Invalidate();
        }
        private void AdjustTextBoxLayout()
        {
            int padding = 10;
            int leftOffset = leftIcon.Visible ? leftIcon.Right + padding : padding;
            int rightOffset = rightIcon.Visible ? this.Width - rightIcon.Left + padding : padding;

            textBox.Location = new Point(leftOffset, ((this.Height - textBox.Height) / 2));
            textBox.Width = this.Width - leftOffset - rightOffset;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = this.ClientRectangle;
            rect.Inflate(-1, -1); // Make border fit nicely inside control

            using (GraphicsPath path = GetRoundedRectanglePath(rect, borderRadius))
            {
                //this.Region = new Region(path); // Apply rounded corners

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Fill the background
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                using (Pen borderPen = new Pen(borderColor, BorderSize)) // Change color or thickness if needed
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(borderPen, path);
                }


            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Prevent the background from being painted
            // This allows the parent's background to show through.
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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.DesignMode)
            {
                // Placeholder setup
                textBox.Text = _placeholderText;
                textBox.ForeColor = Color.Gray;
                isPlaceholderVisible = true;

                if (textBox.Text == "Password")
                    textBox.PasswordChar = '\0';
            }

            // Setup images and visibility
            if (leftSideImage != null)
            {
                leftIcon.Image = leftSideImage;
                leftIcon.Visible = true;
            }
            else
            {
                leftIcon.Visible = false;
            }

            if (RightSideEyeClosed != null)
            {
                rightIcon.Image = RightSideEyeClosed;
                rightIcon.Visible = true;
            }
            else
            {
                rightIcon.Visible = false;
            }

            AdjustTextBoxLayout();
        }

        private static GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90); // Top-left corner
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90); // Top-right corner
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90); // Bottom-right corner
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90); // Bottom-left corner
            path.CloseFigure();
            return path;
        }

    }

}
