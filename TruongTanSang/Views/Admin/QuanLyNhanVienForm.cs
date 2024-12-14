using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
using TruongTanSang_QuanLyLuongNhanVien.Services;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.Admin
{
    public partial class QuanLyNhanVienForm : Form
    {
        private DataTable dt;
        private NhanVienService _nhanVienService;
        private Models.NhanVien selectedData;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ThemNhanVienForm themNhanVienForm = new ThemNhanVienForm();
            if (themNhanVienForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployeeData(); // Tải lại dữ liệu sau khi thêm
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedData != null) // Kiểm tra nếu có nhân viên được chọn
            {
                SuaNhanVienForm suaNhanVienForm = new SuaNhanVienForm(selectedData);
                if (suaNhanVienForm.ShowDialog() == DialogResult.OK)
                {
                    LoadEmployeeData(); // Tải lại dữ liệu sau khi sửa
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedData != null) // Kiểm tra nếu có nhân viên được chọn
            {
                // Xác nhận xóa
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên {selectedData.HoTen}?", "Xác Nhận", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    var nhanVienRepository = new NhanVienRepository();
                    nhanVienRepository.XoaNhanVien(selectedData.MaNV); // Gọi phương thức xóa
                    LoadEmployeeData(); // Tải lại dữ liệu sau khi xóa
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.");
            }
        }
        private void dataGridViewNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra nếu click vào một dòng hợp lệ
            {
                // Lưu thông tin nhân viên được chọn
                var selectedRow = dataGridViewNhanVien.Rows[e.RowIndex];
                String name, phone, email;

                // Nếu DataSource là DataTable, bạn cần lấy thông tin từ DataRowView
                if (selectedRow.DataBoundItem is DataRowView rowView)
                {
                    name = rowView["Họ Tên"].ToString();
                    phone = rowView["Số Điện Thoại"].ToString();
                    email = rowView["Email"].ToString();

                    selectedData = _nhanVienService.FindNhanVien(name, email, phone);
                    if (selectedData != null)
                    {
                        Console.WriteLine($"Đã tìm thấy nhân viên: {selectedData.MaNV}");
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy nhân viên.");
                    }
                }
            }
        }

        private void btnXemLuong_Click(object sender, EventArgs e)
        {
            if (selectedData != null)
            {
                var xemLuongForm = new XemLuongNhanVien(selectedData.HoTen, isAdmin: true)
                {
                    Text = $"Thông Tin Lương - {selectedData.HoTen}",
                    StartPosition = FormStartPosition.CenterParent
                };
                xemLuongForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xem lương.", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThemTienThuong_Click(object sender, EventArgs e)
        {
            if (selectedData == null)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để thêm tiền thưởng.", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string tienThuongStr = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Nhập tiền thưởng cho nhân viên {selectedData.HoTen}\nTháng {DateTime.Now.Month}/{DateTime.Now.Year}:",
                    "Thêm tiền thưởng",
                    "0");

                if (string.IsNullOrEmpty(tienThuongStr))
                    return;

                if (!double.TryParse(tienThuongStr, out double tienThuong))
                {
                    MessageBox.Show("Số tiền không hợp lệ!", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var luongService = new LuongService();
                bool ketQua = luongService.ThemTienThuongThangHienTai(selectedData.MaNV, tienThuong);

                if (ketQua)
                {
                    MessageBox.Show($"Đã cập nhật tiền thưởng {tienThuong:N0} VNĐ cho nhân viên {selectedData.HoTen}!", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật tiền thưởng. Vui lòng kiểm tra lại!", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
