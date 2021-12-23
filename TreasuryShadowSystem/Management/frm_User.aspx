<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_User.aspx.cs" Inherits="TreasuryShadowSystem.Management.frm_User" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/json3.min.js") %>" ></script>
    <script type="text/javascript">
        $.jgrid.no_legacy_api = true;
        $.jgrid.useJSON = true;
    </script>
    
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jqGridUserGroup.js") %>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
   
   <div class="content-module">
				
	    <div class="content-module-heading cf">
		
			<h3 class="fl">USER GROUP MANAGEMENT</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
			<!--<span class="fr expand-collapse-text">Click to collapse</span>
			<span class="fr expand-collapse-text initial-expand">Click to expand</span>-->
		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
	
			<div class="half-size-column fl">
				
	    		<table id="jqgrid_ctrs" cellpadding="0" cellspacing="0"></table>
                <div id="jqgrid_ctrs_pager" style="text-align:center;"></div>    
		    
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
    </div>
   
</asp:Content>
