//using Cyber_Kitchen.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Cyber_Kitchen.Entities;
//using System.Threading.Tasks;
//using Cyber_Kitchen.Models;

//namespace Cyber_Kitchen.Manager
//{
//    public class FetchManager : IFetchAmount
//    {
//        private readonly ApplicationDbContext _context;
//        public FetchManager()
//        {
//            _context = new ApplicationDbContext();
//        }
//        public Task<IEnumerable<AmountPrice>> getPriceCount()
//        {
//            var result = from amount in _context.AmountPrices.ToList()
//                         select new
//                         {
//                             fiveHundered = amount.wh
//                         };
//        }
//    }
//}