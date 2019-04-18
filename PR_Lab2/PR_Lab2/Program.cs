using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PR_Lab2
{
    class Program
    {
        static CountdownEvent count1 = new CountdownEvent(3);
        static CountdownEvent count2 = new CountdownEvent(3);

        static void DoSomething(object id)
        {
            switch((int)id)
            {
                case 1:
                    count1.Signal();
                    break;
                case 2:
                case 3:
                    count1.Signal();
                    count2.Signal();
                    break;
                case 4:
                    count2.Signal();
                    break;
                case 5:
                    count1.Wait();
                    break;
                case 6:
                    count2.Wait();
                    break;
            }
            Console.WriteLine(id + " says hi");
        }

        static void Main(string[] args)
        {
            for (int i=0; i<6; i++)
            {
                new Thread(DoSomething).Start(i+1);   
                
            }

            Console.Read();
        }
    }
}
