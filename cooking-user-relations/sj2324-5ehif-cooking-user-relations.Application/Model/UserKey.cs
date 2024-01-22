﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class UserKey : Key
    {
        public UserKey() : base(prefix: "USR", length: 14)
        {
        }
        public UserKey(string value) : base(value:value,prefix: "USR", length: 14)
        {
        }
    }
}
