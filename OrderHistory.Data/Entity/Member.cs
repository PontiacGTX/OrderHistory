using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderHistory.Data.Entity
{

    [PrimaryKey( "Id")]
    public class Member
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}


