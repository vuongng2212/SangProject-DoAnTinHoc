using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
using TruongTanSang_QuanLyLuongNhanVien.Services;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.Admin
{
    public partial class ThemNhanVienForm : Form
    {
        public ThemNhanVienForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Tạo mã nhân viên tự động
            string maNV = GenerateMaNV();

            // Tạo đối tượng NhanVien mới
            var newNhanVien = new Models.NhanVien
            {
                MaNV = maNV,
                HoTen = txtHoTen.Text,
                DiaChi = txtDiaChi.Text,
                SoDienThoai = txtSoDienThoai.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                HeSoLuong = 1.0,
                MucLuongCoSo = 1000000,
                TrangThai = Models.Enums.TrangThaiNhanVien.DangLamViec,
                Role = "NV"
            };

            // Gọi service để thêm nhân viên
            var nhanVienService = new NhanVienService();
            if (nhanVienService.ThemNhanVien(newNhanVien))
            {
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        // Phương thức để tự động phát sinh mã nhân viên
        private string GenerateMaNV()
        {
            var nhanVienRepository = new NhanVienRepository();
            var nhanViens = nhanVienRepository.LayTatCaNhanVien();

            // Tìm mã nhân viên lớn nhất
            string maxMaNV = nhanViens
                .Where(nv => nv.MaNV.StartsWith("NV"))
                .Select(nv => nv.MaNV)
                .DefaultIfEmpty("NV000") // Nếu không có nhân viên nào, bắt đầu từ NV000
                .Max();

            // Tách chuỗi NV và số
            int nextNumber = int.Parse(maxMaNV.Substring(2)) + 1; // Tăng số lên 1
            return $"NV{nextNumber:D3}"; 
        }
    }
}
