<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR134BOLCR.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR134BOLCR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function() {

            $('#ctl00_bodyContent_txtExecuteDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">รายงานหุ้นกู้ระยะสั้น</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
					        <table>
					            <tr>
					                <td>&nbsp;</td>
					                <td>&nbsp;</td>
					                <td> <div>
					                    <asp:RadioButtonList ID="ChoiceBox" RepeatDirection="Horizontal" 
                                            CellPadding="0" CellSpacing="0" Width="500" runat="server" >
					                        <asp:ListItem Text="By CounterParty" Value="Acct" Selected="True"></asp:ListItem>
					                        <asp:ListItem Text="By Currency" Value="Risk"></asp:ListItem>
					                        <asp:ListItem Text="Detail" Value="Detail"></asp:ListItem>
					                    </asp:RadioButtonList></div>
					                </td>
					            </tr>
					            <tr>
					                <td width="30">&nbsp;</td>
					                <td width="50">Execute Date</td>
					                <td>
					                    <input type="text" id="txtExecuteDate" size="15" runat="server" class="round input-form-textbox" /><asp:RequiredFieldValidator ID="requireDealNo" ControlToValidate="txtExecuteDate" Text="*" runat="server"></asp:RequiredFieldValidator>
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
</asp:Content>
