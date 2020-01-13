using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Reflection;
using System;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using DBserver.Api.DI;
using DBserver.CrossCutting;
using DbServer.Dominio.Extensoes;

namespace Kobold.LastroContratual
{
    public class Startup : IStartup
    {
        
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        protected virtual IContainer InitializeContainer(ContainerBuilder builder, params IModule[] modules)
        {
            var crossCuttingAssembly = typeof(ContaCorrenteModule).GetTypeInfo().Assembly;

            var options = new InitializeOptions(new[] { crossCuttingAssembly }, modules, ContainerBuildOptions.None);
            return InitializeContainer(options, builder);
        }

        public  IContainer InitializeContainer(InitializeOptions options, ContainerBuilder builder)
        {
            RegisterModulesFromAssemblies(options, builder);
            RegisterIndividualModules(options, builder);
            return  builder.Build(options.BuildOptions);
        }

        private static void RegisterModulesFromAssemblies(InitializeOptions options, ContainerBuilder builder)
        {
            if (!options.AssembliesToScan.NuloOuVazio())
                builder.RegisterAssemblyModules(options.AssembliesToScan);
        }

        private static void RegisterIndividualModules(InitializeOptions options, ContainerBuilder builder)
        {
            if (!options.Modules.NuloOuVazio())
                foreach (var module in options.Modules)
                    builder.RegisterModule(module);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
           

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            ConfigureSwagger(services);

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var appContainer = InitializeContainer(builder);

           return appContainer.Resolve<IServiceProvider>();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "DBserver Teste Desenvolvedor .Net",
                        Version = "v1",
                        Description = "teste para desenvolvedor",
                        Contact = new Contact
                        {
                            Name = "Thiago Santos",
                           Email = "thiago.rsantos@ymail.com"
                        }
                    });

               

                c.EnableAnnotations();
               
                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });


        }


        public void Configure(IApplicationBuilder app)
        {
           
            app.UseCors(builder => builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin());
            app.UseMvc();
           
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DBServer");
            });

        }
    }
}
