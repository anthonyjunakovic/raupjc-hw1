using System;
using System.Collections;
using System.Collections.Generic;

namespace Assignment3
{
    class GenericListEnumerator<T> : IEnumerator<T>
    {
        private GenericList<T> _genericList;
        private int _index;

        public T Current
        {
            get
            {
                return _genericList.GetElement(_index);
            }
        }

        public GenericListEnumerator(GenericList<T> genericList)
        {
            _genericList = genericList;
            _index = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Dispose() { } // Not a native object, nothing to dispose

        public bool MoveNext()
        {
            if ((++_index) >= _genericList.Count)
            {
                return false;
            }
            return true;
        }

        public void Reset()
        {
            _index = -1;
        }
    }
}
