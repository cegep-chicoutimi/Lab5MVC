using LabASPCORE.DataAccessLayer.Factories;

namespace LabASPCORE.DataAccessLayer
{
    public class DAL
    {
        private MenuChoixFactory? _menuChoixFact = null;
        private ReservationFactory? _reservationFact = null;

        public static string? ConnectionString { get; set; }

        public MenuChoixFactory MenuChoixFact
        {
            get
            {
                if (_menuChoixFact == null)
                {
                    _menuChoixFact = new MenuChoixFactory();
                }

                return _menuChoixFact;
            }
        }

        public ReservationFactory ReservationFact
        {
            get
            {
                if (_reservationFact == null)
                {
                    _reservationFact = new ReservationFactory();
                }

                return _reservationFact;
            }
        }
    }
}
