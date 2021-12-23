<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_MaintainPTIExpense.aspx.cs" Inherits="TreasuryShadowSystem.Maintain.frm_MaintainPTIExpense" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <style>
        table.MaintainStyle th { padding: 0px; margin: 0px; text-align:center; font-weight: bold; font: 10pt tahoma; height: 25px;}
        table.MaintainStyle tbody tr td { font: 10pt tahoma; height: 25px; }
        table.MaintainStyle input { width: 50px; margin: 0px; padding: 3px; text-align: center; text-transform: uppercase;}
        table.MaintainStyle table { width: auto; }
        table.MaintainStyle table tr td { width: 30px; }
    </style>
    <script type="text/javascript">
        $(document).ready(function() {

            $('#ctl00_bodyContent_txtExecuteDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $('#ctl00_bodyContent_txtSettDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            $('#ctl00_bodyContent_SettDateDialogBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            function ShowDialog() {
                /*$("#dialogmessage").dialog({
                title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                modal: true,
                "buttons": {
                "Close": function() {
                $(this).dialog("close");
                }
                }
                });*/
                alert('xxxx');
            }

            function submitForm() {
                $('#<%=CostBox.ClientID %>').val($('#<%=CostDialogBox.ClientID %>').val());
                $('#<%=DealNoBox.ClientID %>').val($('#<%=DealNoDialogBox.ClientID %>').val());
                $('#<%=SettDateBox.ClientID %>').val($('#<%=SettDateDialogBox.ClientID %>').val());
                $('#<%=ExpenseTypeBox.ClientID %>').val($('#<%=ExpenseTypeDialogBox.ClientID %>').val());
                $('#<%=FeeBox.ClientID %>').val($('#<%=FeeDialogBox.ClientID %>').val());
                $('input#<%=DialogSubmit.ClientID %>').click();
            }

            $(".ic-add").click(function() {
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
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
    <div id="dialog" style="display: none">
        <table widh="400">
            <tr>
                <td>&nbsp;</td>
                <td>Cost</td>
                <td><asp:TextBox ID="CostDialogBox"  Width="100px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Deal No.</td>
                <td><asp:TextBox ID="DealNoDialogBox"  Width="100px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Settlement Date</td>
                <td><asp:TextBox ID="SettDateDialogBox" Width="100px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Expense Type</td>
                <td>
                <asp:DropDownList ID="ExpenseTypeDialogBox" runat="server">
                    <asp:ListItem Text="WD" Value="WD"></asp:ListItem>
                    <asp:ListItem Text="DEP" Value="DEP"></asp:ListItem>
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Fee Amount</td>
                <td><asp:TextBox ID="FeeDialogBox" Width="100px" runat="server"></asp:TextBox></td>
            </tr>
        </table>   
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Maintain TSD Expense</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
						<p>
							Deal No
							<input type="text" id="txtDealNo" size="15" runat="server" class="round input-form-textbox" />
						    
							Trade Date
							<input type="text" id="txtExecuteDate" size="15" runat="server" class="round input-form-textbox" />	
							
							Settlement Date
							<input type="text" id="txtSettDate" size="15" runat="server" class="round input-form-textbox" />	
							
					        <asp:Button ID="btnSubmit" Text="Submit" runat="server" 
                                class="round blue button-submit" onclick="btnSubmit_Click" />
					        <div id="divMsg" style="display:none;">
                                <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                            </div>    
						</p>
					</fieldset>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf" ">
	
			<div class="half-size-column fl" style="overflow:auto; width:100%;height:400px;">	
			    <div style="float:left"><button class="round blue button-submit ic-add">Add Fee Transaction</button></div> 
			    <div style="float:right"><asp:Button ID="ExcelExportButton" runat="server" 
                        CssClass="round blue button-submit ic-download" Text="Excel" 
                        onclick="ExcelExportButton_Click" /></div>
			    <br /><br />			    	
                <asp:GridView ID="GridViewExpense" DataKeyNames="DealFeeID" CellPadding="0" CellSpacing="0" CssClass="MaintainStyle" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:CommandField ShowEditButton="true" ButtonType="Image" ControlStyle-Width="15px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" EditImageUrl="~/Theme/icon/actions-edit.png" UpdateImageUrl="~/Theme/icon/mini_save.png" CancelImageUrl="~/Theme/icon/actions-redo.png" />
                        <asp:BoundField DataField="DealNo" ReadOnly="true" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="DealNo" />
                        <asp:BoundField DataField="DealDate" ReadOnly="true" ItemStyle-Width="80px" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Trade Date" />
                        <asp:BoundField DataField="SettDate" ReadOnly="true" ItemStyle-Width="80px" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Settlement Date" />
                        <asp:BoundField DataField="PS" ReadOnly="true" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderText="P/S" />
                        <asp:BoundField DataField="SECID" ReadOnly="true" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="SECID" />
                        <asp:BoundField DataField="CUST" ReadOnly="true" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Customer" />
                        <asp:BoundField DataField="NOTIONAL" ReadOnly="true" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Notional" />
                        <asp:BoundField DataField="TSD_Fee" ReadOnly="true" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Fee" />
                        <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>TSD Expense</HeaderTemplate>
                            <ItemTemplate><asp:Label ID="lbMethod"  Text='<%# Eval("Method")%>' runat="server"></asp:Label></ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="MethodList" runat="server">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="TR" Value="TR"></asp:ListItem>
                                    <asp:ListItem Text="DVP" Value="DVP"></asp:ListItem>
                                    <asp:ListItem Text="RVP" Value="RVP"></asp:ListItem>
                                    <asp:ListItem Text="DF" Value="DF"></asp:ListItem>
                                    <asp:ListItem Text="RF" Value="RF"></asp:ListItem>
                                    <asp:ListItem Text="WD" Value="WD"></asp:ListItem>
                                    <asp:ListItem Text="DEP" Value="DEP"></asp:ListItem>
                                    <asp:ListItem Text="NONE" Value="NONE"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>                    
                    <EmptyDataTemplate>
                        No Data
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:HiddenField ID="CostBox" runat="server" />
                <asp:HiddenField ID="DealNoBox" runat="server" />
                <asp:HiddenField ID="SettDateBox" runat="server" />
                <asp:HiddenField ID="ExpenseTypeBox" runat="server" />
                <asp:HiddenField ID="FeeBox" runat="server" />
                <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" 
                    runat="server" onclick="DialogSubmit_Click" />
			</div> <!-- end half-size-column -->
		        <asp:GridView ID="GridViewExport" ShowHeader="true" runat="server" Visible="false" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Cost" ReadOnly="true" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Cost" />
                        <asp:BoundField DataField="DealNo" ReadOnly="true" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="DealNo" />
                        <asp:BoundField DataField="DealDate" ReadOnly="true" ItemStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Trade Date" />
                        <asp:BoundField DataField="SettDate" ReadOnly="true" ItemStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="Settlement Date" />
                        <asp:BoundField DataField="PS" ReadOnly="true" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderText="P/S" />
                        <asp:BoundField DataField="SECID" ReadOnly="true" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="SECID" />
                        <asp:BoundField DataField="CUST" ReadOnly="true" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Customer" />
                        <asp:BoundField DataField="NOTIONAL" ReadOnly="true" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Notional" />
                        <asp:BoundField DataField="TSD_Fee" ReadOnly="true" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderText="Fee" />
                        <asp:BoundField DataField="METHOD" ReadOnly="true" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" HeaderText="Payment Method" />
                    </Columns>
                    <SelectedRowStyle BackColor="GreenYellow" />
                </asp:GridView>
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
