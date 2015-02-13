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
    public partial class Departamento : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindGrid();
                BindDropDownUnidades();
            }
        }

        private void BindDropDownUnidades()
        {
            ddlUnidades.DataSource = uow.UnidadPresupuestalBL.Get().ToList();
            ddlUnidades.DataValueField = "Id";
            ddlUnidades.DataTextField = "Nombre";
            ddlUnidades.DataBind();
            
        }

        private void BindGrid()
        {
            gridDeptos.DataSource = uow.DepartamentosBL.Get().ToList();
            gridDeptos.DataBind();
        }

        private void BindControles()
        {
            int id = Utilerias.StrToInt(_IDDepto.Value);

            Departamentos obj = uow.DepartamentosBL.GetByID(id);

            txtClave.Value = obj.Clave;
            txtDescripcion.Value = obj.Nombre;
            ddlUnidades.SelectedValue = obj.UnidadPresupuestalId.ToString();

        }

        private bool ValidarEliminarDepto(Departamentos obj)
        {
            if (obj.detalleAmortizaciones.Count() > 0)
                return false;

            return true;
        }

        private bool ValidarInsercion(string clave,string nombre, Departamentos objDepto = null)
        {
            Departamentos obj = null;

            if (clave.Trim().Equals(string.Empty))
                return false;

            if (objDepto == null)
                obj = uow.DepartamentosBL.Get(e => e.Clave.ToUpper().Trim() == clave.ToUpper().Trim() || e.Nombre.ToUpper().Trim() == nombre.ToUpper().Trim()).FirstOrDefault();
            else
                if (!clave.ToUpper().Trim().Equals(objDepto.Clave.ToUpper().Trim()))
                    obj = uow.DepartamentosBL.Get(e => e.Clave == clave).FirstOrDefault();
                else if (!nombre.ToUpper().Trim().Equals(objDepto.Nombre.ToUpper().Trim()))
                    obj = uow.DepartamentosBL.Get(e => e.Nombre == nombre).FirstOrDefault();

            return obj == null;
        }

        protected void gridDeptos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnEliminar = (ImageButton)e.Row.FindControl("imgBtnEliminar");
                Label lblUnidad = (Label)e.Row.FindControl("lblUnidad");

                int id = Utilerias.StrToInt(gridDeptos.DataKeys[e.Row.RowIndex].Values["Id"].ToString());

                Departamentos d = uow.DepartamentosBL.GetByID(id);
                UnidadPresupuestal u = uow.UnidadPresupuestalBL.GetByID(d.UnidadPresupuestalId);
                lblUnidad.Text = u.Nombre;

                if (imgBtnEliminar != null)
                    imgBtnEliminar.Attributes.Add("onclick", "fnc_ColocarID(" + id + ")");

            }
        }

        protected void gridDeptos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridDeptos.PageIndex = e.NewPageIndex;
            BindGrid();
            divEncabezado.Style.Add("display", "block");
            divMsgError.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _IDDepto.Value = gridDeptos.DataKeys[row.RowIndex].Value.ToString();
            _Accion.Value = "A";

            BindControles();

            divCaptura.Style.Add("display", "block");
            divEncabezado.Style.Add("display", "none");
            divMsgError.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Departamentos obj;
            int id = Utilerias.StrToInt(_IDDepto.Value);
            string M = string.Empty;

            if (_Accion.Value.Equals("N"))
                obj = new Departamentos();
            else
                obj = uow.DepartamentosBL.GetByID(id);

            if (!ValidarInsercion(txtClave.Value, txtDescripcion.Value, !_Accion.Value.Equals("N") ? obj : null))
            {
                divMsgError.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");
                divEncabezado.Style.Add("display", "none");
                divCaptura.Style.Add("display", "block");

                lblMsgError.Text = "Ya existe registro para la CLAVE o NOMBRE escritos. Intente con otros valores.";

                return;
            }

            obj.Clave = txtClave.Value;
            obj.Nombre = txtDescripcion.Value;
            obj.UnidadPresupuestalId = Utilerias.StrToInt(ddlUnidades.SelectedValue);

            if (_Accion.Value.Equals("N"))
                uow.DepartamentosBL.Insert(obj);
            else
                uow.DepartamentosBL.Update(obj);

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

            int id = Utilerias.StrToInt(_IDDepto.Value);

            Departamentos obj = uow.DepartamentosBL.GetByID(id);

            if (!ValidarEliminarDepto(obj))
            {
                M = "No se puede eliminar el registro, se encuentra en uso por otros módulos.";
                lblMsgError.Text = M;
                divMsgError.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");
                return;
            }

            uow.DepartamentosBL.Delete(obj);
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