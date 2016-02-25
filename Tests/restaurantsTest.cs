using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Xunit;

namespace cuisineRestaurants
{
  public class RestaurantsTest : IDisposable
  {
    public RestaurantsTest()
    {
      DBConfiguration.ConnectionString = "Data Source = (localdb)\\mssqllocaldb;Initial Catalog=restaurantCuisine_test; Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Restaurants.GetAll().Count;
      Assert.Equal(0,result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfNameSame()
    {
        Restaurants firstRestaurants = new Restaurants("Carl's Jr.", "Canada", new DateTime(1999, 12, 15), "orange", 1);
        Restaurants secondRestaurants = new Restaurants("Carl's Jr.", "Canada", new DateTime(1999, 12, 15), "orange", 1);
        Assert.Equal(firstRestaurants,secondRestaurants);
    }
    public void Dispose()
   {
     Restaurants.DeleteAll();
   }
  }
}
