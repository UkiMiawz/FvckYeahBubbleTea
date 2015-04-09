using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;

namespace FvckYeahBubbleTea.Models
{
    public class Order:BaseClass
    {
        public Size Size { get; set; }
        public BaseTea BaseTea { get; set; }
        public Flavor Flavor { get; set; }
        public List<Topping> Toppings { get; set; }

        public float TotalPrice
        {
            get { return Size.Price + Toppings.Sum(x => x.Price); }
            
        }
    }
}