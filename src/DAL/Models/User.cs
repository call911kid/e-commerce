using System.Collections.Generic;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public User()
        {
            Orders = new HashSet<Order>();
        }
    }
}
