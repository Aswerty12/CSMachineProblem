using System;
using System.Collections.Generic;
using BST2;
namespace MachineProblem
{
    //Kepp it object oriented, use functions and remember to comment anything that you yourself have trouble explaining.
    class Program
    {
        // Standard Main Loop that lets User select Customer (Ordering) or Employee (Delivering)
        static void Main(string[] args)
        {
            //Create BST and Quuue class here
            var tree = new BinarySearchTree<int, string>();
            Queue<int> orderQueue = new Queue<int>();
            while (true)
            {
                Console.WriteLine("Welcome to RESTAURANT's Delivery Service.");
                Console.WriteLine("Are you\n1)Ordering\n2)Delivering");
                Console.WriteLine("X)Exit");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Order(tree, orderQueue);
                        continue;
                    case "2":
                        Deliver();
                        continue;
                    case "X":
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        continue;
                }
            }
        }
        //Menu to enter an order
        static void Order(BinarySearchTree<int, string> tree, Queue<int> orderQueue)
        {
            //Create and display customer's order number (Randomly generated number not in the bst yet)
            Random rnd = new Random();
            Order new_order = new Order();

            //currentOrder list for adding or deleting content later
            List<string> currentOrder = new List<string>();
            new_order.orderNumber = rnd.Next();

            //Checks tree if orderNumber already exists
            //if tree empty
            if (tree.root == null)
            {
                //Search throws exception unless there is root
                Console.WriteLine("root is null");
                bool while_bool = true;
                while (while_bool)
                {
                    //displays currentOrder and orderNumber
                    Console.WriteLine("Order Number: {0}" +
                    " | Order Content: {1}", new_order.orderNumber, string.Join(",", currentOrder));

                    //choices
                    Console.WriteLine("Press '1' to add content | Press '2' to delete content | Press '3' to return | Press '4' to continue");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            //addContent method from Order class
                            currentOrder = new_order.addContent(currentOrder);
                            while_bool = true;
                            break;

                        case "2":
                            //deleteContent method from Order class
                            currentOrder = new_order.deleteContent(currentOrder);
                            while_bool = true;
                            break;

                        case "3":
                            //returns to main
                            return;

                        case "4":
                            //continues Order method
                            while_bool = false;
                            break;

                        default:
                            break;
                    }
                }

                //Converts string list currentOrder to single string then inserts and enqueues
                new_order.orderContent = string.Join(",", currentOrder);
                tree.Insert(new_order.orderNumber, new_order.orderContent);
                orderQueue.Enqueue(new_order.orderNumber);
                Console.WriteLine("Order Recieved!");
            }

            //if tree has nodes
            else
            {
                //Loops until orderNumber is not in bst
                bool while_bool = true;
                while (while_bool)
                {
                    //orderNumber is in bst
                    //search function from bst class
                    var isFound = tree.Search(new_order.orderNumber);
                    if (isFound != null)
                    {
                        Console.WriteLine("{0} is found. It has a value of {1}", isFound.key, isFound.value);
                        new_order.orderNumber = rnd.Next();
                        while_bool = true;
                    }

                    //orderNumber is not in bst
                    else
                    {
                        bool while_bool2 = true;
                        while (while_bool2)
                        {
                            //displays currentOrder and orderNumber
                            Console.WriteLine("Order Number: {0}" +
                            " | Order Content: {1}", new_order.orderNumber, string.Join(",", currentOrder));

                            //choices
                            Console.WriteLine("Press '1' to add content | Press '2' to delete content | Press '3' to return | Press '4' to continue");
                            string choice = Console.ReadLine();
                            switch (choice)
                            {
                                case "1":
                                    //addContent method from Order class
                                    currentOrder = new_order.addContent(currentOrder);
                                    while_bool2 = true;
                                    break;

                                case "2":
                                    //deleteContent method from Order class
                                    currentOrder = new_order.deleteContent(currentOrder);
                                    while_bool2 = true;
                                    break;

                                case "3":
                                    //returns to main
                                    return;

                                case "4":
                                    //continues Order method
                                    while_bool2 = false;
                                    break;

                                default:
                                    break;
                            }
                        }

                        //Converts string list currentOrder to single string then inserts and enqueues
                        new_order.orderContent = string.Join(",", currentOrder);
                        tree.Insert(new_order.orderNumber, new_order.orderContent);
                        orderQueue.Enqueue(new_order.orderNumber);
                        Console.WriteLine("Order Recieved!");

                        while_bool = false;
                    }
                }
            }
        }



        //Menu to deliver and access delivery related functions
        static void Deliver()
        {
            //Option that DELIVERS, thus deleting whatever was next in queue and the associated BST entry
            //Option that DISPLAYS ORDERS, that traverses INORDER
            //Option that Displays next order (PEEKS at Queue but then searches and displays the associated BST entry
            //Option to return
        }
    }


}
