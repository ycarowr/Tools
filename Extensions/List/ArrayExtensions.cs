using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace YWR.Tools
{
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        ///     Returns a random item from inside the array.
        /// </summary>
        public static T RandomItem<T>(this T[] array)
        {
            if (array.Length == 0)
            {
                throw new IndexOutOfRangeException("Array is Empty");
            }

            int randomIndex = Random.Range(0, array.Length);
            return array[randomIndex];
        }

        /// <summary>
        ///     Shuffles the List using Fisher Yates algorithm: https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle.
        /// </summary>
        public static void Shuffle<T>(this T[] array)
        {
            int n = array.Length;
            for (int i = 0; i <= n - 2; i++)
            {
                //random index
                int rdn = Random.Range(0, n - i);

                //swap positions
                T curVal = array[i];
                array[i] = array[i + rdn];
                array[i + rdn] = curVal;
            }
        }

        /// <summary>
        ///     Prints the list in the following format: [item[0], item[1], ... , item[Count-1]]
        /// </summary>
        public static void Print<T>(this T[] array, string log = "")
        {
            log += "[";
            for (int i = 0; i < array.Length; i++)
            {
                log += array[i].ToString();
                log += i != array.Length - 1 ? ", " : "]";
            }

            Debug.Log(log);
        }

        public static T[] Append<T>(this T[] array, T[] other)
        {
            return ArrayHelper.Append(ref array, other);
        }
    }

    public static class ArrayHelper
    {
        public static T[] Append<T>(ref T[] array, T[] other)
        {
            int size = array.Length + other.Length;
            T[] merge = new T[size];
            int count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                merge[count] = array[i];
                count++;
            }

            for (int i = 0; i < other.Length; i++)
            {
                merge[count] = other[i];
                count++;
            }

            return merge;
        }
    }
}