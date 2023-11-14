using Microsoft.EntityFrameworkCore;
using Transportation.Models;
//test using

namespace Transportation.Repositories
{
    public interface IUserRepo
    {
        Task<User?> GetUserByLogin(string email, string password);
        Task<User?> GetUserById(int id);
        Task<BillToRecord?> GetBillToRecordById(int id);
        Task<TransporterRecord?> GetTransporterRecordById(int id);
        Task<int?> GetActiveUserCount(int role);
        Task<int?> GetActiveTransporterRecordListCount();        
        Task<IEnumerable<User>> GetActiveUserList();
        Task<IEnumerable<TransporterRecord>> GetActiveTransporterRecordList();
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> UpdateTransporterRecord(TransporterRecord record);
        Task<bool> UpdateBillToRecordRecord(BillToRecord record);
        Task<bool> DeleteUser(int id);
        Task<bool> DeleteTransporterRecord(int id);
        Task<bool> ValidateEmail(string email, int? id);
        Task<User?> GetUserByEmail(string email);
        Task<int?> AddTransporterRecord(TransporterRecord record);
        Task<int?> AddBillToRecord(BillToRecord record);
        Task<int?> GetSumOfChargePerAnimalByTransporterRecordId(int? id);
        Task<int?> GetSumOfChargeValueByTransporterRecordId(int? id);
        Task<IEnumerable<BillToRecord>> GetBillToRecordsByTransporterRecordId(int id);
        Task<int?> AddReceiviedOrderRecord(ReceiviedOrderRecord record);
        Task<ReceiviedOrderRecord?> GetReceiviedOrderRecordById(int id);
        Task<ReceiviedOrderRecord?> GetReceiviedOrderByTransporterRecordId(int id);
        Task<bool> UpdateReceiviedOrderRecord(ReceiviedOrderRecord record);
        Task<bool> DeleteReceiviedOrderRecord(int id);

    }
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext context;

        public UserRepo(AppDbContext _appDbContext)
        {
            context = _appDbContext;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await context.User.FirstOrDefaultAsync(x => x.Id == id && x.IsActive == 1);
        }
        public async Task<TransporterRecord?> GetTransporterRecordById(int id)
        {
            return await context.TransporterRecord.FirstOrDefaultAsync(x => x.Id == id && x.IsActive == 1);
        }

        public async Task<int?> GetActiveUserCount(int role)
        {
            return await context.User.CountAsync(x => x.IsActive == 1 && x.Role == role);
        }

        public async Task<User?> GetUserByLogin(string email, string password)
        {
            return await context.User.FirstOrDefaultAsync(x => x.IsActive == 1 && x.Email!.ToLower() == email.Trim().ToLower() && x.Password == password);
        }

        public async Task<IEnumerable<User?>> GetActiveUserList()
        {
            var User = await context.User.Where(x => x.IsActive == 1).OrderByDescending(x=>x.Id).ToListAsync();

            return User;
        }
        public async Task<IEnumerable<TransporterRecord?>> GetActiveTransporterRecordList()
        {
            var TransporterRecord = await context.TransporterRecord.Where(x => x.IsActive == 1).OrderByDescending(x => x.Id).ToListAsync();

            return TransporterRecord;
        }

