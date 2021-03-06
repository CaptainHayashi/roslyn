﻿using System;
using System.Concepts.Prelude;
using System.Concepts.Numerics;
using System.Numerics;

namespace OperatorOverloads
{
    // # Operator Overloads
    //
    // This testbed shows the use of operator-overloaded generic polynomials.
    // Here, we're using the Num<A> concept from System.Concepts.Prelude.

    class Program
    {
        static A M<A, implicit NumA>(A x) where NumA : Num<A> => FromInteger(666) * x * x * x + FromInteger(777) * x * x + FromInteger(888);
        static A N<A, implicit NumA>(A x) where NumA : Num<A>
        {
            var y = FromInteger(0);
            for (int i = 0; i < 100; ++i)
                y = FromInteger(666) * x * x * x + FromInteger(777) * x * x + FromInteger(888);
            return y;
        }

        static void Main(string[] args)
        {
            // These two lines are here to trip the debugger without running
            // the testbed through VS, so that we can step through and
            // disassemble the JIT emitted code for the polynomials.

            System.Diagnostics.Debugger.Launch();
            System.Diagnostics.Debugger.Break();

            Console.WriteLine(M(16)); // int
            Console.WriteLine(M(16.0)); // double
            Console.WriteLine(M(new Vector2(16, 8)));

            Console.WriteLine(N(16)); // int
            Console.WriteLine(N(16.0)); // double
            Console.WriteLine(N(new Vector2(16, 8)));

            var v = new Vector2(16, 8);
            // Should pick up original vector multipliers
            var m = new Vector2(666) * v * v * v + new Vector2(777) * v * v + new Vector2(888);
            Console.WriteLine(m);
        }
    }
}

