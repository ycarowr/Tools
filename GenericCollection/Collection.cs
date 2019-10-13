using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Tools.GenericCollection
{
    /// <summary> Class that wraps a List and adds some Linq functionality with less garbage generation </summary>
    public partial class Collection<T> where T : class
    {
        public Collection() => Units = new List<T>();

        public Collection(List<T> units)
        {
            Units = new List<T>();
            Add(units);
        }

        public List<T> Units { get; }

        public int Size => Units.Count;

        public override string ToString()
        {
            var s = string.Empty;
            foreach (var c in Units)
                s += c + ", ";
            return s;
        }

        #region Operations

        /// <summary> Clear the list completely.</summary>
        public virtual void Restart() => Units.Clear();

        /// <summary>  Add elements to the collection. </summary>
        public void Add(T unit)
        {
            if (unit == null)
                throw new CollectionArgumentException("Null is not a valid type of unit inside the collection");

            if (!Has(unit))
                Units.Add(unit);
            else
                throw new CollectionArgumentException("Unit already inside the collection");
        }

        /// <summary> Add a group of elements to the list. It falls back to Add(T unit) method. Null raises an Exception.</summary>
        public void Add(List<T> units)
        {
            if (units == null)
                throw new CollectionArgumentException("Null is not a valid type of unit inside the collection");

            for (var i = 0; i < units.Count; i++)
                Add(units[i]);
        }

        /// <summary>  Check whether the collection has an unit inside or not.</summary>
        public bool Has(T unit) => Units.Contains(unit);

        /// <summary> Remove element from the collection and returns whether the element has been successfully removed or not. </summary>
        public bool Remove(T unit) => Units.Remove(unit);

        /// <summary> Shuffles the collection using Fisher Yates algorithm </summary>
        public virtual void Shuffle()
        {
            var n = Size;
            for (var i = 0; i <= n - 2; i++)
            {
                //random index
                var rdn = Random.Range(0, n - i);

                //swap positions
                var curVal = Units[i];
                Units[i] = Units[i + rdn];
                Units[i + rdn] = curVal;
            }
        }

        /// <summary> Get and Remove an element randomly from the collection. </summary>
        public T GetAndRemoveRandom()
        {
            if (Size <= 0)
                return null;

            var rdn = Random.Range(0, Units.Count);
            var unit = Units[rdn];
            Remove(unit);
            return unit;
        }

        /// <summary> Get an element randomly from the collection.</summary>
        public T GetRandom()
        {
            if (Size <= 0)
                return null;

            var rdn = Random.Range(0, Units.Count);
            var unit = Units[rdn];
            return unit;
        }

        /// <summary> Get an element from an specific position. </summary>
        public T Get(int index)
        {
            if (index >= Size || index < 0)
                throw new IndexOutOfRangeException();

            var unit = Units[index];
            return unit;
        }

        /// <summary> Get and Remove an element from an specific position.</summary>
        public T GetAndRemove(int index)
        {
            var unit = Get(index);
            Remove(unit);
            return unit;
        }

        /// <summary> Get and Remove the last element from the collection.</summary>
        public T GetLastAndRemove()
        {
            var lastIndex = Size - 1;
            return GetAndRemove(lastIndex);
        }

        /// <summary>  Clears the list. </summary>
        public void Clear() => Units.Clear();

        #endregion
    }
}