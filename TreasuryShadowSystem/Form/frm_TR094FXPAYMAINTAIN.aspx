<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR094FXPAYMAINTAIN.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR094FXPAYMAINTAIN" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function() {
            $('#ctl00_bodyContent_FromDateBox,#ctl00_bodyContent_ToDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('.chkgross').click(function() {
                var dealno = $(this).attr('value');
                var checked = $(this).is(':checked') == true ? 'G' : '';
                var request = $.ajax({
                    type: "POST",
                    url: '<%=Config.Website %>/Form/frm_TR094FXPAYMAINTAIN.aspx/UpdateGross',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ 'DealNo': dealno, 'Gross': checked }),
                    success: function(data) {
                        if (data.d.Result != "OK") {
                            aler(data.d.Message);
                        }
                    },
                    error: function(xhr, ajaxOptions, thrownError) {
                        var err = JSON.parse(xhr.responseText);
                        alert(err.Message);
                    }
                });
            });

            $('.deallist').click(function() {
                var dealno = $(this).attr('value');
                var checked = $(this).is(':checked') == true ? 'G' : '';
                var request = $.ajax({
                    type: "POST",
                    url: '<%=Config.Website %>/Form/frm_TR094FXPAYMAINTAIN.aspx/UpdateGross',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ 'NettingDeal': netting, 'DealNo': dealno, 'Gross': checked }),
                    success: function(data) {
                        if (data.d.Result != "OK") {
                            aler(data.d.Message);
                        }
                    },
                    error: function(xhr, ajaxOptions, thrownError) {
                        var err = JSON.parse(xhr.responseText);
                        alert(err.Message);
                    }
                });
            });

            $('.nettingclass').click(function() {
                var arr = $(this).attr('title').split(',');
                $('.netdeal').val(arr[0]);
                $('.cmne').val(arr[1]);

                var netdeal = $('.netdeal').val();
                var cmne = $('.cmne').val();
                var fromdate = $('#ctl00_bodyContent_FromDateBox').val();
                var todate = $('#ctl00_bodyContent_ToDateBox').val();
                var isnetting = '';
                if (todate == '')
                    todate = fromdate;

                $('#dialogdeal').dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 400,
                    modal: true,
                    open: function() {
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/Form/frm_TR094FXPAYMAINTAIN.aspx/GetNettingDeal',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ 'DealNo': netdeal, 'Cmne': cmne, 'FromDate': fromdate, 'ToDate': todate }),
                            success: function(data) {
                                var obj = data.d.Records;
                                //alert(obj[0].DEALNO);
                                var table = $('<div></div>');

                                var counter = 0;
                                $(obj).each(function() {
                                    if (netdeal != this.DEALNO) {
                                        var chkname = 'chklistitem' + counter;
                                        table.append(
                                        $('<div style="float:left; padding: 3px"></div>').append(
                                            $('<input>').attr({
                                                type: 'checkbox',
                                                name: 'chklistitem',
                                                value: this.DEALNO,
                                                id: chkname
                                            }).addClass('deallist')
                                            ).append(
                                                $('<label>').attr('for', chkname).text(this.DEALNO)
                                            )
                                        );
                                        counter++;
                                    } else {
                                        if (this.NETNOTDEAL == 'Y')
                                            isnetting = 'Y';
                                    }
                                });
                                $('#dvCheckBoxListControl').html(table);
                                rows = 0;
                                $(obj).each(function() { 
                                    
                                    if (netdeal != this.DEALNO) {
                                        if ((this.NETDEAL == 'Y') || (this.NETNOTDEAL == 'Y')) {
                                            $('#chklistitem' + rows).attr('checked', 'checked');
                                            if (this.NETDEAL == 'Y')
                                                $('#chklistitem' + rows).removeAttr('disabled');
                                            else if (this.NETNOTDEAL == 'Y')
                                                $('#chklistitem' + rows).attr('disabled', 'disabled');
                                        } else
                                            $('#chklistitem' + rows).removeAttr('checked');

                                        if (isnetting == 'Y')
                                            $('#chklistitem' + rows).attr('disabled', 'disabled');
                                        rows++;
                                    }
                                }
                                );
                                if (isnetting == 'Y')
                                    alert('Warning!! Deal was netting.');
                            },
                            error: function(xhr, ajaxOptions, thrownError) {
                                var err = JSON.parse(xhr.responseText);
                                alert(err.Message);
                            }
                        });
                    },
                    close: function() {
                        $(this).dialog("close");
                    },
                    "buttons": {
                        "Submit": function() {
                            var deal = '';
                            $('.deallist:checked').not(":disabled").each(function() {
                                deal = deal + ' ' + $(this).val().trim();
                            });
                            var netdeal = $('.netdeal').val();
                            var cmne = $('.cmne').val();

                            var request = $.ajax({
                                type: "POST",
                                url: '<%=Config.Website %>/Form/frm_TR094FXPAYMAINTAIN.aspx/NettingDeal',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({ 'NettingDeal': netdeal, 'CMNE': cmne, 'DealNo': deal }),
                                success: function(data) {
                                    if (data.d.Result != "OK")
                                        alert(data.d.Message);
                                    else {
                                        alert('Netting Complete!!.');
                                        //$("#aspnetForm").submit();
                                        $('#<%=btnSubmit.ClientID %>').click();
                                    }
                                },
                                error: function(xhr, ajaxOptions, thrownError) {
                                    var err = JSON.parse(xhr.responseText);
                                    alert(err.Message);
                                }
                            });

                        },
                        "Close": function() {
                            $(this).dialog("close");
                        }
                    }
                });
                return false;
            });
        });
