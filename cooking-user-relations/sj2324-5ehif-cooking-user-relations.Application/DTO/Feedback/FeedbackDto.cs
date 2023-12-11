using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sj2324_5ehif_cooking_user_relations.Application.DTO.Recipe;
using sj2324_5ehif_cooking_user_relations.Application.DTO.User;

namespace sj2324_5ehif_cooking_user_relations.Application.DTO.Feedback
{
    public class FeedbackDto
    {
        public string Key { get; set; }
        public UserDto From { get; set; }
        public UserDto To { get; set; }
        public int Rating { get; set; }
        public RecipeDto Recipe { get; set; }
        public string Text { get; set; }
    }


}
