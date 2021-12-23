<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_CurrentPosition.aspx.cs" Inherits="TreasuryShadowSystem.Maintain.frm_CurrentPosition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <style>
        table.MaintainStyle th { padding: 0px; margin: 0px; text-align:center; font-weight: bold; font: 10pt tahoma; height: 25px;}
        table.MaintainStyle tbody tr td { font: 10pt tahoma; height: 25px; }
        table.MaintainStyle input { width: 50px; margin: 0px; padding: 3px; text-align: center; text-transform: uppercase;}
        table.MaintainStyle table { width: auto; }
        table.MaintainStyle table tr td { width: 30px; }
        .HideColumn { display:none; }
    </style>
    <script type="text/javascript">
        $(document).ready(function() {

            $('#ctl00_bodyContent_txtFromDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            $('#ctl00_bodyContent_txtToDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            $('#ctl00_bodyContent_SettDateDialogBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            $('#ctl00_bodyContent_EffDateDialogBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });


            $('#ctl00_bodyContent_GridViewFund_ctl02_InlineEffDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            $('#ctl00_bodyContent_ReportDateDialogBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            function Required(name, val) {
                if (val == '') {
                    alert(name + ' ' + val + ' Required!!');
                    return false;
                } else
                    return true;
            }

            function submitForm() {
                if ($('#<%=ActionBox.ClientID %>').val() == "Add") {
                    if ((Required("Report Date", $('#<%=ReportDateDialogBox.ClientID %>').val())) &&
                    (Required("CCY", $('#<%=CCYDialogBox.ClientID %>').val())) &&
                    (Required("Remark", $('#<%=ProductDialogBox.ClientID %>').val())) &&
                    (Required("Today Spot Balance", $('#<%=TDBaseSpotDialogBox.ClientID %>').val())) &&
                    (Required("Today Forward Balance", $('#<%=TDBaseForwardDialogBox.ClientID %>').val())) &&
                    (Required("Spot Rate", $('#<%=SpotRateDialogBox.ClientID %>').val()))) {
                        $('#<%=ReportDateBox.ClientID %>').val($('#<%=ReportDateDialogBox.ClientID %>').val());
                        $('#<%=CCYBox.ClientID %>').val($('#<%=CCYDialogBox.ClientID %>').val());
                        $('#<%=ProductBox.ClientID %>').val($('#<%=ProductDialogBox.ClientID %>').val());
                        $('#<%=TDBaseSpotBox.ClientID %>').val($('#<%=TDBaseSpotDialogBox.ClientID %>').val());
                        $('#<%=TDBaseForwardBox.ClientID %>').val($('#<%=TDBaseForwardDialogBox.ClientID %>').val());
                        $('#<%=SpotRateBox.ClientID %>').val($('#<%=SpotRateDialogBox.ClientID %>').val());
                        $('input#<%=DialogSubmit.ClientID %>').click();
                    }
                } else if ($('#<%=ActionBox.ClientID %>').val() == "EditBalance") {
                if ((Required("Today Spot Rate", $('#<%=TDBaseSpotDialogBalanceBox.ClientID %>').val())) &&
                    (Required("Today Forward Rate", $('#<%=TDBaseForwardDialogBalanceBox.ClientID %>').val()))) {
                        $('#<%=TDBaseSpotBox.ClientID %>').val($('#<%=TDBaseSpotDialogBalanceBox.ClientID %>').val());
                        $('#<%=TDBaseForwardBox.ClientID %>').val($('#<%=TDBaseForwardDialogBalanceBox.ClientID %>').val());
                        $('input#<%=DialogSubmit.ClientID %>').click();
                    }
                } else if ($('#<%=ActionBox.ClientID %>').val() == "EditRate") {
                    $('#<%=SpotRateBox.ClientID %>').val($('#<%=SpotRateDialogRateBox.ClientID %>').val());
                    $('input#<%=DialogSubmit.ClientID %>').click();
                }
            }

            $(".ic-add").click(function() {
                $('#<%=ActionBox.ClientID %>').val("Add");
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    modal: true,
                    "buttons": {
                        "Submit": function() {
                            submitForm();
                        },
                        "Close": function() {
                            $(this).dialog("close");
                        }
                    }
                });
                return false;
            });

            $(".EditBalance").click(function() {
                $('#<%=ActionBox.ClientID %>').val("EditBalance");
                $('#<%=PositionBox.ClientID %>').val($(this).parent().siblings().eq(7).html().trim());
                $('#<%=ReportDateBox.ClientID %>').val($(this).parent().siblings().eq(0).html().trim());
                $('#<%=CCYBox.ClientID %>').val($(this).parent().siblings().eq(1).html().trim());
                $('#<%=ProductBox.ClientID %>').val($(this).parent().siblings().eq(2).html().trim());
                $('#<%=TDBaseSpotDialogBalanceBox.ClientID %>').val($(this).parent().siblings().eq(3).html().trim());
                $('#<%=TDBaseForwardDialogBalanceBox.ClientID %>').val($(this).parent().siblings().eq(4).html().trim());
                $("#dialogbalance").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    modal: true,
                    "buttons": {
                        "Submit": function() {
                            submitForm();
                        },
                        "Close": function() {
                            $(this).dialog("close");
                        }
                    }
                });
                return false;
            });

            $(".EditRate").click(function() {
                $('#<%=ActionBox.ClientID %>').val("EditRate");
                $('#<%=PositionBox.ClientID %>').val($(this).parent().siblings().eq(7).html().trim());
                $('#<%=ReportDateBox.ClientID %>').val($(this).parent().siblings().eq(0).html().trim());
                $('#<%=CCYBox.ClientID %>').val($(this).parent().siblings().eq(1).html().trim());
                $('#<%=ProductBox.ClientID %>').val($(this).parent().siblings().eq(2).html().trim());
                $('#<%=SpotRateDialogRateBox.ClientID %>').val($(this).parent().siblings().eq(5).html().trim());
                $("#dialograte").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    modal: true,
                    "buttons": {
                        "Submit": function() {
                            submitForm();
                        },
                        "Close": function() {
                            $(this).dialog("close");
                        }
                    }
                });
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none">
        <table width="400">
            <tr>
                <td>&nbsp;</td>
                <td>Report Date</td>
                <td><asp:TextBox ID="ReportDateDialogBox" MaxLength="10" Width="100px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>CCY</td>
                <td><asp:TextBox ID="CCYDialogBox" Width="100px" MaxLength="3" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Remark</td>
                <td><asp:TextBox ID="ProductDialogBox" MaxLength="30" Width="100px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Today Spot Balance</td>
                <td><asp:TextBox ID="TDBaseSpotDialogBox" MaxLength="30"  Width="100px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Today Forward Balance</td>
                <td><asp:TextBox ID="TDBaseForwardDialogBox" MaxLength="30" Width="100px" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="display:none">
                <td>&nbsp;</td>
                <td>Spot Rate</td>
                <td><asp:TextBox ID="SpotRateDialogBox" value="0" MaxLength="30" Width="100px" runat="server"></asp:TextBox></td>
            </tr>
        </table>   
    </div>
    <div id="dialogbalance" style="display: none">
        <table width="400">
            <tr>
                <td>&nbsp;</td>
                <td>Today Spot Balance</td>
                <td><asp:TextBox ID="TDBaseSpotDialogBalanceBox" MaxLength="30" Width="100px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Today Forward Balance</td>
                <td><asp:TextBox ID="TDBaseForwardDialogBalanceBox" MaxLength="30" Width="100px" runat="server"></asp:TextBox></td>
            </tr>
        </table>   
    </div>
    <div id="dialograte" style="display: none">
        <table width="400">
            <tr>
                <td>&nbsp;</td>
                <td>Spot Rate</td>
                <td><asp:TextBox ID="SpotRateDialogRateBox" MaxLength="30" Width="100px" runat="server"></asp:TextBox></td>
            </tr>
        </table>   
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Maintain Current Position</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
						<p>
							From
							<input type="text" id="txtFromDate" size="15" runat="server" class="round input-form-textbox" />	
							
							To
							<input type="text" id="txtToDate" size="15" runat="server" class="round input-form-textbox" />	
							
					        <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                                class="round blue button-submit" onclick="btnSubmit_Click" />
					        <div id="divMsg" style="display:none;">
                                <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                            </div>    
						</p>
					</fieldset>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf" ">
	
			<div class="half-size-column fl" style="overflow:auto; width:100%;height:400px;">	
			    <div style="float:left"><button class="round blue button-submit ic-add">Add Current Position</button></div> 
			    <div style="float:right"><asp:Button ID="BtnExport" 
                        CssClass="round blue button-submit ic-excel" runat="server" Text="Export" 
                        onclick="BtnExport_Click" /></div> 
			    <div style="float:right"></div>
			    <br /><br />			    	
                <asp:GridView ID="GridViewFund" DataKeyNames="PositionID" CellPadding="4" 
                    CssClass="MaintainStyle" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False" 
                    runat="server" ForeColor="#333333" GridLines="None">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Image ImageUrl="~/Theme/icon/icon-tracking.png" CssClass="EditBalance" runat="server" />
                                <%--<asp:Image ImageUrl="~/Theme/icon/icon-transaction.png" CssClass="EditRate" runat="server" />--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemStyle Width="70px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="REPORTDATE" HeaderText="ReportDate" SortExpression="ReportDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />                              
                        <asp:BoundField DataField="CCY" HeaderText="CCY" SortExpression="CCY" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" /> 
                        <asp:BoundField DataField="PRODUCT" HeaderText="Remark" SortExpression="PRODUCT" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />                              
                        <asp:BoundField DataField="TDBASESPOT" HeaderText="SPOT POSITION" SortExpression="TDBASESPOT" ItemStyle-Width="100px" DataFormatString = "{0:N2}" ItemStyle-HorizontalAlign="Right" />                              
                        <asp:BoundField DataField="TDBASEFORWARD" HeaderText="FWD POSITION" SortExpression="TDBASEFORWARD" ItemStyle-Width="100px" DataFormatString = "{0:N2}" ItemStyle-HorizontalAlign="Right" />                              
                        <asp:BoundField DataField="SPOT_RATE" HeaderText="SPOT RATE" ItemStyle-Width="80px" DataFormatString = "{0:N6}" ItemStyle-HorizontalAlign="Right" />                              
                        <asp:BoundField DataField="USD_SPOT_RATE" HeaderText="USD SPOT RATE" ItemStyle-Width="100px" DataFormatString = "{0:N6}" ItemStyle-HorizontalAlign="Right" />                              
                        <asp:BoundField DataField="POSITIONID" ItemStyle-CssClass="HideColumn" />                                                           
                    </Columns>                    
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="Black" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        No Data
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                <asp:HiddenField ID="ActionBox" runat="server" />
                <asp:HiddenField ID="PositionBox" runat="server" />
                <asp:HiddenField ID="ReportDateBox" runat="server" />
                <asp:HiddenField ID="CCYBox" runat="server" />
                <asp:HiddenField ID="ProductBox" runat="server" />
                <asp:HiddenField ID="TDBaseSpotBox" runat="server" />
                <asp:HiddenField ID="TDBaseForwardBox" runat="server" />
                <asp:HiddenField ID="SpotRateBox" runat="server" />
                <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" 
                    runat="server" onclick="DialogSubmit_Click" />
                
			</div> <!-- end half-size-column -->
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
	
	<asp:GridView ID="GridExport" DataKeyNames="PositionID" Visible="false" CellPadding="4" 
        CssClass="MaintainStyle" AllowPaging="False" AllowSorting="true" AutoGenerateColumns="False" 
        runat="server" ForeColor="#333333" Font-Names="Tahoma" Font-Size="10pt" GridLines="None">
        <RowStyle BackColor="#F7F6F3"  Font-Names="Tahoma" Font-Size="10pt" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="REPORTDATE" HeaderText="ReportDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />                              
            <asp:BoundField DataField="CCY" HeaderText="CCY" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" /> 
            <asp:BoundField DataField="PRODUCT" HeaderText="Remark" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />                              
            <asp:BoundField DataField="TDBASESPOT" HeaderText="SPOT POSITION" ItemStyle-Width="100px" DataFormatString = "{0:N2}" ItemStyle-HorizontalAlign="Right" />                              
            <asp:BoundField DataField="TDBASEFORWARD" HeaderText="FWD POSITION" ItemStyle-Width="100px" DataFormatString = "{0:N2}" ItemStyle-HorizontalAlign="Right" />                              
            <asp:BoundField DataField="SPOT_RATE" HeaderText="SPOT RATE" ItemStyle-Width="80px" DataFormatString = "{0:N6}" ItemStyle-HorizontalAlign="Right" />                              
            <asp:BoundField DataField="USD_SPOT_RATE" HeaderText="USD SPOT RATE" ItemStyle-Width="100px" DataFormatString = "{0:N6}" ItemStyle-HorizontalAlign="Right" />                              
      </Columns>                    
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="Black" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            No Data
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
</asp:Content>
