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
      //Arrange, Act
      int result = Cuisines.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    public void Dispose()
    {
      // Restaurants.DeleteAll();
      Cuisines.DeleteAll();
    }
  }
}
