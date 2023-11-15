using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sj2324_5ehif_cooking_user.Application.Model
{
    public class User
    {
        private List<Preference> _preferences = new();
        [Key] public long Id { get; private set; }
        public string Key { get; set; }

        [Required]
        [NotMapped]
        public UserKey ObjectKey
        {
            get => new(Key);
            set => Key = value.Value;
        }

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
            ObjectKey = new UserKey();
            Username = username;
            Lastname = lastname;
            Firstname = firstname;
            Email = email;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj as User);
        }

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            return Username == other.Username &&
                   Lastname == other.Lastname &&
                   Firstname == other.Firstname &&
                   Email == other.Email;
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