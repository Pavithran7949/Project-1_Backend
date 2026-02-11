using Dapper;
using SchoolStudentAPI.Entities;
using SchoolStudentAPI.Service;
using System.Data;

public class ActivityLogService : IActivityLogService
{
    private readonly IDbConnection _db;

    public ActivityLogService(IDbConnection db)
    {
        _db = db;
    }

    public void AddLog(string message)
    {
        string sql = "INSERT INTO ActivityLogs (Message) VALUES (@msg)";

        _db.Execute(sql, new { msg = message });
    }

    public List<ActivityLog> GetLogs()
    {
        string sql = "SELECT TOP 50 * FROM ActivityLogs ORDER BY CreatedAt DESC";

        return _db.Query<ActivityLog>(sql).ToList();
    }
}
