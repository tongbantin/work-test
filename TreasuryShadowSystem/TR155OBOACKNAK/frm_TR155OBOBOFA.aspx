<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR155OBOBOFA.aspx.cs" Inherits="TreasuryShadowSystem.TR155OBOACKNAK.frm_TR155OBOBOFA" %>
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
	                <asp:GridView ID="GridData" AutoGenerateColumns="false" CssClass="GridViewStyle" runat="server">
                            <Columns> 
                                <asp:BoundField DataField="ASOF" ItemStyle-HorizontalAlign="Center" HeaderText="As Of" /> 
                                <asp:BoundField DataField="CCY" ItemStyle-HorizontalAlign="Center" HeaderText="Currency" /> 
                                <asp:BoundField DataField="BANK_ID_TYPE" ItemStyle-HorizontalAlign="Center" HeaderText="Bank ID Type" /> 
                                <asp:BoundField DataField="BANK_ID" ItemStyle-HorizontalAlign="Center" HeaderText="Bank ID" /> 
                                <asp:BoundField DataField="ACCOUNT" ItemStyle-HorizontalAlign="Center" HeaderText="Account" /> 
                                <asp:BoundField DataField="DATA_TYPE" ItemStyle-HorizontalAlign="Left" HeaderText="Data Type" />  
                                <asp:BoundField DataField="BAI_CODE" ItemStyle-HorizontalAlign="Left" HeaderText="BAI Code" /> 
                                <asp:BoundField DataField="DESCRIPTION" ItemStyle-HorizontalAlign="Left" HeaderText="Description" />   
                                <asp:BoundField DataField="AMOUNT" ItemStyle-HorizontalAlign="Right" HeaderText="Amount" /> 
                                <asp:BoundField DataField="VALUE_DATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Balance/Value Date" />   
                                <asp:BoundField DataField="CUST_REFERENCE" ItemStyle-HorizontalAlign="Left" HeaderText="Customer Reference" />   
                                <asp:BoundField DataField="IMMEDIATE_AVAILABILITY" ItemStyle-HorizontalAlign="Left" HeaderText="Immediate Availability" />   
                                <asp:BoundField DataField="DAY1_FLOAT" ItemStyle-HorizontalAlign="Left" HeaderText="1Day Float" />   
                                <asp:BoundField DataField="DAY2_FLOAT" ItemStyle-HorizontalAlign="Left" HeaderText="+2Day Float" />   
                                <asp:BoundField DataField="BANK_REFERENCE" ItemStyle-HorizontalAlign="Left" HeaderText="Bank Reference" />   
                                <asp:BoundField DataField="OF_ITEMS" ItemStyle-HorizontalAlign="Left" HeaderText="Of Item" />   
                                <asp:BoundField DataField="TEXT_DESCRIPTION" ItemStyle-HorizontalAlign="Left" HeaderText="Text" />  
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
	<div style="overflow: scroll; width:100%; height: 400px">
		<!--Version 1.0-->	
		<div style="width:100%">				
        <asp:Button ID="btnSave" runat="server" Text="Save Data" Visible="false" 
                onclick="btnSave_Click" />
        </div>     
    </div>
</asp:Content>
