using System.Collections.Generic;
using TruongTanSang_QuanLyLuongNhanVien.Models;

namespace TruongTanSang_QuanLyLuongNhanVien.Repositories.Interfaces
{
    public interface INhanVienRepository
    {
        List<NhanVien> LayTatCaNhanVien();
        NhanVien TimNhanVienTheoMa(string idNhanVien);
        NhanVien TimNhanVienTheoTen(string tenNhanVien);
        void ThemNhanVien(NhanVien nhanVien);
        void XoaNhanVien(string idNhanVien);
        void CapNhatNhanVien(NhanVien nhanVien);
        NhanVien DangNhap(string maNV, string password);
    }
}
