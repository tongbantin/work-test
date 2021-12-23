<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_EncryptUpload.aspx.cs" Inherits="TreasuryShadowSystem.Encrypt.frm_EncryptUpload" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server"> 
      <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/ss_style.css") %>" type="text/css" />
      
      <script type="text/javascript">

        
       function ViewPassword() {
       
            var pass;
            if(document.getElementById('<%=selProdtype.ClientID %>').checked)
            {
                var ddl = document.getElementById("<%=CustomerDDL.ClientID %>");
                var cust = ddl.options[ddl.selectedIndex].value;
            
                var type = document.getElementById("<%=ProdTypeDDL.ClientID %>");
                var prod = type.options[type.selectedIndex].value.split("|");
            
                pass = cust+prod[1];
            }else{
                pass = document.getElementById("<%=fillPassword.ClientID %>").value;
            }
             
             $("#alertPass").html(pass);
             $("#dialog").dialog({
                    title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                    width: 300,
                    modal: true,
                    "buttons": {                        
                        "Close": function() {
                            $(this).dialog("close");
                     }
                    }
                });
            
            //alert(pass);
        }
    </script>
    <style type="text/css">
        
        .searchBtn
        {
        	
        	cursor:pointer; 
        	padding: 2px; 
        	border: 1px solid black; 
        	background-color: Blue;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
     <div id="dialog" style="display: none;"><span id="alertPass"></span></div>
    <div>
	    <div class="content-module-heading cf">		
			<h3 class="fl">Upload Encrypt Files</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<!-- test -->
		<div id="Div2" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
					        <table>
					            <tr>
					                <td>
					                 <asp:RadioButton ID="selProdtype" runat="server" Text=" " GroupName="encryptType"  Checked />
					                <!--<input type="radio"  name="enType" class="entyperadio" value="SYS"  checked="checked"/>&nbsp;Customer&nbsp;-->
					                </td>
					                <td>Customer</td>
					                <td>
					                <asp:DropDownList ID="Source" runat="server" AutoPostBack="true" onselectedindexchanged="btnSelSource_Click" >
                                        <asp:ListItem Text="ALL" Value="all"></asp:ListItem>
                                        <asp:ListItem Text="OPICS" Value="opic"></asp:ListItem>
                                        <asp:ListItem Text="TSS" Value="tss"></asp:ListItem>
							        </asp:DropDownList>
					                <asp:TextBox ID="searchCustomer" runat="server"></asp:TextBox>
					                <asp:ImageButton runat="server" ImageUrl="~/Theme/icon/icon-search.png"  CssClass="searchBtn"  OnClick="btnSearch_Click" />
					                <asp:DropDownList ID="CustomerDDL"  runat="server"></asp:DropDownList></td>
					                <td align="left">&nbsp;Product Type&nbsp;</td>
					                <td><asp:DropDownList ID="ProdTypeDDL" runat="server"></asp:DropDownList></td>
					                <td></td>
					            </tr>
					            <tr>
					                <td>
					                <asp:RadioButton ID="selInput" runat="server" Text=" " GroupName="encryptType"/>
					                <!--<input type="radio"  name="encryptMode" class="" value="MAN" />&nbsp;Password&nbsp;-->
					                </td>
					                <td>Password</td>
					                <td><asp:TextBox ID="fillPassword" runat="server"></asp:TextBox></td>
					                <td align="left">&nbsp;Send to Email&nbsp;</td>
					                <td><asp:TextBox ID="fillEmail" runat="server"></asp:TextBox></td>
					                <td></td>
					            </tr>
					            <tr>
					                <td colspan="2">&nbsp;Files Upload&nbsp; </td>
					                <td>
                                        <asp:FileUpload multiple="multiple"  ID="FileUpload" runat="server"/>
					                </td>
					                <td colspan="3"><input type="button" id="viewPassowrd" value="View Password"  
					                                class="round blue button-submit" onclick="ViewPassword()" />
					                    <!--<asp:Button ID="viewPassword" Text="View Password"
					                        class="round blue button-submit" OnClientClick="ViewPassword()" /> -->
                                         &nbsp;&nbsp;   
                                         <asp:Button ID="sendUpload" Text="Upload & Send" runat="server"
                                            class ="round blue button-submit" onclick="btnSubmit_Click" />
                                    </td>                                    
					                                                  
					            </tr>					            
					        </table>						
					</fieldset>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->

		<!-- test -->

	</div> <!-- end content-module -->
	
</asp:Content>
