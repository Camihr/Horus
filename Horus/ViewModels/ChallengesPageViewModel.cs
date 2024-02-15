using Horus.Models;
using Horus.Repositories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Horus.ViewModels
{
    public class ChallengesPageViewModel : BindableBase, IInitialize
    {
        private readonly INavigationService navigationService;
        private readonly ChallengesRepository challengesRepository;
        private readonly IPageDialogService pageDialogService;

        public ChallengesPageViewModel(
            INavigationService navigationService,
            ChallengesRepository challengesRepository, 
            IPageDialogService pageDialogService
            )
        {
            this.navigationService = navigationService;
            this.challengesRepository = challengesRepository;
            this.pageDialogService = pageDialogService;
        }

        public ICommand ShowDetailCommand => new DelegateCommand<Challenge>(ShowDetail);
        public ICommand GoBackCommand => new DelegateCommand(GoBack);

        public List<Challenge> Challenges { get; set; }
        public int Completed { get; set; }
        public int Total { get; set; }

        public async void Initialize(INavigationParameters parameters)
        {
            await GetChallenges();

        }

        private async Task GetChallenges()
        {
            var response = await challengesRepository.GetChallenges();

            if (response.IsOk)
            {
                Challenges = response.Data;
                Total = Challenges.Count;
                Completed = Challenges.Where(c=>c.IsCompleted).Count();
            }
            else
            {
                await pageDialogService.DisplayAlertAsync("Horus", response.Message, "Aceptar");
            }
        }
        
        private async void ShowDetail(Challenge challenge)
        {
            await pageDialogService.DisplayAlertAsync(challenge.Title, challenge.Description, "Aceptar");
        }
        
        private async void GoBack()
        {
            await navigationService.NavigateAsync("/LoginPage");
        }
    }
}