        public async Task<int?> GetActiveTransporterRecordListCount()
        {
            var TransporterRecord = await context.TransporterRecord.ToListAsync();

            return TransporterRecord.Count;
        }
        public async Task<bool> AddUser(User user)
        {
            try
            {
                context.User.Add(user);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateTransporterRecord(TransporterRecord record)
        {
            try
            {
                context.Entry(record).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                User? user = await GetUserById(id);
                user!.IsActive = 0;
                return await UpdateUser(user);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteTransporterRecord(int id)
        {
            try
            {
                TransporterRecord? record = await GetTransporterRecordById(id);
                record!.IsActive = 0;
                record.DeletedAt = DateTime.UtcNow;
                return await UpdateTransporterRecord(record);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ValidateEmail(string email, int? id)
        {
            int emailCount = 0;

            if (id == null)
            {
                emailCount = await context.User.CountAsync(x => x.IsActive == 1 && x.Email!.ToLower() == email.ToLower().Trim());
            }
            else
            {
                emailCount = await context.User.CountAsync(x => x.IsActive == 1 && x.Id != id && x.Email!.ToLower() == email.ToLower().Trim());
            }

            return emailCount == 0;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await context.User.FirstOrDefaultAsync(x => x.Email!.ToLower() == email.Trim().ToLower() && x.IsActive == 1);
        }

        #region transporter record
        public async Task<int?> AddTransporterRecord(TransporterRecord record)
        {
            try
            {
                context.TransporterRecord.Add(record);
                await context.SaveChangesAsync();
                return record.Id;
            }
            catch
            {
                return null; // Return null in case of an exception or error
            }
        }


        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                context.Entry(user).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Bill To Record
        public async Task<int?> AddBillToRecord(BillToRecord _record)
        {
            try
            {
                context.BillToRecord.Add(_record);
                await context.SaveChangesAsync();
                return _record.Id;
            }
            catch
            {
                return null; // Return null in case of an exception or error
            }
        }

        public async Task<IEnumerable<BillToRecord>> GetBillToRecordsByTransporterRecordId(int id)
        {
            return await context.BillToRecord
                .Where(record => record.TransporterRecordId == id && record.IsActive == 1)
                .ToListAsync();
        }

        public async Task<int?> GetSumOfChargePerAnimalByTransporterRecordId(int? id)
        {
            return await context.BillToRecord
                .Where(record => record.TransporterRecordId == id && record.IsActive == 1)
                .SumAsync(record => record.ChargePerAnimal ?? 0); // Use ?? 0 to handle null values
        }
        public async Task<int?> GetSumOfChargeValueByTransporterRecordId(int? id)
        {
            return await context.BillToRecord
                .Where(record => record.TransporterRecordId == id && record.IsActive == 1)
                .SumAsync(record => record.ChargeValue ?? 0); // Use ?? 0 to handle null values
        }


        public async Task<BillToRecord?> GetBillToRecordById(int id)
        {
            return await context.BillToRecord.FirstOrDefaultAsync(x => x.Id == id && x.IsActive == 1);
        }

        public async Task<bool> UpdateBillToRecordRecord(BillToRecord record)
        {
            try
            {
                context.Entry(record).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Receiving Order Reccord

        public async Task<int?> AddReceiviedOrderRecord(ReceiviedOrderRecord record)
        {
            try
            {
                context.ReceiviedOrderRecord.Add(record);
                await context.SaveChangesAsync();
                return record.Id;
            }
            catch
            {
                return null; // Return null in case of an exception or error
            }
        }

        public async Task<ReceiviedOrderRecord?> GetReceiviedOrderRecordById(int id)
        {
            return await context.ReceiviedOrderRecord.FirstOrDefaultAsync(x => x.Id == id && x.IsActive == 1);
        }
        public async Task<ReceiviedOrderRecord?> GetReceiviedOrderByTransporterRecordId(int id)
        {
            return await context.ReceiviedOrderRecord.FirstOrDefaultAsync(x => x.TransporterRecordId == id && x.IsActive == 1);
        }

        public async Task<bool> UpdateReceiviedOrderRecord(ReceiviedOrderRecord record)
        {
            try
            {
                context.Entry(record).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteReceiviedOrderRecord(int id)
        {
            try
            {
                ReceiviedOrderRecord? record = await GetReceiviedOrderRecordById(id);
                record!.IsActive = 0;
                record.DeletedAt = DateTime.UtcNow;
                return await UpdateReceiviedOrderRecord(record);
            }
            catch
            {
                return false;
            }
        }


        #endregion
    }
}
