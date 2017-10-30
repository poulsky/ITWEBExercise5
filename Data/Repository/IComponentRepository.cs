using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ITWEBExercise5.Models;

namespace ITWEBExercise5.Data.Repository
{
    public interface IComponentRepository
    {
        IEnumerable<Component> GetAll();
        IEnumerable<Component> GetAll(Expression<Func<Component,bool>> predicate);
        Component GetById(int id);
        void Add(Component component);
        void Remove(Component component);
        void Edit(Component component);
    }
}