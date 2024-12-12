using System;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.NhanVien
{
    public partial class ChiTietLuongForm : Form
    {
        private string _thang;
        private string _idNhanVien;
        private int _nam;

        public ChiTietLuongForm(string thang, string idNhanVien, int nam)
        {
            InitializeComponent();
            _thang = thang;
            _idNhanVien = idNhanVien;
            _nam = nam;
            LoadChiTietLuong();
        }

        private void LoadChiTietLuong()
        {
            // Hiển thị thông tin chi tiết lương cho tháng và năm
            lblThang.Text = $"Chi tiết lương cho {_thang}/{_nam}";

            // Giả sử bạn đã có một repository để lấy bảng lương
            var bangLuongRepository = new BangLuongRepository();
            var bangLuongs = bangLuongRepository.LayTatCaBangLuong(); // Lấy tất cả bảng lương

            // Tìm bảng lương cho tháng và năm đã chọn
            foreach (var bl in bangLuongs)
            {
                if (bl.Thang.ToString() == _thang.Split(' ')[1] && bl.IDNhanVien == _idNhanVien && bl.Nam == _nam) // Kiểm tra tháng, ID nhân viên và năm
                {
                    var nhanVien = new NhanVienRepository().TimNhanVienTheoMa(bl.IDNhanVien);
                    lblMaBangLuong.Text = $"Mã Bảng Lương: {bl.IDBangLuong}";
                    lblIDNhanVien.Text = $"ID Nhân Viên: {bl.IDNhanVien}";
                    lblTienThuong.Text = $"Tiền Thưởng: {bl.TienThuong}";
                    lblBaoHiemXaHoi.Text = $"Bảo Hiểm Xã Hội: {bl.BaoHiemXaHoi}";
                    lblSoNgayLamViec.Text = $"Số Ngày Làm Việc: {bl.SoNgayLamViec}";
                    lblLuongThucNhan.Text = $"Lương Thực Nhận: {bl.TinhLuongThucNhan(nhanVien)}";
                    break; // Dừng vòng lặp khi đã tìm thấy bảng lương
                }
            }
        }
    }
}
