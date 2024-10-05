namespace InRiseService.Application.DTOs.PriceDto
{
    public class PriceResponseDto
    {
        public int Id { get; set; }
        public decimal CostPrice { get; set; }
        public decimal PorcentageProfit { get; set; }
        public decimal PorcentageFixedCost { get; set; }
        public decimal PorcentageADMCost { get; set; }
        public decimal PorcentageDiscount { get; set; } = 0.0m;
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal FinalPrice { get; set; }
    }
}