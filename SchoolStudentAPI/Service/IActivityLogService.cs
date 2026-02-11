using SchoolStudentAPI.Entities;

namespace SchoolStudentAPI.Service
{
    public interface IActivityLogService
    {
        void AddLog(string message);
        List<ActivityLog> GetLogs();
    }
}
