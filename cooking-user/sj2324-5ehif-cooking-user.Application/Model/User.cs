using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace sj2324_5ehif_cooking_user.Application.Model
{
    public class User
    {
        private List<Preference> _preferences = new();
        public long Id { get; private set; }
        [Required] public UserKey Key { get; }
        [Required] [StringLength(50)] public string Username { get; set; }
        [Required] [StringLength(100)] public string Lastname { get; set; }
        [Required] [StringLength(100)] public string Firstname { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }
        public virtual IReadOnlyCollection<Preference> Preferences => _preferences;

#pragma warning disable CS8618
        protected User()
        {
        }
#pragma warning restore CS8618
        public User(string username, string lastname, string firstname, string email)
        {
            Key = new UserKey();
            Username = username;
            Lastname = lastname;
            Firstname = firstname;
            Email = email;
        }
        public void AddPreference(Preference preference)
        {
            _preferences.Add(preference);
        }

        public void ClearPreference()
        {
            _preferences.Clear();
        }

        public void RemovePreference(Preference preference)
        {
            _preferences.Remove(preference);
        }

        public void AddPreferenceRange(List<Preference> preferences)
        {
            foreach (var preference in preferences)
            {
                AddPreference(preference);
            }
        }
    }
}