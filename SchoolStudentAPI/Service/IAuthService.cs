using SchoolStudentAPI.Dto;

namespace SchoolStudentAPI.Service
{
    public interface IAuthService
    {
        object Login(LoginDTO dto);
    }
}
