<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_NTDAILY.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_NTDAILY" EnableEventValidation="false" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <style type="text/css">
        .Hide { display:none; }
        .SubGridViewStyle
        {
            width: auto;
            font-family: Arial, Sans-Serif;
            font-size:small;
            table-layout: auto;
            border-collapse: collapse;
            border: #1d1d1d 1px solid;
            height: auto;
            width : auto;
           
            
        }
        /*RowStyles*/
        .SubRowStyle td, .SubAltRowStyle td /*Common Styles*/
        {
            padding: 5px;
            border-right: solid 1px #1d1d1d;
        }
        .SubRowStyle td
        {
            background-color: #009ac7;
            color: #ffffff;
        }
        .SubAltRowStyle td
        {
            background-color: #332a86;
            color: #ffffff;
        }

    </style>    
    <script type="text/javascript">
    
        $(document).ready(function() {

        $(".showbycost").click(function() {
            if (this.tog ^= 1) {
                $(this).closest("tr").after("<tr><td></td><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                //$(this).src("../Theme/icon/actions-redo.png");
            } else {
                $(this).closest("tr").next().remove();
                //$(this).src("../Theme/icon/icon-tracking.png");
            }
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
			<h3 class="fl">Daily Nostro</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">		
		    <div style="float:right">
		        <span>Function : </span>
		        <asp:HyperLink ID="ProcessNostroDaily" NavigateUrl="~/Nostro/frm_NTDAILY.aspx" Text="Summary Estimate" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		        <asp:HyperLink ID="ProcessNostroAdj" NavigateUrl="~/Nostro/frm_NTADJUST.aspx" Text="Adjust Nostro" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		        <asp:HyperLink ID="ProcessNUPDCheck" NavigateUrl="~/Nostro/frm_NUPDCHECK.aspx" Text="Check NUPD" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;		        
		    </div>     
		    <div>&nbsp;</div>
		    <div>&nbsp;</div>
		    <br />	
		    <div style="color:Red;font-size:13px;">* Purple Highlight is Currency Holiday Date</div>     			  	
			<div class="half-size-column fl">			
				   <asp:GridView ID="GridViewNostro" CssClass="GridViewStyle"  AutoGenerateColumns="False" 
				   runat="server"  OnRowDataBound="GridViewNostro_RowDataBound">
                    <Columns> 
                    <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <img class="showbycost" src="../Theme/icon/icon-tracking.png" style="cursor:pointer"/>
                                    <asp:Panel ID="NostroByCost" runat="server" Style="display:none">
                                        <asp:GridView ID="GridNostroByCost" Width="100%" CellPadding="0" CellSpacing="0" DataKeyNames="" CssClass="SubGridViewStyle" AllowPaging="False" 
                                        AutoGenerateColumns="False" runat="server" OnRowDataBound="GridNostroByCost_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="PORT" ItemStyle-Width="99" ItemStyle-HorizontalAlign="Center" /> 
                                                <asp:BoundField DataField="T1" ItemStyle-Width="100"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/> 
                                                <asp:BoundField DataField="T2" ItemStyle-Width="100"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/> 
                                                <asp:BoundField DataField="T3" ItemStyle-Width="100"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/> 
                                                <asp:BoundField DataField="T4" ItemStyle-Width="100"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                                               
                                            </Columns>                    
                                            <EmptyDataTemplate>
                                                No Data
                                            </EmptyDataTemplate>                    
                                            <RowStyle CssClass="SubRowStyle" />                                          
                                            <AlternatingRowStyle CssClass="SubAltRowStyle" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </ItemTemplate>
                    </asp:TemplateField> 
                    
                    <asp:BoundField DataField="CURR"  HeaderText ="CCY"  ItemStyle-Width="100" ItemStyle-Font-Bold="true" /> 
                    <asp:BoundField  DataField="BANKSTR"  HeaderText ="BANK" ItemStyle-Width="100"  ItemStyle-Font-Bold="true" />
                    <asp:BoundField  DataField="ACCTNO"  HeaderText ="NOSTRO ACCT" ItemStyle-Width="120" ItemStyle-Font-Bold="true" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide"  />                  
                    <asp:BoundField DataField="BALD1"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" ItemStyle-Width="100" ItemStyle-Font-Bold="true" />
                     <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderStyle-Font-Names="Arial" HeaderStyle-Font-Size="Medium" HeaderStyle-HorizontalAlign="Center"  ItemStyle-Width="100" ItemStyle-Font-Bold="true" HeaderStyle-BackColor="Green"> 
                        <HeaderTemplate>
                            <asp:LinkButton Text="bal2" ID="bal2" OnClick="show_Click" runat="server" Font-Underline="true" />                            
                        </HeaderTemplate> 
                         <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "BALD2", "{0:n2}")%></ItemTemplate>                                                
                    </asp:TemplateField> 
                     <asp:TemplateField ItemStyle-HorizontalAlign="Right"  HeaderStyle-Font-Names="Arial" HeaderStyle-Font-Size="Medium" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100" ItemStyle-Font-Bold="true"> 
                        <HeaderTemplate>
                            <asp:LinkButton Text="bal3" ID="bal3" OnClick="show_Click" runat="server" Font-Underline="true" />                            
                        </HeaderTemplate> 
                         <ItemTemplate>
                           <%# DataBinder.Eval(Container.DataItem, "BALD3", "{0:n2}")%>
                         </ItemTemplate>                                                
                    </asp:TemplateField> 
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right"  HeaderStyle-Font-Names="Arial" HeaderStyle-Font-Size="Medium" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="100" ItemStyle-Font-Bold="true"> 
                        <HeaderTemplate>
                            <asp:LinkButton Text="bal4" ID="bal4" OnClick="show_Click" runat="server" Font-Underline="true" />                            
                        </HeaderTemplate> 
                         <ItemTemplate>
                           <%# DataBinder.Eval(Container.DataItem, "BALD4", "{0:n2}")%>
                         </ItemTemplate>                                                
                    </asp:TemplateField>
                                
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
		
			</div> <!-- end half-size-column -->

			<!-- sent to mail hidden gridview -->
			<div id="gridemail"  style="display: none;" >			
				   <asp:GridView ID="GridViewNostroMail" AutoGenerateColumns="False" CssClass="GridViewMailStyle" runat="server" OnRowDataBound="GridViewNostroMail_RowDataBound">
                    <Columns>
                    <asp:BoundField DataField="CURR"  HeaderText ="&nbsp;&nbsp;&nbsp;CCY&nbsp;&nbsp;&nbsp;"  HeaderStyle-Width="100px" ItemStyle-Width="100px" /> 
                    <asp:BoundField DataField="BANKSTR"  HeaderText ="&nbsp;&nbsp;&nbsp;BANK&nbsp;&nbsp;&nbsp;"  HeaderStyle-Width="100px" ItemStyle-Width="100px"/>                          
                    <asp:BoundField DataField="BALD2"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"  HeaderStyle-Width="150px" HeaderStyle-BackColor="Green" ItemStyle-Width="100px"/>
                    <asp:BoundField DataField="BALD3"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"  HeaderStyle-Width="150px" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="BALD4"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"  HeaderStyle-Width="150px" ItemStyle-Width="100px"/>                                      
                    </Columns>                    
                    <EmptyDataTemplate>
                        No Data
                    </EmptyDataTemplate> 
                    <HeaderStyle/> 
                    <RowStyle BorderStyle="Solid" />
                                       
                      </asp:GridView>
			</div> 
			<!-- ----------------------- -->
			
			<div><asp:Button ID="Sendmail" runat="server" Text="Sendmail" OnClick = "SendMail" />  &nbsp; <asp:Button ID="btnExportGrid" runat="server" Text="Export To Excel" OnClick = "GridToExcel" /></div>
		
		
		    
		</div> <!-- end content-module-main -->

		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Daily Transactions</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf">
	        <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" />
	        &nbsp;
	       
	        <br />
	        <br />
	        <asp:Label ID="count" runat="server" Font-Names="Arial" Font-Size="Small" />	       	        
			<div class="half-size-column fl">			    
                  <asp:GridView ID="GridDailyTrans" runat="server" AutoGenerateColumns="False" AllowPaging="True">                   
                <Columns> 
                      <asp:BoundField DataField="DEALNO" HeaderStyle-Width="60"  HeaderText ="Deal No."/>   
                    <asp:BoundField DataField="CMNE" HeaderStyle-Width="150" HeaderText ="Customer"/> 
                    <asp:BoundField DataField="PRODCODE" HeaderStyle-Width="60"  HeaderText ="Product" />   
                    <asp:BoundField DataField="PRODTYPE" HeaderStyle-Width="60" HeaderText ="Product Type"/>   
                    <asp:BoundField DataField="CCY" HeaderStyle-Width="50" HeaderText ="CCY"/>
                     <asp:BoundField DataField="CCYAMT" HeaderStyle-Width="50"  HeaderText ="CCY Amount" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />   
                    <asp:BoundField DataField="CTRCCY" HeaderStyle-Width="50" HeaderText ="CTR"/>   
                    <asp:BoundField DataField="CTRAMT" HeaderStyle-Width="50" HeaderText ="CTR Amount"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}"/>
                     <asp:BoundField DataField="VDATE" HeaderStyle-Width="70" HeaderText ="Value Date"/>   
                    <asp:BoundField DataField="DEALDATE" HeaderStyle-Width="60" HeaderText ="Maturity Date"/> 
                    <asp:BoundField DataField="RATE" HeaderStyle-Width="60"  HeaderText ="Rate" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n6}" />   
                </Columns>      
                    <EmptyDataTemplate>Data Not Found</EmptyDataTemplate>               
                    <RowStyle CssClass="RowStyle" />
                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
    
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
