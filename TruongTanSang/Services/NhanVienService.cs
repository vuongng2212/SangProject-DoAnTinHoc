using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
namespace TruongTanSang_QuanLyLuongNhanVien.Services
{
    internal class NhanVienService
    {
        private NhanVienRepository _nhanVienRepository;

        public NhanVienService()
        {
            _nhanVienRepository = new NhanVienRepository();
        }

        public List<NhanVien> LayTatCaNhanVien()
        {
            return _nhanVienRepository.LayTatCaNhanVien();
        }

        public List<NhanVien> TimKiemNhanVien(string name, string email, string phone)
        {
            var nhanViens = LayTatCaNhanVien();
            return nhanViens.Where(nv =>
                (string.IsNullOrEmpty(name) || nv.HoTen.ToLower().Contains(name.ToLower())) &&
                (string.IsNullOrEmpty(email) || nv.Email.ToLower().Contains(email.ToLower())) &&
                (string.IsNullOrEmpty(phone) || nv.SoDienThoai.ToLower().Contains(phone.ToLower()))
            ).ToList();
        }
    }
}
