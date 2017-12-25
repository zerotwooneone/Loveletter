using System;
using Unity;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var container = new Unity.UnityContainer();

            var program = new Program();
            program.Setup(container);

            var application = container.Resolve<Application.ApplicationViewModel>();

            program.Configure(application);

            application.Start();

            while (application.DoContinue().Result)
            {

            }
        }

        private void Configure(Application.ApplicationViewModel applicationViewModel)
        {
            //nothing yet
        }

        private void Setup(IUnityContainer container)
        {
            container.RegisterSingleton<Application.ApplicationViewModel>();
        }
    }
}
