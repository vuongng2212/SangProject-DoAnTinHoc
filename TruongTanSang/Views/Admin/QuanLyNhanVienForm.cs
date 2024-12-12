using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
using TruongTanSang_QuanLyLuongNhanVien.Services;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.Admin
{
    public partial class QuanLyNhanVienForm : Form
    {
        private DataTable dt;
        private NhanVienService _nhanVienService;

        public QuanLyNhanVienForm()
        {
            InitializeComponent();
            _nhanVienService = new NhanVienService();
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            var nhanViens = _nhanVienService.LayTatCaNhanVien();

            var filteredNhanViens = nhanViens.Where(nv => nv.Role == "NV").ToList();

            dt = new DataTable();
            dt.Columns.Add("STT", typeof(int));
            dt.Columns.Add("Họ Tên", typeof(string));
            dt.Columns.Add("Địa Chỉ", typeof(string));
            dt.Columns.Add("Số Điện Thoại", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Hệ Số Lương", typeof(double));
            dt.Columns.Add("Mức Lương Cơ Sở", typeof(double));
            dt.Columns.Add("Trạng Thái", typeof(string));

            int stt = 1;
            foreach (var nv in filteredNhanViens)
            {
                dt.Rows.Add(stt++, nv.HoTen, nv.DiaChi, nv.SoDienThoai, nv.Email, nv.HeSoLuong, nv.MucLuongCoSo, nv.TrangThai);
            }

            dataGridViewNhanVien.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtSearchName.Text.Trim();
            string email = txtSearchEmail.Text.Trim();
            string phone = txtSearchPhone.Text.Trim();

            var filteredData = _nhanVienService.TimKiemNhanVien(name, email, phone);

            dt.Clear();
            int stt = 1;
            foreach (var nv in filteredData)
            {
                dt.Rows.Add(stt++, nv.HoTen, nv.DiaChi, nv.SoDienThoai, nv.Email, nv.HeSoLuong, nv.MucLuongCoSo, nv.TrangThai);
            }

            dataGridViewNhanVien.DataSource = dt;
        }
    }
}
