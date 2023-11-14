using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Drawing;
using Transportation.Filters;
using Transportation.HelpingClasses;
using Transportation.Models;
using Transportation.Repositories;
using Rotativa.AspNetCore;
using static Transportation.HelpingClasses.DtoCollection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Transportation.Controllers
{
    //[ExceptionFilter]
    //[ValidationFilter(Roles = new int[] { 1, 0 })]
    public class AdminController : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly GeneralPurpose gp;
        //private readonly IHubContext<NotificationHub> HubContext;

        public AdminController(IUserRepo _userRepo, GeneralPurpose _generalPurpose)
        {
            userRepo = _userRepo;
            this.gp = _generalPurpose;
        }
        public async Task<IActionResult> Index(string msg = "", string color = "black")
        {
            User? loggedinUser = gp.GetUserClaims();
            if (loggedinUser == null)
            {
                return RedirectToAction("Login", "Auth", new { msg = "", color = "" });

            }
            ViewBag.Drivers = await userRepo.GetActiveUserCount(2);
            ViewBag.Receivers = await userRepo.GetActiveUserCount(3);
            ViewBag.Attendentants = await userRepo.GetActiveUserCount(4);
            ViewBag.Shipers = await userRepo.GetActiveUserCount(5);

            ViewBag.userRole = loggedinUser.Role;
            ViewBag.Message = msg;
            ViewBag.Color = color;
            return View();
        }

        #region Manage User

        
        public IActionResult AddUser(int Role, string msg = "", string color = "black")
        {
            User? loggedinUser = gp.GetUserClaims();
            if (loggedinUser == null)
            {
                return RedirectToAction("Login", "Auth", new { msg = "", color = "" });

            }
            else if(loggedinUser.Role!=1 && loggedinUser.Role!=0)
            {
                return RedirectToAction("Index", "Home");

            }


            ViewBag.Message = msg;
            ViewBag.Color = color;
            ViewBag.UserRole = Role;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostAddUser(User _user)
        {
            User? loggedinUser = gp.GetUserClaims();
            if (loggedinUser == null)
            {
                return RedirectToAction("Login", "Auth", new { msg = "", color = "" });

            }

            _user.Id = _user.Id;
            _user.FirstName = _user.FirstName.Trim();
            _user.LastName = _user.LastName.Trim();
            _user.Email = _user.Email.Trim();
            _user.Password = _user.Password.Trim();
            _user.Role = _user.Role;
            _user.IsActive = 1;
            _user.CreatedBy = loggedinUser.Role;
            _user.CreatedAt = GeneralPurpose.DateTimeNow();

            if (!await userRepo.AddUser(_user))
            {

                return RedirectToAction("AddUser", "Admin", new { Role = _user.Role, msg = "Somethings' Wrong", color = "red" });
            }

            return RedirectToAction("AddUser", "Admin", new { Role = _user.Role, msg = "Record Inserted Successfully", color = "green" });
        }

       
        public IActionResult ViewUser(int Role, string msg = "", string color = "black")
        {
            User? loggedinUser = gp.GetUserClaims();
            if (loggedinUser == null)
            {
                return RedirectToAction("Login", "Auth", new { msg = "", color = "" });

            }
            else if (loggedinUser.Role != 1 && loggedinUser.Role != 0)
            {

                return RedirectToAction("Index", "Home");

            }
            ViewBag.Message = msg;
            ViewBag.Color = color;
            ViewBag.UserRole = Role;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostUpdateUser(User _user)
        {
            User? user = await userRepo.GetUserById(_user.Id);
            if (user == null)
            {
                return RedirectToAction("ViewUser", "Admin", new { msg = "Record not found", color = "red" });
            }
            user.FirstName = _user.FirstName.Trim();
            user.LastName = _user.LastName.Trim();
            user.Email = _user.Email.Trim();
            user.Contact = _user.Contact;


            if (await userRepo.UpdateUser(user))
            {

                return RedirectToAction("ViewUser", "Admin", new { Role = user.Role, msg = "User updated successfully", color = "green" });
            }
            return RedirectToAction("ViewUser", "Admin", new { Role = user.Role, msg = "Somethings' wrong", color = "red" });
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            User? user = await userRepo.GetUserById(int.Parse(id));
            if (!await userRepo.DeleteUser(int.Parse(id)))
            {
                return RedirectToAction("ViewUser", "Admin", new { Role = user.Role, msg = "Somethings' wrong", color = "red" });

            }

            //await HubContext.Clients.All.SendAsync("LogoutUserWhenDelete", id);

            return RedirectToAction("ViewUser", "Admin", new { Role = user.Role, msg = "Record deleted successfully!", color = "green" });
        }

        #endregion

        #region Transporter Record

        public async Task<IActionResult> AddTransporterRecord(int Role, string msg = "", string color = "black")
        {
            User? loggedinUser = gp.GetUserClaims();
            if (loggedinUser == null)
            {
                return RedirectToAction("Login", "Auth", new { msg = "session expire", color = "red" });

            }
            else if (loggedinUser.Role != 1 && loggedinUser.Role != 0 && loggedinUser.Role!=2)
            {
                return RedirectToAction("Index", "Home");

            }

            int? DriverId = null;

            if(loggedinUser.Role==2)
            {
                DriverId = loggedinUser.Id;
            }

            ViewBag.DriverId = DriverId;
            ViewBag.Message = msg;
            ViewBag.Color = color;
            ViewBag.UserRole = Role;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostAddTransporterRecord(TransporterRecord _record)
        {
            try
            {
                User? loggedinUser = gp.GetUserClaims();
                if (loggedinUser == null)
                {
                    return RedirectToAction("Login", "Auth", new { msg = "", color = "" });

                }

                var getTransporterRecord = await userRepo.GetActiveTransporterRecordListCount();


                _record.IsActive = 1;
                _record.CreatedBy = loggedinUser.Id;
                _record.CreatedAt = GeneralPurpose.DateTimeNow();
                _record.No = getTransporterRecord + 1;
                

                var getRecordId = await userRepo.AddTransporterRecord(_record);
                if (getRecordId == null)
                {
                    return RedirectToAction("AddTransporterRecord", "Admin", new { Role = loggedinUser.Role, msg = "Somethings' Wrong", color = "red" });
                }

                return RedirectToAction("ViewTransporterDetail", "Admin", new { way = getRecordId.Value });

            }
            catch (Exception ex)
            {
                return RedirectToAction("AddTransporterRecord", "Admin", new { msg = "Somethings' Wrong.", color = "red" });
            }

        }

        public async Task<IActionResult> ViewTransporterRecord(string msg = "", string color = "")
        {

            User? loggedinUser = gp.GetUserClaims();
            if (loggedinUser == null)
            {
                return RedirectToAction("Login", "Auth", new { msg = "session expire", color = "red" });

            }
            else if (loggedinUser.Role != 1 && loggedinUser.Role != 0 && loggedinUser.Role != 2)
            {
                return RedirectToAction("Index", "Home");

            }

            ViewBag.message = msg;
            ViewBag.Color = color;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostupdateTransporterRecord(TransporterRecord _record)
        {
            TransporterRecord? record = await userRepo.GetTransporterRecordById(_record.Id);

            if (record == null)
            {
                return RedirectToAction("ViewTransporterRecord", "Admin", new { msg = "Record not found", color = "red" });
            }

            record.ModifiedAt = GeneralPurpose.DateTimeNow();
            record.PhoneFrom = _record.PhoneFrom;
            record.BarnFrom = _record.BarnFrom;
            record.BarnTo = _record.BarnTo;
            record.AddressFrom = _record.AddressFrom;
            record.AddressTo = _record.AddressTo;
            //record.DriverId = _record.DriverId;
            //record.AttendentId = _record.AttendentId;
            record.TractorNumber = _record.TractorNumber;
            record.TractorDriver = _record.TractorDriver;
            record.TrailerNumber = _record.TrailerNumber;
            record.TrailerDriver = _record.TrailerDriver;
            record.Date = _record.Date;
            record.FromCityAndState = _record.FromCityAndState;
            record.ToCityAndState = _record.ToCityAndState;
            record.ToPhoneNumber = _record.ToPhoneNumber;
            record.ShipTo = _record.ShipTo;
            record.From = _record.From;


            if (await userRepo.UpdateTransporterRecord(record))
            {

                return RedirectToAction("ViewTransporterRecord", "Admin", new { msg = "Record updated successfully", color = "green" });
            }
            return RedirectToAction("ViewTransporterRecord", "Admin", new { msg = "Somethings' wrong", color = "red" });
        }

        public async Task<IActionResult> DeleteTransporterRecord(int id)
        {
            TransporterRecord? record = await userRepo.GetTransporterRecordById(id);
            if (!await userRepo.DeleteTransporterRecord(id))
            {
                return RedirectToAction("ViewTransporterRecord", "Admin", new { msg = "Somethings' wrong", color = "red" });

            }

            return RedirectToAction("ViewTransporterRecord", "Admin", new { msg = "Record deleted successfully!", color = "green" });
        }

        public async Task<IActionResult> ViewTransporterDetail(int way = -1, string msg = "", string color = "")
        {
            User? loggedinUser = gp.GetUserClaims();
            if (loggedinUser == null)
            {
                return RedirectToAction("Login", "Auth", new { msg = "session expire", color = "red" });

            }
            else if (loggedinUser.Role != 1 && loggedinUser.Role != 0 && loggedinUser.Role!=2)
            {
                return RedirectToAction("Index", "Home");

            }
            var getTransporterDetail = await userRepo.GetTransporterRecordById(way);

            var getBillToRecords = await userRepo.GetBillToRecordsByTransporterRecordId(way);
            var getReceivingOrderRecord = await userRepo.GetReceiviedOrderByTransporterRecordId(way);
            var basicSumOfChargePerAnimal = await userRepo.GetSumOfChargePerAnimalByTransporterRecordId(way); 
            var sumofChargevalue = await userRepo.GetSumOfChargeValueByTransporterRecordId(way);
            
            if(getReceivingOrderRecord!=null)
            {
                var sumOfTotalCharges = basicSumOfChargePerAnimal + sumofChargevalue + getReceivingOrderRecord.Layovers + getReceivingOrderRecord.VetFees + getReceivingOrderRecord.Other;
                ViewBag.TotalCharges = sumOfTotalCharges;
            }
            else
            {
                var sumOfTotalCharges = basicSumOfChargePerAnimal + sumofChargevalue;
                ViewBag.TotalCharges = sumOfTotalCharges;
            }
            
            ViewBag.billToRecords = getBillToRecords;
            ViewBag.TransporterDetail = getTransporterDetail;
            ViewBag.getReceivingOrderRecord = getReceivingOrderRecord;
            ViewBag.message = msg;
            ViewBag.color = color;
            return View();
        }
        #endregion

        #region Bill To Record

        [HttpPost]
        public async Task<IActionResult> PostAddBillToRecord(BillToRecord billToRecord)
        {
            try
            {
                User? loggedinUser = gp.GetUserClaims();
                if (loggedinUser == null)
                {
                    return RedirectToAction("Login", "Auth", new { msg = "Loginto your Account", color = "red" });
                }

                billToRecord.IsActive = 1;
                billToRecord.CreatedBy = loggedinUser.Id;
                billToRecord.CreatedAt = GeneralPurpose.DateTimeNow();

                var result = await userRepo.AddBillToRecord(billToRecord);

                if (result != null)
                {
                    return RedirectToAction("ViewTransporterDetail", "Admin", new { way = billToRecord.TransporterRecordId, msg = "Record Added Successfully", color = "green" });
                }
                else
                {
                    return RedirectToAction("ViewTransporterDetail", "Admin", new { way = billToRecord.TransporterRecordId, msg = "Something went wrong", color = "red" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ViewTransporterDetail", "Admin", new { way = billToRecord.TransporterRecordId, msg = "Something went wrong", color = "red" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetBillToRecordById(int id = -1)
        {
            try
            {
                var billToRecord = userRepo.GetBillToRecordById(id); // Assuming you have a method to retrieve the record by id

                if (billToRecord == null)
                {
                    return Json(null); // Return null if the record is not found
                }

                return Json(billToRecord);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUpdateBillToRecord(BillToRecord billToRecord)
        {
            try
            {
                User? loggedinUser = gp.GetUserClaims();
                if (loggedinUser == null)
                {
                    return RedirectToAction("Login", "Auth", new { msg = "Log into your Account", color = "red" });
                }

                // Retrieve the existing BillToRecord from the database
                var existingRecord = await userRepo.GetBillToRecordById(billToRecord.Id);

                if (existingRecord == null)
                {
                    return RedirectToAction("ViewTransporterDetail", "Admin", new { way = billToRecord.TransporterRecordId, msg = "Record not found", color = "red" });
                }

                // Update the properties of the existing record with the new values
                existingRecord.Name = billToRecord.Name;
                existingRecord.Address = billToRecord.Address;
                existingRecord.AgreedValue = billToRecord.AgreedValue;
                existingRecord.ChargeValue = billToRecord.ChargeValue;
                existingRecord.ChargePerAnimal = billToRecord.ChargePerAnimal;
                existingRecord.Description = billToRecord.Description;
                existingRecord.ModifiedAt = GeneralPurpose.DateTimeNow();

                // Update the record in the database
                if (await userRepo.UpdateBillToRecordRecord(existingRecord))
                {
                    return RedirectToAction("ViewTransporterDetail", "Admin", new { way = billToRecord.TransporterRecordId, msg = "Record updated successfully", color = "green" });
                }
                else
                {
                    return RedirectToAction("ViewTransporterDetail", "Admin", new { way = billToRecord.TransporterRecordId, msg = "Something went wrong", color = "red" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ViewTransporterDetail", "Admin", new { way = billToRecord.TransporterRecordId, msg = "Something went wrong", color = "red" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteBillToRecord(int id)
        {
            try
            {
                BillToRecord billToRecord = await userRepo.GetBillToRecordById(id);

                if (billToRecord == null)
                {
                    return RedirectToAction("ViewTransporterDetail", "Admin", new { way = billToRecord.TransporterRecordId, msg = "Record not found", color = "red" });
                }

                billToRecord.IsActive = 0;
                billToRecord.DeletedAt = GeneralPurpose.DateTimeNow();
                // Update the record in the database
                if (await userRepo.UpdateBillToRecordRecord(billToRecord))
                {
                    return RedirectToAction("ViewTransporterDetail", "Admin", new { way = billToRecord.TransporterRecordId, msg = "Record deleted successfully", color = "green" });
                }
                else
                {
                    return RedirectToAction("ViewTransporterDetail", "Admin", new { way = billToRecord.TransporterRecordId, msg = "Something went wrong", color = "red" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ViewTransporterDetail", "Admin", new { way = id, msg = "Something went wrong", color = "red" });
            }
        }

        #endregion

        #region ReceivedOrder Record

        [HttpPost]
        public async Task<IActionResult> PostAddReceiviedOrderRecord(ReceiviedOrderRecord record)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(record);
                }
                if (record.BasicCharges == null)
                {
                    record.BasicCharges = 0;
                }
                else if (record.ChargesForExcessValue == null)
                {
                    record.ChargesForExcessValue = 0;
                }
                record.IsActive = 1;
                record.CreatedAt = GeneralPurpose.DateTimeNow();
                record.TotalCharges = record.BasicCharges + record.ChargesForExcessValue + record.VetFees + record.Layovers + record.Other;
                var result = await userRepo.AddReceiviedOrderRecord(record);

                if (result != null)
                {
                    return RedirectToAction("ViewTransporterDetail", new { way = record.TransporterRecordId, msg = "Record Inserted Successfully", color = "green" });
                }
                else
                {
                    return RedirectToAction("ViewTransporterDetail", new { way = record.TransporterRecordId, msg = "Something went wrong", color = "red" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("ViewTransporterDetail", new { way = record.TransporterRecordId, msg = "Something went wrong", color = "red" });

            }
        }

        public async Task<IActionResult> ViewReceiviedOrderRecord(int id, string msg = "", string color = "black")
        {
            var record = await userRepo.GetReceiviedOrderRecordById(id);

            if (record == null)
            {
                return RedirectToAction("Index", new { msg = "Record not found", color = "red" });
            }

            ViewBag.Message = msg;
            ViewBag.Color = color;
            return View(record);
        }

        [HttpPost]
        public async Task<IActionResult> PostEditReceiviedOrderRecord(ReceiviedOrderRecord record)
        {
            var getexistingRecord = await userRepo.GetReceiviedOrderRecordById(record.Id);

            if (!ModelState.IsValid)
            {
                return View(record);
            }

            getexistingRecord.ReceivingDate = record.ReceivingDate;
            getexistingRecord.ReceivedBy = record.ReceivedBy;
            getexistingRecord.Check = record.Check;
            getexistingRecord.By = record.By;
            getexistingRecord.CarrierPerAgentOrDriver = record.CarrierPerAgentOrDriver;
            getexistingRecord.Other = record.Other;
            getexistingRecord.Layovers = record.Layovers;
            getexistingRecord.Payment = record.Payment;
            getexistingRecord.PaymentBy = record.PaymentBy;
            getexistingRecord.VetFees = record.VetFees;
            getexistingRecord.TotalCharges = record.VetFees + record.Layovers + record.Other + record.BasicCharges + record.ChargesForExcessValue;

            getexistingRecord.PaymentDate = record.PaymentDate;
            getexistingRecord.PaymentType = record.PaymentType;
            getexistingRecord.ModifiedAt = DateTime.Now;
            if (await userRepo.UpdateReceiviedOrderRecord(getexistingRecord))
            {
                return RedirectToAction("ViewTransporterDetail", new { way = getexistingRecord.TransporterRecordId, msg = "Record updated successfully", color = "green" });
            }
            else
            {
                return RedirectToAction("ViewTransporterDetail", new { way = getexistingRecord.TransporterRecordId, msg = "something went wrong", color = "red" });

            }
        }


        public async Task<IActionResult> DeleteReceiviedOrderRecord(int id)
        {
            var transporterId = -1;
            try
            {
                var record = await userRepo.GetReceiviedOrderRecordById(id);

                if (record == null)
                {
                    return RedirectToAction("Index", "Admin", new { msg = "Record not found", color = "red" });
                }
                transporterId = record.TransporterRecordId.Value;

                if (await userRepo.DeleteReceiviedOrderRecord(id))
                {
                    return RedirectToAction("ViewTransporterDetail", new { way = record.TransporterRecordId, msg = "Record Deleted successfully", color = "green" });

                }
                else
                {
                    return RedirectToAction("ViewTransporterDetail", new { way = record.TransporterRecordId, msg = "Record can't Deleted", color = "red" });

                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("ViewTransporterDetail", new { way = transporterId, msg = "Record can't Deleted", color = "red" });

            }
        }

        public async Task<IActionResult> PdfHtmlContent(int Id = -1)
        {
            TransporterRecord getTransporterDetail = await userRepo.GetTransporterRecordById(Id);
            var getBillToRecords = await userRepo.GetBillToRecordsByTransporterRecordId(Id);
            var getReceivingOrderRecord = await userRepo.GetReceiviedOrderByTransporterRecordId(Id);

            var basicSumOfChargePerAnimal = await userRepo.GetSumOfChargePerAnimalByTransporterRecordId(Id);
            var sumofChargevalue = await userRepo.GetSumOfChargeValueByTransporterRecordId(Id);

            int? sumOfTotalCharges = null;
            if (getReceivingOrderRecord != null)
            {
               sumOfTotalCharges = basicSumOfChargePerAnimal + sumofChargevalue + getReceivingOrderRecord.Layovers + getReceivingOrderRecord.VetFees + getReceivingOrderRecord.Other;
            }
            else
            {
               sumOfTotalCharges = basicSumOfChargePerAnimal + sumofChargevalue;
            }
            // Create a new instance of GeneratePdfRecordDto and populate all its properties
            GeneratePdfRecordViewModel dtoObj = new GeneratePdfRecordViewModel
            {
                // Populate properties from getTransporterDetail
                Id = getTransporterDetail.Id,
                No = getTransporterDetail?.No,
                Date = getTransporterDetail?.Date,
                DriverId = getTransporterDetail?.DriverId,
                AttendentId = getTransporterDetail?.AttendentId,
                From = getTransporterDetail?.From,
                BarnFrom = getTransporterDetail?.BarnFrom,
                PhoneFrom = getTransporterDetail?.PhoneFrom,
                AddressFrom = getTransporterDetail?.AddressFrom,
                FromCityAndState = getTransporterDetail?.FromCityAndState,
                ShipTo = getTransporterDetail?.ShipTo,
                BarnTo = getTransporterDetail?.BarnTo,
                ToPhoneNumber = getTransporterDetail?.ToPhoneNumber,
                AddressTo = getTransporterDetail?.AddressTo,
                ToCityAndState = getTransporterDetail?.ToCityAndState,
                TractorNumber= getTransporterDetail?.TractorNumber,
                TractorDriver= getTransporterDetail?.TractorDriver,
                TrailerNumber= getTransporterDetail?.TrailerNumber,
                TrailerDriver= getTransporterDetail?.TrailerDriver,

                // Populate properties from getReceivingOrderRecord
                By = getReceivingOrderRecord?.By,
                TransporterRecordId = getReceivingOrderRecord?.TransporterRecordId,
                ReceivingDate = getReceivingOrderRecord?.ReceivingDate,
                ReceivedBy = getReceivingOrderRecord?.ReceivedBy,
                PaymentType = getReceivingOrderRecord?.PaymentType,
                Payment = getReceivingOrderRecord?.Payment,
                PaymentDate = getReceivingOrderRecord?.PaymentDate,
                PaymentBy = getReceivingOrderRecord?.PaymentBy,
                Check = getReceivingOrderRecord?.Check,
                BasicCharges = basicSumOfChargePerAnimal,
                ChargesForExcessValue = sumofChargevalue,
                Layovers = getReceivingOrderRecord?.Layovers,
                VetFees = getReceivingOrderRecord?.VetFees,
                Other = getReceivingOrderRecord?.Other,
                TotalCharges = sumOfTotalCharges,
                //TotalCharges = getReceivingOrderRecord?.TotalCharges,
                CarrierPerAgentOrDriver = getReceivingOrderRecord?.CarrierPerAgentOrDriver,

                // Assign IEnumerable<BillToRecord?> billToRecords
                billToRecords = getBillToRecords
            };
            

            return new ViewAsPdf("~/Views/Admin/PdfHtmlContent.cshtml", dtoObj)
            {
                FileName = "TransporterDetailCopy.pdf"
            };
        }

        public async Task<IActionResult> GeneratePdfCopy(int Id = -1)
        {
            TransporterRecord getTransporterDetail = await userRepo.GetTransporterRecordById(Id);
            var getBillToRecords = await userRepo.GetBillToRecordsByTransporterRecordId(Id);
            var getReceivingOrderRecord = await userRepo.GetReceiviedOrderByTransporterRecordId(Id);

            // Create a new instance of GeneratePdfRecordDto and populate all its properties
            GeneratePdfRecordViewModel dtoObj = new GeneratePdfRecordViewModel
            {
                // Populate properties from getTransporterDetail
                Id = getTransporterDetail.Id,
                No = getTransporterDetail?.No,
                Date = getTransporterDetail?.Date,
                DriverId = getTransporterDetail?.DriverId,
                AttendentId = getTransporterDetail?.AttendentId,
                From = getTransporterDetail?.From,
                BarnFrom = getTransporterDetail?.BarnFrom,
                PhoneFrom = getTransporterDetail?.PhoneFrom,
                AddressFrom = getTransporterDetail?.AddressFrom,
                FromCityAndState = getTransporterDetail?.FromCityAndState,
                ShipTo = getTransporterDetail?.ShipTo,
                BarnTo = getTransporterDetail?.BarnTo,
                ToPhoneNumber = getTransporterDetail?.ToPhoneNumber,
                AddressTo = getTransporterDetail?.AddressTo,
                ToCityAndState = getTransporterDetail?.ToCityAndState,
                TractorNumber = getTransporterDetail?.TractorNumber,
                TractorDriver = getTransporterDetail?.TractorDriver,
                TrailerNumber = getTransporterDetail?.TrailerNumber,
                TrailerDriver = getTransporterDetail?.TrailerDriver,

                // Populate properties from getReceivingOrderRecord
                By = getReceivingOrderRecord?.By,
                TransporterRecordId = getReceivingOrderRecord?.TransporterRecordId,
                ReceivingDate = getReceivingOrderRecord?.ReceivingDate,
                ReceivedBy = getReceivingOrderRecord?.ReceivedBy,
                PaymentType = getReceivingOrderRecord?.PaymentType,
                Payment = getReceivingOrderRecord?.Payment,
                PaymentDate = getReceivingOrderRecord?.PaymentDate,
                PaymentBy = getReceivingOrderRecord?.PaymentBy,
                Check = getReceivingOrderRecord?.Check,
                BasicCharges = getReceivingOrderRecord?.BasicCharges,
                ChargesForExcessValue = getReceivingOrderRecord?.ChargesForExcessValue,
                Layovers = getReceivingOrderRecord?.Layovers,
                VetFees = getReceivingOrderRecord?.VetFees,
                Other = getReceivingOrderRecord?.Other,
                TotalCharges = getReceivingOrderRecord?.TotalCharges,
                CarrierPerAgentOrDriver = getReceivingOrderRecord?.CarrierPerAgentOrDriver,

                // Assign IEnumerable<BillToRecord?> billToRecords
                billToRecords = getBillToRecords
            };

            return new ViewAsPdf("~/Views/Admin/GeneratePdfCopy.cshtml", dtoObj)
            {
                FileName = "TransporterDetail.pdf"
            };
        }

        #endregion

    }
}
