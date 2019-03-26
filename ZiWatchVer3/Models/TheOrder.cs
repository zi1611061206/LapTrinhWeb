using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZiWatchVer3.Models
{
    public class TheOrder
    {
        public int MaDonHang { get; set; }

        public string HinhAnh { get; set; }

        public string TenSanPham { get; set; }

        public decimal DonGia { get; set; }

        public decimal SoLuong { get; set; }
    }

    public class ProductItems
    {
        public static List<TheOrder> lstProducts;

        public List<TheOrder> GetProductItems
        {
            get
            {
                lstProducts = new List<TheOrder>();
                lstProducts.Add(new TheOrder { MaDonHang = 1, HinhAnh = "PRODUCT1-1.jpg", TenSanPham = "CASIO 741", DonGia = 750000, SoLuong = 1 });
                lstProducts.Add(new TheOrder { MaDonHang = 1, HinhAnh = "PRODUCT2-1.jpg", TenSanPham = "BENJAZZ 15", DonGia = 1020000, SoLuong = 2 });
                return lstProducts;
            }
        }
    }

}