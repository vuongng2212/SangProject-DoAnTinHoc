using System;
using System.Drawing;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
using TruongTanSang_QuanLyLuongNhanVien.Services;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.NhanVien
{
    public partial class ChiTietLuongForm : Form
    {
        private readonly String _thang;
        private readonly string _idNhanVien;
        private readonly int _nam;
        private readonly LuongService _luongService;
        private readonly bool _isAdmin;
        private TextBox txtTienThuong;

        public ChiTietLuongForm(String thang, string idNhanVien, int nam, bool isAdmin = false)
        {
            InitializeComponent();
            _thang = thang;
            _idNhanVien = idNhanVien;
            _nam = nam;
            _isAdmin = isAdmin;
            _luongService = new LuongService();
            LoadChiTietLuong();
            ConfigureFormForRole();
        }

        private void ConfigureFormForRole()
        {
            if (_isAdmin)
            {
                Button btnCapNhat = new Button
                {
                    Text = "Cập nhật",
                    Location = new Point(200, 250),
                    Size = new Size(100, 30)
                };
                btnCapNhat.Click += BtnCapNhat_Click;

                txtTienThuong = new TextBox
                {
                    Location = lblTienThuong.Location,
                    Size = new Size(150, 20),
                    Text = lblTienThuong.Text.Split(':')[1].Trim().Replace(" VNĐ", "")
                };
                
                this.Controls.Add(btnCapNhat);
                this.Controls.Add(txtTienThuong);
                lblTienThuong.Visible = false;
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                int tienThuong = int.Parse(txtTienThuong.Text);
                
                bool success = _luongService.CapNhatLuongThang(_thang, _idNhanVien, _nam, tienThuong);
                
                if (success)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadChiTietLuong();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietLuong()
        {
            lblThang.Text = $"Chi tiết lương cho {_thang}/{_nam}";

            var chiTietLuong = _luongService.LayChiTietLuongTheoThang(_thang, _idNhanVien, _nam);

            if (chiTietLuong.BangLuong == null)
            {
                MessageBox.Show("Không tìm thấy thông tin lương cho thời gian này!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lblMaBangLuong.Text = $"Mã Bảng Lương: {chiTietLuong.BangLuong.IDBangLuong}";
            lblIDNhanVien.Text = $"ID Nhân Viên: {chiTietLuong.BangLuong.IDNhanVien}";
            lblTienThuong.Text = $"Tiền Thưởng: {chiTietLuong.BangLuong.TienThuong:N0} VNĐ";
            lblBaoHiemXaHoi.Text = $"Bảo Hiểm Xã Hội: {chiTietLuong.BangLuong.BaoHiemXaHoi:N0} VNĐ";
            lblLuongThucNhan.Text = $"Lương Thực Nhận: {chiTietLuong.LuongThucNhan:N0} VNĐ";
        }
    }
}
