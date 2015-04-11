using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;

namespace FvckYeahBubbleTea.Models
{
    public class Order:BaseClass
    {
        public int SizeId { get; set; }
        public virtual TeaSize Size { get; set; }

        public int BaseTeaId { get; set; }
        public virtual BaseTea BaseTea { get; set; }

        public int FlavorId { get; set; }
        public virtual Flavor Flavor { get; set; }

        public virtual ICollection<Topping> Toppings { get; set; }
    }
}