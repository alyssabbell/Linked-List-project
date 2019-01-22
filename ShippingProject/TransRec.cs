/*
 * Name: Alyssa Bell
 * Date: 5/1/2017
 * Filename: InventoryProject
 * Purpose/Description: Stores the data coming in from the Transactions.in file. The data updates the 
 * master inventory list.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingProject
{
    public struct TransRec
    {
        public string strPartNumber;        // format of key is ddd-ddd
        public char chrTransType;           // transaction type - S, P, or R
        public int intTransQty;             // transaction quantity
        // end struct
    }
}
