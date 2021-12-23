<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="frm_PositionView.aspx.cs" Inherits="TreasuryShadowSystem.ForeignExchange.frm_PositionView" Title="Untitled Page" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <style type="text/css">
        .GridViewExportStyle
        {
	        width: auto;
            font-family: Arial, Sans-Serif;
            font-size:small;
            table-layout: auto;
            /*border-collapse: collapse;*/
            border: #1d1d1d 1px solid;
            height: auto;
        }
        /*Header and Pager styles*/
        .HeaderExportStyle 
        {
            background-color:#507CD1;
        }
        .HeaderExportStyle th
        {
            padding: 5px;
            color: #ffffff;
            font-weight: bold;
            background-color:#507CD1;
            text-align: center;
        }
        .HeaderExportStyle a
        {
            text-decoration:none;
            color:#ffffff;
            display:block;
            text-align:left;
            font-weight:normal;
        }
        .RowExportStyle td 
        {
            padding: 5px;
            border-right: solid 1px #1d1d1d;
        }
        .RowExportStyle td
        {
            background-color: #ffffff;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function() {  
            $('#ctl00_bodyContent_DataDateSearchBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });    
             $(".btnshowdetail").click( function() {
                if(this.tog ^= 1) {  
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    //$(this).src("../Theme/icon/actions-redo.png");
                    //$('.RowDetail').css('display','block');
                } else {  
                    $(this).closest("tr").next().remove();
                    //$(this).src("../Theme/icon/icon-tracking.png");
                    //$('.RowDetail').css('display','none');
                }
             });           
             $('#ctl00_bodyContent_ValueDateSearchBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });    
            $('#ctl00_bodyContent_ExecuteDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });    
            
            $('.reportdatebox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });   
            
            
            $('#ctl00_bodyContent_ExecuteFromDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            $('#ctl00_bodyContent_ExecuteToDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $(".addposition").click(function() {
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1000,
                    modal: true,
                    open: function() {             
                        $('.dealnobox').val("");
                        $('.dealdatebox').val("");
                        $('.settledatebox').val("");
                        $('.prodtypebox').val("");
                        $('.productbox').val("");
                        $('.reportdatebox').val("");
                        $('.spotforwardbox').val("");
                        $('.ccybox').val("");
                        $('.amountbox').val("0");
                        $('.adjamountbox').val("0"); 
                        $('.remarkbox').val("");   
                                       
                        $('.amountbox').removeAttr('readonly');
                    },
                    "buttons": {
                        "Submit": function() {  
                            var result = true;   
                            
                            if ($('.reportdatebox').val().trim()=='') {
                                alert('Report Date is require!!');  
                                result = false;
                            }
                            else if ($('.ccybox').val().trim()=='') {
                                alert('CCY is require!!');
                                result = false;
                            }
                            else if ($('.amountbox').val().trim()=='') {
                                alert('Amount is require!!');
                                result = false;
                            }
                            if(result) {                                                 
                                var record = {
                                                REPORTDATE:$.datepicker.parseDate('dd/mm/yy',$('.reportdatebox').val()),
                                                SPOTFWDIND:$('.spotforwardbox').val(),
                                                CCY:$('.ccybox').val(),
                                                AMOUNT:$('.amountbox').val(),
                                                ADJAMOUNT:$('.adjamountbox').val(),
                                                REMARK: $('.remarkbox').val()
                                             };
                                
                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/ForeignExchange/frm_PositionView.aspx/AddPositionDetail',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({'record':record}),  
                                    success: function (data) {
                                        if(data.d.Result=="OK") {
                                            alert("Add Other Deal Complete");
                                            $("#<%=btnSubmit.ClientID %>").click();
                                        } else {
                                            alert(data.d.Message);
                                        }
                                    },
                                    error: function (xhr, ajaxOptions, thrownError) {
                                        var err = JSON.parse(xhr.responseText);
                                        alert(err.Message);
                                    }
                                });
                            }
                        },
                        "Close": function() {
                            $(this).dialog("close");
                            $('.reportdatebox').val("");
                            $('.spotforwardbox').val("");
                            $('.ccybox').val("");
                            $('.amountbox').val("");
                            $('.adjamountbox').val(""); 
                            $('.remarkbox').val("");                           
                        }
                    }
                });                
                return false;
            });
            
            $(document).on("click", ".editposition", function(e) {  
                var detailid = $(this).attr('alt'); 
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1000,
                    modal: true,
                    open: function() { 
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/ForeignExchange/frm_PositionView.aspx/GetPositionDetail',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({'DETAILID':detailid}),  
                            success: function (data) {
                                var obj = data.d;                          
                                $('.dealnobox').val(obj[0].DEALNO);
                                $('.dealdatebox').val(obj[0].DEALDATE_STR);
                                $('.settledatebox').val(obj[0].SETTLEDATE_STR);
                                $('.reportdatebox').val(obj[0].REPORTDATE_STR);
                                $('.prodtypebox').val(obj[0].PRODTYPE);
                                $('.productbox').val(obj[0].PRODUCT);                                
                                $('.spotforwardbox').val(obj[0].SPOTFWDIND)
                                $('.ccybox').val(obj[0].CCY);
                                $('.amountbox').val(obj[0].AMOUNT);
                                $('.amountbox').attr('readonly','true');
                                $('.adjamountbox').val(obj[0].ADJAMOUNT);
                                $('.remarkbox').val(obj[0].REMARK);
                                
                                $('.dealnobox').attr('readonly','true');
                                $('.dealdatebox').attr('readonly','true');
                                $('.settledatebox').attr('readonly','true');
                                $('.reportdatebox').attr('readonly','true');
                                $('.prodtypebox').attr('readonly','true');
                                $('.productbox').attr('readonly','true');                          
                                
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                var err = JSON.parse(xhr.responseText);
                                alert(err.Message);
                            }
                        });
                    },
                    "buttons": {
                        "Submit": function() {  
                            var result = true;    
                            
                            if ($('.reportdatebox').val().trim()=='') {
                                alert('Report Date is require!!');  
                                result = false;
                            }
                            else if ($('.ccybox').val().trim()=='') {
                                alert('CCY is require!!');
                                result = false;
                            }
                            else if ($('.amountbox').val().trim()=='') {
                                alert('Amount is require!!');
                                result = false;
                            }
                            if(result) {                          
                                var record = {
                                                DEALDATE:$.datepicker.parseDate('dd/mm/yy',$('.dealdatebox').val()),
                                                SETTLEDATE:$.datepicker.parseDate('dd/mm/yy',$('.settledatebox').val()),
                                                REPORTDATE:$.datepicker.parseDate('dd/mm/yy',$('.reportdatebox').val()),
                                                SPOTFWDIND:$('.spotforwardbox').val(),
                                                CCY:$('.ccybox').val(),
                                                AMOUNT:$('.amountbox').val(),
                                                ADJAMOUNT:$('.adjamountbox').val(),
                                                REMARK: $('.remarkbox').val(),
                                                DETAILID: detailid
                                             }; 
                                 
                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/ForeignExchange/frm_PositionView.aspx/UpdatePositionDetail',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({'record':record}),  
                                    success: function (data) {
                                        if(data.d.Result=="OK") {
                                            alert("Update Deal Complete");
                                            $("#<%=btnSubmit.ClientID %>").click();
                                        } else {
                                            aler(data.d.Message);
                                        }
                                    },
                                    error: function (xhr, ajaxOptions, thrownError) {
                                        var err = JSON.parse(xhr.responseText);
                                        alert(err.Message);
                                    }
                                });
                            }
                        },
                        "Close": function() {
                            $('.dealnobox').val('');
                            $('.dealdatebox').val('');
                            $('.settledatebox').val('');
                            $('.reportdatebox').val('');
                            $('.prodtypebox').val('');
                            $('.productbox').val('');
                            $('.ccybox').val('');
                            $('.amountbox').val('');
                            $('.adjamountbox').val('');
                            $('.remarkbox').val('');
                            $(this).dialog("close");
                            
                        }
                    }
                });               
                return false;
            });
            
            $(document).on("click", ".deleteposition", function(e) { 
                if(confirm('Are you sure you want to delete?')) {
                    var detailid = $(this).attr('alt');alert(detailid);
                    $.ajax({
                        type: "POST",
                        url: '<%=Config.Website %>/ForeignExchange/frm_PositionView.aspx/DeletePositionDetail',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({'DETAILID':detailid}),  
                        success: function (data) {
                            if(data.d.Result=="OK") {
                                alert("Delete Deal Complete");
                                $("#<%=btnSubmit.ClientID %>").click();
                            } else {
                                alert(data.d.Message);
                            }
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            var err = JSON.parse(xhr.responseText);
                            alert(err.Message);
                        }
                    });
                } return false;
            });
        });
             
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <table>
            <tr>
                <td>DealNo</td>
                <td>
                    <input type="text" name="DealNoBox" id="DealNoBox" class="dealnobox" readonly="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Deal Date</td>
                <td>
                    <input type="text" name="DealDateBox" id="DealDateBox" class="dealdatebox" readonly="true" runat="server" />
                </td>
                <td>Settle Date</td>
                <td>
                    <input type="text" name="SettleDateBox" id="SettleDateBox" class="settledatebox" readonly="true" runat="server" />
                </td>
                <td>Report Date</td>
                <td>
                    <input type="text" name="ReportDateBox" id="ReportDateBox" class="reportdatebox" readonly="false" runat="server" /> <font color="red">
                    *</font>
                </td>
            </tr>
            <tr>
                <td>Product Type</td>
                <td>
                    <input type="text" name="ProductTypeBox" id="ProductTypeBox" class="producttypebox" readonly="true" runat="server" />
                </td>
                <td>Product</td>
                <td>
                    <input type="text" name="ProductBox" id="ProductBox" class="productbox" readonly="true" runat="server" />  
                </td>
                <td>Spot/Forward</td>
                <td>
                    <select id="SpotFowardBox"  name="SpotFowardBox" class="spotforwardbox" runat="server">
                        <option value="S">Spot</option>
                        <option value="F">Forward</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>CCY</td>
                <td>
                    <input type="text" name="ccybox" id="ccybox" class="ccybox" runat="server" /> <font color="red">*</font>
                </td>
                <td>Amount</td>
                <td>
                    <input type="text" name="AmountBox" id="AmountBox" class="amountbox" value="0" runat="server" /> <font color="red">*</font>
                </td>
                <td>Adj. Amount</td>
                <td>
                    <input type="text" name="AdjAmountBox" id="AdjAmountBox" class="adjamountbox" value="0" runat="server" /> <font color="red">*</font>
                </td>
            </tr>
                <td>Remark</td>
                <td>
                    <input type="text" id="RemarkBox" name="RemarkBox" class="remarkbox" runat="server" />
                </td>
            </tr>
        </table>  
    </div>
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
                <fieldset style="vertical-align: middle;">
                    <div style="background-color: Blue; color: White; font: 10pt tahoma; font-weight: bold;padding: 11px; width: 150px; height:100%; float:left; vertical-align: middle">Re-Import Deal</div>
                    <div style="float:left; margin-left: 10px; vertical-align:middle; width: 100px">Execute Date</div>
                    <div style="float:left; width:500px"><input type="text" name="ExecuteDateBox" id="ExecuteDateBox" runat="server" class="round input-form-textbox"/></div>
                    <div style="float:left;"><asp:Button ID="ReImportButton" Text="EXECUTE" runat="server" class="round blue button-submit" onclick="ReImportButton_Click"/></div>
                    </div>
                </fieldset>
                <div>&nbsp;</div>
                <fieldset style="vertical-align: middle;">
                    <div style="background-color: Blue; color: White; font: 10pt tahoma; font-weight:bold; padding: 11px; width: 150px; float:left">Re-generate Fee</div>    
                    <div  style="float:left; margin-left: 10px; vertical-align:middle; width: 100px">Report</div>
                    <div style="float:left; width:300px;vertical-align: middle;">
		            <asp:DropDownList ID="ReportTypeBox" runat="server" class="round input-form-textbox">
		                <asp:ListItem Text="Daily" Value="Daily"></asp:ListItem>
		                <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
		            </asp:DropDownList>
		            </div>  
		            <div style="clear:both"></div>     
		            <div  style="float:left; margin-left: 182px; vertical-align:middle; width: 100px">From</div>
		            <div style="float:left; width:500px">
		            <input type="text" id="ExecuteFromDateBox" size="15" runat="server" class="round input-form-textbox" />								
		            To <input type="text" id="ExecuteToDateBox" size="15" runat="server" class="round input-form-textbox" />
		            </div>
		            <div style="float:left;">	            
                    <asp:Button ID="ReGenBOTFileButton" Text="Re-Generage BOT File" runat="server" class="round blue button-submit" onclick="ReGenBOTFileButton_Click"/>
                    </div>
                </fieldset>	
                
                			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf">
	
			<div class="half-size-column fl">	
			    <fieldset>
                    <div style="float:left; width:100px">Report Date</div>
                    <div style="float:left; width:250px"><asp:TextBox ID="DataDateSearchBox" Runat="server"></asp:TextBox></div>
                    <div style="float:left; width:150px"><asp:Button ID="btnSubmit" Text="Search" runat="server" class="round blue button-submit" onclick="btnSubmit_Click" /></div>            	    
	                <div style="float:left; width:200px"><asp:Button ID="btnAddPosition" CSSClass="round blue button-submit addposition" Text="Add Other Deal" runat="server" OnClientClick="return false;" /></div>
	                <div style="float:right; width:200px"><asp:Button ID="ExportToExcelButton" CSSClass="round blue button-submit" Text="Export" runat="server" OnClick="ExportToExcelButton_Click" /></div>
	            </fieldset>
	            <div>Spot Position</div>
	            <div style="overflow: scroll; height: 400px"> 
	                <asp:GridView ID="GridFXSpotSummary" CssClass="GridViewStyle" Style="margin-bottom: 0px" runat="server">
	                    <EmptyDataTemplate>
                            No Data
                        </EmptyDataTemplate>               
                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
	                </asp:GridView>
                    <asp:GridView ID="GridFXSpot" CssClass="GridViewStyle" runat="server">
                        <Columns>
                        <asp:TemplateField HeaderStyle-Width="39" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img class="btnshowdetail" src="../Theme/icon/icon-tracking.png" style="cursor:pointer"/>                                    
                                    <asp:Panel ID="AllotmentSpotDetail" runat="server" Style="display:none">
                                        <asp:GridView ID="GridFXSpotDetail" CellPadding="0" CellSpacing="0" CssClass="GridViewStyle" runat="server">                                            
                                            <Columns>
                                            <asp:TemplateField HeaderStyle-Width="48" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <img src="../Theme/icon/actions-edit.png" class="editposition" alt="<%# DataBinder.Eval(Container.DataItem, "DETAILID") %>" style="cursor:pointer" />
                                                    <img src="../Theme/icon/actions-delete.png" class="deleteposition" alt="<%# DataBinder.Eval(Container.DataItem, "DETAILID") %>" style="cursor:pointer" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No Data
                                            </EmptyDataTemplate>              
                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle CssClass="HeaderStyle"/>
                                            <EditRowStyle CssClass="EditRowStyle" />
                                            <RowStyle CssClass="RowStyle" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>                     
                        </Columns>                                       
                        <EmptyDataTemplate>
                            No Data
                        </EmptyDataTemplate>               
                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                    </asp:GridView>   
                    <div>&nbsp;</div>
                    <div>Forward Position</div>
	                <asp:GridView ID="GridFXForwardSummary" CssClass="GridViewStyle" Style="margin-bottom: 0px" runat="server">
	                    <EmptyDataTemplate>
                            No Data
                        </EmptyDataTemplate>               
                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
	                </asp:GridView>               
                    <asp:GridView ID="GridFXForward" CssClass="GridViewStyle" runat="server">
                        <Columns>
                        <asp:TemplateField  HeaderStyle-Width="39" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img class="btnshowdetail" src="../Theme/icon/icon-tracking.png" style="cursor:pointer"/>
                                    <asp:Panel ID="AllotmentForwardDetail" runat="server" Style="display:none">
                                        <asp:GridView ID="GridFXForwardDetail" CellPadding="0" CellSpacing="0" CssClass="GridViewStyle" runat="server">                                            
                                            <Columns>
                                            <asp:TemplateField HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <img src="../Theme/icon/actions-edit.png" class="editposition" alt="<%# DataBinder.Eval(Container.DataItem, "DETAILID") %>" style="cursor:pointer" />
                                                    &nbsp;&nbsp;
                                                    <img src="../Theme/icon/actions-delete.png" class="deleteposition" alt="<%# DataBinder.Eval(Container.DataItem, "DETAILID") %>" style="cursor:pointer" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No Data
                                            </EmptyDataTemplate>              
                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle CssClass="HeaderStyle"/>
                                            <EditRowStyle CssClass="EditRowStyle" />
                                            <RowStyle CssClass="RowStyle" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>                     
                        </Columns>                                       
                        <EmptyDataTemplate>
                            No Data
                        </EmptyDataTemplate>               
                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <RowStyle CssClass="RowStyle" />
                    </asp:GridView>                        
                </div>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->		
		
	<asp:GridView ID="GridFX" runat="server" Visible="false" CssClass="GridViewStyle">
	    <RowStyle CssClass="RowStyle" />
	    <HeaderStyle CssClass="HeaderStyle" />
	</asp:GridView>
</asp:Content>
