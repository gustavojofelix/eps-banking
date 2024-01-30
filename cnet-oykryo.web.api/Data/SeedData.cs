using Microsoft.AspNetCore.Identity;

namespace cnet_oykryo.web.api.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>())
            {
                // Check if there are no users in the database
                if (await userManager.FindByEmailAsync("admin@example.com") == null)
                {
                    // Create a new user
                    var adminUser = new AppUser
                    {
                        UserName = "admin@example.com",
                        Email = "admin@example.com",
                        // Other properties
                    };

                    // Add the user to the database
                    await userManager.CreateAsync(adminUser, "Password");

                    // Optionally, add roles and other user-related data here
                }
            }
        }
    }

}
