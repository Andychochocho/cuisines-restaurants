using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace cuisineRestaurants
{
  public class CuisinesTest : IDisposable
  {
    public CuisinesTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurantCuisine_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_CuisinesEmptyAtFirst()
    {
      int result = Cuisines.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_CuisinesReturnTrueForSameName()
    {
      Cuisines firstCuisines = new Cuisines("Chinese");
      Cuisines secondCuisines = new Cuisines("Chinese");
      Assert.Equal(firstCuisines, secondCuisines);
    }

    [Fact]
   public void Test_Save_SavesCuisineToDatabase()
   {

     Cuisines testCuisines = new Cuisines("Equidorian");
     testCuisines.Save();

     List<Cuisines> result = Cuisines.GetAll();
     List<Cuisines> testList = new List<Cuisines>{testCuisines};

     Assert.Equal(testList, result);
   }

   [Fact]
   public void Test_Find_FindsCuisinesInDatabase()
   {
     Cuisines testCuisines = new Cuisines("Egyptian");
     testCuisines.Save();

     Cuisines foundCuisines = Cuisines.Find(testCuisines.GetId());

     Assert.Equal(testCuisines, foundCuisines);
   }

   [Fact]
   public void Test_GetCuisines_RetrieveAllRestaurantsWithCuisines()
   {
     Cuisines testCuisines = new Cuisines("Indian");
     testCuisines.Save();

     Restaurants firstRestaurant = new Restaurants("TajMaHal", "2nd and Madison", new DateTime(2016, 2, 14), "blue", testCuisines.GetId());
     firstRestaurant.Save();
     Restaurants secondRestaurant = new Restaurants("TajMaHal", "2nd and Madison", new DateTime(2016, 2, 14), "blue", testCuisines.GetId());
     secondRestaurant.Save();

     List<Restaurants> testRestaurantsList = new List<Restaurants> {firstRestaurant, secondRestaurant};
     List<Restaurants> resultRestaurantsList = testCuisines.GetRestaurants();

     Assert.Equal(testRestaurantsList, resultRestaurantsList);
   }

    public void Dispose()
    {
      Restaurants.DeleteAll();
      Cuisines.DeleteAll();
    }
  }
}
