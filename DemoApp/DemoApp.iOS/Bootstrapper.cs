using System;
namespace DemoApp.iOS
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
        }

        public static void Init()
        {
            var bootstrapper = new Bootstrapper();
        }
    }
}
