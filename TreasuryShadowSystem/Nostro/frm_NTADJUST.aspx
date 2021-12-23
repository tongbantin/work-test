<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_NTADJUST.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_NTADJUST" EnableEventValidation="false" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
      <script type="text/javascript">
          $(document).ready(function() {

              $('.matdatebox').datepicker({
                  altFormat: 'dd/mm/yy',
                  dateFormat: 'dd/mm/yy',
                  changeMonth: true,
                  changeYear: true
              });
              $('.valuedatebox').datepicker({
                  altFormat: 'dd/mm/yy',
                  dateFormat: 'dd/mm/yy',
                  changeMonth: true,
                  changeYear: true
              });

              $('.searchVDate').datepicker({
                  altFormat: 'dd/mm/yy',
                  dateFormat: 'dd/mm/yy',
                  changeMonth: true,
                  changeYear: true
              });

              

              var originalContentSeries = $("#dialog").html();

              $(".ic-add").click(function() {
                  $("#dialog").dialog({
                      title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                      width: 500,
                      modal: true,
                      open: function() {
                        $("input[name=PeriodAdj][value='D']").prop('checked', true); 
                      },
                      close: function() {
                          $(this).dialog("close");
                          clearBoxVal();
                      },
                      "buttons": {
                      "Submit": function() {
                          
                              var result = true;
                              if ($('.acctBox').val().trim() == '') {
                                  alert('Account No. is require!!');
                                  result = false;
                              }
                              else if ($('.adjustAmt').val().trim() == '') {
                                  alert('Adjust Amount is require!!');
                                  result = false;
                              }
                              else if ($('.valuedatebox').val().trim() == '') {
                                  alert('Value Date is require!!');
                                  result = false;
                              }

                              if (result) {
                                  var record = {
                                      ADJ_CCY: $('.currBox').val(),
                                      ADJ_ACCTNO: $('.acctBox').val(),
                                      ADJ_PRODTYPE: $('#<%=ProdTypeBox.ClientID %>').val(),
                                      ADJ_COST: $('#<%=CostBox.ClientID %>').val(),
                                      ADJ_AMT: $('.adjustAmt').val(),
                                      ADJ_VDATE: $.datepicker.parseDate('dd/mm/yy', $('.valuedatebox').val()),
                                      ADJ_MDATE: $.datepicker.parseDate('dd/mm/yy', $('.matdatebox').val()),
                                      ADJ_REMARK: $('.remarkBox').val(),
                                      ADJ_PD: $('input[name=PeriodAdj]:checked').val()
                                  };

                                  var request = $.ajax({
                                      type: "POST",
                                      url: '<%=Config.Website %>/Nostro/frm_NTADJUST.aspx/AddNostroAdj',
                                      contentType: "application/json; charset=utf-8",
                                      dataType: "json",
                                      data: JSON.stringify({ 'record': record }),
                                      success: function(data) {
                                          if (data.d.Result == "OK") {
                                              alert("Add Adject Nostro Complete");
                                              $("#<%=btnSubmit.ClientID %>").click();
                                          } else {
                                              alert(data.d.Message);
                                          }
                                      },
                                      error: function(xhr, ajaxOptions, thrownError) {
                                          var err = JSON.parse(xhr.responseText);
                                          alert(err.Message);
                                      }
                                  });
                              }
                          },
                          "Close": function() {
                              $(this).dialog("close");
                              clearBoxVal();

                          }
                      }
                  });
                  return false;
              });

              $(".editadj").click(function() {
                  var adjid = $(this).attr('alt');
                  $("#dialog").dialog({
                      title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                      width: 1100,
                      modal: true,
                      open: function() {
                          $.ajax({
                              type: "POST",
                              url: '<%=Config.Website %>/Nostro/frm_NTADJUST.aspx/GetAdjustRow',
                              contentType: "application/json; charset=utf-8",
                              dataType: "json",
                              data: JSON.stringify({ 'ADJ_ID': adjid }),
                              success: function(data) {
                                var obj = data.d;
                                     
                                  $('.currBox').val(obj[0].ADJ_CCY); 
                                  
                                  $('#<%=ProdTypeBox.ClientID %>').val(obj[0].ADJ_PRODTYPE);
                                  $('#<%=CostBox.ClientID %>').val(obj[0].ADJ_COST);
                                  $('.adjustAmt').val(obj[0].ADJ_AMT);
                                  $('.valuedatebox').val(obj[0].ADJ_VDATE_STR);
                                  $('.matdatebox').val(obj[0].ADJ_MDATE_STR);
                                  $('.remarkBox').val(obj[0].ADJ_REMARK);
                                  $("input[name=PeriodAdj][value=" + obj[0].ADJ_PD + "]").prop('checked', true);

                                var a = "";
                                  $('.acctBox').children('option:not(:first)').remove();
                                  $('.acctBox option:first').remove();
                                  a = '<option value="' + obj[0].ADJ_ACCTNO + '|' + obj[0].ADJ_CUST + '">' + obj[0].ADJ_ACCTNO + '</option>';
                                  $('.acctBox').append(a);
 
                              },
                              error: function(xhr, ajaxOptions, thrownError) {
                                  var err = JSON.parse(xhr.responseText);
                                  alert(err.Message);
                              }
                          });
                      },
                      close: function() {
                          $(this).dialog("close");
                          clearBoxVal();

                      },
                      "buttons": {
                          "Submit": function() {
                              var result = true;
                              if ($('.acctBox').val().trim() == '') {
                                  alert('Account No is require!!');
                                  result = false;
                              }
                              else if ($('.adjustAmt').val().trim() == '') {
                                  alert('Adjust Amount is require!!');
                                  result = false;
                              }                             
                              else if ($('.valuedatebox').val().trim() == '') {
                                  alert('Value Date is require!!');
                                  result = false;
                              }                              

                              if (result) {
                                  var record = {
                                      
                                      ADJ_CCY: $('.currBox').val(),
                                      ADJ_ACCTNO: $('.acctBox').val(),
                                      ADJ_PRODTYPE: $('#<%=ProdTypeBox.ClientID %>').val(),
                                      ADJ_COST: $('#<%=CostBox.ClientID %>').val(),
                                      ADJ_AMT: $('.adjustAmt').val(),
                                      ADJ_VDATE: $.datepicker.parseDate('dd/mm/yy', $('.valuedatebox').val()),
                                      ADJ_MDATE: $.datepicker.parseDate('dd/mm/yy', $('.matdatebox').val()),
                                      ADJ_REMARK: $('.remarkBox').val(),
                                      ADJ_PD: $('input[name=PeriodAdj]:checked').val(),
                                      ADJ_ID: adjid                
                                  };

                                  var request = $.ajax({
                                      type: "POST",
                                      url: '<%=Config.Website %>/Nostro/frm_NTADJUST.aspx/UpdateNostroAdj',
                                      contentType: "application/json; charset=utf-8",
                                      dataType: "json",
                                      data: JSON.stringify({ 'record': record }),
                                      success: function(data) {
                                          if (data.d.Result == "OK") {
                                              alert("Update Series Complete");
                                              $("#<%=btnSubmit.ClientID %>").click();
                                              
                                          } else {
                                              aler(data.d.Message);
                                          }
                                      },
                                      error: function(xhr, ajaxOptions, thrownError) {
                                          var err = JSON.parse(xhr.responseText);
                                          alert(err.Message);
                                      }
                                  });
                              }

                          },
                          "Close": function() {
                              $(this).dialog("close");
                              clearBoxVal();

                          }
                      }
                  });
                  return false;
              });

              $(".deleteadj").click(function() {
                  if (confirm('Are you sure you want to delete?')) {
                      var adjid = $(this).attr('alt');
                      $.ajax({
                          type: "POST",
                          url: '<%=Config.Website %>/Nostro/frm_NTADJUST.aspx/DeleteNostroAdj',
                          contentType: "application/json; charset=utf-8",
                          dataType: "json",
                          data: JSON.stringify({ 'ADJ_ID': adjid }),
                          success: function(data) {
                              if (data.d.Result == "OK") {
                                  alert("Delete Adjust Transaction Complete");
                                  $("#<%=btnSubmit.ClientID %>").click();
                              } else {
                                  alert(data.d.Message);
                              }
                          },
                          error: function(xhr, ajaxOptions, thrownError) {
                              var err = JSON.parse(xhr.responseText);
                              alert(err.Message);
                          }
                      });
                  } return false;
              });


              $(".currBox").change(function() {
                  var currval = $(".currBox").val();
                  var request = $.ajax({
                      type: "POST",
                      url: '<%=Config.Website %>/Nostro/frm_NTADJUST.aspx/SecAcctList',
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      data: JSON.stringify({ 'CURRVAL': currval }),
                      success: function(data) {
                          $('.acctBox').html("");
                          $.each(data.d.records, function(idx, obj) {
                              $('.acctBox').append(
                              $('<option></option>').val(obj.Key).html(obj.Value)
                             );
                          });
                      },
                      error: function(xhr, ajaxOptions, thrownError) {
                          var err = JSON.parse(xhr.responseText);
                          alert(err.Message);
                      }
                  });
              });

          });
             
    </script>
     <script type="text/javascript">

         function clearBoxVal() {

             $('.currBox').val("");
             $('.acctBox').val("");
             $('#<%=CostBox.ClientID %>').val("");
             $('#<%=ProdTypeBox.ClientID %>').val("");
             $('.adjustAmt').val("");
             $('.valuedatebox').val("");
             $('.matdatebox').val("");
             $('.remarkBox').val("");

         }
        
    
    </script> 
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">

  <div id="dialog" style="display:none;">
        <table>
            <tr>               
                <td>CCY</td>
                <td>
                    <select id="CurrBox" name="CurrBox" class="currBox" runat="server">
                    </select>                    
                </td>
                <td>
                    <input type="radio"  name="PeriodAdj" class="periodAdj" value="P"  /> Previous &nbsp;&nbsp;
                    <input type="radio"  name="PeriodAdj" class="periodAdj" value="D" checked="checked" /> Daily
                </td>
            </tr>
            <tr>               
                <td>Account No.</td>                 
                <td colspan="2"><select id="AcctBox" name="AcctBox" class="acctBox" runat="server">
                    </select>
                </td>
            </tr>
            <tr>                
                <td><div>Costcenter</div></td>
                <td colspan="2">
                    <asp:DropDownList ID="CostBox" runat="server">
                        <asp:ListItem Text="ALL" Value=""></asp:ListItem>
                        <asp:ListItem Text="Trading" Value="800"></asp:ListItem>
                        <asp:ListItem Text="Banking" Value="340"></asp:ListItem>                       
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>                
                <td>Product Type</td>
                <td colspan="2">
                    <asp:DropDownList ID="ProdTypeBox" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>                
                <td>Amount</td>
                <td colspan="2"><input type="text" id="AdjustAmt" class="adjustAmt" style="width:200px;" runat="server" /></td>
            </tr>
            <tr>                
                <td>Value Date</td>
                <td colspan="2"> <input type="text" name="Valuedatebox" id="Valuedatebox" class="valuedatebox" runat="server" /></td>
            </tr>
            <tr>                
                <td>Maturity Date</td>
                <td colspan="2"> <input type="text" name="MaturityDateBox" id="MaturityDateBox" class="matdatebox" runat="server" /></td>
            </tr>
            <tr>                
                <td>Remark</td>
                <td colspan="2"><input type="text" id="RemarkBox" class="remarkBox" style="width:300px;" runat="server" />                
                </td>
            </tr>
        </table>   
    </div>
   
    
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Nostro Adjust</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">		
		    <div style="float:right">
		        <span>Function : </span>
		        <asp:HyperLink ID="ProcessNostroDaily" NavigateUrl="~/Nostro/frm_NTDAILY.aspx" Text="Summary Estimate" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		        <asp:HyperLink ID="ProcessNostroAdj" NavigateUrl="~/Nostro/frm_NTADJUST.aspx" Text="Adjust Nostro" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;
		        <asp:HyperLink ID="ProcessNUPDCheck" NavigateUrl="~/Nostro/frm_NUPDCHECK.aspx" Text="Check NUPD" CssClass="round blue button-submit" runat="server"></asp:HyperLink>&nbsp;		        
		    </div>     
		    <br />	   
		    	    			  	
			<div id="Div1" class="content-module-main cf">
			<div class="half-size-column fl">					
                <fieldset>
	                <table border="0" style="width:600px;">
	                    <tbody>           	                        	            
	                         <tr>
	                            <td class="td-display" style="width:200px;">Value Date</td>	                            
	                            <td class="td-field" style="width:200px;"><input type="text" name="SearchVDate" id="SearchVDate" class="searchVDate" runat="server" /></td>
	                            <td align="center" style="width:200px;">
                                    <asp:Button ID="btnSubmit" Text="Search" runat="server" 
                                        class="round blue button-submit" onclick="btnSubmit_Click"/></td>
	                        </tr>    	            
	                    </tbody>
	                </table>
	            </fieldset>
			    </br>
			    <button class="round blue button-submit ic-add">Adjust Nostro</button>
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
		
		</div> <!-- end content-module-main -->
		
		<div class="content-module-heading-result cf">
		
			<h3 class="fl">Result</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>
		
		</div> <!-- end content-module-heading -->
		
		<div id="result-content" class="content-module-main cf">
	        <br />	        	       	        
			<div class="half-size-column fl">			    
                  <asp:GridView ID="GridAdjTrans" runat="server" AutoGenerateColumns="False" AllowPaging="True">                   
                <Columns> 
                    <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <img src="../Theme/icon/actions-edit.png" class="editadj" alt="<%# DataBinder.Eval(Container.DataItem, "ADJ_ID") %>" style="cursor:pointer" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <img src="../Theme/icon/actions-delete.png" class="deleteadj" alt="<%# DataBinder.Eval(Container.DataItem, "ADJ_ID") %>" style="cursor:pointer" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CURR" HeaderStyle-Width="60"  HeaderText ="CCY"/>   
                    <asp:BoundField DataField="ACCTNO" HeaderStyle-Width="100" HeaderText ="Account No."/> 
                    <asp:BoundField DataField="COST" HeaderStyle-Width="60"  HeaderText ="Costcenter" />   
                    <asp:BoundField DataField="PRODTYPE" HeaderStyle-Width="120" HeaderText ="Product Type"/>   
                    <asp:BoundField DataField="BALANCE" HeaderStyle-Width="50" HeaderText ="Amount"  ItemStyle-HorizontalAlign="Right" DataFormatString="{0:n2}" />
                    <asp:BoundField DataField="VDATE" HeaderStyle-Width="100"  HeaderText ="Value Date"/>   
                    <asp:BoundField DataField="MDATE" HeaderStyle-Width="120" HeaderText ="Maturity Date"/> 
                    <asp:BoundField DataField="MARK" HeaderStyle-Width="120" HeaderText ="Remark"/>   
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
