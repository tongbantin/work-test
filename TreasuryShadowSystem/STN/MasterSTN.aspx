<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="MasterSTN.aspx.cs"
    Inherits="TreasuryShadowSystem.STN.MasterSTN" %>

<%@ Import Namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>"
        type="text/css" />
    <style>
        .ui-dialog
        {
            width: 500px !important;
        }
        ::-webkit-scrollbar-thumb
        {
            background-color: #DDD !important;
        }
        .PagerStyle
        {
            background-color: #FFF !important;
        }
        .backdrop
        {
            position: absolute;
            padding-left: 450px;
            width: 100%;
            height: 100%;
            background-color: #CCC;
            z-index: 99;
        }
        .show
        {
            display: block;
        }
        .hide
        {
            display: none;
        }
    </style>

    <script type="text/javascript">

        function SubmitUnwind() {
           //alert('FN SubmitUnwind');

            var stnid = $('#<%=hdstnid2.ClientID %>').val();
            var pricereturn = $('.txtpricereturn').val();
            var datereturn = $('.txtdatereturn').val();
            var paydate = $('.txtpaydate').val();

            //alert(stnid);
            //alert(pricereturn);
            //alert(datereturn);
            //alert(paydate);
            

                                                $.ajax({
                                                    type: "POST",
                                                    url: '<%=Config.Website %>/STN/MasterSTN.aspx/UnwindPerson',
                                                    contentType: "application/json; charset=utf-8",
                                                    dataType: "json",
                                                    data: JSON.stringify({ 'STNID': stnid, 'PRICERETURN': pricereturn, 'DATERETURN': datereturn, 'PAYDATE': paydate }),
                                                   success: function(data) {
                                                        if (data.d.Result == "OK") {
                                                          alert("Update complete");
                                                            $("#<%=SearchButton.ClientID %>").click();
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

        function SubmitKO() {
 
            var priceko = "";
            var dateko = $('.txtdateko').val();
            var stocktype = "0";
            var kksymbol = $('#<%=hdkkSymbol.ClientID %>').val();
            var fxrateko = $('.txtfxrateko').val();            
           

            $('.rdostocktype input[type=radio]:checked').each(function() {

                stocktype = $(this).val();
 
            });

            $('.txtpriceko').each(function() {

                if (priceko == "") {

                    priceko = $(this).val();


                } else {

                    priceko += "," + $(this).val();
                }

            }); 
     
            $.ajax({
                type: "POST",
                url: '<%=Config.Website %>/STN/MasterSTN.aspx/UpdateKnockout',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ 'kksymbol': kksymbol, 'stocktype':stocktype,'priceko': priceko, 'dateko': dateko, 'fxrateko': fxrateko }),
                success: function(data) {
                    if (data.d.Result == "OK") {                        
                        alert(data.d.Message);
                        $("#<%=SearchButton.ClientID %>").click();
                    } else {
                        alert(data.d.Message);
                    }
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    var err = JSON.parse(xhr.responseText);
                    alert(err.Message);
                }
            });

            return false; 

        
        }


        function Submitpriceclose() {
        
            //alert('Submitpriceclose');

            var pricelist = "";
            console.log('javascript Submitpriceclose');
          

            $('.txtpriceclose').each(function() {

                if (pricelist == "") {

                    pricelist = $(this).val();


                } else {

                    pricelist += "," + $(this).val();
                }

            });

  
            var days = $('#<%=hdSettledays.ClientID %>').val();
            var kksymbol = $('#<%=hdkkSymbol.ClientID %>').val();
            var fxsettle = $('#<%=hdSettleFx.ClientID %>').val();
            
            console.log(days);
            console.log(kksymbol);
            console.log(fxsettle);

            $.ajax({
                type: "POST",
                url: '<%=Config.Website %>/STN/MasterSTN.aspx/UpdatePriceClose',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ 'kksymbol': kksymbol, 'pricelist': pricelist, 'days': days, 'fxsettle': fxsettle }),
                success: function(data) {
                console.log('result: '+data.d.Result);
                    if (data.d.Result == "OK") {
                        alert(data.d.Message);
                        $("#<%=SearchButton.ClientID %>").click();
                    } else {
                        alert(data.d.Message);
                    }
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    var err = JSON.parse(xhr.responseText);
                    alert(err.Message);
                }
            });

            return false; 

        }


        $(document).ready(function() {

            $('.txtdateko').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            $('.dateunwind').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            function submitForm() {


                if ($('#<%=txtsymbol.ClientID %>').val() == '') {
                    alert('Symbol is require!!');
                    return false;
                }
                if ($('#<%=txtrecord.ClientID %>').val() == '') {
                    alert('Record is require!!');
                    return false;
                }
                if ($('#<%=txtamount.ClientID %>').val() == '') {
                    alert('Amount is require!!');
                    return false;
                }

                if ($('#<%=txtseq.ClientID %>').val() == '') {
                    alert('Sequence is require!!');
                    return false;
                }

                if ($('#<%=txtseqdate.ClientID %>').val() == '') {
                    alert('Sequence Date is require!!');
                    return false;
                }

                $('#<%=hdSymbol.ClientID %>').val($('#<%=txtsymbol.ClientID %>').val());
                $('#<%=hdRecord.ClientID %>').val($('#<%=txtrecord.ClientID %>').val());
                $('#<%=hdAmount.ClientID %>').val($('#<%=txtamount.ClientID %>').val());
                $('#<%=hdProduct.ClientID %>').val($('#<%=productddl.ClientID %>').val());
                $('#<%=hdSeq.ClientID %>').val($('#<%=txtseq.ClientID %>').val());
                $('#<%=hdSeqDate.ClientID %>').val($('#<%=txtseqdate.ClientID %>').val());

                $('input#<%=DialogSubmit.ClientID %>').click();

            }


            $(".ic-add").click(function() {
                $('#<%=ActionBox.ClientID %>').val("Add");
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 750,
                    modal: true,
                    "buttons": {
                        "Submit": function() {
                            submitForm();
                        },
                        "Close": function() {
                            $(this).dialog("close");
                            $('.txtsymbol').val('');
                            $('.txtrecord').val('');
                            $('.txtamount').val('');
                            $('.productddl').val('');
                            $('.txtseq').val('');
                            $('.txtseqdate').val('');
                        }
                    }
                });
                return false;
            });


            $(".btnshowperson").click(function() {
                if (this.tog ^= 1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");

                } else {
                    $(this).closest("tr").next().remove();

                }
            });

            $(document).on("click", ".editperson", function(e) {


                var stnid = $(this).attr('alt');
                alert(stnid);

                $("#dialogperson").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 500,
                    modal: true,
                    open: function() {
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/STN/MasterSTN.aspx/EditPerson',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ 'STNID': stnid }),
                            success: function(data) {
                                debugger;
                                var obj = data.d;

                                $('#ctl00_bodyContent_hdstnid').val(obj[0].STNID);
                                $('.txttitlenameth').val(obj[0].TITLE_NAME_TH);
                                $('.txtfirstnameth').val(obj[0].FIRST_NAME_TH);
                                $('.txtlastnameth').val(obj[0].LAST_NAME_TH);
                                $('.txttitlenameen').val(obj[0].TITLE_NAME_EN);
                                $('.txtfirstnameen').val(obj[0].FIRST_NAME_EN);
                                $('.txtlastnameen').val(obj[0].LAST_NAME_EN);
                                $('.txttaxid').val(obj[0].TAXID);
                                $('.txtcustbankcode').val(obj[0].CUST_BANK_CODE);
                                $('.txtcustbankacct').val(obj[0].CUST_BANK_ACCT);
                                $('.txttel').val(obj[0].TEL);
                                $('.txtemail').val(obj[0].EMAIL);
                                $('.txtfcname').val(obj[0].FC_NAME);
                                $('.txtfcemail').val(obj[0].FC_EMAIL);
                                $('.custtypeddl').val(obj[0].CUST_TYPE);
                                $('.txtpaytype').val(obj[0].PAY_TYPE);

                            },
                            error: function(xhr, ajaxOptions, thrownError) {
                                var err = JSON.parse(xhr.responseText);
                                alert(err.Message);
                            }
                        });
                    },
                    close: function() {
                        $(this).dialog("close");
                        $('#ctl00_bodyContent_hdstnid').val('');
                        $('.txttitlenameth').val('');
                        $('.txtfirstnameth').val('');
                        $('.txtlastnameth').val('');
                        $('.txttitlenameen').val('');
                        $('.txtfirstnameen').val('');
                        $('.txtlastnameen').val('');
                        $('.txttaxid').val('');
                        $('.txtcustbankcode').val('');
                        $('.txtcustbankacct').val('');
                        $('.txttel').val('');
                        $('.txtemail').val('');
                        $('.txtfcname').val('');
                        $('.txtfcemail').val('');
                        $('.custtypeddl').val('');
                        $('.txtpaytype').val('');

                    },
                    "buttons": {
                        "Submit": function() {

                            var result = true;
                            var stnid = $('#ctl00_bodyContent_hdstnid').val();
                            var titlenameth = $('.txttitlenameth').val();
                            var firstnameth = $('.txtfirstnameth').val();
                            var lastnameth = $('.txtlastnameth').val();
                            var titlenameen = $('.txttitlenameen').val();
                            var firstnameen = $('.txtfirstnameen').val();
                            var lastnameen = $('.txtlastnameen').val();
                            var taxid = $('.txttaxid').val();
                            var custbankcode = $('.txtcustbankcode').val();
                            var custbankacct = $('.txtcustbankacct').val();
                            var tel = $('.txttel').val();
                            var email = $('.txtemail').val();
                            var fcname = $('.txtfcname').val();
                            var fcemail = $('.txtfcemail').val();
                            var custtype = $('.custtypeddl').val();
                            var paytype = $('.txtpaytype').val();


                            if (((titlenameth == '') && (firstnameth == '') && (lastnameth == '')) && ((titlenameen == '') && (firstnameen == '') && (lastnameen == ''))) {
                                alert('กรุณากรอกข้อมูลให้ครบ')
                                return false;
                            }

                            if (result) {
                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/STN/MasterSTN.aspx/UpdatePerson',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({
                                        'stnid': stnid,
                                        'titlenameth': titlenameth,
                                        'firstnameth': firstnameth,
                                        'lastnameth': lastnameth,
                                        'titlenameen': titlenameen,
                                        'firstnameen': firstnameen,
                                        'lastnameen': lastnameen,
                                        'taxid': taxid,
                                        'custbankcode': custbankcode,
                                        'custbankacct': custbankacct,
                                        'tel': tel,
                                        'email': email,
                                        'fcname': fcname,
                                        'fcemail': fcemail,
                                        'custtype': custtype,
                                        'paytype': paytype
                                    }),
                                    success: function(data) {
                                        if (data.d.Result != "OK")
                                            alert(data.d.Message);
                                        else {
                                            alert('Update Data Completed');
                                            $("#dialogperson").dialog("close");
                                            $("#<%=SearchButton.ClientID %>").click();
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

                            $('#ctl00_bodyContent_hdstnid').val('');
                            $('.txttitlenameth').val('');
                            $('.txtfirstnameth').val('');
                            $('.txtlastnameth').val('');
                            $('.txttitlenameen').val('');
                            $('.txtfirstnameen').val('');
                            $('.txtlastnameen').val('');
                            $('.txttaxid').val('');
                            $('.txtcustbankcode').val('');
                            $('.txtcustbankacct').val('');
                            $('.txttel').val('');
                            $('.txtemail').val('');
                            $('.txtfcname').val('');
                            $('.txtfcemail').val('');
                            $('.custtypeddl').val('');
                            $('.txtpaytype').val('');

                        }
                    }
                });
                return false;


            });



            $(document).on("click", ".lbUnwind", function(e) {


            //var stnid = $(this).attr('alt');
            var stnid = $(this).attr('title');
                //alert(stnid);

                $("#dialogunwind").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 500,
                    modal: true,
                    open: function() {
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/STN/MasterSTN.aspx/EditUnwind',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ 'STNID': stnid }),
                            success: function(data) {
                                debugger;
                                var obj = data.d;

                                $('#ctl00_bodyContent_hdstnid').val(obj[0].STNID);
                                $('.txtpricereturn').val(obj[0].UNWIND_PRICE);
                                $('.txtdatereturn').val(obj[0].UNWIND_DEALDATE);
                                $('.txtpaydate').val(obj[0].UNWIND_PAYDATE);
                             
                            },
                            error: function(xhr, ajaxOptions, thrownError) {
                                var err = JSON.parse(xhr.responseText);
                                alert(err.Message);
                            }
                        });
                    },
                    close: function() {
                        $(this).dialog("close");
                        $('#ctl00_bodyContent_hdstnid').val('');
                        $('.txtpricereturn').val('');
                        $('.txtdatereturn').val('');
                        $('.txtpaydate').val('');

                    },
                    "buttons": {
                        "Submit": function() {

                            if (confirm('Are you sure you want to set status Unwind?')) {

                                $('#<%=hdstnid2.ClientID %>').val(stnid);
                                $('.btnSubmitUnwind').click();
                            }
                            
                        },
                        "Close": function() {
                            $(this).dialog("close");
                        
                            $('#ctl00_bodyContent_hdstnid').val('');
                            $('.txtpricereturn').val('');
                            $('.txtdatereturn').val('');
                            $('.txtpaydate').val('');

                        }
                    }
                });
                return false;


            });
            
            
            //unwindreport
            $(document).on("click", ".btnUnwindReport", function(e) {

                var stnid = $(this).attr('title');

                $.ajax({
                    type: "POST",
                    url: '<%=Config.Website %>/STN/MasterSTN.aspx/OpenUnwindReport',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ 'STNID': stnid }),
                    success: function(data) {
                        if (data.d.Result == "OK") {
                            $(".iReport").attr("src", data.d.Message);
                        } else {
                            alert(data.d.Message);
                        }
                    },
                    error: function(xhr, ajaxOptions, thrownError) {
                        var err = JSON.parse(xhr.responseText);
                        alert(err.Message);
                    }
                });

                return false;

            });
            
            
            //===================
            //mailunwind
            $(document).on("click", ".mailUnwindReport", function(e) {

                var stnid = $(this).attr('title');

                $.ajax({
                    type: "POST",
                    url: '<%=Config.Website %>/STN/MasterSTN.aspx/MailUnwindReport',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ 'STNID': stnid }),
                    success: function(data) {
                        if (data.d.Result == "OK") {
                           // $(".iReport").attr("src", data.d.Message);
                        } else {
                            alert(data.d.Message);
                        }
                    },
                    error: function(xhr, ajaxOptions, thrownError) {
                        var err = JSON.parse(xhr.responseText);
                        alert(err.Message);
                    }
                });

                return false;

            });

            //==================
            $(document).on("click", ".deleteperson", function(e) {

                var stnid = $(this).attr('alt');
                if (confirm('Are you sure you want to delete?')) {
                    $.ajax({
                        type: "POST",
                        url: '<%=Config.Website %>/STN/MasterSTN.aspx/DeletePerson',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ 'STNID': stnid }),
                        success: function(data) {
                            if (data.d.Result == "OK") {
                                alert("Delete Person Complete");
                                $("#<%=SearchButton.ClientID %>").click();
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
                return false;

            });



        });

        function setLoading() {

            $('#divLoading').removeClass('hide');
            $('#divLoading').addClass('show');

            return true;

        }
        
        function UnwindReprt(param){
             alert(param);
        }

       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <table style="line-height: 2;">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    Symbol :
                </td>
                <td>
                    <asp:TextBox ID="txtsymbol" CssClass="txtsymbol" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    Record :
                </td>
                <td>
                    <asp:TextBox ID="txtrecord" CssClass="txtrecord" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    Amount
                </td>
                <td>
                    <asp:TextBox ID="txtamount" CssClass="txtamount" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    Product
                </td>
                <td>
                    <asp:DropDownList ID="productddl" CssClass="productddl" AppendDataBoundItems="true"
                        runat="server">
                    </asp:DropDownList>
                    <span style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    Sequence :
                </td>
                <td>
                    <asp:TextBox ID="txtseq" CssClass="txtseq" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
             <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    Sequence Date:
                </td>
                <td>
                    <asp:TextBox ID="txtseqdate" CssClass="txtseqdate" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div id="dialogperson" style="display: none;">
        <table style="line-height: 2;">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    TITLE_NAME_TH :
                </td>
                <td>
                    <asp:HiddenField ID="hdstnid" Value="" runat="server" />
                    <asp:TextBox ID="txttitlenameth" class="txttitlenameth" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    FIRST_NAME_TH :
                </td>
                <td>
                    <asp:TextBox ID="txtfirstnameth" class="txtfirstnameth" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    LAST_NAME_TH :
                </td>
                <td>
                    <asp:TextBox ID="txtlastnameth" class="txtlastnameth" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    TITLE_NAME_EN :
                </td>
                <td>
                    <asp:TextBox ID="txttitlenameen" class="txttitlenameen" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    FIRST_NAME_EN :
                </td>
                <td>
                    <asp:TextBox ID="txtfirstnameen" class="txtfirstnameen" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    LAST_NAME_EN :
                </td>
                <td>
                    <asp:TextBox ID="txtlastnameen" class="txtlastnameen" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    TAXID :
                </td>
                <td>
                    <asp:TextBox ID="txttaxid" class="txttaxid" Width="150px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    CUST_BANK_CODE :
                </td>
                <td>
                    <asp:TextBox ID="txtcustbankcode" class="txtcustbankcode" Width="150px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    CUST_BANK_ACCT :
                </td>
                <td>
                    <asp:TextBox ID="txtcustbankacct" class="txtcustbankacct" Width="150px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    TEL :
                </td>
                <td>
                    <asp:TextBox ID="txttel" class="txttel" Width="150px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    EMAIL :
                </td>
                <td>
                    <asp:TextBox ID="txtemail" class="txtemail" Width="150px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    FC_NAME :
                </td>
                <td>
                    <asp:TextBox ID="txtfcname" class="txtfcname" Width="150px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    FC_EMAIL :
                </td>
                <td>
                    <asp:TextBox ID="txtfcemail" class="txtfcemail" Width="150px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    TAX_RATE :
                </td>
                <td>
                    <asp:TextBox ID="custtypeddl" class="custtypeddl" Width="150px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    Payment type:
                </td>
                <td>
                    <asp:TextBox ID="txtpaytype" class="txtpaytype" Width="150px" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialogunwind" style="display: none;">
        <table style="line-height: 2;">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    ราคาขายคืน (%) :
                </td>
                <td>
                    <asp:HiddenField ID="hdkksymbol2" Value="" runat="server" />
                    <asp:TextBox ID="txtpricereturn" class="txtpricereturn" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    วันที่ขายคืน :
                </td>
                <td>
                    <asp:TextBox ID="txtdatereturn" class="txtdatereturn dateunwind" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    วันที่ชำระเงิน :
                </td>
                <td>
                    <asp:TextBox ID="txtpaydate" class="txtpaydate dateunwind" Width="150px" runat="server"></asp:TextBox><span
                        style="color: red">*</span>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialogpriceclose" style="display: none;">
        <table style="line-height: 2;">
            <tr>
                <td colspan="3">
                    <asp:Repeater ID="rptpriceclose" runat="server">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="width:50px;">
                                    ราคาปิด:
                                </td>
                                <td>
                                    ปัจจัยอ้างอิง
                                    <%# Container.ItemIndex+1 %>
                                    (<asp:Label ID="lblunderlying" runat="server" Text='<%#Eval("UNDERLYING") %>'></asp:Label>)
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpriceclose" class="txtpriceclose" Width="150px" Text='<%#Eval("SETTLE_PRICE") %>'
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table>
                    <tr>
                        <td style="width:50px;">&nbsp</td>
                        <td style="width:100px;">จำนวนวัน:</td>
                        <td><asp:TextBox ID="txtsettledays" class="txtsettledays" Width="150px" runat="server"></asp:TextBox></td>
                    </tr>
                    </table>                  
                </td>           
            </tr>           
            <tr id="settlerate" style="display:none" runat="server">
                <td colspan="3">
                    <table>
                    <tr>
                        <td style="width:50px;">&nbsp</td>
                        <td style="width:100px;">อัตราแลกเปลี่ยน :</td>
                        <td><asp:TextBox ID="txtsettlefx" class="txtsettlefx" Width="150px" runat="server"></asp:TextBox></td>
                    </tr>
                    </table>                  
                </td>  
            </tr>
          
        </table>
    </div>
    <div id="dialogpriceko" style="display: none;">
        <table style="line-height: 2;">
            <tr> 
                <td colspan="2">
                    <table>
                    <tr>
                        <td> ประเภทหุ้น:</td> 
                        <td><asp:RadioButtonList ID="rdostocktype" CssClass="rdostocktype" runat="server">
                        <asp:ListItem Text=" หุ้นไทย" Value="0"></asp:ListItem>
                        <asp:ListItem Text=" หุ้นต่างประเทศ" Value="1"></asp:ListItem>
                    </asp:RadioButtonList></td>
                    </tr>
                    </table>                   
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Repeater ID="rptpriceko" runat="server">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td colspan="2">
                                    <strong>ปัจจัยอ้างอิง
                                        <%# Container.ItemIndex+1 %>
                                        (<asp:Label ID="lblunderlying" runat="server" Text='<%#Eval("UNDERLYING") %>'></asp:Label>)</strong>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ราคา knockout:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpriceko" class="txtpriceko" Width="150px" Text='<%#Eval("KO_PRICE") %>'
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>                           
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            
            <tr>      
                <td colspan="2">
                    วันที่ Knockout: &emsp;&emsp;&emsp;&emsp;&emsp;<asp:TextBox ID="txtdateko" class="txtdateko" Width="150px" runat="server"></asp:TextBox>
                </td>
            </tr>
            
             <tr id="korate" style="display:none" runat="server">
                <td>
                     อัตราแลกเปลี่ยน  knockout:&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtfxrateko" class="txtfxrateko" Width="150px" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="divLoading" class="backdrop hide">
        <img src="../Theme/Default/Images/loading_image.gif" alt="Please wait.." />
    </div>
    <div class="content-module">
        <div class="content-module-heading cf">
            <h3 class="fl">
                Master STN</h3>
            <span class="fr expand-collapse-text">Click to collapse and expand</span>
        </div>
        <!-- end content-module-heading -->
        <div id="criteria-content" class="content-module-main cf">
            <div class="half-size-column fl">
                <fieldset>
                    <p>
                        <label for="simple-input">
                            Symbol :
                        </label>
                        <asp:TextBox ID="txtsymbols" runat="server"></asp:TextBox>&nbsp;
                        <asp:Button ID="SearchButton" Text="Search" runat="server" class="round blue button-submit"
                            OnClick="SearchButton_Click" />
                    </p>
                </fieldset>
            </div>
            <!-- end half-size-column -->
        </div>
        <!-- end content-module-main -->
        <div class="content-module-heading-result cf">
            <h3 class="fl">
                Result</h3>
            <span class="fr expand-collapse-text">Click to collapse and expand</span>
        </div>
        <!-- end content-module-heading -->
        <div id="result-content" class="content-module-main cf ">
            <div class="half-size-column fl" style="overflow: auto; width: 100%; height: 800px;">
                <div style="float: left">
                    <button class="round blue button-submit ic-add">
                        Add Master</button></div>
                <div style="float: right">
                </div>
                <br />
                <br />
                <asp:GridView ID="GridViewReport" DataKeyNames="MASSTNID" CssClass="GridViewStyle" 
                    AllowPaging="True" AutoGenerateColumns="False" runat="server" PageSize="10" >
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MASSTNID") %>'
                                    CommandName="Delete" ImageUrl="~/Theme/icon/actions-delete.png" OnClientClick="return confirm('Are you sure you want to delete?');"
                                    runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" ItemStyle-Wrap="false"
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="20" SelectImageUrl="~/Theme/icon/actions-edit.png">
                            <HeaderStyle Width="20px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:CommandField>
                        <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <img class="btnshowperson" src="../Theme/icon/icon-tracking.png" style="cursor: pointer" />
                                <asp:Panel ID="AllotmentPerson" runat="server" Style="display: none">
                                    <asp:GridView ID="GridPerson" Width="100%" CellPadding="0" CellSpacing="0" DataKeyNames="STNID"
                                        CssClass="GridViewStyle" AllowPaging="False" AutoGenerateColumns="False" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <img src="../Theme/icon/actions-delete.png" class="deleteperson" alt="<%# DataBinder.Eval(Container.DataItem, "STNID") %>"
                                                        style="cursor: pointer" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <img src="../Theme/icon/actions-edit.png" class="editperson" alt="<%# DataBinder.Eval(Container.DataItem, "STNID") %>"
                                                        style="cursor: pointer" />
                                                </ItemTemplate>
                                            </asp:TemplateField>                                          
                                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <label class="lbUnwind" title ="<%# DataBinder.Eval(Container.DataItem, "STNID") %>" style="cursor: pointer">unwind</label>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    Unwind</HeaderTemplate>
                                           
                                                <ItemTemplate>                                                   
                                                    <asp:ImageButton ID="btnUnwindReport"  runat="server" ToolTip='<%# DataBinder.Eval(Container.DataItem, "STNID") %>' ImageUrl="~/Theme/icon/mini_save.png" 
                                                    CssClass='<%# (((Decimal)Eval("UNWIND_PRICE") == 0)) ? "btnUnwindReport hide" : "btnUnwindReport show" %>' />   
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CURR_DATE" ItemStyle-HorizontalAlign="Center" HeaderText="CURR_DATE" />
                                            <asp:BoundField DataField="ORDER_NO" ItemStyle-HorizontalAlign="Center" HeaderText="ORDER_NO" />
                                            <asp:BoundField DataField="ACCT_NO" ItemStyle-HorizontalAlign="Center" HeaderText="ACCT_NO" />
                                            <asp:BoundField DataField="SYMBOL" ItemStyle-HorizontalAlign="Center" HeaderText="SYMBOL" />
                                            <asp:BoundField DataField="KKSYMBOL" ItemStyle-HorizontalAlign="Center" HeaderText="KKSYMBOL" />
                                            <asp:BoundField DataField="UNIT" ItemStyle-HorizontalAlign="Center" HeaderText="UNIT" />
                                            <asp:BoundField DataField="AMOUNT" ItemStyle-HorizontalAlign="Center" HeaderText="AMOUNT" />
                                            <%--<asp:BoundField DataField="FULLNAME" ItemStyle-HorizontalAlign="Center" HeaderText="FULLNAME" />--%>
                                            <asp:BoundField DataField="TITLE_NAME_TH" ItemStyle-HorizontalAlign="Center" HeaderText="TITLE NAME" />
                                            <asp:BoundField DataField="FIRST_NAME_TH" ItemStyle-HorizontalAlign="Center" HeaderText="FIRST NAME" />
                                            <asp:BoundField DataField="LAST_NAME_TH" ItemStyle-HorizontalAlign="Center" HeaderText="LAST NAME" />
                                            <asp:BoundField DataField="TITLE_NAME_EN" ItemStyle-HorizontalAlign="Center" HeaderText="TITLE NAME (EN)" />
                                            <asp:BoundField DataField="FIRST_NAME_EN" ItemStyle-HorizontalAlign="Center" HeaderText="FIRST NAME (EN)" />
                                            <asp:BoundField DataField="LAST_NAME_EN" ItemStyle-HorizontalAlign="Center" HeaderText="LAST NAME (EN)" />
                                            <asp:BoundField DataField="TAXID" ItemStyle-HorizontalAlign="Center" HeaderText="TAXID" />
                                            <asp:BoundField DataField="CUST_BANK_CODE" ItemStyle-HorizontalAlign="Center" HeaderText="CUST_BANK_CODE" />
                                            <asp:BoundField DataField="CUST_BANK_ACCT" ItemStyle-HorizontalAlign="Center" HeaderText="CUST_BANK_ACCT" />
                                            <asp:BoundField DataField="TEL" ItemStyle-HorizontalAlign="Center" HeaderText="TEL" />
                                            <asp:BoundField DataField="EMAIL" ItemStyle-HorizontalAlign="Center" HeaderText="EMAIL" />
                                            <asp:BoundField DataField="FC_NAME" ItemStyle-HorizontalAlign="Center" HeaderText="FC_NAME" />
                                            <asp:BoundField DataField="FC_EMAIL" ItemStyle-HorizontalAlign="Center" HeaderText="FC_EMAIL" />
                                            <asp:BoundField DataField="CUST_TYPE2" ItemStyle-HorizontalAlign="Center" HeaderText="TAX_RATE" />
                                            <asp:BoundField DataField="PAY_TYPE" ItemStyle-HorizontalAlign="Center" HeaderText="PAYMENT TYPE" />
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
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnPriceClose" runat="server" Text="Closed Price" CommandName="PriceClose"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KKSYMBOL")+ "," + DataBinder.Eval(Container.DataItem, "STN_TYPE") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnPriceKnockout" runat="server" Text="Knockout Price" CommandName="PriceKnockout"
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KKSYMBOL")+ "," + DataBinder.Eval(Container.DataItem, "STN_TYPE") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="KKSYMBOL" HeaderStyle-Width="70" HeaderText="KKSYMBOL">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Product" HeaderStyle-Width="100" HeaderText="Product">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SUM_RECORD" HeaderStyle-Width="50" HeaderText="Record">
                            <HeaderStyle Width="50px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SUM_AMOUNT" HeaderStyle-Width="50" HeaderText="Amount">
                            <HeaderStyle Width="50px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="STN_SEQ" HeaderStyle-Width="50" HeaderText="Seq">
                            <HeaderStyle Width="50px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="STN_SEQDATE" HeaderStyle-Width="50" HeaderText="Seq Date">
                            <HeaderStyle Width="50px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                View Password</HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnViewPass" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KKSYMBOL")+ "," + DataBinder.Eval(Container.DataItem, "STN_TYPE") %>'
                                    CommandName="ViewPassword" ImageUrl="~/Theme/icon/searchbox_button.png" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                Confirmation/Pricing</HeaderTemplate>
                            <ItemTemplate >  
                                                      
                                <asp:ImageButton ID="btnConfirmPrice" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KKSYMBOL")+ "," + DataBinder.Eval(Container.DataItem, "STN_TYPE") %>'
                                    CommandName="ConfirmPrice" ImageUrl="~/Theme/icon/icon_mail.png" runat="server" />
                                &nbsp;
                                <asp:ImageButton ID="openConfirmPrice" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KKSYMBOL")+ "," + DataBinder.Eval(Container.DataItem, "STN_TYPE") %>'
                                    CommandName="OpenConfirmPrice" ImageUrl="~/Theme/icon/mini_save.png" runat="server"  />
                            
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                            <HeaderTemplate>
                                Settlement</HeaderTemplate>
                            <ItemTemplate >
                            
                                 <div style="display:inline">
                                    <asp:ImageButton ID="btnSettlement" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KKSYMBOL")+ "," + DataBinder.Eval(Container.DataItem, "STN_TYPE") %>'
                                    CommandName="Settlement" ImageUrl="~/Theme/icon/icon_mail.png" runat="server" CssClass='<%# (((Decimal)Eval("SETTLE_PRICE1") == 0) && ((Decimal)Eval("SETTLE_FXRATE") == 0)) ? "hide" : "show" %>' />
                               </div>
                               <div style="display:inline">
                                    <asp:ImageButton ID="OpenSettlement"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KKSYMBOL")+ "," + DataBinder.Eval(Container.DataItem, "STN_TYPE") %>'
                                    CommandName="OpenSettlement" ImageUrl="~/Theme/icon/mini_save.png" runat="server" CssClass='<%# (((Decimal)Eval("SETTLE_PRICE1") == 0) &&((Decimal)Eval("SETTLE_FXRATE") == 0)) ? "hide" : "show" %>' />
                               </div>     

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                Knockout</HeaderTemplate>
                                
                            <ItemTemplate>
                                 <asp:ImageButton ID="btnKO" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KKSYMBOL")+ "," + DataBinder.Eval(Container.DataItem, "STN_TYPE") %>'
                                    CommandName="Knockout" ImageUrl="~/Theme/icon/icon_mail.png" runat="server"
                                    CssClass='<%# ((Decimal)Eval("KO_PRICE1") == 0 || (string)Eval("KO_DATE") == "") ? "hide" : "show" %>' />
       
                                <asp:ImageButton ID="OpenKO" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "KKSYMBOL")+ "," + DataBinder.Eval(Container.DataItem, "STN_TYPE") %>'
                                    CommandName="OpenKnockout" ImageUrl="~/Theme/icon/mini_save.png" runat="server" 
                                    CssClass='<%# ((Decimal)Eval("KO_PRICE1") == 0 || (string)Eval("KO_DATE") == "") ? "hide" : "show" %>' />
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
                <asp:HiddenField ID="ActionBox" runat="server" />
                <asp:HiddenField ID="hdMASSTNID" runat="server" />
                <asp:HiddenField ID="hdSymbol" runat="server" />
                <asp:HiddenField ID="hdRecord" runat="server" />
                <asp:HiddenField ID="hdAmount" runat="server" />
                <asp:HiddenField ID="hdProduct" runat="server" />
                <asp:HiddenField ID="hdSettledays" runat="server" />
                <asp:HiddenField ID="hdSettleFx" runat="server" />
                <asp:HiddenField ID="hdkkSymbol" runat="server" />
                <asp:HiddenField ID="hdstnid2" runat="server" />
                <asp:HiddenField ID="hdSeq" runat="server" />
                <asp:HiddenField ID="hdSeqDate" runat="server" />
                
                <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" runat="server"
                    OnClick="DialogSubmit_Click" />
                <asp:Button ID="btnSubmitpriceclose" class="ui-helper-hidden" Text="Submit" runat="server"
                    OnClientClick="Submitpriceclose()" />
                <asp:Button ID="btnSubmitKO" class="ui-helper-hidden" Text="Submit" runat="server"
                    OnClientClick="SubmitKO()" />
                <asp:Button ID="btnSubmitUnwind" class="btnSubmitUnwind ui-helper-hidden" Text="Submit"
                    runat="server" OnClientClick="SubmitUnwind()" />
            </div>
            <!-- end half-size-column -->
            <div class="half-size-column fl">
                <iframe id="iReport" class="iReport" src="<%# TheUrl %>" runat="server" frameborder="0"
                    marginwidth="1"></iframe>
            </div>
            <div class="half-size-column fl">
                <iframe id="iReport2" class="iReport2" src="<%# TheUrl2 %>" runat="server" frameborder="0"
                    marginwidth="1"></iframe>
            </div>
            <!-- end half-size-column -->
        </div>
        <!-- end content-module-main -->
    </div>
    <!-- end content-module -->
</asp:Content>
