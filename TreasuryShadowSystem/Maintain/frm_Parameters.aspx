<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_Parameters.aspx.cs" Inherits="TreasuryShadowSystem.Maintain.frm_Parameters" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link href="../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="StudentTableContainer" style="width: 500px;"></div>
    <script type="text/javascript">

        $(document).ready(function() {

            //Prepare jtable plugin
            $('#StudentTableContainer').jtable({
                title: 'Web Parameter',
                paging: true,
                pageSize: 10,
                sorting: true,
                defaultSorting: 'Name ASC',
                actions: {
                    listAction: '<%=Config.Website %>/Maintain/frm_Parameters.aspx/ParameterList',
                    createAction: '<%=Config.Website %>/Maintain/frm_Parameters.aspx/CreateParameter',
                    updateAction: '<%=Config.Website %>/Maintain/frm_Parameters.aspx/UpdateParameter',
                    deleteAction: '<%=Config.Website %>/Maintain/frm_Parameters.aspx/DeleteParameter'
                },
                fields: {
                    ParameterID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    Name: {
                        title: 'Name'
                    },
                    Value: {
                        title: 'Value',
                        display: function(data) {
                            if (data.record.Value.length > 43)
                                return data.record.Value.substr(0, 43);
                            else
                                return data.record.Value;
                        },
                        type: 'textarea'
                    }
                }
            });

            $('#StudentTableContainer').jtable('load');
        });

    </script>
</asp:Content>
