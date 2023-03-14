using Microsoft.AspNetCore.Identity;

namespace Payscrow.Admin.Data
{
    // Add profile data for application users by adding properties to the AdminUser class
    public class AdminUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}