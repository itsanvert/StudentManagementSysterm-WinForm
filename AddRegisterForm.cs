using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class AddRegisterForm : UserControl
    {
        public AddRegisterForm()
        {
            InitializeComponent();
            dgvRegister.CellFormatting += dgvRegister_CellFormatting;
        }

        private void dgvRegister_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Customize formatting if needed
        }

        private void load_dataview()
        {
            string query = "SELECT registerId, registerName, registerDate, facultyId, studentId, amount, deposit, balance FROM tblRegister";
            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    BindingSource bsource = new BindingSource
                    {
                        DataSource = dt
                    };
                    dgvRegister.DataSource = bsource;

                    // Customize column widths if needed
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
                }
            }
        }

        

        

      

        

        

       

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Handle painting if needed
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            string checkQuery = "SELECT COUNT(1) FROM tblFaculty WHERE facultyId = @FacultyId";
            using (SqlCommand checkCmd = new SqlCommand(checkQuery, cDataConn.conn))
            {
                checkCmd.Parameters.AddWithValue("@FacultyId", txtFacultyId.Text);

                try
                {
                    cDataConn.conn.Open();
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Invalid Faculty ID. Please check and try again.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking Faculty ID: {ex.Message}");
                    return;
                }
                finally
                {
                    cDataConn.conn.Close();
                }
            }

            // Insert operation if the Faculty ID is valid
            string query = "INSERT INTO tblRegister (registerName, registerDate, facultyId, studentId, amount, deposit, balance) " +
                           "VALUES (@RegisterName, @RegisterDate, @FacultyId, @StudentId, @Amount, @Deposit, @Balance)";

            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                cmd.Parameters.AddWithValue("@RegisterName", txtRegisterName.Text);
                cmd.Parameters.AddWithValue("@RegisterDate", txtRegisterDate.Text);
                cmd.Parameters.AddWithValue("@FacultyId", txtFacultyId.Text);
                cmd.Parameters.AddWithValue("@StudentId", txtStudentId.Text);
                cmd.Parameters.AddWithValue("@Amount", string.IsNullOrWhiteSpace(txtAmount.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtAmount.Text));
                cmd.Parameters.AddWithValue("@Deposit", string.IsNullOrWhiteSpace(txtDeposit.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtDeposit.Text));
                cmd.Parameters.AddWithValue("@Balance", string.IsNullOrWhiteSpace(txtBalance.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtBalance.Text));

                try
                {
                    cDataConn.conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Register added successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding register: {ex.Message}");
                }
                finally
                {
                    cDataConn.conn.Close();
                }
            }
        }

        private void dgvRegister_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnFilter_Click_1(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tblRegister WHERE registerName = @RegisterName";
            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                cmd.Parameters.AddWithValue("@RegisterName", txtSearch.Text);

                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    BindingSource bsource = new BindingSource
                    {
                        DataSource = dt
                    };
                    dgvRegister.DataSource = bsource;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error filtering data: {ex.Message}");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM tblRegister WHERE registerId = @RegisterId";

            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                cmd.Parameters.AddWithValue("@RegisterId", txtRegisterId.Text);

                try
                {
                    cDataConn.conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Register deleted successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting register: {ex.Message}");
                }
                finally
                {
                    cDataConn.conn.Close();
                }
            }
        }

        private void AddRegisterForm_Load(object sender, EventArgs e)
        {
            load_dataview();
        }

        private void AddModify_Click(object sender, EventArgs e)
        {
            string query = "UPDATE tblRegister SET " +
                           "registerName = @RegisterName, " +
                           "registerDate = @RegisterDate, " +
      
                           "amount = @Amount, " +
                           "deposit = @Deposit, " +
                           "balance = @Balance " +
                           "WHERE registerId = @RegisterId";

            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                cmd.Parameters.AddWithValue("@RegisterName", txtRegisterName.Text);
                cmd.Parameters.AddWithValue("@RegisterDate", txtRegisterDate.Text);
                cmd.Parameters.AddWithValue("@FacultyId", txtFacultyId.Text);
                cmd.Parameters.AddWithValue("@StudentId", txtStudentId.Text);
                cmd.Parameters.AddWithValue("@Amount", string.IsNullOrWhiteSpace(txtAmount.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtAmount.Text));
                cmd.Parameters.AddWithValue("@Deposit", string.IsNullOrWhiteSpace(txtDeposit.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtDeposit.Text));
                cmd.Parameters.AddWithValue("@Balance", string.IsNullOrWhiteSpace(txtBalance.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtBalance.Text));
                cmd.Parameters.AddWithValue("@RegisterId", txtRegisterId.Text);

                try
                {
                    cDataConn.conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Register updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating register: {ex.Message}");
                }
                finally
                {
                    cDataConn.conn.Close();
                }
            }
        }

        private void txtFacultyId_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

