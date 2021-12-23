<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_BESeries.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_BESeries" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        function _Dialog(content)
        {
             $("#dialogContent").html(content);
             $("#dialogmessage").dialog("open");
        }
    
        $(document).ready(function() {
            $('#ctl00_bodyContent_MatFromDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('#ctl00_bodyContent_MatToDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true

            });

            $('#ctl00_bodyContent_IssueFormDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('#ctl00_bodyContent_IssueToDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });


            $('.issuedatebox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('.matdatebox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('.settledatebox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('.confirmdatebox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('#ctl00_bodyContent_DepositDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('#ctl00_bodyContent_WithdrawDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $('[id$=avalIssueDateBox]').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $('[id$=avalMaturityDateBox]').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $("#dialogmessage").dialog({ 
                draggable: false,
                modal: true,
                autoOpen: false, 
                closeOnEscape: false,
                open: function(event, ui){
                    $(".ui-dialog-titlebar-close", ui.dialog | ui).show();
                }
            });
            
            $('[id$=AvalDepositDateBox]').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $('[id$=AvalWithdrawDateBox]').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $('[id$=AvalConfirmationDate]').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $('[id$=AvalSettlementDateBox]').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $('[id$=AvalSellerConfirmationDate]').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $('[id$=AvalSellerSettlementDateBox]').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            var originalContentSeries = $("#dialog").html();
            var originalContentAllotment = $("#dialogdetail").html();
            

            $(".ic-add").click(function() {
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1100,
                    modal: true,
                    open: function() {
                                                                      
                    },
                    close: function() {
                        $(this).dialog("close");
                        $('.seriescodebox').val("");
                        $('.totalamtbox').val("");
                        $('.issuedatebox').val("");
                        $('.matdatebox').val("");
                        $('.issueridbox').val("");
                        $('.disratebox').val("");
                        $('.dealnobox').val("");

                        var a = "";
                        $('.issuer').children('option:not(:first)').remove();
                        $('.issuer option:first').remove();

                        $('.issueracctbox').val("");
                        $('.issuerbankbox').val("");
                    },
                    "buttons": {
                    "Submit": function() {
                            
                            var result = true;
                            let message = '';
                            if ($('.seriescodebox').val().trim() == '') {
                                message += "* Series Code is require!! <br />";
                                result = false;
                            }
                            if ($('.totalamtbox').val().trim() == '') {
                                message += "* Total Amount is require!! <br />";
                                result = false;
                            }
                            if ($('.issuedatebox').val().trim() == '') {
                                message += "* Issue Date is require!! <br />";
                                result = false;
                            }
                            if ($('.matdatebox').val().trim() == '') {
                                message += "* Mat Date is require!! <br />";
                                result = false;
                            }
                            if ($('.disratebox').val().trim() == '') {
                                message += "* Discount Rate is require!! <br />";
                                result = false;
                            }
                            if ($.trim($('.issuer').val()) == '') {
                                message += "* Issuer is require!! <br />";
                                result = false;
                            }
                            if ($.datepicker.parseDate('dd/mm/yy', $('.issuedatebox').val()) >= $.datepicker.parseDate('dd/mm/yy', $('.matdatebox').val())) {
                                message += "* The Settlement Date must come first the Maturity Date!! <br />"
                                result = false;
                            }
                            if($.trim($('.seriescodebox').val()) != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/duplicateBESeries',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'beseriesno': $('.seriescodebox').val(), 'beseriesid': 0 }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }
                            if($('.issuedatebox').val() != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('.issuedatebox').val()) }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* Issuer Date : " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }
                            if($('.matdatebox').val() != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('.matdatebox').val()) }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* Maturity Date : " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }
                            

                            if (result) {
                                var record = {
                                    BE_SERIES_CODE: $('.seriescodebox').val(),
                                    BE_TOTAL_AMT: $('.totalamtbox').val(),
                                    BE_ISSUE_DATE: $.datepicker.parseDate('dd/mm/yy', $('.issuedatebox').val()),
                                    BE_MAT_DATE: $.datepicker.parseDate('dd/mm/yy', $('.matdatebox').val()),
                                    BE_ISSUER_ID: $('.issuer').val(),
                                    BE_DIS_RATE: $('.disratebox').val(),                                    
                                    BE_SERIES_DEAL_NO: $('.dealnobox').val()
                                };

                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/AddAvalSeries',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ 'record': record }),
                                    success: function(data) {
                                        if (data.d.Result == "OK") {
                                            alert("Add Series Complete");
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
                            }
                            else 
                            {
                                _Dialog(message);
                            }
                        },
                        "Close": function() {
                            $(this).dialog("close");
                            $('.seriescodebox').val("");
                            $('.totalamtbox').val("");
                            $('.issuedatebox').val("");
                            $('.matdatebox').val("");
                            $('.issueridbox').val("");
                            $('.disratebox').val("");
                            $('.dealnobox').val("");

                            var a = "";
                            $('.issuer').children('option:not(:first)').remove();
                            $('.issuer option:first').remove();

                            $('.issueracctbox').val("");
                            $('.issuerbankbox').val("");
                        }
                    }
                });
                return false;
            });
            
            $("[id$=btnAddAval]").click(function(){
                $("#dialogAval").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1100,
                    modal: true,
                    open: function() {
                                                                      
                    },
                    close: function() {
                        $(this).dialog("close");
                        $('[id$=avalBESeriesBox]').val("");
                        $('[id$=avalTotalAmtBox]').val("");
                        $('[id$=avalIssueDateBox]').val("");
                        $('[id$=avalMaturityDateBox]').val("");
                        // $('.issueridbox').val("");
                        // $('[id$=avalDiscountRateBox]').val("");
                        $('[id$=avalDealNoBox]').val("");

                        var a = "";
                        $('[id$=avalissuer]').children('option:not(:first)').remove();
                        $('[id$=avalissuer] option:first').remove();

//                        $('[id$=avalxissuerbank]').val("");
//                        $('[id$=avalxissueracct]').val("");
                        
                        //$('[id$=avalseller]').children('option:not(:first)').remove();
                        //$('[id$=avalseller] option:first').remove();
                    },
                    "buttons": {
                    "Submit": function() {
                    
                            var result = true;
                            let message = '';
                            if ($('[id$=avalBESeriesBox]').val().trim() == '') {
                                message += "* Series Code is require!! <br />";
                                result = false;
                            }
                            if ($('[id$=avalTotalAmtBox]').val().trim() == '') {
                                message += "* Total Amount is require!! <br />";
                                result = false;
                            }
                            if ($('[id$=avalIssueDateBox]').val().trim() == '') {
                                message += "* Issue Date is require!! <br />";
                                result = false;
                            }
                            if ($('[id$=avalMaturityDateBox]').val().trim() == '') {
                                message += "* Mat Date is require!! <br />";
                                result = false;
                            }
//                            if ($('[id$=avalDiscountRateBox]').val().trim() == '') {
//                                message += "* Discount Rate is require!! <br />";
//                                result = false;
//                            }
                            if ($.trim($('[id$=avalissuer]').val()) == '') {
                                message += "* Issuer is require!! <br />";
                                result = false;
                            }
                            //if($.trim($('[id$=avalseller]').val()) == '')
                            //{
                            //    message += "* Seller is require!! <br />";
                            //    result = false;
                            //} 
                            if ($.datepicker.parseDate('dd/mm/yy', $('[id$=avalIssueDateBox]').val()) >= $.datepicker.parseDate('dd/mm/yy', $('[id$=avalMaturityDateBox]').val())) {
                                message += "* The Settlement Date must come first the Maturity Date!! <br />"
                                result = false;
                            }
                            /*
                            if($('[id$=avalBESeriesBox]').val().trim() != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/duplicateBESeries',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'beseriesno': $('[id$=avalBESeriesBox]').val(), 'beseriesid': 0 }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }
                            */
                            if($('[id$=avalIssueDateBox]').val() != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=avalIssueDateBox]').val()) }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* Issuer Date : " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }
                            if($('[id$=avalMaturityDateBox]').val() != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=avalMaturityDateBox]').val()) }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* Maturity Date : " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }

                            if (result) {
                                var record = {
                                    BE_SERIES_CODE: $('[id$=avalBESeriesBox]').val(),
                                    BE_TOTAL_AMT: $('[id$=avalTotalAmtBox]').val(),
                                    BE_ISSUE_DATE: $.datepicker.parseDate('dd/mm/yy', $('[id$=avalIssueDateBox]').val()),
                                    BE_MAT_DATE: $.datepicker.parseDate('dd/mm/yy', $('[id$=avalMaturityDateBox]').val()),
                                    BE_ISSUER_ID: $('[id$=avalissuer]').val(),
                                    // BE_DIS_RATE: $('[id$=avalDiscountRateBox]').val(),                                    
                                    BE_SERIES_DEAL_NO: $('[id$=avalDealNoBox]').val(),
                                    //BE_SELLER_ID: $('[id$=avalseller]').val(),
                                    BE_AVAL_TYPE: '1'
                                };

                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/AddSeries',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ 'record': record }),
                                    success: function(data) {
                                        if (data.d.Result == "OK") {
                                            alert("Add Series Complete");
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
                            }
                            else
                            {
                                _Dialog(message);
                            }
                        },
                        "Close": function() {
                            $(this).dialog("close");
                            $('[id$=avalBESeriesBox]').val("");
                            $('[id$=avalTotalAmtBox]').val("");
                            $('[id$=avalIssueDateBox]').val("");
                            $('[id$=avalMaturityDateBox]').val("");
                            // $('.issueridbox').val("");
                            // $('[id$=avalDiscountRateBox]').val("");
                            $('[id$=avalDealNoBox]').val("");

                            var a = "";
                            $('[id$=avalissuer]').children('option:not(:first)').remove();
                            $('[id$=avalissuer] option:first').remove();
        
//                            $('[id$=avalIssuerBankBox]').val("");
//                            $('[id$=avalIssuerAcctBox]').val("");
                            
                            //$('[id$=avalseller]').children('option:not(:first)').remove();
                            //$('[id$=avalseller] option:first').remove();
                        }
                    }
                });
                return false;
            });

            $(".editseries").click(function() {
                var res = $(this).attr('alt').split('||');
                var beseriesid = res[0]; // $(this).attr('alt');
                
                if(res[1] != "" && res[1] != undefined && res[1] != "0")
                {
                    $("#dialogAval").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1100,
                    modal: true,
                    open: function() {
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/BE/frm_BESeries.aspx/GetSeries',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ 'BESeriesID': beseriesid }),
                            success: function(data) {
                                var obj = data.d;
                                $('[id$=avalBESeriesBox]').val(obj[0].BE_SERIES_CODE);
                                $('[id$=avalTotalAmtBox]').val(obj[0].BE_TOTAL_AMT);
                                $('[id$=avalIssueDateBox]').val(obj[0].BE_ISSUE_DATE_STR);
                                $('[id$=avalMaturityDateBox]').val(obj[0].BE_MAT_DATE_STR);
                                //$('[id$=avalDiscountRateBox]').val(obj[0].BE_DIS_RATE);
                                $('[id$=avalDealNoBox]').val(obj[0].BE_SERIES_DEAL_NO);
                               
                                var a = "";
                                $('[id$=avalissuer]').children('option:not(:first)').remove();
                                $('[id$=avalissuer] option:first').remove();
                                a = '<option value="' + obj[0].CUSTOMER.CustomerID + '">' + obj[0].CUSTOMER.NameEn + '</option>';
                                $('[id$=avalissuer]').append(a);

//                                $('[id$=avalIssuerAcctBox]').val(obj[0].CUSTOMER.TargetAcct);
//                                $('[id$=avalIssuerBankBox]').val(obj[0].CUSTOMER.TargetBank);
                                
                                // a = "";
                                // $('[id$=avalseller]').children('option:not(:first)').remove();
                                // $('[id$=avalseller] option:first').remove();
                                // a = '<option value="' + obj[0].BE_SELLER_ID + '">' + obj[0].BE_SELLER_NAME + '</option>';
                                // $('[id$=avalseller]').append(a);
                            },
                            error: function(xhr, ajaxOptions, thrownError) {
                                var err = JSON.parse(xhr.responseText);
                                alert(err.Message);
                            }
                        });
                    },
                    close: function() {
                        $(this).dialog("close");
                        $('[id$=avalBESeriesBox]').val("");
                        $('[id$=avalTotalAmtBox]').val("");
                        $('[id$=avalIssueDateBox]').val("");
                        $('[id$=avalMaturityDateBox]').val("");
                        // $('[id$=avalDiscountRateBox]').val("");
                        $('[id$=avalDealNoBox]').val("");

                        $('[id$=avalissuer]').children('option:not(:first)').remove();
                        $('[id$=avalissuer] option:first').remove();

//                        $('[id$=avalxissuerbank]').children('option:not(:first)').remove();
//                        $('[id$=avalxissuerbank] option:first').remove();
//                        $('[id$=avalIssuerAcctBox]').val("");

//                        $('[id$=avalxissueracct]').children('option:not(:first)').remove();
//                        $('[id$=avalxissueracct] option:first').remove();
//                        $('[id$=avalIssuerBankBox]').val("");
                        
                        // $('[id$=avalseller]').children('option:not(:first)').remove();
                        // $('[id$=avalseller] option:first').remove();
                    },
                    "buttons": {
                        "Submit": function() {
                            var result = true;
                            let message = '';
                            if ($('[id$=avalBESeriesBox]').val().trim() == '') {
                                message += "* Series Code is require!! <br />";
                                result = false;
                            }
                            if ($('[id$=avalTotalAmtBox]').val().trim() == '') {
                                message += "* Total Amount is require!! <br />";
                                result = false;
                            }
                            if ($('[id$=avalIssueDateBox]').val().trim() == '') {
                                message += "* Issue Date is require!! <br />";
                                result = false;
                            }
                            if ($('[id$=avalMaturityDateBox]').val().trim() == '') {
                                message += "* Mat Date is require!! <br />";
                                result = false;
                            }
//                            if ($('[id$=avalDiscountRateBox]').val().trim() == '') {
//                                message += "* Discount Rate is require!! <br />";
//                                result = false;
//                            }
                            if ($.trim($('[id$=avalissuer]').val()) == '') {
                                message += "* Issuer is require!! <br />";
                                result = false;
                            }
                            //if($.trim($('[id$=avalseller]').val()) == '')
                            //{
                            //    message += "* Seller is require!! <br />";
                            //    result = false;
                            //} 
                            if ($.datepicker.parseDate('dd/mm/yy', $('[id$=avalIssueDateBox]').val()) >= $.datepicker.parseDate('dd/mm/yy', $('[id$=avalMaturityDateBox]').val())) {
                                message += "* The Settlement Date must come first the Maturity Date!! <br />"
                                result = false;
                            }
                            /*
                            if($('[id$=avalBESeriesBox]').val().trim() != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/duplicateBESeries',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'beseriesno': $('[id$=avalBESeriesBox]').val(), 'beseriesid': beseriesid }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }
                            */
                            if($('[id$=avalIssueDateBox]').val() != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=avalIssueDateBox]').val()) }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* Issuer Date : " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }
                            if($('[id$=avalMaturityDateBox]').val() != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=avalMaturityDateBox]').val()) }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* Maturity Date : " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }

                            if (result) {
                                var record = {
                                    BE_SERIES_CODE: $('[id$=avalBESeriesBox]').val(),
                                    BE_TOTAL_AMT: $('[id$=avalTotalAmtBox]').val(),
                                    BE_ISSUE_DATE: $.datepicker.parseDate('dd/mm/yy', $('[id$=avalIssueDateBox]').val()),
                                    BE_MAT_DATE: $.datepicker.parseDate('dd/mm/yy', $('[id$=avalMaturityDateBox]').val()),
                                    BE_ISSUER_ID: $('[id$=avalissuer]').val(),
                                    //BE_DIS_RATE: $('[id$=avalDiscountRateBox]').val(),
                                    BE_SERIES_DEAL_NO: $('[id$=avalDealNoBox]').val(),
                                    // BE_SELLER_ID: $('[id$=avalseller]').val(),                               
                                    BESERIESID: beseriesid
                                };

                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/UpdateSeries',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ 'record': record }),
                                    success: function(data) {
                                        if (data.d.Result == "OK") {
                                            alert("Update Series Complete");
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
                            }
                            else
                            {
                                _Dialog(message);
                            }
                        },
                        "Close": function() {
                            $(this).dialog("close");
                            $('[id$=avalBESeriesBox]').val("");
                            $('[id$=avalTotalAmtBox]').val("");
                            $('[id$=avalIssueDateBox]').val("");
                            $('[id$=avalMaturityDateBox]').val("");
                            // $('[id$=avalDiscountRateBox]').val("");
                            $('[id$=avalDealNoBox]').val("");

                            $('[id$=avalissuer]').children('option:not(:first)').remove();
                            $('[id$=avalissuer] option:first').remove();

//                            $('[id$=avalxissuerbank]').children('option:not(:first)').remove();
//                            $('[id$=avalxissuerbank] option:first').remove();
//                            $('[id$=avalIssuerAcctBox]').val("");

//                            $('[id$=avalxissueracct]').children('option:not(:first)').remove();
//                            $('[id$=avalxissueracct] option:first').remove();
//                            $('[id$=avalIssuerBankBox]').val("");
                            
                            // $('[id$=avalseller]').children('option:not(:first)').remove();
                            // $('[id$=avalseller] option:first').remove();
                        }
                    }
                });
                }
                else
                {
                    $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 1100,
                    modal: true,
                    open: function() {
                        $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/BE/frm_BESeries.aspx/GetSeries',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({ 'BESeriesID': beseriesid }),
                            success: function(data) {
                                var obj = data.d;
                                $('.seriescodebox').val(obj[0].BE_SERIES_CODE);
                                $('.totalamtbox').val(obj[0].BE_TOTAL_AMT);
                                $('.issuedatebox').val(obj[0].BE_ISSUE_DATE_STR);
                                $('.matdatebox').val(obj[0].BE_MAT_DATE_STR);
                                $('.issueridbox').val(obj[0].BE_ISSUER_ID);
                                $('.disratebox').val(obj[0].BE_DIS_RATE);
                                $('.dealnobox').val(obj[0].BE_SERIES_DEAL_NO);
                               
                                var a = "";
                                $('.issuer').children('option:not(:first)').remove();
                                $('.issuer option:first').remove();
                                a = '<option value="' + obj[0].CUSTOMER.CustomerID + '">' + obj[0].CUSTOMER.NameEn + '</option>';
                                $('.issuer').append(a);

                                $('.issueracctbox').val(obj[0].CUSTOMER.TargetAcct);
                                $('.issuerbankbox').val(obj[0].CUSTOMER.TargetBank);
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
                        $('.totalamtbox').val("");
                        $('.issuedatebox').val("");
                        $('.matdatebox').val("");
                        $('.issueridbox').val("");
                        $('.disratebox').val("");
                        $('.dealnobox').val("");

                        $('.issuer').children('option:not(:first)').remove();
                        $('.issuer option:first').remove();

                        $('.xissuerbank').children('option:not(:first)').remove();
                        $('.xissuerbank option:first').remove();
                        $('.issueracctbox').val("");

                        $('.xissueracct').children('option:not(:first)').remove();
                        $('.xissueracct option:first').remove();
                        $('.issuerbankbox').val("");

                    },
                    "buttons": {
                        "Submit": function() {
                            var result = true;
                            let message = '';
                            if ($('.seriescodebox').val().trim() == '') {
                                message += "* Series Code is require!! <br />";
                                result = false;
                            }
                            if ($('.totalamtbox').val().trim() == '') {
                                message += "* Total Amount is require!! <br />";
                                result = false;
                            }
                            if ($('.issuedatebox').val().trim() == '') {
                                message += "* Issue Date is require!! <br />";
                                result = false;
                            }
                            if ($('.matdatebox').val().trim() == '') {
                                message += "* Mat Date is require!! <br />";
                                result = false;
                            }
                            if ($('.disratebox').val().trim() == '') {
                                message += "* Discount Rate is require!! <br />";
                                result = false;
                            }
                            if ($.trim($('.issuer').val()) == '') {
                                message += "* Issuer is require!! <br />";
                                result = false;
                            }
                            if ($.datepicker.parseDate('dd/mm/yy', $('.issuedatebox').val()) >= $.datepicker.parseDate('dd/mm/yy', $('.matdatebox').val())) {
                                message += "* The Settlement Date must come first the Maturity Date!! <br />"
                                result = false;
                            }
                            if($.trim($('.seriescodebox').val()) != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/duplicateBESeries',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'beseriesno': $('.seriescodebox').val(), 'beseriesid': beseriesid }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }
                            if($('.issuedatebox').val() != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('.issuedatebox').val()) }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* Issuer Date : " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }
                            if($('.matdatebox').val() != '')
                            {
                                $.ajax({
                                    type: 'POST',
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    async: false,
                                    data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('.matdatebox').val()) }),
                                    success: function(data){
                                        if(!data.d.Success)
                                        {
                                            message += "* Maturity Date : " + data.d.Message + "<br />";
                                            result = false;
                                        }
                                    }
                                });
                            }

                            if (result) {
                                var record = {
                                    BE_SERIES_CODE: $('.seriescodebox').val(),
                                    BE_TOTAL_AMT: $('.totalamtbox').val(),
                                    BE_ISSUE_DATE: $.datepicker.parseDate('dd/mm/yy', $('.issuedatebox').val()),
                                    BE_MAT_DATE: $.datepicker.parseDate('dd/mm/yy', $('.matdatebox').val()),
                                    BE_ISSUER_ID: $('.issuer').val(),
                                    BE_DIS_RATE: $('.disratebox').val(),
                                    BE_SERIES_DEAL_NO: $('.dealnobox').val(),                                   
                                    BESERIESID: beseriesid
                                };

                                var request = $.ajax({
                                    type: "POST",
                                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/UpdateSeries',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: JSON.stringify({ 'record': record }),
                                    success: function(data) {
                                        if (data.d.Result == "OK") {
                                            alert("Update Series Complete");
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
                            }
                            else
                            {
                                _Dialog(message);
                            }
                        },
                        "Close": function() {
                            $(this).dialog("close");
                            $('.seriescodebox').val("");
                            $('.totalamtbox').val("");
                            $('.issuedatebox').val("");
                            $('.matdatebox').val("");
                            $('.issueridbox').val("");
                            $('.disratebox').val("");
                            $('.dealnobox').val("");

                            $('.issuer').children('option:not(:first)').remove();
                            $('.issuer option:first').remove();

                            $('.xissuerbank').children('option:not(:first)').remove();
                            $('.xissuerbank option:first').remove();
                            $('.issueracctbox').val("");

                            $('.xissueracct').children('option:not(:first)').remove();
                            $('.xissueracct option:first').remove();
                            $('.issuerbankbox').val("");

                        }
                    }
                });
                }
                return false;
            });

            $(".deleteseries").click(function() {
                if (confirm('Are you sure you want to delete?')) {
                    var beseriesid = $(this).attr('alt');
                    $.ajax({
                        type: "POST",
                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/DeleteSeries',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ 'BESeriesID': beseriesid }),
                        success: function(data) {
                            if (data.d.Result == "OK") {
                                alert("Delete Series Complete");
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

            $(".ic-adddetail").click(function() {
                let seriesid = $(this).val();
                let res = $(this).attr('alt').split('||');
                let matdate = res[0];
                let avalflag = res[1];
                
                if(avalflag != "", avalflag != undefined && avalflag != "0")
                {
                    // display aval allotment dialog
                    $('#dialogdetailAval').dialog({
                        title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                        width: 1100,
                        modal: true,
                        open: function() {
                            $("input[name=AvalOwnerRadio][value='KK']").prop('checked', true);
                            $("[id$=Avaldocidcardbox]").prop('checked', false);
                            $("[id$=Avaldocbookbankbox]").prop('checked', false);
                            $("[id$=Avaldocotherbox]").prop('checked', false);
                        },
                        close: function(){
                            $('[id$=AvalSKSNoBox]').val("");
                            $('[id$=AvalPatraRegNoBox]').val("");
                            $('[id$=AvalClientNameBox]').val("");
                            $('[id$=AvalDepositDateBox]').val("");
                            $('[id$=AvalWithdrawDateBox]').val("");
                            $('[id$=Avaldocidcardbox]').attr('checked', 'false');
                            $('[id$=Avaldocbookbankbox]').attr('checked', 'false');
                            $('[id$=Avaldocotherbox]').attr('checked', 'false');
                            $('[id$=Avaldocothertextbox]').val("");

                            $('[id$=AvalBENoBox]').val("");
                            $('[id$=AvalRefNoBox]').val("");
                            $('[id$=AvalBEAmountBox]').val("");
                            $('[id$=AvalConfirmationDate]').val("");
                            $('[id$=AvalSettlementDateBox]').val("");
                            $('[id$=AvalUnitBox]').val("");
                            $('[id$=AvalYieldBox]').val("");
                            $('[id$=AvalFlagBox]').val("");
                            $('[id$=AvalRemarkBox]').val("");
                            $('[id$=AvalAllotDealNoBox]').val("");

                            $('[id$=Avalbuyer]').children('option:not(:first)').remove();
                            $('[id$=Avalbuyer] option:first').remove();

                            $('[id$=Avalincvat]').children('option:not(:first)').remove();
                            $('[id$=Avalincvat] option:first').remove();
                            
                            $('[id$=avalseller]').children('option:not(:first)').remove();
                            $('[id$=avalseller] option:first').remove();
                            
                            $('[id$=HideAvalBuyerBank]').val("");
                            $('[id$=AvalBuyerBank]').val("");
                            $('[id$=HideAvalSellerBank]').val("");
                            $('[id$=AvalSellerBank]').val("");
                            
                            $('[id$=HideAvalBuyerAccount]').val("");
                            $('[id$=AvalBuyerAccount]').val("");
                            $('[id$=HideAvalSellerAccount]').val("");
                            $('[id$=AvalSellerAccount]').val("");
                            $(this).dialog("close");
                        },
                        "buttons": {
                            "Submit": function(){
                                var flag = "";
                                var result = true;
                                var depositdate = "";
                                var withdrawdate = "";
                                var idcard, bookbank, other, othertext = "";
                                let message = '';

                                if ($('[id$=Avaldocidcardbox]').is(':checked'))
                                    idcard = "Y";
                                if ($('[id$=Avaldocbookbankbox]').is(':checked'))
                                    bookbank = "Y";
                                if ($('[id$=Avaldocotherbox]').is(':checked'))
                                    othertext = $('[id$=Avaldocothertextbox]').val();

                                // Buyer Validate
                                if($('[id$=AvalConfirmationDate]').val() != "")
                                {
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalConfirmationDate]').val()) }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                message += "* Buyer Confirmation Date : " + data.d.Message + "<br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }

                                if($('[id$=AvalSettlementDateBox]').val() != "")
                                {
                                    if ($.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSettlementDateBox]').val()) >= $.datepicker.parseDate('dd/mm/yy', matdate)) {
                                        message += "* Buyer Settlement Date must come first the Maturity Date <br />";
                                        result = false;
                                    }
                                    else
                                    {
                                        $.ajax({
                                            type: 'POST',
                                            url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json',
                                            async: false,
                                            data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSettlementDateBox]').val()) }),
                                            success: function(data){
                                                if(!data.d.Success)
                                                {
                                                    message += "* Buyer Settlement Date : " + data.d.Message + "<br />";
                                                    result = false;
                                                }

                                            }
                                        });
                                    }
                                }
                                
                                // Seller Validate
                                if($('[id$=AvalSellerConfirmationDate]').val() != "")
                                {
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSellerConfirmationDate]').val()) }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                message += "* Seller Confirmation Date : " + data.d.Message + "<br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }
                                
                                if($('[id$=AvalSellerSettlementDateBox]').val() != "")
                                {
                                    if ($.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSellerSettlementDateBox]').val()) >= $.datepicker.parseDate('dd/mm/yy', matdate)) {
                                        message += "* Seller Settlement Date must come first the Maturity Date <br />";
                                        result = false;
                                    }
                                    else
                                    {
                                        $.ajax({
                                            type: 'POST',
                                            url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json',
                                            async: false,
                                            data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSellerSettlementDateBox]').val()) }),
                                            success: function(data){
                                                if(!data.d.Success)
                                                {
                                                    message += "* Seller Settlement Date : " + data.d.Message + "<br />";
                                                    result = false;
                                                }

                                            }
                                        });
                                    }
                                }
                                
                                if ($('[id$=AvalDepositDateBox]').val() != "") {
                                    depositdate = $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalDepositDateBox]').val());
                                }
                                if ($('[id$=AvalWithdrawDateBox]').val() != "") {
                                    withdrawdate = $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalWithdrawDateBox]').val());
                                }
                                
                                if($('[id$=AvalBENoBox]').val() == "")
                                {
                                    message += "* PN No is require!! <br />";
                                    result = false;
                                }
                                /*
                                else if($('[id$=AvalBENoBox]').val() != ""){
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/duplicateBENo',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'beno': $('[id$=AvalBENoBox]').val(), 'allotid': 0 }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                message += "* " + data.d.Message.replace("B/E", "PN") + " <br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }
                                */
                                if($('[id$=AvalBEAmountBox]').val() == "")
                                {
                                    message += "* PN Amount is require!! <br />";
                                    result = false;
                                }
//                                if($('[id$=AvalRefNoBox]').val() == "")
//                                {
//                                    message += "* Ref No is require!! <br />";
//                                    result = false;
//                                }
//                                if($('[id$=AvalYieldBox]').val() == "")
//                                {
//                                    message += "* Yield is require!! <br />"
//                                    result = false;
//                                }
//                                if($('[id$=Avalbuyer]').val() == "")
//                                {
//                                    message += "* Buyer is require!! <br />";
//                                    result = false;
//                                }
//                                if($('[id$=avalseller]').val() == "")
//                                {
//                                    message += "* Seller is require!! <br />";
//                                    result = false;
//                                }
                                
//                                if($('[id$=AvalUnitBox]').val() == "")
//                                {
//                                    message += "* Unit is require!! <br />";
//                                    result = false;
//                                }
                                
                                if (result) {
                                    var record = {
                                        BE_ALLOT_NO: $('[id$=AvalBENoBox]').val(),
                                        BE_ALLOT_REF_NO: $('[id$=AvalRefNoBox]').val(),
                                        BE_ALLOT_AMT: $('[id$=AvalBEAmountBox]').val(),
                                        BE_COMF_DATE: !$('[id$=AvalConfirmationDate]').val() ? "" : $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalConfirmationDate]').val()),
                                        BE_SETTLE_DATE: !$('[id$=AvalSettlementDateBox]').val() ? "" : $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSettlementDateBox]').val()),
                                        BE_ALLOT_UNIT: !$('[id$=AvalUnitBox]').val() ? 0 : $('[id$=AvalUnitBox]').val(),
                                        BE_ALLOT_YIELD: !$('[id$=AvalYieldBox]').val() ? 0 : $('[id$=AvalYieldBox]').val(),
                                        BE_BUYER_ID: $('[id$=Avalbuyer]').val(),
                                        BE_BUYER_INCVAT: $('[id$=Avalincvat]').val(),
                                        BE_SELLER_ID: $('[id$=avalseller]').val(),
                                        BE_FLAG: $('[id$=AvalFlagBox]').val(),
                                        BE_ALLOT_COMMENT: $('[id$=AvalRemarkBox]').val(),
                                        BE_MAT_DATE: $.datepicker.parseDate('dd/mm/yy', matdate),
                                        BE_ALLOT_DEAL_NO: $('[id$=AvalAllotDealNoBox]').val(),
                                        BE_ALLOT_SKSNO: $('[id$=AvalSKSNoBox]').val(),
                                        BE_ALLOT_PATRAREGNO: $('[id$=AvalPatraRegNoBox]').val(),
                                        BE_ALLOT_DEPOSITDATE: depositdate,
                                        BE_ALLOT_CLIENTNAME: $('[id$=AvalClientNameBox]').val(),
                                        BE_ALLOT_WITHDRAWDATE: withdrawdate,
                                        BE_ALLOT_IDCARD: idcard,
                                        BE_ALLOT_BOOKBANK: bookbank,
                                        BE_ALLOT_OTHER: othertext,
                                        BE_SELLER_COMF_DATE: !$('[id$=AvalSellerConfirmationDate]').val() ? "" : $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSellerConfirmationDate]').val()),
                                        BE_SELLER_SETTLE_DATE: !$('[id$=AvalSellerSettlementDateBox]').val() ? "" : $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSellerSettlementDateBox]').val()),
                                        BE_SELLER_ALLOT_YIELD: !$('[id$=AvalSellerYieldBox]').val() ? 0 : $('[id$=AvalSellerYieldBox]').val(),
                                        BE_OWNER: $('input[name=AvalOwnerRadio]:checked').val(),
                                        
                                        BESERIESID: seriesid
                                    };
                                    var request = $.ajax({
                                        type: "POST",
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/AddAllotment',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        data: JSON.stringify({ 'record': record }),
                                        success: function(data) {
                                            if (data.d.Result == "OK") {
                                                alert("Add Allotment Complete");
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
                                }
                                else
                                {
                                    _Dialog(message);
                                }
                            },
                            "Close": function(){
                                $('[id$=AvalSKSNoBox]').val("");
                                $('[id$=AvalPatraRegNoBox]').val("");
                                $('[id$=AvalClientNameBox]').val("");
                                $('[id$=AvalDepositDateBox]').val("");
                                $('[id$=AvalWithdrawDateBox]').val("");
                                $('[id$=Avaldocidcardbox]').attr('checked', 'false');
                                $('[id$=Avaldocbookbankbox]').attr('checked', 'false');
                                $('[id$=Avaldocotherbox]').attr('checked', 'false');
                                $('[id$=Avaldocothertextbox]').val("");

                                $('[id$=AvalBENoBox]').val("");
                                $('[id$=AvalRefNoBox]').val("");
                                $('[id$=AvalBEAmountBox]').val("");
                                $('[id$=AvalConfirmationDate]').val("");
                                $('[id$=AvalSettlementDateBox]').val("");
                                $('[id$=AvalUnitBox]').val("");
                                $('[id$=AvalYieldBox]').val("");
                                $('[id$=AvalFlagBox]').val("");
                                $('[id$=AvalRemarkBox]').val("");
                                $('[id$=AvalAllotDealNoBox]').val("");

                                $('[id$=Avalbuyer]').children('option:not(:first)').remove();
                                $('[id$=Avalbuyer] option:first').remove();

                                $('[id$=Avalincvat]').children('option:not(:first)').remove();
                                $('[id$=Avalincvat] option:first').remove();
                                
                                $('[id$=avalseller]').children('option:not(:first)').remove();
                                $('[id$=avalseller] option:first').remove();
                                
                                $('[id$=HideAvalBuyerBank]').val("");
                                $('[id$=AvalBuyerBank]').val("");
                                $('[id$=HideAvalSellerBank]').val("");
                                $('[id$=AvalSellerBank]').val("");
                                
                                $('[id$=HideAvalBuyerAccount]').val("");
                                $('[id$=AvalBuyerAccount]').val("");
                                $('[id$=HideAvalSellerAccount]').val("");
                                $('[id$=AvalSellerAccount]').val("");
                                
                                $(this).dialog("close");
                            }
                        }
                    });
                }
                else
                {
                    $("#dialogdetail").dialog({
                        title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                        width: 1100,
                        modal: true,
                        open: function() {
                            $("input[name=OwnerRadio][value='KK']").prop('checked', true);  
                        },
                        close: function() {
                            $('.sksnobox').val("");
                            $('.patraregnobox').val("");
                            $('.clientnamebox').val("");
                            $('.depositdatebox').val("");
                            $('.withdrawdatebox').val("");
                            $('.docidcardbox').attr('checked', 'false');
                            $('.docbookbankbox').attr('checked', 'false');
                            $('.docotherbox').attr('checked', 'false');
                            $('.docothertextbox').val("");

                            $('.benobox').val("");
                            $('.refnobox').val("");
                            $('.beamountbox').val("");
                            $('.confirmdatebox').val("");
                            $('.settledatebox').val("");
                            $('.unitbox').val("");
                            $('.yieldbox').val("");
                            $('.flagbox').val("");
                            $('.remarkbox').val("");
                            $('.allotdealnobox').val("");

                            $('.buyer').children('option:not(:first)').remove();
                            $('.buyer option:first').remove();

                            $('.incvat').children('option:not(:first)').remove();
                            $('.incvat option:first').remove();
                            $(this).dialog("close");
                        },
                        "buttons": {
                            "Submit": function() {
                                var flag = "";
                                var result = true;
                                var depositdate = "";
                                var withdrawdate = "";
                                var idcard, bookbank, other, othertext = "";
                                let message = '';

                                if ($('.docidcardbox').is(':checked'))
                                    idcard = "Y";
                                if ($('.docbookbankbox').is(':checked'))
                                    bookbank = "Y";
                                if ($('.docotherbox').is(':checked'))
                                    othertext = $('.docothertextbox').val();

                                if ($.datepicker.parseDate('dd/mm/yy', $('.settledatebox').val()) >= $.datepicker.parseDate('dd/mm/yy', matdate)) {
                                    message += "* The Settlement Date must come first the Maturity Date <br />";
                                    // alert('The Settlement Date must come first the Maturity Date  ');
                                    result = false;
                                }
                                else if($('.settledatebox').val() != "")
                                {
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('.settledatebox').val()) }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                message += "* Settlement Date : " + data.d.Message + "<br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }
                                if ($('.depositdatebox').val() != "") {
                                    depositdate = $.datepicker.parseDate('dd/mm/yy', $('.depositdatebox').val());
                                }
                                if ($('.withdrawdatebox').val() != "") {
                                    withdrawdate = $.datepicker.parseDate('dd/mm/yy', $('.withdrawdatebox').val());
                                }
                                
                                if($('.benobox').val() == "")
                                {
                                    message += "* B/E No is require!! <br />";
                                    result = false;
                                }else if($('.benobox').val() != ""){
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/duplicateBENo',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'beno': $('.benobox').val(), 'allotid': 0 }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                // alert(data.d.Message);
                                                message += "* " + data.d.Message + " <br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }
                                
                                if($('.beamountbox').val() == "")
                                {
                                    message += "* B/E Amount is require!! <br />";
                                    result = false;
                                }
//                                if($('.refnobox').val() == "")
//                                {
//                                    message += "* Ref No is require!! <br />";
//                                    result = false;
//                                }
                                if($('.yieldbox').val() == "")
                                {
                                    message += "* Yield is require!! <br />";
                                    result = false;
                                }
                                if($('.buyer').val() == "")
                                {
                                    message += "* Buyer is require!! <br />";
                                    result = false;
                                }
                                
                                if($('.confirmdatebox').val() == "")
                                {
                                    message += "* Confirmation Date is require!! <br />";
                                    result = false;
                                }
                                else if($('.confirmdatebox').val() != "")
                                {
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('.confirmdatebox').val()) }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                message += "* Confirmation Date : " + data.d.Message + "<br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }
                                
                                if($('.unitbox').val() == "")
                                {
                                    message += "* Unit is require!! <br />";
                                    result = false;
                                }
                                
                                if (result) {
                                    var record = {
                                        BE_ALLOT_NO: $('.benobox').val(),
                                        BE_ALLOT_REF_NO: $('.refnobox').val(),
                                        BE_ALLOT_AMT: $('.beamountbox').val(),
                                        BE_COMF_DATE: $.datepicker.parseDate('dd/mm/yy', $('.confirmdatebox').val()),
                                        BE_SETTLE_DATE: $.datepicker.parseDate('dd/mm/yy', $('.settledatebox').val()),
                                        BE_ALLOT_UNIT: $('.unitbox').val(),
                                        BE_ALLOT_YIELD: $('.yieldbox').val(),
                                        BE_BUYER_ID: $('.buyer').val(),
                                        BE_BUYER_INCVAT: $('.incvat').val(),
                                        BE_FLAG: $('.flagbox').val(),
                                        BE_ALLOT_COMMENT: $('.remarkbox').val(),
                                        BE_MAT_DATE: $.datepicker.parseDate('dd/mm/yy', matdate),
                                        BE_ALLOT_DEAL_NO: $('.allotdealnobox').val(),
                                        BE_ALLOT_SKSNO: $('.sksnobox').val(),
                                        BE_ALLOT_PATRAREGNO: $('.patraregnobox').val(),
                                        BE_ALLOT_DEPOSITDATE: depositdate,
                                        BE_ALLOT_CLIENTNAME: $('.clientnamebox').val(),
                                        BE_ALLOT_WITHDRAWDATE: withdrawdate,
                                        BE_ALLOT_IDCARD: idcard,
                                        BE_ALLOT_BOOKBANK: bookbank,
                                        BE_ALLOT_OTHER: othertext,
                                        BE_OWNER: $('input[name=OwnerRadio]:checked').val(),
                                        BESERIESID: seriesid
                                    };
                                    var request = $.ajax({
                                        type: "POST",
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/AddAllotment',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        data: JSON.stringify({ 'record': record }),
                                        success: function(data) {
                                            if (data.d.Result == "OK") {
                                                alert("Add Allotment Complete");
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
                                }
                                else
                                {
                                    _Dialog(message);
                                }
                            },
                            "Close": function() {
                                $('.sksnobox').val("");
                                $('.patraregnobox').val("");
                                $('.clientnamebox').val("");
                                $('.depositdatebox').val("");
                                $('.withdrawdatebox').val("");

                                $('.docidcardbox').removeAttr('checked');
                                $('.docbookbankbox').removeAttr('checked');
                                $('.docotherbox').removeAttr('checked');
                                $('.docothertextbox').val('');

                                $('.benobox').val("");
                                $('.refnobox').val("");
                                $('.beamountbox').val("");
                                $('.confirmdatebox').val("");
                                $('.settledatebox').val("");
                                $('.unitbox').val("");
                                $('.yieldbox').val("");
                                $('.flagbox').val("");
                                $('.remarkbox').val("");
                                $('.allotdealnobox').val(""),

                                $('.buyer').children('option:not(:first)').remove();
                                $('.buyer option:first').remove();

                                $('.incvat').children('option:not(:first)').remove();
                                $('.incvat option:first').remove();
                                $(this).dialog("close");
                            }
                        }
                    });
                }
                return false;
            });

            $('.runningrefno').click(function() {
                var seriesid = $(this).val();
                var request = $.ajax({
                    type: "POST",
                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/RunningRefNo',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ 'BESeriesID': seriesid }),
                    success: function(data) {
                        if (data.d.Result == "OK") {
                            alert("Add Running RefNo Complete");
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
                return false;
            });
            
            $('.runningrefno2').click(function() {
                var seriesid = $(this).val();
                var request = $.ajax({
                    type: "POST",
                    url: '<%=Config.Website %>/BE/frm_BESeries.aspx/RunningRefNo2',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ 'BESeriesID': seriesid }),
                    success: function(data) {
                        if (data.d.Result == "OK") {
                            alert("Add Running RefNo Complete");
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
                return false;
            });
            
            
            $('#btnAvalIssuerSearch').click(function(){
                $.ajax({
                    type: "POST",
                    url: "<%=Config.Website %>/Services/Customer.asmx/IssuerJson",
                    data: "{cust:'" + $('[id$=avalIssuerBox]').val() + "',custtype:'issuer'}",
                    minLength: 0,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data){
                        var a = "";
                        $('[id$=avalissuer]').children('option:not(:first)').remove();
                        $('[id$=avalissuer] option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return a = a + '<option value="' + this.CustomerID + '">' + this.Name + '</option>';
                        });
                        $('[id$=avalissuer]').append(a);

//                        var b = "";
//                        $('[id$=avalxissuerbank]').children('option:not(:first)').remove();
//                        $('[id$=avalxissuerbank] option:first').remove();
//                        $($.parseJSON(data.d)).each(function() {
//                            return b = b + '<option value="' + this.Bank + '">' + this.Bank + '</option>';
//                        });
//                        $('[id$=avalxissuerbank]').append(b);
                        // $('[id$=avalIssuerBankBox]').val($('[id$=avalxissuerbank]').val());


//                        var c = "";
//                        $('[id$=avalxissueracct]').children('option:not(:first)').remove();
//                        $('[id$=avalxissueracct] option:first').remove();
//                        $($.parseJSON(data.d)).each(function() {
//                            return c = c + '<option value="' + this.Account + '">' + this.Account + '</option>';
//                        });
//                        $('[id$=avalxissueracct]').append(c);
                        // $('[id$=avalIssuerAcctBox]').val($('[id$=avalxissueracct]').val());
                    }
                });
            });
            
            $('#btnAvalSellerSearch').click(function(){
                $.ajax({
                    type: "POST",
                    url: "<%=Config.Website %>/Services/Customer.asmx/SellerJson",
                    data: "{cust:'" + $('[id$=avalSellerBox]').val() + "',custtype:'seller'}",
                    minLength: 0,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data){
                        var a = "";
                        $('[id$=avalseller]').children('option:not(:first)').remove();
                        $('[id$=avalseller] option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return a = a + '<option value="' + this.CustomerID + '">' + this.Name + '</option>';
                        });
                        $('[id$=avalseller]').append(a);
                        
                        var b = "";
                        $('[id$=HideAvalSellerBank]').children('option:not(:first)').remove();
                        $('[id$=HideAvalSellerBank] option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return b = b + '<option value="' + this.Bank + '">' + this.Bank + '</option>';
                        });
                        $('[id$=HideAvalSellerBank]').append(b);
                        $('[id$=AvalSellerBank]').val($('[id$=HideAvalSellerBank]').val());
                        
                        var c = "";
                        $('[id$=HideAvalSellerAccount]').children('option:not(:first)').remove();
                        $('[id$=HideAvalSellerAccount] option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return c = c + '<option value="' + this.Account + '">' + this.Account + '</option>';
                        });
                        $('[id$=HideAvalSellerAccount]').append(c);
                        $('[id$=AvalSellerAccount]').val($('[id$=HideAvalSellerAccount]').val());
                    }
                });
            });

            $('#btn-issuer').click(function() {
                $.ajax({
                    type: "POST",
                    url: "<%=Config.Website %>/Services/Customer.asmx/IssuerJson",
                    data: "{cust:'" + $('.issuerbox').val() + "',custtype:'issuer'}",
                    minLength: 0,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        var a = "";
                        $('.issuer').children('option:not(:first)').remove();
                        $('.issuer option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return a = a + '<option value="' + this.CustomerID + '">' + this.Name + '</option>';
                        });
                        $('.issuer').append(a);

                        var b = "";
                        $('.xissuerbank').children('option:not(:first)').remove();
                        $('.xissuerbank option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return b = b + '<option value="' + this.Bank + '">' + this.Bank + '</option>';
                        });
                        $('.xissuerbank').append(b);
                        $('.issuerbankbox').val($('.xissuerbank').val());


                        var c = "";
                        $('.xissueracct').children('option:not(:first)').remove();
                        $('.xissueracct option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return c = c + '<option value="' + this.Account + '">' + this.Account + '</option>';
                        });
                        $('.xissueracct').append(c);
                        $('.issueracctbox').val($('.xissueracct').val());

                    }
                });
            });


            $('#btn-customer').click(function() {
                $.ajax({
                    type: "POST",
                    url: "<%=Config.Website %>/Services/Customer.asmx/CustomerJson",
                    data: "{cust:'" + $('.buyerbox').val() + "'}",
                    minLength: 0,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        var a = "";
                        $('.buyer').children('option:not(:first)').remove();
                        $('.buyer option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return a = a + '<option value="' + this.value.trim() + '">' + this.label.trim() + '</option>';
                        });
                        $('.buyer').append(a);

                        var b = "";
                        $('.incvat').children('option:not(:first)').remove();
                        $('.incvat option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return b = b + '<option value="' + this.incvat.trim() + '">' + this.value.trim() + '</option>';
                        });
                        $('.incvat').append(b);
                    }
                });
            });
            
            $('#Avalbtn-customer').click(function(){
                $.ajax({
                    type: "POST",
                    url: "<%=Config.Website %>/Services/Customer.asmx/CustomerJson",
                    data: "{cust:'" + $('[id$=AvalBuyerBox]').val() + "'}",
                    minLength: 0,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(data) {
                        var a = "";
                        $('[id$=Avalbuyer]').children('option:not(:first)').remove();
                        $('[id$=Avalbuyer] option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return a = a + '<option value="' + this.value.trim() + '">' + this.label.trim() + '</option>';
                        });
                        $('[id$=Avalbuyer]').append(a);

                        var b = "";
                        $('[id$=Avalincvat]').children('option:not(:first)').remove();
                        $('[id$=Avalincvat] option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return b = b + '<option value="' + this.incvat.trim() + '">' + this.value.trim() + '</option>';
                        });
                        $('[id$=Avalincvat]').append(b);
                        
                        var c = "";
                        $('[id$=HideAvalBuyerBank]').children('option:not(:first)').remove();
                        $('[id$=HideAvalBuyerBank] option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return c = c + '<option value="' + this.Bank + '">' + this.Bank + '</option>';
                        });
                        $('[id$=HideAvalBuyerBank]').append(c);
                        $('[id$=AvalBuyerBank]').val($('[id$=HideAvalBuyerBank]').val());
                        
                        var d = "";
                        $('[id$=HideAvalBuyerAccount]').children('option:not(:first)').remove();
                        $('[id$=HideAvalBuyerAccount] option:first').remove();
                        $($.parseJSON(data.d)).each(function() {
                            return d = d + '<option value="' + this.Account + '">' + this.Account + '</option>';
                        });
                        $('[id$=HideAvalBuyerAccount]').append(d);
                        $('[id$=AvalBuyerAccount]').val($('[id$=HideAvalBuyerAccount]').val());
                    }
                });
            });
            
