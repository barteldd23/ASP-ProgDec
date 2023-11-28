using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.ProgDec.BL.Models
{
    public class ShoppingCart
    {
        // Declaration application specific - Declaration Cost
        const double ITEM_COST = 120.03;

        public List<Declaration> Items {  get; set; } = new List<Declaration>();
        public int NumberOfItems { get {  return Items.Count; } }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double SubTotal { get { return Items.Count *  ITEM_COST; } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Tax { get { return SubTotal * .055; } }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Total { get { return SubTotal + Tax; } }
    }
}
