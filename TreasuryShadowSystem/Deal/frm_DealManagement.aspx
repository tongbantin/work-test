<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_DealManagement.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_DealManagement" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $('#ctl00_bodyContent_InputDateSearchBox').datepicker({
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
			<h3 class="fl">Deal Management</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0">
	                    <tbody>            	                        	            
	                        <tr>
	                            <td class="td-display">Data Date</td>
	                            <td class="td-field">
	                                <asp:TextBox ID="InputDateSearchBox" Runat="server"></asp:TextBox></td>
	                        </tr>
	                        <tr>
	                            <td colspan="2" style="width:800px; text-align: left;">
	                                <asp:Button ID="btnSubmit" Text="Search" runat="server" 
                                        class="round blue button-submit" onclick="btnSubmit_Click" />
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
	            <div style="overflow: scroll; height: 400px">
			        
                    <asp:GridView ID="GridDeal" DataKeyNames="DealInfID" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderStyle  Width="50" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDeleteDocument" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DATADATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Data Date" /> 
                            <asp:BoundField DataField="BU" ItemStyle-HorizontalAlign="Center" HeaderText="BU" /> 
                            <asp:BoundField DataField="DEALID" ItemStyle-HorizontalAlign="Center" HeaderText="DealID" /> 
                            <asp:BoundField DataField="PS" ItemStyle-HorizontalAlign="Center" HeaderText="Purchase/Sell" /> 
                            <asp:BoundField DataField="PRODTYPE" ItemStyle-HorizontalAlign="Center" HeaderText="Product Type" /> 
                            <asp:BoundField DataField="CCYPAIR" ItemStyle-HorizontalAlign="Center" HeaderText="CCYPAIR" /> 
                            <asp:BoundField DataField="CCY" ItemStyle-HorizontalAlign="Center" HeaderText="CCY" /> 
                            <asp:BoundField DataField="NOTIONAL" ItemStyle-HorizontalAlign="Center" HeaderText="Notional" /> 
                            <asp:BoundField DataField="CMNE" ItemStyle-HorizontalAlign="Center" HeaderText="Customer" /> 
                            <asp:BoundField DataField="FLOWDATE" ItemStyle-HorizontalAlign="Left" HeaderText="FlowDate" /> 
                            <asp:BoundField DataField="FLOWAMOUNT" ItemStyle-HorizontalAlign="Center" HeaderText="FlowAmount" /> 
                        </Columns>                    
                        <EmptyDataTemplate>
                            No Data
                        </EmptyDataTemplate>                    
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                    </asp:GridView>
                    <asp:Button ID="btnDeleteSelected" Text="Delete by Selected" 
                    runat="server" class="round blue button-submit" Visible="true" 
                        OnClientClick="return confirm('Do you need deleting rows?')" 
                        onclick="btnDeleteSelected_Click" />
                </div>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->		
	
</asp:Content>
