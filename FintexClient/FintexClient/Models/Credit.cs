using System;
using System.Collections.Generic;

namespace FintexClient.Models
{
    public partial class Credit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Duration { get; set; }
        public int? Sum { get; set; }
        public int? PercentValue { get; set; }
        public DateTime? PeriodOfPayment { get; set; }
        public int? State { get; set; }
        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
