using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;

namespace FvckYeahBubbleTea.Models
{
    public class FinalOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<Order> Toppings { get; set; }
        public float TotalPrice
        {
            get { return Toppings.Sum(x => x.TotalPrice); }
        }
    }
}