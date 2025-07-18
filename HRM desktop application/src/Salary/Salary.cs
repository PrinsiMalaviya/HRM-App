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
    public partial class Salary : UserControl
    {
        public Salary()
        {
            InitializeComponent();
        }

        private void Salary_Load(object sender, EventArgs e)
        {
            LoadEmployeeSalaryData();
        }

        private void LoadEmployeeSalaryData()
        {
            try
            {
                string query = "SELECT employee_id, employee_name, employee_position, salary FROM employee_data";
                DataTable dt = (DataTable)Database.ExecuteQuery(query);

                dataGridViewSalary.DataSource = dt;

                // Optional: Rename headers
                dataGridViewSalary.Columns["employee_id"].HeaderText = "Employee ID";
                dataGridViewSalary.Columns["employee_name"].HeaderText = "Name";
                dataGridViewSalary.Columns["employee_position"].HeaderText = "Position";
                dataGridViewSalary.Columns["salary"].HeaderText = "Salary";

                // ✅ Full Row and Auto Column Fill
                dataGridViewSalary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewSalary.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewSalary.MultiSelect = false;

                // Optional clean look
                dataGridViewSalary.ReadOnly = true;
                dataGridViewSalary.AllowUserToAddRows = false;
                dataGridViewSalary.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading salary data: " + ex.Message);
            }
        }

        private void dataGridViewSalary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewSalary.Rows[e.RowIndex];

                txtEmployeeID.Text = row.Cells["employee_id"].Value?.ToString();
                txtFullName.Text = row.Cells["employee_name"].Value?.ToString();
                txtPosition.Text = row.Cells["employee_position"].Value?.ToString();
                txtSalary.Text = row.Cells["salary"].Value?.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtEmployeeID.Text == "" || txtSalary.Text == "")
            {
                MessageBox.Show("Please select an employee and enter salary.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string query = $"UPDATE employee_data SET salary = '{txtSalary.Text}' WHERE employee_id = '{txtEmployeeID.Text}'";
                var result = Database.ExecuteQuery(query);

                if (Convert.ToInt32(result) > 0)
                {
                    MessageBox.Show("Salary updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeeSalaryData(); // refresh grid
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Salary update failed. Please try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtEmployeeID.Clear();
            txtFullName.Clear();
            txtPosition.Clear();
            txtSalary.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
