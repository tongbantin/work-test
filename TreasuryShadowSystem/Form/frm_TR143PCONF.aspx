<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR143PCONF.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR143PCONF" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {


            $('#ctl00_bodyContent_SearchDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            $('.valuedatebox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $('.tradedatebox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });


            var originalContentSeries = $("#dialog").html();

            $(".ic-add").click(function() {
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1000,
                    modal: true,
                    open: function() {

                    },
                    close: function() {
                        $(this).dialog("close");
                        clearBoxVal();
                    },
                    "buttons": {
                        "Submit": function() {

                            var result = true;

                            if (result) {
                                var record = {
                                    PCON_BROK: $('#<%=BrokerBox.ClientID %>').val(),
                                    PCON_BTYPE: $('#<%=TypeBox.ClientID %>').val(),
                                    PCON_CUST: $('.customerbox').val(),
                                    PCON_VDATE: $.datepicker.parseDate('dd/mm/yy', $('.valuedatebox').val()),
                                    PCON_VDATE: $.datepicker.parseDate('dd/mm/yy', $('.tradedatebox').val()),
                                    PCON_RATE: $('.ratebox').val(),
                                    PCON_SCUR: $('.scurrbox').val(),
                                    PCON_SAMT: $('.samtbox').val(),
                                    PCON_BCUR: $('.bcurrbox').val(),
                                    PCON_BAMT: $('.bamtbox').val(),
                                    PCON_NF: $('#<%=NFBox.ClientID %>').val()
                                };

                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/Form/frm_TR143PCONF.aspx/AddPreconf',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ 'record': record }),
                                    success: function(data) {
                                        if (data.d.Result == "OK") {
                                            alert("Add PreConfirmation Complete");
                                            $("#<%=SubmitBtn.ClientID %>").click();
                                        } else {
                                            alert(data.d.Message);
                                        }
                                    },
                                    error: function(xhr, ajaxOptions, thrownError) {
                                        var err = JSON.parse(xhr.responseText);
                                        alert(err.Message);
                                    }
                                });
                            }
                        },
                        "Close": function() {
                            $(this).dialog("close");
                            clearBoxVal();
                        }
                    }
                });
                return false;
            });


            $(".fill-in").click(function() {
                var detail = $(".detailText").val();
                var selBroker = $('#<%=BrokerListBox.ClientID %>').val();
                var selType = $('#<%=TypeListBox.ClientID %>').val();
                var selNF = $('#<%=nfList.ClientID %>').val();

                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1000,
                    modal: true,
                    open: function() {

                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/Form/frm_TR143PCONF.aspx/ClickFillin',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({'DETAIL': detail,'SELBROK': selBroker,'SELTYPE': selType,'SELNF':selNF}),
                            success: function(data) {
                                var obj = data.d;
                                $('#<%=BrokerBox.ClientID %>').val(selBroker);
                                $('#<%=TypeBox.ClientID %>').val(selType);
                                $('#<%=NFBox.ClientID %>').val(selNF);
                                $('.customerbox').val(obj[0].F_PCON_CUST);
                                $('.valuedatebox').val(obj[0].F_PCON_VDATE_STR);
                                $('.tradedatebox').val(obj[0].F_PCON_TDATE_STR);
                                $('.ratebox').val(obj[0].F_PCON_RATE);
                                $('.scurrbox').val(obj[0].F_PCON_SCUR);
                                $('.samtbox').val(obj[0].F_PCON_SAMT);
                                $('.bcurrbox').val(obj[0].F_PCON_BCUR);
                                $('.bamtbox').val(obj[0].F_PCON_BAMT);

                            },
                            error: function(xhr, ajaxOptions, thrownError) {
                                var err = JSON.parse(xhr.responseText);
                                alert(err.Message);
                            }
                        });
                    },
                    close: function() {
                        $(this).dialog("close");
                        clearBoxVal();
                    },
                    "buttons": {
                        "Submit": function() {

                            var result = true;

                            if (result) {
                                var record = {
                                    PCON_BROK: $('#<%=BrokerBox.ClientID %>').val(),
                                    PCON_BTYPE: $('#<%=TypeBox.ClientID %>').val(),
                                    PCON_CUST: $('.customerbox').val(),
                                    PCON_VDATE: $.datepicker.parseDate('dd/mm/yy', $('.valuedatebox').val()),
                                    PCON_TDATE: $.datepicker.parseDate('dd/mm/yy', $('.tradedatebox').val()),
                                    PCON_RATE: $('.ratebox').val(),
                                    PCON_SCUR: $('.scurrbox').val(),
                                    PCON_SAMT: $('.samtbox').val(),
                                    PCON_BCUR: $('.bcurrbox').val(),
                                    PCON_BAMT: $('.bamtbox').val(),
                                    PCON_NF: $('#<%=NFBox.ClientID %>').val()
                                };

                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/Form/frm_TR143PCONF.aspx/AddPreconf',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ 'record': record }),
                                    success: function(data) {
                                        if (data.d.Result == "OK") {
                                            alert("Add PreConfirmation Complete");

                                            //clear data in detail
                                            clearFormVal();

                                            $("#<%=SubmitBtn.ClientID %>").click();
                                        } else {
                                            alert(data.d.Message);
                                        }
                                    },
                                    error: function(xhr, ajaxOptions, thrownError) {
                                        var err = JSON.parse(xhr.responseText);
                                        alert(err.Message);
                                    }
                                });
                            }
                        },
                        "Close": function() {
                            $(this).dialog("close");
                            clearBoxVal();
                        }
                    }
                });
                return false;
            });

            $(".editpconf").click(function() {
                var pconid = $(this).attr('alt');
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1100,
                    modal: true,
                    open: function() {
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/Form/frm_TR143PCONF.aspx/GetPreconf',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ 'PCON_ID': pconid }),
                            success: function(data) {
                                var obj = data.d;
                                $('#<%=BrokerBox.ClientID %>').val(obj[0].PCON_BROK)
                                $('#<%=TypeBox.ClientID %>').val(obj[0].PCON_BTYPE);
                                $('.customerbox').val(obj[0].PCON_CUST);
                                $('.valuedatebox').val(obj[0].PCON_VDATE_STR);
                                $('.tradedatebox').val(obj[0].PCON_TDATE_STR);
                                $('.ratebox').val(obj[0].PCON_RATE);
                                $('.scurrbox').val(obj[0].PCON_SCUR);
                                $('.samtbox').val(obj[0].PCON_SAMT);
                                $('.bcurrbox').val(obj[0].PCON_BCUR);
                                $('.bamtbox').val(obj[0].PCON_BAMT);
                                $('#<%=NFBox.ClientID %>').val(obj[0].PCON_NF);

                            },
                            error: function(xhr, ajaxOptions, thrownError) {
                                var err = JSON.parse(xhr.responseText);
                                alert(err.Message);
                            }
                        });
                    },
                    close: function() {
                        $(this).dialog("close");
                        clearBoxVal();

                    },
                    "buttons": {
                        "Submit": function() {
                            var result = true;

                            if (result) {
                                var record = {
                                    PCON_BROK: $('#<%=BrokerBox.ClientID %>').val(),
                                    PCON_BTYPE: $('#<%=TypeBox.ClientID %>').val(),
                                    PCON_CUST: $('.customerbox').val(),
                                    PCON_VDATE: $.datepicker.parseDate('dd/mm/yy', $('.valuedatebox').val()),
                                    PCON_TDATE: $.datepicker.parseDate('dd/mm/yy', $('.tradedatebox').val()),
                                    PCON_RATE: $('.ratebox').val(),
                                    PCON_SCUR: $('.scurrbox').val(),
                                    PCON_SAMT: $('.samtbox').val(),
                                    PCON_BCUR: $('.bcurrbox').val(),
                                    PCON_BAMT: $('.bamtbox').val(),
                                    PCON_NF: $('#<%=NFBox.ClientID %>').val(),
                                    PCON_ID: pconid
                                };

                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/Form/frm_TR143PCONF.aspx/UpdPreconf',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ 'record': record }),
                                    success: function(data) {
                                        if (data.d.Result == "OK") {
                                            alert("Update Preconfirmation Complete");
                                            $("#<%=SubmitBtn.ClientID %>").click();
                                        } else {
                                            aler(data.d.Message);
                                        }
                                    },
                                    error: function(xhr, ajaxOptions, thrownError) {
                                        var err = JSON.parse(xhr.responseText);
                                        alert(err.Message);
                                    }
                                });
                            }

                        },
                        "Close": function() {
                            $(this).dialog("close");
                            clearBoxVal();
                        }
                    }
                });
                return false;
            });

            $(".deletepconf").click(function() {
                if (confirm('Are you sure you want to delete?')) {
                    var pconid = $(this).attr('alt');
                    $.ajax({
                        type: "POST",
                        url: '<%=Config.Website %>/Form/frm_TR143PCONF.aspx/DeletePreconf',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ 'PCON_ID': pconid }),
                        success: function(data) {
                            if (data.d.Result == "OK") {
                                alert("Delete Preconfirmation Complete");
                                $("#<%=SubmitBtn.ClientID %>").click();
                            } else {
                                alert(data.d.Message);
                            }
                        },
                        error: function(xhr, ajaxOptions, thrownError) {
                            var err = JSON.parse(xhr.responseText);
                            alert(err.Message);
                        }
                    });
                } return false;
            });

        });
             
    </script>
    <script type="text/javascript">
        function typebox_changed(ddl) {
            if (ddl.value == 'Swap') {
                document.getElementById('nfbox').style.display = 'inline';
                document.getElementById('nfList').style.display = 'inline';                
            } else {
            document.getElementById('nfbox').style.display = 'none';
            document.getElementById('nfList').style.display = 'none'; 
            }
        }

        function clearFormVal() {
            $('.detailText').val("");
            $('#<%=BrokerListBox.ClientID %>').val("");
            $('#<%=TypeListBox.ClientID %>').val("");
            $('#<%=nfList.ClientID %>').val("");
        }

        function clearBoxVal() {

            document.getElementById('nfbox').style.display = 'none';
            $('#<%=BrokerBox.ClientID %>').val("");
            $('#<%=TypeBox.ClientID %>').val("");
            $('#<%=NFBox.ClientID %>').val("");
            $('.customerbox').val("");
            $('.valuedatebox').val("");
            $('.tradedatebox').val("");
            $('.ratebox').val("");
            $('.scurrbox').val("");
            $('.samtbox').val("");
            $('.bcurrbox').val("");
            $('.bamtbox').val("");        
        }
        
    
    </script>   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server"> 
  
  
    <div id="dialog" style="display: none;">
        <table>
            <tr>
                <td>Broker</td>
                <td>
                     <asp:DropDownList ID="BrokerBox" runat="server" ></asp:DropDownList>                   
                </td>
                <td>Type</td>
                <td>
                    <asp:DropDownList ID="TypeBox" runat="server"  onchange="typebox_changed(this)">
							                <asp:ListItem Text="Outright" Value="Outright"></asp:ListItem>
							                <asp:ListItem Text="Forward" Value="Forward"></asp:ListItem>
							                <asp:ListItem Text="Spot" Value="Spot"></asp:ListItem>
							                <asp:ListItem Text="Swap" Value="Swap"></asp:ListItem>
					</asp:DropDownList>	
					<div id="nfbox" style="display:none">
					<asp:DropDownList ID="NFBox" runat="server">
							                <asp:ListItem Text="Near leg" Value="N"></asp:ListItem>
							                <asp:ListItem Text="Far leg" Value="F"></asp:ListItem>							                
					</asp:DropDownList></div>
			    </td>                                   
            </tr>            
            <tr>
                <td>Trade Date</td>
                <td>
                   <input type="text" name="TradeDateBox" id="TradeDateBox" class="tradedatebox"  runat="server" />
                </td>
                 <td>Value Date</td>
                <td>
                    <input type="text" name="ValueDateBox" id="ValueDateBox" class="valuedatebox"  runat="server" />
                </td>            
            </tr>
            <tr>
                <td>Rate</td>
                <td colspan="3">
                  <input type="text" name="RateBox" id="RateBox" class="ratebox" runat="server" /> 
                </td>                
            </tr>
            <tr>
                <td>Buy Currency</td>
                <td>
                   <input type="text" name="BCurrBox" id="BCurrBox" class="bcurrbox"  maxlength="3" runat="server" />
                </td>
                 <td>Buy Amount</td>
                <td>
                    <input type="text" name="BAmtBox" id="BAmtBox" class="bamtbox" runat="server" /> 
                </td>            
            </tr> 
             <tr>
                <td>Sell Currency</td>
                <td>
                   <input type="text" name="SCurrBox" id="SCurrBox" class="scurrbox"  maxlength="3" runat="server" />
                </td>
                 <td>Sell Amount</td>
                <td>
                    <input type="text" name="SAmtBox" id="SAmtBox" class="samtbox" runat="server" /> 
                </td>            
            </tr>          
        </table>  
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Preconfirmation</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">		   
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0">
	                    <tbody> 
	                        <tr style="height:auto">
	                            <td colspan="4">
	                                <div style="float:right">
		                           <span>Business Process : </span>
		                            <asp:HyperLink ID="ProcessConf" NavigateUrl="~/Form/frm_TR143PCONFIG.aspx" Text="1.Configuration Format" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		                            <asp:HyperLink ID="AddPconf" NavigateUrl="~/Form/frm_TR143PCONF.aspx" Text="2.Add PreConfirmation" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		                            <asp:HyperLink ID="MatchPconf" NavigateUrl="~/Form/frm_TR143PCONFREC.aspx" Text="3.Match Data" CssClass="round blue button-submit" runat="server"></asp:HyperLink>
		                            </div>   
	                            </td>
	                        </tr> 
	                         <tr>
	                            <td class="td-field" colspan="4">&nbsp;</td>
	                        </tr>	          	                        	            
	                        <tr>
	                            <td class="td-display">Deal Date</td>
	                            <td class="td-field"><asp:TextBox ID="SearchDateBox" Runat="server"></asp:TextBox></td>
	                            <td style="width:400px; text-align: left;"><asp:Button ID="SubmitBtn" Text="Search" runat="server" 
                                        class="round blue button-submit" onclick="btnSubmit_Click" /> 
                                </td>
	                            <td>&nbsp;</td>	                          
	                        </tr>       	                        	            
	                        <tr>
	                            <td class="td-display">Broker</td>	                           
							    <td class="td-field">
							            <asp:DropDownList ID="BrokerListBox" runat="server"></asp:DropDownList>
							    </td>
							   <td colspan="2">&nbsp;</td>							  
	                        </tr>
	                        <tr>
	                            <td class="td-display">Type</td>
	                            <td class="td-field"  style="float:left">	                               
	                                <asp:DropDownList ID="TypeListBox" runat="server" onchange="typebox_changed(this)">
							                <asp:ListItem Text="Outright" Value="Outright"></asp:ListItem>
							                <asp:ListItem Text="Forward" Value="Forward"></asp:ListItem>
							                <asp:ListItem Text="Spot" Value="Spot"></asp:ListItem>
							                <asp:ListItem Text="Swap" Value="Swap"></asp:ListItem>
							        </asp:DropDownList>
	                            <div id="nfList" style="display:none">
					                <asp:DropDownList ID="nfList" runat="server">
							                <asp:ListItem Text="Near leg" Value="N"></asp:ListItem>
							                <asp:ListItem Text="Far leg" Value="F"></asp:ListItem>							                
					                </asp:DropDownList>
					            </div>
					            </td>
	                            <td colspan="2">&nbsp;</td>	                            
	                        </tr>
	                        <tr>
	                            <td class="td-display">Detail</td>
	                            <td class="td-field" colspan="2">
	                            <textarea id="DetailText" cols="100" rows="6" class="detailText" Runat="server"></textarea>
	                           </td>
	                            <td style="width:200px; text-align: left;">	                          
                                    <button class="round blue button-submit fill-in">Fill In</button>
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
			        <div style="float:left"><button class="round blue button-submit ic-add">Add Preconfirmation</button></div>
			        <div style="float:right"></div>
			        <br /><br />			    	
                    <asp:GridView ID="GridPConf" CellPadding="3" CellSpacing="3" DataKeyNames="PCON_ID" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                        <Columns>                           
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-edit.png" class="editpconf" alt="<%# DataBinder.Eval(Container.DataItem, "PCON_ID") %>" style="cursor:pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-delete.png" class="deletepconf" alt="<%# DataBinder.Eval(Container.DataItem, "PCON_ID") %>" style="cursor:pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>                           
                            <asp:BoundField DataField="PCON_BROK" ItemStyle-HorizontalAlign="Center" HeaderText="Broker" /> 
                            <asp:BoundField DataField="PCON_BTYPE" ItemStyle-HorizontalAlign="Center" HeaderText="Product Type" /> 
                            <asp:BoundField DataField="PCON_CUST" ItemStyle-HorizontalAlign="Center" HeaderText="Customer" /> 
                            <asp:BoundField DataField="PCON_VDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Value Date" /> 
                            <asp:BoundField DataField="PCON_TDATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Trade Date" /> 
                            <asp:BoundField DataField="PCON_NF" ItemStyle-HorizontalAlign="Center" HeaderText="Near/Far" />
                            <asp:BoundField DataField="PCON_RATE" ItemStyle-HorizontalAlign="Right"  HeaderText="Rate" /> 
                             <asp:BoundField DataField="PCON_SCUR" ItemStyle-HorizontalAlign="Center" HeaderText="Sell Currency" /> 
                            <asp:BoundField DataField="PCON_SAMT" ItemStyle-HorizontalAlign="Right"  DataFormatString="{0:n}" HeaderText="Sell Amount" /> 
                            <asp:BoundField DataField="PCON_BCUR" ItemStyle-HorizontalAlign="Center" HeaderText="Buy Currency" /> 
                            <asp:BoundField DataField="PCON_BAMT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n}" HeaderText="Buy Amount" />                                                                           
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
		
	</div> <!-- end content-module -->		
	
</asp:Content>
