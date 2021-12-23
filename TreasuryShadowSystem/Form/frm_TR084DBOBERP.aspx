<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR084DBOBERP.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR084DBOBERP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function() {

            $('#ctl00_bodyContent_txtFromDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
                        
        });
        $(document).ready(function() {

            $('#ctl00_bodyContent_txtToDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
                        
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">
				
	    <div class="content-module-heading cf">
		
			<h3 class="fl">084 - BE Report</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
	
			<div class="half-size-column fl">
				
					<fieldset>
					    <table border="0">
					        <tbody>
					           
					            <tr>
					                <td class="td-display"><asp:Label ID="lblParam2" Text="Branch " runat="server"></asp:Label></td>
					                <td colspan="3" class="td-field-large">
					                    <asp:TextBox ID="txtBranch" runat="server" placeholder="e.g. 01" CssClass="round input-form-textbox"></asp:TextBox><asp:RequiredFieldValidator ID="requireBranch" ControlToValidate="txtBranch" Text="Field is require." runat="server"></asp:RequiredFieldValidator>					                    
                                    </td>
					            </tr>
					            <tr>
					                <td class="td-display"><asp:Label ID="Label2" Text="Customer No. " runat="server"></asp:Label></td>
					                <td class="td-field" colspan="3"><input type="text" id="txtCustomer" size="15" runat="server" class="round input-form-textbox" /></td>					                
					            </tr>
					            <tr>
					                <td class="td-display"><asp:Label ID="Label1" Text="From Date " runat="server"></asp:Label></td>
					                <td class="td-field" colspan="3">
					                    <input type="text" id="txtFromDate" size="15" runat="server" class="round input-form-textbox" />
					                    To 
					                    <input type="text" id="txtToDate" size="15" runat="server" class="round input-form-textbox" />
					                </td>					                
					            </tr>
					              <tr>
					                <td colspan="4" style="width:800px; text-align: left;">
					                    <asp:Button ID="Button1" Text="Submit" runat="server" onclick="btnSubmit_Click" class="round blue button-submit" />
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
