<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR137BOCPTYPE.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR137BOCPTYPE" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
         $(document).ready(function() {
            $('#ctl00_bodyContent_FromDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });    
            $('#ctl00_bodyContent_ToDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });    
            $('.thirdpartytypebox').change(function() {  
                if($('.thirdpartytypebox').val()=='Other') 
                    $('.otherbox').css('display','block');
                else
                    $('.otherbox').css('display','none');
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
			<h3 class="fl">Position Detail</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0">
	                    <tbody>            	                        	            
	                        <tr>
	                            <td class="td-display">ACUP</td>
	                            <td class="td-field">
	                                <select id="ThirdPartyTypeBox" runat="server" name="ThirdPartyTypeBox" class="thirdpartytypebox">
	                                    <option value="RelizedFX">Relized FX</option>
	                                    <option value="IntPNAsset">Int. PN-Asset</option>
	                                    <option value="IntPNLia">Int. PN-Liabilities</option>
	                                    <option value="IntRepoAsset">Int. Repo-Asset</option>
	                                    <option value="IntRepoLia">Int. Repo-Liabilities</option>
	                                    <option value="RelizedIRSCCS">Relized IRS/CCS</option>
	                                    <option value="GainLossBond">Gain/Loss Bond</option>
	                                    <option value="Other">Other</option>
	                                </select>
	                                </td>
	                        </tr>
	                         <tr>
	                            <td class="td-display">Date</td>
	                            <td class="td-field">       
	                                
	                                <asp:TextBox ID="FromDateBox" Runat="server"></asp:TextBox>-
	                                <asp:TextBox ID="ToDateBox" Runat="server"></asp:TextBox>
	                            </td>
	                        </tr>
	                         <tr>
	                            <td class="td-display">&nbsp;</td>
	                            <td class="td-field">       
	                                <textarea id="OtherBox" name="OtherBox" class="otherbox" style="display:none; width:800px;height:100px" runat="server"></textarea>
	                            </td>
	                        </tr>
	                        <tr>
	                            <td colspan="2" style="width:800px; text-align: left;">
	                                <asp:Button ID="btnSubmit" Text="Search" runat="server" 
                                        class="round blue button-submit" onclick="btnSubmit_Click"/>
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
	            <div style="overflow: scroll; height: 827px">
			        <div style="float:left"><asp:Button ID="ExportExcel" runat="server" Text="Excel" 
                            CssClass="round blue button-submit addposition" onclick="ExportExcel_Click" /></div>
			        <br /><br />			    	
                    <asp:GridView ID="GridData" CellPadding="3" PageSize="50" CellSpacing="3" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="True" runat="server">
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
                </div>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->		
	<asp:GridView ID="GridExport" Visible="false" CellPadding="4" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="True" 
        runat="server" ForeColor="#333333" Font-Names="Tahoma" Font-Size="10pt" GridLines="None">
        <RowStyle BackColor="#F7F6F3"  Font-Names="Tahoma" Font-Size="10pt" ForeColor="#333333" />                      
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="Black" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            No Data
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
</asp:Content>
