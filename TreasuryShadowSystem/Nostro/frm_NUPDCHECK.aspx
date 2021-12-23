<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_NUPDCHECK.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_NUPDCHECK" EnableEventValidation="false" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />   
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Check NUPD</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div class="content-module-main cf" >		
		    <div style="float:right">
		        <span>Function : </span>
		        <asp:HyperLink ID="ProcessNostroDaily" NavigateUrl="~/Nostro/frm_NTDAILY.aspx" Text="Summary Estimate" CssClass="round blue button-submit"  runat="server"></asp:HyperLink>
                <asp:HyperLink ID="ProcessNostroAdj" NavigateUrl="~/Nostro/frm_NTADJUST.aspx" Text="Adjust Nostro" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		        <asp:HyperLink ID="ProcessNUPDCheck" NavigateUrl="~/Nostro/frm_NUPDCHECK.aspx" Text="Check NUPD" CssClass="round blue button-submit" runat="server"></asp:HyperLink>
                &nbsp;		        
		    </div>     
		    <div>&nbsp;</div>
		    <div>&nbsp;</div>
		    <br />	   
		    <br />
		    <asp:Label ID="todaydate" runat="server" Font-Names="Arial" Font-Size="Small" />	 
		    <div>&nbsp;</div>  			  	
			<div class="half-size-column fl">			
				   <asp:GridView ID="GridCheckNUPD" DataKeyNames="" CssClass="GridViewStyle"  
                       AutoGenerateColumns="False" runat="server">
                    <Columns> 
                        <asp:BoundField DataField="CURR"  HeaderText ="CCY" ItemStyle-HorizontalAlign="Center"/>    
                        <asp:BoundField DataField="ACCTNO"  HeaderText ="A/C NO."/>
                        <asp:TemplateField HeaderText="COUNT" ItemStyle-HorizontalAlign="Center"> 
                            <ItemTemplate> 
                                 <asp:LinkButton ID="count" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "COUNT")%>' OnCommand="show_Detail" Font-Names="Arial"  Font-Size="Small"></asp:LinkButton>                                 
                            </ItemTemplate>                                                
                        </asp:TemplateField>    
                        <asp:BoundField DataField="NUPDAMT"  HeaderText ="Data from NUPD" ItemStyle-HorizontalAlign="Right"/>
                        <asp:BoundField DataField="FXAMT"  HeaderText ="Data from OPICS" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="GrayText" />

                         <asp:TemplateField HeaderText="DIFF" ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="GrayText"> 
                            <ItemTemplate> 
                                <asp:LinkButton ID="diff" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DIFF")%>'  OnCommand="show_Diff" Font-Names="Arial"  Font-Size="Small" CssClass=""></asp:LinkButton>                              
                            </ItemTemplate>                                                
                        </asp:TemplateField>
                    </Columns>                    
                    <EmptyDataTemplate>
                        No Data
                    </EmptyDataTemplate>                    
                    <RowStyle CssClass="RowStyle"  />
                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle"   />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />  
                    <FooterStyle CssClass="FooterStyle" />                  
                </asp:GridView>
		
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">NUPD Detail</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="showdetail"  class="content-module-main cf"  runat="server">	      
			    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel"/>
			    <br />
                  <asp:GridView ID="GridDetailNUPD" CssClass="GridViewStyle" runat="server" 
                    AutoGenerateColumns="False" AllowPaging="True" >                   
                <Columns> 
                      <asp:BoundField DataField="DEALNO"  HeaderText ="Deal No."/> 
                      <asp:BoundField DataField="CURR"  HeaderText ="CCY"/> 
                      <asp:BoundField DataField="ACCTNO"  HeaderText ="ACCT NO."/>                        
                    <asp:BoundField DataField="CUSTNAME"  HeaderText ="Counter Party"/> 
                    <asp:BoundField DataField="PRODUCT"  HeaderText ="Product" />   
                    <asp:BoundField DataField="TYPE"  HeaderText ="Product Type"/> 
                     <asp:BoundField DataField="AMOUNT"  HeaderText ="Amount" 
                          ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" >   
<ItemStyle HorizontalAlign="Right"></ItemStyle>
                      </asp:BoundField>
                </Columns>      
                    <EmptyDataTemplate>Data Not Found</EmptyDataTemplate>               
                    <RowStyle CssClass="RowStyle" />
                    <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                      <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                </asp:GridView>
			    
			    
			</div> <!-- end half-size-column -->
		
		</div> <!-- end detail-content -->
		
		<!-- start diff content -->		    
		<div id="showdiff" class="content-module-main cf" runat="server">
			    <br />
                  <asp:GridView ID="GridDiff"  runat="server" CssClass="GridViewStyle" 
                    AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" 
                    onrowdatabound="GridDiff_RowDataBound" >                   
                <Columns> 
                      <asp:BoundField DataField="DEALNO"  HeaderText ="Deal No." HeaderStyle-BackColor="GrayText" /> 
                      <asp:BoundField DataField="CURR"  HeaderText ="CCY"  HeaderStyle-BackColor="GrayText" /> 
                      <asp:BoundField DataField="ACCTNO"  HeaderText ="ACCT NO." HeaderStyle-BackColor="GrayText" />                        
                      <asp:BoundField DataField="CUSTNAME"  HeaderText ="Counter Party" HeaderStyle-BackColor="GrayText" /> 
                      <asp:BoundField DataField="PRODUCT"  HeaderText ="Product" HeaderStyle-BackColor="GrayText" />   
                      <asp:BoundField DataField="TYPE"  HeaderText ="Product Type" HeaderStyle-BackColor="GrayText" /> 
                      <asp:BoundField DataField="AMOUNT"  HeaderText ="Amount" ItemStyle-HorizontalAlign="Right" 
                          DataFormatString="{0:n2}" HeaderStyle-BackColor="GrayText" />   
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
		
		
		<!-- end diff-content -->
		
	</div> <!-- end content-module -->
</asp:Content>
