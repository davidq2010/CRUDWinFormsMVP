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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(PetModel petModel)
        {
            throw new NotImplementedException();
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
