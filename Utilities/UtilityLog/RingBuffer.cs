//********************************************************
//建立日期:2015.11.25
//作者:litao
//內容说明:　主要实现类RingBuffer，是个环形缓冲区
//修改日期：
//作者:
//內容说明:
//********************************************************

using System;
using System.Runtime.ConstrainedExecution;

namespace BinHong.Utilities
{
    /// <summary>
    /// 环形缓冲区
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RingBuffer<T>
    {
        private readonly int _capacity;
        private readonly T[] _data;
        private int _readIndex;
        private int _writeIndex;

        /// <summary>
        /// 环形缓冲区变化的事件
        /// </summary>
        public EventHandler<RingBufferEventArgs<T>> Changed;

        public int Free
        {
            get { return ((((this._readIndex - this._writeIndex) - 1) + this._capacity) % this._capacity); }
        }

        public bool IsEmpty
        {
            get { return (this._readIndex == this._writeIndex); }
        }

        public bool IsFull
        {
            get
            {
                return ((((this._writeIndex - this._readIndex) + this._capacity) % this._capacity) == (this._capacity - 1));
            }
        }

        public RingBuffer()
            : this(10)
        {
        }

        public RingBuffer(int capacity)
        {
            this._capacity = 0;
            this._capacity = capacity + 1;
            this._data = new T[this._capacity];
        }

        /// <summary>
        /// Change事件响应方法
        /// </summary>
        /// <param name="e"></param>
        private void OnChanged(RingBufferEventArgs<T> e)
        {
            if (this.Changed!=null)
            {
                this.Changed(null,e);
            }
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public void Clear()
        {
            this._readIndex = this._writeIndex;
            OnChanged(new RingBufferEventArgs<T>(default(T),RingBufferChangeType.Clear));
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public bool Read(out T value)
        {
            value = default(T);
            if (this.IsEmpty)
            {
                return false;
            }
            value = this._data[this._readIndex];
            this._readIndex = (this._readIndex + 1) % this._capacity;

            OnChanged(new RingBufferEventArgs<T>(value, RingBufferChangeType.Read));
            return true;
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        public bool Write(T value)
        {
            if (this.IsFull)
            {
                return false;
            }
            this._data[this._writeIndex] = value;
            this._writeIndex = (this._writeIndex + 1) % this._capacity;

            OnChanged(new RingBufferEventArgs<T>(value, RingBufferChangeType.Write));
            return true;
        }
    }

    /// <summary>
    /// 环形缓冲区事件参数
    /// </summary>
    public class RingBufferEventArgs<T> : EventArgs
    {
        public RingBufferChangeType ChangeType;
        public T Item;

        public RingBufferEventArgs(T t,RingBufferChangeType type)
        {
            ChangeType = type;
            Item = t;
        }
    }

    /// <summary>
    /// 环形缓冲区变化的类型
    /// </summary>
    public enum RingBufferChangeType
    {
        Write,
        Read,
        Clear
    }
}
