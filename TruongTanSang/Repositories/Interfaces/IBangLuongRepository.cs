using System.Collections.Generic;
using TruongTanSang_QuanLyLuongNhanVien.Models;

namespace TruongTanSang_QuanLyLuongNhanVien.Repositories.Interfaces
{
    public interface IBangLuongRepository
    {
        List<BangLuong> LayTatCaBangLuong();
        BangLuong TimBangLuongTheoID(string idBangLuong);
        bool ThemBangLuong(BangLuong bangLuong);
        void XoaBangLuong(string idBangLuong);
        List<BangLuong> LayBangLuongTheoNhanVien(string idNhanVien);
        BangLuong LayBangLuongTheoThang(string thang, string idNhanVien, int nam);
        bool CapNhatBangLuong(BangLuong bangLuong);
    }
}
