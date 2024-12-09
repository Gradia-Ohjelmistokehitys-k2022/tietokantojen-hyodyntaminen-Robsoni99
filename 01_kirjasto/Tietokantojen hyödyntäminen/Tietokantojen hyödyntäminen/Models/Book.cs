using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tietokantojen_hyödyntäminen.Models
{
    [Table("Book")]
    internal class Book
    {
        [Key]
        [Column("Book")]
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int AvailableCopies { get; set; }

    }
}
