using System.Collections.Generic;

namespace ITWEBExercise5.Models
{
    public class Category
    {
        //public Category()
        //{
        //    CategoryComponentTypes = new List<CategoryComponentType>();
        //}
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<CategoryComponentType> CategoryComponentTypes { get; protected set; }
    }
}