using System;
using System.Collections.Generic;
// C# 对象池相关讨论
// https://stackoverflow.com/questions/2510975/c-sharp-object-pooling-pattern-implementation
// .net core ArrayPool<T>的相关讨论
// https://github.com/dotnet/corefx/issues/4547

namespace Arrow.Base
{
    public class ObjectPool<T> where T : IPoolObject
    {
        private Queue<T> Pool = new Queue<T>();
        private Func<T> Factory;
        private Action Deposer;
        private int minExtendLength;
        public ObjectPool(int minLength, int minExtendLength, Func<T> factory, Action deposer)
        {
            this.Factory =factory;
            this.Deposer = deposer;
            this.minExtendLength = minExtendLength;

            for (var i = 0; i < minLength; ++i)
            {
                this.Pool.Enqueue(this.Factory());
            }
        }

        public T Rent()
        {
            if (this.Pool.Count <= 0)
            {
                for (var i = 0; i < this.minExtendLength; ++i)
                    this.Pool.Enqueue(this.Factory());
            }

            var t = this.Pool.Dequeue();
            t.Init();

            return t;
        }

        public void Return(T t)
        {
            t.Release();
            this.Pool.Enqueue(t);
        }
    }
}