/*
        $(document).on('click', ".ui-dialog-buttonset>button:contains('Submit')", function() {
            var names = 0;
            $('.deallist input:checked').each(function() {
                names = names + 1;
            });
            alert(names);
        });*/
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Pre-FX Payment Report Maintain</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0">
	                    <tbody>            	                        	            
	                        <tr>
	                            <td>CUST</td>
	                            <td>
	                                <asp:TextBox ID="CMNEBox" runat="server"></asp:TextBox> 
	                                    <asp:Label ID="WarningBox"  Text="* CAPITAL LETTER" runat="server"></asp:Label>
	                            </td>	                            
	                        </tr>            	                        	            
	                        <tr>
	                            <td>Date</td>
	                            <td>     
	                                    <asp:TextBox ID="FromDateBox" Text="08/05/2017" Runat="server"></asp:TextBox>-
	                                    <asp:TextBox ID="ToDateBox" Text="08/05/2017" Runat="server"></asp:TextBox>
	                            </td>	                            
	                        </tr> 
	                        <tr>
	                            <td style="text-align: left;">
	                                <asp:Button ID="btnSubmit" Text="Search" runat="server" 
                                        class="round blue button-submit" onclick="btnSubmit_Click" />
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
                <div style="overflow: scroll; height: 500px">
	                        <asp:GridView ID="GridFX" DataKeyNames="DealNo" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" PageSize="100" runat="server">
                            <Columns>
                                <asp:BoundField DataField="DEALNO" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="DealNo" /> 
                                <asp:BoundField DataField="PRODTYPE" ItemStyle-HorizontalAlign="Center" HeaderText="Type" /> 
                                <asp:BoundField DataField="CMNE" ItemStyle-HorizontalAlign="Center" HeaderText="Short Name" /> 
                                <asp:BoundField DataField="DEALDATE" ItemStyle-HorizontalAlign="Center" HeaderText="DEALDATE" /> 
                                <asp:BoundField DataField="PRODCODE" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderText="Product" /> 
                                <asp:BoundField DataField="VDATE" ItemStyle-HorizontalAlign="Center" HeaderText="VDATE" /> 
                                <asp:BoundField DataField="CCY" ItemStyle-HorizontalAlign="Center" HeaderText="CCY" /> 
                                <asp:BoundField DataField="CCYAMT" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="CCY Amount" /> 
                                <asp:BoundField DataField="CTRCCY" ItemStyle-HorizontalAlign="Center" HeaderText="CTR" /> 
                                <asp:BoundField DataField="CTRAMT" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="CTR Amount" /> 
                                <asp:TemplateField>
                                    <HeaderTemplate>Pre-FX Netting<br />(Select for Gross Settlement)</HeaderTemplate>
                                    <ItemTemplate>
                                        <input type="checkbox" ID="chkgross" class="chkgross" value="<%# DataBinder.Eval(Container.DataItem, "DEALNO") %>" <%# Eval("GROSSNET").ToString()=="G" ? "checked" : "" %> />
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>POST-FX Netting<br />(Select for Net Settlement)</HeaderTemplate>
                                    <ItemTemplate>
                                        <a href="#" class="nettingclass" title="<%# DataBinder.Eval(Container.DataItem, "DEALNO") %>,<%# DataBinder.Eval(Container.DataItem, "CMNE") %>">Netting</a>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="NETDEALNO" ItemStyle-HorizontalAlign="Center" HeaderText="Netting Deal" /> 
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
                </div>
			</div> <!-- end half-size-column -->
		</div> <!-- end content-module-main -->
		
    <div id="dialogdeal" style="display: none">
        Netting Deal <input type="text" id="netdeal" name="netdeal" class="netdeal" /><br />
        Customer <input type="text" id="cmne" name="cmne" class="cmne" /><br />
        <div id="dvCheckBoxListControl"></div>
    </div>
	</div> <!-- end content-module -->
</asp:Content>
