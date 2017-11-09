using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITWEBExercise5.Models
{
    public class CategoryComponentType
    {
        public int CategoryId { get; set; }
        public  virtual Category Category { get; set; }
        public long ComponentTypeId { get; set; }
        public  virtual ComponentType ComponentType { get; set; }
    }
}
