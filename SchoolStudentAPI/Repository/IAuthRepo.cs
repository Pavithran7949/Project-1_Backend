using SchoolStudentAPI.Entities;

namespace SchoolStudentAPI.Repository
{
    public interface IAuthRepo
    {
        Users ValidateUser(string username, string password);
    }
}
