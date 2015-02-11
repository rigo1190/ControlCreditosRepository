using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Contexto : DbContext
    {
        private int userId;
       
        public Contexto()
            : base("SCC")
        {
            System.Diagnostics.Debug.Print(Database.Connection.ConnectionString);
        }            

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {          
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            modelBuilder.Entity<UsuarioRol>()
              .HasRequired(c => c.Usuario)
              .WithMany(d => d.DetalleRoles)
              .HasForeignKey(c => c.UsuarioId);
            
        }

        public int SaveChanges(int userId)
        {

            var creados = this.ChangeTracker.Entries()
                            .Where(e => e.State == System.Data.Entity.EntityState.Added)
                            .Select(e => e.Entity).OfType<Generica>().ToList();

            foreach (var item in creados)
            {
                item.CreatedAt = DateTime.Now;
                item.CreatedById = (userId==0)?null:(int?)userId;
            }

            var modificados = this.ChangeTracker.Entries()
                            .Where(e => e.State == System.Data.Entity.EntityState.Modified)
                            .Select(e => e.Entity).OfType<Generica>().ToList();

            foreach (var item in modificados)
            {
                item.EditedAt = DateTime.Now;
                item.EditedById = (userId == 0) ? null : (int?)userId;
            }

            return SaveChanges();
            

        }

        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Permiso> Permisos { get; set; }
        public virtual DbSet<OpcionSistema> OpcionesSistema { get; set; }
        public virtual DbSet<UsuarioRol> UsuarioRoles { get; set; }


        public virtual DbSet<Ejercicio> Ejercicios { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }

        public virtual DbSet<Financieras> DBSfinancieras { get; set; }

        public virtual DbSet<FuentesDeFinanciamientos> DBSfuentesdefinanciamientos { get; set; }
        public virtual DbSet<DestinosDeFinanciamientos> DBSdestinosdefinanciamientos { get; set; }

        public virtual DbSet<Fideicomisos> DBSfideicomisos { get; set; }
        public virtual DbSet<Firmas> DBSfirmas { get; set; }
        public virtual DbSet<PeriodosDeAmortizacion> DBSperiodosdeamortizacion { get; set; }
        public virtual DbSet<UnidadPresupuestal> DBSunidadpresupuestal { get; set; }
        public virtual DbSet<Departamentos> DBSdepartamentos { get; set; }


        public virtual DbSet<Creditos> DBScreditos { get; set; }
        public virtual DbSet<Amortizaciones> DBSamortizaciones { get; set; }

        public virtual DbSet<AmortizacionesConceptos> DBSamortizacionesconceptos { get; set; }
    }

}   
