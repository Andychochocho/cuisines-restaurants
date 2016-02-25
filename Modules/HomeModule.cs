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
      // Get["/testing"]=_=>{
      //   return View ["index.cshtml"];
      // };
      Post["/"] = _ => {
        Cuisines newCuisines = new Cuisines(Request.Form["cuisine-name"]);
        newCuisines.Save();
        List<Cuisines> allCuisines = Cuisines.GetAll();
        return View["index.cshtml", allCuisines];
      };
    }
  }
}
