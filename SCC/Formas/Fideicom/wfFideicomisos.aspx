<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfFideicomisos.aspx.cs" Inherits="SCC.Formas.Fideicom.wfFideicomisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">

    function fnc_Confirmar() {
        return confirm("¿Está seguro de eliminar el registro?");
    }

    </script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

    <div id="divDatos" runat="server" class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title">Fideicomisos</h3>
        </div>

        
        <asp:LinkButton ID="linkNew" runat="server" PostBackUrl="#" OnClick ="linkNew_Click">Nuevo</asp:LinkButton>  
        <p>
        <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server"  >
                <Columns>    
                    
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgSubdetalle" ToolTip="Detalle" runat="server" ImageUrl="~/img/Sub.png" OnClick="imgSubdetalle_Click" />                   
                            <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click"/>
                            <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClientClick="return fnc_Confirmar()" OnClick="imgBtnEliminar_Click"/>
                            
                        </ItemTemplate>
                        <HeaderStyle BackColor="#EEEEEE" />
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                    </asp:TemplateField>  
                                              
                    <asp:TemplateField HeaderText="Clave" SortExpression="Clave" >                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblClave" runat="server" Text='<%# Bind("Clave") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Nombre" SortExpression="Nombre">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
     
                    <asp:TemplateField HeaderText="Fiduciario" SortExpression="Fiduciario">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblFiduciario" runat="server" Text='<%# Bind("Financiera.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>                    
        </asp:GridView>       

    </div>

 
    <div id="divCaptura" runat="server" class="panel panel-success">

                    <div class="panel-footer alert alert-danger" id="divMSG" style="display:none" runat="server">
                                <asp:Label ID="lblMSG" runat="server" Text=""></asp:Label>
                    </div>
                            
                    <div class="panel-heading">
                        <h3 class="panel-title">Fideicomisos</h3>
                    </div>
                    
                   <div class="row">

                       
                       <div class="col-md-8">

                              <br />

                              <div class="form-group">
                                   <label for="Clave">No. de Fideicomiso</label>
                                   <div>
                                       <input type="text" class="input-sm required form-control" id="txtClave" runat="server" autocomplete="off" autofocus />                           
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClave" ErrorMessage="El campo clave es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                                   </div>
                              </div>
                            
                              <div class="form-group">
                                   <label for="Nombre">Nombre</label>
                                   <div>
                                       <textarea id="txtNombre" class="input-sm required form-control" runat="server"  rows="3" aria-autocomplete="none"></textarea>                           
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombre" ErrorMessage="El campo Nombre es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                                   </div>
                               </div>
     

                                <div class="form-group">
                                    <label>Fiduciario</label>
                                    <div>
                                        <asp:DropDownList ID="ddlFinanciera" CssClass="form-control" runat="server"></asp:DropDownList>                           
                                    </div>
                                </div>




                             <div class="form-group" id="divguardar" runat="server">
                                <asp:Button  CssClass="btn btn-primary" Text="Guardar" Id="btnGuardar" runat="server"   OnClick="btnGuardar_Click" AutoPostBack="false" ValidationGroup="validateX"/>
                                <asp:Button  CssClass="btn btn-default" Text="Cancelar" Id="btnCancelar" runat="server" OnClick ="btnCancelar_Click" AutoPostBack="false" />
                             </div>

                           <div style="display:none" runat="server">
                                <asp:TextBox ID="_ElId" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                                <asp:TextBox ID="_Accion" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                           </div>

                           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" ViewStateMode="Disabled" />


                       </div>

                   </div>
         

           
        </div>


</div>
</asp:Content>
