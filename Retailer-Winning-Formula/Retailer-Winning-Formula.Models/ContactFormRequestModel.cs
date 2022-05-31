namespace Retailer_Winning_Formula.Models
{
    public class ContactFormRequestModel
    {
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactStore { get; set; }
        public string ContactLocation { get; set; }
        public string ContactRepName { get; set; }
        public long? ContactPhoneNumber { get; set; }
        public bool WhoMyRep { get; set; }
    }
}
