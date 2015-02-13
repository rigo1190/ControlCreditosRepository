<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorCatalogos.Master" AutoEventWireup="true" CodeBehind="Municipios.aspx.cs" Inherits="SCC.Formas.Catalogos.Municipios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

    $(document).ready(function () {
                  $("#<%= txtNombre.ClientID %>").attr('maxlength', '100');
          });

          function fnc_Validar() {

              var desc = $("#<%=txtNombre.ClientID%>").val();
            if (desc == null || desc.length == 0 || desc == undefined) {
                alert("El campo nombre no se puede registrar vacio");
                return false;
            }
            return true;
        }

        function fnc_limpiarCampos() {
            $("#<%=  txtClave.ClientID%>").val("");
              $("#<%=  txtNombre.ClientID%>").val("");
              return true;
          }

          function fnc_OcultarDivs(sender) {
              $("#<%= divBtnNuevo.ClientID %>").css("display", "block");
            $("#<%= divEdicion.ClientID %>").css("display", "none");
            $("#<%= divMsg.ClientID %>").css("display", "none");
            $("#<%= divMsgSuccess.ClientID %>").css("display", "none");

            $("#<%=  txtClave.ClientID%>").val("");
            $("#<%=  txtNombre.ClientID%>").val("");


            return false;
        }

        function fnc_Confirmar() {
            return confirm("¿Está seguro de eliminar el registro?");
        }


        function fnc_EjecutarMensaje(mensaje) {
            alert(mensaje);
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="panel-footer alert alert-success" id="divMsgSuccess" style="display:none" runat="server">
                <asp:Label ID="lblMensajeSuccess" runat="server" Text=""></asp:Label>
    </div>
    <div class="panel-footer alert alert-danger" id="divMsg" style="display:none" runat="server">
                <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>
    </div>



    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title">CatMunicipios</h3>
        </div>

        <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" AllowPaging="True" OnPageIndexChanging="grid_PageIndexChanging"
        >
                <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                                                    
                            <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click"/>
                            <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClientClick="return fnc_Confirmar()" OnClick="imgBtnEliminar_Click"/>
                            
                        </ItemTemplate>
                        <HeaderStyle BackColor="#EEEEEE" />
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                    </asp:TemplateField>                                  


                    <asp:TemplateField HeaderText="Clave" SortExpression="Clave">
                        <EditItemTemplate>
                            <asp:TextBox CssClass="input-sm" ID="txtClave" runat="server" Text='<%# Bind("Clave") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Clave") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="100px"  />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Nombre" SortExpression="Nombre">
                        <EditItemTemplate>
                            <asp:TextBox CssClass="input-sm" ID="txtNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="labelNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                    
                <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                    
        </asp:GridView>

    </div>


    


    <div id="divBtnNuevo" runat="server">
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-default" OnClientClick="return fnc_limpiarCampos()" OnClick="btnNuevo_Click" AutoPostBack="false" />
    </div>
    <div class="row " > 
        <div id="divEdicion" runat="server" class="panel-footer">

                <div class="panel panel-success">                
                    <div class="panel-heading">
                            <h3 class="panel-title">Indique los datos del registro</h3>
                    </div>
                </div>

                <div class="row top-buffer">
                    <div class="col-md-2">
                        <label for="Clave">Clave</label>
                    </div>
                    <div class="col-md-2">
                        <input type="text" class="input-sm required" id="txtClave" runat="server" style="text-align: left;  align-items:flex-start" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClave" ErrorMessage="El campo clave es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                    </div>
                </div>


            
                <div class="row top-buffer">
                    <div class="col-md-2">
                        <label for="Nombre">Nombre</label>
                    </div>
                    <div class="col-md-2">
                        <input type="text" class="input-sm required" id="txtNombre" runat="server" style="text-align: left; width:800px;  align-items:flex-start" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNombre" ErrorMessage="El campo Nombre es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                                
                <div class="form-group">
                    <asp:Button  CssClass="btn btn-default" Text="Guardar" ID="btnCrear" runat="server" OnClick="btnCrear_Click" AutoPostBack="false" ValidationGroup="validateX" />
                    <asp:Button  CssClass="btn btn-default" Text="Cancelar" ID="btnCancelar" runat="server" OnClientClick="return fnc_OcultarDivs()" AutoPostBack="false" />
                </div>

                <div style="display:none" runat="server">
                    <asp:TextBox ID="_idMUN" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                    <asp:TextBox ID="_Accion" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                                    
                </div>

                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" ViewStateMode="Disabled" />

            </div>
    </div>


</asp:Content>
