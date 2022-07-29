using DATA.Models;
using Microsoft.EntityFrameworkCore;
using PERSISTENCE.Configuration;

namespace PERSISTENCE
{
    public class Context : DbContext
    {
        
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }
        #region DbSets
        public DbSet<EstadosUnidades> EstadosUnidades { get; set; }
        public DbSet<SituacionesUnidades> SituacionesUnidades { get; set; }
        public DbSet<Unidades> Unidades { get; set; }
        public DbSet<Marcas> Marcas { get; set; }
        public DbSet<Modelos> Modelos { get; set; }
        public DbSet<Grupos> Grupos { get; set; }
        public DbSet<AgrupacionesSindicales> AgrupacionesSindicales { get; set; }
        public DbSet<CentroDeCosto> CentroDeCosto { get; set; }
        public DbSet<Choferes> Choferes { get; set; }
        public DbSet<Convenios> Convenios { get; set; }
        public DbSet<Empresas> Empresas { get; set; }
        public DbSet<Especialidades> Especialidades { get; set; }
        public DbSet<Funciones> Funciones { get; set; }
        public DbSet<Localidades> Localidades { get; set; }
        public DbSet<Provincias> Provincias { get; set; }
        public DbSet<Titulos> Titulos { get; set; }
        public DbSet<Trazas> Trazas { get; set; }
        public DbSet<UnidadesDeMedida> UnidadesDeMedida { get; set; }
        public DbSet<VariablesUnidades> VariablesUnidades { get; set; }
        public DbSet<Equipamientos> Equipamientos { get; set; }
<<<<<<< HEAD
        public DbSet<Trabajos> Trabajos { get; set; }
        public DbSet<Mecanicos> Mecanicos { get; set; }
        public DbSet<Rubros> Rubros { get; set; }
        //public DbSet<Repuestos> Repuestos { get; set; }
        //public DbSet<Articulos> Articulos { get; set; }
        public DbSet<AlicuotasIVA> AlicuotasIVA { get; set; }
        public DbSet<ValoresMediciones> ValoresMediciones { get; set; }
        public DbSet<Tareas> Tareas { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
=======
        public DbSet<Talleres> Talleres { get; set; }
        public DbSet<Calles> Calles { get; set; }
        public DbSet<Barrios> Barrios { get; set; }
        public DbSet<Domicilios> Domicilios { get; set; }
        public DbSet<Mecanicos> Mecanicos { get; set; }
        public DbSet<Repuestos> Repuestos { get; set; }
        public DbSet<Articulos> Articulos { get; set; }
>>>>>>> REQ-24235-(Segunda-Tanda-Entidades)

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       
            base.OnModelCreating(modelBuilder);
            
            ModelConfig(modelBuilder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new EstadosUnidadesConfiguration(modelBuilder.Entity<EstadosUnidades>());
            new SituacionesUnidadesConfiguration(modelBuilder.Entity<SituacionesUnidades>());
            new UnidadesConfiguration(modelBuilder.Entity<Unidades>());
            new ModelosConfiguration(modelBuilder.Entity<Modelos>());
            new MarcasConfiguration(modelBuilder.Entity<Marcas>());
            new GruposConfiguration(modelBuilder.Entity<Grupos>());
            new AgrupacionesSindicalesConfiguration(modelBuilder.Entity<AgrupacionesSindicales>());
            new CentrodeCostoConfiguration(modelBuilder.Entity<CentroDeCosto>());
            new ChoferesConfiguration(modelBuilder.Entity<Choferes>());
            new ConveniosConfiguration(modelBuilder.Entity<Convenios>());
            new EmpresasConfiguration(modelBuilder.Entity<Empresas>());
            new EspecialidadesConfiguration(modelBuilder.Entity<Especialidades>());
            new FuncionesConfiguration(modelBuilder.Entity<Funciones>());
            new LocalidadesConfiguration(modelBuilder.Entity<Localidades>());
            new ProvinciasConfiguration(modelBuilder.Entity<Provincias>());
            new TitulosConfiguration(modelBuilder.Entity<Titulos>());
            new TrazasConfiguration(modelBuilder.Entity<Trazas>());
            new UnidadesDeMedidaConfiguration(modelBuilder.Entity<UnidadesDeMedida>());
            new VariablesUnidadesConfiguration(modelBuilder.Entity<VariablesUnidades>());
            new EquipamientosConfiguration(modelBuilder.Entity<Equipamientos>());
<<<<<<< HEAD
            new TrabajosConfiguration(modelBuilder.Entity<Trabajos>());
            new RubrosConfiguration(modelBuilder.Entity<Rubros>());
            new MecanicosConfiguration(modelBuilder.Entity<Mecanicos>());
            // new ArticulosConfiguration(modelBuilder.Entity<Articulos>());
            // new RepuestosConfiguration(modelBuilder.Entity<Repuestos>());
            new ValoresMedicionesConfiguration(modelBuilder.Entity<ValoresMediciones>());
            new AlicuotasIVAConfiguration(modelBuilder.Entity<AlicuotasIVA>());
            new TareasConfiguration(modelBuilder.Entity<Tareas>());
            new ProveedoresConfiguration(modelBuilder.Entity<Proveedores>());
=======
            new TalleresConfiguration(modelBuilder.Entity<Talleres>());
            new CallesConfiguration(modelBuilder.Entity<Calles>());
            new BarriosConfiguration(modelBuilder.Entity<Barrios>());
            new DomiciliosConfiguration(modelBuilder.Entity<Domicilios>());
            new MecanicosConfiguration(modelBuilder.Entity<Mecanicos>());
            new RepuestosConfiguration(modelBuilder.Entity<Repuestos>());
            new ArticulosConfiguration(modelBuilder.Entity<Articulos>());
>>>>>>> REQ-24235-(Segunda-Tanda-Entidades)
        }
    }
}
