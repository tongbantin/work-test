<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_Message.aspx.cs" Inherits="TreasuryShadowSystem.Sanction.frm_Message" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {  
               
             $(".btnshowdetail").click( function() {
                if(this.tog ^= 1) {  
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>"); 
                } else {  
                    $(this).closest("tr").next().remove(); 
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
    <asp:Table ID="SWIFTDetailTable" runat="server" Width="1000" HorizontalAlign="Center" BorderWidth="0" CellPadding="3" CellSpacing="3">
        <asp:TableHeaderRow BackColor="BlueViolet" Height="30px">
            <asp:TableCell>&nbsp;SWIFT Message : <asp:Label ID="MsgLabel" runat="server"></asp:Label></asp:TableCell>            
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:GridView ID="GridSanction" ShowHeader="true" CssClass="GridViewStyle" AutoGenerateColumns="false" EmptyDataText="Data Not Found" CellPadding="0" CellSpacing="0" BorderWidth="0" runat="server" DataKeyNames="CHECKINGID">
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="39" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <img class="btnshowdetail" src="../Theme/icon/icon-tracking.png" style="cursor:pointer"/>                                    
                                <asp:Panel ID="AllotmentSpotDetail" runat="server" Style="display:none">
                                    <asp:GridView ID="GridSanctionDetail" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" CssClass="GridViewStyle" runat="server">                                            
                                        <Columns>
                                            <asp:BoundField DataField="category" HeaderText="Category" />    
                                            <asp:BoundField DataField="subCategory" HeaderText="SubCategory" />
                                            <asp:BoundField DataField="types" HeaderText="Types" />
                                            <asp:BoundField DataField="keyword" HeaderText="Keyword" />
                                            <asp:BoundField DataField="matchedNameType" HeaderText="Matched Name Type" />
                                            <asp:BoundField DataField="matchedNameScore" HeaderText="Matched Name Score" />
                                            <asp:BoundField DataField="ruleCode" HeaderText="Rule Code" />
                                            <asp:BoundField DataField="ruleType" HeaderText="Rule Type" />
                                            <asp:BoundField DataField="sourceWatchlist" HeaderText="Source Watchlist" />                                             
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No Data
                                        </EmptyDataTemplate>              
                                        <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <HeaderStyle CssClass="HeaderStyle"/>
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <RowStyle CssClass="RowStyle" />
                                    </asp:GridView>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:BoundField DataField="COND_NAME" HeaderText="Condition" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="MSG" HeaderText="Message" ItemStyle-Wrap="false" />
                        <asp:BoundField DataField="DETAIL" HeaderText="Detail" ItemStyle-Wrap="true" />
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
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="RemarkLabel" Text="Remark" runat="server"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="RemarkBox" TextMode="MultiLine" Width="400" Height="50" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Button ID="RejectButton" Text="Reject" runat="server" 
                OnClientClick="if(!confirm('Do you Reject the transaction?')) return false;" 
                class="round button-warning" onclick="RejectButton_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <div style="float:left">
                <asp:Button ID="RecheckButton" Text="Recheck" runat="server" 
                OnClientClick="if(!confirm('Do you Recheck the transaction?')) return false;" 
                class="round button-submit" onclick="RecheckButton_Click" />
                </div>    
                <div style="float:right">
                <asp:Button ID="AcceptButton" Text="Accept" 
                runat="server" 
                OnClientClick="if(!confirm('Do you Accept the transaction?')) return false;" 
                class="round blue button-submit" onclick="AcceptButton_Click" />  	
                </div> 
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>                   
</fieldset>

</asp:Content>
