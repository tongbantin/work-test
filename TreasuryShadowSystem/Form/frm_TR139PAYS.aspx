<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR139PAYS.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR139PAYS" Title="Untitled Page" %>
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
            $('#ctl00_bodyContent_ToDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            }); 
             $('#ctl00_bodyContent_ProductBox').change(function(){
             if (($('#ctl00_bodyContent_ProductBox').val() == "MATURED") || ($('#ctl00_bodyContent_ProductBox').val() == "IRDV") || ($('#ctl00_bodyContent_ProductBox').val() == "SW"))
                {   
                    $('#ctl00_bodyContent_ConditionBox').css('visibility','hidden');                    
                } else {
                    $('#ctl00_bodyContent_ConditionBox').css('visibility','visible');  
                }
             });

             if (($('#ctl00_bodyContent_ProductBox').val() == "MATURED") || ($('#ctl00_bodyContent_ProductBox').val() == "IRDV") || ($('#ctl00_bodyContent_ProductBox').val() == "SW"))
                {   
                    $('#ctl00_bodyContent_ConditionBox').css('visibility','hidden');                    
                } else {
                    $('#ctl00_bodyContent_ConditionBox').css('visibility','visible');  
                }
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Payment Report (FI,EQ,SWAP)</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
					        <table>
	                             <tr>
	                                <td class="td-display">Product</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="ProductBox" runat="server">
    	                                    <asp:ListItem Text="Fix Income" Value="FI"></asp:ListItem>
    	                                    <asp:ListItem Text="Fix Income-Matured" Value="MATURED"></asp:ListItem>
    	                                    <asp:ListItem Text="Equities" Value="EQ"></asp:ListItem>
    	                                    <asp:ListItem Text="Interest/Dividend" Value="IRDV"></asp:ListItem>
    	                                    <asp:ListItem Text="Swap" Value="SW"></asp:ListItem>
    	                                </asp:DropDownList>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="ConditionBox" runat="server">
    	                                    <asp:ListItem Text="By Trade Date" Value="TradeDate"></asp:ListItem>
    	                                    <asp:ListItem Text="By Settlement Date" Value="SettlementDate"></asp:ListItem>
    	                                </asp:DropDownList>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">Date</td>
	                                <td class="td-field">           	                                
	                                    <asp:TextBox ID="FromDateBox" Runat="server"></asp:TextBox>-
	                                    <asp:TextBox ID="ToDateBox" Runat="server"></asp:TextBox>
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
