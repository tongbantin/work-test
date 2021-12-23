<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR108DBOEMIR.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR108DBOEMIR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
<script type="text/javascript">
    $(document).ready(function() {

        $('#ctl00_bodyContent_ExecuteDateBox').datepicker({
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
			<h3 class="fl">EMIR</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>		
					    <table>
					        <tr>
					            <td>Branch</td>
					            <td><asp:TextBox ID="BranchBox" Text="01" runat="server"></asp:TextBox></td>
					        </tr>
					        <tr>
					            <td>Execute Date</td>
					            <td><input id="ExecuteDateBox" size="15" runat="server" class="round input-form-textbox" /> </td>
					        </tr>
					        <tr>
					            <td>Customer Group</td>
					            <td><asp:DropDownList ID="CustomerGroupBox" AutoPostBack="true" runat="server" 
                                        class="round input-form-textbox" 
                                        onselectedindexchanged="CustomerGroupBox_SelectedIndexChanged"></asp:DropDownList></td>
					        </tr>
					        <tr>
					            <td>Customer</td>
					            <td><asp:DropDownList ID="CustomerBox" runat="server" class="round input-form-textbox"></asp:DropDownList></td>
					        </tr>
					        <tr>
					            <td>&nbsp;</td>
					            <td><asp:Button ID="btnSubmit" Text="Submit" runat="server" class="round blue button-submit" onclick="btnSubmit_Click" /></td>
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
			
                <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="450px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
