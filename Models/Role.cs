using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBudget.Models
{
    public enum Roles
    {
        Admin,
        User
    }

    public class Role
    {
        public int Id { get; set; }
        public String RoleName { get; set; }
        // one role many users
        public virtual ICollection<User> Users { get; set; }
    }
}
