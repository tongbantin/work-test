<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_FXNostroAvg.aspx.cs" Inherits="TreasuryShadowSystem.TR145BORATEAVG.frm_FXNostroAvg" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
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

            $(".chkall").click(function() {
                var chkHeader = $(this);
                var grid = $(this).closest("table");
                $(".chkprintdocument", grid).each(function() {
                    if (chkHeader.is(":checked")) {
                        $(this).prop("checked", true);
                    } else {
                        $(this).prop("checked", false);
                    }
                });
            });
            $(".chkprintdocument").click(function() {
                var grid = $(this).closest("table");
                var chkHeader = $(".chkall", grid);
                if (!$(this).is(":checked")) {
                    chkHeader.prop("checked", false);
                } else {
                    if ($(".chkprintdocument", grid).length == $(".chkprintdocument:checked", grid).length) {
                        chkHeader.prop("checked", true);
                    }
                }
            });

            $(".summaryreport").click(function() {
                var account = $(".account").val();
                var fromdate = $('.fromdate').val();
                var todate = $('.todate').val();
                var request = $.ajax({
                    type: "POST",
                    url: '<%=Config.Website %>/TR145BORATEAVG/frm_FXNostroAvg.aspx/PrintNostroAverageSummary',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ 'Account': account, 'FromDate': fromdate, 'ToDate': todate }),
                    success: function(data) {
                        $('.ireport').attr('src', $('.ireport').attr('src'));
                        $('.dialogreport').dialog({ title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', modal: true, width: 1000 });
                    },
                    error: function(xhr, ajaxOptions, thrownError) {
                        var err = JSON.parse(xhr.responseText);
                        alert(err.Message);
                    }
                });
            });

            $(".diffspotratereport").click(function() {
                var arr = [];
                var boxes = $(":checkbox:checked");
                $(boxes).each(function() {
                    if ($.isNumeric($(this).val()))
                        arr.push($(this).val());
                });
                //alert(arr.join(","));

                var request = $.ajax({
                    type: "POST",
                    url: '<%=Config.Website %>/TR145BORATEAVG/frm_FXNostroAvg.aspx/PrintNostroAccount',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ 'NOSTAVGID': arr.join(",") }),
                    success: function(data) {
                        //alert($('.ireport').attr('src'));
                        $('.ireport').attr('src',$('.ireport').attr('src'));
                        $('.dialogreport').dialog({ title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', modal: true, width: 1000 });
                    },
                    error: function(xhr, ajaxOptions, thrownError) {
                        var err = JSON.parse(xhr.responseText);
                        alert(err.Message);
                    }
                });
            });

            $(".plthbreport").click(function() {
                var arr = [];
                var boxes = $(":checkbox:checked");
                $(boxes).each(function() {
                    if ($.isNumeric($(this).val()))
                        arr.push($(this).val());
                });
                //alert(arr.join(","));

                var request = $.ajax({
                    type: "POST",
                    url: '<%=Config.Website %>/TR145BORATEAVG/frm_FXNostroAvg.aspx/PrintPLTHBAccount',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ 'NOSTAVGID': arr.join(",") }),
                    success: function(data) {
                        $('.ireport').attr('src', $('.ireport').attr('src'));
                        $('.dialogreport').dialog({ title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', modal: true, width: 1000 });
                    },
                    error: function(xhr, ajaxOptions, thrownError) {
                        var err = JSON.parse(xhr.responseText);
                        alert(err.Message);
                    }
                });
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
			<h3 class="fl">FX Nostro Average</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
					        <table>
	                             <tr>
	                                <td class="td-display">&nbsp;Account</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="AccountBox" CssClass="account" runat="server">
    	                                </asp:DropDownList>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">Date</td>
	                                <td class="td-field">           	                                
	                                    <asp:TextBox ID="FromDateBox" Text="01/01/2017" CssClass="fromdate" Runat="server"></asp:TextBox>-
	                                    <asp:TextBox ID="ToDateBox" Text="31/10/2017" CssClass="todate" Runat="server"></asp:TextBox>
	                                </td>
	                            </tr>
					            <tr>
					                <td>&nbsp;</td>
					                <td colspan="2">
					                    <div style="float:left">
					                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                                            class="round blue button-submit" onclick="btnSubmit_Click" />
                                        </div>
					                    <div style="float:right">                                            
					                    <asp:Button ID="btnCalculate" Text="Calculate" runat="server" 
                                            class="round red button-submit" OnClientClick="if (!confirm('ต้องการคำนวณรายการใหม่หรือไม่')) return false;" onclick="btnCalculate_Click" /></div></td>
					            </tr>
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
			    <button id="SummaryReport" class="round button-submit summaryreport" type="button">Summary Report</button>
			    <button id="DiffSpotRateReport" class="round button-submit diffspotratereport" type="button">Diff.SpotRate Report</button>
			    <button id="PLTHBReport" class="round button-submit plthbreport" type="button">P/L THB Report</button>
			    <br /><br />
			    <asp:GridView ID="GridNostroAvg" Visible="true" DataKeyNames="NostAvgID" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" PageSize="200" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input type="checkbox" name="chkall" id="chkall" value="0" class="chkall" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input type="checkbox" name="chkprintdocument" class="chkprintdocument" value='<%# DataBinder.Eval(Container.DataItem, "NostAvgID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="VDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Value Date" /> 
                            <asp:BoundField DataField="BUYAMT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Buy" /> 
                            <asp:BoundField DataField="SELLAMT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Sell" /> 
                            <asp:BoundField DataField="STATEDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Date Statement" /> 
                            <asp:BoundField DataField="STATERATE" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N6}" HeaderText="Statement" /> 
                            <asp:BoundField DataField="OPICSDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Date Opics" /> 
                            <asp:BoundField DataField="OPICSRATE" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N6}" HeaderText="Opics" /> 
                            <asp:BoundField DataField="STATEMOVE" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Movement Statement" /> 
                            <asp:BoundField DataField="OPICSMOVE" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Movement Opics" /> 
                            <asp:BoundField DataField="ADJDIFSPOTRATE" ItemStyle-HorizontalAlign="right" DataFormatString="{0:N2}" HeaderText="Diff.SpotRate" /> 
                            <asp:BoundField DataField="ADJPLTHB" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="P/L THB" /> 
                            <asp:BoundField DataField="BALANCECCY" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Balance CCY" /> 
                            <asp:BoundField DataField="BALANCETHB" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" HeaderText="Balance THB" /> 
                            <asp:BoundField DataField="EXCHRATEAVG" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N6}" HeaderText="Exch Rate Avg." /> 
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
                    <div class="dialogreport" style="display:none">
                    <iframe id="iReport" class="ireport" src="../View/Viewer.aspx" width="100%" height="450px" runat="server" frameborder="0" marginwidth="1"></iframe>	
	                </div>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
				
	</div> <!-- end content-module -->
</asp:Content>
