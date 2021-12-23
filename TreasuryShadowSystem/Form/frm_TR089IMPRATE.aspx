<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR089IMPRATE.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR089IMPRATE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">    
    <script type="text/javascript">
        $(document).ready(function() {

            $('#ctl00_bodyContent_DateBox').datepicker({
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
			<h3 class="fl">Mark-To-Market : Import Rate</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
		    <div style="float:right">
		        <span>Business Process : </span>
		        <asp:HyperLink ID="ProcessConfigSchedule" NavigateUrl="~/Form/frm_TR089CONFMTM.aspx" Text="1.Configuration Schedule" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		        <asp:HyperLink ID="ProcessImportRate" NavigateUrl="~/Form/frm_TR089IMPRATE.aspx" Text="2.Import Rate" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		        <asp:HyperLink ID="ProcessGenerateFile" NavigateUrl="~/Form/frm_TR089OTHMTM.aspx" Text="3.Generate Files" CssClass="round blue button-submit" runat="server"></asp:HyperLink>
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
						    <div style="float:left;width:100px;">Rate Time</div>
						    <div>
						        <asp:DropDownList ID="RoundBox" style="font: 12pt tahoma" runat="server">
							        <asp:ListItem Text="Morning" Value="01"></asp:ListItem>
							        <asp:ListItem Text="Evening" Value="02"></asp:ListItem>
							    </asp:DropDownList>
						    </div>
						</div>
					    <div style="padding: 5px">
						    <div style="float:left;width:100px;">Rate File</div>
						    <div>
						        <asp:FileUpload ID="FileRate" runat="server" />
						    </div>
						</div>
						<div style="float:left;width:100px; ">
						    <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                                class="round blue button-submit" onclick="btnSubmit_Click" />
					        <div id="divMsg" style="display:none;">
                                <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                            </div>   
						</div>
					</fieldset>
			
			</div> <!-- end half-size-column -->
		    <div style="float:right">
		        Sample File : <asp:HyperLink ID="TemplateFile" Text="Snap02.xls" CssClass="round blue button-submit"  NavigateUrl="~/Template/Snap02.xls" runat="server"></asp:HyperLink>
		    </div>
		</div> <!-- end content-module-main -->		
		
		
	</div> <!-- end content-module -->
</asp:Content>
