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
            TextFileIO textFile = new TextFileIO();

            //Create BST and Quuue class here
            var tree = new BinarySearchTree<int, string>();
            Queue<int> orderQueue = new Queue<int>();
            Dictionary<string, decimal> menuList = textFile.LoadMenu();
            //Dictionary<string, decimal> menuList = new Dictionary<string, decimal>()
            //{
            //    {"Coke", 12}, {"Bepis", 15}, {"Tapsi", 45}, {"Spag", 55}
            //};

            //Load BST and queue from text file
           
            foreach (string item in textFile.LoadBST())
            {
                string[] temp = item.Split('=');
                tree.Insert(int.Parse(temp[0]), temp[1]);
                orderQueue.Enqueue(int.Parse(temp[0]));
            }



            while (true)
            {
                Console.WriteLine("Welcome to RESTAURANT's Delivery Service.");
                Console.WriteLine("Are you\n1)Ordering\n2)Delivering");
                Console.WriteLine("X)Exit");
                string input = Console.ReadLine().ToUpper();
                switch (input)
                {
                    case "1":
                        Order(tree, orderQueue, menuList);
                        continue;
                    case "2":
                        Deliver(tree,orderQueue, menuList);
                        continue;
                    case "X":
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        continue;
                }
                break;
            }
        }
        //Menu to enter an order
        static void Order(BinarySearchTree<int, string> tree, Queue<int> orderQueue, Dictionary<string, decimal> menuList)
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
                    " | Order Content: {1} | Current Price: {2}", new_order.orderNumber, string.Join(",", currentOrder), new_order.orderPrice);

                    //choices
                    Console.WriteLine("Press '1' to add to order | \nPress '2' to delete from your order | \nPress '3' to return to main menu | \nPress '4' to send order |\n");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            //addContent method from Order class
                            currentOrder = new_order.addContent(currentOrder, menuList);
                            while_bool = true;
                            break;

                        case "2":
                            //deleteContent method from Order class
                            currentOrder = new_order.deleteContent(currentOrder, menuList);
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
                Console.WriteLine("Order Received! Your total is P{0}", new_order.orderPrice);
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
                            " | Order Content: {1} | Current Price: {2}", new_order.orderNumber, string.Join(",", currentOrder), new_order.orderPrice);

                            //choices
                            Console.WriteLine("Press '1' to add to order | \nPress '2' to delete from your order |\nPress '3' to return to main menu |\nPress '4' to send order |\n");
                            string choice = Console.ReadLine();
                            switch (choice)
                            {
                                case "1":
                                    //addContent method from Order class
                                    currentOrder = new_order.addContent(currentOrder, menuList);
                                    while_bool2 = true;
                                    break;

                                case "2":
                                    //deleteContent method from Order class
                                    currentOrder = new_order.deleteContent(currentOrder, menuList);
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
                        Console.WriteLine("Order Received! Your total is P{0}", new_order.orderPrice);
                        while_bool = false;

                    }
                }
            }

            //Save order in text file
            TextFileIO textFile = new TextFileIO();
            textFile.NewOrder(new_order.orderNumber, new_order.orderContent);

        }

        //Menu to deliver and access delivery related functions
        static void Deliver(BinarySearchTree<int,string> tree, Queue<int> orderQueue, Dictionary<string, decimal> menuList)
        {
            Deliver obj = new Deliver();
            Console.WriteLine("Deliver");

            bool boolean = true;
            while (boolean)
            {
                Console.WriteLine("\nPress '1' to Deliver Current order |\nPress '2' to browse orders |\nPress '3' to View next order |\nPress '4' to delay current order" +
                    "|\nPress '5' to return to main menu |\n");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        //Delivers currently selected order
                        obj.deliverCurrentOrder(tree, orderQueue, menuList);
                        break;

                    case "2":
                        //Option that DISPLAYS ORDERS, that traverses INORDER
                        tree.InOrder();
                        break;

                    case "3":
                        //Peeks at queue
                        obj.displayCurrentOrder(tree, orderQueue, menuList);
                        break;

                    case "4":
                        //Delays current order
                        obj.delayOrder(orderQueue);
                        break;

                    case "5":
                        //Option to return
                        return;

                    default:
                        //Invalid
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }


}