using System;

namespace ProjectsAPI.DTOs
{
    public class AccountToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}