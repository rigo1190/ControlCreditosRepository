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
    public partial class Ejercicios : System.Web.UI.Page
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
            gridEjercicios.DataSource = uow.EjercicioBL.Get().ToList();
            gridEjercicios.DataBind();
        }

        private bool ValidarInsercion(int añoSeñalado, Ejercicio objEjercicio = null)
        {
            Ejercicio obj = null;

            if (objEjercicio == null)
                obj = uow.EjercicioBL.Get(e => e.Año == añoSeñalado).FirstOrDefault();
            else
                if (añoSeñalado != objEjercicio.Año)
                    obj = uow.EjercicioBL.Get(e => e.Año == añoSeñalado).FirstOrDefault();


            return obj == null;
        }

        private bool ValidarEliminarEjercicio(Ejercicio obj)
        {
            return true;
        }

        private void BindControles()
        {
            int idEjercicio = Utilerias.StrToInt(_IDEjercicio.Value);

            Ejercicio obj = uow.EjercicioBL.GetByID(idEjercicio);

            txtAnio.Value = obj.Año.ToString();
            txtFactor.Value = obj.FactorIva.ToString();
            radioStatus.SelectedValue = obj.Estatus == enumEstatusEjercicio.Activo ? "1" : "2";
            txtAnio.Disabled = !ValidarEliminarEjercicio(obj);

        }

        protected void gridEjercicios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgBtnEliminar = (ImageButton)e.Row.FindControl("imgBtnEliminar");

                int id = Utilerias.StrToInt(gridEjercicios.DataKeys[e.Row.RowIndex].Values["Id"].ToString());

                Ejercicio obj = uow.EjercicioBL.GetByID(id);

                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                lblStatus.Text = obj.Estatus == enumEstatusEjercicio.Activo ? "Activo" : "Cerrado";

                if (imgBtnEliminar != null)
                    imgBtnEliminar.Attributes.Add("onclick", "fnc_ColocarID(" + id + ")");

            }
        }

        protected void gridEjercicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridEjercicios.PageIndex = e.NewPageIndex;
            BindGrid();

            divEncabezado.Style.Add("display", "block");
            divCaptura.Style.Add("display", "none");
            divMsgError.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _IDEjercicio.Value = gridEjercicios.DataKeys[row.RowIndex].Value.ToString();
            _Accion.Value = "A";

            BindControles();

            divCaptura.Style.Add("display", "block");
            divEncabezado.Style.Add("display", "none");
            divMsgError.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Ejercicio obj;
            int idEjercicio = Utilerias.StrToInt(_IDEjercicio.Value);
            string M = string.Empty;

            if (_Accion.Value.Equals("N"))
                obj = new Ejercicio();
            else
                obj = uow.EjercicioBL.GetByID(idEjercicio);

            if (!ValidarInsercion(Utilerias.StrToInt(txtAnio.Value), !_Accion.Value.Equals("N") ? obj : null))
            {
                divMsgError.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");
                divEncabezado.Style.Add("display", "none");
                divCaptura.Style.Add("display", "block");

                lblMsgError.Text = "Ya existe registro para el AÑO escrito. Intente con otros valores.";

                return;
            }


            obj.Año = Utilerias.StrToInt(txtAnio.Value);
            obj.FactorIva = Convert.ToDecimal(txtFactor.Value);
            obj.Estatus = radioStatus.SelectedValue.Equals("1") ? enumEstatusEjercicio.Activo : enumEstatusEjercicio.Cerrado;

            if (_Accion.Value.Equals("N"))
                uow.EjercicioBL.Insert(obj);
            else
                uow.EjercicioBL.Update(obj);
            
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

            int idEjercicio = Utilerias.StrToInt(_IDEjercicio.Value);

            Ejercicio obj = uow.EjercicioBL.GetByID(idEjercicio);

            if (!ValidarEliminarEjercicio(obj))
            {
                M = "No se puede eliminar el registro, se encuentra en uso por otros módulos.";
                lblMsgError.Text = M;
                divMsgError.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");
                divEncabezado.Style.Add("display", "block");
                divCaptura.Style.Add("display", "none");
                return;
            }

            uow.EjercicioBL.Delete(obj);
            uow.SaveChanges();

            if (uow.Errors.Count > 0) //Si hubo errores
            {
                M = string.Empty;
                foreach (string cad in uow.Errors)
                    M += cad;

                lblMsgError.Text = M;
                divMsgError.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");
                return;
            }


            BindGrid();

            lblMsgSuccess.Text = M;
            divMsgError.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "block");
            divEncabezado.Style.Add("display", "block");
            divCaptura.Style.Add("display", "none");
        }
    }
}