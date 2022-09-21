﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
