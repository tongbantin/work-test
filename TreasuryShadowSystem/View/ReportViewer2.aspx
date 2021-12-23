<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer2.aspx.cs" Inherits="TreasuryShadowSystem.View.ReportViewer2" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Treasury Shadow System(TSS) -- Kiatnakin Bank Public Co., Ltd.</title>    
    <style>
        * { padding: 0px; margin: 0px; font: 10pt tahoma; }        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="CrystalViewer" runat="server" EnableTheming="false" 
            ShowAllPageIds="true" HasDrillUpButton="False" HasCrystalLogo="False" 
            HasGotoPageButton="False" HasRefreshButton="False" HasSearchButton="False" 
            HasZoomFactorList="False" DisplayGroupTree="False" AutoDataBind="true" 
            HasToggleGroupTreeButton="False" HasViewList="False" 
            EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" 
            />
    </div>
    </form>
</body>
</html>
