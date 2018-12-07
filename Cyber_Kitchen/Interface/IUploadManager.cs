using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Interface
{
    public interface IUploadManager
    {
        Operation<List<VoterModel>> UploadStaffNames(Stream stream, VoterModel model);
       //  Operation<List<UserModel>> Users(Stream stream, UserModel model);
    }
}