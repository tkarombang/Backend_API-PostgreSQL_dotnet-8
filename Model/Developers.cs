using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace MyApp.Model
{
    [Table("developers")]
    public class Developer
    {
        [Key] // Menentukan developer_id sebagai primary key
        [Column("developer_id")]
        public int DeveloperId { get; set; }

    
        [MaxLength(100)] 
        [Column("nama")] // Nama Kolom Di Database
        public required string Nama { get; set; }


        [EmailAddress] // Validasi format email
        [Column("email")]
        public required string Email { get; set; }

        [MaxLength(50)] // Maksimal 50 karakter
        [Column("role")]
        public string? Role { get; set; } 

        [MaxLength(15)]
        [Phone] // Validasi format nomor hp
        [Column("phone")]
        public string? Phone { get; set; }

        [DataType(DataType.Date)] // Format Tanggal
        [Column("tanggal_lahir")]
        public DateTime? TanggalLahir { get; set; }

        [MaxLength(20)]
        [Column("status")]
        public string? Status { get; set; }

        [Column("jenis_kelamin")]
        public JenisKelamin? Gender { get; set; }
        public string? GenderLabel => Gender.HasValue ? Gender.Value.ToString() : null;

        // public ICollection<TaskItem>? TaskItem { get; set; }
        // public ICollection<ProjectDeveloper>? ProjectDevelopers { get; set; }
    }

    // Enum untuk jenis kelamin
    public enum JenisKelamin{
        Pria = 1,
        Wanita = 2
    }
}


//  developer_id integer NOT NULL DEFAULT nextval('developer_developer_id_seq'::regclass),
//     nama character varying(100) COLLATE pg_catalog."default" NOT NULL,
//     email character varying(100) COLLATE pg_catalog."default" NOT NULL,
//     role character varying(50) COLLATE pg_catalog."default",
//     phone character varying COLLATE pg_catalog."default",
//     tanggal_lahir date,
//     status character varying(10) COLLATE pg_catalog."default",
//     jenis_kelamin integer,
