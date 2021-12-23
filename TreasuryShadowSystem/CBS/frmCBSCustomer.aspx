<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frmCBSCustomer.aspx.cs" Inherits="TreasuryShadowSystem.CBS.frmCBSCustomer" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <style>
        input[readonly]
        {
        	border: 1px solid;
            background-color:gray;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function() {
            $(".synccbs").click(function() {
                var cifid = $(".cifbox").val();
                if (cifid == '')
                    alert("กรุณากรอกข้อมูล Customer Number");
                else if ($.isNumeric(cifid) == false)
                    alert("ระบุ Customer Number ไม่ถูกต้อง");
                else if ($(".cifbox").val().length < 5)
                    alert("ระบุ Customer Number ไม่ครบถ้วน");
                else {
                    $.ajax({
                        type: "POST",
                        url: '<%=Config.Website %>/CBS/frmCBSCustomer.aspx/SyncCustomer',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ 'CIFID': cifid }),
                        success: function(data) {
                            var obj = data.d; //alert(obj.Result);
                            if (obj.Result != "ERROR") {
                                $("#dialog").dialog({
                                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                                    width: 1100,
                                    modal: true,
                                    open: function() {
                                        //$('.cbsid').val(obj[0].CBSID);
                                        $('.customernumber').val(obj[0].CUSTOMERNUMBER);
                                        $('.title').val(obj[0].TITLE);
                                        $('.titledesc').val(obj[0].TITLEEN);
                                        $('.formattednameforsorting').val(obj[0].FORMATTEDNAMEFORSORTING);
                                        $('.alternateformattedname').val(obj[0].ALTERNATEFORMATTEDNAME);
                                        $('.addressline1').val(obj[0].ADDRESSLINE1);
                                        $('.addressline2').val(obj[0].ADDRESSLINE2);
                                        $('.addressline3').val(obj[0].ADDRESSLINE3);
                                        $('.addressline4').val(obj[0].ADDRESSLINE4);
                                        $('.addressline5').val(obj[0].ADDRESSLINE5);
                                        $('.addressline6').val(obj[0].ADDRESSLINE6);
                                        $('.addressline7').val(obj[0].ADDRESSLINE7);
                                        $('.birthdate').val(obj[0].BIRTHDATE_STR);
                                        $('.boiflag').val(obj[0].BOIFLAG);
                                        $('.countrycode').val(obj[0].COUNTRYCODE);
                                        $('.countryofcitizenshipincorpo').val(obj[0].COUNTRYOFCITIZENSHIPINCORPO);
                                        $('.customercategory').val(obj[0].CUSTOMERCATEGORY);
                                        $('.customercategorydesc').val(obj[0].CUSTOMERCATEGORYDESC);
                                        $('.customersubtype').val(obj[0].CUSTOMERSUBTYPE);
                                        $('.customersubtypedesc').val(obj[0].CUSTOMERSUBTYPEDESC);
                                        $('.idissuecountrycode').val(obj[0].IDISSUECOUNTRYCODE);
                                        $('.idnumber').val(obj[0].IDNUMBER);
                                        $('.idtypecode').val(obj[0].IDTYPECODE);
                                        $('.idtypecodedesc').val(obj[0].IDTYPECODEDESC);
                                        $('.statecode').val(obj[0].STATECODE);
                                        $('.postalcode').val(obj[0].POSTALCODE);
                                        $('.taxnumber').val(obj[0].TAXNUMBER);
                                        $('.isactive').attr('disabled', 'true');
                                        $('.isinactive').attr('disabled', 'true');
                                        $('.isactive').attr('checked', 'true');
                                    },
                                    close: function() {
                                        $(this).dialog("close");
                                        $('.cbsid').val("");
                                        $('.cno').val("");
                                        $('.customernumber').val("");
                                        $('.title').val("");
                                        $('.formattednameforsorting').val("");
                                        $('.alternateformattedname').val("");
                                        $('.addressline1').val("");
                                        $('.addressline2').val("");
                                        $('.addressline3').val("");
                                        $('.addressline4').val("");
                                        $('.addressline5').val("");
                                        $('.addressline6').val("");
                                        $('.addressline7').val("");
                                        $('.birthdate').val("");
                                        $('.boiflag').val("");
                                        $('.countrycode').val("");
                                        $('.countryofcitizenshipincorpo').val("");
                                        $('.customercategory').val("");
                                        $('.customersubtype').val("");
                                        $('.idissuecountrycode').val("");
                                        $('.idnumber').val("");
                                        $('.idtypecode').val("");
                                        $('.statecode').val("");
                                        $('.postalcode').val("");
                                        $('.taxnumber').val("");
                                        $('.cifbox').val("");
                                    },
                                    "buttons": {
                                        "Submit": function() {
                                            var result = true;
                                            var birthdate = '';
                                            if ($('.birthdate').val() != '')
                                                birthdate = $.datepicker.parseDate('dd/mm/yy', $('.birthdate').val());
                                            var isactive = '1';
                                            if ($('.isactive').attr('checked') == '0')
                                                isactive = '0';
                                            if (result) {
                                                var record = {
                                                    CUSTOMERNUMBER: $('.customernumber').val(),
                                                    TITLE: $('.title').val(),
                                                    CNO: $('.cno').val(),
                                                    FORMATTEDNAMEFORSORTING: $('.formattednameforsorting').val(),
                                                    ALTERNATEFORMATTEDNAME: $('.alternateformattedname').val(),
                                                    ADDRESSLINE1: $('.addressline1').val(),
                                                    ADDRESSLINE2: $('.addressline2').val(),
                                                    ADDRESSLINE3: $('.addressline3').val(),
                                                    ADDRESSLINE4: $('.addressline4').val(),
                                                    ADDRESSLINE5: $('.addressline5').val(),
                                                    ADDRESSLINE6: $('.addressline6').val(),
                                                    ADDRESSLINE7: $('.addressline7').val(),
                                                    BIRTHDATE: birthdate,
                                                    BOIFLAG: $('.boiflag').val(),
                                                    COUNTRYCODE: $('.countrycode').val(),
                                                    COUNTRYOFCITIZENSHIPINCORPO: $('.countryofcitizenshipincorpo').val(),
                                                    CUSTOMERCATEGORY: $('.customercategory').val(),
                                                    CUSTOMERSUBTYPE: $('.customersubtype').val(),
                                                    IDISSUECOUNTRYCODE: $('.idissuecountrycode').val(),
                                                    IDNUMBER: $('.idnumber').val(),
                                                    IDTYPECODE: $('.idtypecode').val(),
                                                    STATECODE: $('.statecode').val(),
                                                    POSTALCODE: $('.postalcode').val(),
                                                    TAXNUMBER: $('.taxnumber').val(),
                                                    ENABLED: isactive
                                                };

                                                var request = $.ajax({
                                                    type: "POST",
                                                    url: '<%=Config.Website %>/CBS/frmCBSCustomer.aspx/AddCustomer',
                                                    contentType: "application/json; charset=utf-8",
                                                    dataType: "json",
                                                    data: JSON.stringify({ 'record': record }),
                                                    success: function(data) {
                                                        if (data.d.Result == "OK") {
                                                            alert("Add Customer Complete");
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
                                                $('.cifbox').val("");
                                            }

                                        },
                                        "Close": function() {
                                            $(this).dialog("close");
                                            $('.cbsid').val("");
                                            $('.cno').val("");
                                            $('.customernumber').val("");
                                            $('.title').val("");
                                            $('.formattednameforsorting').val("");
                                            $('.alternateformattedname').val("");
                                            $('.addressline1').val("");
                                            $('.addressline2').val("");
                                            $('.addressline3').val("");
                                            $('.addressline4').val("");
                                            $('.addressline5').val("");
                                            $('.addressline6').val("");
                                            $('.addressline7').val("");
                                            $('.birthdate').val("");
                                            $('.boiflag').val("");
                                            $('.countrycode').val("");
                                            $('.countryofcitizenshipincorpo').val("");
                                            $('.customercategory').val("");
                                            $('.customersubtype').val("");
                                            $('.idissuecountrycode').val("");
                                            $('.idnumber').val("");
                                            $('.idtypecode').val("");
                                            $('.statecode').val("");
                                            $('.postalcode').val("");
                                            $('.taxnumber').val("");
                                            $('.cifbox').val("");
                                        }
                                    }
                                });
                            } else {
                                alert('ไม่พบข้อมูลรหัสลูกค้าที่ระบุ');
                            }
                        },
                        error: function(xhr, ajaxOptions, thrownError) {
                            var err = JSON.parse(xhr.responseText);
                            alert(err.Message);
                        }
                    });
                }
                return false;
            });
            $(".editcustomer").click(function() {
                var cbsid = $(this).attr('alt');
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1100,
                    modal: true,
                    open: function() {
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/CBS/frmCBSCustomer.aspx/GetCustomer',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ 'CBSID': cbsid }),
                            success: function(data) {
                                var obj = data.d;
                                $('.cbsid').val(obj[0].CBSID);
                                $('.customernumber').val(obj[0].CUSTOMERNUMBER);
                                $('.cno').val(obj[0].CNO);
                                $('.title').val(obj[0].TITLE);
                                $('.titledesc').val(obj[0].TITLEEN);
                                $('.formattednameforsorting').val(obj[0].FORMATTEDNAMEFORSORTING);
                                $('.alternateformattedname').val(obj[0].ALTERNATEFORMATTEDNAME);
                                $('.primaryname01').val(obj[0].PRIMARYNAME01);
                                $('.primaryname02').val(obj[0].ALTERNATEFORMATTEDNAME.substr(0, 40));
                                $('.primaryname03').val(obj[0].PRIMARYNAME03);
                                $('.primaryname04').val(obj[0].PRIMARYNAME04);
                                $('.addressline1').val(obj[0].ADDRESSLINE1);
                                $('.addressline2').val(obj[0].ADDRESSLINE2);
                                $('.addressline3').val(obj[0].ADDRESSLINE3);
                                $('.addressline4').val(obj[0].ADDRESSLINE4);
                                $('.addressline5').val(obj[0].ADDRESSLINE5);
                                $('.addressline6').val(obj[0].ADDRESSLINE6);
                                $('.addressline7').val(obj[0].ADDRESSLINE7);
                                $('.birthdate').val(obj[0].BIRTHDATE_STR);
                                $('.boiflag').val(obj[0].BOIFLAG);
                                $('.countrycode').val(obj[0].COUNTRYCODE);
                                $('.countryofcitizenshipincorpo').val(obj[0].COUNTRYOFCITIZENSHIPINCORPO);
                                $('.customercategory').val(obj[0].CUSTOMERCATEGORY);
                                $('.customercategorydesc').val(obj[0].CUSTOMERCATEGORYDESC);
                                $('.customersubtype').val(obj[0].CUSTOMERSUBTYPE);
                                $('.customersubtypedesc').val(obj[0].CUSTOMERSUBTYPEDESC);
                                $('.idissuecountrycode').val(obj[0].IDISSUECOUNTRYCODE);
                                $('.idnumber').val(obj[0].IDNUMBER);
                                $('.idtypecode').val(obj[0].IDTYPECODE);
                                $('.idtypecodedesc').val(obj[0].IDTYPECODEDESC);
                                $('.statecode').val(obj[0].STATECODE);
                                $('.postalcode').val(obj[0].POSTALCODE);
                                $('.taxnumber').val(obj[0].TAXNUMBER);
                                $('.isactive').removeAttr('disabled');
                                $('.isinactive').removeAttr('disabled');
                                if (obj[0].ENABLED == '1')
                                    $('.isactive').attr('checked', 'true');
                                else
                                    $('.isinactive').attr('checked', 'true');
                            },
                            error: function(xhr, ajaxOptions, thrownError) {
                                var err = JSON.parse(xhr.responseText);
                                alert(err.Message);
                            }
                        });
                    },
                    close: function() {
                        $(this).dialog("close");
                        $('.cbsid').val("");
                        $('.cno').val("");
                        $('.customernumber').val("");
                        $('.title').val("");
                        $('.formattednameforsorting').val("");
                        $('.alternateformattedname').val("");
                        $('.addressline1').val("");
                        $('.addressline2').val("");
                        $('.addressline3').val("");
                        $('.addressline4').val("");
                        $('.addressline5').val("");
                        $('.addressline6').val("");
                        $('.addressline7').val("");
                        $('.birthdate').val("");
                        $('.boiflag').val("");
                        $('.countrycode').val("");
                        $('.countryofcitizenshipincorpo').val("");
                        $('.customercategory').val("");
                        $('.customersubtype').val("");
                        $('.idissuecountrycode').val("");
                        $('.idnumber').val("");
                        $('.idtypecode').val("");
                        $('.statecode').val("");
                        $('.postalcode').val("");
                        $('.taxnumber').val("");
                        $('.cifbox').val("");
                    },
                    "buttons": {
                        "Submit": function() {
                            var result = true;
                            var isactive = 1;  
                            if ($('.isinactive').prop('checked') == true)
                                isactive = 0;
                            if ($('.primaryname01').val() == '') {
                                result = false;
                                alert('กรุณาระบุ CMNE');
                            } else if ($('.primaryname01').val().charAt(0).match("^[a-zA-Z\(\)]+$") == null) {
                                result = false;
                                alert('CMNE ตัวแรกต้องเป็นอักษรเท่านั้น');
                            }
                            else if ($('.primaryname02').val() == '') {
                                result = false;
                                alert('กรุณาระบุ SN');
                            } else if ($('.primaryname04').val() == '') {
                                result = false;
                                alert('กรุณาระบุ SIC');
                            } else if ($('.primaryname01').val().length > 10) {
                                result = false;
                                alert('CMNE มีขนาดเกิน 10 ตัวอักษร');
                            } else if ($('.primaryname02').val().length > 40) {
                                result = false;
                                alert('SN มีขนาดเกิน 40 ตัวอักษร');
                            } else if ($('.primaryname03').val().length > 11) {
                                result = false;
                                alert('BIC Code มีขนาดเกิน 11 ตัวอักษร');
                            } else if ($('.primaryname04').val().length > 10) {
                                result = false;
                                alert('SIC Code มีขนาดเกิน 10 ตัวอักษร');
                            }

                            if (result) {
                                var record = {
                                    CNO: $('.cno').val(),
                                    PRIMARYNAME01: $('.primaryname01').val(),
                                    PRIMARYNAME02: $('.primaryname02').val(),
                                    PRIMARYNAME03: $('.primaryname03').val(),
                                    PRIMARYNAME04: $('.primaryname04').val(),
                                    ENABLED: isactive,
                                    CBSID: cbsid
                                };

                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/CBS/frmCBSCustomer.aspx/UpdateCustomer',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ 'record': record }),
                                    success: function(data) {
                                        if (data.d.Result == "OK") {
                                            alert("Update Customer Complete");
                                            $("#<%=btnSubmit.ClientID %>").click();
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
                            $('.cbsid').val("");
                            $('.cno').val("");
                            $('.customernumber').val("");
                            $('.title').val("");
                            $('.formattednameforsorting').val("");
                            $('.alternateformattedname').val("");
                            $('.addressline1').val("");
                            $('.addressline2').val("");
                            $('.addressline3').val("");
                            $('.addressline4').val("");
                            $('.addressline5').val("");
                            $('.addressline6').val("");
                            $('.addressline7').val("");
                            $('.birthdate').val("");
                            $('.boiflag').val("");
                            $('.countrycode').val("");
                            $('.countryofcitizenshipincorpo').val("");
                            $('.customercategory').val("");
                            $('.customersubtype').val("");
                            $('.idissuecountrycode').val("");
                            $('.idnumber').val("");
                            $('.idtypecode').val("");
                            $('.statecode').val("");
                            $('.postalcode').val("");
                            $('.taxnumber').val("");
                            $('.cifbox').val("");
                        }
                    }
                });
                return false;
            });

        });
         
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <input type="hidden" name="CBSID" id="CBSIDBOX" class="cbsid" runat="server" />
        <table>
            <tr>
                <td>CIFID</td>
                <td>
                    <input type="text" name="CUSTOMERNUMBERBOX" id="CUSTOMERNUMBERBOX" class="customernumber" 
                        runat="server" readonly />&nbsp;
                </td>
                <td>CNO</td>
                <td>
                    <input type="text" name="CNOBOX" id="CNOBOX" class="cno" runat="server" />&nbsp;
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Title</td>
                <td>
                    <input type="text" name="TITLETHBOX" id="TITLETHBOX" class="title" size="5" runat="server" readonly />
                    <input type="text" name="TITLEDESCBOX" id="TITLEDESC" class="titledesc" runat="server" readonly /></td>
                <td>Name (Th)</td>
                <td>
                    <input type="text" name="FORMATTEDNAMEFORSORTINGBOX" id="FORMATTEDNAMEFORSORTINGBOX" class="formattednameforsorting" readonly runat="server" />&nbsp;
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>Name (En)</td>
                <td>
                    <input type="text" name="ALTERNATEFORMATTEDNAMEBOX" id="ALTERNATEFORMATTEDNAMEBOX" class="alternateformattedname" readonly runat="server" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>CMNE<span style="color:Red">*</span></td>
                <td>
                    <input type="text" name="PRIMARYNAME01BOX" id="PRIMARYNAME01BOX" class="primaryname01" runat="server" /></td>
                <td>SN<span style="color:Red">*</span></td>
                <td>
                    <input type="text" name="PRIMARYNAME02BOX" id="PRIMARYNAME02BOX" class="primaryname02" runat="server" /></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>BIC</td>
                <td>
                    <input type="text" name="PRIMARYNAME03BOX" id="PRIMARYNAME03BOX" class="primaryname03" runat="server" /></td>
                <td>SIC<span style="color:Red">*</span></td>
                <td>
                    <input type="text" name="PRIMARYNAME04BOX" id="PRIMARYNAME04BOX" class="primaryname04" value="1000000000" runat="server" /></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Address Line 1</td>
                <td>
                    <input type="text" name="ADDRESSLINE1BOX" id="ADDRESSLINE1BOX" class="addressline1" readonly runat="server" /></td>
                <td>Address Line 2</td>
                <td>
                    <input type="text" name="ADDRESSLINE2BOX" id="ADDRESSLINE2BOX" readonly class="addressline2" runat="server" /></td>
                <td>
                    Address Line 3</td>
                <td>
                    <input type="text" name="ADDRESSLINE3BOX" id="ADDRESSLINE3BOX" readonly class="addressline3" runat="server" /></td>
            </tr>
            <tr>
                <td>Address Line 4</td>
                <td>
                    <input type="text" name="ADDRESSLINE4BOX" id="ADDRESSLINE4BOX" class="addressline4" readonly runat="server" /></td>
                <td>Address Line 5</td>
                <td>
                    <input type="text" name="ADDRESSLINE5BOX" id="ADDRESSLINE5BOX" readonly class="addressline5" runat="server" /></td>
                <td>
                    Address Line 6</td>
                <td>
                    <input type="text" name="ADDRESSLINE6BOX" id="ADDRESSLINE6BOX" readonly class="addressline6" runat="server" /></td>
            </tr>
            <tr>
                <td>
                    Address Line 7</td>
                <td>
                    <input type="text" name="ADDRESSLINE7BOX" id="ADDRESSLINE7BOX" readonly class="addressline7" runat="server" /></td>
            </tr>
            <tr>
                <td>Birth Date</td>
                <td>
                    <input type="text" name="BIRTHDATEBOX" id="BIRTHDATEBOX" class="birthdate" readonly runat="server" /></td>
                <td>BOI Flag</td>
                <td>
                    <input type="text" name="BOIFLAGBOX" id="BOIFLAGBOX" readonly class="boiflag" runat="server" /></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Country Code</td>
                <td>
                    <input type="text" name="COUNTRYCODEBOX" id="COUNTRYCODEBOX" readonly 
                        class="countrycode" runat="server" /></td>
                <td>Country of Citizen</td>
                <td>
                    <input type="text" name="COUNTRYOFCITIZENSHIPINCORPOBOX" id="COUNTRYOFCITIZENSHIPINCORPOBOX" readonly 
                        class="countryofcitizenshipincorpo" runat="server" /></td>
                <td>
                    State Code</td>
                <td>
                    <input type="text" name="STATECODEBOX" id="STATECODEBOX" readonly 
                        class="statecode" runat="server" /></td>
            </tr>
            <tr>
                <td>Customer Category</td>
                <td>
                    <input type="text" name="CUSTOMERCATEGORYBOX" id="CUSTOMERCATEGORYBOX" readonly class="customercategory" size="5" runat="server" />
                    <input type="text" name="CUSTOMERCATEGORYDESCBOX" id="CUSTOMERCATEGORYDESCBOX" readonly class="customercategorydesc" runat="server" /></td>
                <td>Customer SubType</td>
                <td>
                    <input type="text" name="CUSTOMERSUBTYPEBOX" id="CUSTOMERSUBTYPEBOX" readonly class="customersubtype" size="5" runat="server" />
                    <input type="text" name="CUSTOMERSUBTYPEDESCBOX" id="CUSTOMERSUBTYPEDESCBOX" readonly class="customersubtypedesc" runat="server" /></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>Issue by Country</td>
                <td>
                    <input type="text" name="IDISSUECOUNTRYCODEBOX" id="IDISSUECOUNTRYCODEBOX" readonly 
                        class="idissuecountrycode" runat="server" /></td>
                <td>ID Number</td>
                <td>
                    <input type="text" name="IDNUMBERBOX" id="IDNUMBERBOX" readonly 
                        class="idnumber" runat="server" /></td>
                <td>
                    ID Type Code</td>
                <td>
                    <input type="text" name="IDTYPECODEBOX" id="IDTYPECODEBOX" readonly size="5" class="idtypecode" runat="server" />
                    <input type="text" name="IDTYPECODEDESCBOX" id="IDTYPECODEDESCBOX" readonly class="idtypecodedesc" runat="server" /></td>
            </tr>
            <tr>
                <td>Postal Code</td>
                <td>
                    <input type="text" name="POSTALCODEBOX" id="POSTALCODEBOX" readonly 
                        class="postalcode" runat="server" /></td>
                <td>Tax No.</td>
                <td>
                    <input type="text" name="TAXNUMBERBOX" id="TAXNUMBERBOX" readonly 
                        class="taxnumber" runat="server" /></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <input type="radio" name="Status" class="isactive" value="1" disabled /> Active
                    <input type="radio" name="Status" class="isinactive" value="0" disabled /> Inactive</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>  
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Customer Interface (CIF)</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
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
	                            <td class="td-display">Not In</td>
	                            <td class="td-field">
	                                <div style="float:left"><asp:CheckBox ID="OpicsSearchBox" Text="Opics" runat="server" /></div>
	                                <div style="float:left; margin-left: 50px;"><asp:CheckBox ID="TDSSearchBox" Text="DataSet" runat="server" /></div>
	                            </td>
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
			        <div style="float:right"></div>
			        <br /><br />			    	
                    <asp:GridView ID="GridData" CellPadding="3" CellSpacing="3" DataKeyNames="CBSID" 
                    CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkCustomer" Checked="false"  runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CUSTOMERNUMBER" ItemStyle-HorizontalAlign="Center" HeaderText="CIFID" /> 
                            <asp:BoundField DataField="CNO" ItemStyle-HorizontalAlign="Center" HeaderText="CNO" /> 
                            <asp:BoundField DataField="TITLEEN" ItemStyle-HorizontalAlign="Center" HeaderText="TITLE" /> 
                            <asp:BoundField DataField="FORMATTEDNAMEFORSORTING" ItemStyle-HorizontalAlign="Center" HeaderText="NAME" /> 
                            <asp:BoundField DataField="IDTYPECODEDESC" ItemStyle-HorizontalAlign="Center" HeaderText="IDTYPECODE" /> 
                            <asp:BoundField DataField="IDNUMBER" ItemStyle-HorizontalAlign="Center" HeaderText="IDNUMBER" /> 
                            <asp:BoundField DataField="ENABLED" ItemStyle-HorizontalAlign="Center" HeaderText="Active" />
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-edit.png" class="editcustomer" alt="<%# DataBinder.Eval(Container.DataItem, "CBSID") %>" style="cursor:pointer" />
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
                    
                    <div>
                        <div style="float:left">
                            <asp:Button ID="SendOpicsBox" runat="server" Text="Send Opics" 
                                CssClass="round blue button-submit sendOpics" onclick="SendOpicsBox_Click" /> 
			                <asp:Button ID="SendDataSetBox" runat="server" Text="Send DataSet" 
                                CssClass="round blue button-submit sendDataSet" 
                                onclick="SendDataSetBox_Click" /></div>
                        <div style="position:absolute; float:left; margin-left: 500px; width: 400px;">
                            <div style="float:left">
                                CIFID <asp:TextBox ID="CIFBox" CssClass="cifbox" runat="server"></asp:TextBox>
                                <asp:Button ID="SyncCBS" CssClass="round blue button-submit synccbs" 
                                    Text="Sync.CBS" runat="server" /></div>
                        </div>
                    </div>
			        
                    
                </div>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->		
</asp:Content>
