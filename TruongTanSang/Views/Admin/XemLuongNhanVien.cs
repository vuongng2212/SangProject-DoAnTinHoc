using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Services;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
using TruongTanSang_QuanLyLuongNhanVien.Views.NhanVien;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.Admin
{
    public partial class XemLuongNhanVienForm : Form
    {
        private readonly string _tenNhanVien;
        private readonly LuongService _luongService;
        private readonly NhanVienRepository _nhanVienRepository;

        public XemLuongNhanVienForm(string tenNhanVien)
        {
            InitializeComponent();
            _tenNhanVien = tenNhanVien;
            _luongService = new LuongService();
            _nhanVienRepository = new NhanVienRepository();
            LoadYears();
            LoadDashboard();
        }

        private void LoadYears()
        {
            // Thêm các năm vào ComboBox
            for (int year = DateTime.Now.Year - 5; year <= DateTime.Now.Year; year++)
            {
                comboBoxYear.Items.Add(year);
            }

            // Chọn năm hiện tại làm giá trị mặc định
            comboBoxYear.SelectedItem = DateTime.Now.Year;
            lblYear.Text = $"Năm: {comboBoxYear.SelectedItem}";
        }

        private void LoadDashboard()
        {
            // Hiển thị tên nhân viên
            lblTenNhanVien.Text = $"Thông tin lương: {_tenNhanVien}";

            // Lấy ID nhân viên từ tên nhân viên
            string idNhanVien = GetIdNhanVienByName(_tenNhanVien);

            // Lấy thông tin nhân viên
            var nhanVien = _nhanVienRepository.TimNhanVienTheoMa(idNhanVien);

            // Lấy bảng lương của nhân viên
            var bangLuongs = _luongService.LayBangLuongTheoNhanVien(idNhanVien);

            // Tạo bảng thông tin lương
            DataTable dt = new DataTable();
            dt.Columns.Add("Tháng");
            dt.Columns.Add("Lương Thực Nhận");

            // Thêm dữ liệu lương vào DataTable
            if (comboBoxYear.SelectedItem != null)
            {
                int selectedYear = (int)comboBoxYear.SelectedItem;
                foreach (var bl in bangLuongs)
                {
                    if (bl.Nam == selectedYear)
                    {
                        double luongThucNhan = _luongService.TinhLuongThucNhan(nhanVien, bl);
                        dt.Rows.Add($"Tháng {bl.Thang}", luongThucNhan);
                    }
                }
            }

            // Gán dữ liệu cho DataGridView
            dataGridViewLuong.DataSource = dt;

            // Format cột tiền tệ
            if (dataGridViewLuong.Columns.Count > 1)
            {
                dataGridViewLuong.Columns[1].DefaultCellStyle.Format = "N0";
            }
        }

        private string GetIdNhanVienByName(string tenNhanVien)
        {
            var nhanViens = _nhanVienRepository.LayTatCaNhanVien();
            return nhanViens.FirstOrDefault(nv => nv.HoTen == tenNhanVien)?.MaNV;
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (dataGridViewLuong.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridViewLuong.SelectedRows[0].Index;
                string thangText = dataGridViewLuong.Rows[selectedIndex].Cells[0].Value.ToString();
                string thangSo = thangText.Replace("Tháng ", "").Trim();
                
                int nam = (int)comboBoxYear.SelectedItem;
                ChiTietLuongForm chiTietLuongForm = new ChiTietLuongForm(
                    thangSo, 
                    GetIdNhanVienByName(_tenNhanVien), 
                    nam);
                chiTietLuongForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tháng để xem chi tiết.");
            }
        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxYear.SelectedItem != null)
            {
                lblYear.Text = $"Năm: {comboBoxYear.SelectedItem}";
                LoadDashboard();
            }
        }
    }
}
