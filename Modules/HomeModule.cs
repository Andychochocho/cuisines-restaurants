using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace cuisineRestaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get ["/"] = _ => {
        List<Cuisines> allCuisines = Cuisines.GetAll();
        return View ["index.cshtml", allCuisines];
      };
      Get ["/cuisine/new"] = _ =>{
        return View ["cuisine_info.cshtml"];
      };
      Post["/"] = _ => {
        Cuisines newCuisines = new Cuisines(Request.Form["cuisine-name"]);
        newCuisines.Save();
        List<Cuisines> allCuisines = Cuisines.GetAll();
        return View["index.cshtml", allCuisines];
      };
      Get["/cuisine/ClearAll"] = _ => {
        Cuisines.DeleteAll();
        return View ["cuisineClearAll.cshtml"];
      };
      Get["/cuisines/{id}"] =Parameters=> {
        Dictionary <string, object> model = new Dictionary <string, object>();
        var selectedCuisine = Cuisines.Find(Parameters.id);
        var Cusinerestaturant = selectedCuisine.GetRestaurants();
        model.Add ("cuisine", selectedCuisine);
        model.Add("restaurant", Cusinerestaturant);
        return View["cuisine.cshtml", model];
      };

      Get["/cuisines/restaurantAdd"] = _ => {
        List<Cuisines> AllCuisines = Cuisines.GetAll();
        return View["addRestaurant.cshtml", AllCuisines];
      };

      Post["/cuisines/viewRestaurant"] = Parameters => {
        DateTime dt = Convert.ToDateTime((string)Request.Form["date"]);
        Restaurants newRestaurants = new Restaurants(Request.Form["name"], Request.Form["location"], dt, Request.Form["color"], Request.Form["cuisines-Id"]);
        newRestaurants.Save();
        return View["success.cshtml", newRestaurants];
      };
      Get["cuisines/edit/{id}"] = Parameters => {
        Cuisines SelectedCuisines = Cuisines.Find(Parameters.id);
        return View["cuisines_edit.cshtml", SelectedCuisines];
      };

      Patch["/cuisines/edit/{id}"]=Parameters=>{
      Cuisines newCusisines = Cuisines.Find(Parameters.id);
      newCusisines.Update(Request.Form["cuisine-name"]);
      return View ["success.cshtml"];
    };
  }
}
}
