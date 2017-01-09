using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webfinal.Models;

namespace webfinal.Controllers
{
    public class SliderController : Controller
    {
        // GET: Slider
        QLNHDataContext data = new QLNHDataContext();
        public ActionResult Index()
        {
            return View();
        }

        public int KTNull(int co, string s)
        {
            if (s == "")
            {
                return 0;
            }
            return co;
        }
        public ActionResult Them()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }

            int co = 0;
            String tencn = "";
            List<CnAd> cn = Session["CN"] as List<CnAd>;
            foreach (CnAd i in cn)
            {
                if (i.iAction == "Slider" && i.iController == "Slider")
                {
                    tencn = i.iTenCN;
                    co = 1;
                }
            }
            if (co == 0)
            {

                return RedirectToAction("Loi", "Admin");
            }
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(Slider sd, HttpPostedFileBase fileUpload, FormCollection collection)
        {
            var ma = collection["Mahinh"];
           // var link = collection["link"];
            int co = 1;
            co = KTNull(co, ma);
           // co = KTNull(co, link);
            if (co == 0)
            {
                ViewBag.Loi = "hãy nhập đầy đủ thông tin Slider";
                return View();
            }
            else
            {
                if (fileUpload == null)
                {
                    ViewBag.Loi = "Vui Long chọn ảnh ";
                    return View();
                }
                else
                {
                   try
                    {
                        if (ModelState.IsValid)
                        {
                            var filename = Path.GetFileName(fileUpload.FileName);
                            var path = Path.Combine(Server.MapPath("~/themes/images/carousel"), filename);
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.ThongBao = "hinh đã tồn tại";
                            }
                            else
                            {
                                fileUpload.SaveAs(path);
                            }

                            sd.Mahinh = ma;
                            sd.link = filename;
                        data.Sliders.InsertOnSubmit(sd);
                        data.SubmitChanges();
                        ViewBag.Loi = "Thêm Thành Công";
                         return View();
                    }
                    }
                    catch (Exception ex)
                    {
                       ViewBag.Loi = "Có lỗi xẩy ra khi thêm dữ liệu hãy kiểm tra lại . Dữ liệu có thể bị trùng hoặc sai định dạng";
                        return View();
                    }
                   
                }
                return View();
         }
         }
        }
    }