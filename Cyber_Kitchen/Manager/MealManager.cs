using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Manager
{
    public class MealManager : IMealManager
    {
        private ApplicationDbContext _context;
        //private UserManager<ApplicationUser> _userManager ;
        //public MealManager(UserManager<ApplicationUser> userManager)
       public MealManager(ApplicationDbContext context)
        {
           // _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            _context = context;

        }
        public Operation<MealModel> ClockIn(MealModel model)
        {
            return Operation.Create(() => 
            {
                model.Status = true;
                var user = _context.Meals.Where(a =>a.StaffId == model.StaffId);
                //var user = Microsoft.AspNet.Identity.UserManager.FindById(User.Identity.GetUserId());
                //if (user != null)
                   // ApplicationUser user = _userManager.FindAsync("mikel@gmail.com", "Password123@").Result;/// find the user by the staff id//
                
                //model.Validate();
                //var isExists = _userManager.(c => c.StaffId == model.StaffId).FirstOrDefault();
                //if (isExists != null) throw new Exception("meal already exist");

                //var entity = model.Create(model);
                //_context.Meals.Add(entity);
                //_context.SaveChanges();

                return model;
            });
        }
        //public Operation<MealModel[]> GetMeals()
        //{
        //    return Operation.Create(() =>
        //    {
        //        //var entities = _context.Meals.ToList();

        //        var models = entities.Select(c => new MealModel(c)
        //        {
        //            //Voters = new VoterModel(c.Voter),
        //            Restaurant = new RestaurantModel(c.Restaurant),
        //            //User =  new ApplicationUser(c.User)
        //        }
        //        ).ToArray();
        //        return models;
        //    });
        //}
        //public Operation<MealModel> UpdateMeal(MealModel model)
        //{
        //    return Operation.Create(() =>
        //    {
        //        //model.Validate();
        //        var isExist = _context.Meals.Find(model.Id);
        //        if (isExist == null) throw new Exception("Meal does not exist");

        //        var entity = model.Edit(isExist, model);
        //        _context.Entry(entity);
        //        _context.SaveChanges();
        //        return model;
        //    });
        //}
        //public Operation<MealModel> GetMealById(int mealId)
        //{
        //    return Operation.Create(() =>
        //    {
        //        var entity = _context.Meals.Where(c => c.Id == mealId).FirstOrDefault();
        //        if (entity == null) throw new Exception("Meal does not exist");
        //        return new MealModel(entity);

        //    });
        //}
        //public Operation DeleteMeal(int id)
        //{
        //    return Operation.Create(() =>
        //    {
        //        var entity = _context.Meals.Find(id);
        //        if (entity == null) throw new Exception("Meal does not exist");

        //        _context.Meals.Remove(entity);
        //        _context.SaveChanges();
        //    });
        //}
    }
    }