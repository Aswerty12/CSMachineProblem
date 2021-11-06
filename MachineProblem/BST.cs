using System;

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


		public Node<TKey, TValue> ChildNode()
		{
			if (OneChild())
			{
				if (left != null) 
					return left;
				if (right != null)
					return right;
			}
			return null;
		}
		public bool NoChildren()
		{
			if (!TwoChildren() && !OneChild())
				return true;
			return false;
		}

		public bool OneChild()
		{
			if (left != null ^ right != null)
				return true;
			return false;
		}

		public bool TwoChildren()
		{
			if (left != null && right != null)
				return true;
			return false;
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

		public Node<TKey,TValue> MinValue(Node<TKey, TValue> root)
		{
			TKey minval = root.key;
			while (root.left != null)
			{
				minval = root.left.key;
				root = root.left;
			}
			return root;
		}

		public void Delete(Node<TKey, TValue> deleteThis)
		{
			if (deleteThis.NoChildren())
			{
				if (deleteThis.Equals(root))
				{
					root = null;
					return;
				}
				PostOrder((travRoot) =>
				{
					if (travRoot.left != null)
					{
						if (travRoot.left.Equals(deleteThis))
							travRoot.left = null;
					}
					if (travRoot.right != null)
					{
						if (travRoot.right.Equals(deleteThis))
							travRoot.right = null;
					}
				});
				return;
			}


			if (deleteThis.OneChild())
			{
				if (deleteThis.Equals(root))
				{
					root = root.ChildNode();
					return;
				}
				PostOrder((travRoot) =>
				{
					if (travRoot.left != null)
					{
						if (travRoot.left.Equals(deleteThis))
							travRoot.left = deleteThis.ChildNode();
					}
					if (travRoot.right != null)
					{
						if (travRoot.right.Equals(deleteThis))
							travRoot.right = deleteThis.ChildNode();
					}
				});
				return;
			}
			if (deleteThis.TwoChildren())
			{
				Node<TKey, TValue> minchildNode = MinValue(deleteThis.right);
				deleteThis.key = minchildNode.key;
				deleteThis.value = minchildNode.value;
				Delete(minchildNode);
				return;
			}	
		}

		private Node<TKey, TValue> Search(Node<TKey, TValue> root, TKey key)
		{
			if (root == null)
				return root;
			if (root.key.Equals(key))
				return root;
			if (key.CompareTo(root.key) == -1)
				root = Search(root.left, key);
			else
				root = Search(root.right, key);
			return root;
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


		public Node<TKey, TValue> Search(TKey key)
		{
			if (root == null)
				return root;
			if (root.key.Equals(key))
				return root;
			else
			{
				return Search(root, key);
			}
			
		}

		public void InOrder()
		{
			if (root == null)
				Console.WriteLine("No content found.");
			InOrder(root);
		}

		public void PostOrder(Action<Node<TKey, TValue>> delProcess)
		{
			if (root == null)
				Console.WriteLine("No content found.");
			PostOrder(root, delProcess);
		}

		private void PostOrder(Node<TKey,TValue> root, Action<Node<TKey, TValue>> delProcess)
		{
			if (root == null)
			{
				return;
			}

			PostOrder(root.left, delProcess);
			PostOrder(root.right, delProcess);
			delProcess(root);
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
