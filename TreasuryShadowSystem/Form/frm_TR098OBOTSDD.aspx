﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR098OBOTSDD.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR098OBOTSDD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">TSD Fee Summary</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
							Month
							<asp:DropDownList ID="MontlyBox" runat="server"></asp:DropDownList>
							<asp:DropDownList ID="YearBox" runat="server"></asp:DropDownList> 							
							&nbsp;Expense
							<asp:TextBox ID="ExpenseBox" Text="10000" runat="server"></asp:TextBox>							
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
			
                <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="420px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
