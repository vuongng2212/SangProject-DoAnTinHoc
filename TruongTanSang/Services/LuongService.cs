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

        public bool CapNhatLuongThang(string thang, string idNhanVien, int nam, double tienThuong)
        {
            try
            {
                var bangLuong = _bangLuongRepository.LayBangLuongTheoThang(thang, idNhanVien, nam);
                if (bangLuong != null)
                {
                    bangLuong.TienThuong = tienThuong;
                    return _bangLuongRepository.CapNhatBangLuong(bangLuong);
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return false;
            }
        }

        public bool ThemTienThuongThangHienTai(string maNhanVien, double tienThuong)
        {
            try
            {
                string thang = DateTime.Now.Month.ToString();
                int nam = DateTime.Now.Year;

                // Kiểm tra xem đã có bảng lương cho tháng hiện tại chưa
                var bangLuong = _bangLuongRepository.LayBangLuongTheoThang(thang, maNhanVien, nam);
                
                if (bangLuong == null)
                {
                    // Nếu chưa có, tạo mới bảng lương
                    string newIDBangLuong = _bangLuongRepository.LayIDBangLuongCuoiCung(); // Lấy ID mới
                    bangLuong = new BangLuong
                    {
                        IDBangLuong = newIDBangLuong, // Sử dụng ID mới
                        IDNhanVien = maNhanVien,
                        Thang = int.Parse(thang),
                        Nam = nam,
                        TienThuong = tienThuong,
                        BaoHiemXaHoi = 300000 // Có thể set giá trị mặc định hoặc tính toán
                    };
                    return _bangLuongRepository.ThemBangLuong(bangLuong);
                }
                else
                {
                    // Nếu đã có, cập nhật tiền thưởng
                    bangLuong.TienThuong += tienThuong;
                    return _bangLuongRepository.CapNhatBangLuong(bangLuong);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
