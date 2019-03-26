using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZiWatchVer3.Models;

namespace ZiWatchVer3.Controllers
{
    public class CartController : Controller
    {
        dbQLDHDataContext data = new dbQLDHDataContext();

        // GET: Cart
        public ActionResult ShoppingCart()
        {
            return View();
        }

        public ActionResult Pay()
        {
            decimal totalPrices = 0;
            ProductItems productItems = new ProductItems();
            List<TheOrder> lstProducts = productItems.GetProductItems;
            foreach (TheOrder product in lstProducts)
            {
                totalPrices += product.DonGia*product.SoLuong;
            }
            string url = RedirectOnepay(RandomString(), totalPrices.ToString(), "192.186.0.1");
            return Redirect(url);
        }

        private string RandomString()
        {
            var str = new StringBuilder();
            var random = new Random();
            for (int i = 0; i <= 5; i++)
            {
                var c = Convert.ToChar(Convert.ToInt32(random.Next(65, 68)));
                str.Append(c);
            }
            return str.ToString().ToLower();
        }

        public string RedirectOnepay(string codeInvoice, string amount, string ip)
        {
            // Khoi tao lop thu vien
            VPCRequest conn = new VPCRequest("http://mtf.onepay.vn/vpcpay/vpcpay.op");
            conn.SetSecureSecret("18D7EC3F36DF842B42E1AA729E4AB010");

            // Gan cac thong so de truyen sang cong thanh toan onepay
            conn.AddDigitalOrderField("AgainLink", "onepay.vn");
            conn.AddDigitalOrderField("Title", "Tich hop onepay vao web asp.net mvc3,4");
            conn.AddDigitalOrderField("vpc_Locale", "en");
            conn.AddDigitalOrderField("vpc_Version", "2");
            conn.AddDigitalOrderField("vpc_Command", "pay");
            conn.AddDigitalOrderField("vpc_Merchant", "TESTONEPAYUSD");
            conn.AddDigitalOrderField("vpc_AccessCode", "614240F4");
            conn.AddDigitalOrderField("vpc_MerchTxnRef", RandomString());
            conn.AddDigitalOrderField("vpc_OrderInfo", codeInvoice);
            conn.AddDigitalOrderField("vpc_Amount", amount);
            conn.AddDigitalOrderField("vpc_ReturnURL", Url.Action("OnepayResponse", "Home", null, Request.Url.Scheme, null));

            // Thong tin them ve khach hang. De trong neu khong co thong tin
            conn.AddDigitalOrderField("vpc_SHIP_Street01", "");
            conn.AddDigitalOrderField("vpc_SHIP_Provice", "");
            conn.AddDigitalOrderField("vpc_SHIP_City", "");
            conn.AddDigitalOrderField("vpc_SHIP_Country", "");
            conn.AddDigitalOrderField("vpc_Customer_Phone", "");
            conn.AddDigitalOrderField("vpc_Customer_Email", "");
            conn.AddDigitalOrderField("vpc_Customer_Id", "");
            conn.AddDigitalOrderField("vpc_TicketNo", ip);

            string url = conn.Create3PartyQueryString();
            return url;
        }
    }
}