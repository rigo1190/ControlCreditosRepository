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
    public partial class Firma : System.Web.UI.Page
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
            gridFirmas.DataSource = uow.FirmasBL.Get().ToList();
            gridFirmas.DataBind();
        }

        private void BindControles()
        {
            int idTipo = Utilerias.StrToInt(_IDFirma.Value);

            Firmas obj = uow.FirmasBL.GetByID(idTipo);

            txtTesorero.Value = obj.Tesorero;
            txtSubdirector.Value = obj.SubdirectorDeRegistroYControl;
            txtJefe.Value = obj.Tesorero;
            txtDirector.Value = obj.DirectorGeneral;
        }

        private bool ValidarInsercion(string tes, string sub, string jefe, string dir, Firmas objF = null)
        {
            Firmas obj = null;

            if (objF == null)
                obj = uow.FirmasBL.Get(e => e.Tesorero.ToUpper().Trim() == tes.ToUpper().Trim() || 
                    e.SubdirectorDeRegistroYControl.ToUpper().Trim() == sub.ToUpper().Trim() || 
                    e.JefeDeptoOrdenesDePago.ToUpper().Trim() == jefe.ToUpper().Trim() || 
                    e.DirectorGeneral.ToUpper().Trim() == dir.ToUpper().Trim()).FirstOrDefault();
            else
                if (!tes.ToUpper().Trim().Equals(objF.Tesorero.ToUpper().Trim()))
                    obj = uow.FirmasBL.Get(e => e.Tesorero == tes).FirstOrDefault();
                else if (!sub.ToUpper().Trim().Equals(objF.SubdirectorDeRegistroYControl.ToUpper().Trim()))
                    obj = uow.FirmasBL.Get(e => e.SubdirectorDeRegistroYControl == sub).FirstOrDefault();
                else if (!jefe.ToUpper().Trim().Equals(objF.JefeDeptoOrdenesDePago.ToUpper().Trim()))
                    obj = uow.FirmasBL.Get(e => e.JefeDeptoOrdenesDePago == jefe).FirstOrDefault();
                else if (!dir.ToUpper().Trim().Equals(objF.DirectorGeneral.ToUpper().Trim()))
                    obj = uow.FirmasBL.Get(e => e.DirectorGeneral == dir).FirstOrDefault();

            return obj == null;
        }

        protected void gridFirmas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnEliminar = (ImageButton)e.Row.FindControl("imgBtnEliminar");

                int id = Utilerias.StrToInt(gridFirmas.DataKeys[e.Row.RowIndex].Values["Id"].ToString());

                if (imgBtnEliminar != null)
                    imgBtnEliminar.Attributes.Add("onclick", "fnc_ColocarID(" + id + ")");

            }
        }

        protected void gridFirmas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridFirmas.PageIndex = e.NewPageIndex;
            BindGrid();
            divEncabezado.Style.Add("display", "block");
            divMsgError.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _IDFirma.Value = gridFirmas.DataKeys[row.RowIndex].Value.ToString();
            _Accion.Value = "A";

            BindControles();

            divCaptura.Style.Add("display", "block");
            divEncabezado.Style.Add("display", "none");
            divMsgError.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Firmas obj;
            int idTipo = Utilerias.StrToInt(_IDFirma.Value);
            string M = string.Empty;

            if (_Accion.Value.Equals("N"))
                obj = new Firmas();
            else
                obj = uow.FirmasBL.GetByID(idTipo);

            if (!ValidarInsercion(txtTesorero.Value, txtSubdirector.Value,txtJefe.Value,txtDirector.Value, !_Accion.Value.Equals("N") ? obj : null))
            {
                divMsgError.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");
                divEncabezado.Style.Add("display", "none");
                divCaptura.Style.Add("display", "block");

                lblMsgError.Text = "Ya existe registro para los datos escritos. Intente con otros valores.";

                return;
            }


            obj.Tesorero = txtTesorero.Value;
            obj.SubdirectorDeRegistroYControl = txtSubdirector.Value;
            obj.JefeDeptoOrdenesDePago = txtJefe.Value;
            obj.DirectorGeneral = txtDirector.Value;
            

            if (_Accion.Value.Equals("N"))
                uow.FirmasBL.Insert(obj);
            else
                uow.FirmasBL.Update(obj);

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

            int id = Utilerias.StrToInt(_IDFirma.Value);

            Firmas obj = uow.FirmasBL.GetByID(id);


            uow.FirmasBL.Delete(obj);
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