using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace DirectoryWebService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration)
        {
            // passed by appsettings.json
            // passed by command line: dotnet run --Directory "/home/user/"
            var directory = configuration["Directory"];
            Console.WriteLine("Directory: " + directory);

            var provider = new FileExtensionContentTypeProvider
            {
                // add files
                Mappings =
                {
                    [".log"] = "text/plain",
                }
            };

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(directory),
                StaticFileOptions = { ContentTypeProvider = provider },
                EnableDirectoryBrowsing = true,
            });
        }
    }
}
