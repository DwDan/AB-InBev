namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.Common
{
    public class AddressPresentation
    {
        public string City { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public int Number { get; set; }

        public string Zipcode { get; set; } = string.Empty;

        public GeolocationPresentation Geolocation { get; set; } = new();
    }
}
