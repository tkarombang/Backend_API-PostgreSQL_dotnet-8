using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace MyApp.Model
{
    public class Developer
    {
        [Key] // Menentukan developer_id sebagai primary key
        public int developer_id { get; set; }

        public string nama { get; set; }
        public string email { get; set; }
        public string? role { get; set; } 
        public string? phone { get; set; }
        public DateTime? tanggal_lahir { get; set; }
        public string? status { get; set; }
        public int? jenis_kelamin { get; set; }
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
