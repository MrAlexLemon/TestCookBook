using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Entities
{
    public class TestClass : IComparable
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }

        public TestClass(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        public int CompareTo(object obj)
        {
            var qq = obj.GetType().Name;
            TestClass p = obj as TestClass;
            if (p is null)
                throw new Exception("Can't compare two objects.");

            int result = this.A.CompareTo(p.A);


            if (result > 0)
                return 1;
            else if (result < 0)
                return -1;


            if (result == 0)
                result = this.B.CompareTo(p.B);

            if (result > 0)
                return 1;
            else if (result < 0)
                return -1;

            if (result == 0)
                result = this.C.CompareTo(p.C);

            if (result > 0)
                return 1;
            else if (result < 0)
                return -1;

            return result;
        }
    }
}
