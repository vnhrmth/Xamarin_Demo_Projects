using System;
namespace DemoApp.Droid
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
