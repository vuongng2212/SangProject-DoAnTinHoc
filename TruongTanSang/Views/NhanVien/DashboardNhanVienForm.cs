using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.NhanVien
{
    public partial class DashboardNhanVienForm : Form
    {
        private string _tenNhanVien;

        public DashboardNhanVienForm(string tenNhanVien)
        {
            InitializeComponent();
            _tenNhanVien = tenNhanVien;
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            // Hiển thị tên nhân viên
            lblTenNhanVien.Text = $"Chào mừng, {_tenNhanVien}";

            // Lấy ID nhân viên từ tên nhân viên
            string idNhanVien = GetIdNhanVienByName(_tenNhanVien); // Cần có phương thức để lấy ID từ tên

            // Lấy thông tin nhân viên
            var nhanVienRepository = new NhanVienRepository();
            var nhanVien = nhanVienRepository.TimNhanVienTheoMa(idNhanVien); // Lấy thông tin nhân viên theo ID

            // Lấy bảng lương của nhân viên
            var bangLuongRepository = new BangLuongRepository();
            var bangLuongs = bangLuongRepository.LayBangLuongTheoNhanVien(idNhanVien);

            // Tạo bảng thông tin lương
            DataTable dt = new DataTable();
            dt.Columns.Add("Tháng");
            dt.Columns.Add("Lương Thực Nhận");

            // Thêm dữ liệu lương vào DataTable
            foreach (var bl in bangLuongs)
            {
                // Sử dụng thông tin nhân viên để tính lương thực nhận
                dt.Rows.Add($"Tháng {bl.Thang}", bl.TinhLuongThucNhan(nhanVien));
            }

            // Gán dữ liệu cho DataGridView
            dataGridViewLuong.DataSource = dt;
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            // Lấy tháng đã chọn
            if (dataGridViewLuong.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridViewLuong.SelectedRows[0].Index;
                string thang = dataGridViewLuong.Rows[selectedIndex].Cells[0].Value.ToString();

                // Mở form chi tiết lương cho tháng đã chọn
                ChiTietLuongForm chiTietLuongForm = new ChiTietLuongForm(thang, GetIdNhanVienByName(_tenNhanVien));
                chiTietLuongForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tháng để xem chi tiết.");
            }
        }

        private string GetIdNhanVienByName(string tenNhanVien)
        {
            // Giả sử bạn có một repository để lấy nhân viên
            var nhanVienRepository = new NhanVienRepository();
            var nhanVien = nhanVienRepository.LayTatCaNhanVien().FirstOrDefault(nv => nv.HoTen == tenNhanVien);
            return nhanVien?.MaNV; // Trả về ID nhân viên
        }
    }
}
