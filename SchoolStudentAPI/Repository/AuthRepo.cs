using Dapper;
using Microsoft.Data.SqlClient;
using SchoolStudentAPI.Entities;
using SchoolStudentAPI.Helper;

namespace SchoolStudentAPI.Repository
{
    public class AuthRepo : IAuthRepo
    {
        private readonly TestDbContext _dbcontext;

        public AuthRepo(TestDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Users ValidateUser(string username, string password)
        {
            using SqlConnection con = _dbcontext.GetConnection();

            string query = @"SELECT * FROM Users 
                             WHERE Username = @Username AND Password = @Password";

            return con.QueryFirstOrDefault<Users>(query, new { Username = username, Password = password });
        }
    }
}
