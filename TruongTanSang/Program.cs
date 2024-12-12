using System;
using System.Windows.Forms;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations;
using TruongTanSang_QuanLyLuongNhanVien.Services;

namespace TruongTanSang
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi tạo các repository
            var nhanVienRepository = new NhanVienRepository();
            var bangLuongRepository = new BangLuongRepository();

            // Khởi tạo dịch vụ
            var authService = new AuthService(nhanVienRepository);

            // Khởi động giao diện đăng nhập
            var loginForm = new LoginForm(authService);
            Application.Run(loginForm);
        }
    }
}
