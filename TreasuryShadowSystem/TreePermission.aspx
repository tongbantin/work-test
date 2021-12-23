<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TreePermission.aspx.cs" Inherits="TreasuryShadowSystem.TreePermission" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>http://stackoverflow.com/a/10824885/315935</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/ss_style.css") %>" type="text/css" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/cupertino/jquery-ui-1.10.0.custom.css") %>" type="text/css" />
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/classic/style.css") %>" type="text/css" />
    
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Theme/Default/Javascript/jquery-1.8.2.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Theme/Default/Javascript/jquery.html5-placeholder.js") %>"></script>

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery.jqGrid-4.5.2/js/i18n/grid.locale-en.js") %>"></script>
    
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery.cookie.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery.hotkeys.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery.jstree.js") %>"></script>
    
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery-ui-1.10.0.custom.min.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery.blockUI.js") %>" ></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/json3.min.js") %>" ></script>
    <script type="text/javascript">
        $.jgrid.no_legacy_api = true;
        $.jgrid.useJSON = true;
    </script>

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jsTreePesmission.js") %>"></script>
</head>

<body>  
<form runat="server">
<div id="jstree_ctrs_header">User Group: 
    <asp:DropDownList ID="ddUserGroup" AutoPostBack="false" runat="server"></asp:DropDownList>
 <input type="button" id="btnsave" value="Assign Permission" />
</div>
<div id="jstree_ctrs_title"><p>Please match usergroup and permissions:</p></div>
<p>Home</p>
<div id="jstree_ctrs"></div>   

<div id="confirm_dialog" title="Confirmation Required" style="display:none">
  Are you sure assign new permission this?
</div>​
</form>
</body>
</html>