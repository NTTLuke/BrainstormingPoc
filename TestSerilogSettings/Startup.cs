﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(TestSerilogSettings.Startup))]
namespace TestSerilogSettings
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider>(sp =>
            {
                var functionDependencyContext = DependencyContext.Load(typeof(Startup).Assembly);

                var hostConfig = sp.GetRequiredService<IConfiguration>();
                var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(hostConfig, sectionName: "AzureFunctionsJobHost:Serilog", dependencyContext: functionDependencyContext)
                    .CreateLogger();

                return new SerilogLoggerProvider(logger, dispose: true);
            });
        }
    }
}
