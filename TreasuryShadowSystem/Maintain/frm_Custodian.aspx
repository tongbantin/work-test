<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_Custodian.aspx.cs" Inherits="TreasuryShadowSystem.Maintain.frm_Custodian" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {

            function submitForm() {
                if ($('#<%=NameBox.ClientID %>').val() == '') {
                    alert('Name is require!!');
                    return false;
                }
                if ($('#<%=BicBox.ClientID %>').val() == '') {
                    alert('BIC is require!!');
                    return false;
                }
                $('#<%=Name.ClientID %>').val($('#<%=NameBox.ClientID %>').val());
                $('#<%=Bic.ClientID %>').val($('#<%=BicBox.ClientID %>').val());
                $('#<%=AcctNo.ClientID %>').val($('#<%=AcctNoBox.ClientID %>').val());
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
    <script>
        $(function() {
            $(document).on("keydown.autocomplete", ".custbic", function(e) {
                $(this).autocomplete({
                    source: function(request, response) {
                        $.ajax({
                            type: "POST",
                            url: "<%=Config.Website %>/Services/Opics.asmx/CustBicJson",
                            minLength: 2,
                            data: "{name:'" + request.term + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function(data) {
                                //alert(eval("(" + data.d + ")"));
                                response(eval("(" + data.d + ")"));
                            }
                        });
                    },
                    select: function(event, ui) {
                        $(this).parent().parent().parent().children("tr:nth-child(2)").children("td:nth-child(3)").find(".biccode").val(ui.item.value.trim());
                        $(this).val(ui.item.label.trim());
                        return false;
                    }
                });
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
                    <asp:TextBox ID="NameBox" Width="100px" CssClass="custbic" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>BIC</td>
                <td>
                    <asp:TextBox ID="BicBox" CssClass="biccode" Width="100px" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Account No</td>
                <td>
                    <asp:TextBox ID="AcctNoBox"  Width="100px" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>   
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">	
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Custodian Managment</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
						<p>
					        <label for="simple-input">Name</label>
                            <asp:TextBox ID="NameSearchBox" runat="server"></asp:TextBox>
					        <br />
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
			    <div style="float:left"><button class="round blue button-submit ic-add">Add Custodian</button></div> 
			    <div style="float:right"></div>
			    <br /><br />			    	
                <asp:GridView ID="GridViewCustodian" DataKeyNames="CustodianID" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CustodianID") %>' CommandName="Delete" ImageUrl="~/Theme/icon/actions-delete.png" OnClientClick="return confirm('Are you sure you want to delete?');" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" ItemStyle-Wrap="false"  
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" SelectImageUrl="~/Theme/icon/actions-edit.png" >
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="Name" HeaderStyle-Width="70" HeaderText="Name" > 
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="BIC" HeaderStyle-Width="100" HeaderText="BIC Code"  >    
                        <HeaderStyle Width="150px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AcctNo" HeaderStyle-Width="100" HeaderText="A/C No."  > 
                        <HeaderStyle Width="150px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
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
                <asp:HiddenField ID="CustodianID" runat="server" />
                <asp:HiddenField ID="Name" runat="server" />
                <asp:HiddenField ID="Bic" runat="server" />
                <asp:HiddenField ID="AcctNo" runat="server" />
                <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" 
                    runat="server" onclick="DialogSubmit_Click" />
			</div> <!-- end half-size-column -->
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
