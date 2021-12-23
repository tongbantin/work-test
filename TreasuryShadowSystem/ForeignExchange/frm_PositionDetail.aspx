<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_PositionDetail.aspx.cs" Inherits="TreasuryShadowSystem.ForeignExchange.frm_PositionDetail" Title="Untitled Page" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <style> input[readonly]{ background-color: InactiveCaption} </style>
    <script type="text/javascript">
        $(document).ready(function() {
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
            
            $(".addposition").click(function() {
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1000,
                    modal: true,
                    open: function() {
                    
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
                                    url: '<%=Config.Website %>/ForeignExchange/frm_PositionDetail.aspx/AddPositionDetail',
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
                     
            $(".editposition").click(function() { 
                var detailid = $(this).attr('alt'); 
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1000,
                    modal: true,
                    open: function() { 
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/ForeignExchange/frm_PositionDetail.aspx/GetPositionDetail',
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
                                $('.amountbox').attr('readonly','readonly');
                                $('.adjamountbox').val(obj[0].ADJAMOUNT);
                                $('.remarkbox').val(obj[0].REMARK);
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
                                    url: '<%=Config.Website %>/ForeignExchange/frm_PositionDetail.aspx/UpdatePositionDetail',
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
            
            $(".deleteposition").click(function() {  
                if(confirm('Are you sure you want to delete?')) {
                    var detailid = $(this).attr('alt');
                    $.ajax({
                        type: "POST",
                        url: '<%=Config.Website %>/ForeignExchange/frm_PositionDetail.aspx/DeletePositionDetail',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({'DetailID':detailid}),  
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
                    <input type="text" name="DealNoBox" id="DealNoBox" class="dealnobox" readonly runat="server" /> <font color="red">
                    *</font>
                </td>
            </tr>
            <tr>
                <td>Deal Date</td>
                <td>
                    <input type="text" name="DealDateBox" id="DealDateBox" class="dealdatebox" readonly runat="server" /> <font color="red">
                    *</font>
                </td>
                <td>Settle Date</td>
                <td>
                    <input type="text" name="SettleDateBox" id="SettleDateBox" class="settledatebox" readonly runat="server" /> <font color="red">
                    *</font>
                </td>
                <td>Report Date</td>
                <td>
                    <input type="text" name="ReportDateBox" id="ReportDateBox" class="reportdatebox" readonly runat="server" /> <font color="red">
                    *</font>
                </td>
            </tr>
            <tr>
                <td>Product Type</td>
                <td>
                    <input type="text" name="ProductTypeBox" id="ProductTypeBox" class="producttypebox" readonly runat="server" /> <font color="red">
                    *</font>
                </td>
                <td>Product</td>
                <td>
                    <input type="text" name="ProductBox" id="ProductBox" class="productbox" readonly runat="server" />  
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
                    <input type="text" name="ccybox" id="ccybox" class="ccybox" runat="server" />
                </td>
                <td>Amount</td>
                <td>
                    <input type="text" name="AmountBox" id="AmountBox" class="amountbox" runat="server" /> <font color="red">
                    *</font>
                </td>
                <td>Adj. Amount</td>
                <td>
                    <input type="text" name="AdjAmountBox" id="AdjAmountBox" class="adjamountbox" runat="server" /> <font color="red">
                    *</font>
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
                <fieldset>
	                <table border="0">
	                    <tbody>            	                        	            
	                        <tr>
	                            <td class="td-display">DealNo</td>
	                            <td class="td-field">
	                                <asp:TextBox ID="DealNoSearchBox" Runat="server"></asp:TextBox></td>
	                        </tr>       	                        	            
	                        <tr>
	                            <td class="td-display">Value Date</td>
	                            <td class="td-field">
	                                <asp:TextBox ID="ValueDateSearchBox" Runat="server"></asp:TextBox></td>
	                        </tr>
	                        <tr>
	                            <td colspan="2" style="width:800px; text-align: left;">
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
	            <div style="overflow: scroll; height: 827px">
			        <div style="float:left"><button class="round blue button-submit addposition">Add 
                        Other Deal</button></div>
			        <br /><br />			    	
                    <asp:GridView ID="GridDealPosition" CellPadding="3" CellSpacing="3" DataKeyNames="DETAILID" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                        <Columns>                            
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-edit.png" class="editposition" alt="<%# DataBinder.Eval(Container.DataItem, "DETAILID") %>" style="cursor:pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-delete.png" class="deleteposition" alt="<%# DataBinder.Eval(Container.DataItem, "DETAILID") %>" style="cursor:pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DEALNO" ItemStyle-HorizontalAlign="Center" HeaderText="DealNo." /> 
                            <asp:BoundField DataField="DEALDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Right" HeaderText="Deal Date" /> 
                            <asp:BoundField DataField="PRODTYPE" ItemStyle-HorizontalAlign="Center" HeaderText="Product Type" /> 
                            <asp:BoundField DataField="PRODUCT" ItemStyle-HorizontalAlign="Center" HeaderText="Product" /> 
                            <asp:BoundField DataField="CCY" ItemStyle-HorizontalAlign="Left" HeaderText="CCY" /> 
                            <asp:BoundField DataField="AMOUNT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" HeaderText="Amount" /> 
                            <asp:BoundField DataField="ADJAMOUNT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" HeaderText="Adj. Amount" /> 
                            <asp:BoundField DataField="REMARK" ItemStyle-HorizontalAlign="Right" HeaderText="Remark" />                             
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
                    
                    <div>        
                        <fieldset style="padding: 10px; margin: 10px;">
                            <legend style="background-color: Blue; color: White; font: 10pt tahoma bold;padding: 3px;">
                                Re-Import Deal</legend>
                            Execute Date
                            <input type="text" name="ExecuteDateBox" id="ExecuteDateBox" runat="server" class="round input-form-textbox"/> 
                            <asp:Button ID="ReImportButton" Text="Submit" runat="server" 
                                class="round blue button-submit" onclick="ReImportButton_Click"/>
                        </fieldset>
                    </div>                    
                </div>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->		
	
</asp:Content>
