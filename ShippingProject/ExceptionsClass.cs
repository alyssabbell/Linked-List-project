
/*
* Name: Alyssa Bell
 * Filename: InventoryProject
 * Date: 5/1/2017
 * Purpose/Description: To catch duplicate inventory entries
 * Error Checking/Exceptions:
 * DuplicateKey() - catches any duplicate keys sent to the ListClass 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingProject
{
    class DuplicateKey : Exception
    {
        public DuplicateKey()
        {

        }

    }
}
