using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom6.Models;

namespace Nhom6.Controllers
{
    public class ThongKeController : Controller
    {
        //
        // GET: /ThongKe/
        QLNHDataContext data = new QLNHDataContext();
        public ActionResult Index()
        {
            return View();
        }
        //Thong ke tai khoan KH
        public ActionResult   taikhoan()
        {
            int i = data.Admins.Count();
            ViewBag.SL = i;
            return View(i);
        }
        public ActionResult ddh()
        {
            var a = from i in data.DonDatHangs where (i.DaThanhToan == true) select i;
            var b = from i in data.DonDatHangs where (i.TinhTrangGiaoHang == true) select i;
            var c = from i in data.DonDatHangs where (i.DaThanhToan == true && i.TinhTrangGiaoHang == true) select i;
            var d = from i in data.DonDatHangs where (i.DaThanhToan == false && i.TinhTrangGiaoHang == false) select i;
            ViewBag.DDT = a.Count();
            ViewBag.DGH = b.Count();
            ViewBag.DTTGH = c.Count();
            ViewBag.KTTGH = d.Count();
            return View();

        }
	}
}