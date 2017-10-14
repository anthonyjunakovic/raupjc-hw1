using System;

namespace Assignment1
{
    public class IntegerList : IIntegerList
    {
        private const int _initialSize = 4;

        private int[] _internalStorage;
        private int _emptyItemPointer;

        public IntegerList() : this(_initialSize) { }

        public IntegerList(int sizeCount)
        {
            if (sizeCount <= 0)
            {
                throw new IndexOutOfRangeException();
            }
            _internalStorage = new int[sizeCount];
            _emptyItemPointer = 0;
        }
        public void Add(int item)
        {
            if (_emptyItemPointer >= _internalStorage.Length)
            {
                Array.Resize<int>(ref _internalStorage, _internalStorage.Length * 2);
            }
            _internalStorage[_emptyItemPointer++] = item;
        }

        public bool Remove(int item)
        {
            for (int i = 0; i < _emptyItemPointer; i++)
            {
                if (_internalStorage[i] == item)
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

        public int GetElement(int index)
        {
            if ((index < 0) || (index >= _emptyItemPointer))
            {
                throw new IndexOutOfRangeException();
            }
            return _internalStorage[index];
        }

        public int IndexOf(int item)
        {
            for (int i = 0; i < _emptyItemPointer; i++)
            {
                if (_internalStorage[i] == item)
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

        public bool Contains(int item)
        {
            for (int i = 0; i < _emptyItemPointer; i++)
            {
                if (_internalStorage[i] == item)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
