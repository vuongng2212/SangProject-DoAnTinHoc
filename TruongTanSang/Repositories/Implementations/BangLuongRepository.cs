﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TruongTanSang_QuanLyLuongNhanVien.Models;
using TruongTanSang_QuanLyLuongNhanVien.Repositories.Interfaces;

namespace TruongTanSang_QuanLyLuongNhanVien.Repositories.Implementations
{
    public class BangLuongRepository : IBangLuongRepository
    {
        private const string FILE_PATH = @"Data\bangluong.txt";

        public List<BangLuong> LayTatCaBangLuong()
        {
            var bangLuongs = new List<BangLuong>();
            var lines = File.ReadAllLines(FILE_PATH);

            foreach (var line in lines)
            {
                var fields = line.Split('|');
                var bangLuong = new BangLuong
                {
                    IDBangLuong = fields[0],
                    IDNhanVien = fields[1],
                    TienThuong = double.Parse(fields[2]),
                    BaoHiemXaHoi = double.Parse(fields[3]),
                    Thang = int.Parse(fields[4]), 
                    Nam = int.Parse(fields[5])
                };
                bangLuongs.Add(bangLuong);
            }

            return bangLuongs;
        }

        public BangLuong TimBangLuongTheoID(string idBangLuong)
        {
            var bangLuongs = LayTatCaBangLuong();
            return bangLuongs.Find(bl => bl.IDBangLuong == idBangLuong);
        }

        public void ThemBangLuong(BangLuong bangLuong)
        {
            using (var writer = new StreamWriter(FILE_PATH, true))
            {
                writer.WriteLine($"{bangLuong.IDBangLuong}|{bangLuong.IDNhanVien}|{bangLuong.TienThuong}|{bangLuong.BaoHiemXaHoi}|{bangLuong.Thang}|{bangLuong.Nam}");
            }
        }

        public void XoaBangLuong(string idBangLuong)
        {
            var bangLuongs = LayTatCaBangLuong();
            bangLuongs.RemoveAll(bl => bl.IDBangLuong == idBangLuong);
            GhiLaiTatCaBangLuong(bangLuongs);
        }

        private void GhiLaiTatCaBangLuong(List<BangLuong> bangLuongs)
        {
            using (var writer = new StreamWriter(FILE_PATH))
            {
                foreach (var bl in bangLuongs)
                {
                    writer.WriteLine($"{bl.IDBangLuong}|{bl.IDNhanVien}|{bl.TienThuong}|{bl.BaoHiemXaHoi}|{bl.Thang}|{bl.Nam}");
                }
            }
        }

        public List<BangLuong> LayBangLuongTheoNhanVien(string idNhanVien)
        {
            var bangLuongs = LayTatCaBangLuong();
            return bangLuongs.Where(bl => bl.IDNhanVien == idNhanVien).ToList();
        }

        public BangLuong LayBangLuongTheoThang(string thang, string idNhanVien, int nam)
        {
            try
            {
                var bangLuongs = LayTatCaBangLuong();
                int thangInt = int.Parse(thang);
                return bangLuongs.FirstOrDefault(bl => 
                    bl.Thang == thangInt && 
                    bl.IDNhanVien == idNhanVien && 
                    bl.Nam == nam);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return null;
            }
        }

        public bool CapNhatBangLuong(BangLuong bangLuong)
        {
            try
            {
                var bangLuongs = LayTatCaBangLuong();
                var index = bangLuongs.FindIndex(bl => bl.IDBangLuong == bangLuong.IDBangLuong);
                
                if (index >= 0)
                {
                    bangLuongs[index] = bangLuong;
                    GhiLaiTatCaBangLuong(bangLuongs);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return false;
            }
        }
    }
}