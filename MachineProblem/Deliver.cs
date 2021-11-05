using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BST2;

namespace MachineProblem
{
    class Deliver
    {
        public void deliverCurrentOrder(BinarySearchTree<int, string> tree, Queue<int> orderQueue, Dictionary<string, decimal> menuList)
        {
            //Option that DELIVERS, thus deleting whatever was next in queue and the associated BST entry
            Console.WriteLine("\nCurrent order:");
            try
            {
                int getOrderNumber = orderQueue.Dequeue(); //Get first order number in queue

                //Delete order in text file once the deliver is done
                TextFileIO textFile = new TextFileIO();
                textFile.DeleteOrder(getOrderNumber);

                Console.WriteLine("Order number: {0}\n", tree.Search(getOrderNumber).key); //Get the order number
                Console.WriteLine("Orders: {0}\n", tree.Search(getOrderNumber).value); //Get the orders
                string[] splitOrders = tree.Search(getOrderNumber).value.Split(",");
                decimal totalPrice = 0;
                foreach (string order in splitOrders)
                {
                    totalPrice += menuList[order];
                }
                Console.WriteLine("Total Price: {0}\n", totalPrice);

                tree.Delete(getOrderNumber); //delete in bst
            }
            catch (InvalidOperationException) //if the queue is empty
            {
                Console.WriteLine("No orders placed yet.");
            }
        }

        public void displayCurrentOrder(BinarySearchTree<int, string> tree, Queue<int> orderQueue, Dictionary<string, decimal> menuList)
        {
            //Option that Displays next order (PEEKS at Queue but then searches and displays the associated BST entry
            Console.WriteLine("\nNext order:");
            try
            {
                int peekOrderNumber = orderQueue.Peek(); //Get first order number in queue without removing it from queue

                Console.WriteLine("Order number: {0}\n", tree.Search(peekOrderNumber).key);
                Console.WriteLine("Orders: {0}\n", tree.Search(peekOrderNumber).value);
                string[] splitOrders = tree.Search(peekOrderNumber).value.Split(",");
                decimal totalPrice = 0;
                foreach (string order in splitOrders)
                {
                    totalPrice += menuList[order];
                }
                Console.WriteLine("Total Price: {0}\n", totalPrice);
            }
            catch (InvalidOperationException) //if the queue is empty
            {
                Console.WriteLine("No orders placed yet.");
            }
        }

        public void delayOrder(Queue<int> orderQueue)
        {
            try
            {
                int getOrderNumber = orderQueue.Dequeue(); //Get first order number in queue

                //textfile to delay
                TextFileIO textFile = new TextFileIO();
                textFile.DelayOrder(getOrderNumber);

                orderQueue.Enqueue(getOrderNumber); // Enqueues the number
                Console.WriteLine("Order delayed...");
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("No orders placed yet.");
            }
        }
    }
}