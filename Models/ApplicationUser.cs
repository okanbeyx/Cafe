﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Models
{
	public class ApplicationUser:IdentityUser
	{
		[Required]
        public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[NotMapped]
        public string Role { get; set; }
    }
}
