namespace CommBank.Models
{
    public class Goal
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public string? UserId { get; set; } // Hata veren kısım buydu, bunu ekledik.
    }
}