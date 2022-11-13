using CRUDWinFormsMVP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDWinFormsMVP.Repositories
{
    public class PetRepository : BaseRepository, IPetRepository
    {

        public PetRepository(string connectionString)
            : base(connectionString)
        { }

        public void Add(PetModel petModel)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "insert into Pet values (@name, @type, @color)";
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = petModel.Name;
            command.Parameters.Add("@type", System.Data.SqlDbType.NVarChar).Value = petModel.Type;
            command.Parameters.Add("@color", System.Data.SqlDbType.NVarChar).Value = petModel.Color;
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = connection.CreateCommand();
            connection.Open();
            command.CommandText = "delete from Pet where Pet_Id=@id";
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            command.ExecuteNonQuery();
        }

        public void Edit(PetModel petModel)
        {
            using var connection = new SqlConnection(connectionString);
            using SqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText = @"update Pet 
                                        set Pet_Name=@name,Pet_Type= @type,Pet_Color= @color 
                                        where Pet_Id=@id";
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = petModel.Name;
            command.Parameters.Add("@type", System.Data.SqlDbType.NVarChar).Value = petModel.Type;
            command.Parameters.Add("@color", System.Data.SqlDbType.NVarChar).Value = petModel.Color;
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = petModel.Id;
            command.ExecuteNonQuery();
        }

        public IEnumerable<PetModel> GetAll()
        {
            var petList = new List<PetModel>();
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"SELECT * from Pet
                                    order by Pet_Id desc";
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var petModel = new PetModel
                    {
                        Id = (int)reader[0],
                        Name = reader[1].ToString(),
                        Type = reader[2].ToString(),
                        Color = reader[3].ToString(),
                    };
                    petList.Add(petModel);
                }
            }
            return petList;
        }

        public IEnumerable<PetModel> GetByValue(string value)
        {
            var petList = new List<PetModel>();
            if (!int.TryParse(value, out int petId))
            {
                petId = 0;
            }
            string petName = value;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT * from Pet
                                        where Pet_Id=@id or Pet_Name like @name+'%'
                                        order by Pet_Id desc";
                cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = petId;
                cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = petName;
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var petModel = new PetModel
                    {
                        Id = (int)reader[0],
                        Name = reader[1].ToString(),
                        Type = reader[2].ToString(),
                        Color = reader[3].ToString(),
                    };
                    petList.Add(petModel);
                }
            }
            return petList;
        }
    }
}
