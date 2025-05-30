using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Model
{
    public class ProjectDeveloper
    {
        [Column(Order = 0)] // Menentukan project_id sebagai primary key 
        public int ProjectId { get; set; }

        [Column(Order = 1)] // Menentukan developer_id sebagai primary key
        public int DeveloperId { get; set; }

        //NAVIGATION PROPERTION
        public Project? Project { get; set; }
        public Developer? Dev { get; set; }
    }
}
