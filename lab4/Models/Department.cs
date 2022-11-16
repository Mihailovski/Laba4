using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsReleasing { get; set; }
        public Guid FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public List<Specialization> Specializations { get; set; }
        public Department()
        { 

        }
    }
}
