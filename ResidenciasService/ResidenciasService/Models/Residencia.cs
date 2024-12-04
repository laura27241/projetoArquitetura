namespace ResidenciasService.Models
{
    public class Residencia
    {
        public int Id { get; set; } 

        public required string Numero { get; set; } 

        public bool Ativa { get; set; } 
    }
}
