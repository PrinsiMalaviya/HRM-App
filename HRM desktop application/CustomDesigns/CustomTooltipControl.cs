using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Management.Instrumentation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Keyper.CustomDesigns
{

    public class CustomTooltipControl : ToolTip
    {
        private Color _backgroundColor = Color.Black;
        private Color _borderColor = Color.Gray;
        private Color _fontColor = Color.White;
        private FontStyle _fontStyle = FontStyle.Bold;
        Font font = new Font("Lucida Sans", 8f);



        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("dwmapi.dll")]
        private static extern void DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

       
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;
        }


        internal static class NativeMethods
        {
            public const int SWP_NOSIZE = 0x0001;
            public const int SWP_NOZORDER = 0x0004;
            public const int SWP_NOACTIVATE = 0x0010;

            [DllImport("user32.dll", SetLastError = true)]
            public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        }


        public CustomTooltipControl()
        {
            this.OwnerDraw = true;
            this.Popup += CustomTooltipControl_Popup;
            this.Draw += CustomTooltipControl_Draw;
            this.BackColor = Color.White;
            this.UseFading = false;
            this.UseAnimation = false;
        }

        private void CustomTooltipControl_Popup(object sender, PopupEventArgs e)
        {
            string text = this.GetToolTip(e.AssociatedControl);
            Size textSize = TextRenderer.MeasureText(text, new Font(font, _fontStyle));

            int tooltipWidth = textSize.Width + 14;
            int tooltipHeight = textSize.Height + 14;

            e.ToolTipSize = new Size(tooltipWidth, tooltipHeight);
            IntPtr handle = GetHandleFromToolTip();

            if (handle != IntPtr.Zero)
            {
                // Apply rounded corners immediately
                SetTooltipRoundedRegion();

                Timer regionTimer = new Timer
                {
                    Interval = 10
                };
                regionTimer.Tick += (s, args) =>
                {
                    regionTimer.Stop();

                    // Get screen bounds
                    Rectangle screenBounds = Screen.FromControl(e.AssociatedControl).WorkingArea;

                    // Get tooltip window current position
                    GetWindowRect(handle, out RECT rect);
                    int tooltipX = rect.Left;
                    int tooltipY = rect.Top;

                    // Calculate new X if it goes off-screen (10px margin)
                    if (tooltipX + tooltipWidth > screenBounds.Right - 5)
                    {
                        int newX = screenBounds.Right - tooltipWidth - 5;
                        NativeMethods.SetWindowPos(handle, IntPtr.Zero, newX, tooltipY, 0, 0,
                            NativeMethods.SWP_NOZORDER | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
                    }
                    else if (tooltipX < screenBounds.Left + 5)
                    {
                        int newX = screenBounds.Left + 5;
                        NativeMethods.SetWindowPos(handle, IntPtr.Zero, newX, tooltipY, 0, 0,
                            NativeMethods.SWP_NOZORDER | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
                    }
                };
                regionTimer.Start();
            }
        }
        private IntPtr GetHandleFromToolTip()
        {
            var fi = typeof(ToolTip).GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);
            return (fi?.GetValue(this) as NativeWindow)?.Handle ?? IntPtr.Zero;
        }

        private enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }
        private void SetTooltipRoundedRegion()
        {
            var field = typeof(ToolTip).GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);
            NativeWindow window = field?.GetValue(this) as NativeWindow;

            if (window != null && window.Handle != IntPtr.Zero)
            {
                int cornerPref = (int)DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
                DwmSetWindowAttribute(window.Handle, (int)DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE, ref cornerPref, sizeof(int));
            }
        }

        private void CustomTooltipControl_Draw(object sender, DrawToolTipEventArgs e)
        {
            // Custom drawing to avoid the default border
            Graphics g = e.Graphics;
            g.Clear(_backgroundColor); // Background color

            // Draw the text
            using (Brush textBrush = new SolidBrush(_fontColor)) // Text color (adjust as needed)
            {
                g.DrawString(e.ToolTipText, new Font(font, _fontStyle), textBrush, new PointF(7, 7));
            }

            using (Pen borderPen = new Pen(_borderColor, 1)) // Border color 
            {
                g.DrawRectangle(borderPen, 0, 0, e.Bounds.Width, e.Bounds.Height);
            }
        }
    }
}





    





