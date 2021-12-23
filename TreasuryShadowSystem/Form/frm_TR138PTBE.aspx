<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR138PTBE.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR138PTBE" Title="Untitled Page" %>
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
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Be Phatra Report</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0">
	                    <tbody>            	                        	            
	                        <tr>
	                            <td class="td-display">Report</td>
	                            <td class="td-field">
	                                <select id="ReportBox" runat="server" name="ReportBox">
	                                    <option value="PTSUMMARY">รายงานคุมตั๋วแลกเงินภัทร</option>
	                                    <option value="PTCONFBE">รายงานยืนยันรับฝากตั๋ว</option>
	                                    <option value="PTOBE">รายงานแสดง Outstanding ตั๋ว BE</option>
	                                    <option value="PTMBE">รายงานแสดงรายชื่อตั๋วที่ใกล้ครบกำหนด</option>
	                                    <option value="PTFEEBE">รายงานแสดงค่าธรรมเนียมเรียกเก็บค่ารับฝากตั๋วแลกเงิน</option>
	                                    <option value="PTINVOICE">ออกใบแจ้งหนี้</option>
	                                    <option value="PTSECINV">ค่าธรรมเนียมการออกตั๋วแลกเงิน</option>
	                                    <option value="PTSECBN">ค่าธรรมเนียมการโอนเงิน</option>
	                                </select>
	                                </td>
	                        </tr>
	                         <tr>
	                            <td class="td-display">Date</td>
	                            <td class="td-field">   
	                                <asp:TextBox ID="FromDateBox" Runat="server" autocomplete="off"></asp:TextBox> - <asp:TextBox ID="ToDateBox" Runat="server" autocomplete="off"></asp:TextBox>
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
			
                <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="450px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->	
</asp:Content>
