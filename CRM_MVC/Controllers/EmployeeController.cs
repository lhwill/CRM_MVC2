using System;
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
    public class EmployeeController : Controller
    {
       
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string pwd)
        {
            string json = APIClient.GetApiResult("get", $"api/baseinfo/login?name={username}&pwd={pwd}");
            if (!string.IsNullOrEmpty(json))
            {
                HttpContext.Session.SetString("user", json);
                return Redirect("/Employee/index");
            }

            return View();
        }


        public IActionResult Index()
        {
            string json = Encoding.UTF8.GetString(HttpContext.Session.Get("user"));
            EmployeeInfo em = JsonConvert.DeserializeObject<EmployeeInfo>(json);
            ViewBag.em = em;
            List<MenuInfo> list = JsonConvert.DeserializeObject<List<MenuInfo>>(APIClient.GetApiResult("get", "api/baseinfo/getmenu/" + em.EId));
            ViewBag.menu = list;
            ViewBag.pmenu = list.Where(m => m.PId == 0).ToList();

            return View();
        }


    }
}