using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user.Application.models
{
    public class Preference
    {
        public long Id { get; set; }

        // TODO
        [Required]
        public string Key { get; set; }
    }
}
