<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="TreasuryShadowSystem.View.ReportViewer" %>
<%@ Register src="~/UserControl/Footer.ascx" tagname="Footer" tagprefix="ucf" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
        <CR:CrystalReportViewer ID="CrystalViewer" runat="server" EnableTheming="false" 
    ShowAllPageIds="true" HasDrillUpButton="False" HasCrystalLogo="False" 
    HasGotoPageButton="False" HasRefreshButton="False" HasSearchButton="False" 
    HasZoomFactorList="False" DisplayGroupTree="False" AutoDataBind="true" 
    HasToggleGroupTreeButton="False" HasViewList="False"/>
    
</asp:Content>