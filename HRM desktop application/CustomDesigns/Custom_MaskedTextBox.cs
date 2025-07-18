using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Keyper.CustomDesigns
{
    public class Custom_MaskedTextBox : System.Windows.Forms.UserControl
    {
        private MaskedTextBox _maskedTextBox;
        private Color _borderColor = Color.Silver;
        private int _borderSize = 1;
        private int _borderRadius = 7;
        public event EventHandler TextValueChanged;

        #region ------------------ property ------------------

        [Category("Custom")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Custom")]
        public int BorderSize
        {
            get => _borderSize;
            set { _borderSize = value; Invalidate(); }
        }

        [Category("Custom")]
        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = Math.Max(0, value); Invalidate(); }
        }

        [Category("Custom")]
        public string Mask
        {
            get => _maskedTextBox.Mask;
            set => _maskedTextBox.Mask = value;
        }

        [Category("Custom")]
        public override string Text
        {
            get => _maskedTextBox.Text;
            set => _maskedTextBox.Text = value;
        }

        #endregion

        #region ------------------ Constructor------------------
        public Custom_MaskedTextBox()
        {
            // Set up the UserControl properties
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.Opaque |
                        ControlStyles.AllPaintingInWmPaint, true);

            // Set up the UserControl properties
            this.BackColor = Color.White;
            this.Size = new Size(450, 45); // Set a default size for the UserControl

            // Add the TextBox
            _maskedTextBox = new MaskedTextBox
            {
                PasswordChar = '\0',
                BorderStyle = BorderStyle.None, // No border
                BackColor = Color.White,
                Font = new Font("Lucida sans", 11F),
                Location = new Point(10, 5), // Adjust to leave space for the icon
                Width = this.Width - 80, // Adjust to make space for both images
                Name = this.Name,
                Mask = "990.990.9.990",
            };
            this.Controls.Add(_maskedTextBox);

            // Ensure the controls adjust when resized
            this.Resize += (sender, e) =>
            {
                _maskedTextBox.Width = this.Width - 20;
                int x = (this.Width - _maskedTextBox.Width) / 2;
                int y = (this.Height - _maskedTextBox.Height) / 2;
                _maskedTextBox.Location = new Point(x, y);
            };
            _maskedTextBox.BorderStyle = BorderStyle.None;
            _maskedTextBox.KeyPress += MaskedTextBox_KeyPress;
            _maskedTextBox.TextChanged += (s, e) =>
            {
                TextValueChanged?.Invoke(this, e);
            };
        }
        #endregion


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = this.ClientRectangle;
            rect.Inflate(-1, -1); // Make border fit nicely inside control

            using (GraphicsPath path = GetRoundedRectanglePath(rect, _borderRadius))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Fill the background
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                using (Pen borderPen = new Pen(BorderColor, _borderSize)) // Change color or thickness if needed
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(borderPen, path);
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
        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
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


        private void MaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }       
    }
}
