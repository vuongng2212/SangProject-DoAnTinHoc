using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Phương thức tính lương thực nhận
        public double TinhLuongThucNhan(NhanVien nhanVien)
        {
            // Công thức tính lương: 
            // Lương cơ bản = Hệ số lương * Mức lương cơ sở
            // Lương thực nhận = Lương cơ bản + Tiền thưởng - Bảo hiểm
            double luongCoBan = nhanVien.HeSoLuong * nhanVien.MucLuongCoSo;
            return luongCoBan + TienThuong - BaoHiemXaHoi;
        }

        // Phương thức tính tiền thưởng dựa trên số ngày làm việc
        public double TinhTienThuong()
        {
            // Ví dụ: Mỗi ngày làm việc được 50k tiền thưởng
            return SoNgayLamViec * 50000;
        }
    }
}
