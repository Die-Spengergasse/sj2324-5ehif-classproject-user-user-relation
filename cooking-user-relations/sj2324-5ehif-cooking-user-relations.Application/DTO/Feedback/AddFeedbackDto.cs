using sj2324_5ehif_cooking_user_relations.Application.DTO.Recipe;

namespace sj2324_5ehif_cooking_user_relations.Application.DTO.Feedback
{
    public class AddFeedbackDto
    {
        public int Rating { get; set; }
        public RecipeDto Recipe { get; set; }
        public string Text { get; set; }
        
        
    }
}
