using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Models
{
    [Table("user")]
    public class User
    {
        [Column("id_user")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("password_hash")]
        public string PasswordHash { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
