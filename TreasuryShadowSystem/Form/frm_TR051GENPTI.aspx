<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR051GENPTI.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR051GENPTI" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        
             $(document).ready(function() { 
             
                $('#ctl00_bodyContent_SettleDateSearch').datepicker({
                    altFormat: 'dd/mm/yy',
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true,
                    changeYear: true
                });
              
               $("[id*=chkAll]").click(function() {
                   
                    var chkHeader = $(this);
                    var grid = $(this).closest("table");
                    

                    $("input[id*=chkSelectAllot]", grid).each(function() {
                        if (chkHeader.is(":checked")) {
                            $(this).prop("checked", true);
                            $('#<%=CheckAll.ClientID %>').val("ALL");
                        } else {
                            $(this).prop("checked", false);
                            $('#<%=CheckAll.ClientID %>').val("");
                        }
                       
                    });
                    //alert($('#<%=CheckAll.ClientID %>').val());
                });
                
               $("[id*=chkSelectAllot]").click(function() {
               
                    var grid = $(this).closest("table");
                    var chkHeader = $("[id*=chkAll]", grid);
                    if (!$(this).is(":checked")) {
                        chkHeader.prop("checked", false);
                        $('#<%=CheckAll.ClientID %>').val("");
                    } else {
                        if ($("[id*=chkSelectAllot]", grid).length == $("[id*=chkSelectAllot]:checked", grid).length) {
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
			<h3 class="fl">Generate PTI (Back Office)</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
			    <fieldset>
					<table>
					    <tr>
					        <td class="td-display">Settlement Date</td>
					        <td class="td-field"><asp:TextBox ID="SettleDateSearch" runat="server" autocomplete="off"></asp:TextBox>&nbsp;</td>
					    </tr>
					    <tr>
					        <td class="td-display">Deal No.</td>
					        <td class="td-field"><asp:TextBox ID="DealnoSearch" runat="server"></asp:TextBox></td>					    
					    </tr>
					    <tr>
					       <td>&nbsp;</td>
					       <td><asp:Button ID="SearchButton" Text="Search" runat="server" 
                            class="round blue button-submit" onclick="SearchButton_Click"/></td>
					    </tr>
					</table>
				        <label for="simple-input"></label>
				</fieldset>			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf ">
			<div class="half-size-column fl" style="overflow:auto; width:100%;height:400px;">				    
			    <asp:Button ID="btnSave" runat="server" Text="Gen XML" onclick="btnSave_Click" Visible="false" />
                <asp:Button ID="btnDownload" runat="server" Text="Download XML" Visible="false" onclick="btnDownload_Click" />
			    <div style="float:right"></div>
			    <br /><br />
			    <asp:HiddenField ID="CheckAll" runat="server" />			    	
                <asp:GridView ID="GridViewAllotment" DataKeyNames="TransLogID" CssClass="GridViewStyle" PageSize="300" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                    <Columns>
                        <asp:TemplateField>
                        <HeaderTemplate>
                                 <asp:CheckBox ID="chkAll" ForeColor="#FFFFFF" Font-Bold="true" Font-Names="Tahoma" Font-Size="10pt" runat="server" />
                        </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectAllot" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:BoundField DataField="SecID" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center" HeaderText="Security" /> 
                        <asp:BoundField DataField="MsgType" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center" HeaderText="Message" /> 
                        <asp:BoundField DataField="SenderRef" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="DealNo" /> 
                        <asp:BoundField DataField="SettDate" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="Settlement Date" /> 
                        <asp:BoundField DataField="TradeDate" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="Trade Date" /> 
                        <asp:BoundField DataField="DealPrice" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="DealPrice" /> 
                        <asp:BoundField DataField="ISIN" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="ISIN" /> 
                        <asp:BoundField DataField="FaceAmt" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="Face Amount" /> 
                        <asp:BoundField DataField="PS" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="Buy/Sell" /> 
                        <asp:BoundField DataField="Attn" HeaderStyle-Width="150" ItemStyle-HorizontalAlign="Left" HeaderText="Attn" /> 
                        <asp:BoundField DataField="AgentBIC" HeaderStyle-Width="70"  ItemStyle-HorizontalAlign="Center" HeaderText="BIC" /> 
                        <asp:BoundField DataField="AgentCA" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="C/A " />
                        <asp:BoundField DataField="AgentAcctNo" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="Acct No." /> 
                        <asp:BoundField DataField="PrinAmt" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="Clean Amt." />     
                        <asp:BoundField DataField="IntAmt" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="Interest Amt." /> 
                        <asp:BoundField DataField="SettAmt" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="Settlemtn Amt." /> 
                        <asp:BoundField DataField="CostValue" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="Cost Value" /> 
                        <asp:BoundField DataField="Vat" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="WHT" /> 
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
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
