using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using TruongTanSang_QuanLyLuongNhanVien.Models.Enums;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Interfaces;

namespace TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations
{
    public class NhanVienRepository : INhanVienRepository
    {
        private const string FILE_PATH = @"..\..\Data\nhanvien.txt";

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
                    TrangThai = (TrangThaiNhanVien)Enum.Parse(typeof(TrangThaiNhanVien), fields[8]),
                    Role = fields[9],
                };
                nhanViens.Add(nhanVien);
            }

            return nhanViens;
        }

        public void ThemNhanVien(NhanVien nhanVien)
        {
            try
            {
                // Kiểm tra xem file có tồn tại và có dữ liệu không
                bool fileExists = File.Exists(FILE_PATH);
                bool hasContent = fileExists && new FileInfo(FILE_PATH).Length > 0;

                // Tạo chuỗi dữ liệu cho nhân viên mới
                string data = $"{nhanVien.MaNV}|{nhanVien.HoTen}|{nhanVien.DiaChi}|{nhanVien.SoDienThoai}|{nhanVien.Email}|{nhanVien.HeSoLuong}|{nhanVien.MucLuongCoSo}|{nhanVien.Password}|{(int)nhanVien.TrangThai}|{nhanVien.Role}";

                // Nếu file đã có dữ liệu, thêm ký tự xuống dòng trước dữ liệu mới
                if (hasContent)
                {
                    // Kiểm tra xem ký tự cuối cùng của file có phải là newline không
                    string lastChar = File.ReadAllText(FILE_PATH);
                    if (!lastChar.EndsWith(Environment.NewLine))
                    {
                        data = Environment.NewLine + data;
                    }
                }

                // Ghi dữ liệu vào file
                File.AppendAllText(FILE_PATH, data + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}");
            }
        }

        public void CapNhatNhanVien(NhanVien nhanVien)
        {

            var nhanViens = LayTatCaNhanVien();
            var nvTmp = nhanViens.Find(nv => nv.MaNV == nhanVien.MaNV);

            Console.WriteLine(nvTmp.DiaChi);

            var index = nhanViens.FindIndex(nv => nv.MaNV == nhanVien.MaNV);
            if (index >= 0)
            {
                nhanViens[index] = nvTmp;
                nhanViens[index].HoTen = nhanVien.HoTen;
                nhanViens[index].DiaChi = nhanVien.DiaChi;
                nhanViens[index].SoDienThoai = nhanVien.SoDienThoai;
                nhanViens[index].Email = nhanVien.Email;
                nhanViens[index].Password = nhanVien.Password;
                GhiLaiTatCaNhanVien(nhanViens);
            }
            else
            {
                MessageBox.Show($"Lỗi khi sửa nhân viên!");
            }
        }

        public void XoaNhanVien(string maNV)
        {
            var nhanViens = LayTatCaNhanVien();
            var nhanVien = nhanViens.FirstOrDefault(nv => nv.MaNV == maNV);

            if (nhanVien != null)
            {
                nhanVien.TrangThai = TrangThaiNhanVien.NghiViec;
                GhiLaiTatCaNhanVien(nhanViens);
            }
        }

        public NhanVien DangNhap(string soDienThoai, string matKhau)
        {
            var nhanVien = LayTatCaNhanVien().Find(nv => nv.SoDienThoai == soDienThoai);
            return (nhanVien != null && nhanVien.KiemTraMatKhau(matKhau)) ? nhanVien : null;
        }

        public void GhiLaiTatCaNhanVien(List<NhanVien> nhanViens)
        {
            try
            {
                // Tạo một danh sách để chứa các dòng dữ liệu
                var lines = new List<string>();

                // Duyệt qua từng nhân viên và tạo chuỗi dữ liệu
                foreach (var nv in nhanViens)
                {
                    lines.Add($"{nv.MaNV}|{nv.HoTen}|{nv.DiaChi}|{nv.SoDienThoai}|{nv.Email}|{nv.HeSoLuong}|{nv.MucLuongCoSo}|{nv.Password}|{(int)nv.TrangThai}|{nv.Role}");
                }

                // Ghi toàn bộ dữ liệu vào file
                File.WriteAllText(FILE_PATH, string.Join("\n", lines)); // Ghi tất cả dữ liệu vào file
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi ghi lại tất cả nhân viên: {ex.Message}"); // Hiển thị thông báo lỗi
            }
        }

        public NhanVien TimNhanVienTheoTen(string tenNhanVien)
        {
            try
            {
                var nhanViens = LayTatCaNhanVien();
                return nhanViens.FirstOrDefault(nv => 
                    nv.HoTen.Equals(tenNhanVien, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return null;
            }
        }

        public NhanVien TimNhanVienTheoMa(string idNhanVien)
        {
            try
            {
                var nhanViens = LayTatCaNhanVien();
                return nhanViens.FirstOrDefault(nv => nv.MaNV == idNhanVien);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return null;
            }
        }
    }
}