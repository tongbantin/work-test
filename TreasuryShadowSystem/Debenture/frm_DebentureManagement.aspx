<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_DebentureManagement.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_DebentureManagement" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $('#ctl00_bodyContent_InputDateSearchBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });    
            $('#ctl00_bodyContent_StartDateSearchBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });    
            $('#ctl00_bodyContent_ToDateSearchBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });    
            
            function submitForm() {
                if ($('.dealnobox').val().trim() == '') {
                    alert('DealNo is require');
                    return false;
                }

                if ($('.secidbox').val().trim() == '') {
                    alert('SecID is require');
                    return false;
                }

                if ($('.unitbox').val().trim() == '') {
                    alert('Unit is require');
                    return false;
                }

                if ($('.amountbox').val().trim() == '') {
                    alert('Amount is require');
                    return false;
                }
                

                $('#<%=DealNo.ClientID %>').val($('.dealnobox').val());
                $('#<%=SecID.ClientID %>').val($('.secidbox').val());
                $('#<%=Unit.ClientID %>').val($('.unitbox').val());
                $('#<%=Amount.ClientID %>').val($('.amountbox').val());
                $('#<%=Custodian.ClientID %>').val($('.custodianbox').val());
                $('#<%=NameThai.ClientID %>').val($('.namethaibox').val());
                $('#<%=CustID.ClientID %>').val($('.custidbox').val());
                $('#<%=ThaiBMA.ClientID %>').val($('.thaibmabox').val());
                $('#<%=CustType.ClientID %>').val($('.custtypebox').val());
                $('#<%=CType.ClientID %>').val($('.ctypebox').val());
                $('#<%=BOTType.ClientID %>').val($('.bottypebox').val());
                $('#<%=CustomerNumber.ClientID %>').val($('.customernumberbox').val());

                event.preventDefault();
                $('input#<%=DialogSubmit.ClientID %>').click();
            }

            $(".ic-add").click(function() {
                $('#<%=ActionBox.ClientID %>').val("Add");
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 600,
                    modal: true,
                    open: function() {
                        $('.dealnobox').val("");
                        $('.secidbox').val("");
                        $('.unitbox').val("");
                        $('.amountbox').val("");
                        $('.custodianbox').val("");
                        $('.namethaibox').val("");
                        $('.custidbox').val("");
                        $('.thaibmabox').val("");
                        $('.custtypebox').val("");
                        $('.ctypebox').val("");
                        $('.bottypebox').val("");
                        $('.customernumberbox').val("");
                    },
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
            
            
            $("[id*=chkAll]").click(function() {
                var chkHeader = $(this);
                var grid = $(this).closest("table");
                $("input[id*=chkDeleteDocument]", grid).each(function() {
                    if (chkHeader.is(":checked")) {
                        $(this).prop("checked", true);
                    } else {
                        $(this).prop("checked", false);
                    }
                });
            });
            $("[id*=chkDeleteDocument]").click(function() {
                var grid = $(this).closest("table");
                var chkHeader = $("[id*=chkAll]", grid);
                if (!$(this).is(":checked")) {
                    chkHeader.prop("checked", false);
                } else {
                    if ($("[id*=chkDeleteDocument]", grid).length == $("[id*=chkDeleteDocument]:checked", grid).length) {
                        chkHeader.prop("checked", true);
                    }
                }
            }); 
            $(".bottypebox").change(function(){
                var bottypeval = $(".bottypebox").val();
                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/Debenture/frm_DebentureManagement.aspx/SecTypeList',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({'BOTType':bottypeval}),  
                                    success: function (data) {
                                        $('.ctypebox').html("");
                                        $.each(data.d.records, function (idx, obj) {
                                             $('.ctypebox').append(
                                                  $('<option></option>').val(obj.Key).html(obj.Value)
                                              );
                                         });
                                    },
                                    error: function (xhr, ajaxOptions, thrownError) {
                                        var err = JSON.parse(xhr.responseText);
                                        alert(err.Message);
                                    }
                                });           
            });
        });
    </script>
    <script type="text/javascript">
        $(function() {
            /*
            $(document).on("keydown.autocomplete", ".customer", function(e) {
            $(this).autocomplete({
            source: function(request, response) {
            $.ajax({
            type: "POST",
            url: "<%=Config.Website %>/Services/Customer.asmx/CustomerJson",
            data: "{cust:'" + request.term + "'}",
            minLength: 4,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {
            //alert(eval("(" + data.d + ")"));
            response(eval("(" + data.d + ")"));
            }
            });
            },
            select: function(event, ui) {
            $(this).parent().parent().children("td:nth-child(1)").find(".custid").val(ui.item.value.trim());
            $(this).parent().parent().parent().children("tr:nth-child(3)").children("td:nth-child(2)").find(".assetcomp").val(ui.item.icon.trim());
            $(this).val(ui.item.desc.trim());
            return false;
            }
            });
            });
            */

            $(document).on("keydown.autocomplete", ".dealnobox", function(e) {
                $(this).autocomplete({
                    source: function(request, response) {
                        $.ajax({
                            type: "POST",
                            url: "<%=Config.Website %>/Services/Opics.asmx/DealNoJson",
                            data: "{deal:'" + request.term + "'}",
                            contentType: "application/json; charset=utf-8",
                            minLength: 4,
                            dataType: "json",
                            success: function(data) {
                                //alert(eval("(" + data.d + ")"));
                                response(eval("(" + data.d + ")"));
                            }
                        });
                    },
                    select: function(event, ui) {
                        $(this).val(ui.item.label.trim());
                        return false;
                    }
                });
            });

            $(document).on("keydown.autocomplete", ".secidbox", function(e) {
                $(this).autocomplete({
                    source: function(request, response) {
                        $.ajax({
                            type: "POST",
                            url: "<%=Config.Website %>/Services/Opics.asmx/SecIDJson",
                            data: "{secid:'" + request.term + "'}",
                            contentType: "application/json; charset=utf-8",
                            minLength: 4,
                            dataType: "json",
                            success: function(data) {
                                //alert(eval("(" + data.d + ")"));
                                response(eval("(" + data.d + ")"));
                            }
                        });
                    },
                    select: function(event, ui) {
                        $(this).val(ui.item.label.trim());
                        return false;
                    }
                });
            });

            $(document).on("keydown.autocomplete", ".namethaibox", function(e) {
                $(this).autocomplete({
                    source: function(request, response) {
                        $.ajax({
                            type: "POST",
                            url: "<%=Config.Website %>/Services/Opicinf.asmx/DebentureCustomer",
                            data: "{name:'" + request.term + "'}",
                            contentType: "application/json; charset=utf-8",
                            minLength: 6,
                            dataType: "json",
                            success: function(data) {
                                //alert(eval("(" + data.d + ")"));
                                response(eval("(" + data.d + ")"));
                            }
                        });
                    },
                    select: function(event, ui) {                        
                        $(this).val(ui.item.label.trim());
                        $(this).parent().parent().parent().children("tr:nth-child(7)").children("td:nth-child(2)").find(".customerbox").val(ui.item.value.trim());
                        return false;
                    }
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <table>
            <tr>
                <td><label for="simple-input">Customer Number(CBS)</label></td>
                <td>
                    <input type="text" name="CustomerNumberBox" ID="CustomerNumberBox" class="customernumberbox" runat="server" /> 
                </td>      
            </tr>
            <tr>
                <td>DealNo</td>
                <td>
                    <input type="text" name="DealNoBox" id="DealNoBox" class="dealnobox" runat="server" /> <font color="red">
                    *</font>
                </td>
            </tr>
            <tr>
                <td>SecID</td>
                <td>
                    <input type="text" name="SecIDBox" id="SecIDBox" class="secidbox" runat="server" /> <font color="red">
                    *</font>
                </td>
            </tr>
            <tr>
                <td>ThaiBMA No.</td>
                <td>
                    <input type="text" name="ThaiBMANoBox" id="ThaiBMANoBox" class="thaibmabox" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Unit</td>
                <td>
                    <input type="text" name="UnitBox" id="UnitBox" class="unitbox" runat="server" /> <font color="red">
                    *</font>
                </td>
            </tr>
            <tr>
                <td>Amount</td>
                <td>
                    <input type="text" id="AmountBox" name="AmountBox" class="amountbox" runat="server" /> <font color="red">
                    *</font>
                </td>
            </tr>
            <tr>
                <td>Custodian</td>
                <td>
                    <input type="text" name="CustodianBox" id="CustodianBox" class="custodianbox" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Name Thai</td>
                <td>
                    <input type="text" name="NameThaiBox" id="NameThaiBox" class="namethaibox" size="40" runat="server" /> <font color="red">
                    *</font>
                </td>
            </tr>
            <tr>
                <td>Customer ID</td>
                <td>
                    <input type="text" name="CustIDBox" id="CustomerBox" class="custidbox" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Customer Type</td>
                <td>
                    <input type="text" name="CustTypeBox" id="CustTypeBox" class="custtypebox" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Customer Type (For BOT)</td>
                <td>
                    <select id="BOTTypeBox" name="BOTTypeBox" class="bottypebox" runat="server">
                    </select>
                </td>
            </tr>
            <tr>
                <td>Customer Type (For SEC)</td>
                <td>
                    <select id="CTypeBox" name="CTypeBox" class="ctypebox" readonly runat="server">
                    </select>
                </td>
            </tr>
        </table>  
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Debenture Management</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0">
	                    <tbody>            	                        	            
	                        <tr>
	                            <td>Input Date</td>
	                            <td>
	                                <asp:DropDownList ID="InputDateRoundSearchBox" Style="Width: 200px" runat="server"></asp:DropDownList></td>	       
	                                           
	                        </tr>
	                        <tr>
	                            <td><label for="simple-input">SECID</label></td>
	                            <td>
	                                <asp:TextBox ID="BondSearchBox" runat="server"></asp:TextBox>
	                            </td>
	                            <td>
	                                <label for="simple-input">Custodian</label></td>
	                            <td class="td-field">
	                                <asp:DropDownList ID="CustodianSearchBox" runat="server"></asp:DropDownList>
	                            </td>
	                        </tr>            	            
	                        <tr>
	                            <td><label for="simple-input">DealNo</label></td>
	                            <td>
	                                <asp:TextBox ID="DealNoSearchBox" Runat="server"></asp:TextBox>
	                            </td>
	                            <td>
	                                <label for="simple-input">Settlement Date</label></td>
	                            <td class="td-field">
	                                <asp:TextBox ID="StartDateSearchBox" Runat="server"></asp:TextBox>&nbsp;- <asp:TextBox ID="ToDateSearchBox" Runat="server"></asp:TextBox>
	                            </td>
	                        </tr>
	                        <tr>
	                            <td colspan="2" style="text-align: left;">
	                                <asp:Button ID="btnSubmit" Text="Search" runat="server" 
                                        class="round blue button-submit" onclick="btnSubmit_Click" />
 	                            </td>
	                            <td style="text-align: left;">
	                                &nbsp;</td>
	                            <td style="width:800px; text-align: left;">
	                                &nbsp;</td>
	                        </tr>
            	            
	                        <tr>
	                            <td colspan="2" style="text-align: left;">
	                                <asp:Label ID="lblMessage" ForeColor="Red" runat="server"></asp:Label></td>
	                            <td style="text-align: left;">
	                                &nbsp;</td>
	                            <td style="width:800px; text-align: left;">
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
	            <div style="overflow: scroll; height: 400px">
			        <div style="float:left"><button class="round blue button-submit ic-add">Add 
                        Debenture</button></div>
                    <div style="float:left;margin-left: 20px">
                    <asp:Button ID="btnDeleteSelected" Text="Delete by Selected" 
                    runat="server" class="round blue button-submit" Visible="false" style="padding:6px; font: 13.3333px Arial; font-weight:bold;"  
                        OnClientClick="return confirm('Do you need deleting rows?')" 
                        onclick="btnDeleteSelected_Click" />
                    </div>
			        <div style="float:right">
			            Total : <asp:Label ID="TotalRecords" Text="0" runat="server"></asp:Label> Record(s)
			        </div>
			        <br />
                    <br />			    	
                    <asp:GridView ID="GridDebenture" DataKeyNames="DebentureID" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" PageSize="200" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" ForeColor="#FFFFFF" Font-Bold="true" Font-Names="Tahoma" Font-Size="10pt" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkDeleteDocument" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle  Width="50" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="DeleteButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DebentureID") %>' CommandName="Delete" ImageUrl="~/Theme/icon/actions-delete.png" OnClientClick="return confirm('Are you sure you want to delete?');" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="true" ButtonType="Image" ItemStyle-Wrap="false"  
                                ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" SelectImageUrl="~/Theme/icon/actions-edit.png" >
                            <HeaderStyle Width="50px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                            </asp:CommandField>
                            <asp:BoundField DataField="InputDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Input Date" /> 
                            <asp:BoundField DataField="DealNo" ItemStyle-HorizontalAlign="Center" HeaderText="DealNo" /> 
                            <asp:BoundField DataField="SecID" ItemStyle-HorizontalAlign="Center" HeaderText="Security" /> 
                            <asp:BoundField DataField="ThaiBMANo" ItemStyle-HorizontalAlign="Center" HeaderText="ThaiBMA No." /> 
                            <asp:BoundField DataField="Unit" ItemStyle-HorizontalAlign="Center" HeaderText="Unit" /> 
                            <asp:BoundField DataField="Amount" ItemStyle-HorizontalAlign="Center" HeaderText="Amount" /> 
                            <asp:BoundField DataField="Custodian" ItemStyle-HorizontalAlign="Center" HeaderText="Custodian" /> 
                            <asp:BoundField DataField="NameThai" ItemStyle-HorizontalAlign="Left" HeaderText="Name Thai" /> 
                            <asp:BoundField DataField="CustID" ItemStyle-HorizontalAlign="Center" HeaderText="Tax ID/Cust ID" /> 
                            <asp:BoundField DataField="CustType" ItemStyle-HorizontalAlign="Center" HeaderText="Customer Type" /> 
                            <asp:BoundField DataField="CType" ItemStyle-HorizontalAlign="Center" HeaderText="Customer Type (For SEC)" /> 
                            <asp:BoundField DataField="BOTType" ItemStyle-HorizontalAlign="Center" HeaderText="Customer Type (For BOT)" /> 
                            <asp:BoundField DataField="CustomerNumber" ItemStyle-HorizontalAlign="Center" HeaderText="Customer Number" /> 
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
                        
                    <asp:HiddenField ID="ActionBox" runat="server" />
                    <asp:HiddenField ID="DebentureID" runat="server" />
                    <asp:HiddenField ID="DealNo" runat="server" />
                    <asp:HiddenField ID="SecID" runat="server" />
                    <asp:HiddenField ID="Unit" runat="server" />
                    <asp:HiddenField ID="Amount" runat="server" />
                    <asp:HiddenField ID="Custodian" runat="server" />
                    <asp:HiddenField ID="NameThai" runat="server" />
                    <asp:HiddenField ID="CustID" runat="server" />
                    <asp:HiddenField ID="ThaiBMA" runat="server" />
                    <asp:HiddenField ID="CustType" runat="server" />
                    <asp:HiddenField ID="CType" runat="server" />
                    <asp:HiddenField ID="BOTType" runat="server" />
                    <asp:HiddenField ID="CustomerNumber" runat="server" />
                    <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" runat="server" onclick="DialogSubmit_Click" />
                </div>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->		
	
</asp:Content>
