using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCC.Formas.Fideicom
{
    public partial class wfCreditos : System.Web.UI.Page
    {
        private UnitOfWork uow;

        private int idFideicomiso;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindGrid();
                BindCombos();
                divCaptura.Style.Add("display","none");
            }
        }

        private void BindGrid()
        {
            uow = new UnitOfWork();

            this.idFideicomiso = int.Parse(Session["XidFideicomiso"].ToString());

            this.grid.DataSource = uow.CreditosBL.Get(p=>p.FideicomisoId==this.idFideicomiso).ToList();
            this.grid.DataBind();
        }

        private void BindCombos()
        {

            ddlTipoDeMoneda.DataSource = uow.TiposDeMonedasBL.Get().ToList();
            ddlTipoDeMoneda.DataValueField = "Id";
            ddlTipoDeMoneda.DataTextField = "Nombre";
            ddlTipoDeMoneda.DataBind();

            ddlFuenteFinanciamiento.DataSource = uow.FuentesDeFinanciamientosBL.Get().ToList();
            ddlFuenteFinanciamiento.DataValueField = "Id";
            ddlFuenteFinanciamiento.DataTextField = "Nombre";
            ddlFuenteFinanciamiento.DataBind();

            ddlDestinoFinanciamiento.DataSource = uow.DestinosDeFinanciamientosBL.Get().ToList();
            ddlDestinoFinanciamiento.DataValueField = "Id";
            ddlDestinoFinanciamiento.DataTextField = "Nombre";
            ddlDestinoFinanciamiento.DataBind();

            ddlPeriodoAmortizacion.DataSource = uow.PeriodosDeAmortizacionBL.Get().ToList();
            ddlPeriodoAmortizacion.DataValueField = "Id";
            ddlPeriodoAmortizacion.DataTextField = "Nombre";
            ddlPeriodoAmortizacion.DataBind();
        }


        protected void linkNew_Click(object sender, EventArgs e)
        {
            divDatos.Style.Add("display", "none");
            divCaptura.Style.Add("display", "block");
            divMSG.Style.Add("display", "none");
            _Accion.Text = "Nuevo";

            txtNumContrato.Value = string.Empty;
            txtCantidad.Value = string.Empty;
            txtValor.Value = string.Empty;
            txtImporteTotal.Value = string.Empty;
            txtDestino.Value = string.Empty;

            txtNPeriodos.Value = string.Empty;
            txtTasa.Value = string.Empty;
            txtTasaMoratoria.Value = string.Empty;
            txtPlazoInversion.Value = string.Empty;
            txtPlazoGracia.Value = string.Empty;

        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ElId.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            _Accion.Text = "Modify";

            Creditos credito = uow.CreditosBL.GetByID(int.Parse(_ElId.Text));

            txtNumContrato.Value = credito.NumeroDeContrato;
            dtpContrato.Value = String.Format("{0:d}", credito.FechaDelContrato);
            ddlTipoDeMoneda.SelectedValue = credito.TipoDeMonedaId.ToString();
            txtCantidad.Value = credito.CantidadContratada.ToString() ;
            txtValor.Value = credito.ValorTipoMoneda.ToString();
            txtImporteTotal.Value = credito.ImporteContratado.ToString();
            txtDestino.Value = credito.DestinoDelCredito;
            
            ddlFuenteFinanciamiento.SelectedValue = credito.FuenteDeFinanciamientoId.ToString();
            ddlDestinoFinanciamiento.SelectedValue = credito.DestinoDeFinanciamientoId.ToString(); 
            ddlPeriodoAmortizacion.SelectedValue = credito.PeriodoDeAmortizacionId.ToString();
            txtNPeriodos.Value = credito.NPeriodos.ToString();
            dtpInicio.Value = String.Format("{0:d}", credito.Inicio);
            dtpTermino.Value = String.Format("{0:d}", credito.Termino); 

            txtTasa.Value = credito.TasaNormal.ToString();
            txtTasaMoratoria.Value = credito.TasaMoratoria.ToString();
            txtPlazoInversion.Value = credito.PlazoDeInversion;
            txtPlazoGracia.Value = credito.PeriodoDeGracia;


            divDatos.Style.Add("display", "none");
            divCaptura.Style.Add("display", "block");
            divMSG.Style.Add("display", "none");
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ElId.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            Creditos Credito = uow.CreditosBL.GetByID(int.Parse(_ElId.Text));

            uow.Errors.Clear();
            List<Amortizaciones> lista;
            lista = uow.AmortizacionesBL.Get(p => p.CreditoId == Credito.Id).ToList();


            if (lista.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");



            if (uow.Errors.Count == 0)
            {
                uow.CreditosBL.Delete(Credito);
                uow.SaveChanges();
            }


            if (uow.Errors.Count == 0)
            {
                BindGrid();
                divDatos.Style.Add("display", "block");
                divCaptura.Style.Add("display", "none");
                divMSG.Style.Add("display", "none");
            }

            else
            {
                string mensaje;

                divMSG.Style.Add("display", "block");


                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";

                lblMSG.Text = mensaje;
            }
        }

        protected void btnGuardarContrato_Click(object sender, EventArgs e)
        {
            Creditos credito;
            List<Creditos> listaCreditos;

            this.idFideicomiso = int.Parse(Session["XidFideicomiso"].ToString());

            uow.Errors.Clear();

            if (_Accion.Text == "Nuevo") 
                credito = new Creditos();            
            else            
                credito = uow.CreditosBL.GetByID(int.Parse(_ElId.Text));


            credito.FideicomisoId = this.idFideicomiso;
            
            credito.NumeroDeContrato = txtNumContrato.Value;
            credito.FechaDelContrato = DateTime.Parse(dtpContrato.Value.ToString());
            credito.TipoDeMonedaId = int.Parse(ddlTipoDeMoneda.SelectedValue.ToString());
            credito.CantidadContratada = decimal.Parse(txtCantidad.Value.ToString());
            credito.ValorTipoMoneda = decimal.Parse(txtValor.Value.ToString());
            credito.ImporteContratado = credito.CantidadContratada * credito.ValorTipoMoneda;            
            credito.DestinoDelCredito = txtDestino.Value;

            credito.FuenteDeFinanciamientoId = int.Parse(ddlFuenteFinanciamiento.SelectedValue.ToString());
            credito.DestinoDeFinanciamientoId = int.Parse(ddlDestinoFinanciamiento.SelectedValue.ToString());
            credito.PeriodoDeAmortizacionId = int.Parse(ddlPeriodoAmortizacion.SelectedValue.ToString());
            credito.NPeriodos = int.Parse( txtNPeriodos.Value.ToString());
            credito.Inicio = DateTime.Parse(dtpInicio.Value.ToString());
            credito.Termino = DateTime.Parse(dtpTermino.Value.ToString());

            credito.TasaNormal = double.Parse( txtTasa.Value.ToString());
            credito.TasaMoratoria = double.Parse(txtTasaMoratoria.Value.ToString());
            credito.PlazoDeInversion = txtPlazoInversion.Value;
            credito.PeriodoDeGracia = txtPlazoGracia.Value;

            if (_Accion.Text == "Nuevo")
                uow.CreditosBL.Insert(credito);
            else
                uow.CreditosBL.Update(credito);


            //validaciones
            listaCreditos = uow.CreditosBL.Get(p => p.NumeroDeContrato == credito.NumeroDeContrato).ToList();
            if (listaCreditos.Count > 0)
                uow.Errors.Add("El número de crédito que capturo ya ha sido registrada anteriormente, verifique su información");

            //endValidaciones


            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)
            {

                BindGrid();

                divDatos.Style.Add("display", "block");
                divCaptura.Style.Add("display", "none");


            }
            else
            {
                divMSG.Style.Add("display", "block");

                string mensaje = "";
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";

                lblMSG.Text = mensaje;

            }





        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divDatos.Style.Add("display", "block");
            divMSG.Style.Add("display", "none");
            divCaptura.Style.Add("display", "none");
        }
    }
}