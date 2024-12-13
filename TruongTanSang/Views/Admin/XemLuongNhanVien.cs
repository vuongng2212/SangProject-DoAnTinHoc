using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Services;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
using TruongTanSang_QuanLyLuongNhanVien.Views.NhanVien;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.Admin
{
    public partial class XemLuongNhanVien : Form
    {
        private readonly string _tenNhanVien;
        private readonly LuongService _luongService;
        private readonly NhanVienRepository _nhanVienRepository;
        private readonly bool _isAdmin;

        public XemLuongNhanVien(string tenNhanVien, bool isAdmin = false)
        {
            InitializeComponent();
            _tenNhanVien = tenNhanVien;
            _isAdmin = isAdmin;
            _luongService = new LuongService();
            _nhanVienRepository = new NhanVienRepository();
            LoadYears();
            LoadDashboard();
        }

        private void LoadYears()
        {
            for (int year = DateTime.Now.Year - 5; year <= DateTime.Now.Year; year++)
            {
                comboBoxYear.Items.Add(year);
            }
            comboBoxYear.SelectedItem = DateTime.Now.Year;
            lblYear.Text = $"Năm: {comboBoxYear.SelectedItem}";
        }

        private void LoadDashboard()
        {
            lblTenNhanVien.Text = $"Thông tin lương: {_tenNhanVien}";
            string idNhanVien = GetIdNhanVienByName(_tenNhanVien);
            var nhanVien = _nhanVienRepository.TimNhanVienTheoMa(idNhanVien);
            var bangLuongs = _luongService.LayBangLuongTheoNhanVien(idNhanVien);

            DataTable dt = new DataTable();
            dt.Columns.Add("Tháng");
            dt.Columns.Add("Lương Thực Nhận", typeof(decimal));
            dt.Columns.Add("Tiền Thưởng", typeof(decimal));
            dt.Columns.Add("Bảo Hiểm XH", typeof(decimal));

            if (comboBoxYear.SelectedItem != null)
            {
                int selectedYear = (int)comboBoxYear.SelectedItem;
                var bangLuongNam = bangLuongs.Where(bl => bl.Nam == selectedYear);
                foreach (var bl in bangLuongNam)
                {
                    var luongThucNhan = _luongService.TinhLuongThucNhan(nhanVien, bl);
                    dt.Rows.Add($"Tháng {bl.Thang}", luongThucNhan, bl.TienThuong, bl.BaoHiemXaHoi);
                }
            }

            dataGridViewLuong.DataSource = dt;
            dataGridViewLuong.Columns["Lương Thực Nhận"].DefaultCellStyle.Format = "N0";
            dataGridViewLuong.Columns["Tiền Thưởng"].DefaultCellStyle.Format = "N0";
            dataGridViewLuong.Columns["Bảo Hiểm XH"].DefaultCellStyle.Format = "N0";
        }

        private string GetIdNhanVienByName(string tenNhanVien)
        {
            var nhanVien = _nhanVienRepository.TimNhanVienTheoTen(tenNhanVien);
            return nhanVien.MaNV;
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
                    nam,
                    _isAdmin);
                
                if (chiTietLuongForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDashboard();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tháng để xem chi tiết.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
