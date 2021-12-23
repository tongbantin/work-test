<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR154IMPTFILE.aspx.cs" Inherits="TreasuryShadowSystem.TR154TRSSFTLN.frm_TR154IMPTFILE" %>
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
	                <asp:GridView ID="GridData" AutoGenerateColumns="false" CssClass="GridViewStyle" DataKeyNames="DealNo" runat="server">
                            <Columns> 
                                <asp:BoundField DataField="RECNO" ItemStyle-HorizontalAlign="Center" HeaderText="No." /> 
                                <asp:BoundField DataField="CUSTNAME" ItemStyle-HorizontalAlign="Center" HeaderText="Counter Party" /> 
                                <asp:BoundField DataField="BOOKNO" ItemStyle-HorizontalAlign="Center" HeaderText="หนังสือเลขที่" /> 
                                <asp:BoundField DataField="BOOKDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="หนังสือลงวันที่" /> 
                                <asp:BoundField DataField="LOANTYPE" ItemStyle-HorizontalAlign="Center" HeaderText="ประเภทสินเชื่อ" /> 
                                <asp:BoundField DataField="CUSTNAME" ItemStyle-HorizontalAlign="Center" HeaderText="ชื่อลูกค้า" />  
                                <asp:BoundField DataField="PVALUEDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="วันที่ชำระเงิน" /> 
                                <asp:BoundField DataField="PDEALNO" ItemStyle-HorizontalAlign="Center" HeaderText="เลขที่ตั๋ว(เก่า)" />   
                                <asp:BoundField DataField="PDEALDATE" DataFormatString="{0:dd/MM/yyyy}" NullDisplayText="" ItemStyle-HorizontalAlign="Center" HeaderText="วันที่เบิก" /> 
                                <asp:BoundField DataField="PMATDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="วันที่ครบกำหนด" />   
                                <asp:BoundField DataField="PDEALAMT" ItemStyle-HorizontalAlign="Center" HeaderText="จำนวนเงินกู้ (เดิม)" />   
                                <asp:BoundField DataField="PPAYAMT" ItemStyle-HorizontalAlign="Center" HeaderText="จำนวนเงินที่ชำระ" />   
                                <asp:BoundField DataField="DEALNO" ItemStyle-HorizontalAlign="Center" HeaderText="เลขที่ตั๋ว" />   
                                <asp:BoundField DataField="VALUEDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="วันที่เบิก / วันที่ชำระ / วันที่ต่อตั๋ว" />   
                                <asp:BoundField DataField="MATDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="วันที่ครบตั๋วใหม่" />   
                                <asp:BoundField DataField="AMOUNT" ItemStyle-HorizontalAlign="Center" HeaderText="จำนวนเงินกู้" />   
                                <asp:BoundField DataField="INTRATE" ItemStyle-HorizontalAlign="Center" HeaderText="อัตราดอกเบี้ย" />   
                                <asp:BoundField DataField="REFERPROJ" ItemStyle-HorizontalAlign="Center" HeaderText="โครงการ" />   
                                <asp:BoundField DataField="REFERDIV" ItemStyle-HorizontalAlign="Center" HeaderText="สาย" />   
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
        <asp:Button ID="btnSave" runat="server" Text="Generate File" Visible="false" 
                onclick="btnSave_Click" />
        </div>     
    </div>
</asp:Content>
