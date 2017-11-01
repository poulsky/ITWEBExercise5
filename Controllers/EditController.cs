using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITWEBExercise5.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITWEBExercise5.Controllers
{
    public class EditController : Controller
    {
        public List<string> cat = new List<string>();

        public IActionResult Index()
        {
            
            cat.Add("modstand");
            cat.Add("resistor");
            ViewBag.AllCategories = cat;
            ViewBag.Selected = cat.FirstOrDefault();            
            return View();
        }
    }    
}