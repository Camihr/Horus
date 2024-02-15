using Horus.Models;
using Xamarin.Forms;

namespace Horus.Selectors
{
    public class ChallengeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CompletedChallenge { get; set; }
        public DataTemplate IncompleteChallenge { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var challenge = (Challenge)item;

            if (challenge.IsCompleted)
            {
                return CompletedChallenge;
            }

            return IncompleteChallenge;
        }
    }
}
