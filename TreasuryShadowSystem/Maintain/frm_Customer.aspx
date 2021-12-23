<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_Customer.aspx.cs" Inherits="TreasuryShadowSystem.Maintain.frm_Customer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Theme/Default/Css/aspnet.css") %>" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {

            function submitForm() {
                if ($('#<%=NameThBox.ClientID %>').val() == '') {
                    alert('Name (Th) is require!!');
                    return false;
                }
                if ($('#<%=NameEnBox.ClientID %>').val() == '') {
                    alert('Name (En) is require!!');
                    return false;
                }
                if ($('#<%=AddressNoBox.ClientID %>').val() == '') {
                    alert('Address No. is require!!');
                    return false;
                }
                if ($('#<%=AmphurBox.ClientID %>').val() == '') {
                    alert('Name (En) is require!!');
                    return false;
                }
                if ($('#<%=ProvinceBox.ClientID %>').val() == '') {
                    alert('Province is require!!');
                    return false;
                }
                if ($('#<%=PostalCodeBox.ClientID %>').val() == '') {
                    alert('Postal Code is require!!');
                    return false;
                }

                $('#<%=CustomerType.ClientID %>').val($('#<%=CustomerTypeBox.ClientID %>').val());
                $('#<%=Code.ClientID %>').val($('#<%=CodeBox.ClientID %>').val());
                $('#<%=NameTh.ClientID %>').val($('#<%=NameThBox.ClientID %>').val());
                $('#<%=NameEn.ClientID %>').val($('#<%=NameEnBox.ClientID %>').val());
                $('#<%=SegregateAcct.ClientID %>').val($('#<%=SegregateAcctBox.ClientID %>').val());
                $('#<%=CustodianID.ClientID %>').val($('#<%=CustodianBox.ClientID %>').val());
                $('#<%=AssetCompany.ClientID %>').val($('#<%=AssetCompanyBox.ClientID %>').val());
                $('#<%=TaxID.ClientID %>').val($('#<%=TaxIDBox.ClientID %>').val());
                $('#<%=Address.ClientID %>').val($('#<%=AddressBox.ClientID %>').val());
                $('#<%=GIIN.ClientID %>').val($('#<%=GIINBox.ClientID %>').val());
                $('#<%=Contact.ClientID %>').val($('#<%=ContactBox.ClientID %>').val());
                $('#<%=Tel.ClientID %>').val($('#<%=TelBox.ClientID %>').val());
                if ($('#<%=IncVatBox.ClientID %>').is(':checked'))
                    $('#<%=IncVat.ClientID %>').val(1);
                else
                    $('#<%=IncVat.ClientID %>').val(0);
                $('#<%=BuildingName.ClientID %>').val($('#<%=BuildingNameBox.ClientID %>').val());
                $('#<%=FloorNo.ClientID %>').val($('#<%=FloorNoBox.ClientID %>').val());
                $('#<%=RoomNo.ClientID %>').val($('#<%=RoomNoBox.ClientID %>').val());
                $('#<%=VillageName.ClientID %>').val($('#<%=VillageNameBox.ClientID %>').val());
                $('#<%=AddressNo.ClientID %>').val($('#<%=AddressNoBox.ClientID %>').val());
                $('#<%=MooNo.ClientID %>').val($('#<%=MooNoBox.ClientID %>').val());
                $('#<%=Soi.ClientID %>').val($('#<%=SoiBox.ClientID %>').val());
                $('#<%=StreetName.ClientID %>').val($('#<%=StreetNameBox.ClientID %>').val());
                $('#<%=Tumbon.ClientID %>').val($('#<%=TumbonBox.ClientID %>').val());
                $('#<%=Amphur.ClientID %>').val($('#<%=AmphurBox.ClientID %>').val());
                $('#<%=Province.ClientID %>').val($('#<%=ProvinceBox.ClientID %>').val());
                $('#<%=PostalCode.ClientID %>').val($('#<%=PostalCodeBox.ClientID %>').val());
                $('#<%=TargetBank.ClientID %>').val($('#<%=TargetBankBox.ClientID %>').val());
                $('#<%=TargetAcct.ClientID %>').val($('#<%=TargetAcctBox.ClientID %>').val());
                if($('[name$=custTypeRadioList]:checked').val())
                {
                    let custType = $('[name$=custTypeRadioList]:checked').val();
                    if(custType.toUpperCase() == "SELLER")
                    {
                        $('#<%=Seller.ClientID %>').val("1");
                    }
                    else
                    {
                        $('#<%=Issuer.ClientID %>').val("1");
                        $('#<%=IssuerType.ClientID %>').val($('#<%=IssuerTypeBox.ClientID %>').val());
                    }
                }
                $('#<%=IssuerRemark.ClientID %>').val($('#<%=IssuerRemarkBox.ClientID %>').val());
                if ($('#<%=AnnualRegistBox.ClientID %>').is(':checked'))
                    $('#<%=AnnualRegist.ClientID %>').val(1);
                else
                    $('#<%=AnnualRegist.ClientID %>').val(0);
                $('#<%=AnnualRegistText.ClientID %>').val($('#<%=AnnualRegistTextBox.ClientID %>').val());
                $('input#<%=DialogSubmit.ClientID %>').click();
            }
            
            $('#<%=IssuerTypeBox.ClientID %>').change(function() { 
                if ($('#<%=IssuerTypeBox.ClientID %>').val()=="Other")
                    $('#<%=IssuerRemarkBox.ClientID %>').css("display","block");
                else
                    $('#<%=IssuerRemarkBox.ClientID %>').css("display","none");
                    
            });

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
            
            $('[name$=custTypeRadioList]').change(function(){
                let issuerType = $(this).val();
                if(issuerType.toUpperCase() == "SELLER")
                {
                    $('[id$=IssuerTypeBox]').attr("disabled", true);
                }
                else
                {
                    $('[id$=IssuerTypeBox]').attr("disabled", false);
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="dialog" style="display: none;">
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>Customer Type</td>
                <td>
                    <asp:DropDownList ID="CustomerTypeBox" runat="server">
                        <asp:ListItem Text="Individual" Value="P"></asp:ListItem>
                        <asp:ListItem Text="Company" Value="C"></asp:ListItem>
                        <asp:ListItem Text="Insurance" Value="I"></asp:ListItem>
                        <asp:ListItem Text="Fund" Value="F"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="3">         
                    <input type="radio" name="custTypeRadioList" id="custSeller" value="Seller" /> Seller &nbsp;
                    <input type="radio" name="custTypeRadioList" id="custIssuer" value="Issuer" /> Issuer &nbsp;
                    <%--<asp:CheckBox ID="IssuerBox" Text="Issuer" runat="server" style="display:none" /> &nbsp; --%> 
                    <asp:DropDownList ID="IssuerTypeBox" runat="server"></asp:DropDownList>
                    <asp:TextBox ID="IssuerRemarkBox" Style="display:none" runat="server"></asp:TextBox>                  
                </td>               
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Fund Code</td>
                <td colspan="5">
                    <asp:TextBox ID="CodeBox" Width="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Name (Th) <span style="color:red">*</span></td>
                <td colspan="5">
                    <asp:TextBox ID="NameThBox" Width="500px" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Name (En) <span style="color:red">*</span></td>
                <td colspan="5">
                    <asp:TextBox ID="NameEnBox"  Width="500px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Segregate Account</td>
                <td colspan="5">
                    <asp:TextBox ID="SegregateAcctBox"  Width="200px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>TargetBank</td>
                <td>
                    <asp:TextBox ID="TargetBankBox" runat="server"></asp:TextBox></td>
                <td>TargetAcct</td>
                <td colspan="3">
                    <asp:TextBox ID="TargetAcctBox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Custodian</td>
                <td colspan="5">
                    <asp:DropDownList ID="CustodianBox" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Asset Management Company</td>
                <td colspan="5">
                    <asp:TextBox ID="AssetCompanyBox"  Width="500px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Tax ID</td>
                <td colspan="5">
                    <asp:TextBox ID="TaxIDBox"  Width="100px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Building Name</td>
                <td>
                    <asp:TextBox ID="BuildingNameBox"  Width="100px" runat="server"></asp:TextBox>
                </td>
                <td>Room No.</td>
                <td>
                    <asp:TextBox ID="RoomNoBox"  Width="100px" runat="server"></asp:TextBox>
                </td>
                <td>Floor No.</td>
                <td>
                    <asp:TextBox ID="FloorNoBox"  Width="100px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Village Name</td>
                <td>
                    <asp:TextBox ID="VillageNameBox"  Width="200px" runat="server"></asp:TextBox>
                </td>
                <td>Address No. <span style="color:red">*</span></td>
                <td>
                    <asp:TextBox ID="AddressNoBox"  Width="50px" runat="server"></asp:TextBox>
                </td>
                <td>Moo No.</td>
                <td>
                    <asp:TextBox ID="MooNoBox"  Width="50px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Soi</td>
                <td colspan="2">
                    <asp:TextBox ID="SoiBox"  Width="200px" runat="server"></asp:TextBox>
                </td>
                <td>Street Name</td>
                <td colspan="2">
                    <asp:TextBox ID="StreetNameBox"  Width="200px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Tumbon</td>
                <td colspan="2">
                    <asp:TextBox ID="TumbonBox"  Width="200px" runat="server"></asp:TextBox>
                </td>
                <td>Amphur <span style="color:red">*</span></td>
                <td colspan="2">
                    <asp:TextBox ID="AmphurBox"  Width="200px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Province <span style="color:red">*</span></td>
                <td colspan="2">
                    <asp:TextBox ID="ProvinceBox"  Width="300px" runat="server"></asp:TextBox>
                </td>
                <td>Postal Code <span style="color:red">*</span></td>
                <td colspan="2">
                    <asp:TextBox ID="PostalCodeBox"  Width="100px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>GIIN</td>
                <td>
                    <asp:TextBox ID="GIINBox"  Width="100px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Contact Person</td>
                <td>
                    <asp:TextBox ID="ContactBox"  Width="100px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Tel.</td>
                <td>
                    <asp:TextBox ID="TelBox"  Width="100px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Tax</td>
                <td>
                    <asp:CheckBox ID="IncVatBox" Text="หักภาษี" runat="server" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Register</td>
                <td>
                    <div style="float:left"><asp:CheckBox ID="AnnualRegistBox" Text="สมัครใช้บริการรายปี" runat="server" /></div>&nbsp;
                    <div style="float:left"><asp:TextBox ID="AnnualRegistTextBox" runat="server"></asp:TextBox></div>
                    </td>
            </tr>
        </table>   
        <asp:TextBox ID="AddressBox" TextMode="MultiLine" visible="false" Text="" runat="server"></asp:TextBox>
    </div>
    <div id="dialogmessage" style="display: none">
        <asp:Label ID="MessageBox" runat="server"></asp:Label>
    </div>
    <div class="content-module">	
				
	    <div class="content-module-heading cf">		
			<h3 class="fl">Customer Managment</h3>
			<span class="fr expand-collapse-text">Click to collapse and expand</span>		
		</div> <!-- end content-module-heading -->
		
		<div id="criteria-content" class="content-module-main cf">
			<div class="half-size-column fl">
					<fieldset>
						<p>
					        <label for="simple-input">Name</label>
                            <asp:TextBox ID="NameSearchBox" runat="server"></asp:TextBox>&nbsp;
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
			    <div style="float:left"><button class="round blue button-submit ic-add">Add Customer</button></div> 
			    <div style="float:right"></div>
			    <br /><br />			    	
                <asp:GridView ID="GridViewCustomer" DataKeyNames="CustomerID" CssClass="GridViewStyle" AllowPaging="True" AutoGenerateColumns="False" runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CustomerID") %>' CommandName="Delete" ImageUrl="~/Theme/icon/actions-delete.png" OnClientClick="return confirm('Are you sure you want to delete?');" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Image" ItemStyle-Wrap="false"  
                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" SelectImageUrl="~/Theme/icon/actions-edit.png" >
                        <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:CommandField>
                        <asp:BoundField DataField="Code" HeaderStyle-Width="70" HeaderText="Code" > 
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NameTh" HeaderStyle-Width="100" HeaderText="Name (Th)"  >    
                        <HeaderStyle Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="NameEn" HeaderStyle-Width="100" HeaderText="Name (En)"  >    
                        <HeaderStyle Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SegregateAcct" HeaderStyle-Width="100" HeaderText="Segregate Account"  > 
                        <HeaderStyle Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CustodianName" HeaderStyle-Width="100" HeaderText="Custodian"  > 
                        <HeaderStyle Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="AssetCompany" HeaderStyle-Width="100" HeaderText="AssetCompany"  > 
                        <HeaderStyle Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Wrap="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="TaxID" HeaderStyle-Width="100" HeaderText="Tax ID"  > 
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
                        </asp:BoundField>
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
                <asp:HiddenField ID="CustomerID" runat="server" />
                <asp:HiddenField ID="CustomerType" runat="server" />
                <asp:HiddenField ID="Code" runat="server" />
                <asp:HiddenField ID="NameTh" runat="server" />
                <asp:HiddenField ID="NameEn" runat="server" />
                <asp:HiddenField ID="SegregateAcct" runat="server" />
                <asp:HiddenField ID="CustodianID" runat="server" />
                <asp:HiddenField ID="AssetCompany" runat="server" />
                <asp:HiddenField ID="TaxID" runat="server" />
                <asp:HiddenField ID="Address" runat="server" />
                <asp:HiddenField ID="GIIN" runat="server" />
                <asp:HiddenField ID="Contact" runat="server" />
                <asp:HiddenField ID="Tel" runat="server" />
                <asp:HiddenField ID="IncVat" runat="server" />
                <asp:HiddenField ID="BuildingName" runat="server" />
                <asp:HiddenField ID="FloorNo" runat="server" />
                <asp:HiddenField ID="RoomNo" runat="server" />
                <asp:HiddenField ID="VillageName" runat="server" />
                <asp:HiddenField ID="AddressNo" runat="server" />
                <asp:HiddenField ID="MooNo" runat="server" />
                <asp:HiddenField ID="Soi" runat="server" />
                <asp:HiddenField ID="StreetName" runat="server" />
                <asp:HiddenField ID="Tumbon" runat="server" />
                <asp:HiddenField ID="Amphur" runat="server" />
                <asp:HiddenField ID="Province" runat="server" />
                <asp:HiddenField ID="PostalCode" runat="server" />
                <asp:HiddenField ID="TargetBank" runat="server" />
                <asp:HiddenField ID="TargetAcct" runat="server" />
                <asp:HiddenField ID="Issuer" runat="server" />
                <asp:HiddenField ID="Seller" runat="server" />
                <asp:HiddenField ID="IssuerType" runat="server" />
                <asp:HiddenField ID="IssuerRemark" runat="server" />
                <asp:HiddenField ID="AnnualRegist" runat="server" />
                <asp:HiddenField ID="AnnualRegistText" runat="server" />
                <asp:Button ID="DialogSubmit" class="ui-helper-hidden" Text="Submit" 
                    runat="server" onclick="DialogSubmit_Click" />
			</div> <!-- end half-size-column -->
		</div> <!-- end content-module-main -->
		
	</div> <!-- end content-module -->
</asp:Content>
