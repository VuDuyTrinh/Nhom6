﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PagedList; 
using PagedList.Mvc;
using Nhom6.Models;

namespace Nhom6.Controllers
{
    public class TrangChuController : Controller
    {
        QLNHDataContext data = new QLNHDataContext(); // Biến data quản lý CSDL
        private List<SanPham> LaySP()
        {
            return data.SanPhams.ToList(); // hàm trả về list table SanPham
        }
        public ActionResult Lienhe()
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
        public ActionResult ShowRoom()
        {
            return View();
        }
        public ActionResult Index(int? page) // Khi nhấn vào trang chủ hàm này sẽ kích hoạt
        {                                      //tham số page để chỉ ra số trang trong list sản phẩm ở trang chủ
            int SoSP = 9; // SoSP chỉ ra số sản phẩm sẽ hiển thị trên web, nếu quá hơn số sp sẽ tự phân thành trang kế tiếp             
            int SoTrang = (page ?? 1); //Chỉ ra số trang 

            var nh = LaySP(); //Lấy list sản phẩm có trong CSDL
            return View(nh.ToPagedList(SoTrang, SoSP)); //Trả về Model chứa table SanPham, chứa hàm phân trang có trong PagedList
        }

        public ActionResult Hang()
        {
            var hang = (from a in data.HangNHs select a).Take(12); //Lấy 12 Hãng
            return PartialView(hang);
        }
        public ActionResult ChoNam(int? pape)//trang cho nam            
        {
            var t = from a in data.SanPhams where (a.MaLoai == 1) select a;//truy vấn sql
            int SoSP = 9;//so sp trong 1 trang là 9
            int SoTrang = (pape ?? 1);
            return View(t.ToPagedList(SoTrang, SoSP));//trả về sp &trang

        }
        public ActionResult ChoNu(int? pape)//trang sp cho nữ
        {
            var t = from a in data.SanPhams where (a.MaLoai == 2) select a;//truy vấn lấy dữ liệu
            int SoSP = 9;//so sp trong 1 trang la 9
            int SoTrang = (pape ?? 1);
            return View(t.ToPagedList(SoTrang, SoSP));
        }
        public ActionResult SpHang(int? page,int id)//trang sp theo hãng
        {
            int SoSP = 9;
            int SoTrang = (page ?? 1);
            var sp = from a in data.SanPhams where (a.MaHang == id) select a;//truy vấn lấy id của hãng sp mới chọn
            var b = data.HangNHs.SingleOrDefault(n=>n.MaHang==id);
            ViewBag.Hang = b.TenHang;//khai báo viewbag cho tên hãng
            return View(sp.ToPagedList(SoTrang, SoSP));
        }
        public ActionResult chitiet(int id)
        {
            var ct = (from t in data.SanPhams where (t.MaNH == id) select t);//lấy id sản phẩm mới chọn để hiện chi tiết sp đó
            return View(ct.SingleOrDefault());
        }
    }
}