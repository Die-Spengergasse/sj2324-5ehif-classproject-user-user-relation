﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class FeedbackKey : Key
    {
        public FeedbackKey() : base(prefix: "FED", length: 15)
        {
        }
    }
}
