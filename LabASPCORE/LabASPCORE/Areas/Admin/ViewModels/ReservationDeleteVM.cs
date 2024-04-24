using LabASPCORE.Models;

namespace LabASPCORE.Areas.Admin.ViewModels
{
    public class ReservationDeleteVM
    {
        public Reservation Reservation { get; }

        public MenuChoix MenuChoice { get; }

        public string Choice
        {
            get
            {
                return MenuChoice?.Description ?? string.Empty;
            }
        }

        public ReservationDeleteVM(Reservation reservation, MenuChoix choix)
        {
            Reservation = reservation;
            MenuChoice = choix;
        }
    }
}
