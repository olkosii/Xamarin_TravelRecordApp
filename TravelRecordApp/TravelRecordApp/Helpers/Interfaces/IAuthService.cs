﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TravelRecordApp.Helpers.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterUser(string email, string password);
        Task<bool> LoginUser(string email, string password);
        bool IsAuthenticated();
        string GetCurrentUserId();
    }
}
