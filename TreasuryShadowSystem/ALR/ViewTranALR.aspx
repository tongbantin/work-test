<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="ViewTranALR.aspx.cs" Inherits="TreasuryShadowSystem.ALR.ViewTranALR" Title="Untitled Page" %>
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
			<h3 class="fl">View Transaction To ALR</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0">
	                    <tbody>           	                        	            
	                         <tr>
	                            <td class="td-display">Transaction Date</td>
	                            <td class="td-field">       
	                                
	                                <asp:TextBox ID="FromDateBox" Runat="server"></asp:TextBox>
	                                <!--<asp:TextBox ID="ToDateBox" Runat="server"></asp:TextBox>-->
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
			        <div>&nbsp;</div>		
			        <div class="half-size-column fl">		    	
                    <asp:GridView ID="GridData" CellPadding="3" CellSpacing="3" PageSize="10" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                        <Columns>                           
                            <asp:BoundField DataField="BU_CODE" ItemStyle-HorizontalAlign="Center" HeaderText="BUCode" />
                            <asp:BoundField DataField="DEALNO" ItemStyle-HorizontalAlign="Center" HeaderText="Dealno" /> 
                            <asp:BoundField DataField="SETTDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="SettleDate" /> 
                            <asp:BoundField DataField="SEND_ACCT_NO" ItemStyle-HorizontalAlign="Center" HeaderText="Send Acctno." /> 
                            <asp:BoundField DataField="SEND_ACCT_NAME" ItemStyle-HorizontalAlign="Center" HeaderText="Send Acctname" /> 
                            <asp:BoundField DataField="SEND_TAXID" ItemStyle-HorizontalAlign="Center" HeaderText="Send Taxid" /> 
                            <asp:BoundField DataField="SEND_CUST_TYPE" ItemStyle-HorizontalAlign="Center" HeaderText="Send Custtype" /> 
                            <asp:BoundField DataField="REV_ACCT_NO" ItemStyle-HorizontalAlign="Center" HeaderText="Receive Acctno." />                            
                            <asp:BoundField DataField="REV_ACCT_NAME" ItemStyle-HorizontalAlign="Center" HeaderText="Receive Acctname" />                            
                            <asp:BoundField DataField="REV_TAXID" ItemStyle-HorizontalAlign="Center" HeaderText="Receive Taxid." />
                            <asp:BoundField DataField="REV_CUST_TYPE" ItemStyle-HorizontalAlign="Center" HeaderText="Receive Custtype" /> 
                            <asp:BoundField DataField="TRANS_AMT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HeaderText="Amount"  />                            
                            <asp:BoundField DataField="CUST_CODE" ItemStyle-HorizontalAlign="Left" HeaderText="Custcode" /> 
                            <asp:BoundField DataField="CUST_BUILDINGNAME" ItemStyle-HorizontalAlign="Left" HeaderText="Building" /> 
                            <asp:BoundField DataField="CUST_ROOMNO" ItemStyle-HorizontalAlign="Left" HeaderText="Room" /> 
                            <asp:BoundField DataField="CUST_FLOORNO" ItemStyle-HorizontalAlign="Left" HeaderText="Floor" /> 
                            <asp:BoundField DataField="CUST_ADDNO" ItemStyle-HorizontalAlign="Left" HeaderText="Address No." /> 
                            <asp:BoundField DataField="CUST_MOONO" ItemStyle-HorizontalAlign="Left" HeaderText="Moo" /> 
                            <asp:BoundField DataField="CUST_SOI" ItemStyle-HorizontalAlign="Left" HeaderText="Soi" />                  
                            <asp:BoundField DataField="CUST_STREETNAME" ItemStyle-HorizontalAlign="Left" HeaderText="Street" /> 
                            <asp:BoundField DataField="CUST_TUMBON" ItemStyle-HorizontalAlign="Left" HeaderText="Tumbon" /> 
                            <asp:BoundField DataField="CUST_AMPHUR" ItemStyle-HorizontalAlign="Left" HeaderText="Amphur" /> 
                            <asp:BoundField DataField="CUST_PROVINCE" ItemStyle-HorizontalAlign="Left" HeaderText="Province" />  
                            <asp:BoundField DataField="CUST_POSTCODE" ItemStyle-HorizontalAlign="Left" HeaderText="Postcode" />   
                            <asp:BoundField DataField="REFER" ItemStyle-HorizontalAlign="Left" HeaderText="Refer" />
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
                    </div> <!-- end half-size-column -->                                   
                </div>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->		
	
</asp:Content>
