<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="UploadTranALR.aspx.cs"
    Inherits="TreasuryShadowSystem.ALR.UploadTranALR" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>"
        type="text/css" />
    <style>
        .ui-dialog
        {
            width: 500px !important;
        }
        ::-webkit-scrollbar-thumb
        {
            background-color: #DDD !important;
        }
        .PagerStyle
        {
            background-color: #FFF !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {

            $('#ctl00_bodyContent_DateBox').datepicker({
            altFormat: 'dd/mm/yy',
            dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });

            $('.txt_SETTLE_DATE').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
             $(".deleterecord").click(function() {
                if (confirm('Are you sure you want to delete?')) {
                    var tranid = $(this).attr('alt');
                    $.ajax({
                        type: "POST",
                        url: '<%=Config.Website %>/ALR/UploadTranALR.aspx/DeleteRecord',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ 'tranid': tranid }),
                        success: function(data) {
                            if (data.d.Result == "OK") {
                                alert("Delete transaction Complete");
                                $("#<%=btnSearch.ClientID %>").click();
                            } else {
                                alert(data.d.Message);
                            }
                        },
                        error: function(xhr, ajaxOptions, thrownError) {
                            var err = JSON.parse(xhr.responseText);
                            alert(err.Message);
                        }
                    });
                } return false;
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
            <h3 class="fl">
                Import Transaction ALR</h3>
            <span class="fr expand-collapse-text">Click to collapse and expand</span>
        </div>
        <!-- end content-module-heading -->
        <div id="criteria-content" class="content-module-main cf">
            <div class="half-size-column fl">
                <fieldset>
                    <div style="padding: 5px">
                        <div style="float: left; width: 150px;">
                            Deal No :</div>
                        <div style="float: left;">
                            <asp:TextBox ID="txtDealNo" runat="server" placeholder="e.g. 1000008" CssClass="round input-form-textbox"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                        <div style="float: left; width: 150px;">
                            Settlement Date :</div>
                        <div style="float: left;">
                            <asp:TextBox ID="DateBox" runat="server"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                    </div>
                    <div style="float: left; width: 100px;">
                        <asp:Button runat="server" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" />
                        <div id="div2" style="display: none;">
                            <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                        </div>
                    </div>
                </fieldset>
            </div>
            <!-- end half-size-column -->
            <div class="half-size-column fl">
                <fieldset>
                    <div style="padding: 5px">
                        <div style="float: left; width: 150px;">
                            File upload :</div>
                        <div>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                        </div>
                    </div>
                    <div style="float: left; width: 100px;">
                        <asp:Button ID="Button1" Text="Submit" runat="server" class="round blue button-submit"
                            OnClick="btnSubmit_Click" />
                        <div id="div1" style="display: none;">
                            <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                        </div>
                    </div>
                </fieldset>
            </div>
            <!-- end half-size-column -->
        </div>
        <!-- end content-module-main -->
        <div style="overflow: scroll; height: 400px">
             <asp:GridView ID="GridALR"  DataKeyNames="TRANSID" CellPadding="0" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" runat="server">
                        <Columns>                           
                            <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img src="../Theme/icon/actions-delete.png" class="deleterecord" alt="<%# DataBinder.Eval(Container.DataItem, "TRANSID") %>" style="cursor:pointer" />
                                </ItemTemplate>
                            </asp:TemplateField>                          
                            <asp:BoundField DataField="TEAM" ItemStyle-HorizontalAlign="Center" HeaderText="Team" /> 
                            <asp:BoundField DataField="DEALNO" ItemStyle-HorizontalAlign="Center" HeaderText="Dealno"  /> 
                            <asp:BoundField DataField="ID_NO_PART1" ItemStyle-HorizontalAlign="Left" HeaderText="TAXID Part1" /> 
                            <asp:BoundField DataField="ID_NO_PART2" ItemStyle-HorizontalAlign="Left" HeaderText="TAXID Part2" /> 
                            <asp:BoundField DataField="SENDER_CUST_TYPE" ItemStyle-HorizontalAlign="Left" HeaderText="Sending Custtype" /> 
                            <asp:BoundField DataField="SENDING_BANK_CODE" ItemStyle-HorizontalAlign="Left" HeaderText="Sending Bankcode"/> 
                            <asp:BoundField DataField="SENDING_TYPE" ItemStyle-HorizontalAlign="Left" HeaderText="Sending Type" /> 
                            <asp:BoundField DataField="SENDING_AC_NO"  ItemStyle-HorizontalAlign="Left" HeaderText="Sending Acctno"/> 
                            <asp:BoundField DataField="SENDING_INFO" ItemStyle-HorizontalAlign="Left" HeaderText="Sending Info"/> 
                            <asp:BoundField DataField="RECEIVER_CUST_TYPE" ItemStyle-HorizontalAlign="Left" HeaderText="Receiving Custtype" /> 
                            <asp:BoundField DataField="RECEIVING_BANK_CODE" ItemStyle-HorizontalAlign="Left" HeaderText="Receiving Bankcode" />
                            <asp:BoundField DataField="RECEIVING_TYPE" ItemStyle-HorizontalAlign="Left" HeaderText="Receiving Type" /> 
                            <asp:BoundField DataField="RECEIVING_AC_NO" ItemStyle-HorizontalAlign="Left" HeaderText="Receiving Acctno" /> 
                            <asp:BoundField DataField="RECEIVING_INFO"  ItemStyle-HorizontalAlign="Left" HeaderText="Receiving Info" /> 
                            <asp:BoundField DataField="TRANSFER_AMOUNT" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" HeaderText="Amount"/> 
                            <asp:BoundField DataField="SETTLE_DATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="SettleDate"/> 
                            <asp:BoundField DataField="UPLOAD_DATE" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="UploadDate" /> 
                           
                        </Columns>                    
                        <EmptyDataTemplate>
                            No Data Found
                        </EmptyDataTemplate>                    
                        <RowStyle CssClass="RowStyle" />
                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                    </asp:GridView>
        
 
      
        </div>
    </div>
    <!-- end content-module -->
</asp:Content>
