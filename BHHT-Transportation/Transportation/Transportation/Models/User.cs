using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Transportation.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
       
        [Column(TypeName = "nvarchar(255)")]
        public string? FirstName { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? LastName { get; set; }
        
        [Column(TypeName = "nvarchar(255)")]
        public string? Contact { get; set; }

        [Column(TypeName = "nvarchar(355)")]
        public string? Email { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? Password { get; set; }
        public int? Role { get; set; }  // superAdmin=0,admin =1,driver =2,receiver=3,attendent=4,shiper=5
        public int? IsActive { get; set; }
        public int? CreatedBy { get; set; }   // super admin/admin
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
