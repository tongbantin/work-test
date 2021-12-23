<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TC022OBOBE1.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TC022OBOBE" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    
    <script type="text/javascript">

        $(document).ready(function() {


            $('#ctl00_bodyContent_txtMaturityDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
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
<!--
<asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="Panel1" runat="server">
    <ContentTemplate>
    <table align="center" bgcolor="#ffffff" border="0" cellpadding="0" cellspacing="0" width="900">
    <tbody>
        <tr>
            <td valign="top" align="center">
                <table width="80%" style="border: 1px solid #999999;" cellpadding="5" cellspacing="0">
                    <tr>
                        <td bgcolor="#50A4D2" align="left"><asp:Label ID="lblCriteria" Text="Report Criteria" CssClass="title_table" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0" cellpadding="5" cellspacing="0">
                                <tr>
                                    <td width="20%" align="right"><asp:Label ID="lblParam1" Text="Branch " runat="server"></asp:Label></td>
                                    <td width="80%" align="left" ><asp:TextBox ID="txtBranch1" placeholder="e.g. 01" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorBranch" ControlToValidate="txtBranch" EnableClientScript="true" ErrorMessage="Field is required." runat="server"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td align="right"><asp:Label ID="lblParam2" Text="Counter Party" runat="server"></asp:Label></td>
                                    <td align="left" ><asp:DropDownList ID="ddParty" CssClass="input-xxlarge" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidatorParty" ControlToValidate="ddParty" EnableClientScript="true" ErrorMessage="Field is required." runat="server"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <td width="15%" align="right"><asp:Label ID="lblParam3" Text="Maturity Date " runat="server"></asp:Label></td>
                                    <td width="30%" align="left"><asp:Calendar ID="calMaturityFrom" runat="server"></asp:Calendar></td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td align="left">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td align="left">
                                        <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                                            Text="Search" />
                                    </td>
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
-->
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
					                <td colspan="4"></td>
					            </tr>
					            <tr>
					                <td style="width:200px; text-align: left;"><label for="simple-input">Branch</label></td>
					                <td colspan="3" style="width:400px; text-align: left;">
					                    <asp:TextBox ID="txtBranch" placeholder="e.g. 01" runat="server" CssClass="round input-form-textbox"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorBranch" ControlToValidate="txtBranch" EnableClientScript="true" ErrorMessage="Field is required." runat="server"></asp:RequiredFieldValidator>
                                    </td>
					            </tr>
					            <tr>
					                <td style="width:200px; text-align: left;"><label for="simple-input">Counter Party</label></td>
					                <td style="width:200px; text-align: left;"><asp:DropDownList ID="ddParty" CssClass="round input-textbox-date" runat="server"></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidatorParty" ControlToValidate="ddParty" EnableClientScript="true" ErrorMessage="Field is required." runat="server"></asp:RequiredFieldValidator></td>
					                <td style="width:100px; text-align: left;"><label for="simple-input">Maturity Date</label></td>
							        <td style="width:200px; text-align: left;"><input type="text" id="txtMaturityDate" runat="server" class="round input-textbox-date" onclick="return txtMaturityDate_onclick()" /></td>
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
			
                <iframe id="iReport" src="<%# TheUrl %>"  width="1000px" height="500px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
	
</asp:Content>
