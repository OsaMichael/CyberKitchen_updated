using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Interface
{
    public interface IVoterManager
    {
        Operation<VoterModel> CreateVoter(VoterModel model);
        Operation<VoterModel[]> GetVoters();
        Operation<Voter> GetVoters(string email);
        Operation<VoterModel> UpdateVoter(VoterModel model);
        Operation<VoterModel> GetVoterById(int voterId);
        Operation Details(int id);
        Operation DeleteVoter(int id);
       
        Operation<List<VoterModel>> UploadVoterNames(Stream stream, VoterModel model);
        ///////////////////////////////////////////

       

    }
}