using System;
using System.Collections.Generic;
using System.IO;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using TruongTanSang_QuanLyLuongNhanVien.Models.Enums;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Interfaces;

namespace TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations
{
    public class NhanVienRepository : INhanVienRepository
    {
        private const string FILE_PATH = @"Data\nhanvien.txt";

        public List<NhanVien> LayTatCaNhanVien()
        {
            var nhanViens = new List<NhanVien>();
            var lines = File.ReadAllLines(FILE_PATH);

            foreach (var line in lines)
            {
                var fields = line.Split('|');
                var nhanVien = new NhanVien
                {
                    MaNV = fields[0],
                    HoTen = fields[1],
                    DiaChi = fields[2],
                    SoDienThoai = fields[3],
                    Email = fields[4],
                    HeSoLuong = double.Parse(fields[5]),
                    MucLuongCoSo = double.Parse(fields[6]),
                    Password = fields[7],
                    TrangThai = (TrangThaiNhanVien)Enum.Parse(typeof(TrangThaiNhanVien), fields[8])
                };
                nhanViens.Add(nhanVien);
            }

            return nhanViens;
        }

        public NhanVien TimNhanVienTheoMa(string maNV)
        {
            var nhanViens = LayTatCaNhanVien();
            return nhanViens.Find(nv => nv.MaNV == maNV);
        }

        public void ThemNhanVien(NhanVien nhanVien)
        {
            using (var writer = new StreamWriter(FILE_PATH, true))
            {
                writer.WriteLine($"{nhanVien.MaNV}|{nhanVien.HoTen}|{nhanVien.DiaChi}|{nhanVien.SoDienThoai}|{nhanVien.Email}|{nhanVien.HeSoLuong}|{nhanVien.MucLuongCoSo}|{nhanVien.Password}|{(int)nhanVien.TrangThai}");
            }
        }

        public void CapNhatNhanVien(NhanVien nhanVien)
        {
            var nhanViens = LayTatCaNhanVien();
            var index = nhanViens.FindIndex(nv => nv.MaNV == nhanVien.MaNV);
            if (index >= 0)
            {
                nhanViens[index] = nhanVien;
                GhiLaiTatCaNhanVien(nhanViens);
            }
        }

        public void XoaNhanVien(string maNV)
        {
            var nhanViens = LayTatCaNhanVien();
            nhanViens.RemoveAll(nv => nv.MaNV == maNV);
            GhiLaiTatCaNhanVien(nhanViens);
        }

        public NhanVien DangNhap(string maNV, string matKhau)
        {
            var nhanVien = TimNhanVienTheoMa(maNV);
            return (nhanVien != null && nhanVien.KiemTraMatKhau(matKhau)) ? nhanVien : null;
        }

        private void GhiLaiTatCaNhanVien(List<NhanVien> nhanViens)
        {
            using (var writer = new StreamWriter(FILE_PATH))
            {
                foreach (var nv in nhanViens)
                {
                    writer.WriteLine($"{nv.MaNV}|{nv.HoTen}|{nv.DiaChi}|{nv.SoDienThoai}|{nv.Email}|{nv.HeSoLuong}|{nv.MucLuongCoSo}|{nv.Password}|{(int)nv.TrangThai}");
                }
            }
        }
    }
}