using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCC.Formas.Catalogos
{
    public partial class PeriodosAmortizacion : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            gridPerido.DataSource = uow.PeriodosDeAmortizacionBL.Get().ToList();
            gridPerido.DataBind();
        }

        private void BindControles()
        {
            int id= Utilerias.StrToInt(_IDPeriodo.Value);

            PeriodosDeAmortizacion obj = uow.PeriodosDeAmortizacionBL.GetByID(id);

            txtClave.Value = obj.Clave;
            txtDescripcion.Value = obj.Nombre;
            txtNumero.Value = obj.NMeses.ToString();

        }

        private bool ValidarEliminar(PeriodosDeAmortizacion obj)
        {
            if (obj.detalleCreditos.Count() > 0)
                return false;

            return true;
        }

        private bool ValidarInsercion(string clave, string nombre, PeriodosDeAmortizacion objPeriodo = null)
        {
            PeriodosDeAmortizacion obj = null;

            if (clave.Trim().Equals(string.Empty))
                return false;

            if (objPeriodo == null)
                obj = uow.PeriodosDeAmortizacionBL.Get(e => e.Clave.ToUpper().Trim() == clave.ToUpper().Trim() || e.Nombre.ToUpper().Trim() == nombre.ToUpper().Trim()).FirstOrDefault();
            else
                if (!clave.ToUpper().Trim().Equals(objPeriodo.Clave.ToUpper().Trim()))
                    obj = uow.PeriodosDeAmortizacionBL.Get(e => e.Clave == clave).FirstOrDefault();
                else if (!nombre.ToUpper().Trim().Equals(objPeriodo.Nombre.ToUpper().Trim()))
                    obj = uow.PeriodosDeAmortizacionBL.Get(e => e.Nombre == nombre).FirstOrDefault();

            return obj == null;
        }
        protected void gridPerido_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnEliminar = (ImageButton)e.Row.FindControl("imgBtnEliminar");

                int id = Utilerias.StrToInt(gridPerido.DataKeys[e.Row.RowIndex].Values["Id"].ToString());

                if (imgBtnEliminar != null)
                    imgBtnEliminar.Attributes.Add("onclick", "fnc_ColocarID(" + id + ")");

            }
        }

        protected void gridPerido_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPerido.PageIndex = e.NewPageIndex;
            BindGrid();
            divEncabezado.Style.Add("display", "block");
            divMsgError.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _IDPeriodo.Value = gridPerido.DataKeys[row.RowIndex].Value.ToString();
            _Accion.Value = "A";

            BindControles();

            divCaptura.Style.Add("display", "block");
            divEncabezado.Style.Add("display", "none");
            divMsgError.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            PeriodosDeAmortizacion obj;
            int id = Utilerias.StrToInt(_IDPeriodo.Value);
            string M = string.Empty;

            if (_Accion.Value.Equals("N"))
                obj = new PeriodosDeAmortizacion();
            else
                obj = uow.PeriodosDeAmortizacionBL.GetByID(id);

            if (!ValidarInsercion(txtClave.Value, txtDescripcion.Value, !_Accion.Value.Equals("N") ? obj : null))
            {
                divMsgError.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");
                divEncabezado.Style.Add("display", "none");
                divCaptura.Style.Add("display", "block");

                lblMsgError.Text = "Ya existe registro para la CLAVE o NOMBRE escritos. Intente con otros valores.";

                return;
            }

            obj.NMeses = Utilerias.StrToInt(txtNumero.Value);
            obj.Clave = txtClave.Value;
            obj.Nombre = txtDescripcion.Value;

            if (_Accion.Value.Equals("N"))
                uow.PeriodosDeAmortizacionBL.Insert(obj);
            else
                uow.PeriodosDeAmortizacionBL.Update(obj);

            uow.SaveChanges();

            if (uow.Errors.Count > 0)
            {
                foreach (string err in uow.Errors)
                    M += err;

                //MANEJAR EL ERROR
                divMsgError.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");
                divCaptura.Style.Add("display", "block");
                divEncabezado.Style.Add("display", "none");
                lblMsgError.Text = M;
                return;
            }

            BindGrid();

            divMsgError.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "block");
            lblMsgSuccess.Text = "Se ha guardado correctamente";

            divEncabezado.Style.Add("display", "block");
            divCaptura.Style.Add("display", "none");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            string M = "Se ha eliminado correctamente";

            int id = Utilerias.StrToInt(_IDPeriodo.Value);

            PeriodosDeAmortizacion obj = uow.PeriodosDeAmortizacionBL.GetByID(id);

            if (!ValidarEliminar(obj))
            {
                M = "No se puede eliminar el registro, se encuentra en uso por otros módulos.";
                lblMsgError.Text = M;
                divMsgError.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");
                return;
            }

            uow.PeriodosDeAmortizacionBL.Delete(obj);
            uow.SaveChanges();

            if (uow.Errors.Count > 0) //Si hubo errores
            {
                M = string.Empty;
                foreach (string cad in uow.Errors)
                    M += cad;

                lblMsgError.Text = M;
                divMsgError.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");
                divCaptura.Style.Add("display", "none");
                divEncabezado.Style.Add("display", "block");
                return;
            }


            BindGrid();

            lblMsgSuccess.Text = M;
            divMsgError.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "block");
            divCaptura.Style.Add("display", "none");
            divEncabezado.Style.Add("display", "block");
        }
    }
}