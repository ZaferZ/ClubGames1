﻿using System.ComponentModel.DataAnnotations;

namespace _8BallPool.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }
        public bool IsAdmin { get; set; }

    }
}
