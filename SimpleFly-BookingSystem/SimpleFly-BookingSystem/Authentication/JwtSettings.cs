﻿using System;

namespace SimpleFly_BookingSystem.Authentication
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromHours(1);
    }
}
