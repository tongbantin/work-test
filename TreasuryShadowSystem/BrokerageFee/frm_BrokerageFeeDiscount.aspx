<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_BrokerageFeeDiscount.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_BrokerageFeeDiscount" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link href="../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div class="content-module-main">
        <div class="site-container">
            <div class="filtering" id="dvFiltering2">
                     Broker
                    <input type="text" name="brok" id="brok"/> 
                    Product Type
                    <select name="prodtype" id="prodtype" class="prodtype" runat="server"> 
                    </select>
                    <button type="submit" id="SearchButton">Search</button>
            </div>
        </div>
    </div>
    <div id="BrokerageFeeTableContainer" style="width: 1000px;"></div>
    <script type="text/javascript">

        $(document).ready(function() {

            //Prepare jtable plugin
            $('#BrokerageFeeTableContainer').jtable({
                title: 'Brokerage Fee Discount',
                paging: true,
                pageSize: 10,
                sorting: true,
                defaultSorting: 'BROK ASC',
                actions: {
                    listAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFeeDiscount.aspx/DiscountList',
                    createAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFeeDiscount.aspx/CreateDiscount',
                    updateAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFeeDiscount.aspx/UpdateDiscount',
                    deleteAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFeeDiscount.aspx/DeleteDiscount'
                },
                fields: {
                    DISCOUNTID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    BROK: {
                        title: 'Broker',
                        inputClass: 'validate[required]'
                    },
                    PLAN: {
                        title: 'Plan',
                        visibility: 'hidden',
                        options: [{ Value: '1', DisplayText: '1'}]
                    },
                    DISCTYPE: {
                        title: 'Discount Type',
                        visibility: 'hidden',
                        options: [{ Value: 'P', DisplayText: 'Percent'}]
                    },
                    PRODTYPE: {
                        title: 'Product Type',
                        options: [{ Value: 'FX', DisplayText: 'FX' }, { Value: 'FI', DisplayText: 'FI' }, {Value:'SWAP', DisplayText:'SWAP'}],
                        inputClass: 'validate[required]'
                    },
                    DISCOUNT: {
                        title: 'Discount',
                        inputClass: 'validate[required]'
                    },
                    MINAMT: {
                        title: 'Min Amt.',
                        inputClass: 'validate[required]'
                    },
                    MAXAMT: {
                        title: 'Max Amt.',
                        inputClass: 'validate[required]'
                    },
                    EFFSTARTDATE: {
                        title: 'Effective Date',
                        visibility: 'hidden',
                        inputClass: 'validate[required]',
                        type: 'date',
                        displayFormat: 'yy-mm-dd'
                    },
                    EFFENDDATE: {
                        title: 'Expire Date',
                        visibility: 'hidden',
                        inputClass: 'validate[required]',
                        type: 'date',
                        displayFormat: 'yy-mm-dd'
                    },
                    ENABLED: {
                        title: 'Enabled',
                        options: [{ Value: '1', DisplayText: 'Enabled' }, { Value: '0', DisplayText: 'Disabled'}],
                        inputClass: 'validate[required]'
                    }
                }
            });

            $('#SearchButton').click(function(e) {
                e.preventDefault();
                $('#BrokerageFeeTableContainer').jtable('load', {
                    brok: $('#brok').val(),
                    prodtype: $('.prodtype').val()
                });
            });
        });

    </script>
</asp:Content>
