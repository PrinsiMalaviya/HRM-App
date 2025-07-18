using HRM_desktop_application.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRM_desktop_application.src
{
    public partial class dashbord : UserControl
    {
        public dashbord()
        {
            InitializeComponent();
        }

        private void dashbord_Load(object sender, EventArgs e)
        {
            LoadEmployeeStats();
        }

        private void LoadEmployeeStats()
        {
            try
            {
                // TOTAL Employees
                string totalQuery = "SELECT COUNT(*) FROM employee_data WHERE employee_id IS NOT NULL";
                DataTable totalResult = (DataTable)Database.ExecuteQuery(totalQuery); // ✅ Fixed
                if (totalResult != null && totalResult.Rows.Count > 0)
                {
                    lblTotalEmployee.Text = totalResult.Rows[0][0].ToString();
                }
                else
                {
                    lblTotalEmployee.Text = "0";
                }


                // ACTIVE Employees
                string activeQuery = "SELECT COUNT(*) FROM employee_data WHERE employee_status = 'Active'";
                DataTable activeResult = (DataTable)Database.ExecuteQuery(activeQuery);
                if (activeResult != null && activeResult.Rows.Count > 0)
                {
                    lblActiveEmployee.Text = activeResult.Rows[0][0].ToString();
                }
                else
                {
                    lblActiveEmployee.Text = "0";
                }

                // INACTIVE Employees
                string inactiveQuery = "SELECT COUNT(*) FROM employee_data WHERE employee_status = 'Inactive'";
                DataTable inactiveResult = (DataTable)Database.ExecuteQuery(inactiveQuery);
                if (inactiveResult != null && inactiveResult.Rows.Count > 0)
                {
                    lblInActiveEmployee.Text = inactiveResult.Rows[0][0].ToString();
                }
                else
                {
                    lblInActiveEmployee.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stats: " + ex.Message);
            }
        }
    }
}
