using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace cuisineRestaurants
{
  public class Restaurants
   {
     private int _id;
     private int _cuisine_Id;
     private string _name;
     private string _location;
     private DateTime _openingDate;
     private string _color;

     public Restaurants (string name, string location, DateTime openingDate, string color, int cuisine_Id, int Id =0)
     {
       _id = Id;
       _cuisine_Id = cuisine_Id;
       _name = name;
       _location = location;
       _openingDate = openingDate;
       _color = color;
     }

     public override bool Equals(System.Object otherRestaurants)
    {
      if (!(otherRestaurants is Restaurants)) {
        return false;
      }
      else
      {
        Restaurants newRestaurants = (Restaurants) otherRestaurants;
        bool idEquality = this.GetId() == newRestaurants.GetId();
        bool idCuisineEquality= this.GetCuisineId() == newRestaurants.GetCuisineId();
        bool nameEquality = this.GetName() == newRestaurants.GetName();
        bool locationEquality = this.GetLocation() == newRestaurants.GetLocation();
        bool dateEquality = this.GetOpeningDate() == newRestaurants.GetOpeningDate();
        bool colorEquality = this.GetColor() == newRestaurants.GetColor();
        return (idEquality && nameEquality && locationEquality && dateEquality && colorEquality);
      }
    }
      public int GetId()
     {
       return _id;
     }
     public string GetName()
     {
       return _name;
     }
     public void SetName(string newName)
     {
       _name = newName;
     }

     public string GetLocation()
     {
       return _location;
     }

     public void SetLocation(string newLocation)
     {
       _location = newLocation;
     }

     public DateTime GetOpeningDate()
     {
       return _openingDate;
     }

     public void SetOpeningDate(DateTime newDate)
     {
       _openingDate = newDate;
     }
     public string GetColor()
     {
       return _color;
     }

     public void SetColor(string newColor)
     {
       _color = newColor;
     }
     public int GetCuisineId()
     {
       return _cuisine_Id;
     }

     public void SetCuisineId(int newCuisineId )
     {
       _cuisine_Id = newCuisineId;
     }


     public static List<Restaurants> GetAll()
    {
      List<Restaurants> allRestaurants = new List<Restaurants>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("Select * FROM  restaurants ORDER BY name DESC;",conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int RestaurantsId = rdr.GetInt32(0);
        string RestaurantsName = rdr.GetString(1);
        string RestaurantsLocation = rdr.GetString(2);
        DateTime RestaurantsDate = rdr.GetDateTime(3);
        string RestaurantsColor = rdr.GetString(4);
        int RestaurantsCuisineId = rdr.GetInt32(5);

        Restaurants newRestaurants = new Restaurants(RestaurantsName, RestaurantsLocation, RestaurantsDate, RestaurantsColor, RestaurantsId, RestaurantsCuisineId);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allRestaurants;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("Insert INTO restaurants (name, location, opening_date, color, cuisine_id) OUTPUT INSERTED.id VALUES (@RName, @RLocation, @RODate, @RColor, @CId);",conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@RName";
      nameParameter.Value = this.GetName();

      SqlParameter locationParameter = new SqlParameter();
      locationParameter.ParameterName = "@RLocation";
      locationParameter.Value = this.GetLocation();

      SqlParameter dateParameter = new SqlParameter();
      dateParameter.ParameterName = "@RODate";
      dateParameter.Value = this.GetOpeningDate();

      SqlParameter colorParameter = new SqlParameter();
      colorParameter.ParameterName = "@RColor";
      colorParameter.Value = this.GetColor();

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@cId";
      cuisineIdParameter.Value = this.GetCuisineId();

      cmd.Parameters.Add(colorParameter);
      cmd.Parameters.Add(locationParameter);
      cmd.Parameters.Add(dateParameter);
      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(cuisineIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }
    public static Restaurants Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id= @RestaurantsId;", conn);
      SqlParameter RestaurantsIdParameter = new SqlParameter();
      RestaurantsIdParameter.ParameterName = "@RestaurantsId";
      RestaurantsIdParameter.Value = id.ToString();
      cmd.Parameters.Add(RestaurantsIdParameter);
      rdr = cmd.ExecuteReader();

      int foundRestaurantsId = 0;
      string foundRestaurantsName = null;
      string foundRestaurantsLocation = null;
      DateTime foundRestaurantsDate = new DateTime (2016-02-23);
      string foundRestaurantsColor = null;
      int foundCuisinesId = 0;

      while(rdr.Read())
      {
        foundRestaurantsId = rdr.GetInt32(0);
        foundRestaurantsName = rdr.GetString(1);
        foundRestaurantsLocation = rdr.GetString(2);
        foundRestaurantsColor = rdr.GetString(3);
        foundRestaurantsDate = rdr.GetDateTime(4);
        foundCuisinesId = rdr.GetInt32(5);
      }
      Restaurants foundRestaurants = new Restaurants(foundRestaurantsName, foundRestaurantsLocation, foundRestaurantsDate, foundRestaurantsColor, foundRestaurantsId, foundCuisinesId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundRestaurants;
    }
  }
}
