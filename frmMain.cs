using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class frmMain : Form
    {
        private SMS_Compo smsCompo;

        public frmMain()
        {
            InitializeComponent();

            // Initialize the SMS_Compo component
            smsCompo = new SMS_Compo();

            // Set the sizes for the user controls
            smsCompo.AddStudentForm.Size = new Size(874, 575);  // Set size for the student form
            smsCompo.AddFacultyForm.Size = new Size(874, 575);  // Set size for the faculty form
            smsCompo.AddRegisterForm.Size = new Size(874, 575); // Set size for the register form

            // Adding the user controls to the form or a panel
            this.Controls.Add(smsCompo.AddStudentForm);
            this.Controls.Add(smsCompo.AddFacultyForm);
            this.Controls.Add(smsCompo.AddRegisterForm);

            // Set the location to fit within the form
            smsCompo.AddStudentForm.Location = new Point(170, 18);
            smsCompo.AddFacultyForm.Location = new Point(170, 18);
            smsCompo.AddRegisterForm.Location = new Point(170, 18);

            // Initially show the Student form and hide others
            ShowStudentForm();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Load event logic, if any
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            
        }

        

       

        private void btnStudent_Click(object sender, EventArgs e)
        {
            ShowStudentForm();
        }

        private void btnFaculty_Click(object sender, EventArgs e)
        {
            ShowFacultyForm();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //ShowRegisterForm();
        }

        private void ShowStudentForm()
        {
            smsCompo.AddStudentForm.Visible = true;
            smsCompo.AddFacultyForm.Visible = false;
            smsCompo.AddRegisterForm.Visible = false;
        }

        private void ShowFacultyForm()
        {
            smsCompo.AddStudentForm.Visible = false;
            smsCompo.AddFacultyForm.Visible = true;
            smsCompo.AddRegisterForm.Visible = false;
        }

        private void ShowRegisterForm()
        {
            smsCompo.AddStudentForm.Visible = false;
            smsCompo.AddFacultyForm.Visible = false;
            smsCompo.AddRegisterForm.Visible = true;
        }

        private void btnStudent_Click_1(object sender, EventArgs e)
        {
            ShowStudentForm();
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            ShowRegisterForm();
        }

        private void btnFaculty_Click_1(object sender, EventArgs e)
        {
            ShowFacultyForm();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                frmLogin lfrm = new frmLogin();
                lfrm.Show();
                this.Hide();
            }
        }
    }
}
