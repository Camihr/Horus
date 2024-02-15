using Horus.Models;
using Horus.Repositories;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace Horus.ViewModels
{
    public class LoginPageViewModel : BindableBase, IInitialize
    {
        private readonly INavigationService navigationService;
        private readonly AccountRepository accountRepository;
        private readonly IPageDialogService pageDialogService;

        public LoginPageViewModel(
            INavigationService navigationService,
            AccountRepository accountRepository,
            IPageDialogService pageDialogService
            )
        {
            this.navigationService = navigationService;
            this.accountRepository = accountRepository;
            this.pageDialogService = pageDialogService;

#if DEBUG
            User = new UserLogin() { Email = "user1@mail.com", Password = "Password1" };
#else
            User = new UserLogin();
#endif
        }

        public ICommand ShowAlertCommand => new DelegateCommand<string>(ShowAlert);
        public ICommand LoginCommand => new DelegateCommand(Login);

        public UserLogin User { get; set; }

        public void Initialize(INavigationParameters parameters)
        {
            SecureStorage.RemoveAll();
        }

        private async void Login()
        {
            if (!await Validate()) return;

            var response = await accountRepository.SignIn(User);

            if (response.IsOk)
            {
                var value = JsonConvert.SerializeObject(response.Data);
                await SecureStorage.SetAsync("Token", value);

                await navigationService.NavigateAsync("/ChallengesPage");
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Horus", response.Message, "Aceptar");
            }
        }
        
        private async void ShowAlert(string origin) 
        {
            await pageDialogService.DisplayAlertAsync("Horus", origin, "Aceptar");
        }

        private async Task<bool> Validate()
        {

            if (string.IsNullOrWhiteSpace(User.Email))
            {
                await pageDialogService.DisplayAlertAsync("Horus", "El email es requerido", "Aceptar");
                return false;
            }

            if (string.IsNullOrEmpty(User.Password))
            {
                await pageDialogService.DisplayAlertAsync("Horus", "El password es requerido", "Aceptar");
                return false;
            }

            if (!Regex.IsMatch(User.Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$") || User.Password.Length < 7)
            {
                await pageDialogService.DisplayAlertAsync("Horus", "El email y/o la contraseña son inválidas", "Aceptar");
                return false;
            }

            return true;
        }        
    }
}
