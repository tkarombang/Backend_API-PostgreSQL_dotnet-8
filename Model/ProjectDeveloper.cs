using System.ComponentModel.DataAnnotations;

namespace MyApp.Model
{
    public class ProjectDeveloper
    {
        [Key] // Menentukan developer_id sebagai primary key
        public int developer_id { get; set; }

        public string project_id { get; set; }
    }
}
