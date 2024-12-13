using System;
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

        public ChiTietLuongForm(String thang, string idNhanVien, int nam)
        {
            InitializeComponent();
            _thang = thang;
            _idNhanVien = idNhanVien;
            _nam = nam;
            _luongService = new LuongService();
            LoadChiTietLuong();
        }

        private void LoadChiTietLuong()
        {
            // Hiển thị thông tin chi tiết lương cho tháng và năm
            lblThang.Text = $"Chi tiết lương cho {_thang}/{_nam}";

            var chiTietLuong = _luongService.LayChiTietLuongTheoThang(_thang, _idNhanVien, _nam);

            if (chiTietLuong.BangLuong == null)
            {
                MessageBox.Show("Không tìm thấy thông tin lương cho thời gian này!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Hiển thị thông tin chi tiết
            lblMaBangLuong.Text = $"Mã Bảng Lương: {chiTietLuong.BangLuong.IDBangLuong}";
            lblIDNhanVien.Text = $"ID Nhân Viên: {chiTietLuong.BangLuong.IDNhanVien}";
            lblTienThuong.Text = $"Tiền Thưởng: {chiTietLuong.BangLuong.TienThuong:N0} VNĐ";
            lblBaoHiemXaHoi.Text = $"Bảo Hiểm Xã Hội: {chiTietLuong.BangLuong.BaoHiemXaHoi:N0} VNĐ";
            lblLuongThucNhan.Text = $"Lương Thực Nhận: {chiTietLuong.LuongThucNhan:N0} VNĐ";
        }
    }
}
