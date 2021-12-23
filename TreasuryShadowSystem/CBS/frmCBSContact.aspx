<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frmCBSContact.aspx.cs" Inherits="TreasuryShadowSystem.CBS.frmCBSContact" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $(".synccbs").click(function() {
                var cifid = $(".cifbox").val();
                if (cifid == '') {
                    alert("กรุณากรอกข้อมูล Customer Number");
                    return false;
                } else if ($.isNumeric(cifid) == false) {
                    alert("ระบุ Customer Number ไม่ถูกต้อง");
                    return false;
                }
                else if ($(".cifbox").val().length < 5) {
                    alert("ระบุ Customer Number ไม่ครบถ้วน");
                    return false;
                }
            });
            $(".viewcontact").click(function() {
                var contactid = $(this).attr('alt');
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1100,
                    modal: true,
                    open: function() {
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/CBS/frmCBSContact.aspx/GetContact',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ 'ContactID': contactid }),
                            success: function(data) {
                                var obj = data.d;
                                $('.customernumber').val(obj[0].CUSTOMERNUMBER);
                                $('.formattednameforsorting').val(obj[0].FORMATTEDNAMEFORSORTING);
                                $('.priority').val(obj[0].PRIORITY);
                                $('.seq').val(obj[0].SEQ);
                                $('.contactcode').val(obj[0].CONTACTCODE);
                                $('.countrycode').val(obj[0].COUNTRYCODE);
                                $('.interdirectcode').val(obj[0].INTERDIRECTCODE);
                                $('.contactdetail').val(obj[0].CONTACTDETAIL);
                                $('.contactname').val(obj[0].CONTACTNAME);
                                $('.insufficientcontact').val(obj[0].INSUFFICIENTCONTACT);
                                $('.areacode').val(obj[0].AREACODE);
                                $('.contactext').val(obj[0].CONTACTEXT);
                                $('.jobtitle').val(obj[0].JOBTITLE);
                                $('.addresslinkflag').val(obj[0].ADDRESSLINKFLAG);
                                $('.accountlinkflag').val(obj[0].ACCOUNTLINKFLAG);
                            },
                            error: function(xhr, ajaxOptions, thrownError) {
                                var err = JSON.parse(xhr.responseText);
                                alert(err.Message);
                            }
                        });
                    },
                    close: function() {
                        $(this).dialog("close");
                        $('.seriescodebox').val("");
                    }
                });
                return false;
            });

            $(".deletecontact").click(function() {
                if (confirm('Are you sure you want to delete?')) {
                    var contactid = $(this).attr('alt');
                    $.ajax({
                        type: "POST",
                        url: '<%=Config.Website %>/CBS/frmCBSContact.aspx/DeleteContact',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ 'ContactID': contactid }),
                        success: function(data) {
                            if (data.d.Result == "OK") {
                                alert("Delete Contact Complete");
                                $("#<%=btnSubmit.ClientID %>").click();
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
    <style type="text/css">
        .style1
        {
            height: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <table>
            <tr>
                <td class="style1">CIFID</td>
                <td class="style1">
                    <input type="text" name="CUSTOMERNUMBERBOX" id="CUSTOMERNUMBERBOX" class="customernumber" 
                        runat="server" readonly="readonly" />&nbsp;
                </td>
                <td class="style1">NAME</td>
                <td class="style1" colspan="3">
                    <input type="text" name="FORMATTEDNAMEFORSORTINGBOX" id="FORMATTEDNAMEFORSORTINGBOX" class="formattednameforsorting" readonly="readonly" runat="server" />&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">SEQ</td>
                <td class="style1">
                    <input type="text" name="SEQBOX" id="SEQBOX" class="seq" readonly="readonly" runat="server" />&nbsp;
                </td>
                <td class="style1">
                    Priority</td>
                <td class="style1">
                    <input type="text" name="PRIORITYBOX" id="PRIORITYBOX" 
                        class="priority" readonly="readonly" runat="server" /></td>
                <td class="style1">&nbsp;</td>
                <td class="style1">&nbsp;</td>
            </tr>
            <tr>
                <td>Contact Code</td>
                <td>
                    <input type="text" name="CONTACTCODEBOX" id="CONTACTCODEBOX" class="contactcode" runat="server" 
                        readonly="readonly" /></td>
                <td>Country Code</td>
                <td>
                    <input type="text" name="COUNTRYCODEBOX" id="COUNTRYCODEBOX" class="countrycode" readonly="readonly" runat="server" />&nbsp;
                </td>
                <td>
                    Internation Code</td>
                <td>
                    <input type="text" name="INTERDIRECTCODEBOX" id="INTERDIRECTCODEBOX" 
                        class="interdirectcode" readonly="readonly" runat="server" /></td>
            </tr>
            <tr>
                <td>Contact Detail</td>
                <td>
                    <input type="text" name="CONTACTDETAILBOX" id="CONTACTDETAILBOX" class="contactdetail"  readonly="readonly"  runat="server" /></td>
                <td>Contact Name</td>
                <td>
                    <input type="text" name="CONTACTNAMEBOX" id="CONTACTNAMEBOX" class="contactname" readonly="readonly" runat="server" /></td>
                <td>
                    Insuff. Contact</td>
                <td>
                    <input type="text" name="INSUFFICIENTCONTACTBOX" id="INSUFFICIENTCONTACTBOX" 
                        class="insufficientcontact" readonly="readonly" runat="server" /></td>
            </tr>
            <tr>
                <td>Area Code</td>
                <td>
                    <input type="text" name="AREACODEBOX" id="AREACODEBOX" readonly="readonly" class="areacode"
                        runat="server" /></td>
                <td>Ext.</td>
                <td>
                    <input type="text" name="CONTACTEXTBOX" id="CONTACTEXTBOX" readonly="readonly" class="contactext" 
                        runat="server" /></td>
                <td>
                    JobTitle</td>
                <td>
                    <input type="text" name="JOBTITLEBOX" id="JOBTITLEBOX" readonly="readonly" class="jobtitle" 
                        runat="server" /></td>
            </tr>
            <tr>
                <td>Address Link</td>
                <td>
                    <input type="text" name="ADDRESSLINKFLAGBOX" id="ADDRESSLINKFLAGBOX" readonly="readonly" class="addresslinkflag" 
                        runat="server" /></td>
                <td>Account Link</td>
                <td>
                    <input type="text" name="ACCOUNTLINKFLAGBOX" id="ACCOUNTLINKFLAGBOX" 
                        class="accountlinkflag" readonly="readonly" runat="server" /></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>  
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>	
    <div class="content-module">			
		<div id="criteria-content" class="content-module-main cf">
	    <div class="content-module-heading cf">		
			<h3 class="fl">Contact Interface (CIF)</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="Div1" class="content-module-main cf">
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0">
	                    <tbody>            	                        	            
	                        <tr>
	                            <td class="td-display">CIFNo/Opics Customer No</td>
	                            <td class="td-field">
	                                <asp:TextBox ID="CIFSearchBox" Runat="server"></asp:TextBox></td>
	                        </tr>          	                        	            
	                        <tr>
	                            <td class="td-display">Name</td>
	                            <td class="td-field">
	                                <asp:TextBox ID="NameSearchBox" Runat="server"></asp:TextBox></td>
	                        </tr>
	                        <tr>
	                            <td colspan="2" style="width:800px; text-align: left;">
	                                <asp:Button ID="btnSubmit" Text="Search" runat="server" 
                                        class="round blue button-submit" />
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
			        <div style="float:right"></div>
			        <br /><br />			    	
                    <asp:GridView ID="GridData" CellPadding="3" CellSpacing="3" DataKeyNames="CONTACTID" 
                    CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" ShowHeader="true" runat="server">
                        <Columns>
                            <asp:BoundField DataField="CUSTOMERNUMBER" ItemStyle-HorizontalAlign="Center" HeaderText="CIFID" /> 
                            <asp:BoundField DataField="FORMATTEDNAMEFORSORTING" ItemStyle-HorizontalAlign="Center" HeaderText="ชื่อ" /> 
                            <asp:BoundField DataField="SEQ" ItemStyle-HorizontalAlign="Center" HeaderText="ลำดับ" /> 
                            <asp:BoundField DataField="CONTACTCODE" ItemStyle-HorizontalAlign="Center" HeaderText="ประเภท" /> 
                            <asp:BoundField DataField="CONTACTDETAIL" ItemStyle-HorizontalAlign="Center" HeaderText="ข้อมูล" /> 
                            <asp:BoundField DataField="CONTACTEXT" ItemStyle-HorizontalAlign="Center" HeaderText="เบอร์ต่อ" />
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/icon-tracking.png" class="viewcontact" alt="<%# DataBinder.Eval(Container.DataItem, "CONTACTID") %>" style="cursor:pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-delete.png" class="deletecontact" alt="<%# DataBinder.Eval(Container.DataItem, "CONTACTID") %>" style="cursor:pointer" />
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
                    
                    <br />
                    
                    <div style="width:100%">
                        <div style="position:absolute; float:left; margin-left: 300px; width: 400px;">
                            <div style="float:left">
                                CIFID<asp:TextBox ID="CIFBox" CssClass="cifbox" runat="server"></asp:TextBox>
                                <asp:Button ID="SyncCBS" CssClass="round blue button-submit synccbs" 
                                    Text="Sync.CBS" runat="server" onclick="SyncCBS_Click" /></div>
                        </div>
                    </div>
                    
                </div>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->		
</asp:Content>
