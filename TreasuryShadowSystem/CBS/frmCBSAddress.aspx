<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frmCBSAddress.aspx.cs" Inherits="TreasuryShadowSystem.CBS.frmCBSAddress" %>
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
           $(".viewaddress").click(function() {
                var addressid = $(this).attr('alt');
                   $("#dialog").dialog({
                       title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                       width: 1100,
                       modal: true,
                       open: function() {
                           $.ajax({
                               type: "POST",
                               url: '<%=Config.Website %>/CBS/frmCBSAddress.aspx/GetAddress',
                               contentType: "application/json; charset=utf-8",
                               dataType: "json",
                               data: JSON.stringify({ 'AddressID': addressid }),
                               success: function(data) {
                               var obj = data.d;
                                   $('.addressid').val(obj[0].ADDRESSID);
                                   $('.seq').val(obj[0].SEQ);
                                   $('.customernumber').val(obj[0].CUSTOMERNUMBER);
                                   $('.addressformat').val(obj[0].ADDRESSFORMAT);
                                   $('.addresstype').val(obj[0].ADDRESSTYPE);
                                   $('.addressline1').val(obj[0].ADDRESSLINE1);
                                   $('.addressline2').val(obj[0].ADDRESSLINE2);
                                   $('.addressline3').val(obj[0].ADDRESSLINE3);
                                   $('.addressline4').val(obj[0].ADDRESSLINE4);
                                   $('.addressline5').val(obj[0].ADDRESSLINE5);
                                   $('.addressline6').val(obj[0].ADDRESSLINE6);
                                   $('.addressline7').val(obj[0].ADDRESSLINE7);
                                   $('.insufficientaddress').val(obj[0].INSUFFICIENTADDRESS);
                                   $('.remarks').val(obj[0].REMARKS);
                                   $('.postalcode').val(obj[0].POSTALCODE);
                                   $('.statecode').val(obj[0].STATECODE);
                                   $('.countrycode').val(obj[0].COUNTRYCODE);
                                   $('.defaultaddress').val(obj[0].DEFAULTADDRESS);
                                   $('.accountlinked').val(obj[0].ACCOUNTLINKED);
                                   $('.contactlinkindicator').val(obj[0].CONTACTLINKINDICATOR);
                                   $('.consolidatestatement').val(obj[0].CONSOLIDATESTATEMENT);
                                   $('.housenumber').val(obj[0].HOUSENUMBER);
                                   $('.moolabel').text(obj[0].MOOLABEL);
                                   $('.moo').val(obj[0].MOO);
                                   $('.floornumberlabel').text(obj[0].FLOORNUMBERLABEL);
                                   $('.floornumber').val(obj[0].FLOORNUMBER);
                                   $('.roomnumberlabel').text(obj[0].ROOMNUMBERLABEL);
                                   $('.roomnumber').val(obj[0].ROOMNUMBER);
                                   $('.building').val(obj[0].BUILDING);
                                   $('.soilabel').text(obj[0].SOILABEL);
                                   $('.soi').val(obj[0].SOI);
                                   $('.roadlabel').text(obj[0].ROADLABEL);
                                   $('.road').val(obj[0].ROAD);
                                   $('.subdistrict').val(obj[0].SUBDISTRICTTH);
                                   $('.district').val(obj[0].DISTRICTTH);
                                   $('.province').val(obj[0].PROVINCETH);
                                   $('.postcode').val(obj[0].POSTCODE);
                                   $('.country').val(obj[0].COUNTRY);
                                   $('.formattednameforsorting').val(obj[0].FORMATTEDNAMEFORSORTING);
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


           $(".deleteaddress").click(function() {
               if (confirm('Are you sure you want to delete?')) {
                   var addressid = $(this).attr('alt');
                   $.ajax({
                   type: "POST",
                   url: '<%=Config.Website %>/CBS/frmCBSAddress.aspx/DeleteAddress',
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       data: JSON.stringify({ 'AddressID': addressid }),
                       success: function(data) {
                           if (data.d.Result == "OK") {
                               alert("Delete Address Complete");
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <table>
            <tr>
                <td>CIFID</td>
                <td>
                    <input type="text" name="CUSTOMERNUMBERBOX" id="CUSTOMERNUMBERBOX" class="customernumber" 
                        runat="server" readonly="readonly" />&nbsp;
                </td>
                <td>SEQ</td>
                <td>
                    <input type="text" name="SEQBOX" id="SEQBOX" class="seq" readonly="readonly" runat="server" />&nbsp;
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Address Format</td>
                <td>
                    <input type="text" name="ADDRESSFORMATBOX" id="ADDRESSFORMATBOX" class="addressformat" runat="server" 
                        readonly="readonly" /></td>
                <td>Address Type</td>
                <td>
                    <input type="text" name="ADDRESSTYPEBOX" id="ADDRESSTYPEBOX" class="addresstype" readonly="readonly" runat="server" />&nbsp;
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Address Line 1</td>
                <td>
                    <input type="text" name="ADDRESSLINE1BOX" id="ADDRESSLINE1BOX" class="addressline1"   runat="server" 
                        readonly="readonly" /></td>
                <td>Address Line 2</td>
                <td>
                    <input type="text" name="ADDRESSLINE2BOX" id="ADDRESSLINE2BOX" class="addressline2" readonly="readonly" runat="server" /></td>
                <td>
                    Address Line 3</td>
                <td>
                    <input type="text" name="ADDRESSLINE3BOX" id="ADDRESSLINE3BOX" 
                        class="addressline3" readonly="readonly" runat="server" /></td>
            </tr>
            <tr>
                <td>Address Line 4</td>
                <td>
                    <input type="text" name="ADDRESSLINE4BOX" id="ADDRESSLINE4BOX" class="addressline4" 
                        runat="server" readonly="readonly" /></td>
                <td>Address Line 5</td>
                <td>
                    <input type="text" name="ADDRESSLINE5BOX" id="ADDRESSLINE5BOX" class="addressline5" readonly="readonly" 
                        runat="server" /></td>
                <td>
                    Address Line 6</td>
                <td>
                    <input type="text" name="ADDRESSLINE6BOX" id="ADDRESSLINE6BOX" class="addressline6" readonly="readonly" 
                        runat="server" /></td>
            </tr>
            <tr>
                <td>Address Line 7</td>
                <td>
                    <input type="text" name="ADDRESSLINE7BOX" id="ADDRESSLINE7BOX" class="addressline7" 
                        runat="server" readonly="readonly" /></td>
                <td>Insufficient Address</td>
                <td>
                    <input type="text" name="INSUFFICIENTADDRESSBOX" id="INSUFFICIENTADDRESSBOX" readonly="readonly" 
                        class="insufficientaddress" runat="server" /></td>
                <td>
                    Remarks</td>
                <td>
                    <input type="text" name="REMARKSBOX" id="REMARKSBOX" readonly="readonly" 
                        class="remarks" runat="server" /></td>
            </tr>
            <tr>
                <td>Postal Code</td>
                <td>
                    <input type="text" name="POSTALCODEBOX" id="POSTALCODEBOX" class="postalcode" readonly="readonly" 
                        runat="server" onclick="return BirthDateBox_onclick()" /></td>
                <td>State Code</td>
                <td>
                    <input type="text" name="STATECODEBOX" id="STATECODEBOX" readonly="readonly" 
                        class="statecode" runat="server" /></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Country Code</td>
                <td>
                    <input type="text" name="COUNTRYCODEBOX" id="COUNTRYCODEBOX" readonly="readonly" 
                        class="countrycode" runat="server" /></td>
                <td>Default Address</td>
                <td>
                    <input type="text" name="DEFAULTADDRESSBOX" id="DEFAULTADDRESSBOX" readonly="readonly" 
                        class="defaultaddress" runat="server" /></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Account Linked</td>
                <td>
                    <input type="text" name="ACCOUNTLINKEDBOX" id="ACCOUNTLINKEDBOX" readonly="readonly" 
                        class="accountlinked" runat="server" /></td>
                <td>Contact Link Indicator</td>
                <td>
                    <input type="text" name="CONTACTLINKINDICATORBOX" id="CONTACTLINKINDICATORBOX" readonly="readonly" 
                        class="contactlinkindicator" runat="server" /></td>
                <td>
                    Consolidate Statement</td>
                <td>
                    <input type="text" name="consolidatestatementbox" id="CONSOLIDATESTATEMENTBOX" readonly="readonly" 
                        class="consolidatestatement" runat="server" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>House No.</td>
                <td>
                    <input type="text" name="HOUSENUMBERBOX" id="HOUSENUMBERBOX" readonly="readonly" 
                        class="housenumber" runat="server" /></td>
                <td><span class="moolabel"></span></td>
                <td>
                    <input type="text" name="MOOBOX" id="MOOBOX" readonly="readonly" 
                        class="moo" runat="server" /></td>
                <td>
                    <span class="floornumberlabel"></span></td>
                <td>
                    <input type="text" name="FLOORNUMBERBOX" id="FLOORNUMBERBOX" readonly="readonly" 
                        class="floornumber" runat="server" /></td>
            </tr>
            <tr>
                <td><span class="roomnumberlabel"></span></td>
                <td>
                    <input type="text" name="ROOMNUMBERBOX" id="ROOMNUMBERBOX" readonly="readonly" 
                        class="roomnumber" runat="server" /></td>
                <td>Building</td>
                <td>
                    <input type="text" name="BUILDINGBOX" id="BUILDINGBOX" readonly="readonly" 
                        class="building" runat="server" /></td>
                <td>
                    <span class="soilabel"></span></td>
                <td>
                    <input type="text" name="SOIBOX" id="SOIBOX" readonly="readonly" 
                        class="soi" runat="server" /></td>
            </tr>
            <tr>
                <td><span class="roadlabel"></span></td>
                <td>
                    <input type="text" name="ROADBOX" id="ROADBOX" readonly="readonly" 
                        class="road" runat="server" /></td>
                <td>SubDistrict</td>
                <td>
                    <input type="text" name="SUBDISTRICTBOX" id="SUBDISTRICTBOX" readonly="readonly" 
                        class="subdistrict" runat="server" /></td>
                <td>
                    District</td>
                <td>
                    <input type="text" name="DISTRICTBOX" id="DISTRICTBOX" readonly="readonly" 
                        class="district" runat="server" /></td>
            </tr>
            <tr>
                <td>Province</td>
                <td>
                    <input type="text" name="PROVINCEBOX" id="PROVINCEBOX" readonly="readonly" 
                        class="province" runat="server" /></td>
                <td>PostCode</td>
                <td>
                    <input type="text" name="POSTCODEBOX" id="POSTCODEBOX" readonly="readonly" 
                        class="postcode" runat="server" /></td>
                <td>
                    Country</td>
                <td>
                    <input type="text" name="COUNTRYBOX" id="COUNTRYBOX" readonly="readonly" 
                        class="country" runat="server" /></td>
            </tr>
        </table>  
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
						
		<div id="criteria-content" class="content-module-main cf">
	    <div class="content-module-heading cf">		
			<h3 class="fl">Address Interface (CIF)</h3>
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
                    <asp:GridView ID="GridData" CellPadding="3" CellSpacing="3" DataKeyNames="ADDRESSID" 
                    CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" ShowHeader="true" runat="server">
                        <Columns>
                            <asp:BoundField DataField="CUSTOMERNUMBER" ItemStyle-HorizontalAlign="Center" HeaderText="CIFID" /> 
                            <asp:BoundField DataField="FORMATTEDNAMEFORSORTING" ItemStyle-HorizontalAlign="Center" HeaderText="ชื่อ" /> 
                            <asp:BoundField DataField="SEQ" ItemStyle-HorizontalAlign="Center" HeaderText="ลำดับ" /> 
                            <asp:BoundField DataField="ADDRESSLINE" ItemStyle-HorizontalAlign="Center" HeaderText="ที่อยู่" /> 
                            <asp:BoundField DataField="SUBDISTRICTTH" ItemStyle-HorizontalAlign="Center" HeaderText="ตำบล" /> 
                            <asp:BoundField DataField="DISTRICTTH" ItemStyle-HorizontalAlign="Center" HeaderText="อำเภอ" /> 
                            <asp:BoundField DataField="PROVINCETH" ItemStyle-HorizontalAlign="Center" HeaderText="จังหวัด" /> 
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/icon-tracking.png" class="viewaddress" alt="<%# DataBinder.Eval(Container.DataItem, "ADDRESSID") %>" style="cursor:pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-delete.png" class="deleteaddress" alt="<%# DataBinder.Eval(Container.DataItem, "ADDRESSID") %>" style="cursor:pointer" />
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
                                    Text="Sync CBS" runat="server" onclick="SyncCBS_Click" /></div>
                        </div>
                    </div>
                    
                </div>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->		
</asp:Content>
