using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MachineProblem
{
    class TextFileIO
    {
        //To load main menu in main program
        public Dictionary<string, decimal> LoadMenu()
        {
            Dictionary<string, decimal> menu = new Dictionary<string, decimal>();

            try
            {
                //Read all line in text file of Menu.txt
                string[] menuLine = File.ReadAllLines("Menu.txt");
                foreach (string item in menuLine)
                {
                    string[] temp = item.Split(',');
                    menu.Add(temp[0].ToUpper(), decimal.Parse(temp[1]));
                }
            }
            catch
            {
                var fs = File.Open("Menu.txt", FileMode.Append);
                fs.Close();
            }


            return menu;
        }

        //To load the orders in main program 4827914476
        public string[] LoadBST()
        {
            try
            {
                //Read all lines in text file and return it as string[]
                return File.ReadAllLines("TextFileBST.txt");
            }
            catch
            {
                //Create new empty text file
                FileStream newFile = File.Create("TextFileBST.txt");
                newFile.Close();
                return File.ReadAllLines("TextFileBST.txt");
            }

        }

        //Add new line in textfile for new orders
        public void NewOrder(int orderNumber, string content)
        {
            StreamWriter writer = new StreamWriter("TextFileBST.txt", true);
            writer.WriteLine("{0}={1}", orderNumber, content); //Save order info in text file
            writer.Close();
        }

        //To delay order
        public void DelayOrder(int orderNumber)
        {
            string orderDelay = "";

            List<string> list = File.ReadAllLines("TextFileBST.txt").ToList();
            for (int i = 0; i < list.Count; i++)
            {
                string[] temp = list[i].Split('=');
                if (int.Parse(temp[0]) == orderNumber)
                {
                    orderDelay = list[i];//Save line before removing

                    list.RemoveAt(i);//Remove the order line in list
                    break;
                }
            }
            list.Add(orderDelay); //Then add the save line in list

            //Rewrite text file
            StreamWriter writer = new StreamWriter("TextFileBST.txt");
            foreach (string item in list)
            {
                writer.WriteLine(item);
            }
            writer.Close();
        }

        //Delete line according to specific order number
        public void DeleteOrder(int orderNumber)
        {
            //Read all line in text file and put it in the list
            List<string> list = File.ReadAllLines("TextFileBST.txt").ToList();
            for (int i = 0; i < list.Count; i++)
            {
                string[] temp = list[i].Split('=');
                if (int.Parse(temp[0]) == orderNumber)
                {
                    //Remove the order line in list
                    list.RemoveAt(i);
                    break;
                }
            }

            //Rewrite text file
            StreamWriter writer = new StreamWriter("TextFileBST.txt");
            foreach (string item in list)
            {
                writer.WriteLine(item);
            }
            writer.Close();
        }
    }
}