using System;
using AppAgentIntegration.Service;

namespace AppAgentIntegration
{
    internal class Program
    {
       static void Main(string[] args)
       {
           Console.WriteLine("AppAgentIntegration -- START " + DateTime.Now);

           var servicenew = new AgentNewService();
           servicenew.ProcessData();
           Console.WriteLine("AgentNewService -- END " + DateTime.Now);

           var serviceupdate = new AgentUpdateService();
           serviceupdate.ProcessUpdate();
           Console.WriteLine("AgentUpdateService -- END " + DateTime.Now);


           var serviceDelete = new AgentDeleteService();
           serviceDelete.ProcessDelete();
           Console.WriteLine("AppAgentIntegration -- END " + DateTime.Now);
       }

    }
}