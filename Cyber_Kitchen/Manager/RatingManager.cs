using Cyber_Kitchen.Entities;
using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace Cyber_Kitchen.Manager
{
    public class RatingManager : IRatingManager
    {

        private ApplicationDbContext _context;
        //private ClaimsPrincipal principal;
        //private string logedInUser;
        //private readonly string sidUser;

        public RatingManager(ApplicationDbContext context)
        {
            _context = context;

            //Get the current claims principal
            //principal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            //// Get the claims values
            //logedInUser = principal.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
            //sidUser = principal.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).Select(c => c.Value).SingleOrDefault();

        }
        // To avoid using operation class used the bool
        public bool CreateRating(RatingModel model)
        {

            //return Operation.Create(() =>
            //{  
            // var adminExist = _context.Ratings.Where(r => r.RestId== model.RestId  && r.UserId== model.UserId).FirstOrDefault();
            //VAR exist = _context.Ratings.Where(r=> r.RestId== model.RestId  && r.Sid == sidUser).FirstOrDefault();


            //var period = _context.Periods.Where(c => c.IsActive == true).FirstOrDefault();
            //var history = _context.Histories.Where(x => x.PeriodId == period.PeriodId/* && x.StaffNo == period.CreatedBy*/).FirstOrDefault();
            //if (history != null) throw new Exception("already exist");



            var isExist = _context.Ratings.Where(c => c.RestId == model.RestId && c.CreatedBy == model.CreatedBy).FirstOrDefault();
            if ( isExist !=null) throw new Exception(" Sorry you can't vote twice");

    
            //model.Sid = sidUser;
            var entity = model.Create(model);
     
          //  model.CreatedDate = DateTime.Now;
            _context.Ratings.Add(entity);
            _context.SaveChanges();

            var resullt = _context.Periods.Where(x => x.IsActive == true).FirstOrDefault();

            //var entity1 = model.Create(model);
            var histry = new History
            {
                StaffNo = model.CreatedBy,
                StartDate = resullt.StartDate,
                EndDate = resullt.EndDate,
                PeriodId = resullt.PeriodId,
            };

            _context.Histories.Add(histry);
            _context.SaveChanges();

            return true;
            //    return model;
            //});
        }
       
        public Operation<History> GetHistories(int histryId)
        {
            return Operation.Create(() =>
            {
                // List<Voter> model = new List<Voter>();
                var entities = _context.Histories.Where(x => x.Id == histryId).FirstOrDefault();
                if (entities == null) throw new Exception("user does not exist");

                return entities;
            });
        }

        public Operation<RatingModel[]> GetRatings()
        {
            return Operation.Create(() =>
            {
                var entities = _context.Ratings.ToList();

                var models = entities.Select(s => new RatingModel(s)
                {
                    // this are fk to aviod the id displaying in the UI
                    Voters = new VoterModel(s.Voter),
                    Restaurant = new RestaurantModel(s.Restaurant),
                    //Periods = new PeriodModel(s)
                    //User = new ApplicationUser(s.User)
                    // u can also do this
                    RestId = s.RestId,
                    PeriodId = s.PeriodId,
                    Taste = s.Taste,
                    TimeLiness = s.TimeLiness,
                    Quality = s.Quality,
                    Quantity = s.Quantity,
                    CustomerServices = s.CustomerServices,
                    CreatedBy = s.CreatedBy,
                    IsChecked = s.IsChecked,
                    AmountPay = s.AmountPay,
                    IsBackTo = s.IsBackTo,
                    IsCatererSelected = s.IsCatererSelected,
                }

                ).ToArray();
                return models;
            });
        }

        public Operation<RatingModel> UpdateRating(RatingModel model)
        {
            return Operation.Create(() =>
            {
                //model.Validate();
                //var isExist = _context.Scores.Where(c => c.ScoreId == model.ScoreId).AsNoTracking().FirstOrDefault();
                var isExist = _context.Ratings.Find(model.RatId);
                if (isExist == null) throw new Exception("rating does not exist");
                var entity = model.Edit(isExist, model);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();

                return model;
            });
        }
        public Operation<RatingModel> GetRatingById(int ratId)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Ratings.Find(ratId);
                if (entity != null) throw new Exception("rating  exist");
                return new RatingModel(entity);


            });
        }

        #region PRIOD
        public Operation<PeriodModel[]> GetPeriods()
        {
            return Operation.Create(() =>
            {
                var entities = _context.Periods.ToList();
                var model = entities.Select(e => new PeriodModel(e)
                {
                    PeriodName = e.PeriodName,
                    //IsApplicationActive = e.IsApplicationActive,
                    CreatedBy = e.CreatedBy,

                }
                ).ToArray();
                return model;
            });
        }

        public bool CreatePeriod(PeriodModel model)
        {
            var entity = _context.Periods.Where(p => p.PeriodName == model.PeriodName).FirstOrDefault();
            if (entity != null) throw new Exception("Period already exist");

            var period = model.Create(model);
            _context.Periods.Add(period);
            _context.SaveChanges();
            return true;
        }

        public Operation<PeriodModel> GetPeriodById(int id)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Periods.Find(id);
                if (entity != null) throw new Exception("Period already exist");
                return new PeriodModel();
            });
        }


        public void ActivateInstructor(int instructorId)
        {
            var instructor = _context.Periods.Find(instructorId);
            instructor.IsActive = true;
            _context.Entry(instructor).State = EntityState.Modified;
            _context.SaveChanges();

            //var entity = model.Edit(isExist, model);
            //_unitOfWork.Commit();
            //_context.Entry(mike).State = EntityState.Modified;
            //_context.SaveChanges();
        }

        public void DeactivateInstructor(int instructorId)
        {
            var instructor = _context.Periods.Find(instructorId);
            instructor.IsActive = false;
            _context.Entry(instructor).State = EntityState.Modified;
            _context.SaveChanges();

            //_instructorRepository.Update(instructor);
            //_unitOfWork.Commit();
        }

        #endregion

        public Operation<List<SummaryReportModel>> GetRestaurantSummaryReport()
        {
            return Operation.Create(() =>
            {
                var query = (from r in _context.Restaurants.Include("Ratings").Include("AmountPrices")
                             group r by r.RestId into g
                             select new SummaryReportModel
                             {
                                 RestId = g.Key,
                                 RestName = g.Select(c => c.RestName).FirstOrDefault(),
                                 EntryDate = g.Select(c => c.CreatedDate).FirstOrDefault(),
                                 RestSum = g.Select(c => c.Ratings).Sum(v => v.Sum(r => 
                                 r.Quality + r.Quantity + r.Taste + r.TimeLiness + r.CustomerServices)),
                                 Taste = g.Select(c => c.Ratings).Sum(v => v.Sum(r => r.Taste)),
                                 Quality = g.Select(c => c.Ratings).Sum(v => v.Sum(r => r.Quality)),
                                 Quantity = g.Select(c => c.Ratings).Sum(v => v.Sum(r => r.Quantity)),

                                 //CreatedBy = g.Select(c =>c.)
                                 CustomerServices = g.Select(c => c.Ratings).Sum(v => v.Sum(r => r.CustomerServices)),
                                 TimeLiness = g.Select(c => c.Ratings).Sum(v => v.Sum(r => r.TimeLiness)) 

                             }).OrderByDescending(c => c.RestSum.Value).ToList();
                
                // sum (c=> c.sum(x =>x.total))
                // b = 5
                // a = a + b 
                return query;
            });
        }
        public Operation DeleteRating(int id)
        {
            return Operation.Create(() =>
            {
                var entity = _context.Ratings.Find(id);
                if (entity == null) throw new Exception("rating does not exist");

                _context.Ratings.Remove(entity);
                _context.SaveChanges();
            });
        }
    }
}