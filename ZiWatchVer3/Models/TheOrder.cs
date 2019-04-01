using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZiWatchVer3.Models;

namespace ZiWatchVer3.Models
{
    public class TheOrder
    {
        dbQLDHDataContext data = new dbQLDHDataContext();

        public int MaSanPham { get; set; }

        public string HinhAnh { get; set; }

        public string TenSanPham { get; set; }

        public double DonGia { get; set; }

        public int SoLuong { get; set; }

        public double ThanhTien { get { return DonGia * SoLuong; } }

        public TheOrder(int maSanPham)
        {
            MaSanPham = maSanPham;
            SANPHAM dongHo = data.SANPHAMs.Single(n => n.MASANPHAM == MaSanPham);
            TenSanPham = dongHo.TENSANPHAM;
            HinhAnh = dongHo.HINHANH;
            DonGia = double.Parse(dongHo.DONGIA.ToString());
            SoLuong = 1;
        }
    }
}