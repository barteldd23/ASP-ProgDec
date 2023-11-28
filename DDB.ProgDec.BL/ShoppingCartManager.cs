using DDB.ProgDec.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDB.ProgDec.BL
{
    public static class ShoppingCartManager
    {
        public static void Add(ShoppingCart cart, Declaration declaration)
        {
            if(cart != null) { cart.Items.Add(declaration); }
        }

        public static void Remove(ShoppingCart cart, Declaration declaration)
        {
            if (cart != null) { cart.Items.Remove(declaration); }
        }

        public static void Checkout (ShoppingCart cart)
        {
            // Make a new order
            // Set the Order fields as needed.
            
            // foreach(Movie item in cart.Items)
            //
            // Make a new orderItem
            // Set the OrderItem fields from the item
            // order.OrderItems.Add(orderItem)
            
            // OrderManager.Insert(order)

            // Decrement the tblMovie.InStkQty appropriately.

            cart = new ShoppingCart();
        }
    }
}
