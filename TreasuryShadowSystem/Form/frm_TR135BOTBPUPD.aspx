<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR135BOTBPUPD.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR135BOTBPUPD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <fieldset>
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
                            class="round blue button-submit" onclick="btnSubmit_Click"/>
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
        <asp:GridView ID="GridData" AutoGenerateColumns="false" ShowHeader="true" runat="server">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>#</HeaderTemplate>
                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DataSetDate" HeaderText="DataSetDate" />
                <asp:BoundField DataField="Month" HeaderText="Month" />
                <asp:BoundField DataField="TradingBookPosition" HeaderText="TradingBookPosition" />
                <asp:BoundField DataField="OutstandingAmount" HeaderText="OutstandingAmount" />
            </Columns>
            <HeaderStyle Height="30" />
            <SelectedRowStyle BackColor="Aqua" />
        </asp:GridView>
                   
        <asp:Button ID="btnSave" runat="server" Text="Save" Visible="false" 
            onclick="btnSave_Click" />
    </div>
</asp:Content>
