<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR0142SWIFTREC.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR0142SWIFTREC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {

            $('#ctl00_bodyContent_DateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            $('#ctl00_bodyContent_txtFileDate').datepicker({
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
			<h3 class="fl">Reconcile SWIFT data & OPICS data</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">	
		    <div style="float:right">
		        <span>Business Process : </span>
		        <asp:HyperLink ID="ProcessImportSwift" NavigateUrl="~/Swift/frm_ImportSwift.aspx" Text="1.Import Swift File" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		        <asp:HyperLink ID="ProcessRecSwift" NavigateUrl="~/Form/frm_TR0142SWIFTREC.aspx" Text="2.Reconcile OPICS with SWIFT" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;		        
		    </div>	
	        <div>&nbsp;</div>
			<div class="half-size-column fl">
					<fieldset>
					    <div style="padding: 5px">
						    <div style="float:left;width:100px;">Data Date</div>
						    <div>
						        <asp:TextBox ID="DateBox" runat="server"></asp:TextBox>
						    </div>
						</div>
						<div style="padding: 5px">
						 <div style="float:left;width:100px;">Swift Type</div>
						    <div>
						        <asp:DropDownList ID="SwiftType" style="font: 12pt tahoma" runat="server">
						            <asp:ListItem Text="fin.103" Value="fin.103"></asp:ListItem>
							        <asp:ListItem Text="fin.202" Value="fin.202"></asp:ListItem>
							        <asp:ListItem Text="fin.202.COV" Value="fin.202.COV"></asp:ListItem>
							        <asp:ListItem Text="fin.300" Value="fin.300"></asp:ListItem>							        
							    </asp:DropDownList>
						    </div>							
						</div>						
						<div>
					        <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                                class="round blue button-submit" onclick="btnSubmit_Click" />
					        <div id="divMsg" style="display:none;">
                                <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                            </div>    
						</div>
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
