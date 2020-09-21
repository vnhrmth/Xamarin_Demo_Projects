﻿using System;
using Autofac;

namespace DemoApp
{
    public static class Resolver
    {
        private static IContainer _container;
        public static void Initialize(IContainer container)
        {
            Resolver._container = container;
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
