using Prism.Mvvm;

namespace Horus.Models
{
    public class Challenge : BindableBase
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CurrentPoints { get; set; }
        public int TotalPoints { get; set; }
        public bool IsCompleted => CurrentPoints == TotalPoints;        
        public double Percentage => CurrentPoints * 100 / TotalPoints;
        public double Progress => Percentage / 100;
        public string Average => $"{CurrentPoints}/{TotalPoints}";
    }
}
