<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="Lista.aspx.cs" Inherits="SCC.Formas.Creditos.Lista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

</asp:Content>
