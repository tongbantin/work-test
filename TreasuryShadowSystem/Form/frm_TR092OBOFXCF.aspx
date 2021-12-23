<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR092OBOFXCF.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR092OBOFXCF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">
		
			<h3 class="fl">FX Confirmation for SPOT</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
			<!--<span class="fr expand-collapse-text">Click to collapse</span>
			<span class="fr expand-collapse-text initial-expand">Click to expand</span>-->
		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
						<p>
							<label for="simple-input">Deal No</label>
							<asp:TextBox ID="txtDealNo" runat="server" placeholder="e.g. 1000008" CssClass="round input-form-textbox"></asp:TextBox><asp:RequiredFieldValidator ID="requireDealNo" ControlToValidate="txtDealNo" Text="*" runat="server"></asp:RequiredFieldValidator>
							
					        <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                                class="round blue button-submit" onclick="btnSubmit_Click" />
                             &nbsp;
                            <asp:Button ID="btnViewPass" Text="View Password" runat="server" 
                                class="round blue button-submit" onclick="btnViewPass_Click" />
                              &nbsp;
                            <asp:Button ID="btnSendMail" Text="Send Email" runat="server" 
                                class="round blue button-submit" onclick="btnSendMail_Click" />
					        <div id="divMsg" style="display:none;">
                                <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                            </div>    
						</p>
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
