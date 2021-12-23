<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR149OBOFCDOUT.aspx.cs" Inherits="TreasuryShadowSystem.TR149OBOFCDOUT.frm_TR149OBOFCDOUT" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
<script type="text/javascript">
        $(document).ready(function() { 

            $('#ctl00_bodyContent_FromDateBox,#ctl00_bodyContent_ToDateBox').datepicker({
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
			<h3 class="fl">Pre-FX Payment Report</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
                            <table>      
	                             <tr>
	                                <td width="100px">Date</td>
	                                <td>           	                                
	                                    <asp:TextBox ID="FromDateBox"  Runat="server"></asp:TextBox>-
	                                    <asp:TextBox ID="ToDateBox"  Runat="server"></asp:TextBox>
	                                </td>
	                            </tr>   
	                             <tr>
	                                <td width="100px">&nbsp;</td>
	                                <td>           	                          
	                                    <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                                            class="round blue button-submit" onclick="btnSubmit_Click" />
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
                <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="500" runat="server" frameborder="0" marginwidth="1" ></iframe>
                
			</div>  
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
