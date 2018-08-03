using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Interface
{
    public interface IMealManager 
    {
        //Operation<MealModel> CreateMeal(MealModel model);
        Operation<MealModel> ClockIn(MealModel model);
        //Operation<MealModel[]> GetMeals();
        //Operation<MealModel> UpdateMeal(MealModel model);
        //Operation<MealModel> GetMealById(int mealId);
        //Operation DeleteMeal(int id);
    }
}