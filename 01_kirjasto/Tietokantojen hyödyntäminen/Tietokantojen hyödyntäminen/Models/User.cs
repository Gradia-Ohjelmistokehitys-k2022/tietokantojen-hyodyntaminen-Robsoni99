using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tietokantojen_hyödyntäminen.Models
{
    [Table("Member")]
    internal class User
    {
        [Key]
        [Column("BirthDate")]
        public int BirthDate { get; set; }
    }
}
