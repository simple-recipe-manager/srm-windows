using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiskly
{
    class RecipeClass
    {
        int ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        CatClass Category { get; set; }
        List<string> Ingredients { get; set; }
        List<DirectionClass> Directions { get; set; }
        int Yeild { get; set; }
        int PrepTime { get; set; }
        int CookTime { get; set; }
        int Temp { get; set; }
        string Image { get; set; }
    }
}
