<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR056OBODL.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR056OBODL" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="Panel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#ffffff" border="0" cellpadding="0" cellspacing="0" width="900">
            <tbody>
                <tr>
                    <td valign="top" align="center">
                        <table width="50%" style="border: 1px solid #999999;" cellpadding="5" cellspacing="0">
                            <tr>
                                <td bgcolor="#50A4D2" align="left"><asp:Label ID="lblCriteria" Text="Report Criteria" CssClass="title_table" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" border="0" cellpadding="5" cellspacing="0">
                                        <tr>
                                            <td align="right"><asp:Label ID="lblParam2" Text="Deal No. " runat="server"></asp:Label></td>
                                            <td align="left"><asp:TextBox ID="txtDealNo" runat="server" placeholder="e.g. 1000008"></asp:TextBox><asp:RequiredFieldValidator ID="requireDealNo" ControlToValidate="txtDealNo" Text="Field is require." runat="server"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td align="left"><asp:Button ID="btnSubmit" Text="Search" runat="server" 
                                                    onclick="btnSubmit_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br/>
                    </td>
                </tr>
            </tbody>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    
    <script type="text/javascript">

        $(document).ready(function() {


            
            $('#ctl00_bodyContent_btnSubmit').click(function() {
                //$(this).attr('disabled', 'disabled');

                if (BeforeSubmit()) {
                    $('#divMsg').show();
                    return true;
                }
                
            });
        });

        function BeforeSubmit() {
            if ($('#ctl00_bodyContent_txtDealNo').val() == "")
                return false;
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">

    <div class="content-module">
				
	    <div class="content-module-heading cf">
		
			<h3 class="fl">Deal Ticket Deposit & Loan</h3>
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
							
					        <asp:Button ID="btnSubmit" Text="Submit" runat="server" onclick="btnSubmit_Click" class="round blue button-submit" />
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