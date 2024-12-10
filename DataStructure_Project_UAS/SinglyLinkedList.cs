using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure_Project_UAS
{
    public class SinglyLinkedList
    {
        public class Node
        {
            #region DATA MEMBER
            private int key;
            private Node next;
            #endregion

            #region CONSTRUCTOR
            public Node(int input_key, Node input_next)
            {
                this.Key = input_key;
                this.Next = input_next;
            }
            #endregion

            #region PROPERTIES
            public int Key
            {
                get => key;
                set
                {
                    if (value < 0)
                    {
                        throw new Exception("Input Can't Be Negative");
                    }
                    else
                    {
                        key = value;
                    }
                }
            }
            public Node Next { get => next; set => next = value; }
            #endregion
        }

        #region DATA MEMBER
        private Node head;
        #endregion

        #region CONSTRUCTOR
        public SinglyLinkedList(Node input_head)
        {
            this.Head = input_head;
        }
        #endregion

        #region PROPERTIES
        public Node Head { get => head; set => head = value; }
        #endregion

        #region METHOD
        public static void CheckNextNode(ref Node tempBufs, Node helper)
        {

            while (helper.Next != null)
            {
                if (helper.Next.Key > tempBufs.Key)
                {
                    tempBufs.Next = helper.Next;
                    helper.Next = tempBufs;
                    return;
                }
                helper = helper.Next;
            }
            helper.Next = tempBufs;
        }

        public static int[] TranslateToArray(SinglyLinkedList list, int size)
        {
            int[] result = new int[size];
            Node transfer = list.Head;
            int counter = 0;
            while (transfer.Next != null)
            {
                result[counter] = transfer.Key;
                counter++;
                transfer = transfer.Next;
            }
            result[counter] = transfer.Key;
            return result;
        }
        #endregion
    }
}