//public class CustomTooltipControl : ToolTip
//{
//    private Color _backgroundColor = Color.Black;
//    private Color _borderColor = Color.White;
//    private Color _fontColor = Color.White;
//    private int radius = 8;
//    private int _bordersize = 2;

//    [DllImport("user32.dll")]
//    private static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

//    [DllImport("user32.dll")]
//    [return: MarshalAs(UnmanagedType.Bool)]
//    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

//    [StructLayout(LayoutKind.Sequential)]
//    public struct RECT
//    {
//        public int Left;
//        public int Top;
//        public int Right;
//        public int Bottom;
//    }

//    private Size GetWindowSize(IntPtr hWnd)
//    {
//        GetWindowRect(hWnd, out RECT rect);
//        return new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
//    }


//    public CustomTooltipControl()
//    {
//        this.OwnerDraw = true;
//        this.Popup += CustomTooltipControl_Popup;
//        this.Draw += CustomTooltipControl_Draw;
//    }

//    private void CustomTooltipControl_Popup(object sender, PopupEventArgs e)
//    {
//        string text = this.GetToolTip(e.AssociatedControl);
//        Size textSize = TextRenderer.MeasureText(text, new Font("Segoe UI", 9));
//        int pointerHeight = 6;
//        int tooltipWidth = textSize.Width + 24;
//        int tooltipHeight = textSize.Height + 10 + pointerHeight;

//        // Calculate initial tooltip size
//        e.ToolTipSize = new Size(tooltipWidth, tooltipHeight);

//        // Delay region setting until tooltip is shown
//        Timer regionTimer = new Timer();
//        regionTimer.Interval = 10;
//        regionTimer.Tick += (s, args) =>
//        {
//            regionTimer.Stop();

//            // Get tooltip window handle
//            IntPtr handle = this.GetHandleFromToolTip();

//            if (handle != IntPtr.Zero)
//            {
//                // Get screen bounds
//                Rectangle screenBounds = Screen.FromControl(e.AssociatedControl).WorkingArea;

//                // Get tooltip window current position
//                GetWindowRect(handle, out RECT rect);
//                int tooltipX = rect.Left;
//                int tooltipY = rect.Top;

//                // Calculate new X if it goes off-screen (10px margin)
//                if (tooltipX + tooltipWidth > screenBounds.Right - 10)
//                {
//                    int newX = screenBounds.Right - tooltipWidth - 10;
//                    NativeMethods.SetWindowPos(handle, IntPtr.Zero, newX, tooltipY, 0, 0,
//                        NativeMethods.SWP_NOZORDER | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
//                }
//                else if (tooltipX < screenBounds.Left + 10)
//                {
//                    int newX = screenBounds.Left + 10;
//                    NativeMethods.SetWindowPos(handle, IntPtr.Zero, newX, tooltipY, 0, 0,
//                        NativeMethods.SWP_NOZORDER | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);
//                }

//                SetTooltipRoundedRegion();
//            }
//        };
//        regionTimer.Start();
//    }
//    private IntPtr GetHandleFromToolTip()
//    {
//        FieldInfo fi = typeof(ToolTip).GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);
//        NativeWindow window = fi?.GetValue(this) as NativeWindow;
//        return window?.Handle ?? IntPtr.Zero;
//    }
//    internal static class NativeMethods
//    {
//        public const int SWP_NOSIZE = 0x0001;
//        public const int SWP_NOZORDER = 0x0004;
//        public const int SWP_NOACTIVATE = 0x0010;

//        [DllImport("user32.dll", SetLastError = true)]
//        public static extern bool SetWindowPos(
//            IntPtr hWnd, IntPtr hWndInsertAfter,
//            int X, int Y, int cx, int cy, uint uFlags);
//    }

