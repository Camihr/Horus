using Prism.Mvvm;

namespace Horus.Models
{
    public class UserLogin : BindableBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
