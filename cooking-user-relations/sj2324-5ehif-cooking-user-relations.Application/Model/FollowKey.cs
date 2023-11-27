using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class FollowKey : Key
    {
        public FollowKey() : base(prefix: "FOL", length: 15)
        { 
        }
        public FollowKey(string value) : base(value:value,prefix: "FOL", length: 15)
        { 
        }
    }
}

