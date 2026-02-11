using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolStudentAPI.Dto;
using SchoolStudentAPI.Entities;
using SchoolStudentAPI.Service;

namespace SchoolStudentAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentMarksheetController : Controller
    {
        private readonly ISchoolMarksheetService _schoolmarksheetservice;
        private readonly IActivityLogService _logService;

        public StudentMarksheetController(ISchoolMarksheetService schoolmarksheetservice, IActivityLogService logService)
        {
            _schoolmarksheetservice = schoolmarksheetservice;
            _logService = logService;
        }

        [Authorize(Roles = "Admin,Staff")]
        //[Authorize("AllowAnonymous")]
        [HttpGet("api/GetallStudents")]
        public IActionResult GetAllStudents()
        {
            List<ViewStudent> students = _schoolmarksheetservice.GetAllStudents();

            return Ok(students);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("api/GetRemovedStudents")]
        public IActionResult GetRemovedStudents()
        {
            List<ViewStudent> students = _schoolmarksheetservice.GetRemovedStudents();

            return Ok(students);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("api/AddStudent")]
        public IActionResult AddStudent(AddStudentDTO dto)
        {
            int result = _schoolmarksheetservice.AddStudent(dto);

            if (result != 0)
            {
                _logService.AddLog($"Student {result} has been created");

                return Ok(new { message = "Student added successfully", Student_Id = result });
            }
            else
            {
                return StatusCode(500, "Failed to add student");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("api/UpdateStudentName/{id}")]
        public IActionResult UpdateStudentName(int id, UpdateStudentName dto)
        {
            int result = _schoolmarksheetservice.UpdateStudentName(id, dto);
            if (result != 0)
            {
                _logService.AddLog($"Student {id} has been updated");

                return Ok(result);
            }
            else
            {
                return NotFound("Failed to update student name");
            }
        }

        [Authorize(Roles = "Staff")]
        [HttpPut("api/UpdateMarksheet/{id}")]
        public IActionResult UpdateMarksheet(int id, UpdateMarksheetDTO dto)
        {

            try
            {
                var result = _schoolmarksheetservice.UpdateMarksheet(id, dto);
                int resultValue = result.result;
                int totalmarks = result.total;

                if (resultValue == 0)
                {
                    return NotFound("Failed to update marksheet");
                }
                else
                {
                    _logService.AddLog($"Student {id} marksheet updated");
                    return Ok(new { message = "Marksheet updated successfully", Student_ID = id, Totalmarks = totalmarks });
                    

                }
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the marksheet", ex);
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [Authorize(Roles = "Student")]
        [HttpGet("api/GetallMarksheet")]
        public IActionResult GetAllMarksheet()
        {
            List<Students> marksheet = _schoolmarksheetservice.GetAllMarksheet();
            return Ok(marksheet);
        }

        [Authorize(Roles = "Student")]
        [HttpGet("api/GetMarksheetById/{id}")]
        public IActionResult GetMarksheetById(int id)
        {
            Students marksheet = _schoolmarksheetservice.GetMarksheetById(id);
            if (marksheet == null)
            {
                return NotFound($"Marksheet for Student_Id {id} is not found");
            }
            return Ok(marksheet);

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("api/DeleteStudent/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            int result = _schoolmarksheetservice.DeleteStudent(id);
            if (result != 0)
            {
                _logService.AddLog($"Student {id} has been deleted");

                return Ok(result);
            }
            else
            {
                return NotFound("Failed to delete student");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("api/RestoreStudent/{id}")]
        public IActionResult RestoreStudent(int id)
        {
            int result = _schoolmarksheetservice.RestoreStudent(id);
            if (result != 0)
            {
                _logService.AddLog($"Student {id} has been restored");

                return Ok(result);
            }
            else
            {
                return NotFound("Failed to Restore student");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("api/GetAllDepartments")]
        public IActionResult GetAllDepartments()
        {
            List<Department> departments = _schoolmarksheetservice.GetAllDepartments();
            return Ok(departments);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("api/DeleteDepartment/{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            int result = _schoolmarksheetservice.DeleteDepartment(id);
            if (result != 0)
            {
                _logService.AddLog($"Department {id} has been deleted");

                return Ok(result);
            }
            else
            {
                return NotFound("Failed to Delete Department");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("api/AddDepartment")]
        public IActionResult AddDepartment(AddDepartment dto)
        {
            int result = _schoolmarksheetservice.AddDepartment(dto);
            if (result != 0)
            {
                _logService.AddLog($"Department {dto.Dept_Name} has been created");

                return Ok(result);
            }
            else
            {
                return StatusCode(500, "Failed to add Department");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("api/GetStudentById/{id}")]
        public IActionResult GetStudentById(int id)
        {
            FullStudentDetails result = _schoolmarksheetservice.GetStudentById(id);

            if(result == null)
            {
                return NotFound($"Student {id} is not found");
            }
            else
            {
                return Ok(result);
            }

        }

        [Authorize(Roles = "Admin,Staff")]
        [HttpGet("api/GetLogs")]
        public IActionResult GetLogs()
        {
            var logs = _logService.GetLogs();
            return Ok(logs);
        }
    }
}
