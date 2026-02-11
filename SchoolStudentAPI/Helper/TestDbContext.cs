using Microsoft.Data.SqlClient;

namespace SchoolStudentAPI.Helper
{
    public class TestDbContext
    {
        private readonly IConfiguration _configuration;

        public TestDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")
            );
        }
    }
}
