using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Services;

namespace TruongTanSang
{
    public partial class LoginForm : Form
    {
        private readonly AuthService authService;

        public LoginForm(AuthService authService)
        {
            InitializeComponent();
            this.authService = authService;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String user = txtUser.Text;
            String pwd = txtPwd.Text;
            XuLyDangNhap(user, pwd);
        }

        public void XuLyDangNhap(string soDienThoai, string matKhau)
        {
            var nhanVien = authService.DangNhap(soDienThoai, matKhau);
            if (nhanVien != null)
            {
                string role = authService.PhanQuyen(nhanVien);
                if (role == "admin")
                {
                    // Chuyển đến giao diện admin
                    //OpenAdminDashboard();
                    MessageBox.Show("Admin!");

                }
                else
                {
                    // Chuyển đến giao diện nhân viên
                    //OpenEmployeeDashboard();
                    MessageBox.Show("NhanVien!");

                }
            }
            else
            {
                // Thông báo đăng nhập không thành công
                MessageBox.Show("Đăng nhập không thành công!");
            }
        }
    }
}
