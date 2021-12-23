<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR136BOBESERIES.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR136BOBESERIES" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
<script type="text/javascript">
//         $(document).ready(function() {
//         $('#ctl00_bodyContent_MatFromDateBox').datepicker({
//                altFormat: 'dd/mm/yy',
//                dateFormat: 'dd/mm/yy',
//                changeMonth: true,
//                changeYear: true
//            });      
//         });
    $(function(){
        $('#ctl00_bodyContent_BEDatetxt').datepicker({
            altFormat: 'dd/mm/yy',
            dateFormat: 'dd/mm/yy',
            changeMonth: true,
            changeYear: true
        });
        
        $("#dialog").dialog({ 
            draggable: false,
            modal: true,
            autoOpen: false, 
            closeOnEscape: false /*,
            open: function(event, ui){
                $(".ui-dialog-titlebar-close", ui.dialog | ui).show();
            }*/
        });
        
        $('#ctl00_bodyContent_btnSubmit').click(function(e, bypass){
            if(!bypass)
            {
                e.preventDefault();
                let BEReportSelectedValue = $('#ctl00_bodyContent_BEReportDDL').val();
                if(BEReportSelectedValue == "" || BEReportSelectedValue == null || BEReportSelectedValue == undefined || BEReportSelectedValue == "-")
                {
                    $('#dialogContent').text("Please select report");
                    $('#dialog').dialog('open');
                }
                else
                {
                    $('#ctl00_bodyContent_btnSubmit').trigger('click', true);
                }
            }
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
			<h3 class="fl">BE Report</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
					        <table>
					            <tr>
					                <td class="td-display">BE Report</td>
					                <td class="td-field">
					                    <asp:DropDownList ID="BEReportDDL" runat="server" AutoPostBack="true"
                                            onselectedindexchanged="BEReportDDL_SelectedIndexChanged"></asp:DropDownList>
					                </td>
					            </tr>
					            <tr>
					                <td class="td-display">BE Copy</td>
					                <td class="td-field">
					                    <asp:DropDownList ID="BECopyDDL" runat="server"></asp:DropDownList>
					                </td>
					            </tr>
					            <tr>
					                <td class="td-display">BE Category</td>
					                <td class="td-field">
                                        <asp:DropDownList ID="BECategoryDDL" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
					                </td>
					            </tr>
					            <tr>
					                <td class="td-display">Series</td>
					                <td class="td-field">
					                    <asp:TextBox ID="BESeriestxt" runat="server"></asp:TextBox>
					                </td>
					            </tr>
					            <tr>
					                <td class="td-display">
					                    <asp:Label ID="BEDatelbl" runat="server">Maturity Date</asp:Label>
                                    </td>
                                    <td class="td-field">
                                        <asp:TextBox ID="BEDatetxt" runat="server" autocomplete="off"></asp:TextBox>
                                    </td>
					            </tr>
					            <tr>
					                <td>&nbsp;</td>
					                <td colspan="2">
					                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                                            class="round blue button-submit" onclick="btnSubmit_Click" /></td>
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
		
			<div>
			
			     <div style="float:left;margin:10px;"><asp:Button ID ="ViewPassword" runat="server" Text="View Password" 
                    onclick="ViewPassword_Click" /></div>
                    
                 <div style="float:left;margin:10px;"><asp:Button ID="SendSaleConfirmation" runat="server" Text="Send Mail" 
                    onclick="SendConfirmation_Click" /></div>                  

                <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="450px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
	    
	    <div id="dialog" title="Info" style="display:none;">
            <div style="text-align:center;margin-bottom: 20px;margin-top: 10px;">
                <span id="dialogContent">Please wait...</span>
            </div>
        </div>
	</div> <!-- end content-module -->
</asp:Content>
