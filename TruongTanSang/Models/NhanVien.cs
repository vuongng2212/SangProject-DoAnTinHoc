using System;
using TruongTanSang_QuanLyLuongNhanVien.Models.Enums;

namespace TruongTanSang_QuanLyLuongNhanVien.Models
{
    public class NhanVien
    {
        // Thuộc tính
        public string MaNV { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public double HeSoLuong { get; set; }
        public double MucLuongCoSo { get; set; }
        public string Password { get; set; }
        public TrangThaiNhanVien TrangThai { get; set; }
        public string Role { get; set; }

        // Phương thức kiểm tra mật khẩu
        public bool KiemTraMatKhau(string matKhauNhapVao)
        {
            return Password == matKhauNhapVao;
        }
    }
}
