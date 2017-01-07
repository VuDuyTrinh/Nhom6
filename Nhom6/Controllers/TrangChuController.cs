using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom6.Models;
using PagedList;
using PagedList.Mvc;

namespace Nhom6.Controllers
{
    public class TrangChuController : Controller
    {
        QLNHDataContext data = new QLNHDataContext();
        // GET: TrangChu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }
        public List<tuvan> laytuvan()
        {
            List<tuvan> tv = new List<tuvan>();
            var a = from b in data.CauHois orderby b.MaCauHoi descending select b;

            foreach (var item in a)
            {
                tuvan t = new tuvan(item.MaCauHoi);
                tv.Add(t);
            }
            return tv;
        }
        public ActionResult TuVan(int? pape)
        {
            List<tuvan> tv = laytuvan();
            int SoSP = 9;
            int SoTrang = (pape ?? 1);
            return View(tv.ToPagedList(SoTrang, SoSP));
        }
        [HttpPost]
        public ActionResult TuVan(int? pape, FormCollection colection)
        {
            List<tuvan> tv = laytuvan();
            int SoSP = 9;
            int SoTrang = (pape ?? 1);
            if (Session["TaiKhoan"] == null)
            {
                ViewBag.ThongBao = "Bạn phải đăng nhập để gửi thư";
                return View(tv.ToPagedList(SoTrang, SoSP));
            }
            else
            {
                string cauhoi = colection["cauhoi"];
                if (cauhoi == "")
                {
                    ViewBag.ThongBao = "Hãy nhập câu hỏi";
                    return View(tv.ToPagedList(SoTrang, SoSP));
                }
                else
                {
                    KhachHang kh = (KhachHang)Session["TaiKhoan"];
                    CauHoi c = new CauHoi();
                    c.MaKH = kh.MaKH;
                    c.NoiDung = cauhoi;
                    c.TraLoi = 0;
                    data.CauHois.InsertOnSubmit(c);
                    data.SubmitChanges();
                    tv = laytuvan();
                    return View(tv.ToPagedList(SoTrang, SoSP));
                }
            }
            return View(tv.ToPagedList(SoTrang, SoSP));
        }
    }
}