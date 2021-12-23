<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR152OBOSWIFT.aspx.cs" Inherits="TreasuryShadowSystem.TR152OBOSWIFT.frm_TR152OBOSWIFT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $('#ctl00_bodyContent_FromDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('#ctl00_bodyContent_IncomingDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });    
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">FX Deal Matching(Deal and FX Confirmation)</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
					        <table>
	                             <tr>
	                                <td class="td-display">Counterparty</td>
	                                <td class="td-field">       
	                                    <asp:TextBox ID="CTPYBox" Runat="server"></asp:TextBox>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">Incoming Date</td>
	                                <td class="td-field">           	                                
	                                    <asp:TextBox ID="IncomingDateBox" Runat="server"></asp:TextBox>
	                                </td>
	                            </tr>
					            <tr>
					                <td>&nbsp;</td>
					                <td colspan="2">
					                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                                            class="round blue button-submit" onclick="btnSubmit_Click" /></td>
					            </tr>
					        </table>							
							
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
