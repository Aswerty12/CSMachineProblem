using System;
using System.Collections.Generic;
using BST2;
using System.Threading;
namespace MachineProblem
{
    //Kepp it object oriented, use functions and remember to comment anything that you yourself have trouble explaining.
    class Program
    {
        // Standard Main Loop that lets User select Customer (Ordering) or Employee (Delivering)
        static void Main(string[] args)
        {
            TextFileIO textFile = new TextFileIO();

            //Create BST and Queue class here
            var tree = new BinarySearchTree<int, string>();
            Queue<int> orderQueue = new Queue<int>();
            Dictionary<string, decimal> menuList = textFile.LoadMenu();

            foreach (string item in textFile.LoadBST())
            {
                string[] temp = item.Split('=');
                tree.Insert(int.Parse(temp[0]), temp[1]);
                orderQueue.Enqueue(int.Parse(temp[0]));
            }



            while (true)
            {
                Console.WriteLine("Welcome to Food Capital's Delivering Service!");
                Console.Write("Are you:\n1) Ordering\n2) Delivering\nX) Exit\nChoice: ");
                string input = Console.ReadLine().ToUpper();
                Console.Clear();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("You chose to order!\n");
                        Thread.Sleep(500);
                        Order(tree, orderQueue, menuList);
                        continue;
                    case "2":
                        Console.WriteLine("You chose to deliver!\n");
                        Thread.Sleep(500);
                        Deliver(tree, orderQueue, menuList);
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


            bool while_bool = true;
            while (while_bool)
            {
                // Checks if order number already exists
                var isFound = tree.Search(new_order.orderNumber);
                if (isFound != null)
                {
                    Console.WriteLine("{0} already exists. This order contains {1}", isFound.key, isFound.value);
                    new_order.orderNumber = rnd.Next();
                    while_bool = true;
                }

                //displays currentOrder and orderNumber
                Console.WriteLine("Order Number: {0}" +
                " | Order Content: {1} | Current Price: {2}", new_order.orderNumber, string.Join(", ", currentOrder), new_order.orderPrice);

                //choices
                Console.Write("Press '1' to add to order | \nPress '2' to delete from your order | \nPress '3' to return to main menu | \nPress '4' to send order |\nChoice: ");
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
                        //finishes Order method
                        if (new_order.orderPrice > 0)
                        {
                            new_order.orderContent = string.Join(",", currentOrder);
                            tree.Insert(new_order.orderNumber, new_order.orderContent);
                            orderQueue.Enqueue(new_order.orderNumber);  //Converts string list currentOrder to single string then inserts and enqueues
                            Console.WriteLine("\nOrder received! Your total is P{0}.", new_order.orderPrice);
                            Console.ReadKey();
                            while_bool = false;
                        }
                        else
                        {
                            Console.WriteLine("\nOrder cannot be empty! Add items to your order first.", new_order.orderPrice);
                            Console.ReadKey();
                            while_bool = true;
                        }
                        break;

                    default:
                        break;
                }
                Console.Clear();
            }

            
            

            TextFileIO textFile = new TextFileIO();
            textFile.NewOrder(new_order.orderNumber, new_order.orderContent);
        }

        //Menu to deliver and access delivery related functions
        static void Deliver(BinarySearchTree<int, string> tree, Queue<int> orderQueue, Dictionary<string, decimal> menuList)
        {
            Deliver obj = new Deliver();

            bool boolean = true;
            while (boolean)
            {
                Console.Write("Press '1' to deliver current order |\nPress '2' to browse orders |\nPress '3' to view next order |\nPress '4' to delay current order" +
                    "|\nPress '5' to return to main menu |\nChoice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        //Delivers currently selected order
                        obj.deliverCurrentOrder(tree, orderQueue, menuList);
                        break;

                    case "2":
                        //Option that DISPLAYS ORDERS, that traverses INORDER
                        Console.WriteLine("\nDisplaying orders...");
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
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}