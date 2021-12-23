<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_DebentureBE.aspx.cs" Inherits="TreasuryShadowSystem.Debenture.frm_DebentureBE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function() {

            $('#ctl00_bodyContent_txtMaturityDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'yy-mm-dd',
                changeMonth: true,
                changeYear: true
            });          
            

            $('#ctl00_bodyContent_btnSubmit').click(function() {
                //$(this).attr('disabled', 'disabled');
                if (BeforeSubmit()) {
                    $('#divMsg').show();
                    return true;
                }
            });
        });

        function BeforeSubmit() {
            if ($('#ctl00_bodyContent_txtBranch').val() == "")
                return false;
                
            if ($('#ctl00_bodyContent_txtPN').val() == "")
                return false;                
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">
				
	    <div class="content-module-heading cf">
		
			<h3 class="fl">Debenture Series</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> 
		
		<div id="criteria-content" class="content-module-main cf">
	
			<div class="half-size-column fl">
				
					<fieldset>
						<p>
							<label for="simple-input">SECID</label>
							<asp:TextBox ID="txtsecid" runat="server" placeholder="e.g. KK21205Y" CssClass="round input-form-textbox"></asp:TextBox>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtsecid" Text="*" runat="server"></asp:RequiredFieldValidator>
							
					        <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                                class="round blue button-submit" onclick="btnSubmit_Click" />
					        <div id="div4" style="display:none;">
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



