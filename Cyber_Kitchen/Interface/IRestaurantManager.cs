using Cyber_Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Interface
{
    public interface IRestaurantManager
    {
        Operation<RestaurantModel> CreateRestaurant(RestaurantModel model);
        Operation<RestaurantModel[]> GetRestaurants();
        Operation<RestaurantModel> UpdateRestaurant(RestaurantModel model);
        Operation<RestaurantModel> GetRestaurantById(int restaurantId);
        //Operation Details(int id);
        Operation DeleteRestaurant(int id);
        Operation<AmountPriceModel[]> GetAmountPrices();
        Operation<AmountPriceModel> CreateAmountPrice(AmountPriceModel model);
        Operation<AmountPriceModel> GetAmountPriceByCreatedBy(string amountId);
    }
}