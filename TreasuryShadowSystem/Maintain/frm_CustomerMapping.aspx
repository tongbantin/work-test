<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_CustomerMapping.aspx.cs" Inherits="TreasuryShadowSystem.Maintain.frm_CustomerMapping" Title="Untitled Page" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link href="../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
    <style type="text/css">
        input.inputLong { width: 400px !important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="CustTypeTableContainer"></div>
    <script type="text/javascript">

        $(document).ready(function() {

            //Prepare jtable plugin
            $('#CustTypeTableContainer').jtable({
                title: 'Customer Type Mapping',
                paging: true,
                pageSize: 10,
                sorting: true,
                toolbars: {
                    hoverAnimation: true, //Enable/disable small animation on mouse hover to a toolbar item.
                    hoverAnimationDuration: 60, //Duration of the hover animation.
                    hoverAnimationEasing: undefined //Easing of the hover animation. Uses jQuery's default animation ('swing') if set to undefined.
                },
                defaultSorting: 'CUSTTYPEMAPPINGID ASC',
                actions: {
                    listAction: '<%=Config.Website %>/Maintain/frm_CustomerMapping.aspx/CustomerMappingList',
                    createAction: '<%=Config.Website %>/Maintain/frm_CustomerMapping.aspx/CreateCustomerMapping',
                    updateAction: '<%=Config.Website %>/Maintain/frm_CustomerMapping.aspx/UpdateCustomerMapping',
                    deleteAction: '<%=Config.Website %>/Maintain/frm_CustomerMapping.aspx/DeleteCustomerMapping'
                },
                fields: {
                    CUSTTYPEMAPPINGID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    BOTTYPE: {
                        title: 'BOT Type',
                        inputClass: 'inputLong validate[required]'
                    },
                    BOTTYPEDESC: {
                        title: 'Description (BOT)',
                        inputClass: 'inputLong'
                    },
                    SECTYPE: {
                        title: 'SEC Type',
                        inputClass: 'inputLong validate[required]'
                    },
                    SECTYPEDESC: {
                        title: 'Description (SEC)',
                        inputClass: 'inputLong'
                    }
                },
                //Initialize validation logic when a form is created
                formCreated: function(event, data) {
                    data.form.children(':lt(16)').wrapAll('<div class="col1"/>');
                    data.form.children(':gt(0)').wrapAll('<div class="col2"/>');
                    data.form.parents('.ui-dialog').css('width', '500px');
                    var divwidth = data.form.parents('.ui-dialog').width();
                    var left = ($(document).width() - parseFloat(divwidth)) / 2;
                    data.form.parents('.ui-dialog').css('left', left);

                    data.form.validationEngine();
                }
            });

            $('#CustTypeTableContainer').jtable('load');
        });

    </script>
</asp:Content>
