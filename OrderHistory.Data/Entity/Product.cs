using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Data.Entity
{
    [PrimaryKey("Id")]
    public class Product
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
