﻿using Glimpse;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Glimpse
{
    public class GlimpseServices
    {
        public static IEnumerable<IServiceDescriptor> GetDefaultServices()
        {
            return GetDefaultServices(new Configuration());
        }

        public static IEnumerable<IServiceDescriptor> GetDefaultServices(IConfiguration configuration)
        {
            var describe = new ServiceDescriber(configuration);

            //
            // Discovery & Reflection.
            //
            yield return describe.Transient<ITypeActivator, DefaultTypeActivator>();
            yield return describe.Transient<ITypeSelector, DefaultTypeSelector>();
            yield return describe.Transient<IAssemblyProvider, DefaultAssemblyProvider>();
            yield return describe.Transient<ITypeService, DefaultTypeService>();
            yield return describe.Transient(typeof(IDiscoverableCollection<>), typeof(ReflectionDiscoverableCollection<>));

            //
            // Messages.
            //
            yield return describe.Singleton<IMessageConverter, DefaultMessageConverter>();

            //
            // JSON.Net
            //
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
        }
    }
}