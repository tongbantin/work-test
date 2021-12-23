<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TC022OBOBE.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TC022OBOBE1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function() {


            $('#ctl00_bodyContent_txtMaturityDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true
            });
            
            

            $('#ctl00_bodyContent_btnSubmit').click(function() {
                //$(this).attr('disabled', 'disabled');
                if (BeforeSubmit()) {
                    $('#divMsg').show();
                    return true;
                }
            });
        });

        function BeforeSubmit() {
            if ($('#ctl00_bodyContent_txtBranch').val() == "")
                return false;
        }
        function txtMaturityDate_onclick() {

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">
				
	    <div class="content-module-heading cf">
		
			<h3 class="fl">Confirmation_DL (Withdraw)</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
			<!--<span class="fr expand-collapse-text">Click to collapse</span>
			<span class="fr expand-collapse-text initial-expand">Click to expand</span>-->
		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
	
			<div class="half-size-column fl">
				
					<fieldset>
					    <table border="0">
					        <tbody>
					           
					            <tr>
					                <td class="td-display"><label for="simple-input">Branch:</label></td>
					                <td colspan="3" class="td-field-large">
					                    <asp:TextBox ID="txtBranch" placeholder="e.g. 01" runat="server" CssClass="round input-form-textbox"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorBranch" ControlToValidate="txtBranch" EnableClientScript="true" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
                                    </td>
					            </tr>
					            <tr>
					                <td class="td-display"><label for="simple-input">Counter Party:</label></td>
					                <td class="td-field"><asp:DropDownList ID="ddParty" CssClass="round input-from-dropdownlist" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidatorParty" ControlToValidate="ddParty" EnableClientScript="true" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator></td>
					                <td class="td-display"><label for="simple-input">Maturity Date:</label></td>
							        <td class="td-field"><input type="text" id="txtMaturityDate" runat="server" class="round input-textbox-date" onclick="return txtMaturityDate_onclick()" /></td>
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
