namespace PrimeMonteCarlo
{
    using System;

    internal class Program
    {
        private static void Main()
        {
            const long N = 127; /* проверява се дали даденото число n е просто */
            const int K = 10; /* брой опити на Монте Карло алгоритъма със случайна база a */
            Console.WriteLine("Числото {0} e {1}.", N, IsPrime(N, K) ? "просто" : "съставно");
        }

        /* пресмята a^t mod n; */

        private static long Bigmod(long a, long t, long n)
        {
            return (t == 1) ? (a % n) : (Bigmod(a, t - 1, n) * (a % n)) % n;
        }

        private static bool StrongPrime(long n, long a)
        {
            long s = 1, t = n - 1;

            /* частен случай */
            if (n < 2)
            {
                return false;
            }

            if (n == 2)
            {
                return true;
            }

            /* стъпка 1) */
            while (t % 2 != 1)
            {
                s++;
                t /= 2;
            }

            /* стъпка 2) x = a^t mod n; */
            long x = Bigmod(a, t, n);
            if (x == 1)
            {
                return true;
            }

            /* стъпка 3 */
            for (int i = 0; i < s; i++)
            {
                if (x == n - 1)
                {
                    return true;
                }

                x = (x * x) % n;
            }

            return false;
        }

        private static bool IsPrime(long n, int k)
        {
            var rand = new Random();
            for (int i = 0; i < k; i++)
            {
                long a = 2 + (NextLong(rand) % n) - 3;
                if (!StrongPrime(n, a))
                {
                    return false;
                }
            }

            return true;
        }

        private static long NextLong(Random rand)
        {
            return ((long)rand.Next() << 32) | rand.Next();
        }
    }
}