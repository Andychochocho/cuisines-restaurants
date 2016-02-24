using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

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

    public void Dispose()
    {
      // Restaurants.DeleteAll();
      Cuisines.DeleteAll();
    }
  }
}
