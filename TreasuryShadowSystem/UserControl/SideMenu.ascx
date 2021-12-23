<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideMenu.ascx.cs" Inherits="TreasuryShadowSystem.UserControl.SideMenu" %>

<div id="leftsideBar" style="float:left;">    
    <div style="font:10pt tahoma; font-weight:bold; text-decoration:none;">
         <%--   <asp:PlaceHolder ID="PlaceHolderMenu" runat="server"></asp:PlaceHolder>--%>
        <asp:TreeView ID="TreeMenu" runat="server">
        </asp:TreeView>
    </div>
</div>
