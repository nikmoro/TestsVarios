<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="DevExpress17EmptyWebForms.UI.WebForm1" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Data.Linq" Assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="display: block">
        <asp:Label runat="server" ID="lblCatMsgError" ForeColor="Red"></asp:Label>
    </div>

    <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" AutoGenerateColumns="False"
        DataSourceID="SqlDataSource" KeyFieldName="idDigitoMinistracion"
        OnInitNewRow="ASPxGridView1_InitNewRow"
        OnRowInserting="ASPxGridView1_RowInserting"
        OnRowUpdating="ASPxGridView1_RowUpdating"
        OnRowDeleting="ASPxGridView1_RowDeleting">

        <SettingsPager PageSize="15">
        </SettingsPager>
        <Settings ShowTitlePanel="True" />
        <SettingsBehavior AllowFocusedRow="True" />
        <SettingsText Title="DigitosMinistración" />

        <Columns>
            <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True" Width="60px"></dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="idDigitoMinistracion" ReadOnly="True" VisibleIndex="0" Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Codigo" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Descripcion" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>

    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:IepcdbConnectionString %>"
        SelectCommand="sp_DigitosMinistracion" SelectCommandType="StoredProcedure"
        InsertCommand="sp_DigitosMinistracion" InsertCommandType="StoredProcedure"
        UpdateCommand="sp_DigitosMinistracion" UpdateCommandType="StoredProcedure"
        DeleteCommand="sp_DigitosMinistracion" DeleteCommandType="StoredProcedure">

        <SelectParameters>
            <asp:Parameter DefaultValue="Consultar" Name="Op" Type="String" />
        </SelectParameters>

        <UpdateParameters>
            <asp:Parameter DefaultValue="Actualizar" Name="Op" Type="String" />
        </UpdateParameters>

        <InsertParameters>
            <asp:Parameter DefaultValue="Nuevo" Name="Op" Type="String" />
        </InsertParameters>

        <DeleteParameters>
            <asp:Parameter DefaultValue="EliminarVirtual" Name="Op" Type="String" />
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>
