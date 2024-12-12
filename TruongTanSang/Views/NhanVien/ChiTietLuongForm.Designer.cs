using System.Windows.Forms;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.NhanVien
{
    partial class ChiTietLuongForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Label lblThang;
        private Label lblMaBangLuong;
        private Label lblIDNhanVien;
        private DataGridView dataGridViewChiTiet;
        private Label lblTienThuong;
        private Label lblBaoHiemXaHoi;
        private Label lblSoNgayLamViec;
        private Label lblLuongThucNhan;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblThang = new System.Windows.Forms.Label();
            this.lblMaBangLuong = new System.Windows.Forms.Label();
            this.lblIDNhanVien = new System.Windows.Forms.Label();
            this.lblTienThuong = new System.Windows.Forms.Label();
            this.lblBaoHiemXaHoi = new System.Windows.Forms.Label();
            this.lblSoNgayLamViec = new System.Windows.Forms.Label();
            this.lblLuongThucNhan = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThang.Location = new System.Drawing.Point(12, 9);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(0, 20);
            this.lblThang.TabIndex = 0;

            // 
            // lblMaBangLuong
            // 
            this.lblMaBangLuong.AutoSize = true;
            this.lblMaBangLuong.Location = new System.Drawing.Point(12, 50);
            this.lblMaBangLuong.Name = "lblMaBangLuong";
            this.lblMaBangLuong.Size = new System.Drawing.Size(100, 13);
            this.lblMaBangLuong.TabIndex = 1;

            // 
            // lblIDNhanVien
            // 
            this.lblIDNhanVien.AutoSize = true;
            this.lblIDNhanVien.Location = new System.Drawing.Point(12, 80);
            this.lblIDNhanVien.Name = "lblIDNhanVien";
            this.lblIDNhanVien.Size = new System.Drawing.Size(100, 13);
            this.lblIDNhanVien.TabIndex = 2;

            // 
            // lblTienThuong
            // 
            this.lblTienThuong.AutoSize = true;
            this.lblTienThuong.Location = new System.Drawing.Point(12, 110);
            this.lblTienThuong.Name = "lblTienThuong";
            this.lblTienThuong.Size = new System.Drawing.Size(100, 13);
            this.lblTienThuong.TabIndex = 3;

            // 
            // lblBaoHiemXaHoi
            // 
            this.lblBaoHiemXaHoi.AutoSize = true;
            this.lblBaoHiemXaHoi.Location = new System.Drawing.Point(12, 140);
            this.lblBaoHiemXaHoi.Name = "lblBaoHiemXaHoi";
            this.lblBaoHiemXaHoi.Size = new System.Drawing.Size(100, 13);
            this.lblBaoHiemXaHoi.TabIndex = 4;

            // 
            // lblSoNgayLamViec
            // 
            this.lblSoNgayLamViec.AutoSize = true;
            this.lblSoNgayLamViec.Location = new System.Drawing.Point(12, 170);
            this.lblSoNgayLamViec.Name = "lblSoNgayLamViec";
            this.lblSoNgayLamViec.Size = new System.Drawing.Size(100, 13);
            this.lblSoNgayLamViec.TabIndex = 5;

            // 
            // lblLuongThucNhan
            // 
            this.lblLuongThucNhan.AutoSize = true;
            this.lblLuongThucNhan.Location = new System.Drawing.Point(12, 200);
            this.lblLuongThucNhan.Name = "lblLuongThucNhan";
            this.lblLuongThucNhan.Size = new System.Drawing.Size(100, 13);
            this.lblLuongThucNhan.TabIndex = 6;

            // 
            // ChiTietLuongForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblLuongThucNhan);
            this.Controls.Add(this.lblSoNgayLamViec);
            this.Controls.Add(this.lblBaoHiemXaHoi);
            this.Controls.Add(this.lblTienThuong);
            this.Controls.Add(this.lblIDNhanVien);
            this.Controls.Add(this.lblMaBangLuong);
            this.Controls.Add(this.lblThang);
            this.Text = "Chi Tiết Lương";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}