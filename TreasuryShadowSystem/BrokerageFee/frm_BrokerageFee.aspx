<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_BrokerageFee.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_BrokerageFee" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link href="../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {

            $('#dealdate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('#ctl00_bodyContent_FromDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('#ctl00_bodyContent_ToDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module-main">
        <div class="site-container">
            <div class="filtering" id="dvFiltering2">
                <table>
                    <tr>
                        <td>&nbsp;</td>
                        <td>DealNo.</td>
                        <td><input type="text" name="dealno" id="dealno"/> </td>
                        <td>DealDate</td>
                        <td><input type="text" name="dealdate" id="dealdate" class="round input-form-textbox"/></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>Broker</td>
                        <td><select id="brok" name="brok" class="brok" runat="server"></select></td>
                        <td>Product Type</td>
                        <td>
                            <select name="prodtype" id="prodtype" class="prodtype" runat="server">
                            </select> 
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="4"><button type="submit" id="SearchButton">Search</button></td>
                    </tr>
               </table>
            </div>
        </div>
    </div>
    <div id="BrokerageFeeTableContainer" style="width: 1000px;"></div><br />
    <button type="button" id="NoFeeButton" class="round blue button-submit">No Fee</button>
    <script type="text/javascript">

        $(document).ready(function() {

            //Prepare jtable plugin
            $('#BrokerageFeeTableContainer').jtable({
                title: 'Brokerage Fee Deal',
                paging: true,
                pageSize: 10,
                sorting: true,
                defaultSorting: 'DEALNO ASC',
                selecting: true, //Enable selecting
                multiselect: true, //Allow multiple selecting
                selectingCheckboxes: true, //Show checkboxes on first column
                //selectOnRowClick: false, //Enable this to only select using checkboxes
                actions: {
                    listAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFee.aspx/BrokerageFeeList',
                    createAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFee.aspx/CreateBrokerageFee',
                    updateAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFee.aspx/UpdateBrokerageFee',
                    deleteAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFee.aspx/DeleteBrokerageFee'
                },
                fields: {
                    DEALFEEID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    DEALNO: {
                        title: 'DealNo'
                    },
                    PRODTYPE: {
                        title: 'ProdType'
                    },
                    DEALDATE: {
                        title: 'DealDate'
                    },
                    NOTIONAL: {
                        title: 'Notional(CCY).'
                    },
                    CCY: {
                        title: 'CCY'
                    },
                    BROK: {
                        title: 'Brok',
                        options: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFee.aspx/BrokerList'
                    },
                    DURATION: {
                        title: 'Duration'
                    },
                    FEE: {
                        title: 'Fee'
                    },
                    FREEFEE: {
                        title: 'No Fee',
                        options: [{ Value: '0', DisplayText: 'None' }, { Value: '1', DisplayText: 'Free'}]
                    }
                },
                //Initialize validation logic when a form is created
                formCreated: function(event, data) {
                    data.form.children(':lt(5)').wrapAll('<div class="col1"/>');
                    data.form.children(':gt(0)').wrapAll('<div class="col2"/>');
                    data.form.parents('.ui-dialog').css('width', '1000px');
                    var divwidth = data.form.parents('.ui-dialog').width();
                    var left = ($(document).width() - parseFloat(divwidth)) / 2;
                    data.form.parents('.ui-dialog').css('left', left);

                    data.form.find('input[name="PRODTYPE"]').attr('readonly', 'true');
                    data.form.find('input[name="DEALDATE"]').attr('readonly', 'true');
                    data.form.find('input[name="NOTIONAL"]').attr('readonly', 'true');
                    data.form.find('input[name="CCY"]').attr('readonly', 'true');

                    data.form.find('input[name="PRODTYPE"]').css('background-color', 'LightGray');
                    data.form.find('input[name="DEALDATE"]').css('background-color', 'LightGray');
                    data.form.find('input[name="NOTIONAL"]').css('background-color', 'LightGray');
                    data.form.find('input[name="CCY"]').css('background-color', 'LightGray');

                    data.form.validationEngine();
                },
                rowUpdated: function(event, data) {
                    $('#BrokerageFeeTableContainer').jtable('reload');
                }
            });

            $('#SearchButton').click(function(e) {
                e.preventDefault();
                $('#BrokerageFeeTableContainer').jtable('load', {
                    dealno: $('#dealno').val(),
                    dealdate: $('#dealdate').val(),
                    brok: $('.brok').val(),
                    prodtype: $('.prodtype').val()
                });
            });

        });

        $('#NoFeeButton').click(function(e) {
            CallBrokerageFee('NoFeeBrokerageFee');
        });
        
        function CallBrokerageFee(NoorSet) {
            var $selectedRows = $('#BrokerageFeeTableContainer').jtable('selectedRows');
            var rec = "";
            if ($selectedRows.length > 0) {
                $selectedRows.each(function() {
                    var record = $(this).data('record');
                    rec = rec + ' ' + record.DEALFEEID;
                });
            }
            rec = rec.trim().split(' ').join(',');
            $.ajax({
                type: "POST",
                url: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFee.aspx/' + NoorSet,
                data: '{DealFee: "' + rec + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    if (data.d.Result == "ERROR") {
                        alert('Invalid Operation');
                    } else {
                        $('#BrokerageFeeTableContainer').find('input[type=checkbox]').prop('checked', false);
                        $('#BrokerageFeeTableContainer  .jtable-row-selected').removeClass('jtable-row-selected');
                        $('#BrokerageFeeTableContainer').jtable('reload');
                    }
                }
            });
        }
    </script>
    <br />
    <div>        
        <fieldset style="padding: 10px; margin: 10px;">
            <legend style="background-color: Blue; color: White; font: 10pt tahoma bold;padding: 3px;">Re-generate Fee</legend>
            Execute Date
            <input type="text" name="FromDate" id="FromDate" runat="server" class="round input-form-textbox"/> - <input type="text" name="ToDate" id="ToDate" runat="server" class="round input-form-textbox"/> 
            <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                class="round blue button-submit" onclick="btnSubmit_Click"/>
        </fieldset>
    </div>
</asp:Content>
