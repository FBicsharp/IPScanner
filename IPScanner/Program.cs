using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace IPScanner
{
    class Program
    {

        

        static void Main(string[] args)
        {
            string subnet = "192.168.1";
            

            Task.Factory.StartNew(new Action(() =>
            {
                for (int i = 2; i < 255; i++)
                {
                    string ip = $"{subnet}.{i}";
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(ip, 100);
                    
                        var stato = reply.Status == IPStatus.Success ? "OCCUPATO" : "LIBERO";
                        Console.WriteLine($"Indirizzo \tIP:{ip}\tSTATO:{stato} ");
                    
                }
            }));
            Console.ReadKey();
        }



        public static void CheckIPByPing(string subnet, ushort fromNum, ushort toNum, IPstatus typeSearch)
        {
            var listIp = new List<IpInformation>();
            

            Task.Factory.StartNew(new Action(() =>
            {
                for (int i = fromNum; i < toNum; i++)
                {
                    string ip = $"{subnet}.{i}";
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(ip, 50);
                    listIp.Add(
                        new IpInformation()
                        {
                            Ip = ip,
                            Stato = reply.Status == IPStatus.Success ? IPstatus.NonDisponibile : IPstatus.NonDisponibile
                        }
                    );
                }
            }));

            listIp.ForEach((el) => Console.WriteLine(el.ToString()));
            

        }


    }
    public class IpInformation
    {
        public string Ip { get; set; }
        public IPstatus Stato { get; set; }

        public override string ToString()
        {
            return $"\tIP:{Ip}\tSTATO:{nameof(Stato)} ";
        }
    }

    public enum IPstatus
    {
        Disponibile,
        NonDisponibile,
        Entrambi
    }
}
