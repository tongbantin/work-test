<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR051GENERATE.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR051GENERATE" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/xls.core.min.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/xlsx.core.min.js") %>"></script>  
    <script type="text/javascript">
              
   
            
function BindTable(jsondata, tableid) {/*Function used to convert the JSON array to Html Table*/  
     var columns = BindTableHeader(jsondata, tableid); /*Gets all the column headings of Excel*/  
     for (var i = 0; i < jsondata.length; i++) {  
         var row$ = $('<tr/>');  
         for (var colIndex = 0; colIndex < columns.length; colIndex++) {  
             var cellValue = jsondata[i][columns[colIndex]];  
             if (cellValue == null)  
                 cellValue = "";  
             row$.append($('<td/>').html(cellValue));  
         }  
         $(tableid).append(row$);  
     }  
 }  
 function BindTableHeader(jsondata, tableid) {/*Function used to get all column names from JSON and bind the html table header*/  
     var columnSet = [];  
     var headerTr$ = $('<tr/>');  
     for (var i = 0; i < jsondata.length; i++) {  
         var rowHash = jsondata[i];  
         for (var key in rowHash) {  
             if (rowHash.hasOwnProperty(key)) {  
                 if ($.inArray(key, columnSet) == -1) {/*Adding each unique column names to a variable array*/  
                     columnSet.push(key);  
                     headerTr$.append($('<th/>').html(key));  
                 }  
             }  
         }  
     }  
     $(tableid).append(headerTr$);  
     return columnSet;  
 }  
    
    
    </script>  
    <script type="text/javascript">
        $(document).ready(function() {

            function submitForm() {
                if ($('.dealnolot').val() == '') {
                    alert('DealNo is require!!');
                    return false;
                }
                if ($('.faceamtlot').val() == '') {
                    alert('Amount is require!!');
                    return false;
                }
                var remain = $('.remainlot').val() - $('.faceamtlot').val();
                //if ($('.remainlot').val() < $('.faceamtlot').val()) {
                if (remain < 0) {
                    alert('This is Over FaceAmount??');
                    return false;
                }

                $('#<%=DealNo.ClientID %>').val($('.dealnolot').val());
                $('#<%=FaceAmt.ClientID %>').val($('.faceamtlot').val());
                if ($('.customer').val().trim() != '') {
                    $('#<%=Customer.ClientID %>').val($('.customer :selected').val());
                } else {
                    $('#<%=Customer.ClientID %>').val('');
                }
                $('#<%=CostAmt.ClientID %>').val($('.costamtlot').val());

                event.preventDefault();
                $('input#<%=DialogSubmit.ClientID %>').click();
            }

      
            $(".ic-add").click(function() {
                $('#<%=ActionBox.ClientID %>').val("Add");
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 600,
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
            
       
            $(".ic-upload").click(function() {
                //$('#<%=ActionBox.ClientID %>').val("Add");
                $("#dialogupload").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 600,
                    modal: true,
                    "buttons": {
                        "Submit": function() {
                        
                           //uploadExcel();
                            
                           //$('#<%=UploadFile.ClientID %>').val($('.uploadfilebox').val());
                           event.preventDefault();
                           $('input#<%=UploadSubmit.ClientID %>').click();
                        },
                        "Close": function() {
                            $(this).dialog("close");
                        }
                    }
                });
                return false;
            });
            

            $(".prinamtlot").change(function() {
                var cleanval = Number($('.prinamtlot').val());
                var intval = Number($('.intamtlot').val());
                var settval = 0;
                settval = cleanval + intval;
                $('.settamtlot').val(settval.toFixed(2));
            });

            $(".settamtlot").change(function() {
                var settval = $('.settamtlot').val();
                var intval = $('.intamtlot').val();
                var cleanval = 0;
                cleanval = settval - intval;
                $('.prinamtlot').val(cleanval.toFixed(2));
            });

            $(".intamtlot").change(function() {
                var cleanval = Number($('.prinamtlot').val());
                var intval = Number($('.intamtlot').val());                
                var settval = 0;
                settval = cleanval + intval;
                $('.settamtlot').val(settval.toFixed(2));
            });

            //Tuning Performance
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
        
        $(function() {/*
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
            
            $(document).on("keydown.autocomplete", ".dealnolot", function(e) {
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
                        $(this).val(ui.item.label);
                        $(this).parent().parent().parent().find(".dealnolot").val(ui.item.label.trim());
                        $(this).parent().parent().parent().find(".remainlot").val(ui.item.total);
                        return false;
                    }
                });
            });*/
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <input type="hidden" id="OldFaceAmtBox" runat="server" name="oldfaceamt" />
        <table>
            <tr>
                <td>DealNo</td>
                <td>
                    <input type="text" name="dealnolotbox" id="DealNoLotBox" runat="server" class="dealnolot" /><img id="btn-dealno" name="btn-dealno" style="float:left;cursor:pointer; padding: 2px; border: 1px solid black; background-color: Blue;" src="<%=Config.Website %>/Theme/icon/icon-search.png" />
                    <font color="red">* key down and choose</font>
                </td>
            </tr>
            <tr>
                <td>Attn.<input type='hidden' name='custlotid' id="CustomerLotBox" runat="server" class='custid' /></td>
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
                    <input type="text" id="RemainLotBox" name="remainbox" runat="server" readonly="true" class="remainlot" /></td>
            </tr>
            <tr>
                <td>Face Amount</td>
                <td>
                    <input type="text" name="faceamtlotbox" id="FaceAmtLotBox" runat="server" class="faceamtlot"/>
                </td>
            </tr>
            <tr>
                <td>Cost Amount</td>
                <td>
                    <input type="text" name="costamtlotbox" id="CostAmtLotBox" runat="server" class="costamtlot"/>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="PrincipalAmtLotLable" runat="server" Visible="false" Text="Clean Amount"></asp:Label></td>
                <td>
                    <input type="text" name="prinamtlotbox" id="PrincipalAmtLotBox" runat="server" visible="false" class="prinamtlot"/>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="InterestAmtLotLable" runat="server" Visible="false" Text="Interest Amount"></asp:Label></td>
                <td>
                    <input type="text" name="intamtlotbox" id="InterestAmtLotBox"   runat="server"  visible="false" class="intamtlot"/>
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="SettlementAmtLotLable" runat="server" Visible="false" Text="Settlement Amount"></asp:Label></td>
                <td>
                    <input type="text" name="settamtlotbox" id="SettlementAmtLotBox" runat="server" visible="false" class="settamtlot"/>
                </td>
            </tr>
            <!-- Add VAT and DealPrice for edit value -->
             <tr>
                <td><asp:Label ID="VatLotLable" runat="server" Visible="false" Text="WHT"></asp:Label></td>
                <td>
                    <input type="text" name="vatlotbox" id="VatLotBox" runat="server" visible="false" class="vatlot"/>
                </td>
            </tr>
             <tr>
                <td><asp:Label ID="DealPriceLotLable" runat="server" Visible="false" Text="Yield Price"></asp:Label></td>
                <td>
                    <input type="text" name="dealpricelotbox" id="DealPriceLotBox" runat="server" visible="false" class="dealpricelot"/>
                </td>
            </tr>          
        </table>  
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>    
    <div class="content-module">	
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Bond Allotment (Back Office)</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
			
			<table>
			    <tr>
			        <td class="td-display">Deal NO.</td>
			        <td class="td-field"> <asp:TextBox ID="DealNoSearchBox" runat="server"></asp:TextBox></td>
			        <td class="td-display"><asp:Button ID="SearchButton" Text="Search" runat="server" 
                            class="round blue button-submit" onclick="SearchButton_Click"/></td>
			    </tr>
			    <tr>
			        <td class="td-display">Upload File</td>
			        <td class="td-field"><asp:FileUpload ID="UploadFile" runat="server" /></td>
			        <td class="td-display"><asp:Button ID="UploadButton" Text="Upload" runat="server" 
                            class="round blue button-submit" onclick="UploadSubmit_Click" /> </td>
			    </tr>
			</table>
			</div> <!-- end half-size-column -->
			<div id="divMsg" style="display:none;">
                            <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                        </div>  
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->		
		<div id="result-content" class="content-module-main cf ">
			<div class="half-size-column fl" style="overflow:auto; width:100%;height:400px;">	
			    <div style="float:left"><button class="round blue button-submit ic-add">Add Lot</button>&nbsp;&nbsp;</div>&nbsp;&nbsp;
			    <!--<div style="float:left">&nbsp;&nbsp;<button class="round blue button-submit ic-upload">Upload Lot</button></div>-->
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
                        <asp:BoundField DataField="SecID" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center" HeaderText="Security" /> 
                        <asp:BoundField DataField="MsgType" HeaderStyle-Width="50" ItemStyle-HorizontalAlign="Center" HeaderText="Message" /> 
                        <asp:BoundField DataField="SenderRef" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="DealNo" /> 
                        <asp:BoundField DataField="SettDate" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="Settlement Date" /> 
                        <asp:BoundField DataField="TradeDate" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="Trade Date" /> 
                        <asp:BoundField DataField="DealPrice" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="DealPrice" /> 
                        <asp:BoundField DataField="ISIN" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="ISIN" /> 
                        <asp:BoundField DataField="FaceAmt" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="Face Amount" /> 
                        <asp:BoundField DataField="PS" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="Buy/Sell" /> 
                        <asp:BoundField DataField="Attn" HeaderStyle-Width="150" ItemStyle-HorizontalAlign="Left" HeaderText="Attn" /> 
                        <asp:BoundField DataField="AgentBIC" HeaderStyle-Width="70"  ItemStyle-HorizontalAlign="Center" HeaderText="BIC" /> 
                        <asp:BoundField DataField="AgentCA" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="C/A " />
                        <asp:BoundField DataField="AgentAcctNo" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Center" HeaderText="Acct No." /> 
                        <asp:BoundField DataField="PrinAmt" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="Clean Amt." />     
                        <asp:BoundField DataField="IntAmt" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="Interest Amt." /> 
                        <asp:BoundField DataField="SettAmt" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="Settlemtn Amt." /> 
                        <asp:BoundField DataField="CostValue" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="Cost Value" /> 
                        <asp:BoundField DataField="Vat" HeaderStyle-Width="70" ItemStyle-HorizontalAlign="Right" HeaderText="WHT" /> 
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
                <asp:HiddenField ID="Customer" runat="server" />
                <asp:HiddenField ID="FaceAmt" runat="server" />
                <asp:HiddenField ID="SettlementAmt" runat="server" />
                <asp:HiddenField ID="PrinAmt" runat="server" />
                <asp:HiddenField ID="CostAmt" runat="server" />
                <asp:HiddenField ID="IntAmt" runat="server" />
                <asp:HiddenField ID="VatAmt" runat="server" />                
                <asp:HiddenField ID="DealPriceAmt" runat="server" />
                <asp:HiddenField ID="PayType" runat="server" />
                <asp:HiddenField ID="MsgType" runat="server" />
                
                
                <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" runat="server" onclick="DialogSubmit_Click" />
                <asp:Button ID="btnSave" runat="server" Text="Gen XML" onclick="btnSave_Click" Visible="false" />
                <asp:Button ID="btnDownload" runat="server" Text="Download XML" Visible="false" onclick="btnDownload_Click" />
                <asp:LinkButton ID="btnGoTo" runat="server" Text="Show Confirmation" Visible="false" onclick="btnGoTo_Click"></asp:LinkButton>
                <!--Add for upload-->
                <!--<asp:Button ID="UploadSubmit" class="ui-helper-hidden" Text="Submit" runat="server" onclick="UploadSubmit_Click" />-->
                
                
			</div> <!-- end half-size-column -->
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
