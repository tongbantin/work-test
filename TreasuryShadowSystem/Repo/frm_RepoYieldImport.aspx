<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_RepoYieldImport.aspx.cs" Inherits="TreasuryShadowSystem.Repo.frm_RepoYieldImport" Title="Untitled Page" %>
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
	                <td></td>
	                <td>
	                    <div style="color:red">*กรณีมีข้อมูลในระบบอยู่แล้ว เมื่อนำเข้าข้อมูล 
                            ระบบจะลบข้อมูลวันที่เดิมที่ตรงกันออกก่อน และเพิ่มรายการใหม่ไปแทนที่*</div>
	                    <div style="color:red">**กรณีไมเคยมีรายการ ระบบจะเพิ่มรายการใหม่เข้าไป **</div>
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
        <asp:GridView ID="GridData" AutoGenerateColumns="false" DataKeyNames="Records" ShowHeader="true" runat="server">
            <Columns>
                <asp:BoundField DataField="DATA_DATE" HeaderText="Data Date" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="SYMBOL" HeaderText="Symbol" />
                <asp:BoundField DataField="MATURITY_DATE" HeaderText="Maturity" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="AVG_BIDDING" HeaderText="Avg.Bidding" />
                <asp:BoundField DataField="GOVT_INTERPOLATED_YIELD" HeaderText="Govt.Interpolated Yield" />
                <asp:BoundField DataField="TTM_YEAR" HeaderText="TTM (Yrs.)" />
                <asp:BoundField DataField="SPREAD_QUOTED_DATE" HeaderText="Spread Quoted Date" />
                <asp:BoundField DataField="SPREAD" HeaderText="Spread" />
                <asp:BoundField DataField="REFERENCE_YIELD" HeaderText="Reference Yield" />
                <asp:BoundField DataField="SETTLEMENT_DATE" HeaderText="Settlement Date" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="AI_PERCENTAGE" HeaderText="AI %" />
                <asp:BoundField DataField="GROSS_PRICE" HeaderText="Gross Price %" />
                <asp:BoundField DataField="CLEAN_PRICE" HeaderText="Clean Price %" />
                <asp:BoundField DataField="MODIFIED_DURATION" HeaderText="Modified Duration*" />
                <asp:BoundField DataField="CONVEXITY" HeaderText="Convexity" />
                <asp:BoundField DataField="INDEX_RATIO" HeaderText="Index Ratio" />
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
    </div>
</asp:Content>
