<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frmBondUpload.aspx.cs" Inherits="TreasuryShadowSystem.Bond.frmBondUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <fieldset>
	    <table border="0">
	        <tbody>
	            
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
	                    <asp:Label ID="lblMessage" ForeColor="Red" runat="server"></asp:Label></td>
	            </tr>
	            
	        </tbody>
	    </table>
	    
	</fieldset>
		<!--Version 1.0-->			
        <asp:GridView ID="gdData" AutoGenerateColumns="false" ShowHeader="true" runat="server">
            <Columns>
                <asp:BoundField DataField="PartiID" HeaderText="PartiID" />
                <asp:BoundField DataField="MsgType" HeaderText="MsgType" />
                <asp:BoundField DataField="Priority" HeaderText="Priority" />
                <asp:BoundField DataField="SenderRef" HeaderText="SenderRef" />
                <asp:BoundField DataField="RelatedRef" HeaderText="RelatedRef" />
                <asp:BoundField DataField="PlaceCode" HeaderText="PlaceCode" />
                <asp:BoundField DataField="SettlementDate" HeaderText="SettlementDate" />
                <asp:BoundField DataField="TradeDate" HeaderText="TradeDate" />
                <asp:BoundField DataField="PercentageType" HeaderText="PercentageType" />
                <asp:BoundField DataField="DealPrice" HeaderText="DealPrice" />
                <asp:BoundField DataField="ISIN" HeaderText="ISIN" />
                <asp:BoundField DataField="FaceAmount" HeaderText="FaceAmount" />
                <asp:BoundField DataField="AcctNo" HeaderText="AcctNo" />
                <asp:BoundField DataField="CashAcct" HeaderText="CashAcct" />
                <asp:BoundField DataField="BusinessType" HeaderText="BusinessType" />
                <asp:BoundField DataField="SettlementType" HeaderText="SettlementType" />
                <asp:BoundField DataField="PS" HeaderText="Purchase/Sell" />
                <asp:BoundField DataField="SettlementPlace" HeaderText="SettlementPlace" />
                <asp:BoundField DataField="Attn" HeaderText="Attn" />
                <asp:BoundField DataField="AgentBIC" HeaderText="AgentBIC" />
                <asp:BoundField DataField="AgentCA" HeaderText="AgentCA" />
                <asp:BoundField DataField="AgentAcctNo" HeaderText="AgentAcctNo" />
                <asp:BoundField DataField="PrinAmt" HeaderText="PrincipalAmt" />
                <asp:BoundField DataField="IntAmt" HeaderText="InterestAmt" />
                <asp:BoundField DataField="SettlementAmt" HeaderText="SettlementAmt" />
                
                <%--<asp:BoundField DataField="PartialID" HeaderText="PartiID" />
                <asp:BoundField DataField="MsgType" HeaderText="MsgType" />
                <asp:BoundField DataField="Priority" HeaderText="Priority" />
                <asp:BoundField DataField="SenderRef" HeaderText="SenderRef" />
                <asp:BoundField DataField="RelatedRef" HeaderText="RelatedRef" />
                <asp:BoundField DataField="PlaceCode" HeaderText="PlaceCode" />
                <asp:BoundField DataField="SettDate" HeaderText="SettlementDate" />
                <asp:BoundField DataField="TradeDate" HeaderText="TradeDate" />
                <asp:BoundField DataField="PercentType" HeaderText="PercentType" />
                <asp:BoundField DataField="DealPrice" HeaderText="DealPrice" />
                <asp:BoundField DataField="ISIN" HeaderText="ISIN" />
                <asp:BoundField DataField="FaceAmt" HeaderText="FaceAmount" />
                <asp:BoundField DataField="AcctNo" HeaderText="AcctNo" />
                <asp:BoundField DataField="CashAcct" HeaderText="CashAcct" />
                <asp:BoundField DataField="BusinessType" HeaderText="BusinessType" />
                <asp:BoundField DataField="SettType" HeaderText="SettlementType" />
                <asp:BoundField DataField="PS" HeaderText="Purchase/Sell" />
                <asp:BoundField DataField="SettPlace" HeaderText="SettlementPlace" />
                <asp:BoundField DataField="Attn" HeaderText="Attn" />
                <asp:BoundField DataField="AgentBIC" HeaderText="AgentBIC" />
                <asp:BoundField DataField="AgentCA" HeaderText="AgentCA" />
                <asp:BoundField DataField="AgentAcctNo" HeaderText="AgentAcctNo" />
                <asp:BoundField DataField="PrinAmt" HeaderText="PrincipalAmt" />
                <asp:BoundField DataField="IntAmt" HeaderText="InterestAmt" />
                <asp:BoundField DataField="SettAmt" HeaderText="SettlementAmt" />--%>
            </Columns>
            <HeaderStyle Height="30" />
            <SelectedRowStyle BackColor="Aqua" />
        </asp:GridView>
                   
        <asp:Button ID="btnSave" runat="server" Text="Gen XML" onclick="btnSave_Click" />
        <asp:Button ID="btnDownload" runat="server" Text="Download XML" Visible="false" 
            onclick="btnDownload_Click" />
        <asp:LinkButton ID="btnGoTo" runat="server" Text="Show Confirmation" 
            Visible="false" onclick="btnGoTo_Click"></asp:LinkButton>
</asp:Content>