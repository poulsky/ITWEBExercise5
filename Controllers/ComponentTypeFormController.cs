using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITWEBExercise5.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITWEBExercise5.Models;
using Microsoft.AspNetCore.Http;

namespace ITWEBExercise5.Controllers
{
    public class ComponentTypeFormController : Controller
    {
        private readonly EmbeddedStockContext _context;
        public ICategoryRepository _categoryRepository;
        public IComponentTypeRepository _componentTypeRepository;
        public IComponentRepository _componentRepository;
        public ComponentTypeFormController(IComponentRepository componentRepository, ICategoryRepository categoryRepository, IComponentTypeRepository componentTypeRepository, EmbeddedStockContext context)
        {
            _componentTypeRepository = componentTypeRepository;
            _categoryRepository = categoryRepository;
            _context = context;
            _componentRepository = componentRepository;
        }

        // GET: ComponentTypeForm
        public async Task<IActionResult> Index()
        {
            return View(await _context.ComponentTypes.ToListAsync());
        }

        // GET: ComponentTypeForm/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // GET: ComponentTypeForm/Create
        public IActionResult Create()
        {
            var allCategories = _categoryRepository.GetAll().ToList();
            ViewBag.AllCategories = allCategories;
            return View();
        }

        // POST: ComponentTypeForm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentTypeId,ComponentName,ComponentInfo,Location,Status,Datasheet,ImageUrl,Manufacturer,WikiLink,AdminComment")] ComponentType componentType, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var selectedValues = formCollection["categories"].ToString();
                var splitSelected = selectedValues.Split(",");
                var allCategories = _categoryRepository.GetAll().ToList();
                foreach (var typeId in splitSelected)
                {
                    foreach (var cat in allCategories)
                    {
                        if (cat.CategoryId.ToString() == typeId)
                        {
                            _context.CategoryComponentTypes.Add(new CategoryComponentType{Category = cat, ComponentType = componentType});
                        }
                    }
                }

                _context.Add(componentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(componentType);
        }

        // GET: ComponentTypeForm/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }            
           
            var componentType = await _context.ComponentTypes.SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            var componentsOfType = await _context.Components.Where(c => c.ComponentTypeId == id).ToListAsync();
            ViewBag.ComponentsOfType = componentsOfType;

            var categoriesOfType = await _context.CategoryComponentTypes
                .Where(cc => cc.ComponentTypeId == id)
                .Select(cc => cc.Category)
                .ToListAsync();

            var categoriesAvailable = await _context.Categories.Where(c => !categoriesOfType.Contains(c))
                .ToListAsync();

            ViewBag.CategoriesOfType = categoriesOfType;
            ViewBag.CategoriesAvailable = categoriesAvailable;
            if (componentType == null)
            {
                return NotFound();
            }
            return View(componentType);
        }

        // POST: ComponentTypeForm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ComponentTypeId,ComponentName,ComponentInfo,Location,Status,Datasheet,ImageUrl,Manufacturer,WikiLink,AdminComment")] ComponentType componentType, IFormCollection formCollection)
        {
            if (id != componentType.ComponentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var selectedValues = formCollection["categories"].ToString();
                    var splitSelected = selectedValues.Split(",");

                    var categoriesOfType = await _context.CategoryComponentTypes
                        .Where(cc => cc.ComponentTypeId == id)
                        .Select(cc => cc.Category)
                        .ToListAsync();

                    foreach (var categoryId in splitSelected)
                    {
                        var category = _categoryRepository.GetById(Int32.Parse(categoryId));

                        

                        if (!categoriesOfType.Contains(category))
                        {
                            _context.CategoryComponentTypes.Add(new CategoryComponentType { Category = category, ComponentType = componentType });
                        }
                    }

                    foreach (var type in categoriesOfType)
                    {
                        if (!splitSelected.Contains(type.CategoryId.ToString()))
                        {
                            var catCompType = await _context.CategoryComponentTypes
                                .Where(cc => cc.CategoryId == type.CategoryId
                                             && cc.ComponentTypeId == id).SingleOrDefaultAsync();
                            if (catCompType != null)
                            {
                                _context.CategoryComponentTypes.Remove(catCompType);
                                _context.SaveChanges();
                            }
                        }
                    }
                    _context.Update(componentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentTypeExists(componentType.ComponentTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(componentType);
        }

        // GET: ComponentTypeForm/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            if (componentType == null)
            {
                return NotFound();
            }

            return View(componentType);
        }

        // POST: ComponentTypeForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var componentType = await _context.ComponentTypes.SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            _context.ComponentTypes.Remove(componentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentTypeExists(long id)
        {
            return _context.ComponentTypes.Any(e => e.ComponentTypeId == id);
        }
    }
}
