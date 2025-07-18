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
        }

        private void btnDasgboard_Click(object sender, EventArgs e)
        {
            LoadUserControl(new dashbord()); // Load default control
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            LoadUserControl(new AddEmployee());
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            LoadUserControl(new Salary());
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
    }
}
