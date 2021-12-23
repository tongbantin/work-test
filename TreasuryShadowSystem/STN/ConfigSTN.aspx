<%@ Page Language="C#"   MasterPageFile="~/Content.Master"  AutoEventWireup="true" CodeBehind="ConfigSTN.aspx.cs" Inherits="TreasuryShadowSystem.STN.ConfigSTN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server"> 
  <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />   
  <style> 
  .ui-dialog{ width:500px !important;}
 ::-webkit-scrollbar-thumb { background-color:#DDD !important; }
 .PagerStyle{ background-color: #FFF !important;}
  </style>
    <script type="text/javascript">
        $(document).ready(function() {

            function submitForm() {


                if ($('#<%=txtstntype.ClientID %>').val() == '') {
                    alert('กรุณากรอกข้อมูล ประเภทหุ้นกู้อนุพันธ์แฝง!!');
                    return false;
                }
                if ($('#<%=txtstntypeshort.ClientID %>').val() == '') {
                    alert('กรุณากรอกข้อมูล ตัวย่อประเภทหุ้นกู้อนุพันธ์แฝง!!');
                    return false;
                }
                if ($('#<%=txtstnname.ClientID %>').val() == '') {
                    alert('กรุณากรอกข้อมูล ชื่อหุ้นกู้อนุพันธ์แฝง!!');
                    return false;
                }

                $('#<%=hdstntype.ClientID %>').val($('#<%=txtstntype.ClientID %>').val());
                $('#<%=hdstntypeshort.ClientID %>').val($('#<%=txtstntypeshort.ClientID %>').val());
                $('#<%=hdstnname.ClientID %>').val($('#<%=txtstnname.ClientID %>').val());
                $('#<%=hdstndesc.ClientID %>').val($('#<%=txtstndesc.ClientID %>').val());
                $('#<%=hdstnreturn.ClientID %>').val($('#<%=txtstnreturn.ClientID %>').val());
                $('#<%=hdstnredeem.ClientID %>').val($('#<%=txtstnredeem.ClientID %>').val());

                $('input#<%=DialogSubmit.ClientID %>').click();


            }

            $(".ic-add").click(function() {
                $('#<%=ActionBox.ClientID %>').val("Add");
                $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 750,
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

  <div id="dialog" style="display: none;">
        <table style="line-height: 2;">
            <tr>
                <td>&nbsp;</td>
                <td>ประเภทหุ้นกู้อนุพันธ์แฝง :</td>
                <td>
                    <asp:TextBox ID="txtstntype" Width="150px" runat="server"></asp:TextBox><span style="color:red">*</span>
                </td>
                
                
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>ตัวย่อประเภทหุ้นกู้อนุพันธ์แฝง :</td>
                <td>
                    <asp:TextBox ID="txtstntypeshort" Width="150px" runat="server"></asp:TextBox><span style="color:red">*</span>
                </td>
                
                
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>ชื่อหุ้นกู้อนุพันธ์แฝง :</td>
                <td>
                    <asp:TextBox ID="txtstnname" Width="250px" TextMode="MultiLine" runat="server"></asp:TextBox><span style="color:red">*</span>
                </td>
                
                
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>รายละเอียดหุ้นกู้อนุพันธ์แฝง :</td>
                <td>
                    <asp:TextBox ID="txtstndesc" Width="350px" Height="200px" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>ผลตอบแทนของหุ้นกู้ที่มีอนุพันธ์แฝง :</td>
                <td>
                    <asp:TextBox ID="txtstnreturn" Width="350px" Height="200px" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr> 
            <tr>
                <td>&nbsp;</td>
                <td>หลักเกณฑ์การไถ่ถอนหุ้นกู้ที่มีอนุพันธ์แฝง</td>
                <td>
                    <asp:TextBox ID="txtstnredeem" Width="350px" Height="200px" TextMode="MultiLine" runat="server" ></asp:TextBox>
                </td>
            </tr>
            
        </table>    
    </div>
   
   <div id="dialogdetail" style="display: none;">
        <table style="line-height: 2;">
            <tr>
                <td>&nbsp;</td>
                <td>ประเภทหุ้นกู้อนุพันธ์แฝง :</td>
                <td>
                    <asp:Label ID="lblstntype" Width="150px" runat="server"></asp:Label> 
                </td>
                 
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>ตัวย่อประเภทหุ้นกู้อนุพันธ์แฝง :</td>
                <td>
                    <asp:Label ID="lblstntypeshort" Width="150px" runat="server"></asp:Label> 
                </td>
                
                
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>ชื่อหุ้นกู้อนุพันธ์แฝง :</td>
                <td>
                    <asp:Label ID="lblstnname" Width="350px" runat="server"></asp:Label> 
                </td>
                
                
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>รายละเอียดหุ้นกู้อนุพันธ์แฝง :</td>
                <td>
                    <asp:Label ID="lblstndesc" Width="350px"  runat="server"></asp:Label> 
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>ผลตอบแทนของหุ้นกู้ที่มีอนุพันธ์แฝง :</td>
                <td>
                    <asp:Label ID="lblstnreturn" Width="350px"  runat="server"></asp:Label> 
                </td>
            </tr> 
            <tr>
                <td>&nbsp;</td>
                <td>หลักเกณฑ์การไถ่ถอนหุ้นกู้ที่มีอนุพันธ์แฝง</td>
                <td>
                    <asp:Label ID="lblstnredeem" Width="350px"  runat="server" ></asp:Label> 
                </td>
            </tr>
            
        </table>    
    </div>
    
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    
    <div class="content-module">
	    <div class="content-module-heading cf">		
			<h3 class="fl">Config STN</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
			<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
						<p>
					        <label for="simple-input">ประเภทหุ้นกู้อนุพันธ์แฝง : </label>
                            
                            <asp:DropDownList ID="productddl" AppendDataBoundItems=true runat="server"></asp:DropDownList>
                            &nbsp;
					        <asp:Button ID="SearchButton" Text="Search" runat="server" 
                                class="round blue button-submit" onclick="SearchButton_Click"/>
						</p>
					</fieldset>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf ">
			<div class="half-size-column fl" style="overflow:auto; width:100%;height:400px;">	
			    <div style="float:left"><button class="round blue button-submit ic-add">Add Config</button></div> 
			    <div style="float:right"></div>
			    <br /><br />		
			    
			    <asp:GridView ID="GridViewConfig" DataKeyNames="DOCID" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DOCID") %>' CommandName="Delete" ImageUrl="~/Theme/icon/actions-delete.png" OnClientClick="return confirm('Are you sure you want to delete?');" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" ItemStyle-Wrap="false"  
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" SelectImageUrl="~/Theme/icon/actions-edit.png" >
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="STN_TYPE" HeaderStyle-Width="70" HeaderText="STN TYPE" > 
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="STN_TYPE_FULLNAME" HeaderStyle-Width="70" HeaderText="STN TYPE FULLNAME" > 
                        <HeaderStyle Width="150px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                         <asp:BoundField DataField="STN_NAME" HeaderStyle-Width="70" HeaderText="STN NAME" > 
                        <HeaderStyle Width="150px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                         <asp:TemplateField>
                            <ItemTemplate>
                                 <asp:LinkButton ID="btnDetail" runat="server" Text="DETAIL" CommandName="DETAIL" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DOCID") %>' ></asp:LinkButton>
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
              <asp:HiddenField ID="ActionBox" runat="server" />
              <asp:HiddenField ID="hdDOCID" runat="server" />
               <asp:HiddenField ID="hdstntype" runat="server" />
                <asp:HiddenField ID="hdstntypeshort" runat="server" />
                <asp:HiddenField ID="hdstnname" runat="server" />
                <asp:HiddenField ID="hdstndesc" runat="server" />
                <asp:HiddenField ID="hdstnreturn" runat="server" />
                <asp:HiddenField ID="hdstnredeem" runat="server" />
                <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" 
                    runat="server" onclick="DialogSubmit_Click" />
			</div> <!-- end half-size-column -->
			 
			
		</div> <!-- end content-module-main -->
		
		 
		 
	 
	</div>

</asp:Content>