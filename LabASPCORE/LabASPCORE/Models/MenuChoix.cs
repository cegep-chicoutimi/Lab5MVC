namespace LabASPCORE.Models
{
    public class MenuChoix
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;


        // Constructeur vide requis pour la désérialisation
        public MenuChoix()
        {
        }

        public MenuChoix(int id, string desc)
        {
            Id = id;
            Description = desc;
        }
    }
}
