using HRM_desktop_application.DB;
using HRM_desktop_application.src;
using HRM_desktop_application.src.Main_Form;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace HRM_desktop_application
{
    public partial class Loginpage: Form
    {
        Database db = new Database();

        public Loginpage()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        #region ------------------ Login Page ------------------
        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            AuthenticateUser();
        }
        #endregion

        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            login_password.PasswordChar = login_showPass.Checked ? '\0' : '*';
        }

        private void Loginpage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AuthenticateUser();
                e.Handled = true;
            }
        }

        public void AuthenticateUser()
        {
            if (login_username.Text == "" && login_password.Text == "")
            {
                MessageBox.Show("Please fill all blank fields");
                return;
            }
            else if (login_username.Text == "")
            {
                MessageBox.Show("Please enter username");
                return;
            }
            else if (login_password.Text == "")
            {
                MessageBox.Show("Please enter password");
                return;
            }

            string selectQuery = $@"SELECT * FROM login WHERE username = '{login_username.Text}' AND password = '{login_password.Text}'";
            DataTable dt = Database.ExecuteQuery(selectQuery) as DataTable;

            if (dt != null && dt.Rows.Count > 0)
            {
                // User found - allow login
                this.Hide();
                New_MainForm mainPageObj = new New_MainForm();
                mainPageObj.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void Loginpage_Load(object sender, System.EventArgs e)
        {
           Database.EnsureDatabaseAndTablesExist();
        }
    }
}