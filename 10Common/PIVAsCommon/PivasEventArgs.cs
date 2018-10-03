using System;

namespace PIVAsCommon
{
    public class PivasEventArgs<T> : EventArgs
    {
        public PivasEventArgs(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }
        public bool Handled { get; set; }
        public Exception Error { get; set; }

        private bool success;

        public bool Success
        {
            get
            {
                return success = ((Error != null) ? success : false);
            }
            set
            {
                success = value;
            }
        }
    }

    public class PivasEventArgs<K, V> : EventArgs
    {
        public PivasEventArgs(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public K Key { get; set; }

        public V Value { get; set; }

        public bool Handled { get; set; }
        public Exception Error { get; set; }

        private bool success;

        public bool Success
        {
            get
            {
                return success = ((Error != null) ? success : false);
            }
            set
            {
                success = value;
            }
        }
    }
}
