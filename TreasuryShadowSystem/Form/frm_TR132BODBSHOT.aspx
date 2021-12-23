<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR132BODBSHOT.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR132BODBSHOT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
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
            
            $('#ctl00_bodyContent_GenFromDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });    
            $('#ctl00_bodyContent_GenToDateBox').datepicker({
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
			<h3 class="fl">Debenture Export to SEC</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
				
		<div id="Div1" class="content-module-main cf">
		    <div>
		        <fieldset style="border: 1px solid">
		            <legend style="padding: 5px; background-color: Blue; font: 10pt tahoma; font-weight:bold; color:white">Step 1. Generate Internal Ref.</legend>
		            <br />
		            <div style="color:red">*กรุณาเลือกเฉพาะช่วง Trade Date ที่จะส่ง กลต.*</div>
                    <table width="620" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td width="50px">&nbsp;</td>
			                <td width="50">Date</td>
			                <td>
			                   <asp:TextBox ID="GenFromDateBox" Runat="server"></asp:TextBox> - <asp:TextBox ID="GenToDateBox" Runat="server"></asp:TextBox>  
					            &nbsp;
					            <asp:Button ID="GenInternalRefButton" Text="Generate Internal Ref." runat="server" onclick="GenInternalRefButton_Click" />
			                </td>
			            </tr>
                    </table>  
		        </fieldset>
		        
		    </div class="half-size-column fl">
		</div>
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset style="border: 1px solid">
					    <legend style="padding: 5px; background-color: Blue; font: 10pt  tahoma; font-weight:bold; color:white">Step 2. Generate File</legend>					        
		                    <div style="color:red">*กรุณาเลือกเฉพาะช่วง Trade Date ที่จะส่ง กลต. และตรงกับด้านบน*</div>
					        <table>
					            <tr>
					                <td>&nbsp;</td>
					                <td>&nbsp;</td>
					                <td> <div>
					                    <asp:RadioButtonList ID="ChoiceBox" RepeatDirection="Horizontal" 
                                            CellPadding="0" CellSpacing="0" Width="500" runat="server" >
					                        <asp:ListItem Text="Excel" Value="Excel" Selected="True"></asp:ListItem>
					                        <asp:ListItem Text="Cover" Value="Cover"></asp:ListItem>
					                    </asp:RadioButtonList></div>
					                </td>
					            </tr>
					            <tr>
					                <td width="30">&nbsp;</td>
					                <td width="50">Date</td>
					                <td>
					                  <asp:TextBox ID="FromDateBox" Runat="server"></asp:TextBox> - <asp:TextBox ID="ToDateBox" Runat="server"></asp:TextBox>  	
					                </td>
					            </tr>
					            <tr>
					                <td width="30">&nbsp;</td>
					                <td width="50">Security</td>
					                <td>
					                   <asp:TextBox ID="SecurityBox" Enabled="false" runat="server"></asp:TextBox></td>
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
	
			<div class="half-size-column fl">
			
                <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="450px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		
	</div> <!-- end content-module -->
</asp:Content>
