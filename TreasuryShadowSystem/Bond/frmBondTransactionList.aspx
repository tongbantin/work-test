<%@ Page EnableSessionState="True" Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frmBondTransactionList.aspx.cs" Inherits="TreasuryShadowSystem.Bond.frmBondTransactionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">

    <script type="text/javascript">

        $(document).ready(function() {

            $('#ctl00_bodyContent_txtTransDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
        });            

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    	<fieldset>
		    <table border="0">
		        <tbody>
		            
		            <tr>
		                <td class="td-display"><label for="simple-input">Deal No:</label></td>
		                <td class="td-field">
		                    <asp:TextBox ID="txtDealNo" placeholder="1000000" runat="server" CssClass="round input-form-textbox"></asp:TextBox>
		                </td>
		            </tr>
		            <tr>
		                <td class="td-display"><label for="simple-input">Transaction Date:</label></td>
		                <td class="td-field"><input type="text" id="txtTransDate" name="txtTransDate"  runat="server" class="round input-textbox-date" /></td>
		            </tr>
		            <tr>
		                <td colspan="2" style="width:800px; text-align: left;"><asp:Button ID="btnSubmit" 
                                Text="Submit" runat="server" class="round blue button-submit" 
                                onclick="btnSubmit_Click" />
		                    <div id="divMsg" style="display:none;">
                                <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                            </div>    
		                </td>
		            </tr>
		            
		        </tbody>
		    </table>
		    
		</fieldset>
    <asp:GridView ID="GridData" AutoGenerateColumns="false" CellPadding="5" 
    CellSpacing="5" DataKeyNames="TransLogID" ShowHeader="true" runat="server" 
    AllowPaging="True" PageSize="20">
        <Columns>
            <asp:ButtonField ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" 
                CommandName="XML" ButtonType="Image" ImageUrl="~/Theme/icon/savefile.png" >
                <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
            </asp:ButtonField>
            <asp:ButtonField ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" 
                CommandName="Print" ButtonType="Image" ImageUrl="~/Theme/icon/printfile.png" >
                <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
            </asp:ButtonField>
            <asp:TemplateField>
                <ItemTemplate>
                    <table width="500">
                        <tr>
                            <td colspan="5">
                                <strong>Trans Log</strong> : <%# Eval("TransLogNo")%>
                                <strong>Trans Date</strong> : <%# Eval("TransLogDate") %>
                                <strong>Secruties</strong> : <%# Eval("SecID")%>
                            </td>
                        </tr>
                        <tr>
                            <td>                                            
                                <strong>Partial ID</strong> : <%# Eval("PartialID")%>
                            </td>
                            <td>                                            
                                <strong>Message Type</strong> : <%# Eval("MsgType")%>
                            </td>
                            <td>                                            
                                <strong>Priority</strong> : <%# Eval("Priority")%>
                            </td>
                            <td>                                            
                                <strong>Sender Ref.</strong> : <%# Eval("SenderRef")%>
                            </td>
                            <td>                                            
                                <strong>Replate Ref.</strong> : <%# Eval("RelateRef")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>PlaceCode</strong> : <%# Eval("PlaceCode")%>
                            </td>
                            <td>                                            
                                <strong>SettDate</strong> : <%# Eval("SettDate")%>
                            </td>
                            <td>                                            
                                <strong>TradeDate</strong> : <%# Eval("TradeDate")%>
                            </td>
                            <td>                                            
                                <strong>PercentType</strong> : <%# Eval("PercentType")%>
                            </td>
                            <td>                                            
                                <strong>DealPrice</strong> : <%# Eval("DealPrice")%>
                            </td>
                      </tr>
                      <tr>
                            <td>                                            
                                <strong>ISIN</strong> : <%# Eval("ISIN")%>
                            </td>
                            <td>                                            
                                <strong>FaceAmt</strong> : <%# Eval("FaceAmt")%>
                            </td>
                            <td>                                            
                                <strong>AcctNo</strong> : <%# Eval("AcctNo")%>
                            </td>
                            <td>
                                <strong>CashAcct</strong> : <%# Eval("CashAcct")%>
                            </td>
                            <td>                                            
                                <strong>BusinessType</strong> : <%# Eval("BusinessType")%>
                            </td>
                        </tr>
                        <tr>
                            <td>                                            
                                <strong>SettType</strong> : <%# Eval("SettType")%>
                            </td>
                            <td>                                            
                                <strong>SettPlace</strong> : <%# Eval("SettPlace")%>
                            </td>
                            <td>                                            
                                <strong>AgentBIC</strong> : <%# Eval("AgentBIC")%>
                            </td>
                            <td>                                            
                                <strong>AgentAcctNo</strong> : <%# Eval("AgentAcctNo")%>
                            </td>
                            <td>                                            
                                <strong>SettAmt</strong> : <%# Eval("SettAmt")%>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle Height="30" />
        <SelectedRowStyle BackColor="Aqua" />
        <PagerStyle HorizontalAlign="Left"  BackColor="#6699FF" ForeColor="#333333" />
    </asp:GridView>
</asp:Content>
