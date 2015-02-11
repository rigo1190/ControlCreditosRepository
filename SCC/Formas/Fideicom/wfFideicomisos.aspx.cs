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
    public partial class wfFideicomisos : System.Web.UI.Page
    {
        private UnitOfWork uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindGrid();
                BindCombos();
                divCaptura.Style.Add("display","none");
                divMSG.Style.Add("display","none");
            }

        }

        # region metodos 
        
        private void BindGrid()
        {
            uow = new UnitOfWork();
            this.grid.DataSource = uow.FideicomisosBL.Get(p => p.Status == 1).ToList().OrderBy(q=>q.Clave);
            this.grid.DataBind();
        }

        private void BindCombos()
        {
            ddlFinanciera.DataSource = uow.FinancierasBL.Get(p => p.Status != 3).ToList();
            ddlFinanciera.DataValueField = "Id";
            ddlFinanciera.DataTextField = "Nombre";
            ddlFinanciera.DataBind();
        }
        # endregion


        #region eventos
        protected void linkNew_Click(object sender, EventArgs e)
        {
            divDatos.Style.Add("display", "none");
            divCaptura.Style.Add("display", "block");
            divMSG.Style.Add("display", "none");
            _Accion.Text = "Nuevo";
            txtClave.Value = string.Empty;
            txtNombre.Value = string.Empty;

        }
        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ElId.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            _Accion.Text = "Modify"; 

            Fideicomisos fideicomiso = uow.FideicomisosBL.GetByID(int.Parse(_ElId.Text));

            txtClave.Value = fideicomiso.Clave;
            txtNombre.Value = fideicomiso.Nombre;
            ddlFinanciera.SelectedValue = fideicomiso.FinancieraId.ToString();

            divDatos.Style.Add("display", "none");
            divCaptura.Style.Add("display", "block");
            divMSG.Style.Add("display", "none");
            
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ElId.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            Fideicomisos fideicomiso = uow.FideicomisosBL.GetByID(int.Parse(_ElId.Text));


            uow.Errors.Clear();
            List<Creditos> lista;
            lista = uow.CreditosBL.Get(p => p.FideicomisoId == fideicomiso.Id).ToList();


            if (lista.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");



            if (uow.Errors.Count == 0)
            {
                uow.FideicomisosBL.Delete(fideicomiso);
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Fideicomisos fideicomiso;
            List<Fideicomisos> lista;

            uow.Errors.Clear();

            if (_Accion.Text == "Nuevo")
            {
                fideicomiso = new Fideicomisos();

                fideicomiso.Clave = txtClave.Value;
                fideicomiso.Nombre = txtNombre.Value;
                fideicomiso.FinancieraId = int.Parse(ddlFinanciera.SelectedValue.ToString());
                fideicomiso.Status = 1;


                lista = uow.FideicomisosBL.Get(p => p.Clave == fideicomiso.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");

                lista = uow.FideicomisosBL.Get(p => p.Nombre == fideicomiso.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El Nombre que capturo ya ha sido registrada anteriormente, verifique su información");


                uow.FideicomisosBL.Insert(fideicomiso);

            }
            else
            {
                fideicomiso = uow.FideicomisosBL.GetByID(int.Parse(_ElId.Text));

                fideicomiso.Clave = txtClave.Value;
                fideicomiso.Nombre = txtNombre.Value;
                fideicomiso.FinancieraId = int.Parse(ddlFinanciera.SelectedValue.ToString());

                int xid;

                xid = int.Parse(_ElId.Text);

                lista = uow.FideicomisosBL.Get(p => p.Id != xid && p.Clave == fideicomiso.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");

                lista = uow.FideicomisosBL.Get(p => p.Id != xid && p.Nombre == fideicomiso.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El Nombre que capturo ya ha sido registrada anteriormente, verifique su información");

                uow.FideicomisosBL.Update(fideicomiso);
            }


            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)
            {

                txtClave.Value = string.Empty;
                txtNombre.Value = string.Empty;
                
                BindGrid();

                divDatos.Style.Add("display", "block");
                divCaptura.Style.Add("display", "none");
                divMSG.Style.Add("display", "none");

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

        #endregion

        protected void imgSubdetalle_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            Session["XidFideicomiso"] = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            Response.Redirect("wfCreditos.aspx");
        }


    }
}