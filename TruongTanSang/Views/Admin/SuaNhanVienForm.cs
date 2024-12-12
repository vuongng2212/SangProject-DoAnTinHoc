using System;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using TruongTanSang_QuanLyLuongNhanVien.Services;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.Admin
{
    public partial class SuaNhanVienForm : Form
    {
        private TruongTanSang_QuanLyLuongNhanVien.Models.NhanVien _nhanVien;

        public SuaNhanVienForm(TruongTanSang_QuanLyLuongNhanVien.Models.NhanVien nhanVien)
        {
            InitializeComponent();
            _nhanVien = nhanVien;

            // Gán giá trị cho các trường
            txtHoTen.Text = _nhanVien.HoTen;
            txtDiaChi.Text = _nhanVien.DiaChi;
            txtSoDienThoai.Text = _nhanVien.SoDienThoai;
            txtEmail.Text = _nhanVien.Email;
            txtPassword.Text = _nhanVien.Password;
            // Không cho phép sửa các trường khác
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Logic để lưu thông tin nhân viên
            var updatedNhanVien = new TruongTanSang_QuanLyLuongNhanVien.Models.NhanVien
            {
                MaNV = _nhanVien.MaNV,
                HoTen = txtHoTen.Text,
                DiaChi = txtDiaChi.Text,
                SoDienThoai = txtSoDienThoai.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text // Nếu bạn muốn lưu mật khẩu
            };

            // Gọi service để cập nhật thông tin nhân viên
            var nhanVienService = new NhanVienService();
            nhanVienService.CapNhatNhanVien(updatedNhanVien);

            this.DialogResult = DialogResult.OK; // Để thông báo rằng việc lưu thành công
            this.Close(); // Đóng form
        }
    }
}
