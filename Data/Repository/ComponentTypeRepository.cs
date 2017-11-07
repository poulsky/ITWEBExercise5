using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using ITWEBExercise5.Models;

namespace ITWEBExercise5.Data.Repository
{
    public class ComponentTypeRepository : IComponentTypeRepository
    {
        private EmbeddedStockContext _context;

        public ComponentTypeRepository(EmbeddedStockContext context) => _context = context;

        public void Add(ComponentType componentType)
        {
            _context.ComponentTypes.Add(componentType);
            _context.SaveChanges();
        }

        public void Edit(ComponentType component)
        {
            _context.Entry(component).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<ComponentType> GetAll() => _context.ComponentTypes.AsEnumerable();

        public IEnumerable<ComponentType> GetAll(Expression<Func<ComponentType, bool>> predicate) => _context.ComponentTypes.Where(predicate).AsEnumerable();

        public ComponentType GetById(long id) => _context.ComponentTypes.FirstOrDefault(x => x.ComponentTypeId == id);

        public void Remove(ComponentType component)
        {
            _context.ComponentTypes.Remove(component);
            _context.SaveChanges();
        }
    }
}