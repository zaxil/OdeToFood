using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurntByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant AddRestaurant(Restaurant newRestaurant);
        int Commit();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant{Id = 1, Name ="Scott's Pizza", Location = "Portland", Cusine = CusineType.Italian },
                new Restaurant{Id = 2, Name ="Cinamon Club", Location = "London", Cusine = CusineType.Indian },
                new Restaurant{Id = 3, Name ="La Costa", Location = "California", Cusine = CusineType.Mexican }
            };
            
        }


        public IEnumerable<Restaurant> GetRestaurntByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
            
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {

            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
                if (restaurant != null)
                {
                    restaurant.Name = updatedRestaurant.Name;
                    restaurant.Location = updatedRestaurant.Location;
                    restaurant.Cusine = updatedRestaurant.Cusine;
                }

                return restaurant;
        }

        public Restaurant AddRestaurant(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }
    }
}
