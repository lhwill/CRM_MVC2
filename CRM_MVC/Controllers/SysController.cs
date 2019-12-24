﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM_MVC.Common;
using CRM_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM_MVC.Controllers
{
    public class SysController : Controller
    {
        public IActionResult SaleChance()
        {
            return View();
        }

        public IActionResult MenuInfo()
        {
            string json = Encoding.UTF8.GetString(HttpContext.Session.Get("user"));
            EmployeeInfo em = JsonConvert.DeserializeObject<EmployeeInfo>(json);
            List<MenuInfo> list = JsonConvert.DeserializeObject<List<MenuInfo>>(APIClient.GetApiResult("get", "api/baseinfo/getmenu/" + em.EId));
            ViewBag.menu = list;
            return View();
        }

    }
}