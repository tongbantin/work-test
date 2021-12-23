<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR089MAPPINGMTM.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR089MAPPINGMTM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {

            function submitForm() {
                if ($('#<%=txtName.ClientID %>').val() == '') {
                    alert('Name is require!!');
                    return false;
                }
                if ($('#<%=Data1Box.ClientID %>').val() == '') {
                    alert('Column (Select) is require!!');
                    return false;
                }
                if ($('#<%=Data2Box.ClientID %>').val() == '') {
                    alert('Column (Select+) is require!!');
                    return false;
                }
                if ($('#<%=Data3Box.ClientID %>').val() == '') {
                    alert('Table (From) is require!!');
                    return false;
                }
                if ($('#<%=Data4Box.ClientID %>').val() == '') {
                    alert('Condition (Where) is require!!');
                    return false;
                }
                
                $('#<%=Name.ClientID %>').val($('#<%=txtName.ClientID %>').val());
                $('#<%=Data1.ClientID %>').val($('#<%=Data1Box.ClientID %>').val());
                $('#<%=Data2.ClientID %>').val($('#<%=Data2Box.ClientID %>').val());
                $('#<%=Data3.ClientID %>').val($('#<%=Data3Box.ClientID %>').val());
                $('#<%=Data4.ClientID %>').val($('#<%=Data4Box.ClientID %>').val());
                $('input#<%=DialogSubmit.ClientID %>').click();
            }

            $(".mapping-add").click(function() {
                $('#<%=ActionBox.ClientID %>').val("Add");
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 900,
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
                <td>Name</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Column (Select) </td>
                <td><asp:TextBox ID="Data1Box" height="80px" TextMode="MultiLine" Width="700px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Column (Select+) </td>
                <td><asp:TextBox ID="Data2Box"  height="80px" TextMode="MultiLine" Width="700px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Table (From) </td>
                <td><asp:TextBox ID="Data3Box" height="80px" TextMode="MultiLine" Width="700px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Condition (Where)</td>
                <td><asp:TextBox ID="Data4Box" height="80px" TextMode="MultiLine" Width="700px" runat="server"></asp:TextBox></td>
            </tr>
        </table>   
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">	
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Mark-To-Market : Mapping</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
						<p>
					        <label for="simple-input">Name</label>
                            <asp:TextBox ID="NameBox" runat="server"></asp:TextBox>
					        <br /><br />
					        <asp:Button ID="SearchButton" Text="Search" runat="server" 
                                class="round blue button-submit" onclick="SearchButton_Click1"/>
						</p>
					</fieldset>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Mark-To-Market : SQL Mapping</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf ">
			<div class="half-size-column fl" style="overflow:auto; width:100%;height:400px;">	
			    <div style="float:left"><button class="round blue button-submit mapping-add">Add Mapping</button></div> 
			    <div style="float:right"></div>
			    <br /><br />			    	
                <asp:GridView ID="GridViewFund" DataKeyNames="MTMMappingID" 
                    CssClass="GridViewStyle" AllowPaging="True" 
                    AutoGenerateColumns="False" runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MTMMappingID") %>' CommandName="Delete" ImageUrl="~/Theme/icon/actions-delete.png" OnClientClick="return confirm('Are you sure you want to delete?');" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" ItemStyle-Wrap="false"  
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" SelectImageUrl="~/Theme/icon/actions-edit.png" >
<HeaderStyle Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="Name" HeaderStyle-Width="70" HeaderText="Name"  > 
                        </asp:BoundField>
                        <asp:BoundField DataField="Data_1" HeaderStyle-Width="300" HeaderText="Column (Select)"  > 
                        </asp:BoundField>
                        <asp:BoundField DataField="Data_2" HeaderStyle-Width="300" HeaderText="Column (Select+)"  > 
                        </asp:BoundField>
                        <asp:BoundField DataField="Data_3" HeaderStyle-Width="300" HeaderText="Table (From)"  >
                        </asp:BoundField>
                        <asp:BoundField DataField="Data_4" HeaderStyle-Width="300" HeaderText="Condition (Where)"  > 
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
                <asp:HiddenField ID="MTMMapping" runat="server" />
                <asp:HiddenField ID="Name" runat="server" />
                <asp:HiddenField ID="Data1" runat="server" />
                <asp:HiddenField ID="Data2" runat="server" />
                <asp:HiddenField ID="Data3" runat="server" />
                <asp:HiddenField ID="Data4" runat="server" />
                <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" 
                    runat="server" onclick="DialogSubmit_Click" />
			</div> <!-- end half-size-column -->
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
