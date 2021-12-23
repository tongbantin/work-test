<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_DealUpload.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_DealUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <fieldset>
        <legend>Deal without Opics</legend>
	    <table border="0">
	        <tbody>
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
	            	            
	        </tbody>
	    </table>
	    
	</fieldset>
	<div style="overflow: scroll; height: 400px">
		<!--Version 1.0-->			
        <asp:GridView ID="GridData" AutoGenerateColumns="false" DataKeyNames="Records" ShowHeader="true" runat="server">
            <Columns>
                <asp:BoundField DataField="DATADATE" HeaderText="DataDate" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="BU" HeaderText="BU" />
                <asp:BoundField DataField="DEALID" HeaderText="DealID" />
                <asp:BoundField DataField="PS" HeaderText="PS" />
                <asp:BoundField DataField="PRODTYPE" HeaderText="ProdType" />
                <asp:BoundField DataField="CCYPAIR" HeaderText="CCYPAIR" />
                <asp:BoundField DataField="CCY" HeaderText="CCY" />
                <asp:BoundField DataField="NOTIONAL" HeaderText="Notional" />
                <asp:BoundField DataField="CMNE" HeaderText="CounterParty" />
                <asp:BoundField DataField="FLOWDATE" HeaderText="Flow Date" />
                <asp:BoundField DataField="FLOWAMOUNT" HeaderText="Float Amount" />
            </Columns>
            <SelectedRowStyle BackColor="Aqua" />
        </asp:GridView>
                   
        <asp:Button ID="btnSave" runat="server" Text="Save" Visible="false"  
        onclick="btnSave_Click" />
    </div>
</asp:Content>
