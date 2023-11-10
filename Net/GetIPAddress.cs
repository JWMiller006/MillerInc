using MillerInc.Convert.Files;
using MillerInc.Convert.Lists;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using MillerInc.UI;
using System.Linq;

namespace MillerInc.Net
{
    public class GetIPAddress
    {
        public static string GetIP()
        {
            Console.WriteLine("Enter Compter Name");
            string hostName = Console.ReadLine();
            string file = FileUI.GetFilePathUI();
            string IPAdd;
            try
            {
                IPAdd = ListToString.ListString<string>(FileToList.FileRead(file), "", "");
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Computer IP Not Found");
                IPAdd = "10.0.0.255";
            }
            Console.WriteLine("Starting to ping at: " + IPAdd);
            if (CheckIP(hostName, IPAdd))
            {
                try
                {
                    List<IPAddress> dns = Dns.GetHostAddresses(hostName).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine(e.Message);
                    IPAdd = hostName;
                }
            }
            else GetIP();
            return IPAdd;
        }
        // Tests Legitamacy of the Connection and Validates IP
        public static bool CheckIP(string hostName, string IPAdd = "10.10.10.0")
        {
            Console.WriteLine("\n\nPinging for " + hostName + " at " + IPAdd);
            Ping p1 = new();
            PingReply PR = p1.Send(IPAdd);
            try
            {
                p1.Send(hostName);
            }
            catch (PingException)
            {
                Console.WriteLine("Hostname Not Found");
            }
            // check when the ping is not success
            while (!PR.Status.ToString().Equals("Success"))
            {
                Console.WriteLine(PR.Status.ToString());
                PR = p1.Send(IPAdd);
                if (!PR.Status.ToString().Equals("Success"))
                {
                    try
                    {
                        PR = p1.Send(hostName);
                    }
                    catch (PingException)
                    {
                        Console.WriteLine("Hostname Not Found");
                    }
                }
            }
            // check after the ping is n success
            int count = 0;
            while (PR.Status.ToString().Equals("Success") && count != 5)
            {
                //Console.WriteLine(PR.Status.ToString());
                PR = p1.Send(IPAdd);
                if (!PR.Status.ToString().Equals("Success"))
                {
                    try
                    {
                        PR = p1.Send(hostName);
                    }
                    catch (PingException)
                    {
                        Console.WriteLine("Hostname Not Found");
                    }
                }
                if (count == 4)
                {
                    if (PR.Status.ToString().Equals("Success"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                count++;
            }
            return false;
        }
    }
}
