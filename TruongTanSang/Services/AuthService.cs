using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Interfaces;

namespace TruongTanSang_QuanLyLuongNhanVien.Services
{
    public class AuthService
    {
        private readonly INhanVienRepository _nhanVienRepository;

        public AuthService(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }

        public NhanVien DangNhap(string soDienThoai, string matKhau)
        {
            var nhanVien = _nhanVienRepository.DangNhap(soDienThoai, matKhau);
            return nhanVien;
        }

        public string PhanQuyen(NhanVien nhanVien)
        {
            return nhanVien.Role; // Trả về vai trò của nhân viên
        }
    }
}
