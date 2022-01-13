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
        <SettingsSearchPanel Visible="True" />
        <SettingsText Title="DigitosMinistración" />

        <Columns>
            <dx:GridViewCommandColumn ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" ShowDeleteButton="true" VisibleIndex="0" Width="60px">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="idDigitoMinistracion" ReadOnly="True" Visible="False" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Codigo" ShowInCustomizationForm="True" VisibleIndex="1" Caption="Código">
                <PropertiesTextEdit>
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField ErrorText="* Requerido" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Descripcion" ShowInCustomizationForm="True" VisibleIndex="2" Caption="Descripción">
                <PropertiesTextEdit>
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField ErrorText="* Requerido" IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>

    <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:IepcdbConnectionString %>" 
        SelectCommand="sp_DigitosMinistracion" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="Consultar" Name="Op" Type="String"></asp:Parameter>
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
