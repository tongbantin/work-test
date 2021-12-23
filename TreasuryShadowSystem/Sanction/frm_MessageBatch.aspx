<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_MessageBatch.aspx.cs" Inherits="TreasuryShadowSystem.Sanction.frm_MessageBatch" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Message Batch</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
					        <table>
	                             <tr>
	                                <td class="td-display">&nbsp;Order No.</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="OrderNoBox" CssClass="orderno" runat="server">
    	                                </asp:DropDownList>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">Order Date</td>
	                                <td class="td-field">           	                                
	                                    <asp:TextBox ID="OrderDateBox" CssClass="orderdate" Runat="server"></asp:TextBox> 
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Correspondent Branch</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="CorespondentBranchBox" CssClass="correspondentbranch" runat="server">
    	                                </asp:DropDownList>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Customer Name</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="CustomerNameBox" CssClass="customername" runat="server">
    	                                </asp:DropDownList>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Customer Country</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="CustomerCountryBox" CssClass="customercountry" runat="server">
    	                                </asp:DropDownList>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Beneficiary Name</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="BeneficiaryNameBox" CssClass="beneficiaryname" runat="server">
    	                                </asp:DropDownList>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Message For Beneficiary</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="MessageBox" CssClass="message" runat="server">
    	                                </asp:DropDownList>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Account Number</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="AccountNumberBox" CssClass="accountnumber" runat="server">
    	                                </asp:DropDownList>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Amount</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="AmountBox" CssClass="amount" runat="server">
    	                                </asp:DropDownList>
	                                </td>
	                            </tr>
	                            <tr>
	                                <td class="td-display">&nbsp;</td>
	                                <td class="td-field">	                                    
                                        <asp:GridView ID="GridSanction" ShowHeader="true" CssClass="GridViewStyle" AutoGenerateColumns="false" EmptyDataText="Data Not Found" CellPadding="0" CellSpacing="0" BorderWidth="0" runat="server" DataKeyNames="CHECKINGID">
                                            <Columns>
                                                <asp:BoundField DataField="COND_NAME" HeaderText="Condition" ItemStyle-Wrap="false" />
                                                <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-Wrap="false" />
                                                <asp:BoundField DataField="MSG" HeaderText="Message" ItemStyle-Wrap="false" />
                                                <asp:BoundField DataField="DETAIL" HeaderText="Detail" ItemStyle-Wrap="true" />
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
	                                </td>
	                            </tr>
					            <tr>
					                <td>
					                    <asp:Button ID="RejectButton" Text="Reject" runat="server" 
                                        OnClientClick="if(!confirm('Do you Reject the transaction?')) return false;" 
                                        class="round button-warning" onclick="RejectButton_Click" />
					                </td>
					                <td colspan="2">
					                    <div style="float:left">
                                        <asp:Button ID="RecheckButton" Text="Recheck" runat="server" 
                                        OnClientClick="if(!confirm('Do you Recheck the transaction?')) return false;" 
                                        class="round button-submit" onclick="RecheckButton_Click" />
                                        </div>    
                                        <div style="float:right">
                                        <asp:Button ID="AcceptButton" Text="Accept" 
                                        runat="server" 
                                        OnClientClick="if(!confirm('Do you Accept the transaction?')) return false;" 
                                        class="round blue button-submit" onclick="AcceptButton_Click" />  	
                                        </div> 					                    
					                </td>
					            </tr>
					        </table>							
							
					</fieldset>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
			
	</div>
</asp:Content>
