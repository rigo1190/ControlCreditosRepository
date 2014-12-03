using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SCC.Formas.Catalogos
{
    public partial class Municipios : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack) 
            {
                BindGrid();
                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");

                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "none");
            }           
        }


        private void BindGrid()
        {
            this.grid.DataSource = uow.MunicipioBL.Get().ToList();
            this.grid.DataBind();
        }


               

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            _Accion.Text = "Nuevo";

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
                        
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _idMUN.Text= grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            _Accion.Text = "update";

            Municipio mun = uow.MunicipioBL.GetByID(int.Parse(_idMUN.Text));
            BindCatalogo(mun);


            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");

            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _idMUN.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            Municipio mun = uow.MunicipioBL.GetByID(int.Parse(_idMUN.Text));



             
            uow.MunicipioBL.Delete(mun);
            uow.SaveChanges();
            


            if (uow.Errors.Count == 0)
            {
                BindGrid();
                lblMensajeSuccess.Text = "El registro se ha eliminado correctamente";

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");

                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "block");

            }

            else
            {
                string mensaje;

                divMsg.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");

                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";

                lblMensajes.Text = mensaje;
            }
        }

        

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            
            Municipio mun;
            List<Municipio> lista;
            string mensaje = "";
            int orden;

            if (_Accion.Text == "Nuevo")
                mun = new Municipio();
            else
                mun = uow.MunicipioBL.GetByID(int.Parse(_idMUN.Text));





            mun.Clave = txtClave.Value;
            mun.Nombre = txtNombre.Value;

            if (_Accion.Text == "Nuevo") {
                lista = uow.MunicipioBL.Get().ToList();
                orden = lista.Max(p => p.Orden);
                orden++;

                mun.Orden = orden;
            }




            //Validaciones
            uow.Errors.Clear();
            if (_Accion.Text == "Nuevo")
            {

                lista = uow.MunicipioBL.Get(p => p.Clave == mun.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.MunicipioBL.Get(p => p.Nombre == mun.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El Nombre que capturo ya ha sido registrada anteriormente, verifique su información");

                
                uow.MunicipioBL.Insert(mun);
                mensaje = "El nuevo municipio ha sido registrado correctamente";




            }
            else//Update
            {

                int xid;

                xid = int.Parse(_idMUN.Text);

                lista = uow.MunicipioBL.Get(p => p.Id != xid && p.Clave == mun.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.MunicipioBL.Get(p => p.Id != xid && p.Nombre == mun.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El Nombre que capturo ya ha sido registrada anteriormente, verifique su información");

                uow.MunicipioBL.Update(mun);
                mensaje = "Los cambios se registraron satisfactoriamente";
            }





            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_EjecutarMensaje('" + mensaje + "')", true);
                txtClave.Value = string.Empty;
                txtNombre.Value = string.Empty;
                
                BindGrid();

                lblMensajeSuccess.Text = mensaje;

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");
                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "block");

            }
            else
            {
                divMsg.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");

                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";



                lblMensajes.Text = mensaje;

            }
        }




        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            BindGrid();

            divBtnNuevo.Style.Add("display", "block");

            divEdicion.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }




        public void BindCatalogo(Municipio UP)
        {
            txtClave.Value = UP.Clave;
            txtNombre.Value = UP.Nombre;
            _idMUN.Text = UP.Id.ToString();
        }
       

       


    }
}