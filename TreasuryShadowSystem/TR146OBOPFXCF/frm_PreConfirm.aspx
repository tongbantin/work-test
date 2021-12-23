<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_PreConfirm.aspx.cs" Inherits="TreasuryShadowSystem.TR146OBOPFXCF.frm_PreConfirm" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">

        $(document).ready(function() {

        $('#ctl00_bodyContent_txtExecuteDate').datepicker({
            altFormat: 'dd/mm/yy',
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true
        });
        
        $("[id*=chkAllDocument]").click(function() {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[id*=chkDocument]", grid).each(function() {
                if (chkHeader.is(":checked")) {
                    $(this).prop("checked", true);
                } else {
                    $(this).prop("checked", false);
                }
            });
        });

        $("[id*=chkDocument]").click(function() {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkAllDocument]", grid);
            if (!$(this).is(":checked")) {
                chkHeader.prop("checked", false);
            } else {
                if ($("[id*=chkDocument]", grid).length == $("[id*=chkDocument]:checked", grid).length) {
                    chkHeader.prop("checked", true);
                }
            }
        });
            $(".editpreconfirm").click(function() {
                var dealno = $(this).attr('alt');
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 500,
                    modal: true,
                    open: function() {
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/TR146OBOPFXCF/frm_PreConfirm.aspx/GetDescription',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ 'DealNo': dealno }),
                            success: function(data) {
                                var obj = data.d; 
                                if ((obj[0].SWAPDEAL=='0') || (obj[0].SWAPDEAL==''))
                                {  
                                    $('.farpurpose').css('display','none');
                                    $('.nearlabel').css('display','none');
                                } else {  
                                    $('.farpurpose').css('display','block');
                                    $('.nearlabel').css('display','block');
                                }
                                
                                $('.dealnobox').val(obj[0].DEALNO);
                                $('.nearcodebox').val(obj[0].NEARPURPOSE);
                                $('.neardescriptionbox').val(obj[0].NEARPURPOSEDESC);
                                $('.farcodebox').val(obj[0].FARPURPOSE);
                                $('.fardescriptionbox').val(obj[0].FARPURPOSEDESC);
                                $('.tenorbox').val(obj[0].TENORVAL);
                                $('.remarkbox').val(obj[0].REMARK);
                            },
                            error: function(xhr, ajaxOptions, thrownError) {
                                var err = JSON.parse(xhr.responseText);
                                alert(err.Message);
                            }
                        });
                    },
                    close: function() {
                        $(this).dialog("close");
                        $('.dealnobox').val("");
                        $('.nearcodebox').val("");
                        $('.neardescriptionbox').val("");
                        $('.farcodebox').val("");
                        $('.fardescriptionbox').val("");
                        $('.tenorbox').val("");
                        $('.remarkbox').val("");

                    },
                    "buttons": {
                        "Submit": function() {
                            var result = true;
                            var dealno = $('.dealnobox').val();
                            var nearcode = $('.nearcodebox').val();
                            var neardescription = $('.neardescriptionbox').val();
                            var farcode = $('.farcodebox').val();
                            var fardescription = $('.fardescriptionbox').val();
                            var tenor = $('.tenorbox').val();
                            var remark = $('.remarkbox').val();
                            if ((nearcode=='318131') && (neardescription==''))
                            {
                               alert('กรุณากรอก Description')
                               return false;
                            }
                            if ((farcode=='318131') && (fardescription==''))
                            {
                               alert('กรุณากรอก Description')
                               return false;
                            }
                            if (result) {
                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/TR146OBOPFXCF/frm_PreConfirm.aspx/UpdateDescription',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ 'DealNo': dealno, 'NearCode': nearcode, 'NearDescription': neardescription, 'FarCode': farcode, 'FarDescription': fardescription, 'Tenor': tenor, 'Remark': remark }),
                                    success: function(data) {
                                        if (data.d.Result != "OK")
                                            alert(data.d.Message);
                                        else {
                                            alert('Update Data Completed');
                                            $("#dialog").dialog("close");
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
                            $('.dealnobox').val("");
                            $('.descriptionbox').val("");
                            $('.remarkbox').val("");
                        }
                    }
                });
                return false;
            });
            $('.chktenor').click(function() {
                var dealno = $(this).attr('value');
                var checked = $(this).is(':checked') == true ? 'y' : 'n';
                var request = $.ajax({
                    type: "POST",
                    url: '<%=Config.Website %>/TR146OBOPFXCF/frm_PreConfirm.aspx/UpdateTenor',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ 'DealNo': dealno, 'Tenor': checked }),
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
            $('.chkswap').click(function() {
                var dealno = $(this).attr('value');
                var checked = $(this).is(':checked') == true ? 'y' : 'n';
                var request = $.ajax({
                    type: "POST",
                    url: '<%=Config.Website %>/TR146OBOPFXCF/frm_PreConfirm.aspx/UpdateSwapPoint',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ 'DealNo': dealno, 'SwapPoint': checked }),
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
        });
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <input type="text" name="DealNoBox" id="DealNoBox" class="dealnobox" style="display:none" runat="server" />
        <font style="color:Red; font: 9px tahama">**ถ้าไม่ต้องการแก้ไขค่า และให้ระบบใช้ค่าจาก Opics ให้ปล่อยเป็นค่าว่างไว้**</font>
        <table width="500">
            <tr>
                <td width="150">Purpose Code<div class="nearlabel">(Near)</div></td>
                <td width="350">
                    <input type="text" name="NearCodeBox" size="10" id="NearCodeBox" class="nearcodebox" runat="server" /> 
                </td>
            </tr>
            <tr>
                <td width="150" valign="top">Description<div class="nearlabel">(Near)</div></td>
                <td width="350">                    
                    <textarea name="NearDescriptionBox" cols="60" id="NearDescriptionBox" class="neardescriptionbox" runat="server" ></textarea>
                </td>
            </tr>
       </table>
       <table width="500" class="farpurpose">
            <tr>
                <td width="150">Purpose Code<div>(Far)</div></td>
                <td width="350">
                    <input type="text" name="FarCodeBox" id="FarCodeBox" size="10" class="farcodebox" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="150" valign="top">Description<div>(Far)</div></td>
                <td width="350"> 
                    <textarea name="FarDescriptionBox" id="FarDescriptionBox" cols="60" class="fardescriptionbox" runat="server" ></textarea>
                </td>
            </tr>
        </table>
        <table width="500">
            <tr>
                <td width="150">Tenor (Customize)</td>
                <td width="350">
                    <input type="text" name="TenorBox" size="10" id="TenorBox" class="tenorbox" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="150" valign="top">Remark</td>
                <td width="350">
                    <textarea type="text" name="RemarkBox" id="RemarkBox" class="remarkbox" runat="server" ></textarea>
                </td>
            </tr>
        </table>  
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Pre-FX Confirmation</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0">
	                    <tbody>            	                        	            
	                        <tr>
	                            <td>Fund</td>
	                            <td>
	                                <asp:DropDownList ID="FundBox" Style="Width: 200px" runat="server"></asp:DropDownList>
	                            </td>	                            
	                        </tr>
	                        <tr>
	                            <td><label for="simple-input">DealNo</label>
                                </td>
	                            <td>
	                                <asp:TextBox ID="FromDealNoBox" runat="server"></asp:TextBox>
	                            &nbsp;-<asp:TextBox ID="ToDealNoBox" runat="server"></asp:TextBox>
	                            </td>
	                        </tr>              	                        	            
	                        <tr>
	                            <td>Date</td>
	                            <td>
	                                <input type="text" id="txtExecuteDate" size="15" runat="server" class="round input-form-textbox" />
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
		        <div>Send Email : <asp:Label Id="EmailBox" runat="server"></asp:Label></div>
		        <div>
		            <asp:Button ID="btnDownloadSelected" Text="Download by Selected" 
                    runat="server"  ValidationGroup="ExportGroup" 
                    class="round blue button-submit" onclick="btnDownloadSelected_Click" />&nbsp;&nbsp;
		            <asp:Button ID="SendMailSubmit" runat="server" Text="Send Mail" 
                        onclick="SendMailSubmit_Click" /></div>
                <div style="overflow: scroll; height: 500px">
	                        <asp:GridView ID="GridPreConfirm" DataKeyNames="DealNo" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" PageSize="100" runat="server">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllDocument" ForeColor="#FFFFFF" Font-Bold="true" Font-Names="Tahoma" Font-Size="10pt" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDocument" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:ButtonField ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" 
                                    CommandName="Download" ButtonType="Image" ImageUrl="~/Theme/icon/savefile.png" >
                                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                                    <HeaderStyle BackColor="#507CD1" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="SwapDeal" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="DealNo" /> 
                                <asp:BoundField DataField="CMNE" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderText="Short Name" /> 
                                <asp:BoundField DataField="SN" HeaderStyle-Width="400px" ItemStyle-HorizontalAlign="Left" HeaderText="Name" /> 
                                <asp:BoundField DataField="NEARPURPOSE" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Right" HeaderText="Near Purpose" /> 
                                <asp:BoundField DataField="FARPURPOSE" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Right" HeaderText="Far Purpose" /> 
                                <asp:BoundField DataField="TENORVAL" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Right" HeaderText="Tenor" /> 
                                <asp:TemplateField>
                                    <HeaderTemplate>Hide Tenor</HeaderTemplate>
                                    <ItemTemplate>
                                        <input type="checkbox" ID="chkTenor" class="chktenor" value="<%# DataBinder.Eval(Container.DataItem, "DEALNO") %>" <%# Eval("TENOR").ToString()=="y" ? "checked" : "" %> />
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>Hide SwapPoint</HeaderTemplate>
                                    <ItemTemplate>
                                        <input type="checkbox" ID="chkSwap" class="chkswap" value="<%# DataBinder.Eval(Container.DataItem, "DEALNO") %>" <%# Eval("SWAPPOINT").ToString()=="y" ? "checked" : "" %> />
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <img src="../Theme/icon/actions-edit.png" class="editpreconfirm" alt="<%# DataBinder.Eval(Container.DataItem, "DEALNO") %>" style="cursor:pointer" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
