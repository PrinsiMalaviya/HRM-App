using System.Drawing;
using System.Windows.Forms;


namespace Keyper.CustomDesigns
{
    public class EnlivenTooltip : ToolTip
    {
        private string tipText = "";
        private Control targetControl;
        private Timer showTimer;
        private Timer hideTimer;
        private Form tooltipForm;

        public EnlivenTooltip()
        {
            tooltipForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                BackColor = Color.Black,
                TopMost = true,
                
                Size = new Size(200, 30)
            };

            this.IsBalloon = true;
            tooltipForm.Paint += TooltipForm_Paint;

            showTimer = new Timer { Interval = 100 };
            showTimer.Tick += (s, e) => ShowTooltip();

            hideTimer = new Timer { Interval = 100 };
            hideTimer.Tick += (s, e) => CheckMouseLeave();
        }

        public void SetTooltip(Control control, string text)
        {
            tipText = text;
            targetControl = control;

            control.MouseEnter += (s, e) => showTimer.Start();
            control.MouseLeave += (s, e) => hideTimer.Start();
        }

        private void ShowTooltip()
        {
            if (targetControl == null) return;
            this.IsBalloon = true;

            using (Graphics g = tooltipForm.CreateGraphics()) 
            {
                SizeF textsize = g.MeasureString(tipText, new Font("Segoe UI", 9));
                tooltipForm.Size = new Size((int)textsize.Width + 20 +10, (int)textsize.Height + 10);
            }
            Point location = targetControl.PointToScreen(new Point(0, targetControl.Height + 5));
            tooltipForm.Location = location;
            tooltipForm.Invalidate();
            tooltipForm.Show();

            showTimer.Stop();
        }

        private void CheckMouseLeave()
        {
            if (!targetControl.Bounds.Contains(targetControl.PointToClient(Control.MousePosition)))
            {
                tooltipForm.Hide();
                hideTimer.Stop();
            }
        }

        private void TooltipForm_Paint(object sender, PaintEventArgs e)
        {
            using (Brush b = new SolidBrush(Color.White))
            using (Font f = new Font("Segoe UI", 9))
            {
                e.Graphics.DrawString(tipText, f, b, new RectangleF(5, 5, tooltipForm.Width - 10, tooltipForm.Height - 10));
            }
        }
    }
}