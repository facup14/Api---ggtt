using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PERSISTENCE;
using Service.Queries;

namespace API
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
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            services.AddControllers();
            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).ReplaceService<IQueryTranslationPostprocessorFactory, SqlServer2008QueryTranslationPostprocessorFactory>());


            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDbContextCheck<Context>();

            services.AddHealthChecksUI().AddInMemoryStorage();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GGTT", Version = "v1" });
            });
            services.AddTransient<IUnidadesQueryService, UnidadesQueryService>();
            services.AddTransient<ISituacionesUnidadesQueryService, SituacionesUnidadesQueryService>();            
            services.AddTransient<ITitulosQueryService, TitulosQueryService>();
            services.AddTransient<ITrazasQueryService, TrazasQueryService>();
            services.AddTransient<IUnidadesDeMedidaQueryService, UnidadesDeMedidaQueryService>();
            services.AddTransient<IVariablesUnidadesQueryService, VariablesUnidadesQueryService>();
            services.AddTransient<IEquipamientoQueryService, EquipamientoQueryService>();    
            services.AddTransient<IAgrupacionesSindicalesQueryService, AgrupacionesSindicalesQueryService>();
            services.AddTransient<ICentrodeCostoQueryService, CentrodeCostoQueryService>();
            services.AddTransient<IConveniosQueryService, ConveniosQueryService>();
            services.AddTransient<IEmpresasQueryService, EmpresasQueryService>();
            services.AddTransient<IEspecialidadesQueryService, EspecialidadesQueryService>();
            services.AddTransient<IEstadosUnidadesQueryService, EstadosUnidadesQueryService>();
            services.AddTransient<IMarcasQueryService, MarcasQueryService>();
            services.AddTransient<IModelosQueryService, ModelosQueryService>();
            services.AddTransient<IProvinciasQueryService, ProvinciasQueryService>();
            services.AddTransient<IFuncionesQueryService, FuncionesQueryService>();
            services.AddTransient<IGruposQueryService, GruposQueryService>();
            services.AddTransient<ILocalidadQueryService, LocalidadesQueryService>();
            services.AddTransient<IChoferesQueryService, ChoferesQueryService>();
            services.AddTransient<IRubrosQueryService, RubrosQueryService>();
            services.AddTransient<ITrabajosQueryService, TrabajosQueryService>();
            services.AddTransient<IMecanicoQueryService, MecanicoQueryService>();
            services.AddTransient<IValoresMedicionesQueryService, ValoresMedicionesQueryService>();
            services.AddTransient<IAlicuotasIVAQueryService, AlicuotasIVAQueryService>();
            services.AddTransient<ITareasQueryService, TareasQueryService>();
            services.AddTransient<IProveedoresQueryService, ProveedoresQueryService>();
            services.AddTransient<ITalleresQueryService, TalleresQueryService>();
            services.AddTransient<IDomiciliosQueryService, DomiciliosQueryService>();
            services.AddTransient<IBarriosQueryService, BarriosQueryService>();
            services.AddTransient<ICallesQueryService, CallesQueryService>();
            services.AddTransient<IMecanicoQueryService, MecanicoQueryService>();
            services.AddTransient<IRepuestosQueryService, RepuestosQueryService>();
            services.AddTransient<IArticulosQueryService, ArticulosQueryService>();
            services.AddTransient<ICambiosCentroDeCostoQueryService, CambiosCentroDeCostoQueryService>();
            services.AddTransient<IUnidadesChoferesQueryService, UnidadesChoferesQueryService>();

            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://example.com",
                                                          "http://www.contoso.com")
                                                           .AllowAnyHeader()
                                                           .AllowAnyMethod();
                                  });
            });
            services.AddControllers().AddNewtonsoftJson(x =>
            x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            if (env.IsDevelopment())
            {
            }
            app.UseHttpsRedirection();
            
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });            
                
                endpoints.MapHealthChecksUI();
                
                endpoints.MapControllers();
            });
            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/healthchecks-ui";
                options.ApiPath = "/health";
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.RoutePrefix = string.Empty;
            });
            
        }
    }
}
