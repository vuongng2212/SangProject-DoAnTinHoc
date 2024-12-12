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
        private BangLuongRepository _bangLuongRepository;

        public LuongService()
        {
            _bangLuongRepository = new BangLuongRepository();
        }

        public double TinhLuongThucNhan(NhanVien nhanVien)
        {
            // Logic để tính lương thực nhận
            return nhanVien.HeSoLuong * nhanVien.MucLuongCoSo; 
        }

        public List<BangLuong> LayBangLuongTheoNhanVien(string idNhanVien)
        {
            return _bangLuongRepository.LayBangLuongTheoNhanVien(idNhanVien);
        }
    }
}
