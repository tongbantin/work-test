﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR073DBOFIDF.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR073DBOFIDF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function() {

            $('#ctl00_bodyContent_txtFromExecuteDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });


            $('#ctl00_bodyContent_txtToExecuteDate').datepicker({
                altFormat: 'dd/mm/yy',
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true
            });
            
        });
            

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module">
				
	    <div class="content-module-heading cf">
		
			<h3 class="fl">FIDF</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
			<span class="fr expand-collapse-text">Click to collapse</span>
			<span class="fr expand-collapse-text initial-expand">Click to expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
	
			<div class="half-size-column fl">
				
					<fieldset>
					    <table border="0" >
					        <tbody>					            
					            <tr>
					                <td width="150px"><label for="simple-input">Date :</label></td>
							        <td >
							            <input type="text" id="txtFromExecuteDate" runat="server" class="round input-textbox-date" />-
							            <input type="text" id="txtToExecuteDate" runat="server" class="round input-textbox-date" /></td>
					            </tr>
					            <tr>
					                <td><label for="simple-input">Product : </label></td>
							        <td>
							            <asp:DropDownList ID="ProductBox" runat="server">
							                <asp:ListItem Text="Debenture" Value="Debenture"></asp:ListItem>
							                <asp:ListItem Text="DL (Borrowing) & PN" Value="Borrowing"></asp:ListItem>
							                <asp:ListItem Text="Repo" Value="Repo"></asp:ListItem>
							            </asp:DropDownList>  
							        </td>
					            </tr>
					            <tr>
					                <td colspan="2" ><asp:Button ID="btnSubmit" Text="Submit" runat="server" onclick="btnSubmit_Click" class="round blue button-submit" />
					                    <div id="divMsg" style="display:none;">
                                            <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                                        </div>    
					                </td>
					            </tr>
					            
					        </tbody>
					    </table>
					    
					</fieldset>
				
				    
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf">
	
			<div class="half-size-column fl">
			
                <iframe id="iReport" src="<%# TheUrl %>"  width="100%" height="500px" runat="server" frameborder="0" marginwidth="1" ></iframe>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
