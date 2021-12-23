<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR051BONDALLOT.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR051BONDALLOT" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <style>
        table thead tr td { background-color: Gray; color: White; font-weight: bold; font: 10pt tahoma; text-align: center; }
    </style>
    <script type="text/javascript">
        $(document).ready(function() {

            function submitForm() {
                if ($('.dealno').val() == '') {
                    alert('DealNo is require!!');
                    return false;
                }
                if ($('.faceamount').val() == '')
                {
                    alert('Please click the search button');
                    return false;
                }
                $('#<%=DealNo.ClientID %>').val($('.dealno').val());

                var faceamt = $('input[name^=faceamount]').map(function(idx, elem) {
                    return $(elem).val();
                }).get();
                
                var sumfaceamt = 0;
                $('input[name^=faceamount]').each(function() { 
                        if(Number($(this).val())>0) {
                            sumfaceamt += Number($(this).val());
                        } else {
                            sumfaceamt = 0;
                            return false;
                        }
                }); 
                
                if (sumfaceamt<=0) {
                    alert('Please Save Your Transaction');
                    return false;
                }
                
                //if ((sumfaceamt * 1000000) > $('.totallot').val())
                var remain = $('.totallot').val()-(sumfaceamt * 1000000); 
                if(remain<0)   
                {
                    alert('This is Over FaceAmount!!');
                    return false;
                }


                $('#<%=FaceAmt.ClientID %>').val(faceamt);

                var customer = $('input[name^=custid]').map(function(idx, elem) {
                    return $(elem).val();
                }).get();
                $('#<%=Customer.ClientID %>').val(customer);

                event.preventDefault();
                $('input#<%=DialogSubmit.ClientID %>').click();
            }

            function submitLotForm() {
                if ($('.dealnolot').val() == '') {
                    alert('DealNo is require!!');
                    return false;
                }
                if ($('.faceamtlot').val() == '') {
                    alert('Amount is require!!');
                    return false;
                }
                
                //if ($('.remainlot').val() < ($('.faceamtlot').val()*1000000))
                var faceamt = $('.faceamtlot').val()*1000000;                
                var remain = $('.remainlot').val() - faceamt; 
                if(remain<0)                                                
                {
                    alert('This is Over FaceAmount!!');
                    return false;
                }
                
                $('#<%=DealNo.ClientID %>').val($('.dealnolot').val()); 
                $('#<%=FaceAmt.ClientID %>').val($('.faceamtlot').val()); 
                if ($('.customer :selected').val().trim() != '') {
                    $('#<%=Customer.ClientID %>').val($('.customer :selected').val());
                } else {
                    $('#<%=Customer.ClientID %>').val('');
                }
                
                event.preventDefault();
                $('input#<%=DialogSubmit.ClientID %>').click();
            }

            $(".ic-add").click(function() {
                $(".dealno").val("");
                $(".faceamount").val("");
                $("#tblData tbody tr").remove();
                $('#<%=ActionBox.ClientID %>').val("Add");
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 800,
                    height: 500,
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

            $(".ic-add-lot").click(function() {
                $(".dealno").val("");
                $('#<%=ActionBox.ClientID %>').val("Add");
                $("#dialoglot").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 600,
                    modal: true,
                    "buttons": {
                        "Submit": function() {
                            submitLotForm();
                        },
                        "Close": function() {
                            $(this).dialog("close");
                        }
                    }
                });
                return false;
            });

            function Add() {
                $("#frmData tbody tr").remove();
                var str = "<tr><td width='50'>Attn.</td><td valign='top'><input name='searchcustmain' class='searchcustmain' size='20'  style='float:left;width:120px;' type='text' /><img id='btn-customermain' name='btn-customermain' class='btn-customermain' style='float:left;cursor:pointer; padding: 2px; border: 1px solid black; background-color: Blue;' src='<%=Config.Website %>/Theme/icon/icon-search.png' /></td><td><select id='xcustomer' class='xcustomer'></select></td></tr>";
                var str = str + "<tr>" + "<td valign='top'>Company.</td>" + "<td valign='top' width='150'><input name='assetcomp' class='assetcomp' style='width:120px;' readonly='true' size='20' type='text' /><select id='xassetcomp' style='display:none' class='xassetcomp'></select></td>" + "<td  valign='top'><div>FaceAmt.<input name='amount' class='faceamt' size='5' type='text'/>&nbsp;<a href='#' class='btnSave'><img src='<%=Config.Website %>/Theme/icon/mini_save.png'></a>&nbsp;</div></td>" + "</tr>";
                $("#frmData tbody").append(str);
                
                $(".btnSave").bind("click", Save);
                $(".btnDelete").bind("click", Delete);
            };

            function Save() { 
                if ($('.xcustomer :selected').val()==undefined)
                {
                    alert('Attn. is Required');
                    return false;
                }
                if ($('.faceamt').val().trim()=='')
                {
                    alert('Amount is Required');
                    return false;
                }
                var str = "<tr><td><input type='hidden' name='custid[]' value='" + $('.xcustomer :selected').val() + "' class='custid' /><input type='hidden' name='faceamount[]' value='" + $('.faceamt').val() + "' class='face' /></td><td align='center'>" + $('.faceamt').val() + "</td><td>" + $('.xcustomer :selected').text() + "</td><td align='center'>" + $('.assetcomp').val() + "</td><td align='center'><a href='#' class='btnDelete'><img src='<%= Page.ResolveClientUrl("~/Theme/icon/icon_delete.png") %>'></a></td></tr> ";
                $("#tblData tbody").append(str);
                
                $(".btnDelete").bind("click", Delete);
            };

            function Delete() {
                var par = $(this).parent().parent();
                par.remove();
            };

            $(function() { //Add, Save, Edit and Delete functions code 
                //$(".btnEdit").bind("click", Edit);
                $(".btnDelete").bind("click", Delete);
                $("#btnAdd").bind("click", Add);
            });            
            
            
            //Tuning Performance
            $('#btn-dealnomain').click(function() {
                $.ajax({
                    type: "POST",
                    url: "<%=Config.Website %>/Services/Opics.asmx/DealNoJson",
                    data: "{deal:'" + $('.dealno').val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    minLength: 0,
                    dataType: "json",
                    success: function(data) {
                        $($.parseJSON(data.d)).each(function() {                       
                            $(".faceamount").val(this.value);
                            $(".dealdate").val(this.desc);
                            $(".settdate").val(this.icon);
                            $(".totallot").val(this.total);
                            $('#<%=PayType.ClientID %>').val(this.paytype);  
                        });
                    }
                });
            });
            $('#btn-dealno').click(function() {
                $.ajax({
                    type: "POST",
                    url: "<%=Config.Website %>/Services/Opics.asmx/DealNoJson",
                    data: "{deal:'" + $('.dealnolot').val() + "'}",
                    contentType: "application/json; charset=utf-8",
                    minLength: 0,
                    dataType: "json",
                    success: function(data) {
                        $($.parseJSON(data.d)).each(function() {
                            $(".remainlot").val(this.total);
                            $('#<%=PayType.ClientID %>').val(this.paytype);
                        });
                    }
                });
            });
            
            $('#btn-customer').click(function() {
                $.ajax({
                    type: "POST",
                    url: "<%=Config.Website %>/Services/Customer.asmx/CustomerJson",
                    data: "{cust:'" + $('.searchcust').val() + "'}",
                    minLength: 0,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        var a = "";
                        $('.customer').children('option:not(:first)').remove();
                        $('.customer option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return a = a + '<option value="' + this.value.trim() + '">' + this.desc.trim() + '</option>';
                        });
                        $('.customer').append(a);


                        var b = "";
                        $('.xassetcomp').children('option:not(:first)').remove();
                        $('.xassetcomp option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return b = b + '<option value="' + this.icon.trim() + '">' + this.value.trim() + '</option>';
                        });
                        $('.xassetcomp').append(b);
                        $('.assetcomp').val($('.xassetcomp').val());
                    }
                });
            });
            $('.customer').change(function() {
                $('.xassetcomp').prop('selectedIndex', $(this).prop('selectedIndex'));
                $('.assetcomp').val($('.xassetcomp').val());
            });

        });
        
    </script>
    
    
    <script>

        $(function() {
            $(document).on("click", "#btn-customermain", function(e) {
                $.ajax({
                    type: "POST",
                    url: "<%=Config.Website %>/Services/Customer.asmx/CustomerJson",
                    data: "{cust:'" + $('.searchcustmain').val() + "'}",
                    minLength: 0,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        var a = "";
                        $('.xcustomer').children('option:not(:first)').remove();
                        $('.xcustomer option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return a = a + '<option value="' + this.value.trim() + '">' + this.desc.trim() + '</option>';
                        });
                        $('.xcustomer').append(a);


                        var b = "";
                        $('.xassetcomp').children('option:not(:first)').remove();
                        $('.xassetcomp option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return b = b + '<option value="' + this.icon.trim() + '">' + this.value.trim() + '</option>';
                        });
                        $('.xassetcomp').append(b);
                        $('.assetcomp').val($('.xassetcomp').val());
                    }
                });


                $('.xcustomer').change(function() {
                    $('.xassetcomp').prop('selectedIndex', $(this).prop('selectedIndex'));
                    $('.assetcomp').val($('.xassetcomp').val());
                });
            });
        }); 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <table id="Table1" cellspacing="0" cellspacing="0" border="0"> 
            <tr><td>DealNo</td><td><input type="text" name="DealNoBox" class="dealno" /><img id="btn-dealnomain" name="btn-dealnomain" style="float:left;cursor:pointer; padding: 2px; border: 1px solid black; background-color: Blue;" src="<%=Config.Website %>/Theme/icon/icon-search.png" /> <font color="red">* key down and choose</font></td></tr>
            <tr><td>Face Amount</td><td><input type="text" name="FaceAmountBox" class="faceamount" readonly="true" /></td></tr>
            <tr><td>Trade Date</td><td><input type="text" name="DealDateBox" class="dealdate" readonly="true" /></td></tr>
            <tr><td>Settlement Date</td><td><input type="text" name="SettDateBox" class="settdate" readonly="true" /></td></tr>
            <tr><td>Remain</td><td><input type="text" id="TotalBox"  readonly="true" name="totalbox" class="totallot" /></td></tr>
        </table> 
        <button id="btnAdd" class="round blue">New</button>
        <table id="frmData" cellpadding="0" cellspacing="0" border="0">
	        <tbody>
	        </tbody> 
        </table>
        <div style="overflow: auto;">
        <table id="tblData" cellspacing="0" cellspacing="0" border="0"> 
	        <thead> <tr style="height:25px; background-color: #000000"><td width="30px">#</td><td width="100px">Face Amount (Million)</td><td>Attention</td><td width="100px">Asset Company</td><td width="30">&nbsp;</td></tr> 
	        </thead> 
	        <tbody>
	        </tbody> 
        </table> 
        </div>
        <div id="divFace"></div>
    </div>
    <div id="dialoglot" style="display: none;">
        <input type="hidden" id="OldFaceAmtBox" runat="server" name="oldfaceamt" />
        <table>
            <tr>
                <td>DealNo</td>
                <td>
                    <input type="text" name="dealnolotbox" id="DealNoLotBox" runat="server" class="dealnolot" />
                    <img id="btn-dealno" name="btn-dealno" style="float:left;cursor:pointer; padding: 2px; border: 1px solid black; background-color: Blue;" src="<%=Config.Website %>/Theme/icon/icon-search.png" />
                    <font color="red">* key down and choose</font>
                </td>
            </tr>
            <tr>
                <td>Attn.<input type='hidden' name='custlotid' id="CustomerLotBox" runat="server" class='custlotid' /></td>
                <td>
                    <input type="text" name="custlotbox" id="CustomerNameBox" class="searchcust" runat="server" />
                    <img id="btn-customer" name="btn-customer" style="float:left;cursor:pointer; padding: 2px; border: 1px solid black; background-color: Blue;" src="<%=Config.Website %>/Theme/icon/icon-search.png" /><br />
                </td>
            </tr>
            <tr>
                <td></td>
                <td><select id="xcustomer" class="customer" runat="server"></select> <font style="font: 9pt tahoma; color: Red;">* key down and choose. If no data. Please clear box</font></td>
            </tr>
            <tr>
                <td>Asset Company</td>
                <td>
                    <input type="text" name="assetcompbox" id="AssetCompanyBox" runat="server" readonly="true" class="assetcomp"/>
                    <select id="xassetcomp" style="display:none" class="xassetcomp"></select>
                </td>
            </tr>
            <tr>
                <td>Remain</td>
                <td>
                    <input type="text" id="RemainLotBox" name="remainbox" readonly="true" runat="server" class="remainlot" /></td>
            </tr>
            <tr>
                <td>Face Amount</td>
                <td>
                    <input type="text" name="faceamtlotbox" id="FaceAmtLotBox" runat="server" class="faceamtlot"/> * 1,000,000
                </td>
            </tr>
        </table>   
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">	
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Bond Allotment (Front)</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
			<fieldset>
				<p>
			        <label for="simple-input">Deal No</label>
                    <asp:TextBox ID="DealNoSearchBox" runat="server"></asp:TextBox>&nbsp;
			        <asp:Button ID="SearchButton" Text="Search" runat="server" 
                        class="round blue button-submit" onclick="SearchButton_Click"/>
				</p>
			</fieldset>					
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf ">
			<div class="half-size-column fl" style="overflow:auto; width:100%;height:400px;">	
			    
			    <div style="float:left"><button class="round blue button-submit ic-add">Add Bond Allotment</button>&nbsp;&nbsp;</div>
			    <div style="float:left">&nbsp;&nbsp;<button class="round blue button-submit ic-add-lot">Add By Lot</button></div> 
			    <div style="float:right"></div>
			    <br /><br />			    	
                <asp:GridView ID="GridViewAllotment" DataKeyNames="TransLogID" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransLogID") %>' CommandName="Delete" ImageUrl="~/Theme/icon/actions-delete.png" OnClientClick="return confirm('Are you sure you want to delete?');" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" ItemStyle-Wrap="false"  
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" SelectImageUrl="~/Theme/icon/actions-edit.png" >
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="SenderRef" HeaderStyle-Width="70" HeaderText="DealNo" > 
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FaceAmt" HeaderStyle-Width="100" HeaderText="Face Amount"  >    
                        <HeaderStyle Width="150px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AttnName" HeaderStyle-Width="100" HeaderText="Attn"  > 
                        <HeaderStyle Width="300px" HorizontalAlign="Right"></HeaderStyle>
                        </asp:BoundField>
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
                <asp:HiddenField ID="TransLogID" runat="server" />
                <asp:HiddenField ID="DealNo" runat="server" />
                <asp:HiddenField ID="FaceAmt" runat="server" />
                <asp:HiddenField ID="Customer" runat="server" />
                <asp:HiddenField ID="PayType" runat="server" />
                <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" 
                    runat="server" onclick="DialogSubmit_Click" />
			</div> <!-- end half-size-column -->
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
