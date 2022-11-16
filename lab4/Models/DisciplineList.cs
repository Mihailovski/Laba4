using Laba4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4.Models
{
    public class DisciplineList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid DisciplineId { get; set; }
        public Guid TeacherId { get; set; }
        public Discipline Discipline { get; set; }
        public Teacher Teacher { get; set; }
        public DisciplineList()
        { 

        }
    }
}
