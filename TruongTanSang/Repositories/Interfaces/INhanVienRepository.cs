using System.Collections.Generic;
using TruongTanSang_QuanLyLuongNhanVien.Models;

namespace TruongTanSang_QuanLyLuongNhanVien.Repositories.Interfaces
{
    public interface INhanVienRepository
    {
        List<NhanVien> LayTatCaNhanVien();
        NhanVien TimNhanVienTheoMa(string maNV);
        void ThemNhanVien(NhanVien nhanVien);
        void CapNhatNhanVien(NhanVien nhanVien);
        void XoaNhanVien(string maNV);
        NhanVien DangNhap(string maNV, string matKhau);
        void GhiLaiTatCaNhanVien(List<NhanVien> nhanViens);
    }
}
