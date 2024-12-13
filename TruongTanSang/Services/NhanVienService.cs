using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
using System.Windows.Forms;
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
        public NhanVien FindNhanVien(string name, string email, string phone)
        {
            var nhanViens = LayTatCaNhanVien(); // Lấy tất cả nhân viên từ file

            // Tìm nhân viên dựa trên các tiêu chí
            return nhanViens.FirstOrDefault(nv =>
                (string.IsNullOrEmpty(name) || nv.HoTen.Equals(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(email) || nv.Email.Equals(email, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(phone) || nv.SoDienThoai.Equals(phone, StringComparison.OrdinalIgnoreCase))
            );
        }

        public void XoaNhanVien(string maNV)
        {
            _nhanVienRepository.XoaNhanVien(maNV);
        }

        public void CapNhatNhanVien(NhanVien nhanVien)
        {
            _nhanVienRepository.CapNhatNhanVien(nhanVien);
        }

        public (bool isValid, string errorMessage) KiemTraDuLieuNhanVien(string hoTen, string diaChi, string sdt, string email, string password)
        {
            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                return (false, "Họ tên không được để trống!");
            }

            // Kiểm tra địa chỉ
            if (string.IsNullOrWhiteSpace(diaChi))
            {
                return (false, "Địa chỉ không được để trống!");
            }

            // Kiểm tra số điện thoại
            if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^\d{10}$"))
            {
                return (false, "Số điện thoại phải có đúng 10 chữ số!");
            }

            // Kiểm tra email
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern))
            {
                return (false, "Email không đúng định dạng!");
            }

            // Kiểm tra mật khẩu
            if (string.IsNullOrWhiteSpace(password) || password.Length <= 6)
            {
                return (false, "Mật khẩu phải có ít nhất 7 ký tự!");
            }

            return (true, string.Empty);
        }

        public bool ThemNhanVien(NhanVien nhanVien)
        {
            var kiemTra = KiemTraDuLieuNhanVien(
                nhanVien.HoTen,
                nhanVien.DiaChi,
                nhanVien.SoDienThoai,
                nhanVien.Email,
                nhanVien.Password
            );

            if (!kiemTra.isValid)
            {
                MessageBox.Show(kiemTra.errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _nhanVienRepository.ThemNhanVien(nhanVien);
            return true;
        }
    }
}
