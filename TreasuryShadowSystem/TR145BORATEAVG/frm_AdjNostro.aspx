<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_AdjNostro.aspx.cs" Inherits="TreasuryShadowSystem.TR145BORATEAVG.frm_AdjNostro" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link href="../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
    <style>
    .jtable-input-readonly {
        background-color: #EEEEEE;
    }
    </style>
    <script type="text/javascript">
        $(document).ready(function() {
            $('#ctl00_bodyContent_FromDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            $('#ctl00_bodyContent_ToDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Payment Report (FI,EQ,SWAP)</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
					        <table>
	                             <tr>
	                                <td class="td-display">&nbsp;Account</td>
	                                <td class="td-field">       
    	                                <asp:DropDownList ID="AccountBox" CssClass="account" runat="server"></asp:DropDownList>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">Date</td>
	                                <td class="td-field">           	                                
	                                    <asp:TextBox ID="FromDateBox" cssclass="fromdate" runat="server" ></asp:TextBox>-
	                                    <asp:TextBox ID="ToDateBox" cssclass="todate" runat="server" ></asp:TextBox>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">Status</td>
	                                <td class="td-field"> 
	                                    <input type="radio" name="status" id="allstatus" value="" class="allstatus" /> All &nbsp;
	                                    <input type="radio" name="status" id="activestatus" value="True" checked="checked" class="activestatus" /> Active &nbsp;
	                                    <input type="radio" name="status" id="inactivestatus" value="False" class="inactivestatus" /> InActive &nbsp;
	                                </td>
	                            </tr>
					            <tr>
					                <td>&nbsp;</td>
					                <td colspan="2">
					                    <asp:Button ID="SearchButton" Text="Submit" CssClass="round blue button-submit searchbutton" runat="server" />
                                        </td>
					            </tr>
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
			
               <div id="AdjustNostroContainer"></div>
                <script type="text/javascript">

                    $(document).ready(function() {

                        //Prepare jtable plugin
                        $('#AdjustNostroContainer').jtable({
                            title: 'Adjust Nostro',
                            paging: true,
                            pageSize: 10,
                            sorting: true,
                            defaultSorting: 'POSTDATE DESC',
                            actions: {
                                listAction: '<%=Config.Website %>/TR145BORATEAVG/frm_AdjNostro.aspx/AdjNostroList',
                                createAction: '<%=Config.Website %>/TR145BORATEAVG/frm_AdjNostro.aspx/CreateAdjNostro',
                                updateAction: '<%=Config.Website %>/TR145BORATEAVG/frm_AdjNostro.aspx/UpdateAdjNostro',
                                deleteAction: '<%=Config.Website %>/TR145BORATEAVG/frm_AdjNostro.aspx/DeleteAdjNostro'
                            },
                            fields: {
                                ADJNOSTROID: {
                                    key: true,
                                    create: false,
                                    edit: false,
                                    list: false
                                },
                                REFNO: {
                                    title: 'Ref No.',
                                    create: false
                                },
                                ACCTCODEID: {
                                    title: 'Acct Description',
                                    inputClass: 'validate[required]',
                                    options: '<%=Config.Website %>/TR145BORATEAVG/frm_AdjNostro.aspx/GetAccountCode',
                                    width: 200
                                },
                                ADJACCTCODEID: {
                                    title: 'Adjust Acct.',
                                    inputClass: 'validate[required]',
                                    options: '<%=Config.Website %>/TR145BORATEAVG/frm_AdjNostro.aspx/GetAdjAccountCode',
                                    width: 200
                                },
                                SETTDATE: {
                                    title: 'Statement Date',
                                    inputClass: 'validate[required]',
                                    displayFormat: 'dd-M-yy',
                                    type: 'date',
                                    width: 100
                                },
                                POSTDATE: {
                                    title: 'Opics Date',
                                    inputClass: 'validate[required]',
                                    displayFormat: 'dd-M-yy',
                                    type: 'date',
                                    width: 100
                                },
                                DRCR: {
                                    title: 'BUY/SELL',
                                    options: { 'BUY': 'BUY', 'SELL': 'SELL' },
                                    inputClass: 'validate[required]',
                                    width: 100
                                },
                                AMOUNT: {
                                    title: 'Amount',
                                    inputClass: 'validate[required]',
                                    width: 100
                                },
                                DESCRIPTION: {
                                    title: 'Remark',
                                    width: 100
                                },
                                CREATEDATE: {
                                    title: 'Create Date',
                                    type: 'date',
                                    list: false,
                                    add: false
                                },
                                CREATEBY: {
                                    title: 'Create',
                                    add: false
                                },
                                REVIEWDATE: {
                                    title: 'Verify Date',
                                    type: 'date',
                                    list: false,
                                    add: false
                                },
                                REVIEWBY: {
                                    title: 'Verify',
                                    list: false,
                                    add: false
                                },
                                AUTHORIZEDATE: {
                                    title: 'Authorize Date',
                                    type: 'date',
                                    list: false,
                                    add: false
                                },
                                AUTHORIZEBY: {
                                    title: 'Authorize',
                                    list: false,
                                    add: false
                                },
                                CANCELDAY: {
                                    title: 'Cancel Opics Trans.',
                                    options: { 'N': 'Addition', 'Y': 'Cancel Opics' },
                                    defaultValue: 'N'
                                },
                                ENABLED: {
                                    title: 'Status',
                                    options: { '0': 'InActive', '1': 'Active' },
                                    defaultValue: '1'
                                }
                                /*,PrintButton: {
                                    title: '',
                                    create: false,
                                    edit: false,
                                    display: function(data) {
                                        var $img = $('<img src="<%=Config.Website %>/Theme/icon/printfile.png" class="imgprint" />');
                                        $(".imgprint").click(function() {
                                            var adjnostroid = data.record.ADJNOSTROID;
                                            var request = $.ajax({
                                                type: "POST",
                                                url: '<%=Config.Website %>/TR145BORATEAVG/frm_AdjNostro.aspx/PrintAdjNostro',
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                data: JSON.stringify({ 'ADJNOSTROID': adjnostroid }),
                                                success: function(data) {
                                                    $('#iReport').attr('src', '../View/Viewer.aspx');
                                                    $('.dialogreport').dialog({ title: 'ธนาคารเกียรตินาคิน จำกัด(มหาชน)', modal: true, width: 1000 });
                                                },
                                                error: function(xhr, ajaxOptions, thrownError) {
                                                    var err = JSON.parse(xhr.responseText);
                                                    alert(err.Message);
                                                }
                                            });
                                        });
                                        return $img;
                                    },
                                    width: '1%'
                                }*/
                            },
                            //Initialize validation logic when a form is created
                            formCreated: function(event, data) {
                                $('button').addClass("round blue button-submit");
                                $('#EditDialogSaveButton').css('visibility', 'visible');
                                $('#EditDialogSaveButton').text('Submit');
                                if (data.formType == 'edit') {
                                    $('#Edit-REFNO').attr('disabled', 'disabled');
                                    $('#Edit-REFNO').addClass('jtable-input-readonly');
                                    $('#Edit-SETTDATE').attr('disabled', 'disabled');
                                    $('#Edit-SETTDATE').addClass('jtable-input-readonly');
                                    $('#Edit-POSTDATE').attr('disabled', 'disabled');
                                    $('#Edit-POSTDATE').addClass('jtable-input-readonly');
                                    $('#Edit-DRCR').attr('disabled', 'disabled');
                                    $('#Edit-DRCR').addClass('jtable-input-readonly');
                                    $('#Edit-AMOUNT').attr('disabled', 'disabled');
                                    $('#Edit-AMOUNT').addClass('jtable-input-readonly');
                                    $('#Edit-DESCRIPTION').attr('disabled', 'disabled');
                                    $('#Edit-DESCRIPTION').addClass('jtable-input-readonly');
                                    $('#Edit-CREATEDATE').attr('disabled', 'disabled');
                                    $('#Edit-CREATEDATE').addClass('jtable-input-readonly');
                                    $('#Edit-CREATEBY').attr('readonly', 'true');
                                    $('#Edit-CREATEBY').addClass('jtable-input-readonly');
                                    $('#Edit-REVIEWDATE').attr('disabled', 'disabled');
                                    $('#Edit-REVIEWDATE').addClass('jtable-input-readonly');
                                    $('#Edit-REVIEWBY').attr('readonly', 'true');
                                    $('#Edit-REVIEWBY').addClass('jtable-input-readonly');
                                    $('#Edit-AUTHORIZEDATE').attr('disabled', 'disabled');
                                    $('#Edit-AUTHORIZEDATE').addClass('jtable-input-readonly');
                                    $('#Edit-AUTHORIZEBY').attr('readonly', 'true');
                                    $('#Edit-AUTHORIZEBY').addClass('jtable-input-readonly');

                                    $('#Edit-ACCTCODEID option').attr('disabled', 'disabled');
                                    $('#Edit-ACCTCODEID').addClass('jtable-input-readonly');
                                    $('#Edit-ADJACCTCODEID option').attr('disabled', 'disabled');
                                    $('#Edit-ADJACCTCODEID').addClass('jtable-input-readonly');
                                    $('#Edit-CANCELDAY option').attr('disabled', 'disabled');
                                    $('#Edit-CANCELDAY').addClass('jtable-input-readonly');
                                    $('#Edit-ENABLED option').attr('disabled', 'disabled');
                                    $('#Edit-ENABLED').addClass('jtable-input-readonly');

                                    if ($('#Edit-REVIEWBY').val() == '') {
                                        $('#EditDialogSaveButton').text('Verify');
                                    } else if ($('#Edit-AUTHORIZEBY').val() == '') {
                                        $('#EditDialogSaveButton').text('Authorize');
                                    } else {
                                        $('#EditDialogSaveButton').css('visibility', 'hidden');
                                    }
                                } else {
                                    $('#Edit-CREATEDATE').attr('readonly', 'true');
                                    $('#Edit-CREATEDATE').addClass('jtable-input-readonly');
                                    $('#Edit-CREATEBY').attr('readonly', 'true');
                                    $('#Edit-CREATEBY').addClass('jtable-input-readonly');
                                    $('#Edit-REVIEWDATE').attr('readonly', 'true');
                                    $('#Edit-REVIEWDATE').addClass('jtable-input-readonly');
                                    $('#Edit-REVIEWBY').attr('readonly', 'true');
                                    $('#Edit-REVIEWBY').addClass('jtable-input-readonly');
                                    $('#Edit-AUTHORIZEDATE').attr('readonly', 'true');
                                    $('#Edit-AUTHORIZEDATE').addClass('jtable-input-readonly');
                                    $('#Edit-AUTHORIZEBY').attr('readonly', 'true');
                                    $('#Edit-AUTHORIZEBY').addClass('jtable-input-readonly');
                                }
                                data.form.validationEngine();
                            }/*,                            
                            //Validate form when it is being submitted
                            formSubmitting: function (event, data) {
                                return data.form.validationEngine('validate');
                            },
                            //Dispose validation logic when form is closed
                            formClosed: function (event, data) {
                                //data.form.validationEngine('hide');
                                //data.form.validationEngine('detach');
                            }*/
                        });

                        //$('#AdjustNostroContainer').jtable('load');

                        $('.searchbutton').click(function(e) {
                            e.preventDefault();
                            var status = "";
                            if ($('.activestatus').is(':checked'))
                                status = "1";
                            else if ($('.inactivestatus').is(':checked'))
                                status = "0";
                            $('#AdjustNostroContainer').jtable('load', {
                                Account: $('.account').val(),
                                FromDate: $('.fromdate').val(),
                                ToDate: $('.todate').val(),
                                Status: status
                            });
                        });

                        //Load all records when page is first shown
                        $('.searchbutton').click();
                    });

                </script>
                <div class="dialogreport" style="display:none">		 
                    <iframe id="iReport" src="../View/Viewer.aspx"  width="100%" height="450px" runat="server" frameborder="0" marginwidth="1" ></iframe>	
	            </div>             
   
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
				
	</div> <!-- end content-module -->
    
</asp:Content>
