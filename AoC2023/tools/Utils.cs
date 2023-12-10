using System.Runtime;

namespace AoC2023.tools;

public static class Utils
{
    public static long CRT((long rest, long mod)[] funcs)
    {
        for (long i = 0; i < funcs.Length; i++)
        {
            for (long j = i + 1; j < funcs.Length; j++)
            {
                if (GCD(funcs[i].mod, funcs[j].mod) != 1)
                {
                    throw new ArgumentException(
                        $"Parameters are not coprimes: {funcs[i].mod.ToString()}, {funcs[j].mod.ToString()}");
                    //TODO: add processing for valid non-coprimes
                }
            }
        }

        long[] values = new long[funcs.Length];
        long modproduct = 1;
        Array.Fill(values, 1);


        for (long i = 0; i < funcs.Length; i++)
        {
            modproduct *= funcs[i].mod;
            for (long j = 0; j < funcs.Length; j++)
            {
                if (i == j)
                {
                    continue;
                }

                values[i] *= funcs[j].mod;
            }

            if (values[i] % funcs[i].mod == funcs[i].rest)
            {
                continue;
            }

            values[i] *= _modinverse((values[i] % funcs[i].mod, funcs[i].mod)) * funcs[i].rest;
        }

        return values.Sum() % modproduct;

    }

    private static long _modinverse((long rest, long mod) func)
    {
        long x = 0, y = 0;
        if (GCD(func.rest, func.mod, ref x, ref y) != 1)
        {
            throw new ArgumentException("No inverse possible for this function");
        }

        return (x % func.mod + func.mod) % func.mod;
    }

    public static long GCD(long a, long b)
    {
        if (a == 0 || b == 0)
        {
            return 0;
        }
        long _ = 0;
        long __ = 0;
        return GCD(a, b, ref _, ref __);
    }

    public static long GCD(long[] arr)
    {
        if (arr.Length <= 1)
        {
            throw new ArgumentException("Need at least two numbers to calculate GCD");
        }

        if (arr.Contains(0))
        {
            return 0;
        }

        long result = arr[0];
        for (long i = 1; i < arr.Length; i++)
        {
            result = GCD(result, arr[i]);

            if (result == 1)
            {
                return 1;
            }
        }

        return result;
    }

    private static long GCD(long a, long b, ref long x, ref long y)
    {
        if (b == 0)
        {
            x = 1;
            y = 0;
            return a;
        }

        long x1 = 0, y1 = 0;
        long result = GCD(b, a % b, ref x1, ref y1);
        x = y1;
        y = x1 - y1 * (a / b);
        return result;
    }

    public static long LCM(long a, long b)
    {
        if (a == 0 || b == 0)
        {
            throw new ArgumentException("No LCM possible for 0");
        }

        long resa = (long)a * (long)b;
        return (long) (resa / GCD(a,b));
    }

    public static long LCM(int[] arr)
    {
        if (arr.Length <= 1)
        {
            throw new ArgumentException("Need at least two numbers to calculate LCM");
        }

        long result = arr[0];
        for (long i = 1; i < arr.Length; i++)
        {
            result = LCM(result, arr[i]);
        }

        return result;
    }
}