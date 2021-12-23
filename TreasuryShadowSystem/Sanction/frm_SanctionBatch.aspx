<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_SanctionBatch.aspx.cs" Inherits="TreasuryShadowSystem.Sanction.frm_SanctionBatch" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script language="javascript">
        $(document).ready(function(){
            $('#ctl00_bodyContent_btnSubmit,#ctl00_bodyContent_btnSave').click(function() {             
                $('#divMsg').show();
            });
        });
    </script>
    <style>
        #divMsg
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <fieldset>
	    <table border="0">
	        <tbody>
	            <tr>
	                <td><label for="simple-input">Source:</label></td>
	                <td>
	                    <asp:DropDownList ID="MySource" runat="server">
	                        <asp:ListItem Text="RIA-Unicode (Default)" Value="RIA-Unicode"></asp:ListItem>
	                        <asp:ListItem Text="RIA-UTF8" Value="RIA-UTF8"></asp:ListItem>
	                        <asp:ListItem Text="MERCHANTS" Value="MERCHANTS"></asp:ListItem>
	                        <asp:ListItem Text="DOLLARSMART" Value="DOLLARSMART"></asp:ListItem>
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
        <asp:GridView ID="GridData" AutoGenerateColumns="false" CssClass="GridViewStyle" DataKeyNames="Records" runat="server">
            <Columns> 
                <asp:BoundField DataField="CORRESPONDENT_ID" HeaderText="CORRESPONDENT ID" />
                <asp:BoundField DataField="PAYING_AGENT_SEQUENCE_ID" HeaderText="PAYING AGENT SEQUENCE ID" />
                <asp:BoundField DataField="RIA_ORDER_ID" HeaderText="RIA ORDER ID" />
                <asp:BoundField DataField="ORDER_NUMBER" HeaderText="ORDER NUMBER" />
                <asp:BoundField DataField="ORDER_DATE" HeaderText="ORDER DATE" />
                <asp:BoundField DataField="CURRENCY_TO_PAY" HeaderText="CURRENCY TO PAY" />
                <asp:BoundField DataField="AMOUNT_TO_PAY" HeaderText="AMOUNT TO PAY" />
                <asp:BoundField DataField="CLAVE" HeaderText="CLAVE" />
                <asp:BoundField DataField="CORRESPONDENT_BRANCH" HeaderText="CORRESPONDENT BRANCH" />
                <asp:BoundField DataField="CORRESPONDENT_BRANCH_NUMBER" HeaderText="CORRESPONDENT BRANCH NUMBER" />
                <asp:BoundField DataField="CUSTOMER_FIRST_NAME" HeaderText="CUSTOMER FIRST NAME" />
                <asp:BoundField DataField="CUSTOMER_LAST_NAME_1" HeaderText="CUSTOMER LAST NAME 1" />
                <asp:BoundField DataField="CUSTOMER_LAST_NAME_2" HeaderText="CUSTOMER LAST NAME 2" />
                <asp:BoundField DataField="CUSTOMER_TEL_NO" HeaderText="CUSTOMER TEL NO" />
                <asp:BoundField DataField="CUSTOMER_ADDRESS" HeaderText="CUSTOMER ADDRESS" />
                <asp:BoundField DataField="CUSTOMER_CITY" HeaderText="CUSTOMER CITY" />
                <asp:BoundField DataField="CUSTOMER_STATE" HeaderText="CUSTOMER STATE" />
                <asp:BoundField DataField="CUSTOMER_ZIP_CODE" HeaderText="CUSTOMER ZIP CODE" />
                <asp:BoundField DataField="CUSTOMER_COUNTRY" HeaderText="CUSTOMER COUNTRY" />
                <asp:BoundField DataField="BENEFICIARY_FIRST_NAME" HeaderText="BENEFICIARY FIRST NAME" />
                <asp:BoundField DataField="BENEFICIARY_LAST_NAME_1" HeaderText="BENEFICIARY LAST NAME 1" />
                <asp:BoundField DataField="BENEFICIARY_LAST_NAME_2" HeaderText="BENEFICIARY LAST NAME_2" />
                <asp:BoundField DataField="BENEFICIARY_TEL_NO" HeaderText="BENEFICIARY TEL NO" />
                <asp:BoundField DataField="BENEFICIARY_ADDRESS" HeaderText="BENEFICIARY ADDRESS" />
                <asp:BoundField DataField="BENEFICIARY_CITY" HeaderText="BENEFICIARY CITY" />
                <asp:BoundField DataField="BENEFICIARY_STATE" HeaderText="BENEFICIARY STATE" />
                <asp:BoundField DataField="BENEFICIARY_ZIP_CODE" HeaderText="BENEFICIARY ZIP CODE" />
                <asp:BoundField DataField="BENEFICIARY_COUNTRY" HeaderText="BENEFICIARY COUNTRY" />
                <asp:BoundField DataField="MESSAGE_FOR_BENEFICIARY" HeaderText="MESSAGE FOR BENEFICIARY" />
                <asp:BoundField DataField="DELIVERY_METHOD" HeaderText="DELIVERY METHOD" />
                <asp:BoundField DataField="BANK_ACCOUNT_NUMBER" HeaderText="BANK ACCOUNT NUMBER" />
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
        </div> 
        <asp:HiddenField ID="MyFileLoad" runat="server" />
        <div id="divMsg" style="display:none;">
            <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
            
        </div>    
        <asp:Button ID="btnSave" runat="server" Text="Check Sanction" Visible="false"  
        onclick="btnSave_Click" />
        <asp:GridView ID="GridResult" AutoGenerateColumns="false"  CssClass="GridViewStyle" Visible="false" ShowFooter="true" runat="server">
            <Columns> 
                <asp:BoundField DataField="STATUS" HeaderText="Status" />
                <asp:BoundField DataField="RESULT" ItemStyle-HorizontalAlign="Right" HeaderText="Record(s)" />
            </Columns>                
            <EmptyDataTemplate>
                No Data
            </EmptyDataTemplate>                    
            <RowStyle CssClass="RowStyle" Font-Size="8pt" Font-Names="Tahoma" />
            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
            <PagerStyle CssClass="PagerStyle" />
            <SelectedRowStyle CssClass="SelectedRowStyle" />
            <HeaderStyle CssClass="HeaderStyle"  Font-Size="7pt" Font-Names="Tahoma"/>
            <FooterStyle CssClass="FooterStyle"  Font-Size="8pt" Font-Names="Tahoma" Font-Bold="true" ForeColor="White" BackColor="Blue"/>
            <EditRowStyle CssClass="EditRowStyle" />
            <AlternatingRowStyle CssClass="AltRowStyle" />            
        </asp:GridView>             
        <asp:Button ID="btnDownloadPass" runat="server" Text="Download Message Passed" Visible="false" onclick="btnDownloadPass_Click" />      
    </div>
</asp:Content>
