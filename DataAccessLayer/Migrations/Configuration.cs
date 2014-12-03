namespace DataAccessLayer.Migrations
{
    using DataAccessLayer.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.Models.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccessLayer.Models.Contexto context)
        {            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );

            return;                       
           
            context.Roles.AddOrUpdate(
            
                new Rol { Id = 1 , Clave="R001", Nombre="Desarrollador", Orden=1},
                new Rol { Id = 2 , Clave="R002", Nombre="Ejecutivo", Orden = 2 },
                new Rol { Id = 3 , Clave="R003", Nombre="Administrador", Orden=3},
                new Rol { Id = 4 , Clave="R004", Nombre="Capturista", Orden=4},
                new Rol { Id = 5,  Clave="R005", Nombre="Analista", Orden = 5 }   
            );
                        

            context.Usuarios.AddOrUpdate(

               new Usuario { Id = 1, Login = "desarrollador", Password = "desarrollador", Nombre = "Usuario Desarrollador", Activo = true,RolId=1 },
               new Usuario { Id = 2, Login = "ejecutivo", Password = "ejecutivo", Nombre = "Usuario Ejecutivo", Activo = true, RolId = 2 },
               new Usuario { Id = 3, Login = "admin", Password = "admin", Nombre = "Usuario Administrador", Activo = true, RolId = 3 }
               
            );

           

            context.OpcionesSistema.AddOrUpdate(
            
                new OpcionSistema { Id = 1, Clave = "OS001", Descripcion = "Catálogos",Activo=true,Orden=1}
                
            );

            //context.Permisos.AddOrUpdate(

            //    new Permiso { Id = 1, RolId = 3, OpcionSistemaId = 4, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
            //    new Permiso { Id = 2, RolId = 3, OpcionSistemaId = 5, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
            //    new Permiso { Id = 3, RolId = 3, OpcionSistemaId = 6, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
            //    new Permiso { Id = 4, RolId = 4, OpcionSistemaId = 1, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
            //    new Permiso { Id = 5, RolId = 4, OpcionSistemaId = 2, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle }
                
            //);            
            

            context.Ejercicios.AddOrUpdate(              
               new Ejercicio { Id = 1, Año = 2015, FactorIva = 1.6M, Estatus = enumEstatusEjercicio.Activo   }               
            );

            
                       


            context.Municipios.AddOrUpdate(              
              new Municipio {Id=1,Clave="M001",Nombre="Acajete",Orden=1 },
              new Municipio {Id=2,Clave="M002",Nombre="Acatlán",Orden=2 },
              new Municipio {Id=3,Clave="M003",Nombre="Acayucan",Orden=3 },
              new Municipio {Id=4,Clave="M004",Nombre="Actopan",Orden=4 }              
            );


           context.DBSfinancieras.AddOrUpdate(
                new Financieras { Id=1,Clave="001",Nombre="FINTEGRA",Banco="BANCO DEL BAJIO, S.A. INSTITUCIÓN BANCARIA",CuentaBancaria ="599397",CLABE="030180599397703013"}                
           );

           context.DBSfideicomisos.AddOrUpdate(
               new Fideicomisos { Id=1, Clave = "001", Nombre = "Fideicomiso A",Status=1}
           );

           context.DBStiposdecreditos.AddOrUpdate(
               new TiposDeCredito { Id=1,Clave="ED",Nombre="ESTATAL DIRECTA"},
               new TiposDeCredito { Id=2,Clave="BE2006",Nombre="BURSATILIZACIÓN ESTATAL 2006"},
               new TiposDeCredito { Id=3,Clave="BE2012",Nombre="BURSATILIZACIÓN ESTATAL 2012"},
               new TiposDeCredito { Id=4,Clave="BM2008",Nombre="BURSTILIZACIÓN MUNICIPAL 2008"},
               new TiposDeCredito { Id=5,Clave="FAIS",Nombre="FONDO DE APORTACIONES PARA LA INFRAESTRUCTURA SOCIAL"}
           );

           context.DBSfirmas.AddOrUpdate(
            new Firmas { Id=1,Tesorero ="LIC. ANTONIO TAREK ABDALA SAAD", SubdirectorDeRegistroYControl ="C.P. LUIS ENRIQUE GUERRERO OLVERA", JefeDeptoOrdenesDePago="C.P. JOAQUÍN SZYMANSKI VARONA"}
           );

           context.DBSperiodosdeamortizacion.AddOrUpdate(
               new PeriodosDeAmortizacion { Id=1,Clave="MEN",Nombre ="MENSUAL",NMeses=1},
               new PeriodosDeAmortizacion { Id=2,Clave="BIM",Nombre ="BIMESTRAL",NMeses=2},
               new PeriodosDeAmortizacion { Id=3,Clave="TRI",Nombre ="TRIMESTRAL",NMeses=3},
               new PeriodosDeAmortizacion { Id=4,Clave="CUA",Nombre ="CUATRIMESTRAL",NMeses=4},
               new PeriodosDeAmortizacion { Id=5,Clave="SEM",Nombre ="SEMESTRAL",NMeses=6},
               new PeriodosDeAmortizacion { Id=6,Clave="AN",Nombre ="ANUAL",NMeses=12}
               );

          context.SaveChanges();

          
           
        }








    }
}
