<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_FundPosition.aspx.cs" Inherits="TreasuryShadowSystem.Maintain.frm_FundPosition" %>
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

            $('#ctl00_bodyContent_txtFromDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            $('#ctl00_bodyContent_txtToDate').datepicker({
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

            $('#ctl00_bodyContent_EffDateDialogBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });


            $('#ctl00_bodyContent_GridViewFund_ctl02_InlineEffDateBox').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
            function submitForm() {
                $('#<%=EffDateBox.ClientID %>').val($('#<%=EffDateDialogBox.ClientID %>').val());
                $('#<%=AmountBox.ClientID %>').val($('#<%=AmountDialogBox.ClientID %>').val());
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
        <table width="400">
            <tr>
                <td>&nbsp;</td>
                <td>Effective date</td>
                <td><asp:TextBox ID="EffDateDialogBox"  Width="100px" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Amount</td>
                <td><asp:TextBox ID="AmountDialogBox"  Width="100px" runat="server"></asp:TextBox></td>
            </tr>
        </table>   
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Maintain Capital Balance</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
						<p>
							From
							<input type="text" id="txtFromDate" size="15" runat="server" class="round input-form-textbox" />	
							
							To
							<input type="text" id="txtToDate" size="15" runat="server" class="round input-form-textbox" />	
							
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
			    <div style="float:left"><button class="round blue button-submit ic-add">Add Capital Balance</button></div> 
			    <div style="float:right"></div>
			    <br /><br />			    	
                <asp:GridView ID="GridViewFund" Width="280px" DataKeyNames="FundPositionID" CellPadding="0" CellSpacing="0" CssClass="MaintainStyle" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:CommandField ShowEditButton="true" ButtonType="Image" ControlStyle-Width="15px" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" EditImageUrl="~/Theme/icon/actions-edit.png" UpdateImageUrl="~/Theme/icon/mini_save.png" CancelImageUrl="~/Theme/icon/actions-redo.png" />
                        <asp:TemplateField ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>Effective Date</HeaderTemplate>
                            <ItemTemplate><asp:Label ID="lbEffDate"  Text='<%# Eval("EffDate","{0:dd/M/yyyy}")%>' runat="server"></asp:Label></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="InlineEffDateBox" Text='<%# Eval("EffDate","{0:dd/M/yyyy}")%>' Width="90px" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>   
                        <asp:TemplateField ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            <HeaderTemplate>Amount</HeaderTemplate>
                            <ItemTemplate><asp:Label ID="lbAmount"  Text='<%# Eval("Amount","{0:N2}")%>' runat="server"></asp:Label></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="InlineAmountBox" Text='<%# Eval("Amount")%>'  Width="80px" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>                                 
                    </Columns>                    
                    <EmptyDataTemplate>
                        No Data
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:HiddenField ID="ActionBox" runat="server" />
                <asp:HiddenField ID="FundPositionBox" runat="server" />
                <asp:HiddenField ID="EffDateBox" runat="server" />
                <asp:HiddenField ID="AmountBox" runat="server" />
                <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" 
                    runat="server" onclick="DialogSubmit_Click" />
			</div> <!-- end half-size-column -->
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
