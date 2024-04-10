using InRiseService.Domain;

namespace InRiseService.Application.DTOs.UserAddressDto
{
    public class UserAddressDtoInsertResponse : BaseDomain
    {
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public int Number { get; set; } = default!;
        public string Region { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string Observation { get; set; } = default!;
        public int UserId { get; set; } = default!;
    }
}
