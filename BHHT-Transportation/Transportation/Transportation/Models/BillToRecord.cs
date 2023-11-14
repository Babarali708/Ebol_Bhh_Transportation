namespace Transportation.Models
{
    public class BillToRecord
    {
        public int Id { get; set; }
        public int? TransporterRecordId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? AgreedValue { get; set; }
        public int? ChargeValue { get; set; }
        public int? ChargePerAnimal { get; set; }
        public string? Description { get; set; }
        public int? IsActive { get; set; }
        public int? CreatedBy { get; set; }   // super admin/admin
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}
