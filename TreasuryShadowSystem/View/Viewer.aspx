<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Viewer.aspx.cs" Inherits="TreasuryShadowSystem.View.Viewer" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="CrystalViewer" runat="server" EnableTheming="false" Width="100%" BorderWidth="1"
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
