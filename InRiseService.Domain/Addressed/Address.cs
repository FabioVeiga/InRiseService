namespace InRiseService.Domain.Addressed
{
    public class Address : BaseDomain
    {
        public string PostalCode { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string CityEn { get; set; } = null!;
        public string StateEn { get; set; } = null!;
        public string StateCode { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string ProviceCode { get; set; } = null!;
    }
}