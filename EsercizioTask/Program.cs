using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EsercizioTask
{
    class Program
    {
        static void Main(string[] args)
        {
            //2 Modi per avviare i Task
            //Modo rapido
            //Task.Run(() => StampaNumeri());
            //Task.Run(() => StampaNumeri());

            //Secondo metodo
            Task<int> t1 = Task.Factory.StartNew(() => StampaNumeri(true),
                CancellationToken.None, //Se posso interrompere il Task
                TaskCreationOptions.LongRunning, //Non permermette di avviare Task figli
                TaskScheduler.Default // Scelgo come devono essere gestiti i Task
                );

            Task<int> t2 = Task.Factory.StartNew(() => StampaNumeri(false));

            Console.WriteLine($"Totale numeri pari: {t1.Result}");
            Console.WriteLine($"Totale numeri dispari: {t2.Result}");

            Console.ReadLine();
        }
        private static int StampaNumeri(bool isPari)
        {
            int totaleNumeri = 0;
            Console.WriteLine($"Il Task corrente usa i ThreadPool: {Thread.CurrentThread.IsThreadPoolThread}");
            if (isPari)
            {
                for (int i = 0; i < 100; i = i + 2)
                {
                    Console.WriteLine($"Task n°{Task.CurrentId}: {i}");
                    totaleNumeri++;
                }
            }
            else
            {
                for (int i = 1; i < 100; i = i + 2)
                {
                    Console.WriteLine($"Task n°{Task.CurrentId}: {i}");
                    totaleNumeri++;
                }
            }
            Thread.Sleep(5000);
            return totaleNumeri;

        }
    }
}