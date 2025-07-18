using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRM_desktop_application.src.Main_Form
{
    public partial class New_MainForm : Form
    {
        public New_MainForm()
        {
            InitializeComponent();
             SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private async void New_MainForm_Load(object sender, EventArgs e)
        {
            await Task.Delay(100); // Wait for layout to finish
            LoadUserControl(new dashbord());
            SetActiveTab(btnDasgboard, "Main Page");
        }

        private void btnDasgboard_Click(object sender, EventArgs e)
        {
            LoadUserControl(new dashbord()); // Load default control
            SetActiveTab(btnDasgboard, "Main Page");
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            LoadUserControl(new AddEmployee());
            SetActiveTab(btnAddEmployee, "Add Employee");
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Salary());
            SetActiveTab(btnSalary, "Salary");
        }
        private void LoadUserControl(System.Windows.Forms.UserControl uc)
        {
            panel5.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel5.Controls.Add(uc);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Loginpage loginObj = new Loginpage();
            loginObj.ShowDialog();
        }

        private void SetActiveTab(Button activeButton, string tabName)
        {
            lblSelectedTab.Visible = true;
            lblSelectedTab.Text = tabName;

            // Define selected color
            Color selectedColor = Color.FromArgb(242, 245, 248);

            // List of all buttons
            Button[] allButtons = { btnDasgboard, btnAddEmployee, btnSalary};

            // Matching label lines
            Label[] allLines = { lblDeviceLine, lblUserLine, lblAttendanceLine };

            for (int i = 0; i < allButtons.Length; i++)
            {
                Button btn = allButtons[i];
                Label line = allLines[i];

                bool isActive = (btn == activeButton);

                // Set button style
                btn.BackColor = isActive ? selectedColor : Color.Transparent;
                btn.FlatAppearance.MouseOverBackColor = selectedColor;
                btn.FlatAppearance.MouseDownBackColor = selectedColor;
                btn.ForeColor = isActive ? Color.Black : Color.FromArgb(149, 152, 153);

                // Set line visibility
                line.Visible = isActive;
            }
        }
    }
}