//    private void SetTooltipRoundedRegion()
//    {
//        // Get the tooltip window handle using reflection
//        var field = typeof(ToolTip).GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
//        NativeWindow window = field?.GetValue(this) as NativeWindow;

//        if (window != null && window.Handle != IntPtr.Zero)
//        {
//            using (Graphics g = Graphics.FromHwnd(window.Handle))
//            {
//                Rectangle bounds = new Rectangle(Point.Empty, GetWindowSize(window.Handle));
//                using (GraphicsPath path = GetRoundedRectanglePath(bounds, 8))
//                {
//                    Region region = new Region(path);
//                    SetWindowRgn(window.Handle, region.GetHrgn(g), true);
//                }
//            }
//        }
//    }

//    private void CustomTooltipControl_Draw(object sender, DrawToolTipEventArgs e)
//    {
//        Graphics g = e.Graphics;
//        g.SmoothingMode = SmoothingMode.AntiAlias;
//        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

//        Rectangle bounds = e.Bounds;

//        using (GraphicsPath path = GetRoundedRectanglePath(bounds, radius))
//        {
//            // Draw background
//            using (SolidBrush bgBrush = new SolidBrush(_backgroundColor))
//            {
//                g.FillPath(bgBrush, path);
//            }

//            // Draw border
//            using (Pen borderPen = new Pen(_borderColor, _bordersize))
//            {
//                g.DrawPath(borderPen, path);
//            }
//        }

//        // Draw text, centered horizontally and accounting for the pointer height with extra space
//        int pointerHeight = 6; // Match the pointer height
//        int extraPadding = 1; // Add more space above the text (adjust this value as needed)
//        Font font = new Font("Segoe UI", 9);
//        Size textSize = TextRenderer.MeasureText(e.ToolTipText, font);
//        int textX = bounds.X + (bounds.Width - textSize.Width) / 2; // Center horizontally
//        int textY = bounds.Y + 3 + pointerHeight + extraPadding; // Add extra padding above the text
//        Rectangle textRect = new Rectangle(textX, textY, textSize.Width, bounds.Height - 6 - pointerHeight - extraPadding);
//        TextRenderer.DrawText(g, e.ToolTipText, font, textRect, _fontColor, TextFormatFlags.Left);
//    }

//    private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
//    {
//        GraphicsPath path = new GraphicsPath();
//        int diameter = radius * 2;

//        // Define the pointer dimensions
//        int pointerWidth = 12; // Width of the triangle base
//        int pointerHeight = 6; // Height of the triangle
//        int pointerX = rect.X + (rect.Width - pointerWidth) / 2; // Center the pointer horizontally

//        // Start from the top-left corner, but account for the pointer
//        path.AddArc(rect.X, rect.Y + pointerHeight, diameter, diameter, 180, 90); // Top-left corner (shifted down by pointerHeight)

//        // Add the pointer at the top center
//        path.AddLine(rect.X + radius, rect.Y + pointerHeight, pointerX, rect.Y + pointerHeight); // Line to the left base of the pointer
//        path.AddLine(pointerX, rect.Y + pointerHeight, pointerX + pointerWidth / 2, rect.Y); // Line to the tip of the pointer
//        path.AddLine(pointerX + pointerWidth / 2, rect.Y, pointerX + pointerWidth, rect.Y + pointerHeight); // Line to the right base of the pointer

//        // Continue with the rest of the rectangle
//        path.AddLine(pointerX + pointerWidth, rect.Y + pointerHeight, rect.Right - radius, rect.Y + pointerHeight); // Line to the top-right corner
//        path.AddArc(rect.Right - diameter, rect.Y + pointerHeight, diameter, diameter, 270, 90); // Top-right corner
//        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90); // Bottom-right corner
//        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90); // Bottom-left corner
//        path.CloseFigure();

//        return path;
//    }
//}
