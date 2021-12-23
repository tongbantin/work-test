﻿<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR025OITAL.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR025OITAL" Title="Untitled Page" %>
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
                    <table width="80%" style="border: 1px solid #eeeeee;" cellpadding="5" cellspacing="0">
                        <tr>
                            <td bgcolor="#50A4D2" align="left"><asp:Label ID="lblCriteria" Text="Report Criteria" CssClass="title_table" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellpadding="5" cellspacing="0">
                                    <tr>
                                        <td width="20%" align="right"><asp:Label ID="lblParam1" Text="Branch " runat="server"></asp:Label></td>
                                        <td width="80%" align="left" colspan="3"><asp:TextBox ID="txtBranch" placeholder="e.g. 01" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorBranch" ControlToValidate="txtBranch" EnableClientScript="true" ErrorMessage="Field is required." runat="server"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td width="20%" align="right"><asp:Label ID="lblParam2" Text="Transaction Date From " runat="server"></asp:Label></td>
                                        <td width="30%" align="left"><asp:Calendar ID="calTransFrom" runat="server"></asp:Calendar></td>
                                        <td width="10%" align="center"><asp:Label ID="lblParam3" Text="To " runat="server"></asp:Label></td>
                                        <td width="40%" align="left"><asp:Calendar ID="calTransTo" runat="server"></asp:Calendar></td>
                                    </tr>
                                    <tr>
                                        <td align="right"><asp:Label ID="lblParam4" Text="Cash Interface Status " runat="server"></asp:Label></td>
                                        <td align="left" colspan="3">
                                            <asp:DropDownList ID="ddCashIStatus" runat="server">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Success" Value="S"></asp:ListItem>
                                                <asp:ListItem Text="Fail" Value="F"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right"><asp:Label ID="lblParam5" Text="Securities Interface Status " runat="server"></asp:Label></td>
                                        <td align="left" colspan="3">
                                            <asp:DropDownList ID="ddSecuritiesIStatus" runat="server">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Success" Value="S"></asp:ListItem>
                                                <asp:ListItem Text="Fail" Value="F"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right"><asp:Label ID="lblParam6" Text="Third-Party Status " runat="server"></asp:Label></td>
                                        <td align="left" colspan="3">
                                            <asp:DropDownList ID="ddThirdParty" runat="server">
                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                                <asp:ListItem Text="S" Value="S"></asp:ListItem>
                                                <asp:ListItem Text="I" Value="I"></asp:ListItem>
                                                <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
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
                    <br />
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


            $('#ctl00_bodyContent_txtTransStartDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true
            });
            $('#ctl00_bodyContent_txtTransEndDate').datepicker({
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
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">
				
	    <div class="content-module-heading cf">
		
			<h3 class="fl">Statement Report for payments before eod</h3>
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
					                <td class="td-display"><label for="simple-input">Cash Interface Status:</label></td>
					                <td class="td-field">
					                    <asp:DropDownList ID="ddCashIStatus" runat="server" CssClass="round input-from-dropdownlist">
                                            <asp:ListItem Text="Please select..." Value=""></asp:ListItem>
                                            <asp:ListItem Text="Success" Value="S"></asp:ListItem>
                                            <asp:ListItem Text="Fail" Value="F"></asp:ListItem>
                                        </asp:DropDownList>
					                </td>
					                <td class="td-display-small"><label for="simple-input">Securities Interface Status:</label></td>
							        <td class="td-field">
							            <asp:DropDownList ID="ddSecuritiesIStatus" runat="server" CssClass="round input-from-dropdownlist">
                                                <asp:ListItem Text="Please select..." Value=""></asp:ListItem>
                                                <asp:ListItem Text="Success" Value="S"></asp:ListItem>
                                                <asp:ListItem Text="Fail" Value="F"></asp:ListItem>
                                            </asp:DropDownList>
							        </td>
					            </tr>
					            <tr>
					                <td class="td-display"><label for="simple-input">Third-Party Status:</label></td>
					                <td class="td-field" colspan="3">
					                    <asp:DropDownList ID="ddThirdParty" runat="server" CssClass="round input-from-dropdownlist">
                                                <asp:ListItem Text="Please select..." Value=""></asp:ListItem>
                                                <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                                <asp:ListItem Text="S" Value="S"></asp:ListItem>
                                                <asp:ListItem Text="I" Value="I"></asp:ListItem>
                                                <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                            </asp:DropDownList>
					                </td>
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