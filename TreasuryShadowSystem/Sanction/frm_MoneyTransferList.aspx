<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_MoneyTransferList.aspx.cs" Inherits="TreasuryShadowSystem.Sanction.frm_MoneyTransferList" Title="Untitled Page" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link href="../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
    <script type="text/javascript">

        $(document).ready(function() {

            $('#execdate').datepicker({
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
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Search</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0">
	                    <tbody>              	                        	            
	                        <tr>
	                            <td>File</td>
	                            <td>
	                                <select id="fileload" name="fileload" class="fileload" runat="server"></select>
	                            </td>	                            
	                        </tr>         	                        	            
	                        <tr>
	                            <td>Trans No.</td>
	                            <td>
	                                <input type="text" id="transno" class="round input-form-textbox" />
	                            </td>	                            
	                        </tr>          	                        	            
	                        <tr>
	                            <td>Date</td>
	                            <td>
	                                <input type="text" id="execdate" size="15" class="round input-form-textbox" />
	                            </td>	                            
	                        </tr> 
	                        <tr>
	                            <td><label for="simple-input">Status</label>
                                </td>
	                            <td>
                                    <select id="status">
                                        <option value="">All</option>
                                        <option value="WAITING_TO_SEND">Sending to Sancion service</option>
                                        <option value="CHECKING_SANCTION">Checking Sanction</option>
                                        <option value="WAITING_TO_REVIEW">Please Review!!</option>
                                        <option value="APPROVE">Approved</option>
                                        <option value="DISAPPROVE">Disapproved</option>
                                        <option value="TRANSFER_TO_SWIFT">Passed</option>
                                    </select>
	                            </td>
	                        </tr>    
	                        <tr>
	                            <td style="text-align: left;">
	                                <button type="submit" id="LoadRecordsButton" class="round blue button-submit" >Search</button>
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
                <asp:Button ID="btnDownloadPass" runat="server" Text="Download Message Passed" Visible="true" onclick="btnDownloadPass_Click" />                   
                <asp:Button ID="btnDownloadSelected" runat="server" Text="Download Message By Selected" Visible="true" onclick="btnDownloadSelected_Click" />   
                <br />  
                <asp:HiddenField ID="SelectedRowList" runat="server" /> 
                <div id="SwiftTableContainer" style="width: 500px;"></div>
                <script type="text/javascript">

                    $(document).ready(function() {

                        //Prepare jtable plugin
                        $('#SwiftTableContainer').jtable({ 
                            paging: true,
                            pageSize: 10,
                            sorting: true,
                            selecting: true, //Enable selecting
                            multiselect: true, //Allow multiple selecting 
                            selectOnRowClick: true,
                            defaultSorting: 'REFNO',
                            actions: {
                                listAction: '<%=Config.Website %>/Sanction/frm_MoneyTransferList.aspx/MoneyTransferList' 
                            },
                            fields: {
                                PARTYMSGID: {
                                    key: true,
                                    create: false,
                                    edit: false,
                                    list: false                                    
                                },
                                REFNO: {
                                    title: 'Ref NO.',
                                    readonly: true
                                },
                                MSG: {
                                    title: 'Message', 
                                    type: 'textarea', 
                                    width: '500px',
                                    readonly: true
                                },
                                STATUS: {
                                    title: 'Status' ,
                                    options: { 'WAITING_TO_SEND': 'Sending'
                                        , 'CHECKING_SANCTION': 'Checking'
                                        , 'WAITING_TO_REVIEW': 'Please Review!!' 
                                        , 'APPROVE': 'Approved' 
                                        , 'DISAPPROVE': 'Disapproved' 
                                        , 'TRANSFER_TO_SWIFT': 'Passed' 
                                    },
                                    readonly: true
                                },
                                ShowDetailColumn: {
                                    title: '',
                                    display: function (data) { 
                                        if (data.record.STATUS == "WAITING_TO_REVIEW")
                                            return '<a href="<%=Config.Website %>/Sanction/frm_MessageMoneyTransfer.aspx?Msg=' + data.record.PARTYMSGID + '">Detail</a>';
                                        else
                                            return '';                                            
                                    }
                                },
                                ShowDownloadColumn: {
                                    title: '',
                                    display: function (data) { 
                                        if (data.record.STATUS == "APPROVE")
                                            return '<input type="checkbox" >';
                                        else
                                            return '';                                            
                                    }
                                }
                            },
                            selectionChanged: function () {  
                                                                 
                                //Get all selected rows
                                var $selectedRows = $('#SwiftTableContainer').jtable('selectedRows');
                 
                                $('#ctl00_bodyContent_SelectedRowList').val('');
                                if ($selectedRows.length > 0) {
                                    //Show selected rows                                    
                                    $selectedRows.each(function () {
                                        var record = $(this).data('record');  
                                        $('#ctl00_bodyContent_SelectedRowList').val($('#ctl00_bodyContent_SelectedRowList').val() + record.PARTYMSGID + ",");
                                    });
                                } else {
                                    //No rows selected
                                    $('#ctl00_bodyContent_SelectedRowList').value('');
                                }                                
                            }
                        });
                        
                        //Re-load records when user click 'load records' button.
                        $('#LoadRecordsButton').click(function (e) {
                            e.preventDefault();
                            $('#SwiftTableContainer').jtable('load', {
                                fileload: $('.fileload').val(),
                                transno: $('#transno').val(),
                                execdate: $('#execdate').val(),
                                status: $('#status').val()
                            });
                        });
                 
                        //Load all records when page is first shown
                        //$('#LoadRecordsButton').click();
                        
                        //$('#SwiftTableContainer').jtable('load');
                    });
                    
                    $('.jtable').addClass('GridViewStyle');
                </script>
			</div> <!-- end half-size-column -->
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->  
</asp:Content>
