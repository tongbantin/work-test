<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TreasuryShadowSystem.Login" %>

<%@ Register src="UserControl/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <title>Treasury Support System(TSS) -- Kiatnakin Bank Public Co., Ltd.</title>
	<meta http-equiv="description" content="Treasury Support System(TSS) -- Kiatnakin Bank Public Co., Ltd." />
    <meta http-equiv="content-type" content="text/html; charset=windows-874" />
    <!--<link rel="stylesheet" href="Theme/Default/Css/style.css" type="text/css" />-->
    <%--<link rel="stylesheet" href="Theme/Default/Css/ss_style.css" type="text/css" />--%>
    <link rel="stylesheet" href="Theme/Default/Css/bootstrap.css" type="text/css" /> 
    <link href="Theme/cupertino/jquery-ui-1.10.0.custom.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Theme/Default/Javascript/jquery-1.8.2.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Theme/Default/Javascript/jquery.html5-placeholder.js") %>"></script>

    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Scripts/jquery.blockUI.js") %>" ></script>
    
    
    <script type="text/javascript" src="<%= Page.ResolveClientUrl("~/Theme/Default/Javascript/bootstrap.js") %>"></script>
    
    <%--<script>
        //insert or remove DOM nodes here
        //...
        var ie = $.browser.msie && typeof window['XMLHttpRequest'] !== "object";
        if (ie)
            $(window).resize();
    </script>--%>
    
    <!-- Optimize for mobile devices -->
	<meta name="viewport" content="width=device-width, initial-scale=1.0"/>  
	<style>
	    .content-wrap{
            background-color: #403a6a;
        }
        .font-white {
                color:#FFF;
        }
        .logo {
            text-align:center;
            margin-bottom: 2%;
        }
	</style>
<script type="text/javascript">

    function blockPage() {
        $.blockUI({
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#000',
                'border-radius': '10px',
                opacity: .5,
                color: '#fff'
            }
        });
    }

    function unblockPage() {
        $.unblockUI();
    }

    function showMessage(t) {
        alert(t);
    }

    $(document).ready(function() {

        $('#btnSubmit').click(function() {
            //$(this).attr('disabled', 'disabled');
            $('#divMsg').show();
            return true;
        });

        $('.input').keypress(function(e) {
            if (e.which == 13) {
                $(this).blur();
                $('#btnSubmit').focus().click();
                //alert('enter');
            }
        });
    });
     
</script>
    
</head>
<body class="content-wrap font-white">
     

    <!-- TOP BAR -->
	<%--<div id="top-bar">
		
		<div class="page-full-width">
		
			<a href="#" class="round button dark ic-left-arrow image-left ">Return to website</a>

		</div> 
		
	
	</div> --%>
	<!-- end top-bar -->
	
	
	
	<!-- HEADER -->
	<%--<div id="header">
		
		<div class="page-full-width cf">
			<div id="login-intro" class="fl">
			    
				<h1>Login to Treasury Support System</h1>
				<h5>Enter your credentials below</h5>
			
			</div>  
			
			<a href="#" id="company-branding" class="fr"><img src="Theme/Default/Images/kklogo.jpg" alt="Kiatnakin Bank" /></a>
			
		</div>  

	</div>--%>
	<!-- end header -->
	
	
	<!-- MAIN CONTENT -->
	<div  class="container">
	    <div class="row" style="height:100px">&nbsp;</div>
	    <div class="row">
	        <div class="span4">&nbsp;</div>
	        <div class="span4">
	            <div class="row">	        
                    <asp:Image ID="Image1" ImageUrl="~/Theme/Default/Images/logo_kkp.png" Width="552" Height="120" runat="server" />
	            </div>
	            <div class="row" style="height:50px;">&nbsp;</div>
	            <div class="row">
	                <div id="divMsg" style="display:none; position:absolute; left:450px; width:1000px; z-index:99">
                        <img src="Theme/Default/Images/loading_image.gif" alt="Please wait.." />
                    </div>             
                      
                    <form id="loginform" runat="server" class="form-horizontal"> 
                        <div class="control-group">
                            <div class="controls">
                                <label class="control-label">Treasury Shadow System</label>
                            </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="login-username">username</label>
                        <div class="controls">
                          <input type="text" id="loginusername" runat="server" autofocus />
                        </div>
                        </div>
                        <div class="control-group">
                            <label class="control-label" for="inputPassword">Password</label>
                        <div class="controls">
                          <input type="password" id="loginpassword" runat="server" />
                        </div>
                        </div>
                        <div class="control-group">
                            <div class="controls"> 
                              <asp:Button ID="btnSubmit" Text="Submit" runat="server"  onclick="btnSubmit_Click" /> 
                            </div>
                        </div> 
                        <br/>
                        <div class="error-box round" id="divError" runat="server">
                            <asp:Label ID="lbMessage" runat="server"></asp:Label>
                        </div>
		            </form>
                </div>    
	        </div>
	        <div class="span4">&nbsp;</div>
	    </div>
	    
    </div> <!-- end content -->
   
    
    <!-- FOOTER -->
	<%--<div id="footer">

        <uc1:Footer ID="Footer1" runat="server" />
		
	
	</div> --%><!-- end footer -->

</body>
</html>
