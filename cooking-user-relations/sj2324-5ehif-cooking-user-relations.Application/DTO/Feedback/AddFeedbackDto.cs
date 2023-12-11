using sj2324_5ehif_cooking_user_relations.Application.DTO.Recipe;
using sj2324_5ehif_cooking_user_relations.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.DTO.Feedback
{
    public class AddFeedbackDto
    {
        public UserDto From { get; set; }
        public UserDto To { get; set; }
        public int Rating { get; set; }
        public RecipeDto Recipe { get; set; }
        public string Text { get; set; }
    }
}
