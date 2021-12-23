<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_FXNostroUpload.aspx.cs" Inherits="TreasuryShadowSystem.TR145BORATEAVG.frm_FXNostroUpload" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">

    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <fieldset>
	    <table border="0">
	        <tbody> 
	            <tr>
	                <td class="td-display">ccy&nbsp;</td>
	                <td class="td-field">
	                    <asp:DropDownList ID="ddCurrency" runat="server">
	                        <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
	                        <asp:ListItem Text="EUR" Value="EUR"></asp:ListItem>
	                        <asp:ListItem Text="GBP" Value="GBP"></asp:ListItem>
	                        <asp:ListItem Text="CHF" Value="CHF"></asp:ListItem>
	                        <asp:ListItem Text="HKD" Value="HKD"></asp:ListItem>
	                        <asp:ListItem Text="SGD" Value="SGD"></asp:ListItem>
	                        <asp:ListItem Text="CAD" Value="CAD"></asp:ListItem>
	                        <asp:ListItem Text="AUD" Value="AUD"></asp:ListItem>
	                        <asp:ListItem Text="JPY" Value="JPY"></asp:ListItem>
	                        <asp:ListItem Text="CNH" Value="CNH"></asp:ListItem>
	                        <asp:ListItem Text="CNH_N" Value="CNH_N"></asp:ListItem>
	                        <asp:ListItem Text="USD_CHAT" Value="USD_CHAT"></asp:ListItem>
                        </asp:DropDownList>
	                </td>
	            </tr>
	            <tr>
	                <td class="td-display"><label for="simple-input">File:</label></td>
	                <td class="td-field">
	                    <asp:FileUpload ID="MyFile" class="round input-form-textbox" runat="server" />
	                </td>
	            </tr>
	            <tr>
	                <td colspan="2" style="width:800px; text-align: left;">
	                <asp:Button ID="btnSubmit" Text="Upload" runat="server" 
                            class="round blue button-submit" onclick="btnSubmit_Click" />
	                    <div id="divMsg" style="display:none;">
                            <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                        </div>    
	                </td>
	            </tr>
	            	            
	            <tr>
	                <td colspan="2" style="width:800px; text-align: left;">
	                    &nbsp;</td>
	            </tr>
	            	            
	        </tbody>
	    </table>
	    
	</fieldset>
	<div style="overflow: scroll; width:100%; height: 400px">
		<!--Version 1.0-->	
		<div style="width:100%">				
        <asp:GridView ID="GridData" AutoGenerateColumns="false" CssClass="GridViewStyle" DataKeyNames="NostAvgID" runat="server">
            <Columns> 
                <asp:BoundField DataField="VDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Value Date" /> 
                <asp:BoundField DataField="BUYAMT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Buy" /> 
                <asp:BoundField DataField="SELLAMT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Sell" /> 
                <asp:BoundField DataField="STATEDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Date Statement" /> 
                <asp:BoundField DataField="STATERATE" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N6}" HeaderText="Statement" /> 
                <asp:BoundField DataField="OPICSDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Date Opics" /> 
                <asp:BoundField DataField="OPICSRATE" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N6}" HeaderText="Opics" /> 
                <asp:BoundField DataField="STATEMOVE" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Movement Statement" /> 
                <asp:BoundField DataField="OPICSMOVE" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Movement Opics" /> 
                <asp:BoundField DataField="ADJDIFSPOTRATE" ItemStyle-HorizontalAlign="right" DataFormatString="{0:N2}" HeaderText="Diff.SpotRate" /> 
                <asp:BoundField DataField="ADJPLTHB" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="P/L THB" /> 
                <asp:BoundField DataField="BALANCECCY" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Balance CCY" /> 
                <asp:BoundField DataField="BALANCETHB" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Balance THB" /> 
                <asp:BoundField DataField="EXCHRATEAVG" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N6}" HeaderText="Exch Rate Avg." /> 
            </Columns>                
            <EmptyDataTemplate>
                No Data
            </EmptyDataTemplate>                    
            <RowStyle CssClass="RowStyle" Font-Size="8pt" Font-Names="Tahoma" />
            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
            <PagerStyle CssClass="PagerStyle" />
            <SelectedRowStyle CssClass="SelectedRowStyle" />
            <HeaderStyle CssClass="HeaderStyle"  Font-Size="7pt" Font-Names="Tahoma"/>
            <EditRowStyle CssClass="EditRowStyle" />
            <AlternatingRowStyle CssClass="AltRowStyle" />            
        </asp:GridView>
        <asp:Button ID="btnSave" runat="server" Text="Save Data" Visible="false"  
        onclick="btnSave_Click" />
        </div>     
    </div>
</asp:Content>
