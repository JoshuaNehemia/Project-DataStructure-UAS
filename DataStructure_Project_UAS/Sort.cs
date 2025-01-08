using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using static DataStructure_Project_UAS.SinglyLinkedList;

namespace DataStructure_Project_UAS
{
    public class Sort
    {
        private Sort() { }

        public static int[] BubbleSort(int[] c)
        {
            int temp;

            for (int i = 0; i < c.Length - 1; i++)
            {
                for (int j = 0; j < c.Length - 1; j++)
                {
                    if (c[j] > c[j + 1])
                    {
                        temp = c[j];
                        c[j] = c[j + 1];
                        c[j + 1] = temp;
                    }
                }
            }

            return c;
        }

        public static int[] HeapSort(int[] c)
        {
            Heapify(c);
            int[] result = new int[c.Length];

            for (int i = 0; i < c.Length; i++)
            {
                result[i] = c[0];
                c[0] = int.MaxValue;
                PushDown(c, 0);
            }
            return result;

            void Heapify(int[] dataset)
            {
                for (int i = 0; i < dataset.Length; i++)
                {
                    PushUp(dataset, i);
                }
            }

            void PushUp(int[] dataset, int index)
            {
                if (index == 0)
                    return;
                int parent_index = (index-1) / 2;

                if (dataset[parent_index] > dataset[index])
                {
                    int buffer = dataset[parent_index];
                    dataset[parent_index] = dataset[index];
                    dataset[index] = buffer;
                }
                PushUp(dataset, parent_index);

            }

            void PushDown(int[] dataset, int index)
            {
                int lchild = index * 2 + 1;
                int rchild = index * 2 + 2;
                int smallest_index = lchild;
                if (lchild > dataset.Length - 1)
                {
                    return;
                }
                if (rchild < (dataset.Length) && dataset[rchild] < dataset[smallest_index])
                {
                    smallest_index = rchild;
                }
                if (dataset[smallest_index] < dataset[index])
                {
                    int buffer = dataset[smallest_index];
                    dataset[smallest_index] = dataset[index];
                    dataset[index] = buffer;
                }
                PushDown(dataset, smallest_index);
            }
        }
        public static int[] InsertionSort(int[] c)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            int size = c.Length;
            int[] result = new int[size];
            Node head = new Node(c[0], null);
            SinglyLinkedList list = new SinglyLinkedList(null);
            Node tempBuf;
            for (int i = 0; i < size; i++)
            {
                tempBuf = new Node(c[i], null);
                if (i == 0)
                {
                    list.Head = tempBuf;
                }
                else
                {
                    Node helper = list.Head;
                    if (list.Head.Key > tempBuf.Key)
                    {
                        tempBuf.Next = list.Head;
                        Node buffer = tempBuf;
                        tempBuf = list.Head;
                        list.Head = buffer;
                    }
                    else
                    {
                        CheckNextNode(ref tempBuf, helper);
                    }
                }
            }

            return TranslateToArray(list, size);
        }

        public static int[] RadixSort(int[] c)
        {
            int[] result = new int[c.Length];
            result = c;

            int maxNumber = result.Max();
            int maxDigits = maxNumber.ToString().Length;

            List<int>[] radix = new List<int>[10];
            for(int i=0;i<10;i++)
            {
                radix[i] = new List<int>();
            }

            int digitPlace = 1;
            for(int i=0; i<maxDigits; i++)
            {
                for (int j = 0; j < result.Length; j++)
                {
                    int digit = (result[j] / digitPlace) % 10;
                    radix[digit].Add(result[j]);
                }
                int index = 0;
                for (int j = 0; j < 10; j++)
                {
                    foreach (int num in radix[j])
                    {
                        result[index] = num;
                        index++;
                    }
                }
                for (int j = 0; j < 10; j++)
                {
                    radix[j].Clear();
                }
                digitPlace *= 10;
            }

            return result;
        }
        public static int[] MergeSort(int[] c)
        {
            Sort(c, 0, c.Length - 1);
            return c;

            void Merge(int[] arr, int l, int m, int r)
            {
                int t1 = m - l + 1;
                int t2 = r - m;

                int[] tempL = new int[t1];
                int[] tempR = new int[t2];

                for (int i = 0; i < t1; i++) tempL[i] = arr[l + i];
                for (int j = 0; j < t2; j++) tempR[j] = arr[m + 1 + j];

                int iIndex = 0, jIndex = 0;

                int k = l;
                while (iIndex < t1 && jIndex < t2)
                {
                    if (tempL[iIndex] <= tempR[jIndex])
                    {
                        arr[k] = tempL[iIndex];
                        iIndex++;
                    }
                    else
                    {
                        arr[k] = tempR[jIndex];
                        jIndex++;
                    }
                    k++;
                }

                while (iIndex < t1)
                {
                    arr[k] = tempL[iIndex];
                    iIndex++;
                    k++;
                }

                while (jIndex < t2)
                {
                    arr[k] = tempR[jIndex];
                    jIndex++;
                    k++;
                }
            }

            void Sort(int[] arr, int l, int r)
            {
                if (l < r)
                {
                    int m = l + (r - l) / 2;

                    Sort(arr, l, m);
                    Sort(arr, m + 1, r);

                    Merge(arr, l, m, r);
                }
            }
        }
    }
}
