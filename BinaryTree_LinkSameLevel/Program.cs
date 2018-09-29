using System;
using System.Collections.Generic;

namespace BinaryTree_LinkSameLevel
{
    class Program
    {
        public class Node
        {
            public int val; // value of node
            public Node left; // left subtree
            public Node right; // right subtree
            public Node level; // level pointer (node “to the right”)           
        }

        static void Main(string[] args)
        {
            #region Creating and initialization

            List<Node> nodes = new List<Node>();
            var maxNodeValue = 6; // count-1 of nodes at tree; tree could build dynamically depending on this variable

            // initialization of next tree structure:          0
            //                                               /    \
            //                                              1      2
            //                                             / \    / \  
            //                                            3   4  5   6

            for (int i = 0; i <= maxNodeValue; i++) //initalization of values
            {
                nodes.Add(new Node()
                {
                    val = i
                });
            }

            for (int i = 0; i < nodes.Count; i++) //linking child nodes to parent
            {               
                var rightIndex = (i+1) * 2;
                var leftIndex = rightIndex-1;

                if (leftIndex<nodes.Count)
                {
                    nodes[i].left = nodes[leftIndex];
                }
                if (rightIndex<nodes.Count)
                {
                    nodes[i].right = nodes[rightIndex];
                }              
            }           

            var entryNode = nodes[0];

            #endregion

            LinkRightNeighbour(entryNode);
            Print(entryNode);
            Console.ReadKey();
        }

        private static void Print(Node inputNode)
        {
            //pre-order type of moving through the tree
            if (inputNode == null) return;

            if (inputNode.level == null)
            {
                Console.WriteLine("Node with value " + inputNode.val + " has no neighbour at right");
            }
            else
            {
                Console.WriteLine("Node with value " + inputNode.val + " has at right neighbour with value " + inputNode.level.val);
            }
            Print(inputNode.left);
            Print(inputNode.right);
        }

        public static void LinkRightNeighbour(Node entryNode)
        {
            if (entryNode == null) return;

            Queue<Node> parentNodes = new Queue<Node>();
            Queue<Node> childNodes = new Queue<Node>();
            parentNodes.Enqueue(entryNode);
            Node currNode = null;
            Node prevNode = null;           
            bool notToLink = true;
            bool last = true;
            do
            {
                while (parentNodes.Count != 0)
                {
                    last = parentNodes.Count == 1 ? true : false; // we don't have to link if it is last node at current level
                  
                    currNode = parentNodes.Dequeue();
                    if (currNode.left != null)
                    {
                        childNodes.Enqueue(currNode.left);
                    }
                    if (currNode.right != null)
                    {
                        childNodes.Enqueue(currNode.right);
                    }

                    if (prevNode != null && !notToLink) prevNode.level = currNode;

                    prevNode = currNode;
                    notToLink = last;
                }

                Swap(ref parentNodes, ref childNodes); // when we have linked all nodes at current level (queue with parent nodes is empty),
                                                       //we have to set child nodes queue as parent nodes queue

            } while (parentNodes.Count != 0);
        }

        public static void Swap(ref Queue<Node> first, ref Queue<Node> second)
        {
            Queue<Node> temp = new Queue<Node>();
            temp = first;
            first = second;
            second = temp;
        }
    }
}
