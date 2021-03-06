﻿<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfCreditos.aspx.cs" Inherits="SCC.Formas.Fideicom.wfCreditos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">


    $(document).ready(function () {
        $('.campoNumerico').autoNumeric('init');
    });



    function fnc_Confirmar() {
        return confirm("¿Está seguro de eliminar el registro?");
    }

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">


    <div id="divDatos" runat="server" class="panel panel-success">
        <div class="panel-heading">
            

            <div class="row">
                <div class="col-md-10"><h3 class="panel-title">Créditos</h3></div>                
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/Fideicom/wfFideicomisos.aspx") %>" >Regresar</a></div>                

             </div>   
        </div>

        
        <asp:LinkButton ID="linkNew" runat="server" PostBackUrl="#" OnClick ="linkNew_Click">Nuevo</asp:LinkButton>  
        <p>
        <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server"  >
                <Columns>    
                    
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click"/>
                            <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClientClick="return fnc_Confirmar()" OnClick="imgBtnEliminar_Click"/>
                            
                        </ItemTemplate>
                        <HeaderStyle BackColor="#EEEEEE" />
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                    </asp:TemplateField>  
                                              
                    <asp:TemplateField HeaderText="Número de Contrato" SortExpression="Número de Contrato" >                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblNoContrato" runat="server" Text='<%# Bind("NumeroDeContrato") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Fecha del Contrato" SortExpression="Fecha del Contrato">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("FechaDelContrato","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
     
                    <asp:TemplateField HeaderText="Importe Contratado" SortExpression="Importe Contratado">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblImporte" runat="server" Text='<%# Bind("ImporteContratado","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fuente de Financiamiento" SortExpression="Fuente de Financiamiento">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblFuente" runat="server" Text='<%# Bind("FuenteDeFinanciamiento.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Destino de Financiamiento" SortExpression="Destino de Financiamiento">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblDestino" runat="server" Text='<%# Bind("DestinoDeFinanciamiento.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Periodo de Amortización" SortExpression="Periodo de Amortización">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblPeriodo" runat="server" Text='<%# Bind("PeriodoDeAmortizacion.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="N Periodos" SortExpression="N Periodos">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblNPeriodo" runat="server" Text='<%# Bind("NPeriodos") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>                    
        </asp:GridView>       

    </div>


    <div class="panel-footer alert alert-danger" id="divMSG" style="display:none" runat="server">
        <asp:Label ID="lblMSG" runat="server" Text=""></asp:Label>
    </div>

     <div id="divCaptura" runat="server" class="panel panel-success">


                            
                    <div class="panel-heading">
                        <h3 class="panel-title">Créditos</h3>
                    </div>
                    
                   <div class="row">

                     <div class="col-md-4">

                          <div class="form-group">
                               <label for="Numero">Número de Contrato</label>
                             <div>
                                <input type="text" class="input-sm required form-control" id="txtNumContrato" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNumContrato" ErrorMessage="El número de contrato es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div>
                     
                            <div class="form-group">
                               <label for="FechaContrato">Fecha de Contrato</label>
                             <div>
                                <input type="text" class="required form-control date-picker" id="dtpContrato" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtpContrato" ErrorMessage="La fecha del contrato es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div>                     

                         <div class="form-group">
                               <label for="tipoDeMoneda">Tipo de  Moneda</label>
                             <div>
                                <asp:DropDownList ID="ddlTipoDeMoneda" CssClass="form-control" runat="server"></asp:DropDownList>                            
                            </div>
                          </div>

                         <div class="form-group">
                               <label for="Cantidad">Cantidad</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtCantidad" runat="server" style="text-align: left; align-items:flex-start"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtCantidad" ErrorMessage="El campo cantidad es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div>

                          <div class="form-group">
                               <label for="Valor">Valor Actual en $ del tipo de moneda</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtValor" runat="server" style="text-align: left; align-items:flex-start"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtValor" ErrorMessage="El campo valor es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div>


                         <div class="form-group">
                               <label for="txtImporteTotal">Importe Contratado</label>
                             <div>                                
                                <input type="text" disabled="disabled" class="input-sm required form-control campoNumerico" id="txtImporteTotal" runat="server" style="text-align: left; align-items:flex-start"  />                                
                            </div>
                          </div>

                         <div class="form-group">
                             <label for="Destino">Destino del Crédito</label>
                             <div>
                                <textarea id="txtDestino" class="input-sm required form-control" runat="server"  rows="3" aria-autocomplete="none"></textarea>                           
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDestino" ErrorMessage="El campo Destino del Crédito es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div>

  
 

                     </div>

                     <div class="col-md-4">
                      

                          <div class="form-group">
                               <label for="Fuente">Fuente de Financiamiento</label>
                             <div>
                                <asp:DropDownList ID="ddlFuenteFinanciamiento" CssClass="form-control" runat="server"></asp:DropDownList>                            
                            </div>
                          </div>

                          <div class="form-group">
                               <label for="Destino">Destino de Financiamiento</label>
                             <div>
                                <asp:DropDownList ID="ddlDestinoFinanciamiento" CssClass="form-control" runat="server"></asp:DropDownList>                            
                            </div>
                          </div>

                         <div class="form-group">
                               <label for="PeriodoAmortizacion">Periodo de Amortización</label>
                             <div>
                                <asp:DropDownList ID="ddlPeriodoAmortizacion" CssClass="form-control" runat="server"></asp:DropDownList>                            
                            </div>
                          </div>


                         <div class="form-group">
                               <label for="NPeriodos">N Periodos</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtNPeriodos" runat="server" style="text-align: left; align-items:flex-start" data-m-dec="0"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNPeriodos" ErrorMessage="El campo de N periodos es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div>

                          <div class="form-group">
                               <label for="Inicio">Inicio</label>
                             <div>
                                <input type="text" class="required form-control date-picker" id="dtpInicio" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpInicio" ErrorMessage="La fecha de inicio es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div>

                          <div class="form-group">
                               <label for="Termino">Término</label>                          
                             <div>
                                <input type="text" class="input-sm required form-control date-picker" id="dtpTermino" runat="server" data-date-format = "dd/mm/yyyy" autocomplete="off" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="dtpTermino" ErrorMessage="La fecha de término es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div>

                     


                      </div>


                    <div class="col-md-4">      
                    
                        <div class="form-group">
                               <label for="TasaNormal">Tasa Normal</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtTasa" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="99"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtTasa" ErrorMessage="La tasa normal es un campo obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div>

                          <div class="form-group">
                               <label for="TasaMoratoria">Tasa Moratoria</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtTasaMoratoria" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="99"/>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTasaMoratoria" ErrorMessage="La tasa moratoria es un campo obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div> 

                          <div class="form-group">
                               <label for="PlazoInversion">Plazo de Inversión</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtPlazoInversion" runat="server" style="text-align: left; align-items:flex-start" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPlazoInversion" ErrorMessage="El Plazo de Inversión es un campo obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div>
 
                            <div class="form-group">
                               <label for="PlazaGracia">Plazo de Gracia</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtPlazoGracia" runat="server" style="text-align: left; align-items:flex-start" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtPlazoGracia" ErrorMessage="El plazo de gracia es un campo obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            </div>
                          </div>
                    
                                                   
                        <div class="form-group"  id="divBtnGuardarCredito" runat="server">
                            <asp:Button  CssClass="btn btn-primary" Text="Guardar" ID="btnGuardarContrato" runat="server" AutoPostBack="false" OnClick ="btnGuardarContrato_Click" ValidationGroup="validateX" />                        
                            <asp:Button  CssClass="btn btn-default" Text="Cancelar" Id="btnCancelar" runat="server" OnClick ="btnCancelar_Click" AutoPostBack="false" />
                        </div>
                    </div>                        

                   </div>
         
                    <div style="display:none" runat="server">
                        <asp:TextBox ID="_ElId" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                        <asp:TextBox ID="_Accion" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                    </div>

                   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" ViewStateMode="Disabled" />
           
        </div>




</div>
</asp:Content>
