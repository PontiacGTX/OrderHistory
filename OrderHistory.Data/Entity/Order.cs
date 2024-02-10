using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHistory.Data.Entity
{
    [PrimaryKey("Id")]
    public class Order
    {

        [Key]
        public long Id { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        public long MemberId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public long ProductId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime DateOrder { get; set; }
    }
}
