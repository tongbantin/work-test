<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR089SENDMAIL.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR089SENDMAIL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $('#ctl00_bodyContent_txtFileDate').datepicker({
                altFormat: 'yymmdd',
                dateFormat: 'yymmdd',
                changeMonth: true,
                changeYear: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>Custodian</td>
                <td>
                    <asp:TextBox ID="CustodianView" runat="server" Width="400px" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Email</td>
                <td>
                    <asp:TextBox ID="EmailView" TextMode="MultiLine" Width="400px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Email(Cc)</td>
                <td>
                    <asp:TextBox ID="EmailCcView" TextMode="MultiLine" Width="400px" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>File Attach</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Mark-To-Market : Review & Send Mail</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
						<p>
							<label for="simple-input">Time</label>
							<asp:DropDownList ID="RoundBox" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="RoundBox_SelectedIndexChanged">
                                <asp:ListItem Text="Morning(Everyday)" Value="01"></asp:ListItem>
                                <asp:ListItem Text="Morning(EOM)" Value="02"></asp:ListItem>
                                <asp:ListItem Text="Evening(Everyday)" Value="03"></asp:ListItem>
                                <asp:ListItem Text="Evening(Friday)" Value="04"></asp:ListItem>
                                <asp:ListItem Text="Evening(EOM)" Value="05"></asp:ListItem>
							</asp:DropDownList>
					        <br />
					        <label for="simple-input">Custodian</label> 
					        <asp:DropDownList ID="CustodianBox" runat="server">
					        </asp:DropDownList>					        
					        <br /><br />
					        <asp:Button ID="SearchButton" Text="Search" runat="server" 
                                class="round blue button-submit" onclick="SearchButton_Click" />
						</p>
					</fieldset>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf">	
			<div class="half-size-column fl" style="height:1000px">
			    Total File <asp:Label ID="TotalBox" Text="0" runat="server"></asp:Label> File(s)
                <asp:GridView ID="GridData" runat="server"  CssClass="GridViewStyle"  
                    DataKeyNames="MTMScheduleID" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCtrl" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" ItemStyle-Wrap="false"  
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" SelectImageUrl="~/Theme/icon/actions-edit.png" >
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="Fund" HeaderText="Fund" HeaderStyle-Width="80px" >
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Custodian" HeaderText="Custodian" HeaderStyle-Width="80px" >
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NumFiles" HeaderText="Total of Fund" HeaderStyle-Width="100px" >
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <RowStyle CssClass="RowStyle" />
                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
                <br />
                <asp:Button ID="btnSendMail" Text="SendMail" runat="server" 
                    onclick="btnSendMail_Click" />
                <br /><br />
                    <asp:GridView ID="FileAttachView" ShowHeader="false" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="false" GridLines="None">
                        <Columns>
                            <asp:HyperLinkField DataTextField="Filename" HeaderText="Filename" DataNavigateUrlFormatString="~/{0}/{1}" DataNavigateUrlFields="Path,Filename" />
                        </Columns>
                        <RowStyle BackColor="#EFF3FB" />                    
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />                    
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
