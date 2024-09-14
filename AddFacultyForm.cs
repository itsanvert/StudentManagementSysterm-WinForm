using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class AddFacultyForm : UserControl
    {
        public AddFacultyForm()
        {
            InitializeComponent();
            dgvFaculty.CellFormatting += DgvFaculty_CellFormatting;
            Load += AddFacultyForm_Load;
        }

        private void DgvFaculty_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Customize formatting if needed
        }

        private void LoadDataView()
        {
            string query = "SELECT facultyId, facultyName, facultyDes FROM tblFaculty";
            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    dgvFaculty.DataSource = new BindingSource { DataSource = dt };
                    // Customize column widths if needed
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
                }
            }
        }

        private void DgvFaculty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgvFaculty.Rows[e.RowIndex];
                txtFacultyId.Text = selectedRow.Cells["facultyId"].Value?.ToString() ?? string.Empty;
                txtFacultyName.Text = selectedRow.Cells["facultyName"].Value?.ToString() ?? string.Empty;
                txtFacultyDes.Text = selectedRow.Cells["facultyDes"].Value?.ToString() ?? string.Empty;
            }
        }

        private void AddFacultyForm_Load(object sender, EventArgs e)
        {
            LoadDataView();
            // Customize DataGridView column widths and row height if needed
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            // Implement import functionality if needed
        }


        private void BtnAdd_Click_1(object sender, EventArgs e)
        {
            // Adding a faculty record
            string query = "INSERT INTO tblFaculty (facultyName, facultyDes) VALUES (@FacultyName, @FacultyDes)";
            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                cmd.Parameters.AddWithValue("@FacultyName", txtFacultyName.Text);
                cmd.Parameters.AddWithValue("@FacultyDes", txtFacultyDes.Text);

                ExecuteNonQuery(cmd, "Faculty added successfully.");
            }
        }

        private void BtnFilter_Click_1(object sender, EventArgs e)
        {
            // Filtering faculty records
            string query = "SELECT * FROM tblFaculty WHERE facultyName LIKE @FacultyName";
            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                cmd.Parameters.AddWithValue("@FacultyName", "%" + txtSearch.Text + "%");

                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    dgvFaculty.DataSource = new BindingSource { DataSource = dt };
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error filtering data: {ex.Message}");
                }
            }
        }

        private void AddModify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFacultyId.Text))
            {
                // If FacultyId is empty, add a new record
                BtnAdd_Click_1(sender, e);
            }
            else
            {
                // If FacultyId is not empty, update an existing record
                string query = "UPDATE tblFaculty SET facultyName = @FacultyName, facultyDes = @FacultyDes WHERE facultyId = @FacultyId";
                using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
                {
                    cmd.Parameters.AddWithValue("@FacultyName", txtFacultyName.Text);
                    cmd.Parameters.AddWithValue("@FacultyDes", txtFacultyDes.Text);
                    cmd.Parameters.AddWithValue("@FacultyId", txtFacultyId.Text);

                    ExecuteNonQuery(cmd, "Faculty updated successfully.");
                }
            }
        }

        private void BtnClear_Click_1(object sender, EventArgs e)
        {
            // Clear all input fields
            txtFacultyId.Clear();
            txtFacultyName.Clear();
            txtFacultyDes.Clear();
            txtSearch.Clear(); // Assuming you have a search text box to clear
        }

        private void ExecuteNonQuery(SqlCommand cmd, string successMessage)
        {
            try
            {
                cDataConn.conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show(successMessage);
                LoadDataView(); // Refresh the DataGridView after operation
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing command: {ex.Message}");
            }
            finally
            {
                cDataConn.conn.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO tblFaculty (facultyName, facultyDes) VALUES (@FacultyName, @FacultyDes)";
            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                cmd.Parameters.AddWithValue("@FacultyName", txtFacultyName.Text);
                cmd.Parameters.AddWithValue("@FacultyDes", txtFacultyDes.Text);

                ExecuteNonQuery(cmd, "Faculty added successfully.");
            }
        }

        private void dgvFaculty_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddFacultyForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
