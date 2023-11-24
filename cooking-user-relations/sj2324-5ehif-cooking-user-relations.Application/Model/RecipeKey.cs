using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class RecipeKey : Key
    {
        public RecipeKey() : base(prefix: "REC", length: 15)
        {
        }
        public RecipeKey(string key) : base(value:key,prefix: "REC", length: 15)
        {
        }
    }
}
