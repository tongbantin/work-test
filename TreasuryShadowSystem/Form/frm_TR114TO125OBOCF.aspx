<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR114TO125OBOCF.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR114TO125OBOCF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Confirmation IRS/CCS</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
							Customer Type						
							<asp:DropDownList ID="CustomerTypeBox" runat="server">
							    <asp:ListItem Text="Corporate" Value="CORP"></asp:ListItem>
							    <asp:ListItem Text="Life Insurance" Value="LINS"></asp:ListItem>
							    <asp:ListItem Text="Non Life Insurance" Value="NLINS"></asp:ListItem>
							    <asp:ListItem Text="Mutual Fund" Value="MFUND"></asp:ListItem>
							    <asp:ListItem Text="Private Fund" Value="PFUND"></asp:ListItem>
							</asp:DropDownList>
							<br />
							Deal No	
							<asp:TextBox ID="DealNoBox" runat="server"></asp:TextBox>					
					        <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                                class="round blue button-submit" onclick="btnSubmit_Click" />
					</fieldset>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf">
	
			<div class="half-size-column fl">
			
                <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="450px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
