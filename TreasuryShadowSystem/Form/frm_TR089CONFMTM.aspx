<%@ Page Title="" Language="C#" EnableTheming="true" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR089CONFMTM.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR089CONFMTM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {

            function submitForm() {
                if ($('#<%=MutualFundBox.ClientID %>').val() == '') {
                    alert('Fund is require!!');
                    return false;
                }
                
                if($('#<%=AllMDateBox.ClientID %>').is(":checked"))  {
                    $('#<%=AllMDate.ClientID %>').val('1');
                } else {
                    $('#<%=AllMDate.ClientID %>').val('0');                
                }               
                $('#<%=Round.ClientID %>').val($('#<%=RoundBox.ClientID %>').val());
                $('#<%=MutualFund.ClientID %>').val($('#<%=MutualFundBox.ClientID %>').val());
                $('#<%=OpenClose.ClientID %>').val($('#<%=OpenCloseBox.ClientID %>').val());
                $('#<%=Source.ClientID %>').val($('#<%=SourceBox.ClientID %>').val());
                $('#<%=SpecialSource.ClientID %>').val($('#<%=SpecialSourceBox.ClientID %>').val());
                $('#<%=Rate.ClientID %>').val($('#<%=RateBox.ClientID %>').val());
                $('#<%=FileType.ClientID %>').val($('#<%=FileTypeBox.ClientID %>').val());
                $('#<%=FileName.ClientID %>').val($('#<%=FileNameBox.ClientID %>').val());
                $('#<%=Template.ClientID %>').val($('#<%=TemplateBox.ClientID %>').val());
                $('#<%=Notes.ClientID %>').val($('#<%=NotesBox.ClientID %>').val());
                $('input#<%=DialogSubmit.ClientID %>').click();
            }

            $(".ic-add").click(function() {
                $('#<%=ActionBox.ClientID %>').val("Add");
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 600,
                    modal: true,
                    "buttons": {
                        "Submit": function() {
                            submitForm();
                        },
                        "Close": function() {
                            $(this).dialog("close");
                        }
                    }
                });
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>Time</td>
                <td>
                    <asp:DropDownList ID="RoundBox" runat="server">
                        <asp:ListItem Text="Morning-Everyday" Value="01"></asp:ListItem>
                        <asp:ListItem Text="Morning-EOM" Value="02"></asp:ListItem>
                        <asp:ListItem Text="Evening-Everyday" Value="03"></asp:ListItem>
                        <asp:ListItem Text="Evening-Friday" Value="04"></asp:ListItem>
                        <asp:ListItem Text="Evening-EOM" Value="05"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Fund</td>
                <td><asp:TextBox ID="MutualFundBox"  Width="400px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><div>Type of Fund</div></td>
                <td>
                    <asp:DropDownList ID="OpenCloseBox" runat="server">
                        <asp:ListItem Text="N/A" Value=""></asp:ListItem>
                        <asp:ListItem Text="Open" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Close" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Source</td>
                <td>
                    <asp:DropDownList ID="SourceBox" runat="server">
                        <asp:ListItem Text="CMPN" Value="CMPN"></asp:ListItem>
                        <asp:ListItem Text="GIT" Value="GIT"></asp:ListItem>
                        <asp:ListItem Text="BFIX" Value="BFIX"></asp:ListItem>
                        <asp:ListItem Text="REUTERS" Value="REUTERS"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Source(Special)</td>
                <td><asp:TextBox ID="SpecialSourceBox"  Width="200px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Rate</td>
                <td>
                    <asp:DropDownList ID="RateBox" runat="server">
                        <asp:ListItem Text="Morning" Value="01"></asp:ListItem>
                        <asp:ListItem Text="Evening" Value="02"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><div>File Type</div></td>
                <td>
                    <asp:DropDownList ID="FileTypeBox" runat="server">
                        <asp:ListItem Text="PDF" Value="PDF"></asp:ListItem>
                        <asp:ListItem Text="Excel" Value="Excel"></asp:ListItem>
                    </asp:DropDownList>
                 </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>All Maturity Date</td>
                <td><asp:CheckBox ID="AllMDateBox" Checked="true" runat="server" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Filename (Prefix)</td>
                <td><asp:TextBox ID="FileNameBox"  Width="200px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Template</td>
                <td>
                    <asp:DropDownList ID="TemplateBox" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Notes</td>
                <td><asp:TextBox ID="NotesBox" TextMode="MultiLine" Width="400px" runat="server"></asp:TextBox>                    
                </td>
            </tr>
        </table>   
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">	
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Mark-To-Market : Config Schedule</h3>
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
						<p>
					        <label for="simple-input">Time</label>
                            <asp:DropDownList ID="RoundSearchBox" AutoPostBack="true" runat="server" 
                                onselectedindexchanged="RoundSearchBox_SelectedIndexChanged">
                                <asp:ListItem Text="All" Value=""></asp:ListItem>
                                <asp:ListItem Text="Morning(Everyday)" Value="01"></asp:ListItem>
                                <asp:ListItem Text="Morning(EOM)" Value="02"></asp:ListItem>
                                <asp:ListItem Text="Evening(Everyday)" Value="03"></asp:ListItem>
                                <asp:ListItem Text="Evening(Friday)" Value="04"></asp:ListItem>
                                <asp:ListItem Text="Evening(EOM)" Value="05"></asp:ListItem>
                            </asp:DropDownList>
					        <br />
							<label for="simple-input">Fund</label> 
					        <asp:DropDownList ID="MutualFundsSearchBox" runat="server">
					        </asp:DropDownList>					        
					        <br /><br />
					        <asp:Button ID="SearchButton" Text="Search" runat="server" 
                                class="round blue button-submit" onclick="SearchButton_Click"/>
						</p>
					</fieldset>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf ">
			<div class="half-size-column fl" style="overflow:auto; width:100%;height:400px;">	
			    <div style="float:left"><button class="round blue button-submit ic-add">Add Schedule</button></div> 
			    <div style="float:right"></div>
			    <br /><br />			    	
                <asp:GridView ID="GridViewFund" DataKeyNames="MTMScheduleID" 
                    CssClass="GridViewStyle" AllowPaging="True" 
                    AutoGenerateColumns="False" runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MTMScheduleID") %>' CommandName="Delete" ImageUrl="~/Theme/icon/actions-delete.png" OnClientClick="return confirm('Are you sure you want to delete?');" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" ItemStyle-Wrap="false"  
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" SelectImageUrl="~/Theme/icon/actions-edit.png" >
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="Round" HeaderStyle-Width="70" HeaderText="Time" > 
                        <HeaderStyle Width="70px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="MutualFund" HeaderStyle-Width="100" 
                            HeaderText="Fund"  > 
                        <HeaderStyle Width="100px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="OpenClose" HeaderStyle-Width="70" 
                            HeaderText="Type of Fund"  > 
                        <HeaderStyle Width="70px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Source" HeaderStyle-Width="70" HeaderText="Source"  > 
                        <HeaderStyle Width="70px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SpecialSource" HeaderStyle-Width="70" HeaderText="Other Source"  > 
                        <HeaderStyle Width="70px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Rate" HeaderStyle-Width="70" HeaderText="Rate"  > 
                        <HeaderStyle Width="70px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FileType" HeaderStyle-Width="70" HeaderText="FileType"  > 
                        <HeaderStyle Width="70px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AllMDate" HeaderStyle-Width="70" 
                            HeaderText="All"  > 
                        <HeaderStyle Width="70px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="FileName" HeaderStyle-Width="100" HeaderText="FileName(Prefix)"  > 
                        <HeaderStyle Width="100px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Template" HeaderStyle-Width="100" HeaderText="Template"  > 
                        <HeaderStyle Width="100px"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>                    
                    <EmptyDataTemplate>
                        No Data
                    </EmptyDataTemplate>                    
                    <RowStyle CssClass="RowStyle" />
                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
                <asp:HiddenField ID="ActionBox" runat="server" />
                <asp:HiddenField ID="MTMScheduleBox" runat="server" />
                <asp:HiddenField ID="Round" runat="server" />
                <asp:HiddenField ID="MutualFund" runat="server" />
                <asp:HiddenField ID="OpenClose" runat="server" />
                <asp:HiddenField ID="Source" runat="server" />
                <asp:HiddenField ID="SpecialSource" runat="server" />
                <asp:HiddenField ID="Rate" runat="server" />
                <asp:HiddenField ID="FileType" runat="server" />
                <asp:HiddenField ID="AllMDate" runat="server" />
                <asp:HiddenField ID="FileName" runat="server" />
                <asp:HiddenField ID="Template" runat="server" />
                <asp:HiddenField ID="Notes" runat="server" />
                <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" 
                    runat="server" onclick="DialogSubmit_Click" />
			</div> <!-- end half-size-column -->
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
