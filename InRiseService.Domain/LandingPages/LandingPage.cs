namespace InRiseService.Domain.LandingPages
{
    public class LandingPage
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool IsAcceptRGPD { get; set; }
    }
}