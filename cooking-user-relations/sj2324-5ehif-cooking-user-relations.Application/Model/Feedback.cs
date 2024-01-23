using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user_relations.Application.Model
{
    public class Feedback :IEntity
    {
        private const int MinRating = 1;
        private const int MaxRating = 5;

        [Key] public long Id { get; private set;  }
        public string Key { get; set; }
        [NotMapped]
        [Required]
        public FeedbackKey ObjectKey
        {
            get => new(Key);
            set => Key = value.Value;
        } 
        [Required] public User From { get; }
        [Required] public User To { get; }

        [Required] public int Rating { get; set; }
        [Required] public Recipe Recipe { get; }
        [Required] public string Text { get; set; }

#pragma warning disable CS8618
        protected Feedback()
        {
        }
#pragma warning restore CS8618

        public Feedback(User from, User to, int rating, Recipe recipe, string text)
        {
            if (from == null) throw new ArgumentNullException(nameof(from));
            if (to == null) throw new ArgumentNullException(nameof(to));
            if (rating < MinRating || rating > MaxRating)
                throw new ArgumentOutOfRangeException(nameof(rating), $"Rating must be between {MinRating} and {MaxRating}.");
            if (recipe == null) throw new ArgumentNullException(nameof(recipe));
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Feedback text cannot be empty or whitespace.", nameof(text));

            ObjectKey = new FeedbackKey();
            From = from;
            To = to;
            Rating = rating;
            Recipe = recipe;
            Text = text;

        }




    }
}
