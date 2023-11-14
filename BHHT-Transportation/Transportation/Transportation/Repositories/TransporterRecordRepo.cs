//using Microsoft.EntityFrameworkCore;
//using Transportation.Models;

//namespace Transportation.Repositories
//{
//    public class TransporterRecordRepo
//    {
//        public interface ITransporterRecordRepo
//        {
            
//            Task<bool> AddTransporterRecord(TransporterRecord record);
            
//        }
//        public class TransporterRecordRepo : ITransporterRecordRepo
//        {
//            private readonly AppDbContext context;

//            public TransporterRecordRepo(AppDbContext _appDbContext)
//            {
//                context = _appDbContext;
//            }

//            public async Task<bool> AddTransporterRecord(TransporterRecord _record)
//            {
//                try
//                {
//                    context.TransporterRecord.Add(_record);
//                    await context.SaveChangesAsync();
//                    return true;
//                }
//                catch
//                {
//                    return false;
//                }
//            }


//        }


//    }
//}
