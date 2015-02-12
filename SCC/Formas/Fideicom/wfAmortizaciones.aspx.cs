﻿using DataAccessLayer;
using DataAccessLayer.Models;
using BusinessLogicLayer;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SCC.Formas.Fideicom
{
    public partial class wfAmortizaciones : System.Web.UI.Page
    {
        private UnitOfWork uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                cargarCreditos();
            }
        }





        private void cargarCreditos()
        {
            
            List<Fideicomisos> listaFideicomisos = uow.FideicomisosBL.Get().ToList();



            int i = 0;
            foreach (Fideicomisos padre in listaFideicomisos)
            {
                i++;

                System.Web.UI.HtmlControls.HtmlGenericControl divPanel = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl divPanelHeading = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl divPanelCollapse = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl divPanelBody = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");

                System.Web.UI.HtmlControls.HtmlGenericControl h4 = new System.Web.UI.HtmlControls.HtmlGenericControl("H4");
                System.Web.UI.HtmlControls.HtmlGenericControl a = new System.Web.UI.HtmlControls.HtmlGenericControl("A");

                System.Web.UI.HtmlControls.HtmlGenericControl p = new System.Web.UI.HtmlControls.HtmlGenericControl("P");

                System.Web.UI.HtmlControls.HtmlGenericControl tabla = new System.Web.UI.HtmlControls.HtmlGenericControl("TABLE");


                //heading
                divPanelHeading.Attributes.Add("class", "panel-heading");

                h4.Attributes.Add("class", "panel-title");

                a.Attributes.Add("data-toggle", "collapse");
                a.Attributes.Add("data-parent", "#accordion");
                a.Attributes.Add("href", "#collapse" + i.ToString());
                a.InnerText = padre.Clave + " : " + padre.Nombre;

                h4.Controls.Add(a);
                divPanelHeading.Controls.Add(h4);


                //Collapse
                divPanelCollapse.Attributes.Add("id", "collapse" + i.ToString());
                divPanelCollapse.Attributes.Add("class", "panel-collapse collapse");


                divPanelBody.Attributes.Add("class", "panel-body");



                List<Creditos> detalle = uow.CreditosBL.Get(q => q.FideicomisoId == padre.Id).ToList();

                tabla.Attributes.Add("class", "table");
                tabla.Attributes.Add("cellspacing", "0");

                System.Web.UI.HtmlControls.HtmlGenericControl trHead = new System.Web.UI.HtmlControls.HtmlGenericControl("TR");
                System.Web.UI.HtmlControls.HtmlGenericControl thOne = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thTwo = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thThree = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thFour = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thFive = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thSix = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thSeven = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thEight = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");

                trHead.Attributes.Add("align", "center");


                thOne.InnerText = "No Contrato";
                thTwo.InnerText = "Fuente de Financiamiento";
                thThree.InnerText = "Destino de financiamiento";
                thFour.InnerText = "Fecha";
                thFive.InnerText = "Importe Contratado";
                thSix.InnerText = "Importe Amortizado";
                thSeven.InnerText = "Saldo";
                thEight.InnerText = "Amortizaciones";

                trHead.Controls.Add(thOne);
                trHead.Controls.Add(thTwo);
                trHead.Controls.Add(thThree);
                trHead.Controls.Add(thFour);
                trHead.Controls.Add(thFive);
                trHead.Controls.Add(thSix);
                trHead.Controls.Add(thSeven);
                trHead.Controls.Add(thEight);  

                tabla.Controls.Add(trHead);


                foreach (Creditos item in detalle)
                {

                    System.Web.UI.HtmlControls.HtmlGenericControl tr = new System.Web.UI.HtmlControls.HtmlGenericControl("TR");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdOne = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdTwo = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdThree = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdFour = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdFive = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdSix = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdSeven = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdEight = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");

                    tdOne.Attributes.Add("align", "left");
                    tdOne.InnerText = item.NumeroDeContrato;
                    tdTwo.InnerText = item.FuenteDeFinanciamiento.Nombre;
                    tdThree.InnerText = item.DestinoDeFinanciamiento.Nombre; 
                    tdFour.InnerText = item.FechaDelContrato.ToString("d");
                    tdFive.InnerText = item.ImporteContratado.ToString("C2");
                    tdSix.InnerText = "$000.00";
                    tdSeven.InnerText = "$000.00";
                    tdEight.InnerText = "...linkto";

                    tdFive.Attributes.Add("align", "right");
                    tdSix.Attributes.Add("align", "right");
                    tdSeven.Attributes.Add("align", "right");
 

                    tr.Controls.Add(tdOne);
                    tr.Controls.Add(tdTwo);
                    tr.Controls.Add(tdThree);
                    tr.Controls.Add(tdFour);
                    tr.Controls.Add(tdFive);
                    tr.Controls.Add(tdSix);
                    tr.Controls.Add(tdSeven);
                    tr.Controls.Add(tdEight);


                    tabla.Controls.Add(tr);
                }





                divPanelBody.Controls.Add(tabla);
                divPanelCollapse.Controls.Add(divPanelBody);


                //Agregar Elemento
                divPanel.Attributes.Add("class", "panel panel-default");
                divPanel.Controls.Add(divPanelHeading);
                divPanel.Controls.Add(divPanelCollapse);

                this.accordion.Controls.Add(divPanel);

            }


        }



    }
}