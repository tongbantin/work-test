<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR143PCONFIG.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR143PCONFIG" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {

            $(".ic-add").click(function() {
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1000,
                    modal: true,
                    open: function() {

                    },
                    close: function() {
                        $(this).dialog("close");
                        //clearBoxVal();
                    },
                    "buttons": {
                        "Submit": function() {

                            var result = checknull();

                            if (result) {
                                var record = {
                                    BROKNAME: $('#<%=BrokerBox.ClientID %>').val(),
                                    TYPESTYLE: $('#<%=TypeBox.ClientID %>').val(),
                                    NF: $('#<%=NFBox.ClientID %>').val(),
                                    SELLCODE: $('.sellCodeBox').val(),
                                    BUYCODE: $('.buyCodeBox').val(),
                                    VDATECODE: $('.vdateCodeBox').val(),
                                    TDATECODE: $('.tdateCodeBox').val(),
                                    RATECODE: $('.rateCodeBox').val(),
                                    DATEFORMAT: $('.vdateFormatBox').val(),
                                    NOTE: $('.noteBox').val()
                                };

                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/Form/frm_TR143PCONFIG.aspx/AddConfPrecon',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ 'record': record }),
                                    success: function(data) {
                                        if (data.d.Result == "OK") {
                                            alert("Add Preconfirm Config Complete");
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


            $(".editpconfig").click(function() {
                var pconfigid = $(this).attr('alt');
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1100,
                    modal: true,
                    open: function() {
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/Form/frm_TR143PCONFIG.aspx/GetPConfig',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ 'PCONFIG_ID': pconfigid }),
                            success: function(data) {
                                var obj = data.d;
                                $('#<%=BrokerBox.ClientID %>').val(obj[0].BROKNAME);
                                $('#<%=TypeBox.ClientID %>').val(obj[0].TYPESTYLE);
                                $('#<%=NFBox.ClientID %>').val(obj[0].NF);
                                $('.sellCodeBox').val(obj[0].SELLCODE);
                                $('.buyCodeBox').val(obj[0].BUYCODE);
                                $('.vdateCodeBox').val(obj[0].VDATECODE);
                                $('.tdateCodeBox').val(obj[0].TDATECODE),
                                $('.rateCodeBox').val(obj[0].RATECODE);
                                $('.vdateFormatBox').val(obj[0].DATEFORMAT);
                                $('.noteBox').val(obj[0].NOTE);

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
                                    BROKNAME: $('#<%=BrokerBox.ClientID %>').val(),
                                    TYPESTYLE: $('#<%=TypeBox.ClientID %>').val(),
                                    NF: $('#<%=NFBox.ClientID %>').val(),
                                    SELLCODE: $('.sellCodeBox').val(),
                                    BUYCODE: $('.buyCodeBox').val(),
                                    VDATECODE: $('.vdateCodeBox').val(),
                                    TDATECODE: $('.tdateCodeBox').val(),
                                    RATECODE: $('.rateCodeBox').val(),
                                    DATEFORMAT: $('.vdateFormatBox').val(),
                                    NOTE: $('.noteBox').val(),
                                    PCONFIG_ID: pconfigid
                                };

                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/Form/frm_TR143PCONFIG.aspx/UpdPreconf',
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

            $(".delpconfig").click(function() {
                if (confirm('Are you sure you want to delete?')) {
                    var pconfigid = $(this).attr('alt');

                    $.ajax({
                        type: "POST",
                        url: '<%=Config.Website %>/Form/frm_TR143PCONFIG.aspx/DeletePreconf',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ 'PCONFIG_ID': pconfigid }),
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

        function clearBoxVal() {

             $('#<%=BrokerBox.ClientID %>').val("");
             $('#<%=TypeBox.ClientID %>').val("");
             $('#<%=NFBox.ClientID %>').val("");
             $('.sellCodeBox').val("");
             $('.buyCodeBox').val("");
             $('.vdateCodeBox').val("");
             $('.tdateCodeBox').val("");             
             $('.rateCodeBox').val("");
             $('.vdateFormatBox').val("");
             $('.tdateFormatBox').val("");
             $('.noteBox').val("");
         }

         function checknull() {

             if ($('#<%=BrokerBox.ClientID %>').val() == '') {
                 alert('Broker Name is require!!');
                 return false;
             } else if ($('.sellCodeBox').val() == '') {
                 alert('Sell Code is require!!');
                 return false;
             } else if ($('.buyCodeBox').val() == '') {
                 alert('Buy Code is require!!');
                 return false;
             } else if ($('.vdateCodeBox').val() == '') {
                 alert('Value Date Code is require!!');
                 return false;
             } else if ($('.rateCodeBox').val() == '') {
                 alert('Rate Code is require!!');
                 return false;
             } else if ($('.vdateFormatBox').val() == '') {
                 alert('Value Date format is require!!');
                 return false;
             } else {
                 return true;
             }
         }

         function typebox_changed(ddl) {
             
             if (ddl.value == 'Swap') {
                 document.getElementById('nfbox').style.display = 'inline';          
             } else {
                 document.getElementById('nfbox').style.display = 'none';    
             }
         }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>Broker Name </td>
                <td><asp:DropDownList ID="BrokerBox" runat="server" ></asp:DropDownList></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Type</td>               
                <td>
                    <asp:DropDownList ID="TypeBox" runat="server" Enabled=true  onchange="typebox_changed(this)">
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
                <td>&nbsp;</td>
                <td>Sell Code </td>
                <td><input type="text" name="SellCodeBox" id="SellCodeBox" class="sellCodeBox"  runat="server" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Buy Code </td>
                <td><input type="text" name="BuyCodeBox" id="TeBuyCodeBoxxt1" class="buyCodeBox"  runat="server" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Vdate Code </td>
                <td><input type="text" name="VdateCodeBox" id="VdateCodeBox" class="vdateCodeBox"  runat="server" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Tdate Code </td>
                <td><input type="text" name="TdateCodeBox" id="TdateCodeBox" class="tdateCodeBox"  runat="server" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Rate Code </td>
                <td><input type="text" name="RateCodeBox" id="RateCodeBox" class="rateCodeBox"  runat="server" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Vdate Format </td>
                <td><input type="text" name="VdateFormatBox" id="VdateFormatBox" class="vdateFormatBox"  runat="server" />
                </td>
            </tr>     
            <tr>
                <td>&nbsp;</td>
                <td>Note </td>
                <td><input type="text" name="NoteBox" id="NoteBox" class="noteBox"  runat="server" />
                </td>
            </tr>   
        </table>   
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">	
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Confirmation Document : Config Format</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
		    
		    <div style="float:right">
		          <span>Business Process : </span>
		                            <asp:HyperLink ID="ProcessConf" NavigateUrl="~/Form/frm_TR143PCONFIG.aspx" Text="1.Configuration Format" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		                            <asp:HyperLink ID="AddPconf" NavigateUrl="~/Form/frm_TR143PCONF.aspx" Text="2.Add PreConfirmation" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		                            <asp:HyperLink ID="MatchPconf" NavigateUrl="~/Form/frm_TR143PCONFREC.aspx" Text="3.Match Data" CssClass="round blue button-submit" runat="server"></asp:HyperLink>
		    </div>
		    <div>&nbsp;</div>
			<div style="display: none;">
					<asp:Button ID="SubmitBtn"  Text="Search" runat="server" class="round blue button-submit" onclick="SearchButton_Click" />
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->		
				
		<div id="result-content" class="content-module-main cf ">
			<div class="half-size-column fl" style="overflow:auto; width:100%;height:400px;">	
			    <div style="float:left"><button class="round blue button-submit ic-add">Add New Format</button></div> 
			    <div style="float:right"></div>
			    <br /><br />			    	
                <asp:GridView ID="GridViewPconfig" DataKeyNames="PCONFIG_ID" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                    <Columns>
                         <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-edit.png" class="editpconfig" alt="<%# DataBinder.Eval(Container.DataItem, "PCONFIG_ID") %>" style="cursor:pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-delete.png" class="delpconfig" alt="<%# DataBinder.Eval(Container.DataItem, "PCONFIG_ID") %>" style="cursor:pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>                           
                            <asp:BoundField DataField="BROKNAME" ItemStyle-HorizontalAlign="Center" HeaderText="Broker" /> 
                            <asp:BoundField DataField="TYPESTYLE" ItemStyle-HorizontalAlign="Center" HeaderText="Type" /> 
                            <asp:BoundField DataField="NF" ItemStyle-HorizontalAlign="Center" HeaderText="Near/Far" />                             
                            <asp:BoundField DataField="SELLCODE" ItemStyle-HorizontalAlign="Center" HeaderText="Sell Code" /> 
                            <asp:BoundField DataField="BUYCODE" ItemStyle-HorizontalAlign="Center" HeaderText="Buy Code" /> 
                            <asp:BoundField DataField="VDATECODE" ItemStyle-HorizontalAlign="Center" HeaderText="Vdate Code" /> 
                            <asp:BoundField DataField="TDATECODE" ItemStyle-HorizontalAlign="Center" HeaderText="Tdate Code" /> 
                            <asp:BoundField DataField="RATECODE" ItemStyle-HorizontalAlign="Center" HeaderText="Rate Code" />
                            <asp:BoundField DataField="DATEFORMAT" ItemStyle-HorizontalAlign="Center" HeaderText="Date Format" />
                            <asp:BoundField DataField="NOTE" ItemStyle-HorizontalAlign="Center" HeaderText="Note" />
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
