using BankManager.Client.HttpHandlers;
using BankManager.Client.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;

namespace BankManager.Client
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
            services.AddControllersWithViews();

            services.AddScoped<IBankApiService, BankApiService>();
            services.AddTransient<AuthenticationDelegatingHandler>();

            services.AddAuthentication(configure =>
                    {
                        configure.DefaultScheme          = CookieAuthenticationDefaults.AuthenticationScheme;
                        configure.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    })
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, configure =>
                    {
                        configure.Authority                     = "https://localhost:1000";
                        configure.ClientId                      = "BankManager";
                        configure.ClientSecret                  = "bankmanager";
                        configure.ResponseType                  = "code id_token";
                        configure.GetClaimsFromUserInfoEndpoint = true;
                        configure.SaveTokens                    = true;
                        //configure.SignInScheme                = CookieAuthenticationDefaults.AuthenticationScheme;

                        configure.Scope.Add("BankA.Write");
                        configure.Scope.Add("BankA.Read");
                        configure.Scope.Add("BankB.Write");
                        configure.Scope.Add("BankB.Read");
                    });

            services.AddHttpClient("BankA", configure =>
                    {
                        configure.BaseAddress = new Uri(Configuration.GetSection("BankSettings:BaseAdresses").Get<string[][]>()[0][0]);
                        configure.DefaultRequestHeaders.Clear();
                        configure.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                    })
                    .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            services.AddHttpClient("BankB", configure =>
                    {
                        configure.BaseAddress = new Uri(Configuration.GetSection("BankSettings:BaseAdresses").Get<string[][]>()[1][0]);
                        configure.DefaultRequestHeaders.Clear();
                        configure.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                    })
                    .AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            services.AddHttpClient("BankManager", configure =>
                    {
                        configure.BaseAddress = new Uri(Configuration["ApiGatewaySettings"]);
                        configure.DefaultRequestHeaders.Clear();
                        configure.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                    }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
