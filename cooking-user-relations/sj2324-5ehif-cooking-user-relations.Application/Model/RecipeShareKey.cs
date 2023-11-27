using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class RecipeShareKey : Key
    {
        public RecipeShareKey() : base(prefix: "RES", length: 15)
        {
        }
        public RecipeShareKey(string value) : base(value:value,prefix: "RES", length: 15)
        {
        }
    }
}
