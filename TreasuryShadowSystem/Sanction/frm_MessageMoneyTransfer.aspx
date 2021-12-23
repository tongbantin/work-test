<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_MessageMoneyTransfer.aspx.cs" Inherits="TreasuryShadowSystem.Sanction.frm_MessageMoneyTransfer" Title="Untitled Page" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {  
               
             $(".btnshowdetail").click( function() {
                if(this.tog ^= 1) {  
                    $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                    //$(this).src("../Theme/icon/actions-redo.png");
                    //$('.RowDetail').css('display','block');
                } else {  
                    $(this).closest("tr").next().remove();
                    //$(this).src("../Theme/icon/icon-tracking.png");
                    //$('.RowDetail').css('display','none');
                }
             });   
             
        });
        function showDetail(id)
        {   
            $("#dialog").dialog({
                title: "ธนาคารเกียรตินาคิน จำกัด(มหาชน)",
                width: 900,
                modal: true,
                open: function() {     
                    var result = true;   
                    if(result) {                                     
                        var request = $.ajax({
                            type: "POST",
                            url: '<%=Config.Website %>/Sanction/frm_MessageMoneyTransfer.aspx/GetSanctionDetail',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            data: JSON.stringify({'uid':id}),  
                            success: function (data) {
                                if(data.d.Result=="OK") {  
                                    $('.vuid').val(data.d.Record.uid);
                                    $('.vfirstname').val(data.d.Record.firstName);
                                    $('.vlastname').val(data.d.Record.lastName);
                                    $('.vtitle').val(data.d.Record.title);
                                    $('.vupdatecategorization').val(data.d.Record.updateCategorization);
                                    $('.ventereddate').val(data.d.Record.enteredDate);
                                    $('.vupdateddate').val(data.d.Record.updatedDate);
                                    $('.vcategory').val(data.d.Record.category);
                                    $('.vsubcategory').val(data.d.Record.subCategory);
                                    $('.vei').val(data.d.Record.ei);
                                    $('.vdecreased').val(data.d.Record.decreased);
                                    $('.vdob').val(data.d.Record.dob);
                                    $('.vdateofbirth').val(data.d.Record.dateOfBirth);
                                    $('.vplaceofbirth').val(data.d.Record.placeOfBirth);
                                    $('.vnationality').val(data.d.Record.nationality);
                                    $('.vstreet').val(data.d.Record.location[0].street);
                                    $('.vcity').val(data.d.Record.location[0].city);
                                    $('.vregion').val(data.d.Record.location[0].region);
                                    $('.vpostcode').val(data.d.Record.location[0].postcode);
                                    $('.vcountry').val(data.d.Record.location[0].country);
                                    $('.vpositions').val(data.d.Record.positions);
                                    $('.vageasofdate').val(data.d.Record.ageAsOfDate);
                                    $('.vissuingstate').val(data.d.Record.identification[0].issuingState);
                                    $('.vidtypname').val(data.d.Record.identification[0].idTypeName);
                                    $('.vidnumber').val(data.d.Record.identification[0].idNumber);
                                    $('.valias').val(data.d.Record.alias);
                                    $('.vfurtherinfo').val(data.d.Record.furtherInfo);
                                    $('.vexternalsource').val(data.d.Record.externalSource[0]);
                                } else {
                                    alert(data.d.Message);
                                    $(this).dialog("close");     
                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                var err = JSON.parse(xhr.responseText);
                                alert(err.Message);
                            }
                        });
                    }
                },
                "buttons": {
                    "Close": function() {
                        $(this).dialog("close");                 
                    }
                }
            });                
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">        
        <table>
            <tr>
                <td>UID</td>
                <td>
                    <input type="text" name="vUID" id="vUID" class="vuid" readonly="true" runat="server" />
                </td>
                <td>Firstname</td>
                <td>
                    <input type="text" name="vFirstName" id="vFirstName" class="vfirstname" readonly="true" runat="server" />
                </td>
                <td>Lastname</td>
                <td>
                    <input type="text" name="vLastName" id="vLastName" class="vlastname" readonly="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Title</td>
                <td>
                    <input type="text" name="vTitle" id="vTitle" class="vtitle" readonly="true" runat="server" />
                </td>
                <td>Update Categorization</td>
                <td>
                    <input type="text" name="vUpdateCategorization" id="vUpdateCategorization" class="vupdatecategorization" readonly="true" runat="server" />
                </td>
                <td>Entered Date</td>
                <td>
                    <input type="text" name="vEnteredDate" id="vEnteredDate" class="ventereddate" readonly="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Updated Date</td>
                <td>
                    <input type="text" name="vUpdatedDate" id="vUpdatedDate" class="vupdateddate" readonly="true" runat="server" />
                </td>
                <td>Category</td>
                <td>
                    <input type="text" name="vCategory" id="vCategory" class="vcategory" readonly="true" runat="server" />
                </td>
                <td>SubCategory</td>
                <td>
                    <input type="text" name="vSubCategory" id="vSubCategory" class="vsubcategory" readonly="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td>EI</td>
                <td>
                    <input type="text" name="vEI" id="vEI" class="vei" readonly="true" runat="server" />
                </td>
                <td>Deceased</td>
                <td>
                    <input type="text" name="vDecreased" id="vDecreased" class="vdecreased" readonly="true" runat="server" />
                </td>
                <td>DOB</td>
                <td>
                    <input type="text" name="vDOB" id="vDOB" class="vdob" readonly="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Dates Of Birth</td>
                <td>
                    <input type="text" name="vDateOfBirth" id="vDateOfBirth" class="vdateofbirth" readonly="true" runat="server" />
                </td>
                <td>Place Of Birth</td>
                <td>
                    <input type="text" name="vPlaceOfBirth" id="vPlaceOfBirth" class="vplaceofbirth" readonly="true" runat="server" />
                </td>
                <td>Nationality</td>
                <td>
                    <input type="text" name="vNationality" id="vNationality" class="vnationality" readonly="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Location-Street</td>
                <td>
                    <input type="text" name="vStreet" id="vStreet" class="vstreet" readonly="true" runat="server" />
                </td>
                <td>Location-City</td>
                <td>
                    <input type="text" name="vCity" id="vCity" class="vcity" readonly="true" runat="server" />
                </td>
                <td>Location-Region</td>
                <td>
                    <input type="text" name="vRegion" id="vRegion" class="vregion" readonly="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Location-Postcode</td>
                <td>
                    <input type="text" name="vPostCode" id="vPostCode" class="vpostcode" readonly="true" runat="server" />
                </td>
                <td>Location-Country</td>
                <td>
                    <input type="text" name="vCountry" id="vCountry" class="vcountry" readonly="true" runat="server" />
                </td>
                <td>Positions</td>
                <td>
                    <input type="text" name="vPositions" id="vPositions" class="vpositions" readonly="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Age</td>
                <td>
                    <input type="text" name="vAge" id="vAge" class="vage" readonly="true" runat="server" />
                </td>
                <td>Age AsOf Date</td>
                <td>
                    <input type="text" name="vAgeAsOfDate" id="vAgeAsOfDate" class="vageasofdate" readonly="true" runat="server" />
                </td>
                <td>Identification-Issuing State</td>
                <td>
                    <input type="text" name="vIssuingState" id="vIssuingState" class="vissuingstate" readonly="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td>Idetification-IDTypeName</td>
                <td>
                    <input type="text" name="vIdTypeName" id="vIdTypeName" class="vidtypname" readonly="true" runat="server" />
                </td>
                <td>Identification-IDNumber</td>
                <td>
                    <input type="text" name="vIdNumber" id="vIdNumber" class="vidnumber" readonly="true" runat="server" />
                </td>
                <td>alias</td>
                <td>
                    <input type="text" name="vAlias" id="vAlias" class="valias" readonly="true" runat="server" />
                </td>
            </tr>
            <tr>
                <td>furtherInfo</td>
                <td>
                    <textarea name="vFurtherInfo" id="vFurtherInfo" rows="10" cols="50" class="vfurtherinfo" readonly="true" runat="server"></textarea>
                </td>
                <td>External Source</td>
                <td>
                    <textarea name="vExternalSource" id="vExternalSource" rows="10" cols="50" class="vexternalsource" readonly="true" runat="server" ></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Message Batch</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
					        <table>
	                             <tr>
	                                <td class="td-display">&nbsp;Order No.</td>
	                                <td class="td-field">       
    	                                <asp:TextBox ID="OrderNoBox" ReadOnly="true" CssClass="orderno" runat="server">
    	                                </asp:TextBox>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Order Date</td>
	                                <td class="td-field">           	                                
	                                    <asp:TextBox ID="OrderDateBox" ReadOnly="true" CssClass="orderdate" Runat="server"></asp:TextBox> 
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Correspondent Branch</td>
	                                <td class="td-field">       
    	                                <asp:TextBox ID="CorrespondentBranchBox" ReadOnly="true" CssClass="correspondentbranch" runat="server">
    	                                </asp:TextBox>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Customer Name</td>
	                                <td class="td-field">       
    	                                <asp:TextBox ID="CustomerNameBox" ReadOnly="true" CssClass="customername" runat="server">
    	                                </asp:TextBox>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Customer Country</td>
	                                <td class="td-field">       
    	                                <asp:TextBox ID="CustomerCountryBox" ReadOnly="true" CssClass="customercountry" runat="server">
    	                                </asp:TextBox>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Beneficiary Name</td>
	                                <td class="td-field">       
    	                                <asp:TextBox ID="BeneficiaryNameBox" ReadOnly="true" CssClass="beneficiaryname" runat="server">
    	                                </asp:TextBox>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Message For Beneficiary</td>
	                                <td class="td-field">       
    	                                <asp:TextBox ID="MessageBeneficiaryBox" ReadOnly="true" CssClass="message" runat="server">
    	                                </asp:TextBox>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Account Number</td>
	                                <td class="td-field">       
    	                                <asp:TextBox ID="AccountNumberBox" ReadOnly="true" CssClass="accountnumber" runat="server">
    	                                </asp:TextBox>
	                                </td>
	                            </tr>
	                             <tr>
	                                <td class="td-display">&nbsp;Amount</td>
	                                <td class="td-field">       
    	                                <asp:TextBox ID="AmountBox" ReadOnly="true" CssClass="amount" runat="server">
    	                                </asp:TextBox>
	                                </td>
	                            </tr>
	                            <tr>
	                                <td class="td-display" colspan="2">	                                    
                                        <asp:GridView ID="GridSanction" ShowHeader="true" CssClass="GridViewStyle" AutoGenerateColumns="false" EmptyDataText="Data Not Found" CellPadding="0" CellSpacing="0" BorderWidth="0" runat="server" DataKeyNames="CHECKINGID">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="39" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <img class="btnshowdetail" src="../Theme/icon/icon-tracking.png" style="cursor:pointer"/>                                    
                                                        <asp:Panel ID="AllotmentSpotDetail" runat="server" Style="display:none">
                                                            <asp:GridView ID="GridSanctionDetail" AutoGenerateColumns="false" CellPadding="0" CellSpacing="0" CssClass="GridViewStyle" runat="server">                                            
                                                                <Columns>
                                                                    <asp:BoundField DataField="category" HeaderText="Category" />    
                                                                    <asp:BoundField DataField="subCategory" HeaderText="SubCategory" />
                                                                    <asp:BoundField DataField="types" HeaderText="Types" />
                                                                    <asp:BoundField DataField="keyword" HeaderText="Keyword" />
                                                                    <asp:BoundField DataField="matchedNameType" HeaderText="Matched Name Type" />
                                                                    <asp:BoundField DataField="matchedNameScore" HeaderText="Matched Name Score" />
                                                                    <asp:BoundField DataField="ruleCode" HeaderText="Rule Code" />
                                                                    <asp:BoundField DataField="ruleType" HeaderText="Rule Type" />
                                                                    <asp:BoundField DataField="ruleDesc" HeaderText="Rule Desc" />   
                                                                    <asp:BoundField DataField="sourceWatchlist" HeaderText="Source Watchlist" />                                              
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    No Data
                                                                </EmptyDataTemplate>              
                                                                <EmptyDataRowStyle CssClass="EmptyRowStyle" />
                                                                <PagerStyle CssClass="PagerStyle" />
                                                                <HeaderStyle CssClass="HeaderStyle"/>
                                                                <EditRowStyle CssClass="EditRowStyle" />
                                                                <RowStyle CssClass="RowStyle" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>   
                                                <asp:BoundField DataField="COND_NAME" HeaderText="Condition" ItemStyle-Wrap="false" />
                                                <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-Wrap="false" />
                                                <asp:BoundField DataField="MSG" HeaderText="Message" ItemStyle-Wrap="false" />                                                  
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
	                                </td>
	                            </tr>
	                            <tr>
	                                <td class="td-display">&nbsp;Remark</td>
	                                <td class="td-field">	                                    
                                        <asp:TextBox ID="RemarkBox" TextMode="MultiLine" Width="400" Height="50" runat="server"></asp:TextBox>	                                
	                                </td>
	                            </tr>
					            <tr>
					                <td>
					                    <asp:Button ID="RejectButton" Text="Reject" runat="server" 
                                        OnClientClick="if(!confirm('Do you Reject the transaction?')) return false;" 
                                        class="round button-warning" onclick="RejectButton_Click" />
					                </td>
					                <td colspan="2">
					                    <%--<div style="float:left">
                                        <asp:Button ID="RecheckButton" Text="Recheck" runat="server" 
                                        OnClientClick="if(!confirm('Do you Recheck the transaction?')) return false;" 
                                        class="round button-submit" onclick="RecheckButton_Click" />
                                        </div>   --%> 
                                        <div style="float:right">
                                        <asp:Button ID="AcceptButton" Text="Accept" 
                                        runat="server" 
                                        OnClientClick="if(!confirm('Do you Accept the transaction?')) return false;" 
                                        class="round blue button-submit" onclick="AcceptButton_Click" />  	
                                        </div> 					                    
					                </td>
					            </tr>
					        </table>							
							
					</fieldset>
			
			</div> <!-- end half-size-column -->
		
		</div> <!-- end content-module-main -->
			
	</div>
</asp:Content>
