using System;
using System.Collections.Generic;

namespace FintexClient.Models
{
    public partial class CreditOffer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Term { get; set; }
        public int? MaxSum { get; set; }
        public int? Procent { get; set; }
        public string Conditions { get; set; }
    }
}
