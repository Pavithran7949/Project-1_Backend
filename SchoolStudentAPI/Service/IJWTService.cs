using SchoolStudentAPI.Entities;

namespace SchoolStudentAPI.Service
{
    public interface IJwtService
    {
        string GenerateToken(Users user);
    }
}
