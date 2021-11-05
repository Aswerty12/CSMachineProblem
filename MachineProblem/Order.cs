using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineProblem
{
    class Order
    {
        //Class attributes
        public int orderNumber;
        public string orderContent;
        public decimal orderPrice;

        //Add function for ordering
        public List<string> addContent(List<string> currentOrder, Dictionary<string, decimal> menuList)
        {
            string content;
            // Loops if content is not in the menu
            do
            {
                Console.WriteLine("Add order content: ");
                content = Console.ReadLine();
                if (menuList.ContainsKey(content.ToUpper()))
                {
                    currentOrder.Add(content);
                    break;
                }
                else
                    Console.WriteLine("This item does not exist within the menu! Try again.");
            } while (true);

            addPrice(content, menuList);

            return currentOrder;
        }

        //Delete function
        public List<string> deleteContent(List<string> currentOrder, Dictionary<string, decimal> menuList)
        {
            int contentIndex;
            try
            {
                Console.WriteLine("Number of content to be deleted: ");
                bool boolean = int.TryParse(Console.ReadLine(), out contentIndex);
                subtractPrice(currentOrder.ElementAt(contentIndex-1), menuList);
                if (currentOrder.Count > 1)
                {
                    currentOrder.RemoveAt(contentIndex-1);
                }
                else
                {
                    currentOrder.Clear();
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("Content does not exist.");
            }
            
            return currentOrder;
        }

        public void addPrice(string content, Dictionary<string, decimal> menuList)
        {
            decimal itemPrice = menuList[content.ToUpper()];
            orderPrice += itemPrice;
        }

        public void subtractPrice(string content, Dictionary<string, decimal> menuList)
        {
            decimal itemPrice = menuList[content.ToUpper()];
            orderPrice -= itemPrice;
        }
    }
}
