using System;

namespace TruongTanSang_QuanLyLuongNhanVien.Models
{
    public class BangLuong
    {
        // Thuộc tính
        public string IDBangLuong { get; set; }
        public string IDNhanVien { get; set; }
        public double TienThuong { get; set; }
        public double BaoHiemXaHoi { get; set; }
        public int SoNgayLamViec { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }

        public BangLuong()
        {
        }

        public BangLuong(string iDBangLuong, string iDNhanVien, double tienThuong, double baoHiemXaHoi, int soNgayLamViec, int thang, int nam)
        {
            IDBangLuong = iDBangLuong;
            IDNhanVien = iDNhanVien;
            TienThuong = tienThuong;
            BaoHiemXaHoi = baoHiemXaHoi;
            SoNgayLamViec = soNgayLamViec;
            Thang = thang;
            Nam = nam;
        }



        // Phương thức tính lương thực nhận
        public double TinhLuongThucNhan(NhanVien nhanVien)
        {
            double luongCoBan = nhanVien.HeSoLuong * nhanVien.MucLuongCoSo;
            return luongCoBan + TienThuong - BaoHiemXaHoi;
        }

        // Phương thức tính tiền thưởng dựa trên số ngày làm việc
        public double TinhTienThuong()
        {
            return SoNgayLamViec * 50000;
        }
    }
}