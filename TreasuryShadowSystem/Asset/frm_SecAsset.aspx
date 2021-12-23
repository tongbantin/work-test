<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_SecAsset.aspx.cs" Inherits="TreasuryShadowSystem.Asset.frm_SecAsset" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link href="../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
    <style type="text/css">
        input.inputLong { width: 400px !important; }
    </style>
    <script type="text/javascript">
        function CallExecuteJob() {
            var requestUri = '<%=Config.Website %>/Asset/frm_SecAsset.aspx/LoadSecAsset';
            $.ajax({
                type: "POST",
                url: requestUri,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {		
                    if (data.d.Result == "ERROR")
                        alert(data.d.Message);
                    else 
						alert('Transfer Complete'); 
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div>
        <fieldset>
            <table cellpadding="5" cellspacing="5">
                <tr>
                    <td>&nbsp;</td>
                    <td>SECID</td>
                    <td><asp:TextBox ID="SecBox" CssClass="SecBox" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Asset Group </td>
                    <td><asp:DropDownList CssClass="AssetGroupBox" ID="AssetGroupBox" runat="server">
					</asp:DropDownList></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="2"><asp:Button ID="btnSubmit" Text="Search" runat="server"  
                        class="LoadRecordsButton round blue button-submit"/></td>
                </tr>
            </table>
											        
	    </fieldset>
	    
	    </div>
    <div id="SecAssetTableContainer" style="height:500px; overflow:auto"></div>
    <script type="text/javascript">

        $(document).ready(function() {

            //Prepare jtable plugin
            $('#SecAssetTableContainer').jtable({
                    title: 'Sec Asset',
                    paging: false,
                    sorting: false,
                    defaultSorting: 'SECID ASC',
                    toolbar: {
                        items: [{
                            icon: '<%=Config.Website %>/Theme/icon/icon-transaction.png',
                            text: 'Load From Opics',
                            click: function() {
                                CallExecuteJob();
                            }
                        }]
                    },
                    actions: {
                        listAction: '<%=Config.Website %>/Asset/frm_SecAsset.aspx/SecAssetList',
                        createAction: '<%=Config.Website %>/Asset/frm_SecAsset.aspx/CreateSecAsset',
                        updateAction: '<%=Config.Website %>/Asset/frm_SecAsset.aspx/UpdateSecAsset',
                        deleteAction: '<%=Config.Website %>/Asset/frm_SecAsset.aspx/DeleteSecAsset'
                    },
                    fields: {
                        SECASSETID: {
                            key: true,
                            create: false,
                            edit: false,
                            list: false
                        },
                        SECID: {
                            title: 'SECID'
                        },
                        QTY: {
                            title: 'Unit'
                        },
                        DEPOSITQTY: {
                            title: 'Deposit Unit'
                        },
                        MDATE: {
                            title: 'Maturity Date',
                            type: 'date',
                            visibility: 'hidden'
                        },
                        ASSETGROUPID: {
                            title: 'Asset Group',
                            options: '<%=Config.Website %>/Asset/frm_SecAsset.aspx/GetAssetGroupOption'
                        },
                        ENABLED: {
                            title: 'Enabled',
                            options: { '0': 'Disable', '1': 'Enable' },
                            defaultValue: '1',
                            inputClass: 'validate[required]',
                            visibility: 'hidden'
                        }
                    },
                    //Initialize validation logic when a form is created
                    formCreated: function(event, data) {
                        data.form.validationEngine();
                    }
                });

                //$('#SecAssetTableContainer').jtable('load');
                $('.LoadRecordsButton').click(function(e) {
                    e.preventDefault();
                    $('#SecAssetTableContainer').jtable('load', {
                        secid: $('.SecBox').val(),
                        assetgroupid: $('.AssetGroupBox').val()
                    });
                });
            });

    </script>
</asp:Content>
