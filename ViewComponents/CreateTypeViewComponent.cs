using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITWEBExercise5.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITWEBExercise5.ViewComponents
{
    public class CreateTypeViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string filter)
        {
            var test = new Category();
            test.Name = filter;

            return View(test);
        }
    }

}
