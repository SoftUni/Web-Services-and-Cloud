namespace WcfClientSumator
{
    using System;

    using WcfClientSumator.SumatorServiceReference;

    class SumatorClientConsoleApp
    {
        static void Main()
        {
            var client = new ServiceSumatorClient();
            long sum = client.Sum(3, 2);
            Console.WriteLine("Result from the service: {0}", sum);
        }
    }
}
