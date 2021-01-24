using LightInject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Npgsql;
using Sbran.CQS.Registries;
using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Registries;
using System;
using System.Text;


namespace Sbran.WebApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			// Set to false. This will be the default in v5.x and going forward.
			//_container.Options.ResolveUnregisteredConcreteTypes = false;

			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
            var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            services.AddMvc(option =>
            {
                option.FormatterMappings.SetMediaTypeMappingForFormat("octet-stream", MediaTypeHeaderValue.Parse("application/octet-stream"));
            }).AddControllersAsServices();

            var builder = new NpgsqlConnectionStringBuilder
            {
                Username = "postgres",
                Password = "47H8Ms5a",
                Host = "localhost",
                Port = 5432,
                Database = "postgres",
                IntegratedSecurity = true,
                Pooling = true
            };

            var connectionString = Configuration.GetConnectionString("PostgreSQLConnection");

            services
                // .AddEntityFrameworkNpgsql() Depricated
                .AddDbContext<DomainContext>(options => options.UseNpgsql(connectionString)/*, contextLifetime: ServiceLifetime.Transient, optionsLifetime: ServiceLifetime.Transient*/)
                .AddDbContext<SystemContext>(options => options.UseNpgsql(connectionString/*, builder => builder.MigrationsAssembly("ICS.Domain")*/));

            /*//services.AddEntityFrameworkProxies();
             * services.AddEntityFrameworkNpgsql()
                .AddDbContext<DomainContext>(opt =>
                {
                    opt.UseNpgsql(connectionString);
                    //opt.UseLazyLoadingProxies(true);
                })
                .AddDbContext<SystemContext>(opt =>
                {
                    opt.UseNpgsql(connectionString);
                    //opt.UseLazyLoadingProxies(true);
                });*/

            // Web API middleware which will register all the controllers (classes derived from ControllerBase)
            /// <see cref="https://medium.com/imaginelearning/asp-net-core-3-1-microservice-quick-start-c0c2f4d6c7fa"/>
            services.AddControllers().AddNewtonsoftJson();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });


            services.AddSwagger("Invitation control service (ICS)", openApiSettings =>
             {
                 /*
                 openApiSettings.OperationProcessors.Add(new AddHeaderProcessor(XCreatorId, true, "”никальный идентификатор сервиса"));

                 openApiSettings.AddSwaggerTypeMappersInheritedFrom(
                     typeof(StringValueObject),
                     objectType => new PrimitiveTypeMapper(objectType, jsonSchema => jsonSchema.Type = JsonObjectType.String));
                 */
             });

            /*
            services.AddSwaggerGen(c =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWT Auth Demo", Version = "v1" });
            });
            */

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
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
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();

            app.UseOpenApi();

            app.UseStaticFiles();
			if (!env.IsDevelopment())
			{
				app.UseSpaStaticFiles();
			}

			app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501

				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}

		// Use this method to add services directly to LightInject
		// Important: This method must exist in order to replace the default provider.
		public void ConfigureContainer(IServiceContainer container)
		{
			var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();

			container.RegisterSingleton(factory => jwtTokenConfig);
			container.RegisterSingleton<JwtRefreshTokenCache>();
			container.Register<IUserService, UserService>();
			container.Register<IJwtAuthService, JwtAuthService>();

			container.RegisterFrom<AdapterCompositionRoot>();
			container.RegisterFrom<RepositoryCompositionRoot>();
			container.RegisterFrom<ServiceCompositionRoot>();
			container.RegisterFrom<CommandCompositionRoot>();
		}
		


	}
}
	