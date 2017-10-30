using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using ITWEBExercise5.Models;

namespace ITWEBExercise5.Data.Repository
{
    public class ComponentRepository : IComponentRepository
    {
        EmbeddedStockContext _context;
        public ComponentRepository(EmbeddedStockContext context) => _context = context;

        public void Add(Component component)
        {
            _context.Components.Add(component);
            _context.SaveChanges();
        }

        public void Edit(Component component)
        {
            _context.Entry(component).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<Component> GetAll() => _context.Components.AsEnumerable();

        public IEnumerable<Component> GetAll(Expression<Func<Component, bool>> predicate) => _context.Components.Where(predicate).AsEnumerable();

        public Component GetById(int id) => _context.Components.FirstOrDefault(x => x.ComponentId == id);

        public void Remove(Component component)
        {
            _context.Components.Remove(component);
            _context.SaveChanges();
        }
    }
}