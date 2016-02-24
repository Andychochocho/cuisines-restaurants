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
    public void Cuisines_EmptyAtFirst()
    {
      int result = Cuisines.GetAll().Count;
      Assert.Equal(0, result);
    }

    [Fact]
    public void Cuisines_EqualToEachOther()
    {
      Cuisines firstCuisines = new Cuisines("Chinese");
      Cuisines secondCuisines = new Cuisines("Chinese");
      Assert.Equal(firstCuisines, secondCuisines);
    }

    public void Dispose()
    {
      // Restaurants.DeleteAll();
      Cuisines.DeleteAll();
    }
  }
}
