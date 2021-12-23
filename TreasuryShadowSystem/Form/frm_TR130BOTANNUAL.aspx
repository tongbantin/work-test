<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR130BOTANNUAL.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR130BOTANNUAL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">
		
	    <div class="content-module-heading cf">		
			<h3 class="fl">Confirmation FX Bond</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
							ปี						
							<asp:DropDownList ID="YearBox" runat="server">
							</asp:DropDownList>			
							<br />
							รายงาน 
							<asp:RadioButtonList ID="RangeTimeBox" RepeatDirection="Horizontal" runat="server">
							    <asp:ListItem Text="รายงานไตรมาส 1" Value="Quarter1" Selected="True"></asp:ListItem>
							    <asp:ListItem Text="รายงานไตรมาส 2" Value="Quarter2"></asp:ListItem>
							    <asp:ListItem Text="รายงานไตรมาส 3" Value="Quarter3"></asp:ListItem>
							    <asp:ListItem Text="รายงานไตรมาส 4" Value="Quarter4"></asp:ListItem>
							    <asp:ListItem Text="รายงานสรุปรายปี" Value="SummaryYear"></asp:ListItem>
							</asp:RadioButtonList>
							ประเภทรายงาน
							<asp:RadioButtonList ID="TypeBox" RepeatDirection="Horizontal" runat="server">
							    <asp:ListItem Text="สรุป" Value="Summary" Selected="True"></asp:ListItem>
							    <asp:ListItem Text="แยกรายการ" Value="Transaction"></asp:ListItem>
							</asp:RadioButtonList>
							<br />
					        <asp:Button ID="btnSubmit" Text="Search" runat="server" 
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
