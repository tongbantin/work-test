<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_DebentureImport.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_DebentureImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $("[id*=chkAllOldCust]").click(function() {
                var chkHeader = $(this);
                var grid = $(this).closest("table");
                $("input[id*=chkOldCust]", grid).each(function() {
                    if (chkHeader.is(":checked")) {
                        $(this).prop("checked", true);
                    } else {
                        $(this).prop("checked", false);
                    }
                });
                if (chkHeader.is(":checked")) {
                    $("[id*=chkAllNewCust]").prop("checked", false);
                }
            });

            $("[id*=chkOldCust]").click(function() {
                var grid = $(this).closest("table");
                var chkHeader = $("[id*=chkAllOldCust]", grid);
                if (!$(this).is(":checked")) {
                    chkHeader.prop("checked", false);
                } else {
                    if ($("[id*=chkOldCust]", grid).length == $("[id*=chkOldCust]:checked", grid).length) {
                        chkHeader.prop("checked", true);
                    }
                    $("[id*=chkAllNewCust]").prop("checked", false);
                }
            });
            $("[id*=chkAllNewCust]").click(function() {
                var chkHeader = $(this);
                var grid = $(this).closest("table");
                $("input[id*=chkNewCust]", grid).each(function() {
                    if (chkHeader.is(":checked")) {
                        $(this).prop("checked", true);
                    } else {
                        $(this).prop("checked", false);
                    }
                });
                if (chkHeader.is(":checked")) {
                    $("[id*=chkAllOldCust]").prop("checked", false);
                }
            });

            $("[id*=chkNewCust]").click(function() {
                var grid = $(this).closest("table");
                var chkHeader = $("[id*=chkAllNewCust]", grid);
                if (!$(this).is(":checked")) {
                    chkHeader.prop("checked", false);
                } else {
                    if ($("[id*=chkNewCust]", grid).length == $("[id*=chkNewCust]:checked", grid).length) {
                        chkHeader.prop("checked", true);
                    }
                    $("[id*=chkAllOldCust]").prop("checked", false);
                }
            });
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <fieldset>
	    <table border="0">
	        <tbody>
	            <tr>
	                <td></td>
	                <td>
	                    <div style="color:red">*นำเข้าข้อมูลได้เฉพาะรายการใหม่เท่านั้น*</div>
	                    <div style="color:red">** กรณีนำเข้ารายการซ้ำ จะต้องไปลบในหน้าจัดการข้อมูล 
                            (Manage) เท่านั้น**</div>
	                </td>
	            </tr>
	            <tr>
	                <td class="td-display"><label for="simple-input">File:</label></td>
	                <td class="td-field">
	                    <asp:FileUpload ID="MyFile" class="round input-form-textbox" runat="server" />
	                </td>
	            </tr>
	            <tr>
	                <td colspan="2" style="width:800px; text-align: left;">
	                <asp:Button ID="btnSubmit" Text="Upload" runat="server" 
                            class="round blue button-submit" onclick="btnSubmit_Click" />
	                    <div id="divMsg" style="display:none;">
                            <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                        </div>    
	                </td>
	            </tr>
	            	            
	            <tr>
	                <td colspan="2" style="width:800px; text-align: left;">
	                    &nbsp;</td>
	            </tr>
	            	            
	        </tbody>
	    </table>
	    
	</fieldset>
	<div style="overflow: scroll; width:100%; height: 400px">
		<!--Version 1.0-->	
		<div style="width:100%">		
        <asp:GridView ID="GridData" AutoGenerateColumns="false" DataKeyNames="Records" ShowHeader="true" runat="server">
            <Columns>
                <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAllOldCust" Text="Old" ForeColor="#FFFFFF" Font-Bold="true" Font-Names="Tahoma" Font-Size="10pt" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:RadioButton ID="chkOldCust" GroupName='<%# Eval("Records")%>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="7pt" Font-Names="Tahoma"></ItemStyle>
                            <HeaderStyle BackColor="#507CD1" Font-Size="7pt" Font-Names="Tahoma" ForeColor="White"/>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAllNewCust" Text="New" ForeColor="#FFFFFF" Font-Bold="true" Font-Names="Tahoma" Font-Size="10pt" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:RadioButton ID="chkNewCust" GroupName='<%# Eval("Records")%>' runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="7pt" Font-Names="Tahoma"></ItemStyle>
                            <HeaderStyle BackColor="#507CD1" Font-Size="7pt" Font-Names="Tahoma" ForeColor="White"/>
                        </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>#</HeaderTemplate>
                    <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="INPUTROUND" HeaderText="R." />
                <asp:BoundField DataField="DEALNO" HeaderText="DealNo" />
                <asp:BoundField DataField="SECID" HeaderText="SECID" />
                <asp:BoundField DataField="UNIT" HeaderText="Unit" />
                <asp:BoundField DataField="AMOUNT" HeaderText="Amount" />
                <asp:BoundField DataField="Custodian" HeaderText="Custodian" />
                <asp:BoundField DataField="NAMETHAI" HeaderText="Name Thai" />
                <asp:BoundField DataField="CUSTID" HeaderText="CustID" />
                <asp:BoundField DataField="THAIBMANO" HeaderText="ThaiBMA No." />
                <asp:BoundField DataField="CUSTTYPE" HeaderText="CustType (OPICS)" />
                <asp:BoundField DataField="CTYPE" HeaderText="CustType(SEC)"/>
                <asp:BoundField DataField="BOTTYPE" HeaderText="CustType(BOT)" />
                <asp:BoundField DataField="RelateCustID" ItemStyle-BackColor="Gold" HeaderText="CustID(Old)" />
                <asp:BoundField DataField="RelateNameThai" ItemStyle-BackColor="Gold" HeaderText="Name Thai(Old)" />
            </Columns>                
            <EmptyDataTemplate>
                No Data
            </EmptyDataTemplate>                    
            <RowStyle CssClass="RowStyle" Font-Size="8pt" Font-Names="Tahoma" />
            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
            <PagerStyle CssClass="PagerStyle" />
            <SelectedRowStyle CssClass="SelectedRowStyle" />
            <HeaderStyle CssClass="HeaderStyle"  Font-Size="7pt" Font-Names="Tahoma"/>
            <EditRowStyle CssClass="EditRowStyle" />
            <AlternatingRowStyle CssClass="AltRowStyle" />            
        </asp:GridView>
        </div>           
        <asp:Button ID="btnSave" runat="server" Text="Save" Visible="false"  
        onclick="btnSave_Click" />
    </div>
</asp:Content>
