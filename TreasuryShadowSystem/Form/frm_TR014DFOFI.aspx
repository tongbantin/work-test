<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR014DFOFI.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR014DFOFI" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">
				
	    <div class="content-module-heading cf">
		
			<h3 class="fl">Confirmation_DL (Deposit)</h3>
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
					                <td colspan="3" class="td-field">
					                    <asp:TextBox ID="txtBranch" placeholder="e.g.01" runat="server" CssClass="round input-form-textbox"></asp:TextBox><asp:RequiredFieldValidator ID="requireBranch" ControlToValidate="txtBranch" EnableClientScript="true" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
					                </td>
					            </tr>
					            <tr>
					                <td class="td-display"><label for="simple-input">Transaction Date From:</label></td>
					                <td class="td-field"><input type="text" id="txtTransStartDate"  runat="server" class="round input-textbox-date" /></td>
					                <td class="td-display-small"><label for="simple-input">To:</label></td>
							        <td class="td-field"><input type="text" id="txtTransEndDate" runat="server" class="round input-textbox-date" /></td>
					            </tr>
					            <tr>
					                <td class="td-display"><label for="simple-input">Deal no. From:</label></td>
					                <td class="td-field"><input type="text" id="txtDealNoFrom" runat="server" class="round input-form-textbox" /></td>
					                <td class="td-display-small"><label for="simple-input">To:</label></td>
							        <td class="td-field"><input type="text" id="txtDealNoTo" runat="server" class="round input-form-textbox" /></td>
					            </tr>
					            <tr>
					                <td colspan="4" style="width:800px; text-align: left;"><asp:Button ID="btnSubmit" Text="Submit" runat="server" onclick="btnSubmit_Click" class="round blue button-submit" />
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
