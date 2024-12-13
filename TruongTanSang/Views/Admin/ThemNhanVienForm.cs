using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
using TruongTanSang_QuanLyLuongNhanVien.Services;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.Admin
{
    public partial class ThemNhanVienForm : Form
    {
        public ThemNhanVienForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var nhanVienService = new NhanVienService();
            if (nhanVienService.ThemNhanVienMoi(
                txtHoTen.Text,
                txtDiaChi.Text,
                txtSoDienThoai.Text,
                txtEmail.Text,
                txtPassword.Text))
            {
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
