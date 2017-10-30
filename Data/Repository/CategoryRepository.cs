using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using ITWEBExercise5.Models;

namespace ITWEBExercise5.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private EmbeddedStockContext _context;
        public CategoryRepository(EmbeddedStockContext context) => _context = context;

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Edit(Category category)
        {
            _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<Category> GetAll() => _context.Categories.AsEnumerable();

        public IEnumerable<Category> GetAll(Expression<Func<Category, bool>> predicate) => _context.Categories.Where(predicate).AsEnumerable();

        public Category GetById(int id) => _context.Categories.FirstOrDefault(x => x.CategoryId == id);

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}