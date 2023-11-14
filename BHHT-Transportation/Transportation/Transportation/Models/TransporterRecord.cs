namespace Transportation.Models
{
    public class TransporterRecord
    {
        public int Id { get; set; }
        public int? No { get; set; }
        public string? Date { get; set; }
        public int? DriverId { get; set; }
        public int? AttendentId { get; set; }
        public string? From { get; set; }
        public string? BarnFrom { get; set; }
        public string? PhoneFrom { get; set; }
        public string? AddressFrom { get; set; }
        public string? FromCityAndState { get; set; }
        public string? ShipTo { get; set; }
        public string? BarnTo { get; set; }
        public string? ToPhoneNumber { get; set; }
        public string? AddressTo { get; set; }
        public string? ToCityAndState { get; set; }
        public string? TractorNumber { get; set; }
        public string? TractorDriver { get; set; }
        public string? TrailerNumber { get; set; }
        public string? TrailerDriver { get; set; }
        public int? IsActive { get; set; }
        public int? CreatedBy { get; set; }   // super admin/admin
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}
