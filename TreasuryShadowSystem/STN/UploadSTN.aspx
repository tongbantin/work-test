<%@ Page Language="C#"   MasterPageFile="~/Content.Master"  AutoEventWireup="true" CodeBehind="UploadSTN.aspx.cs" Inherits="TreasuryShadowSystem.STN.UploadSTN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server"> 
  <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />   
  <style> 
  .ui-dialog{ width:500px !important;}
 ::-webkit-scrollbar-thumb { background-color:#DDD !important; }
 .PagerStyle{ background-color: #FFF !important;}
  </style>
    <script type="text/javascript">
        $(document).ready(function() {

            $('#ctl00_bodyContent_DateBox').datepicker({
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
			<h3 class="fl">Import STN</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
		    
		
			<div class="half-size-column fl">
					<fieldset>
					     
					    <div style="float:left;padding: 5px">
						    <div style="float:left;width:150px;">File upload :</div>
						    <div style="float:left;">
						        <asp:FileUpload ID="FileUpload1" runat="server" />
						    </div>
						    <div style="float:left;width:100px; ">
						    <asp:Button ID="Button1" Text="Submit" runat="server" 
                                class="round blue button-submit" onclick="btnSubmit_Click" />
					        <div id="div1" style="display:none;">
                                <img src="../Theme/Default/Images/loading.gif" alt="Please wait.." />
                            </div>   
						</div>
						</div>
						
					</fieldset>
			
			</div> <!-- end half-size-column -->
			 
		</div> <!-- end content-module-main -->	
		
		 
	 
	</div> <!-- end content-module -->
</asp:Content>