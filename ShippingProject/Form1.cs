/*
 * Name: Alyssa Bell
 * Date: 5/1/2017
 * Filename: InventoryProject
 * Purpose/Description: This form application takes in inventory files from the user, and updates the master 
 * inventory list based on the quantity on hand and the quanity on order.
 * After the inventory list has been updated, the program outputs reports that contain the quanities of each
 * type of transaction, and also outputs a report of the items that have not been ordered/sold.
 * Error Checking: The program performs error checking on the input files - if the files are not found, 
 * the user will see an error message. It also, alerts the user if any duplicate values have been deleted. 
 * Assumptions: The list input lists will not change in format/order.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ShippingProject
{
    public partial class Form1 : Form
    {
        const int INITIAL_SIZE = 4;
        // declare reference for struct
        InvRec invRecStruct = new InvRec();
        // declare array to store structs in
        InvRec[] invRecArray = new InvRec[INITIAL_SIZE];
        // declare reference to ListClass
        ListClass accessClass = new ListClass();
        // declare reference for struct
        TransRec transRecStruct = new TransRec();
        TransRec[] transactionsIn = new TransRec[INITIAL_SIZE];

        static int localCurrPos = 0;
        // declaring new oject for the array after it has been expanded
        InvRec[] resizedArray = new InvRec[localCurrPos];
        public Form1()
        {
            InitializeComponent();
        }


        private void FillInvBtn_Click(object sender, EventArgs e)
        {
            invRecArray = new InvRec[INITIAL_SIZE];
            StreamReader din;
            string filename = "Inventory.in";
            din = new StreamReader(filename);

            if (!File.Exists(filename))
            {
                MessageBox.Show("The input file Inventory.in does not exist. The program will terminate.");
                this.Close();
            }

            MessageBox.Show("Your inventory list has been filled");

            // determines the line of inputs inside the Inventory.in file
            string readToCount = "";
            int lineCount = 0;          // this is the array length!
            readToCount = din.ReadLine();
            while (readToCount != null)
            {
                lineCount++;
                readToCount = din.ReadLine();
            }


            invRecArray = new InvRec[lineCount];

            StreamReader din2;
            din2 = new StreamReader(filename);

            string lineRead = "";
            // this reads the first line of Inventory.in
            lineRead = din2.ReadLine();
            // following code will insert the values from the first line into invRecArray[0]
            string[] firstLnRdArray = lineRead.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            invRecStruct.strPartNumber = firstLnRdArray[0];
            invRecStruct.strDescription1 = firstLnRdArray[1];
            invRecStruct.strDescription2 = firstLnRdArray[2];
            invRecStruct.strDescription3 = firstLnRdArray[3];
            string firstQtyOnHand = firstLnRdArray[4];
            int qtyOnHandIntConv = Convert.ToInt32(firstQtyOnHand);
            invRecStruct.intQtyOnHand = qtyOnHandIntConv;
            string firstQtyOnOrder = firstLnRdArray[5];
            int qtyOnOrderIntConv = Convert.ToInt32(firstQtyOnOrder);
            invRecStruct.intQtyOnOrder = qtyOnOrderIntConv;


            invRecArray[0].strPartNumber = invRecStruct.strPartNumber;
            invRecArray[0].strDescription1 = invRecStruct.strDescription1;
            invRecArray[0].strDescription2 = invRecStruct.strDescription2;
            invRecArray[0].strDescription3 = invRecStruct.strDescription3;
            invRecArray[0].intQtyOnHand = invRecStruct.intQtyOnHand;
            invRecArray[0].intQtyOnOrder = invRecStruct.intQtyOnOrder;


            //  DO SPLIT METHOD ON THE STRING strPartNumber ONCE EVERYTHING HAS BEEN READ INTO STRUCT AND STORED IN ARRAY
            int i = 1;
            string importLineRead = "";
            importLineRead = din2.ReadLine();
            while (importLineRead != null)
            {

                string[] importLnRdArray = importLineRead.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                invRecStruct.strPartNumber = importLnRdArray[0];
                invRecStruct.strDescription1 = importLnRdArray[1];
                invRecStruct.strDescription2 = importLnRdArray[2];
                invRecStruct.strDescription3 = importLnRdArray[3];
                string localQtyOnHand = importLnRdArray[4];
                invRecStruct.intQtyOnHand = Convert.ToInt32(localQtyOnHand);
                string localQtyOnOrder = importLnRdArray[5];
                invRecStruct.intQtyOnOrder = Convert.ToInt32(localQtyOnOrder);


                invRecArray[i].strPartNumber = invRecStruct.strPartNumber;
                invRecArray[i].strDescription1 = invRecStruct.strDescription1;
                invRecArray[i].strDescription2 = invRecStruct.strDescription2;
                invRecArray[i].strDescription3 = invRecStruct.strDescription3;
                invRecArray[i].intQtyOnHand = invRecStruct.intQtyOnHand;
                invRecArray[i].intQtyOnOrder = invRecStruct.intQtyOnOrder;


                i++;
                importLineRead = din2.ReadLine();

            } // * * * * * * * * * * * * END OF READING IN INVENTORY FILE * * * * * * * * * 
            din.Close();
            din2.Close();


            // Call function to remove hyphen before doing other processes like sorting
            RemoveHyphen(/* IN & OUT */invRecArray);

            accessClass.SetInClassArray(/* OUT */invRecArray);
            accessClass.NeedToExpand(/* IN & OUT */invRecArray);

            // NOW USE RESIZED ARRAY TO INSERT
            resizedArray = accessClass.GetIncomingArray();


            procTransBtn.Enabled = true;
            fillInvBtn.Enabled = false;

        }


        // * * * * * * * * * * * * * * START OF PROCESSINGING TRANSACTIONS * * * * * * * * * * * * * * * * 

        private void ProcTransBtn_Click(object sender, EventArgs e)
        {

            transRecStruct = new TransRec();
            StreamReader din1;
            string filename = "Transactions.in";
            din1 = new StreamReader(filename);

            if (!File.Exists(filename))
            {
                MessageBox.Show("The input file Transactions.in does not exist. The program will terminate.");
                this.Close();
            }

            else
                MessageBox.Show("The Transactions are being read into the program.");

            // This chunk determines how long to make the array if storying these values into their own away
            string readToCount = "";
            int lineCount = 0;
            readToCount = din1.ReadLine();
            while (readToCount != null)
            {
                lineCount++;
                readToCount = din1.ReadLine();
            }

            // array will store the Transactions.In file
            transactionsIn = new TransRec[lineCount];


            // Read in first line of Transactions.In
            StreamReader din3;
            din3 = new StreamReader(filename);

            string transLineRead = "";
            transLineRead = din3.ReadLine();


            string[] firstLnRdArray = transLineRead.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            transRecStruct.strPartNumber = firstLnRdArray[0];
            // this char conversion is currently showing the ascii int val in the struct..
            transRecStruct.chrTransType = Convert.ToChar(firstLnRdArray[1]);
            transRecStruct.intTransQty = Convert.ToInt32(firstLnRdArray[2]);

            transactionsIn[0].strPartNumber = transRecStruct.strPartNumber;
            transactionsIn[0].chrTransType = transRecStruct.chrTransType;
            transactionsIn[0].intTransQty = transRecStruct.intTransQty;

            //MessageBox.Show("The first line of the file is" + transRecStruct.strPartNumber + transRecStruct.chrTransType + transRecStruct.intTransQty);

            // Split string with hyphen and store into struct
            string forTransSplit = transRecStruct.strPartNumber;
            string[] remHyphen1 = forTransSplit.Split('-');
            string firstHalfTransPartNum = remHyphen1[0];
            string secHalfTransPartNum = remHyphen1[1];
            string localTransPartNum = firstHalfTransPartNum + secHalfTransPartNum;
            transRecStruct.strPartNumber = localTransPartNum;


            if (accessClass.FindElement(/* IN */resizedArray, /* IN */Convert.ToInt32(localTransPartNum)) == true)
            {
                accessClass.GetCurrentPosition();

                if (transRecStruct.chrTransType == 's' || transRecStruct.chrTransType == 'S' && (resizedArray[accessClass.GetCurrentPosition()].intQtyOnHand - transRecStruct.intTransQty > -1))
                {
                    resizedArray[accessClass.GetCurrentPosition()].intQtyOnHand = resizedArray[accessClass.GetCurrentPosition()].intQtyOnHand - transRecStruct.intTransQty;
                }

                else if (transRecStruct.chrTransType == 'p' || transRecStruct.chrTransType == 'P')
                {
                    resizedArray[accessClass.GetCurrentPosition()].intQtyOnOrder = resizedArray[accessClass.GetCurrentPosition()].intQtyOnOrder + transRecStruct.intTransQty;
                }

                else if (transRecStruct.chrTransType == 'r' || transRecStruct.chrTransType == 'R' && (resizedArray[accessClass.GetCurrentPosition()].intQtyOnHand - transRecStruct.intTransQty > -1))
                {
                    resizedArray[accessClass.GetCurrentPosition()].intQtyOnHand = resizedArray[accessClass.GetCurrentPosition()].intQtyOnHand + transRecStruct.intTransQty;
                    resizedArray[accessClass.GetCurrentPosition()].intQtyOnOrder = resizedArray[accessClass.GetCurrentPosition()].intQtyOnOrder - transRecStruct.intTransQty;
                }

                resizedArray = accessClass.GetIncomingArray();

            }

            else   // if the part num is not in the master list, show error and ignore it.
            {
                MessageBox.Show(localTransPartNum + " does not exist in the list.");
            }
            // * * * * * * * * * END OF PROCESSING FIRST LINE OF TRANSACTIONS.IN * * * * * * * *

            int m = 1;
            string importLineRead1 = "";
            importLineRead1 = din3.ReadLine();
            while (importLineRead1 != null)
            {

                string[] importtLnRdArray1 = importLineRead1.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                transRecStruct.strPartNumber = importtLnRdArray1[0];
                // this char conversion is currently showing the ascii int val in the struct..
                transRecStruct.chrTransType = Convert.ToChar(importtLnRdArray1[1]);
                transRecStruct.intTransQty = Convert.ToInt32(importtLnRdArray1[2]);

                transactionsIn[m].strPartNumber = transRecStruct.strPartNumber;
                transactionsIn[m].chrTransType = transRecStruct.chrTransType;
                transactionsIn[m].intTransQty = transRecStruct.intTransQty;

                // Split string with hyphen and store into struct
                string forTransSplit1 = transRecStruct.strPartNumber;
                string[] remHyphen2 = forTransSplit1.Split('-');
                string firstHalfTransPartNum1 = remHyphen2[0];
                string secHalfTransPartNum1 = remHyphen2[1];
                string localTransPartNum1 = firstHalfTransPartNum1 + secHalfTransPartNum1;
                transRecStruct.strPartNumber = localTransPartNum1;


                if (accessClass.FindElement(/* IN */resizedArray,/* IN */ Convert.ToInt32(localTransPartNum1)) == true)
                {
                    accessClass.GetCurrentPosition();

                    if (transRecStruct.chrTransType == 's' || transRecStruct.chrTransType == 'S')
                    {
                        resizedArray[accessClass.GetCurrentPosition()].intQtyOnHand = resizedArray[accessClass.GetCurrentPosition()].intQtyOnHand - transRecStruct.intTransQty;
                        
                    }

                    else if (transRecStruct.chrTransType == 'p' || transRecStruct.chrTransType == 'P')
                    {
                        resizedArray[accessClass.GetCurrentPosition()].intQtyOnOrder = resizedArray[accessClass.GetCurrentPosition()].intQtyOnOrder + transRecStruct.intTransQty;
                    }

                    else if (transRecStruct.chrTransType == 'r' || transRecStruct.chrTransType == 'R')
                    {
                        resizedArray[accessClass.GetCurrentPosition()].intQtyOnHand = resizedArray[accessClass.GetCurrentPosition()].intQtyOnHand + transRecStruct.intTransQty;
                        resizedArray[accessClass.GetCurrentPosition()].intQtyOnOrder = resizedArray[accessClass.GetCurrentPosition()].intQtyOnOrder - transRecStruct.intTransQty;
                    }

                }

                else   // if the part num is not in the master list, show error and ignore it.
                {
                    MessageBox.Show(localTransPartNum + " does not exist in the list.");
                }

                

                m++;
                importLineRead1 = din3.ReadLine();
            }

            resizedArray = accessClass.GetIncomingArray();

            din1.Close();
            din3.Close();

            procTransBtn.Enabled = false;
            outputBtn.Enabled = true;
        }




        private void OutputBtn_Click(object sender, EventArgs e)
        {
            // for sales.out
            StreamWriter dout;
            dout = new StreamWriter("sales.out");
            dout.WriteLine("Part No" + "     Qty");

            // for purchases.out
            StreamWriter dout1;
            dout1 = new StreamWriter("purchases.out");
            dout1.WriteLine("Part No" + "     Qty");

            // for receipts.out
            StreamWriter dout2;
            dout2 = new StreamWriter("receipts.out");
            dout2.WriteLine("Part No" + "     Qty");

           for (int a = 0; a < transactionsIn.Length; a++)
            {
                if(transactionsIn[a].chrTransType == 's' || transactionsIn[a].chrTransType =='S')
                {
                    dout.WriteLine(transactionsIn[a].strPartNumber + "     " + transactionsIn[a].intTransQty);

                }

                else if(transactionsIn[a].chrTransType == 'p' || transactionsIn[a].chrTransType == 'P')
                {
                    dout1.WriteLine(transactionsIn[a].strPartNumber + "     " + transactionsIn[a].intTransQty);
                }

                else if(transactionsIn[a].chrTransType == 'r' || transactionsIn[a].chrTransType == 'R')
                {
                    dout2.WriteLine(transactionsIn[a].strPartNumber + "     " + transactionsIn[a].intTransQty);
                }
            }

            dout.Close();
            dout1.Close();
            dout2.Close();

            // write to Inventory.out
            StreamWriter dout5;
            dout5 = new StreamWriter("Inventory.out");

            for (int y = 0; y < resizedArray.Length; y++)
            {
                if (resizedArray[y].strPartNumber != null)
                {
                    dout5.WriteLine(resizedArray[y].strPartNumber + "     " + resizedArray[y].strDescription1 + "     " + resizedArray[y].strDescription2 + "     " + resizedArray[y].strDescription3 + "     " + resizedArray[y].intQtyOnHand + "    " + resizedArray[y].intQtyOnOrder);

                }
            }

            dout5.Close();

            // output to noActicity.out
            StreamWriter dout4;
            dout4 = new StreamWriter("noActivity.out");
            dout4.WriteLine("Part No" + "    ************ Description ************");
            
            // loop through resizedArray, for each pass through check transactions for matching part num
            InvRec[] filteredArray = new InvRec[resizedArray.Length];
            RemoveHyphenFromTrans(/* IN & OUT */transactionsIn);
           
            for(int g = 0; g < resizedArray.Length; g++)
            {
                for(int f = 0; f < transactionsIn.Length; f++)
                {
                    
                    if(transactionsIn[f].strPartNumber != resizedArray[g].strPartNumber)
                    {
                        //accessClass.DeleteFromList(resizedArray);
                        filteredArray[g].strPartNumber = resizedArray[g].strPartNumber;
                        filteredArray[g].strDescription1 = resizedArray[g].strDescription1;
                        filteredArray[g].strDescription2 = resizedArray[g].strDescription2;
                        filteredArray[g].strDescription3 = resizedArray[g].strDescription3;
                        
                    }              
                }  
            }

        
            for (int z = 0; z < filteredArray.Length; z++)
            {
                    dout4.WriteLine(filteredArray[z].strPartNumber + "    " + filteredArray[z].strDescription1 + "            " + filteredArray[z].strDescription2 + "       " + filteredArray[z].strDescription3);
            }
                dout4.Close();

            outputBtn.Enabled = false;

            MessageBox.Show("Your reports have been created and saved in the bin folder");
        }


        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void RemoveHyphen(/* IN & OUT */InvRec[] array)
        {
            InvRec[] tempArray = new InvRec[array.Length];

            for (int j = 0; j < array.Length; j++)
            {

                tempArray[j] = array[j];
                string forSplit = tempArray[j].strPartNumber;
                string[] remHyphen = forSplit.Split('-');
                string firstHalfPartNum = remHyphen[0];
                string secHalfPartNum = remHyphen[1];
                string localPartNum = firstHalfPartNum + secHalfPartNum;

                tempArray[j].strPartNumber = localPartNum;
                array[j].strPartNumber = tempArray[j].strPartNumber;
                array[j].strDescription1 = tempArray[j].strDescription1;
                array[j].strDescription2 = tempArray[j].strDescription2;
                array[j].strDescription3 = tempArray[j].strDescription3;
                array[j].intQtyOnOrder = tempArray[j].intQtyOnOrder;
                array[j].intQtyOnHand = tempArray[j].intQtyOnHand;
            }

            array = tempArray;
        }

        public void RemoveHyphenFromTrans(/* IN & OUT */TransRec[] transArray)
        {
            TransRec[] tempArray1 = new TransRec[transArray.Length];

            for (int d = 0; d < transArray.Length; d++)
            {

                tempArray1[d] = transArray[d];
                string forSplit3 = tempArray1[d].strPartNumber;
                string[] remHyphen3 = forSplit3.Split('-');
                string firstHalfPartNum3 = remHyphen3[0];
                string secHalfPartNum3 = remHyphen3[1];
                string localPartNum3 = firstHalfPartNum3 + secHalfPartNum3;

                tempArray1[d].strPartNumber = localPartNum3;
                transArray[d].strPartNumber = tempArray1[d].strPartNumber;
                transArray[d].chrTransType = tempArray1[d].chrTransType;
                transArray[d].intTransQty = tempArray1[d].intTransQty;
            }

            transArray = tempArray1;
        }

    }
}
