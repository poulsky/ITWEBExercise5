using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ITWEBExercise5.Models;

namespace ITWEBExercise5.Data.Repository
{
    public interface IComponentTypeRepository
    {
        IEnumerable<ComponentType> GetAll();
        IEnumerable<ComponentType> GetAll(Expression<Func<ComponentType,bool>> predicate);
        ComponentType GetById(int id);
        void Add(ComponentType componentType);
        void Remove(ComponentType component);
        void Edit(ComponentType component);
    }
}