using Cyber_Kitchen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Cyber_Kitchen.Interface
{
    public interface IFetchAmount
    {
        Task<IEnumerable<AmountPrice>> getPriceCount();
    }
}