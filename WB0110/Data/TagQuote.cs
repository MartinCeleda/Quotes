using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WB0110.Data
{
    public class TagQuote
    {
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }

        [ForeignKey("Quote")]
        public int QuoteId { get; set; }
        public Quote Quote { get; set; }

    }
}
