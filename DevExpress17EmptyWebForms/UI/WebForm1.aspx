<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DevExpress17EmptyWebForms.UI.WebForm1" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Data.Linq" Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" AutoGenerateColumns="False" DataSourceID="SqlDataSource" KeyFieldName="Id">
        <SettingsPager PageSize="15">
        </SettingsPager>
        <Settings ShowTitlePanel="True" />
        <SettingsBehavior AllowFocusedRow="True" />
        <SettingsSearchPanel Visible="True" />
        <SettingsText Title="Precios" />
        <Columns>
            <dx:GridViewCommandColumn ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" ShowDeleteButton="true" VisibleIndex="0">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="Id" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="CC" ShowInCustomizationForm="True" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Clave" ShowInCustomizationForm="True" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Descripcion" ShowInCustomizationForm="True" VisibleIndex="4">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FF" ShowInCustomizationForm="True" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Volumen1" ShowInCustomizationForm="True" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Precio1" ShowInCustomizationForm="True" VisibleIndex="7">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="MonedaContrato" ShowInCustomizationForm="True" VisibleIndex="8">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TipoDecambio" ShowInCustomizationForm="True" VisibleIndex="9">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Entregados" ShowInCustomizationForm="True" VisibleIndex="10">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:IepcdbConnectionString %>" DeleteCommand="DELETE FROM [Precios] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Precios] ([Id], [CC], [Clave], [Descripcion], [FF], [Volumen1], [Precio1], [MonedaContrato], [TipoDecambio], [Entregados]) VALUES (@Id, @CC, @Clave, @Descripcion, @FF, @Volumen1, @Precio1, @MonedaContrato, @TipoDecambio, @Entregados)" SelectCommand="SELECT * FROM [Precios]" UpdateCommand="UPDATE [Precios] SET [CC] = @CC, [Clave] = @Clave, [Descripcion] = @Descripcion, [FF] = @FF, [Volumen1] = @Volumen1, [Precio1] = @Precio1, [MonedaContrato] = @MonedaContrato, [TipoDecambio] = @TipoDecambio, [Entregados] = @Entregados WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Id" Type="Int32" />
            <asp:Parameter Name="CC" Type="Int32" />
            <asp:Parameter Name="Clave" Type="String" />
            <asp:Parameter Name="Descripcion" Type="String" />
            <asp:Parameter Name="FF" Type="String" />
            <asp:Parameter Name="Volumen1" Type="Decimal" />
            <asp:Parameter Name="Precio1" Type="Decimal" />
            <asp:Parameter Name="MonedaContrato" Type="String" />
            <asp:Parameter Name="TipoDecambio" Type="Decimal" />
            <asp:Parameter Name="Entregados" Type="Decimal" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="CC" Type="Int32" />
            <asp:Parameter Name="Clave" Type="String" />
            <asp:Parameter Name="Descripcion" Type="String" />
            <asp:Parameter Name="FF" Type="String" />
            <asp:Parameter Name="Volumen1" Type="Decimal" />
            <asp:Parameter Name="Precio1" Type="Decimal" />
            <asp:Parameter Name="MonedaContrato" Type="String" />
            <asp:Parameter Name="TipoDecambio" Type="Decimal" />
            <asp:Parameter Name="Entregados" Type="Decimal" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
