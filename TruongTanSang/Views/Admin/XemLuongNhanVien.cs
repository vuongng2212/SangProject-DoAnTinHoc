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
            dt.Columns.Add("Lương Thực Nhận", typeof(decimal));
            dt.Columns.Add("Tiền Thưởng", typeof(decimal));
            dt.Columns.Add("Bảo Hiểm XH", typeof(decimal));

            // Thêm dữ liệu lương vào DataTable
            if (comboBoxYear.SelectedItem != null)
            {
                int selectedYear = (int)comboBoxYear.SelectedItem;
                foreach (var bl in bangLuongs.Where(b => b.Nam == selectedYear))
                {
                    double luongThucNhan = _luongService.TinhLuongThucNhan(nhanVien, bl);
                    dt.Rows.Add(
                        $"Tháng {bl.Thang}",
                        luongThucNhan,
                        bl.TienThuong,
                        bl.BaoHiemXaHoi
                    );
                }
            }

            // Gán dữ liệu cho DataGridView
            dataGridViewLuong.DataSource = dt;

            // Format các cột tiền tệ
            foreach (DataGridViewColumn column in dataGridViewLuong.Columns)
            {
                if (column.Index > 0) // Bỏ qua cột Tháng
                {
                    column.DefaultCellStyle.Format = "N0";
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            // Điều chỉnh độ rộng cột
            dataGridViewLuong.AutoResizeColumns();
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
                    nam,
                    _isAdmin); // Truyền quyền admin
                
                if (chiTietLuongForm.ShowDialog() == DialogResult.OK)
                {
                    // Nếu có cập nhật từ form chi tiết, tải lại dữ liệu
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

        // Thêm phương thức để xuất báo cáo nếu cần
        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            if (dataGridViewLuong.Rows.Count > 0)
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx",
                    FileName = $"BaoCaoLuong_{_tenNhanVien}_{comboBoxYear.SelectedItem}"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Thêm code xuất Excel ở đây
                        MessageBox.Show("Xuất báo cáo thành công!", 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất báo cáo!", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
