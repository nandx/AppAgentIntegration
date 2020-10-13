using AppAgentIntegration.Service;

namespace AppAgentIntegration
{
    internal class Program
    {
       static void Main(string[] args)
       {
           AgentNewService service = new AgentNewService();
           service.ProcessData();
       }

    }
}