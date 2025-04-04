using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HotelManagement.DTO;

namespace HotelManagement.DAL
{
    public class ManageFloorDAL
    {
        private string connectionString = @"Data Source=LAPTOP-CGUI40EU\MAY1;Initial Catalog=HotelManagement;Integrated Security=True;Encrypt=False";

        public List<ManageFloorDTO> GetAllFloors()
        {
            List<ManageFloorDTO> list = new List<ManageFloorDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, max_rooms, description FROM [Floor]";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ManageFloorDTO floor = new ManageFloorDTO
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        MaxRooms = Convert.ToInt32(reader["max_rooms"]),
                        Description = reader["description"].ToString()
                    };
                    list.Add(floor);
                }
            }
            return list;
        }

        public bool AddFloor(ManageFloorDTO floor)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Floor (max_rooms, description) VALUES (@max_rooms, @description)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@max_rooms", floor.MaxRooms);
                cmd.Parameters.AddWithValue("@description", floor.Description);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateFloor(ManageFloorDTO floor)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Floor SET max_rooms = @max_rooms, description = @description WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@max_rooms", floor.MaxRooms);
                cmd.Parameters.AddWithValue("@description", floor.Description);
                cmd.Parameters.AddWithValue("@id", floor.Id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteFloor(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Floor WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
