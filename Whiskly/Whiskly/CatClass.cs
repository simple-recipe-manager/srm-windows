using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whiskly
{
    class CatClass
    {
        public CatClass(int cat_id, string category)
        {
            this.cat_id = cat_id;
            this.category = category;
        }
        public int cat_id { get; set; }
        public string category { get; set; }
    }
}
