using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST2
{
    public class Node<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public TKey key;
        public TValue value;
        public Node<TKey, TValue> left, right;

        public Node(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
            left = right = null;
        }

        public string displayNode()
        {
            StringBuilder output = new StringBuilder();
            displayNode(output, 0);
            return output.ToString();
        }

        private void displayNode(StringBuilder output, int depth)
        {

            if (right != null)
                right.displayNode(output, depth + 1);

            output.Append('\t', depth);
            output.AppendLine(value.ToString());


            if (left != null)
                left.displayNode(output, depth + 1);

        }
    }

    public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public Node<TKey, TValue> root;

        private Node<TKey, TValue> Insert(Node<TKey, TValue> root, TKey key, TValue value)
        {
            if (root == null)
            {
                root = new Node<TKey, TValue>(key, value);
                root.key = key;
                root.value = value;
            }
            else if (key.CompareTo(root.key) == -1) //less than
                root.left = Insert(root.left, key, value);
            else
                root.right = Insert(root.right, key, value);

            return root;
        }

        public TKey MinValue(Node<TKey, TValue> root)
        {
            TKey minval = root.key;
            while (root.left != null)
            {
                minval = root.left.key;
                root = root.left;
            }
            return minval;
        }

        private Node<TKey, TValue> Delete(Node<TKey, TValue> root, TKey key)
        {
            if (root == null)
                return root;
            else if (root.key.Equals(key)) //equals
                root = null;
            else if (key.CompareTo(root.key) == -1) //less than
                root.left = Delete(root.left, key);
            else if (key.CompareTo(root.key) == 1)  //greater than
                root.right = Delete(root.right, key);
            else
            {
                if (root.left == null)
                    return root.right;
                else if (root.right == null)
                    return root.left;

                root.key = MinValue(root.right);
                root.right = Delete(root.right, root.key);
            }
            return root;
        }

        private Node<TKey, TValue> Search(Node<TKey, TValue> root, TKey key)
        {
            if (root == null)
                return root;
            else if (root.key.Equals(key))
                return root;
            else if (key.CompareTo(root.key) == -1)
                return Search(root.left, key);
            else
                return Search(root.right, key);
        }

        private void InOrder(Node<TKey, TValue> root)
        {
            if (root != null)
            {
                InOrder(root.left);
                Console.WriteLine(root.key + " " + root.value);
                InOrder(root.right);
            }

        }

        private void PreOrder(Node<TKey, TValue> root)
        {
            if (root != null)
            {
                Console.WriteLine(root.key + " " + root.value);
                PreOrder(root.left);
                PreOrder(root.right);
            }
        }

        public void Insert(TKey key, TValue value)
        {
            if (root == null)
            {
                root = new Node<TKey, TValue>(key, value);
                root.key = key;
                root.value = value;
            }
            else
            {
                Insert(root, key, value);
            }
        }

        public void Delete(TKey key)
        {
            if (root.key.Equals(key))
                root = null;
            else
                Delete(root, key);
        }

        public Node<TKey, TValue> Search(TKey key)
        {
            if (root.key.Equals(key))
                return root;

            else
            {
                return Search(root, key);
            }
        }

        public void InOrder()
        {
            InOrder(root);
        }

        public void PreOrder()
        {
            PreOrder(root);
        }

        //    **How to use**
        //
        //  var tree = new BinarySearchTree<string, int>();
        //  tree.Insert(23, "peepoo");
        //  isFound = tree.Search(23)
        //  if (isFound != null)
        //      {
        //          Console.WriteLine("{0} is found. It has a value of {1}", isFound.key, isFound.value);
        //      }
        //      else
        //      {
        //          Console.WriteLine("No such key exists.");
        //      }
    }
}
