<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_DebentureCloseBook.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_DebentureCloseBook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Outstanding Short-Terms/Long-Terms</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
					        <table>
					            <tr>
					                <td width="30">&nbsp;</td>
					                <td width="50">Date</td>
					                <td>
					                    <asp:DropDownList ID="MonthBox" runat="server">
							                <asp:ListItem Text="มกราคม" Value="01"></asp:ListItem>
							                <asp:ListItem Text="กุมภาพันธ์" Value="02"></asp:ListItem>
							                <asp:ListItem Text="มีนาคม" Value="03"></asp:ListItem>
							                <asp:ListItem Text="เมษายน" Value="04"></asp:ListItem>
							                <asp:ListItem Text="พฤษภาคม" Value="05"></asp:ListItem>
							                <asp:ListItem Text="มิถุนายน" Value="06"></asp:ListItem>
							                <asp:ListItem Text="กรกฏาคม" Value="07"></asp:ListItem>
							                <asp:ListItem Text="สิงหาคม" Value="08"></asp:ListItem>
							                <asp:ListItem Text="กันยายน" Value="09"></asp:ListItem>
							                <asp:ListItem Text="ตุลาคม" Value="10"></asp:ListItem>
							                <asp:ListItem Text="พฤศจิกายน" Value="11"></asp:ListItem>
							                <asp:ListItem Text="ธันวาคม" Value="12"></asp:ListItem>
							            </asp:DropDownList>
							            &nbsp;
							            <asp:DropDownList ID="YearBox" runat="server"></asp:DropDownList>	
					                </td>
					            </tr>
					            <!--
					            <tr>
					                <td>&nbsp;</td>
					                <td>&nbsp;</td>
					                <td> <div>
					                    <asp:RadioButtonList ID="ChoiceBox" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" Width="500" runat="server">
					                        <asp:ListItem Text="Short" Value="Short" Selected="True"></asp:ListItem>
					                        <asp:ListItem Text="Long" Value="Long"></asp:ListItem>
					                    </asp:RadioButtonList></div>
					                </td>
					            </tr>
					            -->
					            <tr>
					                <td>&nbsp;</td>
					                <td>&nbsp;</td>
					                <td> &nbsp;</td>
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
				
	</div> <!-- end content-module -->
</asp:Content>
