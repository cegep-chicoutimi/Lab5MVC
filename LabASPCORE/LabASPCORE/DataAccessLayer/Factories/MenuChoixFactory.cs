using LabASPCORE.Models;
using MySql.Data.MySqlClient;

namespace LabASPCORE.DataAccessLayer.Factories
{
    public class MenuChoixFactory
    {
        private MenuChoix CreateFromReader(MySqlDataReader mySqlDataReader)
        {
            int id = (int)mySqlDataReader["Id"];
            string desc = mySqlDataReader["Description"].ToString() ?? string.Empty;

            return new MenuChoix(id, desc);
        }

        public MenuChoix CreateEmpty()
        {
            return new MenuChoix(0, string.Empty);
        }

        public List<MenuChoix> GetAll()
        {
            List<MenuChoix> menuChoix = new List<MenuChoix>();
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_menuchoices ORDER BY Description";

                mySqlDataReader = mySqlCmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    MenuChoix Choix = CreateFromReader(mySqlDataReader);
                    menuChoix.Add(Choix);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return menuChoix;
        }

        public MenuChoix? Get(int id)
        {
            MenuChoix? Choix = null;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_menuchoices WHERE Id = @Id";
                mySqlCmd.Parameters.AddWithValue("@Id", id);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    Choix = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return Choix;
        }

        public MenuChoix? GetByDesc(string desc)
        {
            MenuChoix? choice = null;
            MySqlConnection? mySqlCnn = null;
            MySqlDataReader? mySqlDataReader = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                mySqlCmd.CommandText = "SELECT * FROM tp5_menuchoices WHERE Description = @Desc";
                mySqlCmd.Parameters.AddWithValue("@Desc", desc);

                mySqlDataReader = mySqlCmd.ExecuteReader();
                if (mySqlDataReader.Read())
                {
                    choice = CreateFromReader(mySqlDataReader);
                }
            }
            finally
            {
                mySqlDataReader?.Close();
                mySqlCnn?.Close();
            }

            return choice;
        }

        public void Save(MenuChoix menuChoix)
        {
            MySqlConnection? mySqlCnn = null;

            try
            {
                mySqlCnn = new MySqlConnection(DAL.ConnectionString);
                mySqlCnn.Open();

                MySqlCommand mySqlCmd = mySqlCnn.CreateCommand();
                if (menuChoix.Id == 0)
                {
                    // On sait que c'est un nouveau produit avec Id == 0,
                    // car c'est ce que nous avons affecter dans la fonction CreateEmpty().
                    mySqlCmd.CommandText = "INSERT INTO tp5_menuchoices(Description) " +
                                           "VALUES (@Desc)";
                }
                else
                {
                    mySqlCmd.CommandText = "UPDATE tp5_menuchoices " +
                                           "SET Description=@Desc " +
                                           "WHERE Id=@Id";

                    mySqlCmd.Parameters.AddWithValue("@Id", menuChoix.Id);
                }

                mySqlCmd.Parameters.AddWithValue("@Desc", menuChoix.Description.Trim());

                mySqlCmd.ExecuteNonQuery();

                if (menuChoix.Id == 0)
                {
                    // Si c'était un nouveau produit (requête INSERT),
                    // nous affectons le nouvel Id de l'instance au cas où il serait utilisé dans le code appelant.
                    menuChoix.Id = (int)mySqlCmd.LastInsertedId;
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
                mySqlCmd.CommandText = "DELETE FROM tp5_menuchoices WHERE Id=@Id";
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
