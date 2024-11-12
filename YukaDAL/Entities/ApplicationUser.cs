using Microsoft.AspNetCore.Identity;

namespace YukaDAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreationDate { get; set; }
        
        public ApplicationUser () => CreationDate = DateTime.UtcNow;
    }
}
