<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridMenu.aspx.cs" Inherits="TreasuryShadowSystem.GridMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>http://stackoverflow.com/a/10824885/315935</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/cupertino/jquery-ui-1.10.0.custom.css") %>" type="text/css" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Scripts/jquery.jqGrid-4.5.2/css/ui.jqgrid.css") %>" type="text/css"  />
    
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Theme/Default/Javascript/jquery-1.8.2.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Theme/Default/Javascript/jquery.html5-placeholder.js") %>"></script>

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery.jqGrid-4.5.2/js/i18n/grid.locale-en.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery.jqGrid-4.5.2/js/jquery.jqGrid.min.js") %>"></script>
    
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery-ui-1.10.0.custom.min.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery.blockUI.js") %>" ></script>
  
    <script type="text/javascript">
        $.jgrid.no_legacy_api = true;
        $.jgrid.useJSON = true;
    </script>

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jqGridMenu.js") %>"></script>
</head>

<body>
<table id="jqgrid_ctrs" cellpadding="0" cellspacing="0"></table>
<div id="jqgrid_ctrs_pager" style="text-align:center;"></div>    

</body>
</html>