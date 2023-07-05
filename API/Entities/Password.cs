﻿using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Password
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HashedPassword { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }

}

// implement a method to make the date be updated when user update password
