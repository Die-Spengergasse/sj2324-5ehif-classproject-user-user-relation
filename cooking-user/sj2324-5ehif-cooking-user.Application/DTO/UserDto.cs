﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sj2324_5ehif_cooking_user.Application.DTO
{
    public class UserDto
    {
        public long? Id { get; set; }
        //public UserKeyDto? UserKey { get; set; }
        public string? Username { get; set; }
        public string? Lastname { get; set; }
        public string? Firstname { get; set; }
        public string? Email { get; set; }
        public List<PreferenceDto>? Preferences { get; set; } = new();
    }
}
