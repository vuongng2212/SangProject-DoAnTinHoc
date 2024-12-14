using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Interfaces;
using System.Windows;
using System.Windows.Forms;

namespace TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations
{
    public class BangLuongRepository : IBangLuongRepository
    {
        private const string FILE_PATH = @"..\..\Data\bangluong.txt";

        public List<BangLuong> LayTatCaBangLuong()
        {
            var bangLuongs = new List<BangLuong>();
            var lines = File.ReadAllLines(FILE_PATH);

            foreach (var line in lines)
            {
                var fields = line.Split('|');
                var bangLuong = new BangLuong
                {
                    IDBangLuong = fields[0],
                    IDNhanVien = fields[1],
                    TienThuong = double.Parse(fields[2]),
                    BaoHiemXaHoi = double.Parse(fields[3]),
                    Thang = int.Parse(fields[4]),
                    Nam = int.Parse(fields[5])
                };
                bangLuongs.Add(bangLuong);
            }

            return bangLuongs;
        }

        public BangLuong TimBangLuongTheoID(string idBangLuong)
        {
            var bangLuongs = LayTatCaBangLuong();
            return bangLuongs.Find(bl => bl.IDBangLuong == idBangLuong);
        }

        public bool ThemBangLuong(BangLuong bangLuong)
        {
            try
            {
                // Kiểm tra xem file có tồn tại và có dữ liệu không
                bool fileExists = File.Exists(FILE_PATH);
                bool hasContent = fileExists && new FileInfo(FILE_PATH).Length > 0;

                // Tạo chuỗi dữ liệu cho nhân viên mới
                string data = $"{bangLuong.IDBangLuong}|{bangLuong.IDNhanVien}|{bangLuong.TienThuong}|{bangLuong.BaoHiemXaHoi}|{bangLuong.Thang}|{bangLuong.Nam}";

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
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}");
                return false;
            }
        }

        public void XoaBangLuong(string idBangLuong)
        {
            var bangLuongs = LayTatCaBangLuong();
            bangLuongs.RemoveAll(bl => bl.IDBangLuong == idBangLuong);
            GhiLaiTatCaBangLuong(bangLuongs);
        }

        private void GhiLaiTatCaBangLuong(List<BangLuong> bangLuongs)
        {
            try
            {
                var lines = new List<string>();
                foreach (var bl in bangLuongs)
                {
                    lines.Add($"{bl.IDBangLuong}|{bl.IDNhanVien}|{bl.TienThuong}|{bl.BaoHiemXaHoi}|{bl.Thang}|{bl.Nam}");
                }
                File.WriteAllText(FILE_PATH, string.Join("\n", lines));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi ghi lại tất cả bảng lương: {ex.Message}");
            }
        }

        public List<BangLuong> LayBangLuongTheoNhanVien(string idNhanVien)
        {
            var bangLuongs = LayTatCaBangLuong();
            return bangLuongs.Where(bl => bl.IDNhanVien == idNhanVien).ToList();
        }

        public BangLuong LayBangLuongTheoThang(string thang, string idNhanVien, int nam)
        {
            try
            {
                var bangLuongs = LayTatCaBangLuong();
                int thangInt = int.Parse(thang);
                return bangLuongs.FirstOrDefault(bl => 
                    bl.Thang == thangInt && 
                    bl.IDNhanVien == idNhanVien && 
                    bl.Nam == nam);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return null;
            }
        }

        public bool CapNhatBangLuong(BangLuong bangLuong)
        {
            try
            {
                var bangLuongs = LayTatCaBangLuong();
                var index = bangLuongs.FindIndex(bl => bl.IDBangLuong == bangLuong.IDBangLuong);
                
                if (index >= 0)
                {
                    bangLuongs[index] = bangLuong;
                    GhiLaiTatCaBangLuong(bangLuongs);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật bảng lương: {ex.Message}");
                return false;
            }
        }

        public string LayIDBangLuongCuoiCung()
        {
            var bangLuongs = LayTatCaBangLuong();
            if (bangLuongs.Count == 0)
            {
                return "BL000"; 
            }

            var lastID = bangLuongs.OrderByDescending(bl => bl.IDBangLuong).First().IDBangLuong;
            int numericPart = int.Parse(lastID.Substring(2)); // Lấy phần số sau "BL"
            return $"BL{numericPart + 1:D3}"; // Tăng giá trị lên 1 và định dạng lại
        }
    }
}