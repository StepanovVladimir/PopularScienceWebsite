using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Models
{
    [Table("user_role")]
    public class UserRole
    {
        [Column("id_user")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("id_role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
