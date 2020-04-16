using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;


namespace PingPractice
{
    public class letsPing
    {
        public long ping(string IPAdress)
        {
            Ping ping = new Ping();
            PingReply theReply = ping.Send(IPAdress);
            Console.WriteLine(theReply.Status.ToString());
            Console.WriteLine(theReply.RoundtripTime + "ms");
            Console.WriteLine("");
            return theReply.RoundtripTime;
        }

        public void timeTestTenSeconds()
        {
            Stopwatch stopWatch = new Stopwatch();
            Console.WriteLine("Delaying for 10 seconds.");
            stopWatch.Start();
            Thread.Sleep(10000);
            stopWatch.Stop();
        }

        //method for lowest ping
        public long lowestPing(long[] roundTripTime)
        {
            long lowestTime = roundTripTime[0];

            for (int i = 0; i < roundTripTime.Length; i++)
            {
                if (lowestTime >= roundTripTime[i])
                {
                    lowestTime = roundTripTime[i];
                }
            }

            return lowestTime;
        }

        //method for highest ping

        public long highestPing(long[] roundTripTime)
        {
            long highestPing = 0;

            for (int i = 0; i < roundTripTime.Length; i++)
            {
                if (highestPing <= roundTripTime[i])
                {
                    highestPing = roundTripTime[i];
                }
            }

            return highestPing;
        }

        //method for average ping

        public long averagePing(long[] roundTripTime)
        {
            long averagePing = 0;

            for (int i = 0; i < roundTripTime.Length; i++)
            {
                averagePing += roundTripTime[i];
            }
            averagePing = averagePing / 6;

            return averagePing;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string IPAdress = "www.google.com";

            letsPing newPing = new letsPing();

            long[] roundTripTime = new long[6]; //currently will only store 6 different round trip times
            Console.WriteLine("This program will ping a selected IP Adress every ten seconds for 1 minute\n Press enter to begin");
            Console.ReadLine();


            int runAmount = 6;
            for(int i = 0; i < runAmount; i++)
            {
                roundTripTime[i] = newPing.ping(IPAdress);
                newPing.timeTestTenSeconds();
            }
            Console.WriteLine("DONE!");
            Console.WriteLine("Every round trip time:");
            //Prints out every Round Trip Time
            for(int i = 0; i < roundTripTime.Length; i++)
            {
                Console.WriteLine(roundTripTime[i]);
            }

            //The rest of this code calculates average, highest, and lowest ping time

            //average
            long averagePing = newPing.averagePing(roundTripTime);

            Console.WriteLine("Average Ping time: " + averagePing);

            //highest
            long highestPing = newPing.highestPing(roundTripTime);

            Console.WriteLine("Highest Ping time: " + highestPing);

            //lowest
            long lowestPing = newPing.lowestPing(roundTripTime);

            Console.WriteLine("Lowest Ping time: " + lowestPing);

            //Writing results to a .txt file
            using (StreamWriter sw = File.CreateText(@"C:\Users\Vipertoo\Desktop\PingFiles\test.txt"))
            {
                sw.WriteLine("Highest Ping Time: " + highestPing);
                sw.WriteLine("Lowest Ping Time: " + lowestPing);
                sw.WriteLine("Average Ping Time: " + averagePing);
            }

            Console.ReadLine();
        }
    }
}
