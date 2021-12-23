<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR089DFMTM.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR089DFMTM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <style type="text/css">
      .hiddencol
      {
        display: none;
      }
    </style>
    <script type="text/javascript">
        $(document).ready(function() {
            $('#ctl00_bodyContent_txtTransDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });            
        });  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <fieldset>
		    <table border="0">
		        <tbody>		            
		            <tr>
		                <td class="td-display"><label for="simple-input">Transaction Date:</label></td>
		                <td class="td-field"><input type="text" id="txtTransDate" name="txtTransDate"  runat="server" class="round input-textbox-date" /></td>
		            </tr>
		            <tr>
		                <td colspan="2" style="width:800px; text-align: left;">
                            <asp:Button ID="btnSubmit" 
                                Text="Submit" runat="server" class="round blue button-submit" onclick="btnSubmit_Click" 
                                 />
		                    <div id="divMsg" style="display:none;">
                                <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                            </div>    
		                </td>
		            </tr>
		            
		        </tbody>
		    </table>
		    
		</fieldset>
        <asp:GridView ID="GridData" AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="RowID" runat="server" 
        AllowPaging="True" CellSpacing="5" PageSize="20">
            <Columns>
                <asp:ButtonField ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center" 
                    CommandName="XCEL" ButtonType="Image" ImageUrl="~/Theme/icon/savefile.png" >
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:ButtonField>
                <asp:BoundField  DataField="Name" />   
                <asp:BoundField  DataField="PathFile" ItemStyle-CssClass="hiddencol" />      
            </Columns>
            <AlternatingRowStyle BackColor="#0099cc" />
        </asp:GridView>
</asp:Content>
