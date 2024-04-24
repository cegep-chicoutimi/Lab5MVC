using System.ComponentModel.DataAnnotations;

namespace LabASPCORE.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Courriel { get; set; } = string.Empty;
        public int NbPersonne { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateReservation { get; set; } = DateTime.Now;
        public int IdMenuChoix { get; set; }


        // Constructeur vide requis pour la désérialisation
        public Reservation()
        {
        }

        public Reservation(int id, string nom, string email, int nbPersonne, DateTime dateReservation, int idMenuChoix)
        {
            Id = id;
            Nom = nom;
            Courriel = email;
            NbPersonne = nbPersonne;
            DateReservation = dateReservation;
            IdMenuChoix = idMenuChoix;
        }
    }
}
