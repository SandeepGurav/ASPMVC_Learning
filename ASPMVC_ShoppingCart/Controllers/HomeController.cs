﻿using ASPMVC_ShoppingCart.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPMVC_ShoppingCart.Controllers
{
   
    public class HomeController : Controller
    {
        // GET: Home      
        public ActionResult Index()
        {
            return View();
        }
    }
}