<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TROREPO01.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TROREPO01" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    
    <script type="text/javascript">

        $(document).ready(function() {
            //            var d = new Date();
            //            var yy = d.getFullYear();
            //            var mm = 100 + d.getMonth() + 1;
            //            //var dd = d.getDate();
            //            var dd = '06';
            //            var currentDate = yy + '-' + mm.toString().substring(1, 3) + '-' + dd;


            $('#ctl00_bodyContent_txtReportDate').datepicker({
                altFormat: 'yy/mm/dd',
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true
            });

            $('#ctl00_bodyContent_btnSubmit').click(function() {
                //$(this).attr('disabled', 'disabled');
                $('#divMsg').show();
                return true;
            });
            //$("#ctl00_bodyContent_txtReportDate").val(currentDate);

//            $("div.content-module-heading.cf").click(function() {
//                if ($('#criteria-content').is(':visible')==true)
//                    $('#criteria-content').animate({ height: 'hide' });
//                else
//                    $('#criteria-content').animate({ height: 'show' });
//            });

//            $("div.content-module-heading-result.cf").click(function() {
//                if ($('#result-content').is(':visible') == true)
//                    $('#result-content').animate({ height: 'hide' });
//                else
//                    $('#result-content').animate({ height: 'show' });
//            });
        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">

    
    
    <div class="content-module">
				
	    <div class="content-module-heading cf">
		
			<h3 class="fl">REPO Report</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
			
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
	
			<div  class="half-size-column fl">
			
				
				
					<fieldset>
					
						<p>
							<label for="simple-input">Report Date</label>
							<input type="text" id="txtReportDate" runat="server" class="round input-textbox-date" />
							
					        <asp:Button ID="btnSubmit" Text="Submit" runat="server" onclick="btnSubmit_Click" class="round blue button-submit" />
					        <div id="divMsg" style="display:none;">
                                <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                            </div>    
						</p>
						
						
					</fieldset>
				
				    
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf">
	
			<div class="half-size-column fl">
			
                <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="500px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
	
	
						
</asp:Content>
