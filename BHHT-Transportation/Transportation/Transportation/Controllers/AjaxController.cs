using Microsoft.AspNetCore.Mvc;
using Transportation.HelpingClasses;
using Transportation.Models;
using Transportation.Repositories;
using static Transportation.HelpingClasses.DtoCollection;

namespace Transportation.Controllers
{
    public class AjaxController : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly GeneralPurpose gp;

        public AjaxController(IUserRepo _userRepo, IHttpContextAccessor haccess)
        {
            userRepo = _userRepo;
            gp = new GeneralPurpose(haccess);
        }


        #region User

        [HttpPost]
        public async Task<IActionResult> GetUserDataTableList(int way, string Role = "", string email = "")
        {
            var ulist = await userRepo.GetActiveUserList();
            ulist = ulist.Where(x => x.Role == way).ToList();

            if (!String.IsNullOrEmpty(Role))
            {
                ulist = ulist.Where(x => x.Role.ToString().Contains(Role.ToLower())).ToList();
            }             

            if (!String.IsNullOrEmpty(email))
            {
                ulist = ulist.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToList();
            }

            int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            int length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            string searchValue = Request.Form["search[value]"].FirstOrDefault();
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            if (sortColumnName != "" && sortColumnName != null)
            {
                if (sortColumnName != "0")
                {
                    if (sortDirection == "asc")
                    {
                        ulist = ulist.OrderByDescending(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList();
                    }
                    else
                    {
                        ulist = ulist.OrderBy(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList();
                    }
                }
            }

            int totalrows = ulist.Count();

            //datatable filter
            if (!string.IsNullOrEmpty(searchValue))
            {
                ulist = ulist.Where(x => x.Contact != null && x.Contact.ToLower().Contains(searchValue.ToLower())
                                    ||  x.FirstName != null && x.FirstName.ToLower().Contains(searchValue.ToLower())
                                    ||  x.LastName != null && x.LastName.ToLower().Contains(searchValue.ToLower())
                                    ).ToList();
            }

            int totalrowsafterfilterinig = ulist.Count();


            // pagination
            ulist = ulist.Skip(start).Take(length).ToList();

            List<UserDto> udto = new List<UserDto>();
            var userCreatedBy = "";
            foreach (User u in ulist)
            {



                if(u.CreatedBy==0)
                {
                    userCreatedBy = "Super Admin";
                }
                else
                {
                    userCreatedBy = "Admin";

                }
                UserDto obj = new UserDto()
                {
                    Id = u.Id.ToString(),
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Contact = u.Contact,
                    CreatedBy=userCreatedBy
                };

                udto.Add(obj);
            }

            return Json(new { data = udto, draw = Request.Form["draw"].FirstOrDefault(), recordsTotal = totalrows, recordsFiltered = totalrowsafterfilterinig });
        }

        [HttpPost]
        public async Task<IActionResult> GetUserById(string id)
        {
            User? u = await userRepo.GetUserById(int.Parse(id));
            if (u == null)
            {
                return Json(0);
            }

            UserDto obj = new UserDto()
            {
                Id = u.Id.ToString(),
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Contact = u.Contact,
            };

            return Json(obj);
        }
        #endregion


        [HttpPost]
        public async Task<IActionResult> ValidateEmail(string email, int id)
        {
            return Json(await userRepo.ValidateEmail(email, id));
        }


        #region Transporter Record
        
        [HttpPost]
        public async Task<IActionResult> GetDriverUsers(int role=-1)
        {
            try
            {
                var driverUsers = await userRepo.GetActiveUserList(); // Get all active users
                driverUsers = driverUsers.Where(x => x.Role == role).ToList(); // Filter by Role = 2 (driver)

                List<UserDto> driverUserDto = new List<UserDto>();
                foreach (User user in driverUsers)
                {
                    UserDto userDto = new UserDto()
                    {
                        Id = user.Id.ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        
                    };

                    driverUserDto.Add(userDto);
                }

                return Json(driverUserDto);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                return Json(new { error = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> GetTransporterRecordDataTable(string date = "")
        {
            User? loggedinUser = gp.GetUserClaims();
            if (loggedinUser == null)
            {
                return RedirectToAction("Login", "Auth", new { msg = "session expire", color = "red" });

            }

            
            var recordlist = await userRepo.GetActiveTransporterRecordList();
            var recordCount=recordlist.Count();

            if(loggedinUser.Role==2 && recordCount != 0)
            {
                recordlist=recordlist.Where(x=>x.DriverId!=null && x.DriverId==loggedinUser.Id).ToList();
            }
            //if (!String.IsNullOrEmpty(Role))
            //{
            //    ulist = ulist.Where(x => x.Role.ToString().Contains(Role.ToLower())).ToList();
            //}

            if (!String.IsNullOrEmpty(date))
            {
                recordlist = recordlist.Where(x => x.Date.Trim().ToLower().Contains(date)).ToList();
            }

            int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            int length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            string searchValue = Request.Form["search[value]"].FirstOrDefault();
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();

            if (sortColumnName != "" && sortColumnName != null)
            {
                if (sortColumnName != "0")
                {
                    if (sortDirection == "asc")
                    {
                        recordlist = recordlist.OrderByDescending(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList();
                    }
                    else
                    {
                        recordlist = recordlist.OrderBy(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList();
                    }
                }
            }

            int totalrows = recordlist.Count();

            //datatable filter
            if (!string.IsNullOrEmpty(searchValue))
            {
                recordlist = recordlist.Where(x => x.Date!=null && x.Date.ToLower().Contains(searchValue.ToString())
                                    || x.BarnTo != null && x.BarnTo.ToLower().Contains(searchValue.ToLower())
                                    || x.BarnFrom != null && x.BarnFrom.ToLower().Contains(searchValue.ToLower()) || x.TractorNumber!=null && x.TractorNumber.ToLower().Contains(searchValue.ToString()) ||x.TrailerDriver!=null && x.TractorDriver.ToLower().Contains(searchValue.ToString()) || x.TrailerNumber!=null && x.TrailerNumber.ToLower().Contains(searchValue.ToString()) ||x.TrailerDriver!=null &&x.TrailerDriver.ToLower().Contains(searchValue)
                                    ).ToList();
            }

            int totalrowsafterfilterinig = recordlist.Count();


            // pagination
            recordlist = recordlist.Skip(start).Take(length).ToList();

            List<TransporterRecordDto> tdto = new List<TransporterRecordDto>();
            foreach (TransporterRecord _rec in recordlist)
            {
                // COOMENT OUT FOR DRIVER AND ATTENDANT
                //var getAttendent = await userRepo.GetUserById(_rec.AttendentId.Value);
                //var getDriver = await userRepo.GetUserById(_rec.DriverId.Value);
                //var attendentName = "";
                //var driverName = "";
                //if(getAttendent != null && getDriver!=null) 
                //{
                //    attendentName = getAttendent.FirstName + " " + getAttendent.LastName;
                //    driverName = getDriver.FirstName + " " + getDriver.LastName;
                //}
                //else
                //{
                //    attendentName = "N/A";
                //    driverName = "N/A";
                //}
                
                TransporterRecordDto obj = new TransporterRecordDto()
                {
                    Id = _rec.Id,
                    Date=_rec.Date,
                    
                    BarnFrom=_rec.BarnFrom,
                    BarnTo=_rec.BarnTo,
                    tractorNumber=_rec.TractorNumber,
                    tractorDriver=_rec.TractorDriver,
                    trailerDriver=_rec.TrailerDriver,
                    trailerNumber=_rec.TrailerNumber,

                };

                tdto.Add(obj);
            }

            return Json(new { data = tdto, draw = Request.Form["draw"].FirstOrDefault(), recordsTotal = totalrows, recordsFiltered = totalrowsafterfilterinig });
        }

        [HttpPost]
        public async Task<IActionResult> GetTransporterRecordById(int? id)
        {
            TransporterRecord? record = await userRepo.GetTransporterRecordById(id.Value);
            if (record == null)
            {
                return Json(null);
            }

            TransporterRecordDto obj = new TransporterRecordDto()
            {
                Id = record.Id,
                Date = record.Date,
                DriverId = record.DriverId,
                AttendentId = record.AttendentId,
                From = record.From,
                BarnFrom = record.BarnFrom,
                BarnTo = record.BarnTo,
                PhoneFrom = record.PhoneFrom,
                ToPhoneNumber = record.ToPhoneNumber,
                AddressFrom = record.AddressFrom,
                AddressTo = record.AddressTo,
                FromCityAndState = record.FromCityAndState,
                ToCityAndState = record.ToCityAndState,
                ShipTo = record.ShipTo,
                tractorNumber=record.TractorNumber,
                tractorDriver=record.TractorDriver,
                trailerNumber=record.TrailerNumber,
                trailerDriver=record.TrailerDriver,

               
            };

            return Json(obj);
        }
        #endregion

        #region Bill To Record

        [HttpPost]
        public async Task<IActionResult> GetBillToRecordById(int id = -1)
        {
            try
            {
                var billToRecord =await userRepo.GetBillToRecordById(id); 

                if (billToRecord == null)
                {
                    return Json(null); 
                }

                return Json(billToRecord);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        #endregion

    }
}
