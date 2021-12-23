<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_BrokerageFeeConfig.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_BrokerageFeeConfig" %>
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
                title: 'Brokerage Fee Config',
                paging: true,
                pageSize: 10,
                sorting: true,
                defaultSorting: 'BROK ASC',
                actions: {
                    listAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFeeConfig.aspx/ConfigList',
                    createAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFeeConfig.aspx/CreateConfig',
                    updateAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFeeConfig.aspx/UpdateConfig',
                    deleteAction: '<%=Config.Website %>/BrokerageFee/frm_BrokerageFeeConfig.aspx/DeleteConfig'
                },
                fields: {
                    DEALFEECONFID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    BROK: {
                        title: 'Broker',
                        inputClass: 'validate[required]'
                    },
                    PRODTYPE: {
                        title: 'Product Type',
                        options: [{ Value: 'FI', DisplayText: 'FI' }, { Value: 'IR', DisplayText: 'IRS' }, { Value: 'IF', DisplayText: 'IF' }, { Value: 'ES', DisplayText: 'Equity Swap' }, { Value: 'CC', DisplayText: 'CCS' }, { Value: 'SPOT', DisplayText: 'SPOT' }, { Value: 'FORWARD', DisplayText: 'FORWARD' }, { Value: 'SWAP', DisplayText: 'FX SWAP'}],
                        inputClass: 'validate[required]'
                    },
                    SECTYPE: {
                        title: 'Security Type'
                    },
                    LEG: {
                        title: 'Leg',
                        options: [{ Value: '', DisplayText: '' }, { Value: 'F', DisplayText: 'FAR' }, { Value: 'N', DisplayText: 'NEAR'}],
                        inputClass: 'validate[required]'
                    },
                    PERUNIT: {
                        title: 'Per Unit',
                        inputClass: 'validate[required]',
                        visibility: 'hidden'
                    },
                    MINAGE: {
                        title: 'Min Age'
                    },
                    MAXAGE: {
                        title: 'Max Age'
                    },
                    BYAGE: {
                        title: 'By Age',
                        options: [ { Value: '0', DisplayText: 'Disabled'},{ Value: '1', DisplayText: 'Enabled' }],
                        inputClass: 'validate[required]',
                        visibility: 'hidden'
                    },
                    BYUNIT: {
                        title: 'By Unit',
                        options: [{ Value: '1', DisplayText: 'Enabled' }, { Value: '0', DisplayText: 'Disabled'}],
                        inputClass: 'validate[required]',
                        visibility: 'hidden'
                    },
                    FEE: {
                        title: 'Fee',
                        inputClass: 'validate[required]'
                    },
                    EFFSTARTDATE: {
                        title: 'Effective Date',
                        visibility: 'hidden'
                    },
                    EFFENDDATE: {
                        title: 'Expire Date',
                        visibility: 'hidden'
                    },
                    PAYPERIOD: {
                        title: 'Pay Period',
                        visibility: 'hidden'
                    },
                    ENABLED: {
                        title: 'Enabled',
                        options: [{ Value: '1', DisplayText: 'Enabled' }, { Value: '0', DisplayText: 'Disabled'}],
                        inputClass: 'validate[required]',
                        visibility: 'hidden'
                    }
                }
            });

            $('#SearchButton').click(function (e) {
                e.preventDefault();
                $('#BrokerageFeeTableContainer').jtable('load', {
                    brok: $('#brok').val(),
                    prodtype: $('.prodtype').val()
                });
            });
        });

    </script>
</asp:Content>
