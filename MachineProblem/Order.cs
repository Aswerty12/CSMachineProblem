using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineProblem
{
    //test
    class Order
    {
        //Class attributes
        public int orderNumber;
        public string orderContent;

        //Add function for ordering
        public List<string> addContent(List<string> currentOrder)
        {
            string content;
            Console.WriteLine("Add order content: ");
            content = Console.ReadLine();

            currentOrder.Add(content);

            return currentOrder;
        }

        //Delete function
        public List<string> deleteContent(List<string> currentOrder)
        {
            int contentIndex;
            try
            {
                Console.WriteLine("Number of content to be deleted: ");
                bool boolean = int.TryParse(Console.ReadLine(), out contentIndex);
                currentOrder.RemoveAt(contentIndex);
            }
            catch (System.ArgumentOutOfRangeException)
            {
                Console.WriteLine("Content does not exist");
            }
            
            return currentOrder;
        }
    }
}
