using System;

namespace Assignment2
{
    public class GenericList<X> : IGenericList<X>
    {
        private const int _initialSize = 4;

        private X[] _internalStorage;
        private int _emptyItemPointer;

        public GenericList() : this(_initialSize) { }

        public GenericList(int sizeCount)
        {
            if (sizeCount <= 0)
            {
                throw new IndexOutOfRangeException();
            }
            _internalStorage = new X[sizeCount];
            _emptyItemPointer = 0;
        }
        public void Add(X item)
        {
            if (_emptyItemPointer >= _internalStorage.Length)
            {
                Array.Resize<X>(ref _internalStorage, _internalStorage.Length * 2);
            }
            _internalStorage[_emptyItemPointer++] = item;
        }

        public bool Remove(X item)
        {
            for (int i = 0; i < _emptyItemPointer; i++)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return RemoveAt(i);
                }
            }
            return false;
        }

        public bool RemoveAt(int index)
        {
            if ((index < 0) || (index >= _emptyItemPointer))
            {
                throw new IndexOutOfRangeException();
            }
            Array.Copy(_internalStorage, index + 1, _internalStorage, index, --_emptyItemPointer - index);
            return true;
        }

        public X GetElement(int index)
        {
            if ((index < 0) || (index >= _emptyItemPointer))
            {
                throw new IndexOutOfRangeException();
            }
            return _internalStorage[index];
        }

        public int IndexOf(X item)
        {
            for (int i = 0; i < _emptyItemPointer; i++)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return i;
                }
            }
            // Exception could be thrown instead, but the usual thing in .NET is to return
            // an invalid index if an item is not found in an array
            return -1;
        }

        public int Count
        {
            get
            {
                return _emptyItemPointer;
            }
        }
        public void Clear()
        {
            _emptyItemPointer = 0;
        }

        public bool Contains(X item)
        {
            for (int i = 0; i < _emptyItemPointer; i++)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
