<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_BatchSchedule.aspx.cs" Inherits="TreasuryShadowSystem.Batch.frm_BatchSchedule" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function() {

            $('#ctl00_bodyContent_FromDate').datepicker({
                altFormat: 'yy-mm-dd',
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Rerun Batch Schedule</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
                            <table>   
	                             <tr>
	                                <td width="100px">Date</td>
	                                <td>           	                                
                                        <asp:TextBox ID="FromDate" runat="server"></asp:TextBox>
	                                </td>
	                            </tr>   
	                             <tr>
	                                <td width="100px">&nbsp;</td>
	                                <td>           	                          
                                        <asp:Button ID="btnRunBOTRate" Text="BOT Exchange Rate" runat="server" 
                                            onclick="btnRunBOTRate_Click" />
                                        <asp:Button ID="btnRunBOTLoanInterest" Text="BOT Loan Interest" runat="server" 
                                            onclick="btnRunBOTLoanInterest_Click" />
	                                </td>
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
                <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="500" runat="server" frameborder="0" marginwidth="1" ></iframe>
                
			</div>  
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
