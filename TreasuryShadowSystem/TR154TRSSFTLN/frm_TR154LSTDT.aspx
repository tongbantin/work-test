<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR154LSTDT.aspx.cs" Inherits="TreasuryShadowSystem.TR154TRSSFTLN.frm_TR154LSTDT" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link href="../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>

    <script src="../Scripts/jquery-ui-1.10.0.custom.min.js" type="text/javascript"></script>
    <style type="text/css">
        input.inputLong { width: 400px !important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module-main">
    <div class="site-container">
        <div class="filtering" id="dvFiltering" style="height: 35px;">
             <form id="Form2" method="post" action='@Url.Action("DS_LTX", "ExportCSV")'  target="TheNewWindow"  style="clear:both;float:left;">
                Value Date
                <input type="text" name="datasetdate" id="txt_valuedate" value="" class="round input-form-textbox datadate"/>
                Maturity Date
                <input type="text" name="datasetdate" id="txt_matdate" value="" class="round input-form-textbox datadate"/>                
                <button type="submit" id="LoadRecordsButton2" class="round blue button-submit">Load records</button>
            </form>  
      
        </div>              
    </div>
</div>

    <div id="SoftLoanTableContainer"></div>
    <button id="DeleteAllMarkls">Delete All Selected Marks</button>
    <script type="text/javascript">
 

        $(document).ready(function() {

            $('#txt_valuedate,#txt_matdate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            //Prepare jtable plugin
            $('#SoftLoanTableContainer').jtable({
                title: 'SoftLoan Transaction',
                paging: true,
                pageSize: 10,
                sorting: true,
                defaultSorting: 'CTPYCODE ASC',
                selecting: true, //Enable selecting
                multiselect: true, //Allow multiple selecting
                selectingCheckboxes: true, //Show checkboxes on first column
                actions: {
                    listAction: '<%=Config.Website %>/TR154TRSSFTLN/frm_TR154LSTDT.aspx/SoftLoanList'
                    //,createAction: '<%=Config.Website %>/TR154TRSSFTLN/frm_TR154LSTDT.aspx/Create'
                    //,updateAction: '<%=Config.Website %>/TR154TRSSFTLN/frm_TR154LSTDT.aspx/Update'
                    , deleteAction: '<%=Config.Website %>/TR154TRSSFTLN/frm_TR154LSTDT.aspx/Delete'
                },
                /*toolbar: {
                items: [
                {
                icon: '../Images/import-icon17x17.png',
                text: '<a href="#" class="export">Generate File</a>',
                click: function() {
                var requestUri = '<%=Config.Website %>/TR154TRSSFTLN/frm_TR154LSTDT.aspx/GenerateFile';
                alert($('#SoftLoanTableContainer').jtable('selectedRows'));
                $.ajax({
                type: "POST",
                url: requestUri,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ LISTID: $('#SoftLoanTableContainer').jtable('selectedRows') }),
                dataType: "json",
                success: function(data) {
                if (data.Result == "ERROR")
                alert(data.Message.toString());
                else alert(data.Result.toString());
                }
                });
                }
                }
                ]
                },*/
                fields: {
                    RECID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    RECNO: {
                        title: 'Rec No.',
                        list: false
                    },
                    CTPYCODE: {
                        title: 'Couterparty',
                        list: false
                    },
                    BOOKNO: {
                        title: 'Book No.',
                        list: false
                    },
                    CUSTNAME: {
                        title: 'Customer' 
                    },
                    DEALNO: {
                        title: 'DEALNO' 
                    },
                    LOANTYPE: {
                        title: 'Loan Type',
                        list: false
                    },
                    PDEALNO: {
                        title: 'P-DealNo',
                        list: false
                    },
                    REFERPROJ: {
                        title: 'REFERPROJ',
                        list: false
                    },
                    REFERDIV: {
                        title: 'REFERDIV',
                        list: false
                    },
                    PDEALAMT: {
                        title: 'PDEALAMT',
                        display: function(data) {
                            return Number(data.record.PDEALAMT).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                        }
                    },
                    PPAYAMT: {
                        title: 'PPAYAMT',
                        display: function(data) {
                            return Number(data.record.PPAYAMT).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                        }
                    },
                    AMOUNT: {
                        title: 'AMOUNT',
                        display: function(data) {
                            return Number(data.record.AMOUNT).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
                        }

                    },
                    INTRATE: {
                        title: 'INTRATE'
                    },
                    BOOKDATE: {
                        title: 'BOOKDATE',
                        type: 'date',
                        dateFormat: 'dd/mm/yyyy'
                    },
                    VALUEDATE: {
                        title: 'VALUEDATE',
                        type: 'date',
                        dateFormat: 'dd/mm/yyyy'
                    },
                    MATDATE: {
                        title: 'MATDATE',
                        type: 'date',
                        dateFormat: 'dd/mm/yyyy'
                    },
                    PDEALDATE: {
                        title: 'PDEALDATE',
                        type: 'date',
                        dateFormat: 'dd/mm/yyyy'
                    },
                    PVALUEDATE: {
                        title: 'PVALUEDATE',
                        type: 'date',
                        dateFormat: 'dd/mm/yyyy'
                    },
                    PMATDATE: {
                        title: 'PMATDATE',
                        type: 'date',
                        dateFormat: 'dd/mm/yyyy'
                    }
                },
                //Register to selectionChanged event to hanlde events
                selectionChanged: function() {
                    //Get all selected rows
                    //                    var $selectedRows = $('#SoftLoanTableContainer').jtable('selectedRows');

                    //                    $('#SelectedRowList').empty();
                    //                    if ($selectedRows.length > 0) {
                    //                        //Show selected rows
                    //                        $selectedRows.each(function() {
                    //                            var record = $(this).data('record');
                    //                            $('#SelectedRowList').append(
                    //                            '<b>StudentId</b>: ' + record.StudentId +
                    //                            '<br /><b>Name</b>:' + record.Name + '<br /><br />'
                    //                            );
                    //                        });
                    //                    } else {
                    //                        //No rows selected
                    //                        $('#SelectedRowList').append('No row selected! Select rows to see here...');
                    //                    }
                }
            });

            //$('#SoftLoanTableContainer').jtable('load');
            $('#SoftLoanTableContainer').jtable('load', {
            txt_valuedate: $('#txt_valuedate').val(),
            txt_matdate: $('#txt_matdate').val()
            });

            $('#DeleteAllMarkls').button().click(function() {
                var $selectedRows = $('#SoftLoanTableContainer').jtable('selectedRows');
                $('#SoftLoanTableContainer').jtable('deleteRows', $selectedRows);
            });
            
        });

        $('#LoadRecordsButton2').click(function(e) {
            e.preventDefault();
            $('#SoftLoanTableContainer').jtable('load', {
            txt_valuedate: $('#txt_valuedate').val(),
            txt_matdate: $('#txt_matdate').val(),
                fiarrangmentnumber: $('#fiarrangmentnumber').val(),
                transtype: $('#transtype').val(),
                NA: $('#na:checked').val()
            });
        });
        $('#LoadRecordsButton2').click();

       
        
    </script>
</asp:Content>
