<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="Lista.aspx.cs" Inherits="SCC.Formas.opCreditos.Lista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script type="text/javascript">

            $(document).ready(function () {
                $('.campoNumerico').autoNumeric('init');
            });


        </script>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="divDatos" runat="server">    
    <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            
                            <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click" />
                            <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClick ="imgBtnEliminar_Click"/>
                            
                        </ItemTemplate>
                        <HeaderStyle BackColor="#EEEEEE" />
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                    </asp:TemplateField>                                  


                    <asp:TemplateField HeaderText="Financiera" SortExpression="Financiera">
                        <ItemTemplate>
                            <asp:Label ID="labelFinanciera" runat="server" Text='<%# Bind("Financiera.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="200px"  />
                    </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Número de Contrato" SortExpression="Número de Contrato">
                        <ItemTemplate>
                            <asp:Label ID="labelNoContrato" runat="server" Text='<%# Bind("NumeroDeContrato") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="200px"  />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Importe Contratado">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ImporteContratado")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>
  

                </Columns>
                    
                
                    
        </asp:GridView>
    
        <div id="divBotonNuevo" runat="server">
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-default"  AutoPostBack="false" OnClick="btnNuevo_Click" />
        </div>
    </div>


    



    <div id="divNuevoRegistro" runat="server" class="panel-footer" >

        <div class="panel panel-success">                
            <div class="panel-heading">
                <h3 class="panel-title">Indique los datos del nuevo registro</h3>
            </div>
        </div>


        <div class="row">


            <div class="col-md-4">

                <div class="form-group">
                    <label>Financiera</label>
                    <div>
                        <asp:DropDownList ID="ddlFinanciera" CssClass="form-control" runat="server"></asp:DropDownList>                           
                    </div>
                </div>


                <div class="form-group">
                    <label>Numero de Contrato</label>
                    <div>
                    <input type="text" class="input-sm required form-control" id="txtNumeroContrato" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                           
                </div>
                </div>


                <div class="form-group">
                    <label>Fecha del Contrato</label>
                    <div>
                    <input type="text" class="required form-control date-picker" id="dtpFechaContrato" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                    </div>
                </div>
                   

                <div class="form-group">
                    <label>Importe Contratado</label>
                    <div class="input-group">
                    <span class="input-group-addon">$</span>
                    <input type="text" class="input-sm required form-control campoNumerico" id="txtImporteContratado" runat="server" style="text-align: left; align-items:flex-start" />
                    </div>
                </div>


                <div class="form-group">
                    <label>Destino del Crédito</label>
                    <div>
                    <textarea id="txtDestinoDelCredito" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" rows="3"></textarea>
                </div>
                </div>
                      
                      
                     
            </div>                             
            <div class="col-md-4">

                

                <div class="form-group">
                    <label>Periodo de Amortización</label>
                    <div>
                        <asp:DropDownList ID="ddlPeriodoDeAmortizacion" CssClass="form-control" runat="server"></asp:DropDownList>                           
                    </div>
                </div>

                <div class="form-group">
                    <label>Número de Periodos</label>
                    <div>
                        <input type="text" class="input-sm required form-control campoNumerico" id="txtNPeriodos" runat="server" style="text-align: left; align-items:flex-start" />
                    </div>
                </div>

                <div class="form-group">
                    <label>Amortización</label>
                    <div class="input-group">
                    <span class="input-group-addon">$</span>
                    <input type="text" class="input-sm required form-control campoNumerico" id="txtAmortizacion" runat="server" style="text-align: left; align-items:flex-start" />
                    </div>
                </div>

                <div class="form-group">
                    <label>Inicio</label>
                    <div>
                    <input type="text" class="required form-control date-picker" id="dtpInicio" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                    </div>
                </div>
                
                <div class="form-group">
                    <label>Término</label>
                    <div>
                    <input type="text" class="required form-control date-picker" id="dtpTermino" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                    </div>
                </div>


            </div>
            <div class="col-md-4">

                <div class="form-group">
                    <label>Tasa Normal</label>
                    <div>
                        <input type="text" class="input-sm required form-control campoNumerico" id="txtTasa" runat="server" style="text-align: left; align-items:flex-start" />
                    </div>
                </div>

                <div class="form-group">
                    <label>Tasa Moratoria</label>
                    <div>
                        <input type="text" class="input-sm required form-control campoNumerico" id="txtTasaMoratoria" runat="server" style="text-align: left; align-items:flex-start" />
                    </div>
                </div>

                <div class="form-group">
                    <label>Plazo de Inversión</label>
                    <div>
                        <input type="text" class="input-sm required form-control" id="txtPlazoDeInversion" runat="server" style="text-align: left; align-items:flex-start" />
                    </div>
                </div>


                <div class="form-group">
                    <label>Periodo de Gracia</label>
                    <div>
                        <input type="text" class="input-sm required form-control" id="txtPeriodoDeGracia" runat="server" style="text-align: left; align-items:flex-start" />
                    </div>
                </div>


                <div class="form-group">
                    <label>Observaciones</label>
                    <div>
                    <textarea id="txtObservaciones" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" rows="3"></textarea>
                </div>
                </div>


            </div>
                 
                

            </div>

            

         
            
        




            <div class="col-md-2">              
                <div class="form-group" >.<br />
                    <asp:Button  CssClass="btn btn-default" Text="Guardar" ID="btnGuardar" runat="server" AutoPostBack="false" OnClick ="btnGuardar_Click" />
                    <asp:Button  CssClass="btn btn-default" Text="Cancelar" ID="btnCancelar" runat="server" AutoPostBack="false" OnClick="btnCancelar_Click" />                        
                </div>
            </div>

            <div style="display:none" runat="server">
                <asp:TextBox ID="_ElId" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>                
            </div>
    
        </div>

</asp:Content>
