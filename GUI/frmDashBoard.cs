using GUI.DTO;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace GUI
{
    public partial class frmDashBoard : Form
    {
        public frmDashBoard(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;
        }

        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        void ChangeAccount(int type)
        {
            if (loginAccount.Type == 2)
            {
                btnManage.Visible = false;
                btnChangePass.Location = new Point(350, 82);
                btnLogout.Location = new Point(604, 82);
            }
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSeller frm = new frmSeller();
            frm.ShowDialog();
            this.Show();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAdminNewDesign frm = new frmAdminNewDesign();
            frm.ShowDialog();
            this.Show();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            frmAccountSettings frm = new frmAccountSettings(loginAccount);
            frm.ShowDialog();
            this.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin lg = new frmLogin();
            lg.Show();
        }

        private void frmDashBoard_Load(object sender, EventArgs e)
        {
            string connectionSTR = Properties.Settings.Default.QLRPConnectionString;
            SqlConnection sqlConn = new SqlConnection(connectionSTR);
            try
            {
                sqlConn.Open();
                string mnv = LoginAccount.StaffID.ToString();
                string sql = "select HoTen from NhanVien where id = '" + mnv + "'";
                string sqlUser = "select UserName from TaiKhoan where idNV ='" + mnv + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlConn);
                SqlDataReader data = cmd.ExecuteReader();
                if (data.Read())
                {
                    lab_name.Text = "Tên nhân viên: " + data.GetString(0) + "";
                    data.Close();
                }
                SqlCommand cmdUser = new SqlCommand(sqlUser, sqlConn);
                SqlDataReader dataUser = cmdUser.ExecuteReader();
                if (dataUser.Read())
                {
                    lab_user.Text = "Tên tài khoản: " + dataUser.GetString(0) + "";
                    dataUser.Close();
                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}