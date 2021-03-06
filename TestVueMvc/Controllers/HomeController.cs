﻿using System;
using System.Web.Mvc;

namespace TestVueMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Request.RawUrl.ToLower().Contains("/home")) return Redirect("/");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            throw new ArgumentException("错误");
            //return View();
        }
    }
}