using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user.Application.Model
{
    public class Preference
    {
        [Required] public PreferenceKey Key { get; }
        public string Name { get; }

        public Preference(PreferenceKey key, string name)
        {
            Key = key;
            Name = name;
        }
    }
}