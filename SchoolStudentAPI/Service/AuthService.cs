using SchoolStudentAPI.Dto;
using SchoolStudentAPI.Repository;

namespace SchoolStudentAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _repo;
        private readonly IJwtService _jwt;

        public AuthService(IAuthRepo repo, IJwtService jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }

        public object Login(LoginDTO dto)
        {
            var user = _repo.ValidateUser(dto.Username, dto.Password);

            if (user == null)
                return null;

            var token = _jwt.GenerateToken(user);

            return new
            {
                message = "Login Success",
                token = token,
                role = user.Role,
                username = user.Username
            };
        }
    }
}
