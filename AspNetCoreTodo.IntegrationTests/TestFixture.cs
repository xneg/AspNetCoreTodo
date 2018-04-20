using System;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;

namespace AspNetCoreTodo.IntegrationTests
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _server;

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<AspNetCoreTodo.Startup>()
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\AspNetCoreTodo"));
                    configBuilder.AddJsonFile("appsettings.json");

                    configBuilder.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["Facebook:AppId"] = "fake-app-id",
                        ["Facebook:AppSecret"] = "fake-app-secret"
                    });
                });

            _server = new TestServer(builder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("https://localhost:5000");
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}