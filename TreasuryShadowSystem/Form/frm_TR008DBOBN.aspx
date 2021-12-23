<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR008DBOBN.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR008DBOBN" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    
    <script type="text/javascript">

        $(document).ready(function() {
            
            $('#ctl00_bodyContent_txtReportDateFrom').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true
            });

            $('#ctl00_bodyContent_txtReportDateTo').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true
            });

            $('#ctl00_bodyContent_btnSubmit').click(function() {
                //$(this).attr('disabled', 'disabled');
                $('#divMsg').show();
                return true;
            });
        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">

    
    
    <div class="content-module">
				
	    <div class="content-module-heading cf">
		
			<h3 class="fl">Bond Movement</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
			
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
	
			<div  class="half-size-column fl">
				
					<fieldset>
					    <table border="0">
					        <tbody>
					           
					            <tr>
					                <td class="td-display"><label for="simple-input">Execute Date:</label></td>
					                <td colspan="3" class="td-field-large"><input type="text" id="txtReportDateFrom" runat="server" class="round input-textbox-date" /></td>
					                <td class="td-display"><label for="simple-input">To:</label></td>
					                <td class="td-field"><input type="text" id="txtReportDateTo" runat="server" class="round input-textbox-date" /></td>
					            </tr>
					              <tr>
					                <td colspan="4" style="width:800px; text-align: left;">
					                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" onclick="btnSubmit_Click" class="round blue button-submit" />
					                    <div id="divMsg" style="display:none;">
                                            <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                                        </div>    
					                </td>
					            </tr>
					            
					        </tbody>
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
			
                <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="500px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
	
	
						
</asp:Content>