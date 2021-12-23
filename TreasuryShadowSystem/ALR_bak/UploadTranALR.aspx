<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="UploadTranALR.aspx.cs"
    Inherits="TreasuryShadowSystem.ALR.UploadTranALR" %>

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
            altFormat: 'dd/MM/yy',
            dateFormat: 'dd/MM/yy',
                changeMonth: true,
                changeYear: true
            });

            $('.txt_SETTLE_DATE').datepicker({
                altFormat: 'dd/MM/yy',
                dateFormat: 'dd/MM/yy',
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
            <asp:GridView ID="GridALR" DataKeyNames="TRANSID" CellPadding="0" CssClass="GridViewStyle"
                AllowPaging="True" AllowSorting="true" PageSize="20" AutoGenerateColumns="False"
                runat="server">
                <Columns>
                     <asp:TemplateField HeaderText="Delete" ShowHeader="false">
                        <ItemTemplate>
                                    <img src="../Theme/icon/actions-delete.png" class="delettrans" alt="<%# DataBinder.Eval(Container.DataItem, "TRANSID") %>" style="cursor:pointer" />
                        </ItemTemplate>
                    </asp:TemplateField>                  
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            TEAM</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblteam" Text='<%# Eval("TEAM")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtteam" Text='<%# Eval("TEAM")%>' Width="90px" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            DEALNO</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbldealno" Text='<%# Eval("DEALNO")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtdealno" Text='<%# Eval("DEALNO")%>' Width="90px" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            ID_NO_PART1</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_ID_NO_PART1" Text='<%# Eval("ID_NO_PART1")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_ID_NO_PART1" Text='<%# Eval("ID_NO_PART1")%>' Width="90px" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            ID_NO_PART2</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_ID_NO_PART2" Text='<%# Eval("ID_NO_PART2")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_ID_NO_PART2" Text='<%# Eval("ID_NO_PART2")%>' Width="90px" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            SENDER_CUST_TYPE</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_SENDER_CUST_TYPE" Text='<%# Eval("SENDER_CUST_TYPE")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_SENDER_CUST_TYPE" Text='<%# Eval("SENDER_CUST_TYPE")%>' Width="90px"
                                runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            SENDING_BANK_CODE</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_SENDING_BANK_CODE" Text='<%# Eval("SENDING_BANK_CODE")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_SENDING_BANK_CODE" Text='<%# Eval("SENDING_BANK_CODE")%>' Width="90px"
                                runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            SENDING_TYPE</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_SENDING_TYPE" Text='<%# Eval("SENDING_TYPE")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_SENDING_TYPE" Text='<%# Eval("SENDING_TYPE")%>' Width="90px"
                                runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            SENDING_AC_NO</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_SENDING_AC_NO" Text='<%# Eval("SENDING_AC_NO")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_SENDING_AC_NO" Text='<%# Eval("SENDING_AC_NO")%>' Width="90px"
                                runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            SENDING_INFO</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_SENDING_INFO" Text='<%# Eval("SENDING_INFO")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_SENDING_INFO" Text='<%# Eval("SENDING_INFO")%>' Width="90px"
                                runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            RECEIVER_CUST_TYPE</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_RECEIVER_CUST_TYPE" Text='<%# Eval("RECEIVER_CUST_TYPE")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_RECEIVER_CUST_TYPE" Text='<%# Eval("RECEIVER_CUST_TYPE")%>'
                                Width="90px" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            RECEIVING_BANK_CODE</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_RECEIVING_BANK_CODE" Text='<%# Eval("RECEIVING_BANK_CODE")%>'
                                runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_RECEIVING_BANK_CODE" Text='<%# Eval("RECEIVING_BANK_CODE")%>'
                                Width="90px" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            RECEIVING_TYPE</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_RECEIVING_TYPE" Text='<%# Eval("RECEIVING_TYPE")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_RECEIVING_TYPE" Text='<%# Eval("RECEIVING_TYPE")%>' Width="90px"
                                runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            RECEIVING_AC_NO</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_RECEIVING_AC_NO" Text='<%# Eval("RECEIVING_AC_NO")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_RECEIVING_AC_NO" Text='<%# Eval("RECEIVING_AC_NO")%>' Width="90px"
                                runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            RECEIVING_INFO</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_RECEIVING_INFO" Text='<%# Eval("RECEIVING_INFO")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_RECEIVING_INFO" Text='<%# Eval("RECEIVING_INFO")%>' Width="90px"
                                runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            TRANSFER_AMOUNT</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_TRANSFER_AMOUNT" Text='<%# Eval("TRANSFER_AMOUNT")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_TRANSFER_AMOUNT" Text='<%# Eval("TRANSFER_AMOUNT")%>' Width="90px"
                                runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            SETTLE_DATE</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_SETTLE_DATE" Text='<%# Eval("SETTLE_DATE")%>' runat="server"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_SETTLE_DATE" CssClass="txt_SETTLE_DATE" Text='<%# Eval("SETTLE_DATE")%>'
                                Width="90px" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <HeaderTemplate>
                            UPLOAD_DATE</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_UPLOAD_DATE" Text='<%# Eval("UPLOAD_DATE")%>' runat="server"></asp:Label></ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                    </asp:TemplateField>                   
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
