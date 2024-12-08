﻿namespace ApiService.Models
{
    public class RegisterRequest
    {
        public int Id { get; set;}
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string? Role { get; set; } = null;
    }
}
