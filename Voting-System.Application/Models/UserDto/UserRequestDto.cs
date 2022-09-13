﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting_System.Application.Models.UserDto
{
    public record UserRequestDto
    {
        public string? FullName { get; set; }
        public string? IdentityCardId { get; set; }
        public uint IdentityCardNumber { get; set; }
        public string? IdentityCardSeries { get; set; }
        public DateTime? IdentityCardEmitedDate { get; set; }
        public string? Mail { get; set; }
        public string? Password { get; set; }
    }
}
