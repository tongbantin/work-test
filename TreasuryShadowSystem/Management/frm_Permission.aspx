<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_Permission.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_Permission" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/json3.min.js") %>" ></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jsTreePesmission.js") %>"></script>
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
				
	    		<div id="jstree_ctrs_header">
	    		    <label for="simple-input">User Group:</label>
                    <asp:DropDownList ID="ddUserGroup" AutoPostBack="false" runat="server" CssClass="round input-from-dropdownlist"></asp:DropDownList>
                    <input type="button" id="btnsave" value="Assign Permission" class="round blue button-submit" />
                </div>
                <div id="jstree_ctrs_title"><p>Please match usergroup and permissions:</p></div>
                <p>Home</p>
                <div id="jstree_ctrs"></div>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
    </div>
    <div id="confirm_dialog" title="Confirmation Required" style="display:none">
      Are you sure assign new permission this?
    </div>
</asp:Content>
