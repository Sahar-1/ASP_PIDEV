using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiDevEsprit.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public VoteType VoteType { get; set; }
    }
}