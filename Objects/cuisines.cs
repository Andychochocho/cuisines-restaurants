using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace cuisineRestaurants
{
  public class Cuisines
  {
      private string _name;
      private int _id;

    public Cuisines(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public override bool Equals(System.Object otherCuisines)
    {
      if(!(otherCuisines is Cuisines))
      {
        return false;
      }
      else
      {
        Cuisines newCuisines = (Cuisines) otherCuisines;
        bool idEquality = this.GetId() == newCuisines.GetId();
        bool nameEquality = this.GetName() == newCuisines.GetName();
        return(idEquality && nameEquality);
      }
    }

    public void Update (string newName)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE cuisines SET name=@NewName OUTPUT INSERTED.name WHERE id= @CuisinesId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);

      SqlParameter CuisinesIdParameter = new SqlParameter ();
      CuisinesIdParameter.ParameterName = "@CuisinesId";
      CuisinesIdParameter.Value= this.GetId();
      cmd.Parameters.Add(CuisinesIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
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
    // Getters and Setters
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void Setname(string newName)
    {
      _name = newName;
    }

    public static List<Cuisines> GetAll()
    {
      List<Cuisines> allCuisines = new List<Cuisines> {};
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int cuisinesId = rdr.GetInt32(0);
        string cuisinesName = rdr.GetString(1);
        Cuisines newCuisines = new Cuisines(cuisinesName, cuisinesId);
        allCuisines.Add(newCuisines);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCuisines;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cuisines (name) OUTPUT INSERTED.id VALUES (@cuisinesnames);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@cuisinesnames";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Cuisines Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines WHERE id = @CuisinesId;", conn);
      SqlParameter CuisinesIdParameter = new SqlParameter();
      CuisinesIdParameter.ParameterName = "@CuisinesId";
      CuisinesIdParameter.Value = id.ToString();
      cmd.Parameters.Add(CuisinesIdParameter);
      rdr = cmd.ExecuteReader();

      int foundCuisinesId = 0;
      string foundCuisinesName = null;

      while(rdr.Read())
      {
        foundCuisinesId = rdr.GetInt32(0);
        foundCuisinesName = rdr.GetString(1);
      }
      Cuisines foundCuisines = new Cuisines(foundCuisinesName, foundCuisinesId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundCuisines;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines", conn);
      cmd.ExecuteNonQuery();
    }

    public List<Restaurants> GetRestaurants()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE cuisine_Id = @CuisinesId ORDER BY name DESC;", conn);
      SqlParameter CuisinesIdParameter = new SqlParameter ();
      CuisinesIdParameter.ParameterName = "@CuisinesId";
      CuisinesIdParameter.Value = this.GetId();
      cmd.Parameters.Add(CuisinesIdParameter);
      rdr = cmd.ExecuteReader();

      List<Restaurants> restaurants = new List<Restaurants> {};
      while(rdr.Read())
      {
        int RestaurantsId = rdr.GetInt32(0);
        string RestaurantsName = rdr.GetString(1);
        string RestaurantsLocation = rdr.GetString(2);
        string RestaurantsColor = rdr.GetString(4);
        int cuisineId = rdr.GetInt32(5);
        DateTime RestaurantsDate = rdr.GetDateTime(3);
        Restaurants newRestaurants = new Restaurants(RestaurantsName, RestaurantsLocation, RestaurantsDate, RestaurantsColor, cuisineId, RestaurantsId);
        restaurants.Add(newRestaurants);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return restaurants;
    }
  }
}
