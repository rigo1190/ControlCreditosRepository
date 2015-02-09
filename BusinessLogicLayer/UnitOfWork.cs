using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace BusinessLogicLayer
{
    public class UnitOfWork : IDisposable
    {       
        internal Contexto contexto;
        private int usuarioId;
        private List<String> errors = new List<String>();
        private IBusinessLogic<Usuario> usuarioBL;
        private IBusinessLogic<Rol> rolBL;        
        private IBusinessLogic<Permiso> permisoBL;
        private IBusinessLogic<OpcionSistema> opcionsistemaBL;
        private IBusinessLogic<UsuarioRol> usuariorolBL;
        private IBusinessLogic<Ejercicio> ejercicioBL;
        private IBusinessLogic<Municipio> municipioBL;

        private IBusinessLogic<Financieras> financierasBL;
        private IBusinessLogic<Fideicomisos> fideicomisosBL;
                private IBusinessLogic<Firmas> firmasBL;
        private IBusinessLogic<PeriodosDeAmortizacion> periodosdeamortizacionBL;
        private IBusinessLogic<UnidadPresupuestal> unidadpresupuestalBL;
        private IBusinessLogic<Departamentos> departamentosBL;

        private IBusinessLogic<Creditos> creditosBL;
        private IBusinessLogic<Amortizaciones> amortizacionesBL;
        private IBusinessLogic<AmortizacionesConceptos> amortizacionesconceptosBL;
    

        public UnitOfWork()
        {
            this.contexto = new Contexto();
        }

        public UnitOfWork(string usuarioId)
        {           
            this.usuarioId = Utilerias.StrToInt(usuarioId);
            this.contexto = new Contexto();
        }

        public IBusinessLogic<Usuario> UsuarioBL
        {
            get
            {
                if (this.usuarioBL == null)
                {
                    this.usuarioBL = new GenericBusinessLogic<Usuario>(contexto);
                }

                return usuarioBL;
            }
        }   

        public IBusinessLogic<Rol> RolBL
        {
            get
            {
                if (this.rolBL == null)
                {
                    this.rolBL = new GenericBusinessLogic<Rol>(contexto);
                }

                return rolBL;
            }
        }

        public IBusinessLogic<UsuarioRol> UsuarioRolBL
        {
            get
            {
                if (this.usuariorolBL == null)
                {
                    this.usuariorolBL = new GenericBusinessLogic<UsuarioRol>(contexto);
                }

                return usuariorolBL;
            }
        }

        public IBusinessLogic<Permiso> PermisoBL
        {
            get
            {
                if (this.permisoBL == null)
                {
                    this.permisoBL = new GenericBusinessLogic<Permiso>(contexto);
                }

                return permisoBL;
            }
        }

        public IBusinessLogic<OpcionSistema> OpcionSistemaBL
        {
            get
            {
                if (this.opcionsistemaBL == null)
                {
                    this.opcionsistemaBL = new GenericBusinessLogic<OpcionSistema>(contexto);
                }

                return this.opcionsistemaBL;
            }
        }
               
        public IBusinessLogic<Ejercicio> EjercicioBL
        {
            get
            {
                if (this.ejercicioBL == null)
                {
                    this.ejercicioBL = new GenericBusinessLogic<Ejercicio>(contexto);
                }

                return this.ejercicioBL;
            }
        }


        public IBusinessLogic<Municipio> MunicipioBL
        {
            get
            {
                if (this.municipioBL == null)
                {
                    this.municipioBL = new GenericBusinessLogic<Municipio>(contexto);
                }

                return this.municipioBL;
            }
        }


        public IBusinessLogic<Financieras> FinancierasBL
        {
            get
            {
                if (this.financierasBL == null)
                {
                    this.financierasBL = new GenericBusinessLogic<Financieras>(contexto);
                }

                return this.financierasBL;
            }
        }

        public IBusinessLogic<Fideicomisos> FideicomisosBL
        {
            get
            {
                if (this.fideicomisosBL == null)
                {
                    this.fideicomisosBL = new GenericBusinessLogic<Fideicomisos>(contexto);
                }

                return this.fideicomisosBL;
            }
        }

         


        public IBusinessLogic<Firmas> FirmasBL
        {
            get
            {
                if (this.firmasBL == null)
                {
                    this.firmasBL = new GenericBusinessLogic<Firmas>(contexto);
                }
                return this.firmasBL;
            }
        }


        public IBusinessLogic<PeriodosDeAmortizacion> PeriodosDeAmortizacionBL
        {
            get
            {
                if (this.periodosdeamortizacionBL == null)
                {
                    this.periodosdeamortizacionBL = new GenericBusinessLogic<PeriodosDeAmortizacion>(contexto);
                }

                return this.periodosdeamortizacionBL;
            }
        }

        public IBusinessLogic<UnidadPresupuestal> UnidadPresupuestalBL
        {
            get
            {
                if (this.unidadpresupuestalBL == null)
                {
                    this.unidadpresupuestalBL = new GenericBusinessLogic<UnidadPresupuestal>(contexto);
                }
                return this.unidadpresupuestalBL;
            }
        }


        public IBusinessLogic<Departamentos> DepartamentosBL
        {
            get
            {
                if (this.departamentosBL == null)
                {
                    this.departamentosBL = new GenericBusinessLogic<Departamentos>(contexto);
                }
                return this.departamentosBL;
            }        
        }

        public IBusinessLogic<Creditos> CreditosBL
        {
            get
            {
                if (this.creditosBL == null)
                {
                    this.creditosBL = new GenericBusinessLogic<Creditos>(contexto);
                }
                return this.creditosBL;
            }
        }

        public IBusinessLogic<Amortizaciones> AmortizacionesBL
        {
            get
            {
                if (this.amortizacionesBL == null)
                {
                    this.amortizacionesBL = new GenericBusinessLogic<Amortizaciones>(contexto);
                }
                return this.amortizacionesBL;
            }
        }


        public IBusinessLogic<AmortizacionesConceptos> AmortizacionesConceptosBL
        {
            get
            {
                if (this.amortizacionesconceptosBL == null)
                {
                    this.amortizacionesconceptosBL = new GenericBusinessLogic<AmortizacionesConceptos>(contexto);
                }
                return this.amortizacionesconceptosBL;
            }
        }
     
        public void SaveChanges()
        {
            try
            {
                errors.Clear();
                contexto.SaveChanges(usuarioId);
            }
            catch (DbEntityValidationException ex)
            {

                this.RollBack();

                foreach (var item in ex.EntityValidationErrors)
                {

                    errors.Add(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors", item.Entry.Entity.GetType().Name, item.Entry.State));

                    foreach (var error in item.ValidationErrors)
                    {
                        errors.Add(String.Format("Propiedad: \"{0}\", Error: \"{1}\"", error.PropertyName, error.ErrorMessage));
                    }


                }

            }
            catch (DbUpdateException ex)
            {
                this.RollBack();
                errors.Add(String.Format("{0}", ex.InnerException.InnerException.Message));
            }
            catch (System.InvalidOperationException ex)
            {
                this.RollBack();
                errors.Add(String.Format("{0}", ex.Message));
            }
            catch (Exception ex)
            {
                this.RollBack();
                errors.Add(String.Format("{0}\n{1}", ex.Message, ex.InnerException.Message));
            }
            
        }

        public List<String> Errors 
        {
            get 
            {
                return errors;
            }
        }
        
        public void RollBack()
        {
           
            var changedEntries = contexto.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged);

            #region < Pendiente revisar, esto podria cancelar toda una sesión de trabajo >

            //foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            //{
            //    entry.CurrentValues.SetValues(entry.OriginalValues);
            //    entry.State = EntityState.Unchanged;
            //}

            //foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            //{
            //    entry.State = EntityState.Detached;
            //} 

            #endregion

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }
            
        }
        
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    contexto.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
