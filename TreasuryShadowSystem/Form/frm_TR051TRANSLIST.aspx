<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR051TRANSLIST.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR051TRANSLIST" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">   
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $('#ctl00_bodyContent_txtTransDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            $("[id*=chkAllPrintDocument]").click(function() {
                var chkHeader = $(this);
                var grid = $(this).closest("table");
                $("input[id*=chkPrintDocument]", grid).each(function() {
                    if (chkHeader.is(":checked")) {
                        $(this).prop("checked", true);
                    } else {
                        $(this).prop("checked", false);
                    }
                });
                if (chkHeader.is(":checked")) {
                    $("[id*=chkAllPrintReviseDocument]").prop("checked", false);
                }
            });

            $("[id*=chkPrintDocument]").click(function() {
                var grid = $(this).closest("table");
                var chkHeader = $("[id*=chkAllPrintDocument]", grid);
                if (!$(this).is(":checked")) {
                    chkHeader.prop("checked", false);
                } else {
                    if ($("[id*=chkPrintDocument]", grid).length == $("[id*=chkPrintDocument]:checked", grid).length) {
                        chkHeader.prop("checked", true);
                    }
                    $("[id*=chkAllPrintReviseDocument]").prop("checked", false);
                }
            });
            
            $("[id*=chkAllPrintReviseDocument]").click(function() {
                var chkHeader = $(this);
                var grid = $(this).closest("table");
                $("input[id*=chkPrintReviseDocument]", grid).each(function() {
                    if (chkHeader.is(":checked")) {
                        $(this).prop("checked", true);
                    } else {
                        $(this).prop("checked", false);
                    }
                });
                if (chkHeader.is(":checked")) {
                    $("[id*=chkAllPrintDocument]").prop("checked", false);
                }
            });

            $("[id*=chkPrintReviseDocument]").click(function() {
                var grid = $(this).closest("table");
                var chkHeader = $("[id*=chkAllPrintReviseDocument]", grid);
                if (!$(this).is(":checked")) {
                    chkHeader.prop("checked", false);
                } else {
                    if ($("[id*=chkPrintReviseDocument]", grid).length == $("[id*=chkPrintReviseDocument]:checked", grid).length) {
                        chkHeader.prop("checked", true);
                    }
                    $("[id*=chkAllPrintDocument]").prop("checked", false);
                }
            });

            $("[id*=chkAllPrintNoSignDocument]").click(function() {
                var chkHeader = $(this);
                var grid = $(this).closest("table");
                $("input[id*=chkPrintNoSignDocument]", grid).each(function() {
                    if (chkHeader.is(":checked")) {
                        $(this).prop("checked", true);
                    } else {
                        $(this).prop("checked", false);
                    }
                });
            });

            $("[id*=chkPrintNoSignDocument]").click(function() {
                var grid = $(this).closest("table");
                var chkHeader = $("[id*=chkAllPrintNoSignDocument]", grid);
                if (!$(this).is(":checked")) {
                    chkHeader.prop("checked", false);
                } else {
                    if ($("[id*=chkPrintNoSignDocument]", grid).length == $("[id*=chkPrintNoSignDocument]:checked", grid).length) {
                        chkHeader.prop("checked", true);
                    }                    
                }
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
			<h3 class="fl">Bond Allotment (Download Confirmation & XML)</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
    	        <fieldset>
		            <table border="0">
		                <tbody>        		            
		                    <tr>
		                        <td width="50px"><label for="simple-input">Deal No:</label></td>
		                        <td class="td-field">
		                            <asp:TextBox ID="txtDealNo" placeholder="1000000" runat="server" 
                                        CssClass="round input-form-textbox"></asp:TextBox>
		                        </td>
		                    </tr>
		                    <tr>
		                        <td ><label for="simple-input">Transaction Date:</label></td>
		                        <td class="td-field"><input type="text" id="txtTransDate" name="txtTransDate"  runat="server" class="round input-textbox-date" /></td>
		                    </tr>
		                    <tr>
		                        <td colspan="2" style="text-align: left;">
                                    <asp:Button ID="btnSubmit" 
                                        Text="Search" runat="server" class="round blue button-submit" 
                                        onclick="btnSubmit_Click" />
		                            <div id="divMsg" style="display:none;">
                                        <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                                    </div>    
		                        &nbsp;</td>
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
			                
                <asp:GridView ID="GridData" AutoGenerateColumns="false" Width="100%" CssClass="GridViewStyle" CellPadding="0" EnableTheming="false" ShowHeader="true" ShowFooter="true" 
                CellSpacing="0" DataKeyNames="TransLogID" runat="server" 
                AllowPaging="True" PageSize="20">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAllPrintDocument" Text="By Original" ForeColor="#FFFFFF" Font-Bold="true" Font-Names="Tahoma" Font-Size="10pt" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:RadioButton ID="chkPrintDocument" GroupName='<%# Eval("TransLogID")%>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                            <HeaderStyle BackColor="#507CD1" ForeColor="White"/>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAllPrintReviseDocument" Text="By Revise" ForeColor="#FFFFFF" Font-Bold="true" Font-Names="Tahoma" Font-Size="10pt" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:RadioButton ID="chkPrintReviseDocument" GroupName='<%# Eval("TransLogID")%>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                            <HeaderStyle BackColor="#507CD1" ForeColor="White"/>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAllPrintNoSignDocument" Text="No Sign" ForeColor="#FFFFFF" Font-Bold="true" Font-Names="Tahoma" Font-Size="10pt" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkPrintNoSignDocument" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                            <HeaderStyle BackColor="#507CD1" ForeColor="White"/>
                        </asp:TemplateField>
                        <asp:ButtonField ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" 
                            CommandName="XML" ButtonType="Image" ImageUrl="~/Theme/icon/savefile.png" >
                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                            <HeaderStyle BackColor="#507CD1" />
                        </asp:ButtonField>
                        <%-- <asp:ButtonField ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" 
                            CommandName="Print" ButtonType="Image" ImageUrl="~/Theme/icon/printfile.png" >
                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                            <HeaderStyle BackColor="#507CD1"/>
                        </asp:ButtonField>
                        <asp:ButtonField ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" 
                            CommandName="PrintRevise" ButtonType="Image" ImageUrl="~/Theme/icon/actions-redo.png" >
                            <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                            <HeaderStyle BackColor="#507CD1"/>
                        </asp:ButtonField> --%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <table style="font:10pt tahoma">
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
                          
                    <PagerStyle CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                </asp:GridView>     
                <br />           
                <asp:Button ID="btnDownloadSelected" Text="Download by Selected" 
                    runat="server"  ValidationGroup="ExportGroup" 
                    class="round blue button-submit" Visible="false" onclick="btnDownloadSelected_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnSendMailSelected" Text="Send Mail by Selected" 
                    runat="server"  ValidationGroup="ExportGroup" 
                    class="round blue button-submit" Visible="false" onclick="btnSendMailSelected_Click" />  
                <div>&nbsp;</div>
                <fieldset style="border: 1px solid #dddddd">
                    <legend>Download All</legend>
                    <br />
                    <table width="620" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td width="50px">&nbsp;</td>
                            <td width="70px">Deal No.</td>
                            <td width="500px"><asp:TextBox ID="DealNoBox" ValidationGroup="ExportGroup" runat="server"></asp:TextBox>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:RadioButtonList ID="ChoiceConditionBox" Width="300px" RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="2" runat="server">
                                    <asp:ListItem Text="by Original" Value="Original" Selected></asp:ListItem>
                                    <asp:ListItem Text="by Revise" Value="Revise"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:CheckBox ID="NoSignBox" Text="No Sign" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td colspan="2">
                                <asp:Button ID="btnDownloadAll" Text="Download All" runat="server"  ValidationGroup="ExportGroup" 
                                    class="round blue button-submit" onclick="btnDownloadAll_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnViewPassword" Text="View Password" runat="server"  ValidationGroup="ExportGroup" 
                                    class="round blue button-submit" onclick="btnViewPassword_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btnSendMail" Text="Send Mail" runat="server"  ValidationGroup="ExportGroup" 
                                    class="round blue button-submit" onclick="btnSendMail_Click" />  
                            </td>
                        </tr>
                    </table>                    
                </fieldset>  
                <asp:HiddenField ID="hfCount" runat="server" Value = "0" />
    
			    </div> <!-- end half-size-column -->
			    
			</div> <!-- end half-size-column -->
		
    </div> <!-- end content-module-main -->
</asp:Content>
