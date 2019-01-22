/* Name: Alyssa Bell
 * Date: 5/1/17
 * Filename: InventoryProject
 * Purpose: To alter an incoming inventory list for trcking & updating purposes
 * 
 * Assumptions: the inventory file list will retain the same types of information and format, and that no additional
 * values will be added in.
 * 
 * Error Checking/Exceptions: There is a duplicate key exception. If a part number already exists within the list,
 * a duplicate of it will not be added.
 * 
 * Summary of Methods: 
public void SetInClassArray(int [] array) - Takes in the incoming array  and sets the class's list from this incoming list

public int[] GetIncomingArray() - this returns the class's array after it has been set

public int GetArrayLenght(int[] array) - returns the entire lenght of the list (including padded 0s) using listArray.Length

public void SetCurrentPosition(int[] pos) - takes in the pos value that has been returned from other methods and stores it

public int GetCurrentPosition() - this just returns the current position that has been set inside SetCurrentPosition()
 * by the other methods

public void SortList(int[] list, ref leftPointer, int left, int right) - this takes in the class's list sorts it with Quicksort

public void RearrangeList(int [] list, ref leftPointer, ref rightPointer, pivotVal) - this moves values around when performing QuickSort

public void PrintArray() - it outputs the array to the screen

public bool FindElement(int [] array, int num) - returns T or F if a value is found

public void ExpandList(int[] array) - this doubles the size of the array length and saves the new size

public void ContractList(int[] array) - this only cuts the list size in half 

public void InsertIntoList(int[] array, int insert)
- this takes in current position, places in the value to be inserted (inside an already sorted list),
 and then updates current position
- if the insert value is less than listArray[0], it is added to the front of the list
- if the insert value is greater than listArray[listArray.Length-1], it's added to the end of list
- if the insert value is greater than listArray[0] && < listArray[listArray.Length-1], it will be 
  inserted into the appropriate space in the list
- it then calls NeedToExpand() in case the current position is now past the halfway mark


public void DeleteFromList(InvRec[] array)
- sets current position, scans through sorted list pos by pos, and deletes the 2nd record
of a duplicate value and throws exception, then shifts the remaing values down one.
- it then sets the current position

public bool NeedToExpand(InvRec[] array)
- if the array is filled up == true, this calls ExpantList() to double the size of the array
- lastly, it updates the current position
- returns false if list is not full

public bool NeedToShrink(InvRec[] array)
- if the min occupancy of the list is less than 25% and the array is already larger than 4 elements,
 it will call Contract() and cut the list in half
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingProject
{

    class ListClass
    {
        // PDMs
        private int currSize;
        private int currMaxSize;
        private int currPos;
        // making the array constant size for now for testing purposes
        const int INITIAL_SIZE = 4;
        InvRec[] listArray = new InvRec[INITIAL_SIZE];



        // default constructor
        public ListClass()
        {
            currSize = 0;
            currMaxSize = 0;
            currPos = 0;
            listArray = new InvRec[INITIAL_SIZE];

        }

        // non-default constructor
        public ListClass(InvRec[] array, int pos)
        {
            this.currSize = array.Length;
            this.currMaxSize = array.Length * 2;
            this.currPos = pos;
            // making deep copy of array
            InvRec[] temp = new InvRec[array.Length];
           
            for (int i = 0; i < currSize; i++)
            {
                temp[i] = listArray[i];
            }
            
            this.listArray = temp;
           

        }

        public void SetInClassArray(InvRec[] array)
        {
            listArray = array;
        }


        public InvRec[] GetIncomingArray()
        {
            return listArray;
        }

       
        public int GetArrayLength(InvRec[] array)
        {
            currSize = GetIncomingArray().Length;
            return currSize;
        }


     
        public void SetCurrentPosition(int pos)
        {

            currPos = pos;
        }

        public int GetCurrentPosition()
        {

            return currPos;
        }




        // uses QuickSort to sort array
        public void SortList(/* IN & OUT */ InvRec[] list, /* IN */int left, /* IN */int right)
        {

            int leftPointer = left;
            int rightPointer = right;
            int num = (left + right) / 2;
            int pivotVal = Convert.ToInt32(list[num].strPartNumber);

            RearrangeList(/* IN & OUT */list, /* IN & OUT */ ref leftPointer,/* IN & OUT */ ref rightPointer, /* IN */pivotVal);

            if (leftPointer < right)
            {
                SortList(/* IN & OUT */list,/* IN */ leftPointer,/* IN */ right);
            }

            if (left < rightPointer)
            {
                SortList(/* IN & OUT */list, /* IN */left, /* IN */rightPointer);
            }
        }

        public void RearrangeList(/* IN & OUT */InvRec[] list, /* IN & OUT */ref int leftPointer, /* IN & OUT */ref int rightPointer, /* IN & OUT */int pivotVal)
        {
            while (leftPointer <= rightPointer)
            {
             
                while (Convert.ToInt32(list[leftPointer].strPartNumber) < pivotVal)
                {
                    leftPointer++;

                }

         
                while (Convert.ToInt32(list[rightPointer].strPartNumber) > pivotVal)
                {
                    rightPointer--;
                }

                if (leftPointer <= rightPointer)
                {
                    string tempPartNumber = list[leftPointer].strPartNumber;
                    string tempDesc1 = list[leftPointer].strDescription1;
                    string tempDesc2 = list[leftPointer].strDescription2;
                    string tempDesc3 = list[leftPointer].strDescription3;
                    int tempQtyOnHand = list[leftPointer].intQtyOnHand;
                    int tempQtyOnOrder = list[leftPointer].intQtyOnOrder;

                    list[leftPointer].strPartNumber = list[rightPointer].strPartNumber;
                    list[leftPointer].strDescription1 = list[rightPointer].strDescription1;
                    list[leftPointer].strDescription2 = list[rightPointer].strDescription2;
                    list[leftPointer].strDescription3 = list[rightPointer].strDescription3;
                    list[leftPointer].intQtyOnHand = list[rightPointer].intQtyOnHand;
                    list[leftPointer].intQtyOnOrder = list[rightPointer].intQtyOnOrder;

                    list[rightPointer].strPartNumber = tempPartNumber;
                    list[rightPointer].strDescription1 = tempDesc1;
                    list[rightPointer].strDescription2 = tempDesc2;
                    list[rightPointer].strDescription3 = tempDesc3;
                    list[rightPointer].intQtyOnHand = tempQtyOnHand;
                    list[rightPointer].intQtyOnOrder = tempQtyOnOrder;
                    leftPointer++;
                    rightPointer--;

                }
            }

        }

       

        // finding an element that was sent in by user and returning T or F if it's in the list
        public bool FindElement(InvRec[] array, int num)
        {
            int numIsHere = 0;
            bool isFound = false;

            for (int i = 0; i < listArray.Length; i++)
            {
                if (Convert.ToInt32(listArray[i].strPartNumber) == num)
                {
                    isFound = true;
                    Console.WriteLine("The value you're searching for ({0}) is in the list", num);
                    numIsHere = i;
                }
            }

            if (isFound == false)
            {
                Console.WriteLine("{0} does not exist in the list currently", num);
            }

            SetCurrentPosition(numIsHere);
            return isFound;
        }



        public void ExpandList(InvRec[] array)
        {
            currSize = listArray.Length;
            //int[] myArr = new int[arraySize];

            InvRec[] temp = new InvRec[currSize * 2];
            //writing 0-4 into this array
            for (int i = 0; i < currSize; i++)
            {
                temp[i].strPartNumber = listArray[i].strPartNumber;
                temp[i].strDescription1 = listArray[i].strDescription1;
                temp[i].strDescription2 = listArray[i].strDescription2;
                temp[i].strDescription3 = listArray[i].strDescription3;
                temp[i].intQtyOnHand = listArray[i].intQtyOnHand;
                temp[i].intQtyOnOrder = listArray[i].intQtyOnOrder;
                    
            }

            listArray = temp;

            // BECOME THE NEW LENGTH THAT CAN BE CHANGED WHEN THIS METHOD IS USED
            currSize = currSize * 2;

            Console.WriteLine("The expanded array is {0}", GetArrayLength(listArray));
            for (int k = 0; k < listArray.Length; k++)
            {
                Console.WriteLine(listArray[k]);
            }
        }


        public void ContractList(InvRec[] array)
        {
            currSize = listArray.Length / 2;

            InvRec[] temp = new InvRec[currSize];

            for (int i = 0; i < currSize; i++)
            {
                temp[i].strPartNumber = listArray[i].strPartNumber;
                temp[i].strDescription1 = listArray[i].strDescription1;
                temp[i].strDescription2 = listArray[i].strDescription2;
                temp[i].strDescription3 = listArray[i].strDescription3;
                temp[i].intQtyOnHand = listArray[i].intQtyOnHand;
                temp[i].intQtyOnOrder = listArray[i].intQtyOnOrder;

            }

            listArray = temp;
            currSize = currSize / 2;

            Console.WriteLine("The contracted array is {0}", GetArrayLength(listArray));

            for (int l = 0; l < listArray.Length; l++)
            {
                Console.WriteLine(listArray[l]);
            }
        }


        public void InsertIntoList(InvRec[] array, InvRec insert)
        {
            string tempPartNum = "";
            string tempDesc1 = "";
            string tempDesc2 = "";
            string tempDesc3 = "";
            int tempQtyOnHand = 0;
            int tempQtyOnOrder = 0;

            // update the current position
            int currPos = -1;

            for (int m = 0; Convert.ToInt32(listArray[m].strPartNumber) > 0; m++)
            {
                currPos++;
            }

            SetCurrentPosition(currPos);


            
            int i = 0;
            int posCounter = 0; // this is the value where the value to insert would go
            InvRec[] tempArray = new InvRec[currSize + 1];


            // If insert val is < val at listArray[0]
            if (Convert.ToInt32(insert.strPartNumber) < Convert.ToInt32(listArray[0].strPartNumber))
            {
                for (int a = 0; a < listArray.Length; a++)
                {
                    tempArray[a + 1].strPartNumber = listArray[a].strPartNumber;
                    tempArray[a + 1].strDescription1 = listArray[a].strDescription1;
                    tempArray[a + 1].strDescription2 = listArray[a].strDescription2;
                    tempArray[a + 1].strDescription3 = listArray[a].strDescription3;
                    tempArray[a + 1].intQtyOnHand = listArray[a].intQtyOnHand;
                    tempArray[a + 1].intQtyOnOrder = listArray[a].intQtyOnOrder;
                }

                for (int b = 0; b < tempArray.Length - 1; b++)
                {
                    listArray[b].strPartNumber = tempArray[b].strPartNumber;
                    listArray[b].strDescription1 = tempArray[b].strDescription1;
                    listArray[b].strDescription2 = tempArray[b].strDescription2;
                    listArray[b].strDescription3 = tempArray[b].strDescription3;
                    listArray[b].intQtyOnHand = tempArray[b].intQtyOnHand;
                    listArray[b].intQtyOnOrder = tempArray[b].intQtyOnOrder;
                    
                }

                listArray[0].strPartNumber = insert.strPartNumber;

                for (int c = 0; c < listArray.Length; c++)
                {
                    Console.WriteLine("The new array is {0}", listArray[c]);
                }
            }

            // if insert > listArray[last position] - insert at end of array 
            else if (Convert.ToInt32(insert) > Convert.ToInt32(listArray[currPos + 1].strPartNumber))
            {
                listArray[currPos + 1].strPartNumber = insert.strPartNumber;

                for (int d = 0; d < listArray.Length; d++)
                {
                    Console.WriteLine("The new array is {0}", listArray[d]);
                }
            }


            // If insert val > listArray[0]  and insert val is < val at listArray.Lenght-1
            else if ((Convert.ToInt32(insert) > Convert.ToInt32(listArray[0].strPartNumber)) && (Convert.ToInt32(insert) < listArray.Length - 1))
            {

                while (Convert.ToInt32(listArray[i].strPartNumber) < Convert.ToInt32(insert))
                {
                    i++;
                    posCounter++;
                }

                posCounter = i;

                // shift everything starting at end of array down to insert position over to the right 1 space
                for (int k = listArray.Length - 2; k >= posCounter; k--)
                {
                    
                    tempPartNum = listArray[k].strPartNumber;
                    tempDesc1 = listArray[k].strDescription1;
                    tempDesc2 = listArray[k].strDescription2;
                    tempDesc3 = listArray[k].strDescription3;
                    tempQtyOnHand = listArray[k].intQtyOnHand;
                    tempQtyOnOrder = listArray[k].intQtyOnOrder;

                    listArray[k + 1].strPartNumber = tempPartNum;
                    listArray[k + 1].strDescription1 = tempDesc1;
                    listArray[k + 1].strDescription2 = tempDesc2;
                    listArray[k + 1].strDescription3 = tempDesc3;
                    listArray[k + 1].intQtyOnHand = tempQtyOnHand;
                    listArray[k + 1].intQtyOnOrder = tempQtyOnOrder;
                }


                listArray[posCounter].strPartNumber = insert.strPartNumber;
                listArray[posCounter].strDescription1 = insert.strDescription1;
                listArray[posCounter].strDescription2 = insert.strDescription2;
                listArray[posCounter].strDescription3 = insert.strDescription3;
                listArray[posCounter].intQtyOnHand = insert.intQtyOnHand;
                listArray[posCounter].intQtyOnOrder = insert.intQtyOnOrder;

                for (int j = 0; j < listArray.Length; j++)
                {
                    Console.WriteLine("The new array is {0}", listArray[j]);
                }
            }

            currPos = -1;

            for (int n = 0; Convert.ToInt32(listArray[n].strPartNumber) > 0; n++)
            {
                currPos++;
            }

            SetCurrentPosition(currPos);
            NeedToExpand(listArray);
        }



        public void DeleteFromList(InvRec[] array)
        {
            string tempPartNum = "";
            string tempDesc1 = "";
            string tempDesc2 = "";
            string tempDesc3 = "";
            int tempQtyOnHand = 0;
            int tempQtyOnOrder = 0;

            // setting currPos
            int currPos = -1;

            for (int x = 0; Convert.ToInt32(listArray[x].strPartNumber) > 0; x++)
            {
                currPos++;
            }

            int dupToDelete = 0;
          
            SetCurrentPosition(currPos);



            for (int y = 0; y <= currPos; y++)
            {
                // if these 2 values side by side are the same, delete the right one
                if (listArray[y].strPartNumber == listArray[y + 1].strPartNumber && Convert.ToInt32(listArray[y].strPartNumber) > 0)
                {
                    try
                    {
                        Console.WriteLine("\n{0} is a duplicate record and has been deleted from the list.\n", listArray[y + 1]);
                    }
                    catch
                    {
                        throw new DuplicateKey();
                    }

                    // this is marking the position of the right value that can be replaced
                    dupToDelete = y + 1;

                    // shift everything starting at y+2 down one to replace y+1
                    for (int z = dupToDelete; z <= currPos; z++)
                    {
                        tempPartNum = listArray[z + 1].strPartNumber;
                        tempDesc1 = listArray[z + 1].strDescription1;
                        tempDesc2 = listArray[z + 1].strDescription2;
                        tempDesc3 = listArray[z + 1].strDescription3;
                        tempQtyOnHand = listArray[z + 1].intQtyOnHand;
                        tempQtyOnOrder = listArray[z + 1].intQtyOnOrder;

                        listArray[z].strPartNumber = tempPartNum;
                        listArray[z].strDescription1 = tempDesc1;
                        listArray[z].strDescription2 = tempDesc2;
                        listArray[z].strDescription3 = tempDesc3;
                        listArray[z].intQtyOnHand = tempQtyOnHand;
                        listArray[z].intQtyOnOrder = tempQtyOnOrder;
                    }

                }

            }



            // setting currPos
            currPos = -1;

            for (int x = 0; Convert.ToInt32(listArray[x].strPartNumber) > 0; x++)
            {
                currPos++;
            }

            SetCurrentPosition(currPos);

        }


        public bool NeedToExpand(InvRec[] array)
        {
            bool needToExpand = false;
            int currPos = -1;

            // setting currPos
            if (listArray.Length == 4)
            {

                for (int x = 0; x < listArray.Length; x++)
                {
                    currPos++;
                }
            }

            if (listArray.Length > 4)
            {
      
                for (int x = 0; x < listArray.Length; x++)
                {
                    if (int.Parse(listArray[x].strPartNumber) > 0)
                    {
                        currPos++;
                    }
                }
            }

            if (currPos > (array.Length / 2))
            {
                needToExpand = true;
                ExpandList(array);



            }

            SetCurrentPosition(currPos);

            return needToExpand;
        }



        // need to set the currPos still
        public bool NeedToShrink(InvRec[] array)
        {

            // setting currPos
            int currPos = -1;

            for (int x = 0; Convert.ToInt32(listArray[x].strPartNumber) > 0; x++)
            {
                currPos++;
            }

            SetCurrentPosition(currPos);

            bool needToShrink = false;
            // this might be useless if it's reading the 0s..need to change that so it's counting values >0
            float minOccupancy = ((float)listArray.Length * (float).25);
            //if the current position is less than 25% of the array size and the array is at least 8 elements long
            if ((currPos < minOccupancy) && (listArray.Length >= (INITIAL_SIZE * 2)))
            {
                needToShrink = true;
                ContractList(array);
            }

            return needToShrink;
        }

    }
}
