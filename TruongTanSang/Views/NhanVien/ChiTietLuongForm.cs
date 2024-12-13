using System;
using System.Drawing;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
using TruongTanSang_QuanLyLuongNhanVien.Services;
using TruongTanSang_QuanLyLuongNhanVien.Models;

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
        private Label lblTT;
        private BangLuong _currentBangLuong;

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
                lblTT = new Label
                {
                    Location = new Point(lblTienThuong.Location.X, lblTienThuong.Location.Y),
                    Size = new Size(100, 20),
                    Text = "Tiền thường: ",
                    Font = new Font("Segoe UI", 9F)
                };

                txtTienThuong = new TextBox
                {
                    Location = new Point(lblTienThuong.Location.X + 100, lblTienThuong.Location.Y),
                    Size = new Size(150, 20),
                    Text = _currentBangLuong?.TienThuong.ToString("N0") ?? "0"
                };

                Button btnCapNhat = new Button
                {
                    Text = "Cập nhật",
                    Location = new Point(txtTienThuong.Location.X+160, txtTienThuong.Location.Y-5),
                    Size = new Size(100, 30)
                };
                btnCapNhat.Click += BtnCapNhat_Click;

                this.Controls.Add(lblTT);
                this.Controls.Add(txtTienThuong);
                this.Controls.Add(btnCapNhat);

                lblTienThuong.Visible = false;
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtTienThuong.Text.Replace(",", ""), out double tienThuong))
                {
                    MessageBox.Show("Vui lòng nhập số tiền hợp lệ!", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (tienThuong < 0)
                {
                    MessageBox.Show("Tiền thưởng không thể là số âm!", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _currentBangLuong.TienThuong = tienThuong;
                bool success = _luongService.CapNhatLuongThang(_thang, _idNhanVien, _nam, tienThuong);

                if (success)
                {
                    MessageBox.Show("Cập nhật tiền thưởng thành công!", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadChiTietLuong();
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Cập nhật tiền thưởng thất bại!", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChiTietLuong()
        {
            lblThang.Text = $"Chi tiết lương cho {_thang}/{_nam}";

            var chiTietLuong = _luongService.LayChiTietLuongTheoThang(_thang, _idNhanVien, _nam);
            _currentBangLuong = chiTietLuong.BangLuong;

            if (_currentBangLuong == null)
            {
                MessageBox.Show("Không tìm thấy thông tin lương cho thời gian này!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lblMaBangLuong.Text = $"Mã Bảng Lương: {_currentBangLuong.IDBangLuong}";
            lblIDNhanVien.Text = $"ID Nhân Viên: {_currentBangLuong.IDNhanVien}";
            lblTienThuong.Text = $"Tiền Thưởng: {_currentBangLuong.TienThuong:N0} VNĐ";
            lblBaoHiemXaHoi.Text = $"Bảo Hiểm Xã Hội: {_currentBangLuong.BaoHiemXaHoi:N0} VNĐ";
            lblLuongThucNhan.Text = $"Lương Thực Nhận: {chiTietLuong.LuongThucNhan:N0} VNĐ";

            if (_isAdmin && txtTienThuong != null)
            {
                txtTienThuong.Text = _currentBangLuong.TienThuong.ToString("N0");
            }
        }
    }
}
