using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SCC.Formas.Creditos
{
    public partial class Lista : System.Web.UI.Page
    {
        private UnitOfWork uow;

        private int idMunicipio;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            idMunicipio = int.Parse(Session["MunicipioId"].ToString());


            if (!IsPostBack)
            {
                BindGrid();
            } 
        }



        private void BindGrid()
        {
            uow = new UnitOfWork();
            
            this.grid.DataSource = uow.CreditosBL.Get(p=>p.MunicipioId == idMunicipio).ToList();
            this.grid.DataBind();
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {

        }

    }
}