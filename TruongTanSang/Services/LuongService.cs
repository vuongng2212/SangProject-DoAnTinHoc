using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
using TruongTanSang_QuanLyLuongNhanVien.Models;
namespace TruongTanSang_QuanLyLuongNhanVien.Services
{
    internal class LuongService
    {
        private readonly BangLuongRepository _bangLuongRepository;
        private readonly NhanVienRepository _nhanVienRepository;

        public LuongService()
        {
            _bangLuongRepository = new BangLuongRepository();
            _nhanVienRepository = new NhanVienRepository();
        }

        public double TinhLuongThucNhan(NhanVien nhanVien, BangLuong bangLuong)
        {
            double luongCoBan = nhanVien.HeSoLuong * nhanVien.MucLuongCoSo;
            
            double tienThuong = bangLuong.TienThuong;
            
            double baoHiemXaHoi = bangLuong.BaoHiemXaHoi;
            
            double luongThucNhan = luongCoBan + tienThuong - baoHiemXaHoi;
            
            return luongThucNhan;
        }

        public List<BangLuong> LayBangLuongTheoNhanVien(string idNhanVien)
        {
            return _bangLuongRepository.LayBangLuongTheoNhanVien(idNhanVien);
        }

        public (BangLuong BangLuong, double LuongThucNhan) LayChiTietLuongTheoThang(
            string thang, string idNhanVien, int nam)
        {
            var bangLuongs = LayBangLuongTheoNhanVien(idNhanVien);
            
            if (!int.TryParse(thang, out int thangInt))
            {
                return (null, 0);
            }

            var bangLuong = bangLuongs.FirstOrDefault(bl => 
                bl.Thang == thangInt && 
                bl.Nam == nam);

            if (bangLuong == null)
            {
                return (null, 0);
            }

            var nhanVien = _nhanVienRepository.TimNhanVienTheoMa(bangLuong.IDNhanVien);
            if (nhanVien == null)
            {
                return (null, 0);
            }

            double luongThucNhan = TinhLuongThucNhan(nhanVien, bangLuong);
            return (bangLuong, luongThucNhan);
        }
    }
}
