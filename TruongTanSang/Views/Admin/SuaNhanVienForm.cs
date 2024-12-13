using System;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using TruongTanSang_QuanLyLuongNhanVien.Services;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.Admin
{
    public partial class SuaNhanVienForm : Form
    {
        private string _maNV;
        private NhanVienService _nhanVienService;

        public SuaNhanVienForm(Models.NhanVien nhanVien)
        {
            InitializeComponent();
            _nhanVienService = new NhanVienService();
            _maNV = nhanVien.MaNV;

            // Gán giá trị cho các trường
            txtHoTen.Text = nhanVien.HoTen;
            txtDiaChi.Text = nhanVien.DiaChi;
            txtSoDienThoai.Text = nhanVien.SoDienThoai;
            txtEmail.Text = nhanVien.Email;
            txtPassword.Text = nhanVien.Password;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_nhanVienService.CapNhatThongTinNhanVien(
                _maNV,
                txtHoTen.Text,
                txtDiaChi.Text,
                txtSoDienThoai.Text,
                txtEmail.Text,
                txtPassword.Text))
            {
                MessageBox.Show("Cập nhật thông tin nhân viên thành công!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
