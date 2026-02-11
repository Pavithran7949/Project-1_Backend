using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolStudentAPI.Entities
{
    [Table("StudentMarksheet")]
    public class Students
    {
        [Key]
        public int Student_Id { get; set; }
        public string Student_Name { get; set; }
        public string Dept { get; set; }
        public int Maths { get; set; }
        public int Science { get; set; }
        public int English { get; set; }
        public int Social { get; set; }
        public int Tamil { get; set; }
        public int Total_Marks { get; set; }
    }
}
