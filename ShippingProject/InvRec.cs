/*
 * Name: Alyssa Bell
 * Date: 5/1/2017
 * Filename: InventoryProject
 * Purpose/Description: Stores the data coming in from the Inventory.in file and holds it
 * for the master inventory list
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingProject
{
    public struct InvRec 
    {
        public string strPartNumber;        // format of key is ddd-ddd
        public string strDescription1;      // Level 1 description
        public string strDescription2;      // Level 2 description 
        public string strDescription3;      // Level 3 descrition
        public int intQtyOnHand;            // quantity on hand
        public int intQtyOnOrder;           // quantity on order 
        //end struct
    }
}
