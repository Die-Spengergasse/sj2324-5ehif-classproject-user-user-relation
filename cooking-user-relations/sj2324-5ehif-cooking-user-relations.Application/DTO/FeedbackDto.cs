using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.DTO
{
        public class FeedbackDto
        {
            public string FeedbackKey { get; set; }
            public UserDto FromUser { get; set; }
            public UserDto ToUser { get; set; }
            public int Rating { get; set; }
            public RecipeDto Recipe { get; set; }
            public string Text { get; set; }
        }
    

}
