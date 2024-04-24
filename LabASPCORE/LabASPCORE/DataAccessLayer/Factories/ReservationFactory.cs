
using LabASPCORE.Models;
using MySql.Data.MySqlClient;

namespace LabASPCORE.DataAccessLayer.Factories
{
    public class ReservationFactory
    {
        private Reservation CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            int id = (int)mySqlDataReader["Id"];
            string nom = mySqlDataReader["Nom"].ToString() ?? string.Empty;
            string email = mySqlDataReader["Courriel"].ToString() ?? string.Empty;
            int nbPersonne = (int)mySqlDataReader["NbPersonne"];
            DateTime dateReservation = DateTime.Parse(mySqlDataReader["DateReservation"].ToString());
            int idMenuChoix = (int)mySqlDataReader["MenuChoiceId"];

            return new Reservation(id, nom, email, nbPersonne, dateReservation, idMenuChoix);
        }

        public Reservation CreateEmpty()
        {
            return new Reservation(0, string.Empty, string.Empty, 0, DateTime.Now, 0);
        }

        public List<Reservation> GetAll()
        {
            List<Reservation> reservations = new List<Reservation>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT Id, Nom, Courriel, NbPersonne, date(DateReservation) as DateReservation, MenuChoiceId FROM tp5_reservations ORDER BY Nom";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Reservation reservation = CreateFromReader(mySqlDataReader);
                    reservations.Add(reservation);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return reservations;
        }

        public List<Reservation> GetByCategory(int categoryId)
        {
            List<Reservation> reservations = new List<Reservation>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_reservations WHERE CategoryId = @CatId ORDER BY Code";
                mySqlCmd.Parameters.AddWithValue("@CatId", categoryId);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    Reservation reservation = CreateFromReader(mySqlDataReader);
                    reservations.Add(reservation);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return reservations;
        }

        public Reservation? Get(int id)
        {
            Reservation? reservation = null;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT Id, Nom, Courriel, NbPersonne, date(DateReservation) as DateReservation, MenuChoiceId FROM tp5_reservations WHERE Id = @Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    reservation = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return reservation;
        }

        public Reservation? GetByCode(string code)
        {
            Reservation? reservation = null;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp4_products WHERE Code = @Code";
                mySqlCmd.Parameters.AddWithValue("@Code", code);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    reservation = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return reservation;
        }

        public void Save(Reservation reservation)
        {
            MySqlConnection? mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "INSERT INTO tp5_reservations(Nom, Courriel, NbPersonne, DateReservation, MenuChoiceId) " +
                                           "VALUES (@Nom, @Email, @NbPers, @Date, @Menu)";



                mySqlCmd.Parameters.AddWithValue("@Nom", reservation.Nom);
                mySqlCmd.Parameters.AddWithValue("@Email", reservation.Courriel);
                mySqlCmd.Parameters.AddWithValue("@NbPers", reservation.NbPersonne);
                mySqlCmd.Parameters.AddWithValue("@Date", reservation.DateReservation);
                mySqlCmd.Parameters.AddWithValue("@Menu", reservation.IdMenuChoix);

                mySqlCmd.ExecuteNonQuery();

                if (reservation.Id == 0)
                {
                    // Si c'était un nouveau produit (requête INSERT),
                    // nous affectons le nouvel Id de l'instance au cas où il serait utilisé dans le code appelant.
                    reservation.Id = (int)mySqlCmd.LastInsertedId;
                }
            }
            finally
            {
                mySqlCnn?.Close();
            }
        }

        public void Delete(int id)
        {
            MySqlConnection? mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "DELETE FROM tp4_products WHERE Id=@Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);
                mySqlCmd.ExecuteNonQuery();
            }
            finally
            {
                mySqlCnn?.Close();
            }
        }
    }
}
