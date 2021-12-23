<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="TreasuryShadowSystem.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
body {
    margin: 0px;
    padding: 0px;
}
/* iframe's parent node */
div#root {
    /*position: fixed;*/
    width: 100%;
    height: 600px;
}

/* iframe itself */
div#root iframe {
    display: block;
    width: 100%;
    height: 100%;
    border: none;
}
</style>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div id="root">
    <iframe id="ifMain" name="ifMain" align="center" src="Form/frm_TR094OBOFXSW.aspx" frameborder="0" scrolling="auto" allowtransparency hidefocus></iframe>
    </div>
</asp:Content>
