using System;
using System.Collections.Generic;

namespace FintexClient.Models
{
    public partial class Client
    {
        public Client()
        {
            Credit = new HashSet<Credit>();
        }

        public int Id { get; set; }
        public string Fio { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Pass { get; set; }
        public long? Phone { get; set; }
        public int? Passport { get; set; }
        public string AccountNumber { get; set; }
        public int? IsBlocked { get; set; }

        public virtual ICollection<Credit> Credit { get; set; }
    }
}
