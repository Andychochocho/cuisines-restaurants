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
     Restaurants secondRestaurant = new Restaurants("TajMaBal", "3rd and Madison", new DateTime(2014, 8, 8), "red", testCuisines.GetId());
     secondRestaurant.Save();

     List<Restaurants> testRestaurantsList = new List<Restaurants> {firstRestaurant, secondRestaurant};
     List<Restaurants> resultRestaurantsList = testCuisines.GetRestaurants();

     Assert.Equal(testRestaurantsList, resultRestaurantsList);
   }

   [Fact]
   public void Test_Update_UpdatesCuisinesInDatabase()
   {
     string name = "American";
     Cuisines testCuisines = new Cuisines(name);
     testCuisines.Save();
     string newCuisines = "Chinese";

     testCuisines.Update(newCuisines);
     string result = testCuisines.GetName();
     Assert.Equal(newCuisines,result);
   }

   [Fact]
   public void Test_Delete_DeletesCuisinesFromDatabase()
   {
     string name1 = "Ethiopian";
     Cuisines testCuisines1 = new Cuisines(name1);
     testCuisines1.Save();

     string name2 = "Brazilian";
     Cuisines testCuisines2 = new Cuisines(name2);
     testCuisines2.Save();

     Restaurants testRestaurants1 = new Restaurants("Chipotle", "2nd and Stark", new DateTime(1980, 12, 11), "red", testCuisines1.GetId());
     testRestaurants1.Save();

     Restaurants testRestaurants2 = new Restaurants("BurgerKing", "Division and 95th", new DateTime(1921, 02, 23), "turqoiuse", testCuisines2.GetId());
     testRestaurants2.Save();

     testCuisines1.Delete();
     List<Cuisines> resultCuisines = Cuisines.GetAll();
     List<Cuisines> testCuisinesList = new List<Cuisines> {testCuisines2};

     List<Restaurants> resultRestaurants = Restaurants.GetAll();
     List<Restaurants> testRestaurantsList = new List<Restaurants> {testRestaurants2};

     Assert.Equal(testCuisinesList, resultCuisines);
     Assert.Equal(testRestaurantsList, resultRestaurants);
   }
    public void Dispose()
    {
      Restaurants.DeleteAll();
      Cuisines.DeleteAll();
    }
  }
}
