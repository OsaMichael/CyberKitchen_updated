using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Interface
{
    public interface ICatererInfoManager
    {
        Operation<CatererInfoModel> CreateCatererInfo(CatererInfoModel model);
        Operation<CatererInfoModel[]> GetCatererInfos();
        Operation<CatererInfoModel> UpdateCatererInfo(CatererInfoModel model);
        Operation<CatererInfoModel> GetCatererInfoById(int caterId);
        Operation DeleteCatererInfo(int id);
    }
}