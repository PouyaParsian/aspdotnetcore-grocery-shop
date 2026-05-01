using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsFinaly { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
