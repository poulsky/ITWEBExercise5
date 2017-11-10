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
    public class CategoryFormController : Controller
    {
        private readonly EmbeddedStockContext _context;
        
        public ICategoryRepository _categoryRepository;
        public IComponentTypeRepository _componentTypeRepository;

        public CategoryFormController(ICategoryRepository categoryRepository, IComponentTypeRepository componentTypeRepository, EmbeddedStockContext context)
        {
            _componentTypeRepository = componentTypeRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }

        // GET: CategoryForm
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: CategoryForm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .SingleOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: CategoryForm/Create
        public IActionResult Create()
        {
            var allTypes = _componentTypeRepository.GetAll().ToList();
            ViewBag.AllTypes = allTypes;

            return View();
        }

        // POST: CategoryForm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name")] Category category, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var selectedValues = formCollection["types"].ToString();
                var splitSelected = selectedValues.Split(",");
                var allTypes = _componentTypeRepository.GetAll().ToList();

                foreach (var typeId in splitSelected)
                {
                    foreach (var type in allTypes)
                    {
                        if (type.ComponentTypeId.ToString() == typeId)
                        {
                            _context.CategoryComponentTypes.Add(new CategoryComponentType { Category = category, ComponentType = type });
                        }
                    }
                }
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryForm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.SingleOrDefaultAsync(m => m.CategoryId == id);
            var allTypes = _componentTypeRepository.GetAll().ToList();
            ViewBag.AllTypes = allTypes;

            var typesOfCategory = await _context.CategoryComponentTypes
                .Where(cc => cc.CategoryId == id)
                .Select(cc => cc.ComponentType)
                .ToListAsync();

            var typesAvailable = await _context.ComponentTypes.Where(c => !typesOfCategory.Contains(c))
                .ToListAsync();
            ViewBag.TypesOfCateogry = typesOfCategory;
            ViewBag.TypesAvailable = typesAvailable;

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryForm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name")] Category category, IFormCollection formCollection)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var selectedValues = formCollection["types"].ToString();
                    string[] splitSelected = new string[0];
                    if (!string.IsNullOrEmpty(selectedValues))
                    {
                        splitSelected = selectedValues.Split(",");
                    }
                    
                    var allTypes = _componentTypeRepository.GetAll().ToList();

                    var typesOfCategory = await _context.CategoryComponentTypes
                        .Where(cc => cc.CategoryId == id)
                        .Select(cc => cc.ComponentType)
                        .ToListAsync();

                    foreach (var typeId in splitSelected)
                    {
                        var componentType = _componentTypeRepository.GetById(Int32.Parse(typeId));

                        

                        if (!typesOfCategory.Contains(componentType))
                        {
                            _context.CategoryComponentTypes.Add(new CategoryComponentType { Category = category, ComponentType = componentType });
                        }
                    }

                    foreach (var type in typesOfCategory)
                    {
                        if (!splitSelected.Contains(type.ComponentTypeId.ToString()))
                        {
                            var catCompType = await _context.CategoryComponentTypes
                                .Where(cc => cc.ComponentTypeId == type.ComponentTypeId
                                             && cc.CategoryId == id).SingleOrDefaultAsync();
                            if (catCompType != null)
                            {
                                _context.CategoryComponentTypes.Remove(catCompType);
                                _context.SaveChanges();
                            }
                        }
                    }

                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: CategoryForm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .SingleOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoryForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(m => m.CategoryId == id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