//            $('[id$=avalissuer]').change(function(){
//                $('[id$=avalxissuerbank]').prop('selectedIndex', $(this).prop('selectedIndex'));
//                $('[id$=avalIssuerBankBox]').val($('[id$=avalxissuerbank]').val());

//                $('[id$=avalxissueracct]').prop('selectedIndex', $(this).prop('selectedIndex'));
//                $('[id$=avalIssuerAcctBox]').val($('[id$=avalxissueracct]').val());
//            });
            
            $('[id$=avalseller]').change(function(){
                $('[id$=HideAvalSellerBank]').prop('selectedIndex', $(this).prop('selectedIndex'));
                $('[id$=AvalSellerBank]').val($('[id$=HideAvalSellerBank]').val());
                
                $('[id$=HideAvalSellerAccount]').prop('selectedIndex', $(this).prop('selectedIndex'));
                $('[id$=AvalSellerAccount]').val($('[id$=HideAvalSellerAccount]').val());
            });

            $('.issuer').change(function() {
                $('.xissuerbank').prop('selectedIndex', $(this).prop('selectedIndex'));
                $('.issuerbankbox').val($('.xissuerbank').val());

                $('.xissueracct').prop('selectedIndex', $(this).prop('selectedIndex'));
                $('.issueracctbox').val($('.xissueracct').val());
            });

            $('.buyer').change(function() {
                $('.incvat').prop('selectedIndex', $(this).prop('selectedIndex'));
                
                $('[id$=HideAvalBuyerBank]').prop('selectedIndex', $(this).prop('selectedIndex'));
                $('[id$=AvalBuyerBank]').val($('[id$=HideAvalBuyerBank]').val());
                
                $('[id$=HideAvalBuyerAccount]').prop('selectedIndex', $(this).prop('selectedIndex'));
                $('[id$=AvalBuyerAccount]').val($('[id$=HideAvalBuyerAccount]').val());
            });

            $(".btnshowdetail").click(function() {
                if (this.tog ^= 1) {
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    //$(this).src("../Theme/icon/actions-redo.png");
                } else {
                    $(this).closest("tr").next().remove();
                    //$(this).src("../Theme/icon/icon-tracking.png");
                }
            });

        });
             
    </script>
    <script type="text/javascript">
        $(function() {
            $(document).on("click", ".editdetail", function(e) {
                let res = $(this).attr('alt').split('||');
                let beallotmentid = res[0];
                let avalflag = res[1]; 
                let matdate = $(this).attr('title');
                
                if(avalflag != "" && avalflag != undefined && avalflag != "0")
                {
                    $('#dialogdetailAval').dialog({
                        title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                        width: 1100,
                        modal: true,
                        open: function(){
                            $.ajax({
                                type: "POST",
                                url: '<%=Config.Website %>/BE/frm_BESeries.aspx/GetAllotment',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({ 'BEAllotmentID': beallotmentid }),
                                success: function(data){
                                    var obj = data.d;
                                    $('[id$=AvalSKSNoBox]').val(obj[0].BE_ALLOT_SKSNO);
                                    $('[id$=AvalPatraRegNoBox]').val(obj[0].BE_ALLOT_PATRAREGNO);
                                    $('[id$=AvalDepositDateBox]').val(obj[0].BE_ALLOT_DEPOSITDATE_STR);
                                    $('[id$=AvalClientNameBox]').val(obj[0].BE_ALLOT_CLIENTNAME);
                                    $('[id$=AvalWithdrawDateBox]').val(obj[0].BE_ALLOT_WITHDRAWDATE_STR);
                                    if (obj[0].BE_ALLOT_IDCARD == 'Y')
                                        $('[id$=Avaldocidcardbox]').prop('checked', true);
                                    if (obj[0].BE_ALLOT_BOOKBANK == 'Y')
                                        $('[id$=Avaldocbookbankbox]').prop('checked', true);
                                    if (obj[0].BE_ALLOT_OTHER != '') {
                                        $('[id$=Avaldocotherbox]').prop('checked', true);
                                        $('[id$=Avaldocothertextbox]').val(obj[0].BE_ALLOT_OTHER);
                                    }
                                    $('[id$=AvalBENoBox]').val(obj[0].BE_ALLOT_NO)
                                    $('[id$=AvalRefNoBox]').val(obj[0].BE_ALLOT_REF_NO);
                                    $('[id$=AvalBEAmountBox]').val(obj[0].BE_ALLOT_AMT);
                                    $('[id$=AvalAllotDealNoBox]').val(obj[0].BE_ALLOT_DEAL_NO);
                                    
                                    // buyer
                                    if(obj[0].BE_COMF_DATE_STR)
                                    {
                                        $('[id$=AvalConfirmationDate]').val(obj[0].BE_COMF_DATE_STR);    
                                    }
                                    
                                    if(obj[0].BE_SETTLE_DATE_STR)
                                    {
                                        $('[id$=AvalSettlementDateBox]').val(obj[0].BE_SETTLE_DATE_STR);
                                    }
                                    
                                    if(obj[0].BE_ALLOT_YIELD && obj[0].BE_ALLOT_YIELD != 0)
                                    {
                                        $('[id$=AvalYieldBox]').val(obj[0].BE_ALLOT_YIELD);
                                    }
                                    
                                    if(obj[0].CUSTOMER)
                                    {
                                        if(obj[0].CUSTOMER.TargetBank)
                                        {
                                            $('[id$=AvalBuyerBank]').val(obj[0].CUSTOMER.TargetBank);
                                        }
                                        
                                        if(obj[0].CUSTOMER.TargetAcct)
                                        {
                                            $('[id$=AvalBuyerAccount]').val(obj[0].CUSTOMER.TargetAcct);
                                        }
                                    
                                        var a = "";
                                        $('[id$=Avalbuyer]').children('option:not(:first)').remove();
                                        $('[id$=Avalbuyer] option:first').remove();
                                        a = '<option value="' + obj[0].CUSTOMER.CustomerID + '">' + obj[0].CUSTOMER.NameEn + '</option>';
                                        $('[id$=Avalbuyer]').append(a);
                                        
                                        var b = "";
                                        $('[id$=Avalincvat]').children('option:not(:first)').remove();
                                        $('[id$=Avalincvat] option:first').remove();
                                        b = b + '<option value="' + obj[0].CUSTOMER.IncVat + '">' + obj[0].CUSTOMER.CustomerID + '</option>';
                                        $('[id$=Avalincvat]').append(b);
                                    }
                                    
                                    // Seller
                                    if(obj[0].DISPLAY_SELLER_COMF_DATE)
                                    {
                                        $('[id$=AvalSellerConfirmationDate]').val(obj[0].DISPLAY_SELLER_COMF_DATE);    
                                    }
                                    
                                    if(obj[0].DISPLAY_SELLER_SETTLE_DATE)
                                    {
                                        $('[id$=AvalSellerSettlementDateBox]').val(obj[0].DISPLAY_SELLER_SETTLE_DATE);
                                    }
                                    
                                    if(obj[0].BE_SELLER_ALLOT_YIELD && obj[0].BE_SELLER_ALLOT_YIELD != 0)
                                    {
                                        $('[id$=AvalSellerYieldBox]').val(obj[0].BE_SELLER_ALLOT_YIELD);
                                    }
                                    
                                    if(obj[0].CUSTSELLER)
                                    {
                                        if(obj[0].CUSTSELLER.TargetBank)
                                        {
                                            $('[id$=AvalSellerBank]').val(obj[0].CUSTSELLER.TargetBank);
                                        }
                                        
                                        if(obj[0].CUSTSELLER.TargetAcct)
                                        {
                                            $('[id$=AvalSellerAccount]').val(obj[0].CUSTSELLER.TargetAcct);
                                        }
                                    
                                        var c = "";
                                        $('[id$=avalseller]').children('option:not(:first)').remove();
                                        $('[id$=avalseller] option:first').remove();
                                        c = c + '<option value="' + obj[0].BE_SELLER_ID + '">' + obj[0].SELLER_NAME + "</option>";
                                        $('[id$=avalseller]').append(c);
                                    }
                                    
                                    $('[id$=AvalUnitBox]').val(obj[0].BE_ALLOT_UNIT);
                                    $('[id$=AvalFlagBox]').val(obj[0].BE_FLAG);
                                    $('[id$=AvalRemarkBox]').val(obj[0].BE_ALLOT_COMMENT);
                                    $("input[name=AvalOwnerRadio][value=" + obj[0].BE_OWNER + "]").prop('checked', true);
                                },
                                error: function(xhr, ajaxOptions, thrownError){
                                    var err = JSON.parse(xhr.responseText);
                                    alert(err.Message);
                                }
                            });
                        },
                        close: function(){
                            $('[id$=AvalSKSNoBox]').val("");
                            $('[id$=AvalPatraRegNoBox]').val("");
                            $('[id$=AvalClientNameBox]').val("");
                            $('[id$=AvalDepositDateBox]').val("");
                            $('[id$=AvalWithdrawDateBox]').val("");
                            $('[id$=Avaldocidcardbox]').attr('checked', 'false');
                            $('[id$=Avaldocbookbankbox]').attr('checked', 'false');
                            $('[id$=Avaldocotherbox]').attr('checked', 'false');
                            $('[id$=Avaldocothertextbox]').val("");

                            $('[id$=AvalBENoBox]').val("");
                            $('[id$=AvalRefNoBox]').val("");
                            $('[id$=AvalBEAmountBox]').val("");
                            $('[id$=AvalConfirmationDate]').val("");
                            $('[id$=AvalSettlementDateBox]').val("");
                            $('[id$=AvalUnitBox]').val("");
                            $('[id$=AvalYieldBox]').val("");
                            $('[id$=AvalFlagBox]').val("");
                            $('[id$=AvalRemarkBox]').val("");
                            $('[id$=AvalAllotDealNoBox]').val("");

                            $('[id$=Avalbuyer]').children('option:not(:first)').remove();
                            $('[id$=Avalbuyer] option:first').remove();

                            $('[id$=Avalincvat]').children('option:not(:first)').remove();
                            $('[id$=Avalincvat] option:first').remove();
                            
                            $('[id$=avalseller]').children('option:not(:first)').remove();
                            $('[id$=avalseller] option:first').remove();
                            
                            $('[id$=HideAvalBuyerBank]').val("");
                            $('[id$=AvalBuyerBank]').val("");
                            $('[id$=HideAvalSellerBank]').val("");
                            $('[id$=AvalSellerBank]').val("");
                            
                            $('[id$=HideAvalBuyerAccount]').val("");
                            $('[id$=AvalBuyerAccount]').val("");
                            $('[id$=HideAvalSellerAccount]').val("");
                            $('[id$=AvalSellerAccount]').val("");
                            $(this).dialog("close");
                        },
                        "buttons": {
                            "Submit": function() {
                                var flag = "";
                                var result = true;
                                var depositdate = "";
                                var withdrawdate = "";
                                var idcard, bookbank, other, othertext = "";
                                let message = '';

                                if ($('[id$=Avaldocidcardbox]').is(':checked'))
                                    idcard = "Y";
                                if ($('[id$=Avaldocbookbankbox]').is(':checked'))
                                    bookbank = "Y";
                                if ($('[id$=Avaldocotherbox]').is(':checked'))
                                    othertext = $('[id$=Avaldocothertextbox]').val();

                                // Buyer Validate
                                if($('[id$=AvalConfirmationDate]').val() != "")
                                {
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalConfirmationDate]').val()) }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                message += "* Buyer Confirmation Date : " + data.d.Message + "<br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }

                                if($('[id$=AvalSettlementDateBox]').val() != "")
                                {
                                    if ($.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSettlementDateBox]').val()) >= $.datepicker.parseDate('dd/mm/yy', matdate)) {
                                        message += "* Buyer Settlement Date must come first the Maturity Date <br />";
                                        result = false;
                                    }
                                    else
                                    {
                                        $.ajax({
                                            type: 'POST',
                                            url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json',
                                            async: false,
                                            data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSettlementDateBox]').val()) }),
                                            success: function(data){
                                                if(!data.d.Success)
                                                {
                                                    message += "* Buyer Settlement Date : " + data.d.Message + "<br />";
                                                    result = false;
                                                }

                                            }
                                        });
                                    }
                                }
                                
                                // Seller Validate
                                if($('[id$=AvalSellerConfirmationDate]').val() != "")
                                {
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSellerConfirmationDate]').val()) }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                message += "* Seller Confirmation Date : " + data.d.Message + "<br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }
                                
                                if($('[id$=AvalSellerSettlementDateBox]').val() != "")
                                {
                                    if ($.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSellerSettlementDateBox]').val()) >= $.datepicker.parseDate('dd/mm/yy', matdate)) {
                                        message += "* Seller Settlement Date must come first the Maturity Date <br />";
                                        result = false;
                                    }
                                    else
                                    {
                                        $.ajax({
                                            type: 'POST',
                                            url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json',
                                            async: false,
                                            data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSellerSettlementDateBox]').val()) }),
                                            success: function(data){
                                                if(!data.d.Success)
                                                {
                                                    message += "* Seller Settlement Date : " + data.d.Message + "<br />";
                                                    result = false;
                                                }

                                            }
                                        });
                                    }
                                }
                                
                                if ($('[id$=AvalDepositDateBox]').val() != "") {
                                    depositdate = $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalDepositDateBox]').val());
                                }
                                if ($('[id$=AvalWithdrawDateBox]').val() != "") {
                                    withdrawdate = $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalWithdrawDateBox]').val());
                                }
                                
                                if($('[id$=AvalBENoBox]').val() == "")
                                {
                                    message += "* PN No is require!! <br />";
                                    result = false;
                                }
                                /*
                                else if($('[id$=AvalBENoBox]').val() != ""){
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/duplicateBENo',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'beno': $('[id$=AvalBENoBox]').val(), 'allotid': beallotmentid }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                message += "* " + data.d.Message.replace("B/E", "PN") + " <br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }
                                */
                                if($('[id$=AvalBEAmountBox]').val() == "")
                                {
                                    message += "* PN Amount is require!! <br />";
                                    result = false;
                                }
                                
                                if (result == true) {
                                    var record = {
                                        BE_ALLOT_NO: $('[id$=AvalBENoBox]').val(),
                                        BE_ALLOT_REF_NO: $('[id$=AvalRefNoBox]').val(),
                                        BE_ALLOT_AMT: $('[id$=AvalBEAmountBox]').val(),
                                        BE_COMF_DATE: !$('[id$=AvalConfirmationDate]').val() ? "" : $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalConfirmationDate]').val()),
                                        BE_SETTLE_DATE: !$('[id$=AvalSettlementDateBox]').val() ? "" : $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSettlementDateBox]').val()),
                                        BE_ALLOT_UNIT: !$('[id$=AvalUnitBox]').val() ? 0 : $('[id$=AvalUnitBox]').val(),
                                        BE_ALLOT_YIELD: !$('[id$=AvalYieldBox]').val() ? 0 : $('[id$=AvalYieldBox]').val(),
                                        BE_BUYER_ID: $('[id$=Avalbuyer]').val(),
                                        BE_BUYER_INCVAT: $('[id$=Avalincvat]').val(),
                                        BE_SELLER_ID: $('[id$=avalseller]').val(),
                                        BE_FLAG: $('[id$=AvalFlagBox]').val(),
                                        BE_ALLOT_COMMENT: $('[id$=AvalRemarkBox]').val(),
                                        BE_MAT_DATE: $.datepicker.parseDate('dd/mm/yy', matdate),
                                        BE_ALLOT_DEAL_NO: $('[id$=AvalAllotDealNoBox]').val(),
                                        BE_ALLOT_SKSNO: $('[id$=AvalSKSNoBox]').val(),
                                        BE_ALLOT_PATRAREGNO: $('[id$=AvalPatraRegNoBox]').val(),
                                        BE_ALLOT_DEPOSITDATE: depositdate,
                                        BE_ALLOT_CLIENTNAME: $('[id$=AvalClientNameBox]').val(),
                                        BE_ALLOT_WITHDRAWDATE: withdrawdate,
                                        BE_ALLOT_IDCARD: idcard,
                                        BE_ALLOT_BOOKBANK: bookbank,
                                        BE_ALLOT_OTHER: othertext,
                                        BE_SELLER_COMF_DATE: !$('[id$=AvalSellerConfirmationDate]').val() ? "" : $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSellerConfirmationDate]').val()),
                                        BE_SELLER_SETTLE_DATE: !$('[id$=AvalSellerSettlementDateBox]').val() ? "" : $.datepicker.parseDate('dd/mm/yy', $('[id$=AvalSellerSettlementDateBox]').val()),
                                        BE_SELLER_ALLOT_YIELD: !$('[id$=AvalSellerYieldBox]').val() ? 0 : $('[id$=AvalSellerYieldBox]').val(),
                                        BE_OWNER: $('input[name=AvalOwnerRadio]:checked').val(),
                                        BEALLOTMENTID: beallotmentid
                                    };
                                    var request = $.ajax({
                                        type: "POST",
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/UpdateAllotment',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        data: JSON.stringify({ 'record': record }),
                                        success: function(data) {
                                            if (data.d.Result == "OK") {
                                                alert("Update Allotment Complete");
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
                                }
                                else
                                {
                                    _Dialog(message);
                                }
                            },
                            "Close": function(){
                                $('[id$=AvalSKSNoBox]').val("");
                                $('[id$=AvalPatraRegNoBox]').val("");
                                $('[id$=AvalClientNameBox]').val("");
                                $('[id$=AvalDepositDateBox]').val("");
                                $('[id$=AvalWithdrawDateBox]').val("");
                                $('[id$=Avaldocidcardbox]').attr('checked', 'false');
                                $('[id$=Avaldocbookbankbox]').attr('checked', 'false');
                                $('[id$=Avaldocotherbox]').attr('checked', 'false');
                                $('[id$=Avaldocothertextbox]').val("");

                                $('[id$=AvalBENoBox]').val("");
                                $('[id$=AvalRefNoBox]').val("");
                                $('[id$=AvalBEAmountBox]').val("");
                                $('[id$=AvalConfirmationDate]').val("");
                                $('[id$=AvalSettlementDateBox]').val("");
                                $('[id$=AvalUnitBox]').val("");
                                $('[id$=AvalYieldBox]').val("");
                                $('[id$=AvalFlagBox]').val("");
                                $('[id$=AvalRemarkBox]').val("");
                                $('[id$=AvalAllotDealNoBox]').val("");

                                $('[id$=Avalbuyer]').children('option:not(:first)').remove();
                                $('[id$=Avalbuyer] option:first').remove();

                                $('[id$=Avalincvat]').children('option:not(:first)').remove();
                                $('[id$=Avalincvat] option:first').remove();
                                
                                $('[id$=avalseller]').children('option:not(:first)').remove();
                                $('[id$=avalseller] option:first').remove();
                                
                                $('[id$=HideAvalBuyerBank]').val("");
                                $('[id$=AvalBuyerBank]').val("");
                                $('[id$=HideAvalSellerBank]').val("");
                                $('[id$=AvalSellerBank]').val("");
                                
                                $('[id$=HideAvalBuyerAccount]').val("");
                                $('[id$=AvalBuyerAccount]').val("");
                                $('[id$=HideAvalSellerAccount]').val("");
                                $('[id$=AvalSellerAccount]').val("");
                                
                                $(this).dialog("close");
                            }
                        }
                    });
                }
                else
                {
                    $("#dialogdetail").dialog({
                        title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                        width: 1100,
                        modal: true,
                        open: function() {
                            $.ajax({
                                type: "POST",
                                url: '<%=Config.Website %>/BE/frm_BESeries.aspx/GetAllotment',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                data: JSON.stringify({ 'BEAllotmentID': beallotmentid }),
                                success: function(data) {
                                    var obj = data.d;
                                    $('.sksnobox').val(obj[0].BE_ALLOT_SKSNO);
                                    $('.patraregnobox').val(obj[0].BE_ALLOT_PATRAREGNO);
                                    $('.depositdatebox').val(obj[0].BE_ALLOT_DEPOSITDATE_STR);
                                    $('.clientnamebox').val(obj[0].BE_ALLOT_CLIENTNAME);
                                    $('.withdrawdatebox').val(obj[0].BE_ALLOT_WITHDRAWDATE_STR);
                                    if (obj[0].BE_ALLOT_IDCARD == 'Y')
                                        $('.docidcardbox').prop('checked', true);
                                    if (obj[0].BE_ALLOT_BOOKBANK == 'Y')
                                        $('.docbookbankbox').prop('checked', true);
                                    if (obj[0].BE_ALLOT_OTHER != '') {
                                        $('.docotherbox').prop('checked', true);
                                        $('.docothertextbox').val(obj[0].BE_ALLOT_OTHER);
                                    }
                                    $('.benobox').val(obj[0].BE_ALLOT_NO)
                                    $('.refnobox').val(obj[0].BE_ALLOT_REF_NO);
                                    $('.beamountbox').val(obj[0].BE_ALLOT_AMT);
                                    $('.confirmdatebox').val(obj[0].BE_COMF_DATE_STR);
                                    $('.settledatebox').val(obj[0].BE_SETTLE_DATE_STR);
                                    $('.unitbox').val(obj[0].BE_ALLOT_UNIT);
                                    $('.yieldbox').val(obj[0].BE_ALLOT_YIELD);
                                    $('.flagbox').val(obj[0].BE_FLAG);
                                    $('.remarkbox').val(obj[0].BE_ALLOT_COMMENT);
                                    $('.allotdealnobox').val(obj[0].BE_ALLOT_DEAL_NO);
                                    $("input[name=OwnerRadio][value=" + obj[0].BE_OWNER + "]").prop('checked', true);

                                    var a = "";
                                    $('.buyer').children('option:not(:first)').remove();
                                    $('.buyer option:first').remove();
                                    a = '<option value="' + obj[0].CUSTOMER.CustomerID + '">' + obj[0].CUSTOMER.NameEn + '</option>';
                                    $('.buyer').append(a);

                                    var b = "";
                                    $('.incvat').children('option:not(:first)').remove();
                                    $('.incvat option:first').remove();
                                    b = b + '<option value="' + obj[0].CUSTOMER.IncVat + '">' + obj[0].CUSTOMER.CustomerID + '</option>';
                                    $('.incvat').append(b);
                                },
                                error: function(xhr, ajaxOptions, thrownError) {
                                    var err = JSON.parse(xhr.responseText);
                                    alert(err.Message);
                                }
                            });
                        },
                        close: function() {
                            $(this).dialog("close");
                            $('.sksnobox').val("");
                            $('.patraregnobox').val("");
                            $('.clientnamebox').val("");
                            $('.depositdatebox').val("");
                            $('.withdrawdatebox').val("");

                            $('.docidcardbox').removeAttr('checked');
                            $('.docbookbankbox').removeAttr('checked');
                            $('.docotherbox').removeAttr('checked');
                            $('.docothertextbox').val('');

                            $('.benobox').val("");
                            $('.refnobox').val("");
                            $('.beamountbox').val("");
                            $('.confirmdatebox').val("");
                            $('.settledatebox').val("");
                            $('.unitbox').val("");
                            $('.yieldbox').val("");
                            $('.flagbox').val("");
                            $('.remarkbox').val("");
                            $('.allotdealnobox').val("");
                            
                            

                            $('.buyer').children('option:not(:first)').remove();
                            $('.buyer option:first').remove();

                            $('.incvat').children('option:not(:first)').remove();
                            $('.incvat option:first').remove();

                        },
                        "buttons": {
                            "Submit": function() {
                                var flag = "";
                                var result = true;
                                var depositdate = "";
                                var withdrawdate = "";
                                var idcard, bookbank, other, othertext = "";
                                let message = "";

                                if ($('.docidcardbox').is(':checked'))
                                    idcard = "Y";
                                if ($('.docbookbankbox').is(':checked'))
                                    bookbank = "Y";
                                if ($('.docotherbox').is(':checked'))
                                    othertext = $('.docothertextbox').val();
                                    
                                if ($.datepicker.parseDate('dd/mm/yy', $('.settledatebox').val()) >= $.datepicker.parseDate('dd/mm/yy', matdate)) {
                                    message += "* The Settlement Date must come first the Maturity Date <br />";
                                    // alert('The Settlement Date must come first the Maturity Date  ');
                                    result = false;
                                }else if($('.settledatebox').val() != ""){
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('.settledatebox').val()) }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                message += "* Settlement Date : " + data.d.Message + "<br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }
                                
                                if ($('.depositdatebox').val() != "") {
                                    depositdate = $.datepicker.parseDate('dd/mm/yy', $('.depositdatebox').val());
                                }
                                if ($('.withdrawdatebox').val() != "") {
                                    withdrawdate = $.datepicker.parseDate('dd/mm/yy', $('.withdrawdatebox').val());
                                }
                                
                                if($('.benobox').val() == "")
                                {
                                    message += "* B/E No is require!! <br />";
                                    result = false;
                                }else if($('.benobox').val() != ""){
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/duplicateBENo',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'beno': $('.benobox').val(), 'allotid': beallotmentid }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                // alert(data.d.Message);
                                                message += "* " + data.d.Message + " <br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }
                                
                                if($('.beamountbox').val() == "")
                                {
                                    message += "* B/E Amount is require!! <br />";
                                    result = false;
                                }
//                                if($('.refnobox').val() == "")
//                                {
//                                    message += "* Ref No is require!! <br />";
//                                    result = false;
//                                }
                                if($('.yieldbox').val() == "")
                                {
                                    message += "* Yield is require!! <br />";
                                    result = false;
                                }
                                if($('.buyer').val() == "")
                                {
                                    message += "* Buyer is require!! <br />";
                                    result = false;
                                }
                                
                                if($('.confirmdatebox').val() == "")
                                {
                                    message += "* Confirmation Date is require!! <br />";
                                    result = false;
                                }
                                else if($('.confirmdatebox').val() != "")
                                {
                                    $.ajax({
                                        type: 'POST',
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/validateHoliday',
                                        contentType: 'application/json; charset=utf-8',
                                        dataType: 'json',
                                        async: false,
                                        data: JSON.stringify({ 'date': $.datepicker.parseDate('dd/mm/yy', $('.confirmdatebox').val()) }),
                                        success: function(data){
                                            if(!data.d.Success)
                                            {
                                                message += "* Confirmation Date : " + data.d.Message + "<br />";
                                                result = false;
                                            }
                                        }
                                    });
                                }
                                
                                if($('.unitbox').val() == "")
                                {
                                    message += "* Unit is require!! <br />";
                                    result = false;
                                }
                                
                                if (result == true) {
                                    var record = {
                                        BE_ALLOT_NO: $('.benobox').val(),
                                        BE_ALLOT_REF_NO: $('.refnobox').val(),
                                        BE_ALLOT_AMT: $('.beamountbox').val(),
                                        BE_COMF_DATE: $.datepicker.parseDate('dd/mm/yy', $('.confirmdatebox').val()),
                                        BE_SETTLE_DATE: $.datepicker.parseDate('dd/mm/yy', $('.settledatebox').val()),
                                        BE_ALLOT_UNIT: $('.unitbox').val(),
                                        BE_ALLOT_YIELD: $('.yieldbox').val(),
                                        BE_BUYER_ID: $('.buyer').val(),
                                        BE_BUYER_INCVAT: $('.incvat').val(),
                                        BE_FLAG: $('.flagbox').val(),
                                        BE_ALLOT_COMMENT: $('.remarkbox').val(),
                                        BE_MAT_DATE: $.datepicker.parseDate('dd/mm/yy', matdate),
                                        BE_ALLOT_DEAL_NO: $('.allotdealnobox').val(),
                                        BE_ALLOT_SKSNO: $('.sksnobox').val(),
                                        BE_ALLOT_PATRAREGNO: $('.patraregnobox').val(),
                                        BE_ALLOT_DEPOSITDATE: depositdate,
                                        BE_ALLOT_CLIENTNAME: $('.clientnamebox').val(),
                                        BE_ALLOT_WITHDRAWDATE: withdrawdate,
                                        BE_ALLOT_IDCARD: idcard,
                                        BE_ALLOT_BOOKBANK: bookbank,
                                        BE_ALLOT_OTHER: othertext,
                                        BE_OWNER: $('input[name=OwnerRadio]:checked').val(),
                                        BEALLOTMENTID: beallotmentid
                                    };
                                    var request = $.ajax({
                                        type: "POST",
                                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/UpdateAllotment',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        data: JSON.stringify({ 'record': record }),
                                        success: function(data) {
                                            if (data.d.Result == "OK") {
                                                alert("Update Allotment Complete");
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
                                }
                                else
                                {
                                    _Dialog(message);
                                }
                            },
                            "Close": function() {
                                $(this).dialog("close");
                                $('.sksnobox').val("");
                                $('.patraregnobox').val("");
                                $('.clientnamebox').val("");
                                $('.depositdatebox').val("");
                                $('.withdrawdatebox').val("");

                                $('.benobox').val("");
                                $('.refnobox').val("");
                                $('.beamountbox').val("");
                                $('.confirmdatebox').val("");
                                $('.settledatebox').val("");
                                $('.unitbox').val("");
                                $('.yieldbox').val("");
                                $('.flagbox').val("");
                                $('.remarkbox').val("");
                                $('.allotdealnobox').val("");
                               

                                $('.buyer').children('option:not(:first)').remove();
                                $('.buyer option:first').remove();

                                $('.incvat').children('option:not(:first)').remove();
                                $('.incvat option:first').remove();

                            }
                        }
                    });                
                }

                return false;
            });
            $(document).on("click", ".deletedetail", function(e) {
                var beallotmentid = $(this).attr('alt');
                if (confirm('Are you sure you want to delete?')) {
                    $.ajax({
                        type: "POST",
                        url: '<%=Config.Website %>/BE/frm_BESeries.aspx/DeleteAllotment',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ 'BEAllotmentID': beallotmentid }),
                        success: function(data) {
                            if (data.d.Result == "OK") {
                                alert("Delete Allotment Complete");
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
                }
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server"> 
    <div id="dialogdetail" style="display:none">
        <table width="100%">
            <tr>
                <td>SKS No</td>
                <td>
                    <input type="text" name="SKSNoBox" id="SKSNoBox" class="sksnobox" runat="server" /> 
                </td>
                <td>Patra RegNo.</td>
                <td>
                    <input type="text" name="PatraRegNoBox" id="PatraRegNoBox" class="patraregnobox" runat="server" /> 
                </td>
                <td>Deposit Date</td>
                <td>
                    <input type="text" name="DepositDateBox" id="DepositDateBox" class="depositdatebox" runat="server" autocomplete="off" /> 
                </td>
            </tr>
            <tr>
                <td>Client Name</td>
                <td colspan="3">
                    <input type="text" name="ClientNameBox" id="ClientNameBox" class="clientnamebox" size="70" runat="server" /> 
                </td>
                <td>Withdraw Date</td>
                <td>
                    <input type="text" name="WithdrawDateBox" id="WithdrawDateBox" class="withdrawdatebox" runat="server" autocomplete="off" /> 
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <div style="float:left;">
                    <input type="checkbox" name="DocIDCardBox" id="DocIDCardBox" class="docidcardbox" runat="server" />ID Card
                    </div>
                    <div style="float:left;margin-left: 50px; ">
                    <input type="checkbox" name="DocBookBankBox" id="DocBookBankBox" class="docbookbankbox" runat="server" />Book Bank
                    </div>
                    <div style="float:left;margin-left: 50px; ">
                    <input type="checkbox" name="DocOtherBox" id="DocOtherBox" class="docotherbox" runat="server" />Other
                    <input type="text" name="DocOtherTextBox" id="DocOtherTextBox" class="docothertextbox" runat="server" /> 
                    </div>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>B/E No</td>
                <td>
                    <input type="text" name="BENoBox" id="BENoBox" class="benobox" runat="server" /> <font color="red">*</font>
                </td>
                <td>B/E Amount</td>
                <td>
                    <input type="text" name="BEAmountBox" id="BEAmountBox" class="beamountbox" runat="server" /> <font color="red">*</font>
                </td>
                <td>Ref No.</td>
                <td>
                    <input type="text" name="DealNoBox" id="Text1" class="refnobox" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Buyer</td>
                <td colspan="3">
                    <input type="text" name="BuyerBox" id="BuyerBox" class="buyerbox" runat="server" /> 
                    <img id="btn-customer" name="btn-customer" style="float:left;cursor:pointer; padding: 2px; border: 1px solid black; background-color: Blue;" src="<%=Config.Website %>/Theme/icon/icon-search.png" /> 
                    <select id="buyeridbox" class="buyer" runat="server"></select> <font color="red">*</font>
                    <select id="incvatbox" class="incvat" style="display:none" runat="server"></select>
                </td>
                <td>Deal No.</td>
                <td><input type="text" name="AllotDealNoBox" id="AllotDealNoBox" class="allotdealnobox" runat="server" /> </td>
            </tr>
            <tr>
                <td>Confirmation Date</td>
                <td>
                    <input type="text" name="ConfirmationDate" id="ConfirmationDate" class="confirmdatebox" size="40" runat="server" autocomplete="off" /> <font color="red">*</font>
                </td>
                <td>Settlement Date</td>
                <td colspan="2">
                    <input type="text" name="SettlementDateBox" id="SettlementDateBox" class="settledatebox" runat="server" autocomplete="off" />
                </td>
                <td >
                    <input type="radio"  name="OwnerRadio" class="ownerradio" value="KK"  checked="checked"/> KK &nbsp;&nbsp;
                    <input type="radio"  name="OwnerRadio" class="ownerradio" value="OT" /> Other
                </td>
            </tr>
            <tr>
                <td>Unit</td>
                <td>
                    <input type="text" name="UnitBox" id="UnitBox" class="unitbox" size="40" runat="server" /> <font color="red">*</font>
                </td>
                <td>Yield(%)</td>
                <td colspan="3">
                    <input type="text" name="YieldBox" id="YieldBox" class="yieldbox" runat="server" /><font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <select name="FlagBox" id="FlagBox" class="flagbox" runat="server">
                        <option value="">Use</option>
                        <option value="D">Cancel</option>
                    </select>
                </td>
                <td>Remark</td>
                <td>
                    <input type="text" name="RemarkBox" ID="RemarkBox" class="remarkbox" runat="server" />                    
                </td>
            </tr>
        </table>  
    </div>
    <div id="dialogdetailAval" style="display:none">
        <table width="100%">
            <tr>
                <td>SKS No</td>
                <td>
                    <input type="text" name="AvalSKSNoBox" id="AvalSKSNoBox" class="AvalSKSNoBox" runat="server" /> 
                </td>
                <td>Patra RegNo.</td>
                <td>
                    <input type="text" name="AvalPatraRegNoBox" id="AvalPatraRegNoBox" class="AvalPatraRegNoBox" runat="server" /> 
                </td>
                <td>Deposit Date</td>
                <td>
                    <input type="text" name="AvalDepositDateBox" id="AvalDepositDateBox" class="AvalDepositDateBox" runat="server" autocomplete="off" /> 
                </td>
            </tr>
            <tr>
                <td>Client Name</td>
                <td colspan="3">
                    <input type="text" name="AvalClientNameBox" id="AvalClientNameBox" class="AvalClientNameBox" size="70" runat="server" /> 
                </td>
                <td>Withdraw Date</td>
                <td>
                    <input type="text" name="AvalWithdrawDateBox" id="AvalWithdrawDateBox" class="AvalWithdrawDateBox" runat="server" autocomplete="off" /> 
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="5">
                    <div style="float:left;">
                    <input type="checkbox" name="AvalDocIDCardBox" id="Avaldocidcardbox" class="Avaldocidcardbox" runat="server" />ID Card
                    </div>
                    <div style="float:left;margin-left: 50px; ">
                    <input type="checkbox" name="AvalDocBookBankBox" id="Avaldocbookbankbox" class="Avaldocbookbankbox" runat="server" />Book Bank
                    </div>
                    <div style="float:left;margin-left: 50px; ">
                    <input type="checkbox" name="AvalDocOtherBox" id="Avaldocotherbox" class="Avaldocotherbox" runat="server" />Other
                    <input type="text" name="AvalDocOtherTextBox" id="Avaldocothertextbox" class="Avaldocothertextbox" runat="server" /> 
                    </div>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>PN No</td>
                <td>
                    <input type="text" name="AvalBENoBox" id="AvalBENoBox" class="AvalBENoBox" runat="server" /> <font color="red">*</font>
                </td>
                <td>PN Amount</td>
                <td>
                    <input type="text" name="AvalBEAmountBox" id="AvalBEAmountBox" class="AvalBEAmountBox" runat="server" style="width:135px;" /> <font color="red">*</font>
                </td>
                <td>Ref No.</td>
                <td>
                    <input type="text" name="AvalRefNoBox" id="AvalRefNoBox" class="AvalRefNoBox" runat="server" />
                </td>
                <td>Deal No.</td>
                <td><input type="text" name="AvalAllotDealNoBox" id="AvalAllotDealNoBox" class="AvalAllotDealNoBox" runat="server" /> </td>
            </tr>
        </table>  
        <fieldset style="border-width: 2px; border-style: groove; border-color: threedface; padding:5px;">
            <legend><strong>SELLER</strong></legend>
            <table width="100%">
                <tr>
                    <td>Seller</td>
                    <td colspan="5">
                        <input type="text" name="avalSellerBox" id="avalSellerBox" runat="server" />
                        <img id="btnAvalSellerSearch" name="btnAvalSellerSearch" style="float:left;cursor:pointer; padding: 2px; border: 1px solid black; background-color: Blue;" src="<%=Config.Website %>/Theme/icon/icon-search.png" />
                        <select id="avalseller" runat="server"></select>
                        <select id="HideAvalSellerBank" style="display:none"></select>
                        <select id="HideAvalSellerAccount" style="display:none"></select>
                    </td>
                </tr>
                <tr>
                    <td>Seller Bank</td>
                    <td>
                        <input type="text" name="AvalSellerBank" id="AvalSellerBank" autocpmplete="off" />
                    </td>
                    <td>Seller Account</td>
                    <td>
                        <input type="text" name="AvalSellerAccount" id="AvalSellerAccount" autocpmplete="off" />
                    </td>
                </tr>
                <tr>
                    <td>Confirmation Date</td>
                    <td>
                        <input type="text" name="AvalSellerConfirmationDate" id="AvalSellerConfirmationDate" size="40" runat="server" autocomplete="off" />
                    </td>
                    <td>Settlement Date</td>
                    <td colspan="2">
                        <input type="text" name="AvalSellerSettlementDateBox" id="AvalSellerSettlementDateBox" runat="server" autocomplete="off" />
                    </td>
                    <td>Yield(%)</td>
                    <td colspan="3">
                        <input type="text" name="AvalSellerYieldBox" id="AvalSellerYieldBox" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset style="border-width: 2px; border-style: groove; border-color: threedface;  padding:5px;">
            <legend><strong>BUYER</strong></legend>
            <table width="100%">
                <tr>
                    <td>Buyer</td>
                    <td colspan="5">
                        <input type="text" name="AvalBuyerBox" id="AvalBuyerBox" class="AvalBuyerBox" runat="server" /> 
                        <img id="Avalbtn-customer" name="Avalbtn-customer" style="float:left;cursor:pointer; padding: 2px; border: 1px solid black; background-color: Blue;" src="<%=Config.Website %>/Theme/icon/icon-search.png" /> 
                        <select id="Avalbuyer" class="Avalbuyer" runat="server"></select>
                        <select id="Avalincvat" class="Avalincvat" style="display:none" runat="server"></select>
                        <select id="HideAvalBuyerBank" style="display:none"></select>
                        <select id="HideAvalBuyerAccount" style="display:none"></select>
                    </td>
                </tr>
                <tr>
                    <td>Buyer Bank</td>
                    <td>
                        <input type="text" name="AvalBuyerBank" id="AvalBuyerBank" autocpmplete="off" />
                    </td>
                    <td>Buyer Account</td>
                    <td>
                        <input type="text" name="AvalBuyerAccount" id="AvalBuyerAccount" autocpmplete="off" />
                    </td>
                </tr>
                <tr>
                    <td>Confirmation Date</td>
                    <td>
                        <input type="text" name="AvalConfirmationDate" id="AvalConfirmationDate" class="AvalConfirmationDate" size="40" runat="server" autocomplete="off" />
                    </td>
                    <td>Settlement Date</td>
                    <td colspan="2">
                        <input type="text" name="AvalSettlementDateBox" id="AvalSettlementDateBox" class="AvalSettlementDateBox" runat="server" autocomplete="off" />
                    </td>
                    <td>Yield(%)</td>
                    <td colspan="3">
                        <input type="text" name="AvalYieldBox" id="AvalYieldBox" class="AvalYieldBox" runat="server" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <table width="100%">
            <tr>
                <td>Unit</td>
                <td>
                    <input type="text" name="AvalUnitBox" id="AvalUnitBox" class="AvalUnitBox" size="40" runat="server" />
                </td>
                <td>
                    <select name="AvalFlagBox" id="AvalFlagBox" class="AvalFlagBox" runat="server">
                        <option value="">Use</option>
                        <option value="D">Cancel</option>
                    </select>
                </td>
                <td>Remark</td>
                <td>
                    <input type="text" name="AvalRemarkBox" ID="AvalRemarkBox" class="AvalRemarkBox" runat="server" />                    
                </td>
                <td >
                    <input type="radio"  name="AvalOwnerRadio" class="AvalOwnerRadio" value="KK"  checked="checked"/> KK &nbsp;&nbsp;
                    <input type="radio"  name="AvalOwnerRadio" class="AvalOwnerRadio" value="OT" /> Other
                </td>
            </tr>
        </table>
    </div>
    <div id="dialog" style="display: none;">
        <table>
            <tr>
                <td>B/E Series</td>
                <td>
                    <input type="text" name="BESeriesBox" id="BESeriesBox" class="seriescodebox" runat="server" /> <font color="red">*</font>
                </td>
                <td>Total Amount</td>
                <td>
                    <input type="text" name="TotalAmtBox" id="TotalAmtBox" class="totalamtbox" runat="server" /> <font color="red">*</font>
                </td>
                <td>DealNo</td>
                <td>
                    <input type="text" name="DealNoBox" id="DealNoBox" class="dealnobox" runat="server" /> <font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td>Issuer</td>
                <td>
                    <input type="text" name="IssuerBox" id="IssuerBox" class="issuerbox" runat="server" />  
                    <img id="btn-issuer" name="btn-issuer" style="float:left;cursor:pointer; padding: 2px; border: 1px solid black; background-color: Blue;" src="<%=Config.Website %>/Theme/icon/icon-search.png" />
                </td>
                <td colspan="4">
                    <select id="xissuer" class="issuer" runat="server"></select> <font color="red">*</font>
                    <select id="xissuerBank" style="display:none" class="xissuerbank"></select>
                    <select id="xissuerAcct" style="display:none" class="xissueracct"></select>
                </td>
            </tr>
            <tr>
                <td>Issuer Bank</td>
                <td>
                    <input type="text" name="IssuerBankBox" id="IssuerBankBox" class="issuerbankbox" runat="server" />
                </td>
                <td>Issuer Acct</td>
                <td>
                    <input type="text" name="IssuerAcctBox" id="IssuerAcctBox" class="issueracctbox" runat="server" /> <font color="red">*</font>
                </td>
                <td>Discount Rate</td>
                <td>
                    <input type="text" id="DiscountRateBox" name="DiscountRateBox" class="disratebox" runat="server" />(%) <font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td>Issue Date</td>
                <td>
                    <input type="text" name="IssueDateBox" id="IssueDateBox" class="issuedatebox" size="40" runat="server" autocomplete="off" /> <font color="red">*</font>
                </td>
                <td>Maturity Date</td>
                <td colspan="3">
                    <input type="text" name="MaturityDateBox" id="MaturityDateBox" class="matdatebox" runat="server" autocomplete="off" />
                </td>                
            </tr>
            
        </table>  
    </div>
    <div id="dialogAval" style="display: none;">
        <table>
            <tr>
                <td>PN Series</td>
                <td>
                    <input type="text" name="avalBESeriesBox" id="avalBESeriesBox" runat="server" /> <font color="red">*</font>
                </td>
                <td>Total Amount</td>
                <td>
                    <input type="text" name="avalTotalAmtBox" id="avalTotalAmtBox" runat="server" /> <font color="red">*</font>
                </td>
                <td>DealNo</td>
                <td>
                    <input type="text" name="avalDealNoBox" id="avalDealNoBox" runat="server" /> <font color="red">*</font>
                </td>
            </tr>
            <tr>
                <td>Issuer</td>
                <td>
                    <input type="text" name="avalIssuerBox" id="avalIssuerBox" runat="server" />  
                    <img id="btnAvalIssuerSearch" name="btnAvalIssuerSearch" style="float:left;cursor:pointer; padding: 2px; border: 1px solid black; background-color: Blue;" src="<%=Config.Website %>/Theme/icon/icon-search.png" />
                </td>
                <td colspan="4">
                    <select id="avalissuer" runat="server"></select> <font color="red">*</font>
                    <select id="avalxissuerbank" style="display:none"></select>
                    <select id="avalxissueracct" style="display:none"></select>
                </td>
            </tr>
           <%-- <tr>
                <td>Seller</td>
                <td>
                    <input type="text" name="avalSellerBox" id="avalSellerBox" runat="server" />
                    <img id="btnAvalSellerSearch" name="btnAvalSellerSearch" style="float:left;cursor:pointer; padding: 2px; border: 1px solid black; background-color: Blue;" src="<%=Config.Website %>/Theme/icon/icon-search.png" />
                </td>
                <td colspan="4">
                    <select id="avalseller" runat="server"></select><font color="red">*</font>
                </td>
            </tr>--%>
            <%--<tr>
                <td>Issuer Bank</td>
                <td>
                    <input type="text" name="avalIssuerBankBox" id="avalIssuerBankBox" runat="server" />
                </td>
                <td>Issuer Acct</td>
                <td>
                    <input type="text" name="avalIssuerAcctBox" id="avalIssuerAcctBox" runat="server" /> <font color="red">*</font>
                </td>
                <td>Discount Rate</td>
                <td>
                    <input type="text" name="avalDiscountRateBox" id="avalDiscountRateBox" runat="server" />(%) <font color="red">*</font>
                </td>
            </tr>--%>
            <tr>
                <td>Issue Date</td>
                <td>
                    <input type="text" name="avalIssueDateBox" id="avalIssueDateBox" size="40" runat="server" autocomplete="off" /> <font color="red">*</font>
                </td>
                <td>Maturity Date</td>
                <td colspan="3">
                    <input type="text" name="avalMaturityDateBox" id="avalMaturityDateBox" runat="server" autocomplete="off" />
                </td>                
            </tr>
            
        </table>  
    </div>
    <div id="dialogmessage" style="display: none">
        <%--<asp:Label ID="MessageBox" runat="server"></asp:Label>--%>
        <div style="text-align:left;margin-bottom: 20px;margin-top: 10px;">
            <span id="dialogContent">Please wait...</span>
        </div>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">B/E Series</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0">
	                    <tbody>            	                        	            
	                        <tr>
	                            <td class="td-display">B/E Serires</td>
	                            <td class="td-field">
	                                <asp:TextBox ID="BESeriesSearchBox" Runat="server"></asp:TextBox></td>
	                        </tr>       	                        	            
	                        <tr>
	                            <td class="td-display">Maturity Date</td>
	                            <td class="td-field">
	                                <asp:TextBox ID="MatFromDateBox" Runat="server" autocomplete="off"></asp:TextBox>-
	                                <asp:TextBox ID="MatToDateBox" Runat="server" autocomplete="off"></asp:TextBox></td>
	                        </tr>
	                        <tr>
	                            <td class="td-display">Issue Date</td>
	                            <td class="td-field">
	                                <asp:TextBox ID="IssueFormDateBox" Runat="server" autocomplete="off"></asp:TextBox>-
	                                <asp:TextBox ID="IssueToDateBox" Runat="server" autocomplete="off"></asp:TextBox></td>
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
			        <div style="float:left"><button class="round blue button-submit ic-add">Add B/E Series</button></div>
			        <div style="float:left;margin-left:5px;"><button id="btnAddAval" class="round blue button-submit" style="background-image: url(../Theme/Default/images/ss_template/icons/ic_add.png);">Add B/E Series Aval</button></div>
			        <div style="float:right"></div>
			        <br /><br />			    	
                    <asp:GridView ID="GridBESeries" CellPadding="3" CellSpacing="3" DataKeyNames="BESERIESID" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                <div><button class="round blue button-submit runningrefno"  value="<%# DataBinder.Eval(Container.DataItem, "BESERIESID") %>">RefNo.T1</button></div>
                                    &nbsp;
                                <div><button class="round blue button-submit runningrefno2"  value="<%# DataBinder.Eval(Container.DataItem, "BESERIESID") %>">RefNo.T2 (new)</button></div>   
                                </ItemTemplate>                               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-edit.png" class="editseries" alt="<%# DataBinder.Eval(Container.DataItem, "BESERIESID") %>||<%# DataBinder.Eval(Container.DataItem, "BE_AVAL_TYPE") %>" style="cursor:pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-delete.png" class="deleteseries" alt="<%# DataBinder.Eval(Container.DataItem, "BESERIESID") %>" style="cursor:pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img class="btnshowdetail" src="../Theme/icon/icon-tracking.png" style="cursor:pointer"/>
                                    <asp:Panel ID="AllotmentDetail" runat="server" Style="display:none">
                                        <asp:GridView ID="GridBEAllotment" Width="100%" CellPadding="0" CellSpacing="0" DataKeyNames="BEALLOTMENTID" CssClass="GridViewStyle" AllowPaging="False" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <img src="../Theme/icon/actions-edit.png" class="editdetail" alt="<%# DataBinder.Eval(Container.DataItem, "BEALLOTMENTID") %>||<%# DataBinder.Eval(Container.DataItem, "BE_AVAL_TYPE") %>" title="<%# DataBinder.Eval(Container.DataItem, "BE_MAT_DATE_STR") %>" style="cursor:pointer" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <img src="../Theme/icon/actions-delete.png" class="deletedetail" alt="<%# DataBinder.Eval(Container.DataItem, "BEALLOTMENTID") %>" style="cursor:pointer" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BE_ALLOT_PATRAREGNO" ItemStyle-HorizontalAlign="Center" HeaderText="PATRA No." /> 
                                                <asp:BoundField DataField="BE_ALLOT_NO" ItemStyle-HorizontalAlign="Center" HeaderText="No" /> 
                                                <asp:BoundField DataField="BE_ALLOT_AMT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HeaderText="Amount" /> 
                                                <asp:BoundField DataField="SELLER_NAME" ItemStyle-HorizontalAlign="Center" HeaderText="Seller" />
                                                <asp:BoundField DataField="DISPLAY_SELLER_COMF_DATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="ConfirmDate" />
                                                <asp:BoundField DataField="DISPLAY_SELLER_SETTLE_DATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="SettleDate" />
                                                <asp:BoundField DataField="BE_SELLER_ALLOT_YIELD" ItemStyle-HorizontalAlign="Center" HeaderText="Yield" DataFormatString="{0:n4}" /> 
                                                <asp:BoundField DataField="BUYER_NAME" ItemStyle-HorizontalAlign="Center" HeaderText="Buyer" />
                                                <%--<asp:BoundField DataField="BE_COMF_DATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="ConfirmDate" /> 
                                                <asp:BoundField DataField="BE_SETTLE_DATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderText="SettleDate" /> --%>
                                                <asp:BoundField DataField="DISPLAY_BUYER_COMF_DATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="ConfirmDate" /> 
                                                <asp:BoundField DataField="DISPLAY_BUYER_SETTLE_DATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Left" HeaderText="SettleDate" /> 
                                                <asp:BoundField DataField="BE_ALLOT_UNIT" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:n2}" HeaderText="Unit" /> 
                                                <asp:BoundField DataField="BE_ALLOT_YIELD" ItemStyle-HorizontalAlign="Center" HeaderText="Yield" DataFormatString="{0:n4}" /> 
                                                <asp:BoundField DataField="BE_ALLOT_REF_NO" ItemStyle-HorizontalAlign="Center" HeaderText="Ref No." /> 
                                                <asp:BoundField DataField="BE_ALLOT_DEAL_NO" ItemStyle-HorizontalAlign="Center" HeaderText="Deal No." /> 
                                                <asp:BoundField DataField="BE_OWNER" ItemStyle-HorizontalAlign="Center" HeaderText="Owner" /> 
                                            </Columns>                    
                                            <EmptyDataTemplate>
                                                No Data
                                            </EmptyDataTemplate>                    
                                            <RowStyle CssClass="RowStyle" />
                                            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <HeaderStyle CssClass="HeaderStyle"/>
                                            <EditRowStyle CssClass="EditRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="BE_SERIES_CODE" ItemStyle-HorizontalAlign="Center" HeaderText="Series" /> 
                            <asp:BoundField DataField="BE_TOTAL_AMT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HeaderText="Amount" /> 
                            <asp:BoundField DataField="BE_ISSUE_DATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="IssueDate" /> 
                            <asp:BoundField DataField="BE_MAT_DATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="MaturityDate" /> 
                            <asp:BoundField DataField="BE_ISSUER_NAME" ItemStyle-HorizontalAlign="Left" HeaderText="Issuer" /> 
                            <asp:BoundField DataField="BE_ISSUER_BANK" ItemStyle-HorizontalAlign="Left" HeaderText="Bank" /> 
                            <asp:BoundField DataField="BE_ISSUER_ACCT" ItemStyle-HorizontalAlign="Center" HeaderText="Account" /> 
                            <asp:BoundField DataField="BE_DIS_RATE" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n4}" HeaderText="Rate" /> 
                            <asp:BoundField DataField="BE_SERIES_REMAIN" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HeaderText="Remain" /> 
                            <asp:BoundField DataField="BE_SERIES_PROCEED" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HeaderText="Proceed Amt" /> 
                            <asp:BoundField DataField="BE_SERIES_DEAL_NO" ItemStyle-HorizontalAlign="Center" HeaderText="DealNo" />                            
                            <asp:TemplateField>
                                <ItemTemplate><button class="round blue button-submit ic-adddetail" value="<%# DataBinder.Eval(Container.DataItem, "BESERIESID") %>" alt="<%# DataBinder.Eval(Container.DataItem, "BE_MAT_DATE_STR") %>||<%# DataBinder.Eval(Container.DataItem, "BE_AVAL_TYPE") %>">Add Lot</button></ItemTemplate>
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
