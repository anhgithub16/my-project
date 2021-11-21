using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security
{
    public static class ExtentionMethod
    {
        public static string GetUserId(this ControllerBase controller)
        {
            string userId = controller.User.Identity.Name;
            return userId;
        }
    }
}
