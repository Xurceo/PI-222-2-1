﻿using DAL.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Time { get; set; }

        public int LotId { get; set; }

        [ForeignKey("LotId")]
        public required Lot Lot { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
