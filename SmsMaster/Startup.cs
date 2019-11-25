using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmsMaster.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SmsMaster.AutoMapper;
using SmsMaster.Data.Interfaces;
using SmsMaster.Business.Interfaces;
using SmsMaster.Business;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Swashbuckle.AspNetCore.Swagger;

namespace SmsMaster
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<SmsMasterContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddTransient<ICountriesRepository, CountriesRepository>();
            services.AddTransient<ISmsRepository, SmsRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ISmsServiceAdapter>(provider =>
            {
                string smsServiceUrl = Configuration.GetSection("SmsServiceUrl").Value;
                string sendSmsUrl = Configuration.GetSection("SendSmsUrl").Value;
                return new SmsServiceAdapter(smsServiceUrl, sendSmsUrl);
            }
            );


            services.AddTransient<ICountriesBusiness, CountriesBusiness>();
            services.AddTransient<ISmsBusiness, SmsBusiness>();
            services.AddTransient<IStatisticsBusiness, StatisticsBusiness>();

            services.AddMvc(options =>
                {
                    // requires using Microsoft.AspNetCore.Mvc.Formatters;
                    //options.OutputFormatters.RemoveType<StringOutputFormatter>();
                    //options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                })
                .AddXmlSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "SMS API",
                    Description = "An API with sms related operations.",
                    Version = "v1"
                });
                options.DescribeAllEnumsAsStrings();

                //var xmlDocPath = PlatformServices.Default.Application.ApplicationBasePath;
                //options.IncludeXmlComments(Path.Combine(xmlDocPath, "CBH.Form.Services.xml"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //automatic migrations are only for demo 
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<SmsMasterContext>().Database.Migrate();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "help";
            });
        }
    }
}
