using HRM_desktop_application.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HRM_desktop_application.src
{
    public partial class AddEmployee : UserControl
    {
        public AddEmployee()
        {
            InitializeComponent();
        }


        private void AddEmployee_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear(); // Clear old columns if any

            DataGridViewTextBoxColumn serialColumn = new DataGridViewTextBoxColumn();
            serialColumn.Name = "sr_no";
            serialColumn.HeaderText = "Sr. No.";
            serialColumn.ReadOnly = true;
            dataGridView1.Columns.Add(serialColumn);

            dataGridView1.Columns.Add("employee_id", "Employ ID");
            dataGridView1.Columns.Add("employee_name", "Name");
            dataGridView1.Columns.Add("employee_gender", "Gender");
            dataGridView1.Columns.Add("employee_phoneNo", "Phone Number");
            dataGridView1.Columns.Add("employee_position", "Position");
            dataGridView1.Columns.Add("employee_status", "Status");


            dataGridView1.AutoGenerateColumns = false;
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            try
            {
                string selectQuery = "SELECT * FROM employee_data";
                DataTable dt = (DataTable)Database.ExecuteQuery(selectQuery);

                dataGridView1.Rows.Clear(); // Clear old rows

                int srNo = 1;
                foreach (DataRow row in dt.Rows)
                {
                    dataGridView1.Rows.Add(
                        srNo++, // Sr. No.
                        row["employee_id"],
                        row["employee_name"],
                        row["employee_gender"],
                        row["employee_phoneNo"],
                        row["employee_position"],
                        row["employee_status"]
                    );
                }

                dataGridView1.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load data: " + ex.Message);
            }
        }

        #region ------------------ Add Event ------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (texEmpoyeeID.Text == ""
               || txtfullName.Text == ""
               || cmbGender.Text == ""
               || txtPhoneNumber.Text == ""
               || cmbpostion.Text == ""
               || cmbStatus.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string insertQuery = $@"INSERT INTO employee_data (employee_id, employee_name, employee_gender, employee_phoneNo, employee_position, employee_status)
                                          VALUES ('{texEmpoyeeID.Text}', '{txtfullName.Text}', '{cmbGender.Text}', '{txtPhoneNumber.Text}', '{cmbpostion.Text}', '{cmbStatus.Text}') ";

                    var result = Database.ExecuteQuery(insertQuery); // Executes the query

                    if (Convert.ToInt32(result) > 0)
                    {
                        MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployeeData(); // Refresh grid
                        ClearFields();      // Clear form (optional)
                    }
                    else
                    {
                        MessageBox.Show("Failed to add employee.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        #endregion

        #region ------------------ Update event ------------------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (texEmpoyeeID.Text == "" || txtfullName.Text == "" || cmbGender.Text == ""|| txtPhoneNumber.Text == "" || cmbpostion.Text == "" || cmbStatus.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string updateQuery = $@"UPDATE employee_data SET employee_name = '{txtfullName.Text}', employee_gender = '{cmbGender.Text}', employee_phoneNo = '{txtPhoneNumber.Text}',
                                        employee_position = '{cmbpostion.Text}', employee_status = '{cmbStatus.Text}' WHERE employee_id = '{texEmpoyeeID.Text}'";
                    var result = Database.ExecuteQuery(updateQuery);

                    if (Convert.ToInt32(result) > 0)
                    {
                        MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployeeData(); // Refresh grid
                        ClearFields();      // Clear form (optional)
                    }
                    else
                    {
                        MessageBox.Show("No record was updated. Please check the Employee ID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            string input = txtPhoneNumber.Text;

            // Remove any non-digit characters (optional)
            input = new string(input.Where(char.IsDigit).ToArray());
            txtPhoneNumber.Text = input;
            txtPhoneNumber.SelectionStart = txtPhoneNumber.Text.Length; // keep cursor at end

            if (input.Length > 10)
            {
                MessageBox.Show("Phone number must be exactly 10 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneNumber.Text = input.Substring(0, 10); // Trim extra digits
                txtPhoneNumber.SelectionStart = txtPhoneNumber.Text.Length;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                texEmpoyeeID.Text = row.Cells["employee_id"].Value?.ToString();
                txtfullName.Text = row.Cells["employee_name"].Value?.ToString();
                cmbGender.Text = row.Cells["employee_gender"].Value?.ToString();
                txtPhoneNumber.Text = row.Cells["employee_phoneNo"].Value?.ToString();
                cmbpostion.Text = row.Cells["employee_position"].Value?.ToString();
                cmbStatus.Text = row.Cells["employee_status"].Value?.ToString();
            }
        }

        #endregion

        private void ClearFields()
        {
            texEmpoyeeID.Clear();
            txtfullName.Clear();
            cmbGender.SelectedIndex = -1;
            txtPhoneNumber.Clear();
            cmbpostion.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
        }

        #region ------------------ Delete event ------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Get employee_id from selected row
                        string employeeId = dataGridView1.SelectedRows[0].Cells["employee_id"].Value.ToString();

                        string deleteQuery = $"DELETE FROM employee_data WHERE employee_id = '{employeeId}'";
                        var rowsAffected = Database.ExecuteQuery(deleteQuery);

                        if (Convert.ToInt32(rowsAffected) > 0)
                        {
                            MessageBox.Show("Employee deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadEmployeeData(); // Refresh the DataGridView
                            ClearFields();      // Optional : Clear form fields
                        }
                        else
                        {
                            MessageBox.Show("No record was deleted.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region ------------------ Clear event ------------------
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        #endregion
    }
}
 