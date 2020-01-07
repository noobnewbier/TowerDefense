using System.Collections.Generic;
using UnityEngine;

namespace Experimental
{
    public class RuntimeSet<T> : ScriptableObject
    {
        private List<T> _items;
        public List<T> Items => _items ?? (_items = new List<T>());
    }
}