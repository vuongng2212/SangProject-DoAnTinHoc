using System.Collections.Generic;
using TruongTanSang_QuanLyLuongNhanVien.Models;

namespace TruongTanSang_QuanLyLuongNhanVien.Repositories.Interfaces
{
    public interface IBangLuongRepository
    {
        List<BangLuong> LayTatCaBangLuong();
        BangLuong TimBangLuongTheoID(string idBangLuong);
        void ThemBangLuong(BangLuong bangLuong);
        void CapNhatBangLuong(BangLuong bangLuong);
        void XoaBangLuong(string idBangLuong);
    }
}
