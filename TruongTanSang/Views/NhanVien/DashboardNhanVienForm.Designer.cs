using System.Windows.Forms;

namespace TruongTanSang_QuanLyLuongNhanVien.Views.NhanVien
{
    partial class DashboardNhanVienForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Label lblTenNhanVien;
        private DataGridView dataGridViewLuong;
        private Button btnXemChiTiet;

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
            this.components = new System.ComponentModel.Container();
            this.lblTenNhanVien = new System.Windows.Forms.Label();
            this.dataGridViewLuong = new System.Windows.Forms.DataGridView();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLuong)).BeginInit();
            this.SuspendLayout();

            // 
            // lblTenNhanVien
            // 
            this.lblTenNhanVien.AutoSize = true;
            this.lblTenNhanVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenNhanVien.Location = new System.Drawing.Point(12, 9);
            this.lblTenNhanVien.Name = "lblTenNhanVien";
            this.lblTenNhanVien.Size = new System.Drawing.Size(0, 20);
            this.lblTenNhanVien.TabIndex = 0;

            // 
            // dataGridViewLuong
            // 
            this.dataGridViewLuong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLuong.Location = new System.Drawing.Point(12, 50);
            this.dataGridViewLuong.Name = "dataGridViewLuong";
            this.dataGridViewLuong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLuong.Size = new System.Drawing.Size(776, 350);
            this.dataGridViewLuong.TabIndex = 1;

            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.Location = new System.Drawing.Point(12, 420);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(150, 30);
            this.btnXemChiTiet.TabIndex = 2;
            this.btnXemChiTiet.Text = "Xem Chi Tiết";
            this.btnXemChiTiet.UseVisualStyleBackColor = true;
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);

            // 
            // DashboardNhanVienForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnXemChiTiet);
            this.Controls.Add(this.dataGridViewLuong);
            this.Controls.Add(this.lblTenNhanVien);
            this.Text = "Dashboard Nhân Viên";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLuong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}