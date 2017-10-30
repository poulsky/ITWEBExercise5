using ITWEBExercise5.Models;
using System.Collections.Generic;
using System.Drawing;

namespace ITWEBExercise5.Data
{
    public static class DbInitializer
    {
        public static void Initializer(EmbeddedStockContext context)
        {
            context.Database.EnsureCreated();


            var component1 = new Component{
                ComponentNumber = 89,
                SerialNo = "KL89",
                Status = ComponentStatus.Available,
                AdminComment = "Working",
                UserComment = "A small flaw"
            };
            var component2 = new Component{
                ComponentNumber = 42,
                SerialNo = "ML42",
                Status = ComponentStatus.Loaned,
                AdminComment = "User au593874 has it",
                UserComment = "I'm keeping it"
            };
            var image = new byte[0]; //System.IO.File.ReadAllBytes("\resistor.jpg");
            var esimage = new ESImage{
                ImageData = image,
                Thumbnail = image,
                ImageMimeType = "image/jpeg"
            };
            var componenttype = new ComponentType
            {
                ComponentName = "Resistor",
                ComponentInfo = "Just a resistor",
                Location = "in yer butt",
                Status = ComponentTypeStatus.Available,
                Datasheet = "data",
                ImageUrl = "url",
                Image = esimage,
                Manufacturer = "Poul Ejnar A/S",
                WikiLink = "wikilink",
                AdminComment = "comment"
            };
            componenttype.Components.Add(component1);
            componenttype.Components.Add(component2);
            var category = new Category
            {
                Name = "Test"
            };
            category.ComponentTypes.Add(componenttype);

            context.Categories.Add(category);
            context.SaveChanges();
        }
    }
}