var _parentUrl = getAbsolutePath();

$(function() {
    var prmSearch = { multipleSearch: true, overlay: false };
    var grid = $("#jqgrid_ctrs");
    var lastsel;

    $("#jqgrid_ctrs").jqGrid({
        prmNames: {
            search: "isSearch",
            nd: null,
            rows: "numRows",
            page: "page",
            sort: "sortField",
            order: "sortOrder"
        },
        // add by default to avoid webmethod parameter conflicts
        postData: { searchString: '', searchField: '', searchOper: '' },
        // setup ajax call to webmethod
        datatype: function(pdata) { getData(pdata); },
        height: 330,
        // this is what jqGrid is looking for in json callback
        jsonReader: {
            root: "rows",
            page: "page",
            total: "totalpages",
            records: "totalrecords",
            cell: "cell",
            id: "MenuID", //index of the column with the PK in it 
            userdata: "userdata",
            repeatitems: true
        },
        colNames: ['ID', 'SEQ', 'DATA_CODE', 'DATA_TYPE', 'DATA_NAME_ENG', 'PATH_URL', 'PARENT_MENU', 'ENABLED'],
        colModel: [
                    { name: 'MenuID', index: 'MenuID', width: 50, align: 'right', searchoptions: { sopt: ['eq', 'ne'] }, editable: true, editoptions: { readonly: 'true'} },
                    { name: 'Seq', index: 'Seq', width: 50, align: 'right', searchoptions: { sopt: ['eq', 'ne'] }, editable: true, edittype: 'text', editrules: { required: true} },
                    { name: 'DataCode', index: 'DataCode', width: 100, searchoptions: { sopt: ['eq', 'ne'] }, editable: true, edittype: 'text', editrules: { required: true} },
                    { name: 'DataType', index: 'DataType', width: 100, searchoptions: { sopt: ['eq', 'ne'] }, editable: true, edittype: 'select', editoptions: { value: { '': 'N/A', 'MENU': 'MENU', 'REPORT': 'REPORT'}} },
                    { name: 'DataNameEng', index: 'DataNameEng', width: 200, searchoptions: { sopt: ['eq', 'ne', 'cn'] }, editable: true, edittype: 'text', editoptions: { size: 35 }, editrules: { required: true} },
                    { name: 'PathUrl', index: 'PathUrl', width: 250, sortable: false, search: false, editable: true, edittype: 'text', editoptions: { size: 35} },
                    { name: 'UnderMenu', index: 'UnderMenu', width: 100, searchoptions: { sopt: ['eq'] }, editable: true, edittype: 'select', editoptions: { value: { '': 'OWNER', '1': 'Home', '57': 'Deal Ticket',
                        '21': 'Management', '3': 'Font office', '4': 'Back office', '5': 'Accounting', '6': 'IT Administrator'
                    }
                    }
                    },
                    { name: 'Enabled', index: 'Enabled', width: 100, searchoptions: { sopt: ['eq'] }, editable: true, edittype: 'checkbox', editoptions: { value: "1:0"} }
                ],
        rowNum: 15,
        rowList: [15, 30, 45],
        //loadonce: true,
        gridview: true,
        //height: 'auto',
        pager: '#jqgrid_ctrs_pager',
        rownumbers: false,
        viewrecords: true,
        sortname: 'MenuID',
        sortorder: "asc",
        //editurl: '<%= ResolveClientUrl("~/adminws.asmx/PerformCRUDAction") %>',
        editurl: _parentUrl + '/adminws.asmx/PerformCRUDAction',
        caption: "Menu Master",
        onCellSelect: function(rowid, iCol, aData) {

            //            if (rowid && rowid !== lastsel) {

            //                if (lastsel)
            //                    $("#jqgrid_ctrs").jqGrid('restoreRow', lastsel);
            //                $("#jqgrid_ctrs").jqGrid('editRow', rowid, true);
            //                lastsel = rowid;
            //            }
        },
        success: function(data, textStatus) {
            if (textStatus == "success")
                ReceivedClientData(JSON.parse(getMain(data)).rows);
        },
        error: function(data, textStatus) {
            alert('An error has occured retrieving data!');
        }
    }).navGrid('#jqgrid_ctrs_pager', { view: false, del: false, add: true, edit: true, refresh: true },
               { closeOnEscape: true, reloadAfterSubmit: true, closeAfterEdit: true, width: 400 }, // default settings for edit
               {closeOnEscape: true, reloadAfterSubmit: true, closeAfterEdit: true, width: 400 }, // default settings for add
               {}, // delete instead that del:false we need this
               {closeOnEscape: true, multipleSearch: false, closeAfterSearch: true }, // search options
               {} /* view parameters*/
             );


});

function getData(pData) {

    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        //url: '<%= ResolveClientUrl("~/adminws.asmx/GetMenuData") %>',
        url: _parentUrl + '/adminws.asmx/GetMenuData',
        //data: '{}',
        data: JSON.stringify(pData),
        dataType: "json",
        success: function(data, textStatus) {
            if (textStatus == "success") {
                //ReceivedClientData(JSON.parse(getMain(data)).rows);
                var grid = $("#jqgrid_ctrs")[0];
                grid.addJSONData(JSON.parse(data.d));
            }
        },
        error: function(data, textStatus) {
            alert('An error has occured retrieving data!');
        }
    });
}
function ReceivedClientData(data) {
    var thegrid = $("#jqgrid_ctrs");
    thegrid.clearGridData();
    for (var i = 0; i < data.length; i++)
        thegrid.addRowData(i + 1, data[i]);
}
function getMain(dObj) {
    if (dObj.hasOwnProperty('d'))
        return dObj.d;
    else
        return dObj;
}

function getAbsolutePath() {
    var loc = window.location;
    var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
    var newUrl = loc.href.substring(0, loc.href.length - (loc.pathname + loc.search + loc.hash).length);
    if (pathName != "/") {
        var p = pathName.split('/');
        var pn = '/' + p[1] + '/';
        if (pn.indexOf("Web")!=-1)
            newUrl = loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pn.length));
    }
    return newUrl;
}