﻿
namespace CleanArchitecture.Application.Models.Identity
{
    public class RegistrationsResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
