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

      SqlCommand cmd = new SqlCommand("SELECT * FROM Cuisines;", conn);
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
      SqlParameter cuisinesIdParameter = new SqlParameter();
      cuisinesIdParameter.ParameterName = "@CuisinesId";
      cuisinesIdParameter.Value = id.ToString();
      cmd.Parameters.Add(cuisinesIdParameter);
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
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
