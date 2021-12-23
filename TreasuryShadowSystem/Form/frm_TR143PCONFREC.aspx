<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR143PCONFREC.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR143PCONFREC" %>
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
<asp:Content ID="pconfrec" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="Div1" style="display: none">
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Preconfirmation SWIFT data & OPICS data</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="Div2" class="content-module-main cf">	
		    <div style="float:right">
		       <span>Business Process : </span>
		                            <asp:HyperLink ID="ProcessConf" NavigateUrl="~/Form/frm_TR143PCONFIG.aspx" Text="1.Configuration Format" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		                            <asp:HyperLink ID="AddPconf" NavigateUrl="~/Form/frm_TR143PCONF.aspx" Text="2.Add PreConfirmation" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		                            <asp:HyperLink ID="MatchPconf" NavigateUrl="~/Form/frm_TR143PCONFREC.aspx" Text="3.Match Data" CssClass="round blue button-submit" runat="server"></asp:HyperLink>
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
		
	    <div id="Div3" class="content-module-main cf">
	
			<div class="half-size-column fl">
			
                <iframe id="Iframe1" src="<%# TheUrl %>"  width="100%" height="450px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>

