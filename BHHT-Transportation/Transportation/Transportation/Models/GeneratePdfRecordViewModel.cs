namespace Transportation.Models
{
    public class GeneratePdfRecordViewModel
    {
        //getTransporterDetail
        public int Id { get; set; }
        public int? No { get; set; }
        public string? Date { get; set; }
        public string? Driver { get; set; }
        public string? Attendent { get; set; }

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

        //getReceivingOrderRecord
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

        //getBillToRecords
        public IEnumerable<BillToRecord?> billToRecords { get; set; }
    }
}
