﻿using System;
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
	}
}