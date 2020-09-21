using System;
using System.Linq;
using System.Reflection;
using Autofac;
using DemoApp.Repositories;
using DemoApp.ViewModels;
using Xamarin.Forms;

namespace DemoApp
{
    public abstract class BootstrapperBase
    {
        protected ContainerBuilder containerBuilder { get; private set;}

        public BootstrapperBase()
        {
            Initialize();
            FinishInitialization();
        }

        protected virtual void Initialize()
        {
            // Register views and viewmodels
            var currentAssembly = Assembly.GetExecutingAssembly();
            containerBuilder = new ContainerBuilder();

            foreach (var type in currentAssembly.DefinedTypes
                      .Where(e =>
                             e.IsSubclassOf(typeof(Page)) ||
                             e.IsSubclassOf(typeof(BaseViewModel))))
            {
                containerBuilder.RegisterType(type.AsType());
            }

            // register repository
            containerBuilder.RegisterType<TodoItemRepository>().SingleInstance();
        }

        private void FinishInitialization()
        {
            var container = containerBuilder.Build();
            Resolver.Initialize(container);
        }
    }
}
