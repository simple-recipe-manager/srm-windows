using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiskly
{
    class RecipeClass
    {
        public Guid ID { get; set; }
        public long Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<string> Ingredients { get; set; }
        public List<DirectionClass> Directions { get; set; }
        public string Yeild { get; set; }
        public TimeSpan PrepTime { get; set; }
        public TimeSpan CookTime { get; set; }
        public string Temp { get; set; }
        public string TempUnit { get; set; }
        public string Image { get; set; }
        public List<SourceClass> Source { get; set; }
        public List<string> Notes { get; set; }
    }
}