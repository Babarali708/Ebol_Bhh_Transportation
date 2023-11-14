namespace Transportation.Models
{
    public class ReceiviedOrderRecord
    {
        public int Id { get; set; }
        public string? By { get; set; }
        public int? TransporterRecordId { get; set; }
        public string? ReceivingDate { get; set; }
        public string? ReceivedBy { get; set; }
        public string? PaymentType { get; set; }
        public string? Payment { get; set; }
        public string? PaymentDate { get; set; }
        public string? PaymentBy { get; set; }
        public string? Check { get; set; }
        public int? BasicCharges { get; set; }
        public int? ChargesForExcessValue { get; set; }
        public int? Layovers { get; set; }
        public int? VetFees { get; set; }
        public int? Other { get; set; }
        public int? TotalCharges { get; set; }
        public string? CarrierPerAgentOrDriver { get; set; }
        public int? IsActive { get; set; }
        public int? CreatedBy { get; set; }   // super admin/admin
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}
