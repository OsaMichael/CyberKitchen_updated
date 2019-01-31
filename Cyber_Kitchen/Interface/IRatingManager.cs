using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Interface
{
    public interface IRatingManager
    {

       // Operation<LoginModel> VerifyUser(string UserName, string Password);

        bool CreateRating(RatingModel model);
        Operation<RatingModel[]> GetRatings();
        Operation<RatingModel> UpdateRating(RatingModel model);
        Operation<RatingModel> GetRatingById(int ratId);
        Operation<List<SummaryReportModel>> GetRestaurantSummaryReport();
        Operation DeleteRating(int id);
        //Operation<List<RankingModel>> GetRanking();
        //Operation DeleteSummaryReport(int id);
        //Operation<SummaryReportModel> GetSummaryReportById(int RestId);

        Operation<PeriodModel[]> GetPeriods();
        bool CreatePeriod(PeriodModel model);
        Operation<PeriodModel> GetPeriodById(int id);
        void DeactivateInstructor(int instructorId);
        void ActivateInstructor(int instructorId);
<<<<<<< HEAD
        Operation<History> GetHistories(int histryId);
=======
>>>>>>> 45d48fb9b9502dc94d7482958e81beb27f2b68e7
       // bool CreateRating(RatingModel model, int PeriodId);
    }
}