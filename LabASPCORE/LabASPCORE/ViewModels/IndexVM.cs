using LabASPCORE.Models;

namespace LabASPCORE.ViewModels
{
    public class IndexVM
    {
        public Reservation Reservation { get; set; } = new Reservation();
        public List<MenuChoix>? Choices { get; set; }

        public IndexVM()
        {
        }

        public IndexVM(Reservation reservation, List<MenuChoix> choices)
        {
            Reservation = reservation;
            Choices = choices;
        }
    }
}
