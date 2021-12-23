<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_SanctionList.aspx.cs" Inherits="TreasuryShadowSystem.Sanction.frm_SanctionList" Title="Untitled Page" %>
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
                                        <option value="FAIL_TO_SEND">Fail to check Sanction</option>
                                        <option value="WAITING_TO_REVIEW">Waiting to review</option>
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
                <div id="SwiftTableContainer" style="width: 500px;"></div>
                <script type="text/javascript">

                    $(document).ready(function() {

                        //Prepare jtable plugin
                        $('#SwiftTableContainer').jtable({
                            title: 'SWIFT Message',
                            paging: true,
                            pageSize: 10,
                            sorting: true,
                            defaultSorting: 'REFNO',
                            actions: {
                                listAction: '<%=Config.Website %>/Sanction/frm_SanctionList.aspx/SWIFTList' 
                            },
                            fields: {
                                SWIFTMSGID: {
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
                                    readonly: true
                                },
                                ShowDetailColumn: {
                                    title: '',
                                    display: function (data) {
                                        return '<a href="<%=Config.Website %>/Sanction/frm_Message.aspx?Msg=' + data.record.SWIFTMSGID + '">Detail</a>';
                                    }
                                }
                            }
                        });
                        
                        //Re-load records when user click 'load records' button.
                        $('#LoadRecordsButton').click(function (e) {
                            e.preventDefault();
                            $('#SwiftTableContainer').jtable('load', {
                                transno: $('#transno').val(),
                                execdate: $('#execdate').val(),
                                status: $('#status').val()
                            });
                        });
                 
                        //Load all records when page is first shown
                        //$('#LoadRecordsButton').click();
                        
                        //$('#SwiftTableContainer').jtable('load');
                    });

                </script>
			</div> <!-- end half-size-column -->
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->  
</asp:Content>
