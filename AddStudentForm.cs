using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class AddStudentForm : UserControl
    {
        public AddStudentForm()
        {
            InitializeComponent();
            dgvStudent.CellFormatting += dgvStudent_CellFormatting;
        }
        private void dgvStudent_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvStudent.Columns["photo"].Index && e.Value != DBNull.Value)
            {
                byte[] imageData = (byte[])e.Value;
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Image img = Image.FromStream(ms);
                    // Resize the image to a smaller size
                    int desiredWidth = 40; // Adjust this value to make the image smaller
                    int desiredHeight = 40; // Adjust this value to make the image smaller
                    Image resizedImg = new Bitmap(img, new Size(desiredWidth, desiredHeight));

                    e.Value = resizedImg;
                    e.FormattingApplied = true;
                }
            }
        }




        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private void load_dataview()
        {
            string query = "SELECT studentId, firstName, lastName, latinName, Sex, DOB, POB, Address_, phoneNumber, photo, email, userName, password_ FROM tblStudent";
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
                    dgvStudent.DataSource = bsource;

                    dgvStudent.Columns[0].Width = 70;
                    dgvStudent.Columns[1].Width = 130;
                    dgvStudent.Columns[2].Width = 130;
                    dgvStudent.Columns[3].Width = 130;
                    dgvStudent.Columns[4].Width = 130;
                    dgvStudent.Columns[5].Width = 130;
                    dgvStudent.Columns[6].Width = 130;
                    dgvStudent.Columns[7].Width = 130;
                    dgvStudent.Columns[8].Width = 130;
                    dgvStudent.Columns[9].Width = 130;
                    dgvStudent.Columns[10].Width = 130;
                    dgvStudent.Columns[11].Width = 130;
                    dgvStudent.Columns[12].Width = 130;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
                }
            }
        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvStudent.Rows[e.RowIndex];

                txtStudentId.Text = selectedRow.Cells["studentId"].Value?.ToString() ?? string.Empty;
                txtFirstName.Text = selectedRow.Cells["firstName"].Value?.ToString() ?? string.Empty;
                txtLastName.Text = selectedRow.Cells["lastName"].Value?.ToString() ?? string.Empty;
                txtLatinName.Text = selectedRow.Cells["latinName"].Value?.ToString() ?? string.Empty;
                cboSex.Text = selectedRow.Cells["Sex"].Value?.ToString() ?? string.Empty;
                txtDOB.Text = selectedRow.Cells["DOB"].Value?.ToString() ?? string.Empty;
                txtPOB.Text = selectedRow.Cells["POB"].Value?.ToString() ?? string.Empty;
                txtAddress.Text = selectedRow.Cells["Address_"].Value?.ToString() ?? string.Empty;
                txtPhoneNum.Text = selectedRow.Cells["phoneNumber"].Value?.ToString() ?? string.Empty;
                txtEmail.Text = selectedRow.Cells["email"].Value?.ToString() ?? string.Empty;
                txtUserName.Text = selectedRow.Cells["userName"].Value?.ToString() ?? string.Empty;
                txtPassword.Text = selectedRow.Cells["password_"].Value?.ToString() ?? string.Empty;

                if (selectedRow.Cells["photo"].Value is byte[] photo)
                {
                    using (MemoryStream ms = new MemoryStream(photo))
                    {
                        pbImage.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pbImage.Image = null;
                }
            }
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {
            load_dataview();
            dgvStudent.Columns["photo"].Width = 40; // Width of the column for profile images
            dgvStudent.RowTemplate.Height = 40; // Adjust row height for small images
        }



        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Select a Photo";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pbImage.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM tblStudent WHERE firstName = @FirstName";
            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", txtSearch.Text);

                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    BindingSource bsource = new BindingSource
                    {
                        DataSource = dt
                    };
                    dgvStudent.DataSource = bsource;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error filtering data: {ex.Message}");
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO tblStudent (firstName, lastName, latinName, Sex, DOB, POB, Address_, phoneNumber, photo, email, userName, password_) " +
                           "VALUES (@FirstName, @LastName, @LatinName, @Sex, @DOB, @POB, @Address, @PhoneNumber, @Photo, @Email, @UserName, @Password)";

            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@LatinName", txtLatinName.Text);
                cmd.Parameters.AddWithValue("@Sex", cboSex.Text);
                cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                cmd.Parameters.AddWithValue("@POB", txtPOB.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNum.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                if (pbImage.Image != null)
                {
                    byte[] imageBytes = ImageToByteArray(pbImage.Image);
                    cmd.Parameters.AddWithValue("@Photo", imageBytes);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Photo", DBNull.Value);
                }

                try
                {
                    cDataConn.conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student added successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding student: {ex.Message}");
                }
                finally
                {
                    cDataConn.conn.Close();
                }
            }
        }

        private void AddModify_Click(object sender, EventArgs e)
        {
            string query = "UPDATE tblStudent SET " +
                           "firstName = @FirstName, " +
                           "lastName = @LastName, " +
                           "latinName = @LatinName, " +
                           "Sex = @Sex, " +
                           "DOB = @DOB, " +
                           "POB = @POB, " +
                           "Address_ = @Address, " +
                           "phoneNumber = @PhoneNumber, " +
                           "photo = @Photo, " +
                           "email = @Email, " +
                           "userName = @UserName, " +
                           "password_ = @Password " +
                           "WHERE studentId = @StudentId";

            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@LatinName", txtLatinName.Text);
                cmd.Parameters.AddWithValue("@Sex", cboSex.Text);
                cmd.Parameters.AddWithValue("@DOB", txtDOB.Text);
                cmd.Parameters.AddWithValue("@POB", txtPOB.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNum.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@StudentId", txtStudentId.Text);

                if (pbImage.Image != null)
                {
                    byte[] imageBytes = ImageToByteArray(pbImage.Image);
                    cmd.Parameters.AddWithValue("@Photo", imageBytes);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Photo", DBNull.Value);
                }

                try
                {
                    cDataConn.conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student updated successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating student: {ex.Message}");
                }
                finally
                {
                    cDataConn.conn.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM tblStudent WHERE studentId = @StudentId";

            using (SqlCommand cmd = new SqlCommand(query, cDataConn.conn))
            {
                cmd.Parameters.AddWithValue("@StudentId", txtStudentId.Text);

                try
                {
                    cDataConn.conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student deleted successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting student: {ex.Message}");
                }
                finally
                {
                    cDataConn.conn.Close();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
