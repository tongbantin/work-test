<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR089OTHMTM.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR089OTHMTM" %>
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
    <script type="text/javascript">
        function showViewPassword(sender)
        {
            var btn = document.getElementById("viewPass");
            btn.style.display = sender.checked?'inline':'none';
        }
    
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Mark-To-Market : Generate Files</h3>
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
							<div style="float:left;width:100px;"><label for="simple-input">Time</label></div>
						    <div>
						        <asp:DropDownList ID="RoundBox" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="RoundBox_SelectedIndexChanged" >
                                    <asp:ListItem Text="Morning-Everyday" Value="01"></asp:ListItem>
                                    <asp:ListItem Text="Morning-EOM" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="Evening-Everyday" Value="03"></asp:ListItem>
                                    <asp:ListItem Text="Evening-Friday" Value="04"></asp:ListItem>
                                    <asp:ListItem Text="Evening-EOM" Value="05"></asp:ListItem>
							    </asp:DropDownList>
						    </div>
						</div>
						<div style="padding: 5px">
							<div style="float:left;width:100px;"><label for="simple-input">Fund</label></div>
						    <div>
						        <asp:DropDownList ID="MutualFundsBox" runat="server">
							    </asp:DropDownList>
						    </div>
						</div>
						<div style="padding: 5px;">
						    <div style="float:left;width:100px;">Send Email</div>
						    <div><asp:CheckBox ID="CheckSendMailBox" runat="server" onclick="showViewPassword(this)" /></div>
						    <div style="margin-left:100px">
						        <div>To : <asp:Label ID="ToMailBox" runat="server"></asp:Label></div>
						        <div>Cc : <asp:Label ID="CcMailBox" runat="server"></asp:Label></div>
						    </div>
						</div>
						<div id="viewPass" style="display:none;float:left;margin:10px;">
						    <asp:Button ID="btnViewPass" Text="View Password" runat="server" 
                                class="round blue button-submit" onclick="btnViewPass_Click" />
						</div>
						<div style="float:left;margin:10px;">
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
			    <fieldset>
				    <table border="0">
				        <tbody>
				           
				            <tr>
				                <td class="td-display"><asp:Label ID="lblParam2" Text="Data Date " runat="server"></asp:Label></td>
				                <td colspan="3" class="td-field-large">
				                    <input type="text" id="txtFileDate" size="15" runat="server" class="round input-form-textbox" />			                    
                                </td>
				            </tr>
				              <tr>
				                <td colspan="4" style="width:800px; text-align: left;">
				                    <asp:Button ID="btnSearch" Text="Search" runat="server" 
                                        class="round blue button-submit" onclick="btnSearch_Click" />
				                </td>
				            </tr>
				            
				        </tbody>
				    </table>
				</fieldset>
			    <br />
                <asp:GridView ID="GridData" width="500px" CssClass="GridViewStyle" runat="server" 
                    AutoGenerateColumns="False" AllowPaging="True">
                    <Columns>                          
                        <asp:TemplateField>
                            <HeaderTemplate>No.</HeaderTemplate>
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:HyperLinkField DataTextField="Filename" HeaderText="Filename" DataNavigateUrlFormatString="~/{0}/{1}" DataNavigateUrlFields="Path,Filename" />
                    </Columns>     
                    <EmptyDataTemplate>Data Not Found</EmptyDataTemplate>               
                    <RowStyle CssClass="RowStyle" />
                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
			    
			    <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="500px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
