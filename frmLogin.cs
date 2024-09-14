using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string query = "select * from tblStudent where userName=N'" + txtUsername.Text + "' and password_ = N'" + txtPassword.Text + "'";
            SqlCommand cmd = new SqlCommand(query, cDataConn.conn);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Login Successfully");
                this.Hide();
                frmMain frmM = new frmMain();
                frmM.ShowDialog();
            }
            else
            {
                MessageBox.Show("User not found. Login fail");
            }
        }

        private void ShowPass_CheckedChanged(object sender, EventArgs e)
        {
           txtPassword.PasswordChar = ShowPass.Checked ? '\0' : '*';
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

