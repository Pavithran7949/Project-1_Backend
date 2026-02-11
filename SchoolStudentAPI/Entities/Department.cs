using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolStudentAPI.Entities
{
    [Table("Department_Details")]
    public class Department
    {
        [Key]
        public int Dept_Id { get; set; }
        public string Dept_Code { get; set; }
        public string Dept_Name { get; set; }
    }
}
