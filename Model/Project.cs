


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Model
{
    public class Project
    {
        [Key]
        [Column("project_id")]
        public int ProjectId { get; set;}

        [MaxLength(100)]
        [Column("nama")]
        public required string Nama { get; set;}

        [Column("description")]
        public required string Description { get; set; }

        [DataType(DataType.Date)]
        [Column("start_date")]
        public DateTime? Start_Date { get; set; }
        
        [DataType(DataType.Date)]
        [Column("end_date")]
        public DateTime? End_Date { get; set; }

        [MaxLength(20)]
        [Column("status")]
        public string? Status{ get; set; }
    }

}