using System;
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


        public ActionResult SpHot(int i) // i là số sản phẩm sẽ bỏ qua để lấy các sản phẩm khác nhau trong carousel slide
        {
            var sp = (from a in data.SanPhams select a).Skip(i).Take(4); //Lấy 4 sản phẩm để hiển thị trong carousel slide
            return PartialView(sp); //Đây là PartialView của LayoutUser
        }
        public ActionResult LienHe()
        {
            return View();
        }
      
        public ActionResult ShowRoom()
        {
            return View();
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