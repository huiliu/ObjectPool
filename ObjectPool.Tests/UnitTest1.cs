#if false
using System;
using System.Collections.Generic;
using Xunit;
using Arrow.Base;
// c# xTest测试基本操作
// https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test

namespace Arrow.Base.Tests
{
    class ABC
        : IPoolObject
    {
        public void Init()
        {
            Console.WriteLine("Initial ABC!");
        }

        public void Release()
        {
            Console.WriteLine("Release ABC!");
        }
    }
    public class UnitTest1
    {
        ABC Factory()
        {
            return new ABC();
        }

        [Fact]
        public void Test1()
        {
            var pool = new ObjectPool<ABC>(8, 4, Factory);
            List<ABC> xx = new List<ABC>(10);
            for (var i = 0; i < 10; ++i)
                xx.Add(pool.Rent());

            foreach (var item in xx)
                pool.Return(item);
        }
    }
}
#endif
