using Transportation.Models;

namespace Transportation.HelpingClasses
{
    public class DtoCollection
    {
        public class UserDto
        {
            public string? Id { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Contact { get; set; }
            public string? Email { get; set; }
            public string? CreatedBy { get; set; }
        }

        public class TransporterRecordDto
        {
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
            public int? IsActive { get; set; }
            public int? CreatedBy { get; set; }   // super admin/admin
            public DateTime? CreatedAt { get; set; }
            public DateTime? DeletedAt { get; set; }
            public DateTime? ModifiedAt { get; set; }

            public string? tractorNumber { get; set; }
            public string? tractorDriver { get; set; }
            public string? trailerNumber { get; set; }
            public string? trailerDriver { get; set; }

        }

        //public class GeneratePdfRecordDto
        //{
        //    //getTransporterDetail
        //    public int Id { get; set; }
        //    public int? No { get; set; }
        //    public string? Date { get; set; }
        //    public string? Driver { get; set; }
        //    public string? Attendent { get; set; }
        //    public int? DriverId { get; set; }
        //    public int? AttendentId { get; set; }
        //    public string? From { get; set; }
        //    public string? BarnFrom { get; set; }
        //    public string? PhoneFrom { get; set; }
        //    public string? AddressFrom { get; set; }
        //    public string? FromCityAndState { get; set; }
        //    public string? ShipTo { get; set; }
        //    public string? BarnTo { get; set; }
        //    public string? ToPhoneNumber { get; set; }
        //    public string? AddressTo { get; set; }
        //    public string? ToCityAndState { get; set; }

        //    //getReceivingOrderRecord
        //    public string? By { get; set; }
        //    public int? TransporterRecordId { get; set; }
        //    public string? ReceivingDate { get; set; }
        //    public string? ReceivedBy { get; set; }
        //    public string? PaymentType { get; set; }
        //    public string? Payment { get; set; }
        //    public string? PaymentDate { get; set; }
        //    public string? PaymentBy { get; set; }
        //    public string? Check { get; set; }
        //    public int? BasicCharges { get; set; }
        //    public int? ChargesForExcessValue { get; set; }
        //    public int? Layovers { get; set; }
        //    public int? VetFees { get; set; }
        //    public int? Other { get; set; }
        //    public int? TotalCharges { get; set; }
        //    public string? CarrierPerAgentOrDriver { get; set; }

        //    //getBillToRecords
        //    public IEnumerable<BillToRecord?> billToRecords { get; set; }       

        //}
    }
}
