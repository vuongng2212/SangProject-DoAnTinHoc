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

        public NhanVien()
        {
        }

        public NhanVien(string maNV, string diaChi, string soDienThoai, string hoTen, string email, double heSoLuong, double mucLuongCoSo, string password, TrangThaiNhanVien trangThai, string role)
        {
            MaNV = maNV;
            DiaChi = diaChi;
            SoDienThoai = soDienThoai;
            HoTen = hoTen;
            Email = email;
            HeSoLuong = heSoLuong;
            MucLuongCoSo = mucLuongCoSo;
            Password = password;
            TrangThai = trangThai;
            Role = role;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        // Phương thức kiểm tra mật khẩu
        public bool KiemTraMatKhau(string matKhauNhapVao)
        {
            return Password == matKhauNhapVao;
        }
    }
}
