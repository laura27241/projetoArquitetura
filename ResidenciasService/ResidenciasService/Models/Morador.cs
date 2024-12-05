namespace MoradoresService.Models
{
    public class Morador
    {
        public int Id { get; set; } 

        public required string Nome { get; set; } 

        public required string Residencia { get; set; } 

        public decimal Divida { get; set; } 
    }
}
