var _parentUrl = getAbsolutePath();

$(function() {
    var lastsel;
    var _ddUserGroup = $('#ctl00_bodyContent_ddUserGroup');
    //var _ddUserGroup = $('#ddUserGroup');
    
    _ddUserGroup.change(function() {
        //location.reload();
        getData();
    });

    $("#btnsave").on("click", function(e) {
        e.preventDefault();
        var _chgroup = _ddUserGroup.val();
        var _done = true;
        if ((typeof _chgroup == "undefined") || (_chgroup == "")) {
            alert('Please select user group');
            _done = false;
        }
        if (_done)
            $("#confirm_dialog").dialog("open");
    });

    $("#confirm_dialog").dialog({
        autoOpen: false,
        modal: true,
        buttons: {
            "Confirm": function() {
                $(this).dialog("close");
                OnSubmit();
            },
            "Cancel": function() {
                $(this).dialog("close");
            }
        }
    });

    //jsTree
    getData();

});

function getData() {
    var _ddUserGroup = $('#ctl00_bodyContent_ddUserGroup');

    $("#jstree_ctrs").jstree({
        // the `plugins` array allows you to configure the active plugins on this instance
        "plugins": ["themes", "json_data", "checkbox", "sort", "ui"],
        "json_data": {
            "ajax": {
                "type": "POST",
                "dataType": "json",
                "async": true,
                "contentType": "application/json;",
                "url": _parentUrl + "adminws.asmx/GetAllNodesMenu",
                "data": function(node) {
                    var param = "";
                    var _group = _ddUserGroup.val();
                    if ((typeof _group == "undefined") || (_group == ""))
                        _group = null;
                    else
                        _group = _ddUserGroup.val();

                    if (node == -1) {
                        param = '{ "operation" : "get_children", "id" : -1, "group": ' + _group + ' }';
                    }
                    else {
                        //get the children for this node
                        param = '{ "operation" : "get_children", "id" : ' + $(node).attr("id") + ', "group": ' + _group + ' }';
                    }
                    return param;
                },
                "success": function(retval) {
                    return retval.d;
                }
            }
        }
    }).bind("loaded.jstree", function(event, data) {
        //Open All Nodes
        var parents = [];
        $('#jstree_ctrs >ul>li').each(function(i) {
            parents.push($(this).attr("id"));
        });
        
        data.inst.get_container().find('li').each(function(i) {
            if ($.inArray($(this).attr('id'), parents) != -1)
                data.inst.open_node($(this));
            //else console.log('child');
            //data.inst.open_node($(this));
        });
        //Selected
        $('li[selected]').each(function() {
            $(this).removeClass('jstree-unchecked').addClass('jstree-checked');
        });

        $('li[undetermind="true"]').each(function() {

            var childSelected = $("#" + $(this).attr("id") + ">ul>li[selected]");
            var childs = $("#" + $(this).attr("id") + ">ul>li");

            if (childSelected.length > 0) {
                if (childSelected.length == childs.length)
                    $(this).removeClass('jstree-unchecked').addClass('jstree-checked');
                else
                    $(this).removeClass('jstree-unchecked').addClass('jstree-undetermined');
            }

        });
    });
}

function ReceivedClientData(data) {
    var thegrid = $("#jstree_ctrs");
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

function recursive_simplify(node) {
    if (node.children) {
        for (var i = 0; i < node.children.length; i++) {
            node.children[i] = recursive_simplify(node.children[i])
        }
    }
    delete node['metadata'];
    return node
}

function OnSubmit()
{            
    var tree = $.jstree._reference('#jstree_ctrs');
    var checked = tree.get_checked();
    var result = [];
    var listid = "";
    var _ddUserGroup = $('#ctl00_bodyContent_ddUserGroup');
    
    for (var i = 0, checkedLength = checked.length; i < checkedLength; i++) {
        var checkedJson = tree.get_json(checked[i], ['id', 'rel', 'data-bin', 'data-pos'])[0];
        checkedJson = recursive_simplify(checkedJson);
        //result.push(checkedJson.attr.id);
        
        if (listid == "") listid = checkedJson.attr.id;
        else listid += "," + checkedJson.attr.id;

        //find child
        if (typeof checkedJson.children != "undefined") {
            for (var j = 0; j < checkedJson.children.length; j++) {
                listid += "," + checkedJson.children[j].attr.id;
            }
        }
    }
    //for undetermined
    var undetermined = $('#jstree_ctrs').find(".jstree-undetermined");
    for (var j = 0, checkedLength = undetermined.length; j < checkedLength; j++) {
        var undeterminedJson = tree.get_json(undetermined[j], ['id', 'rel', 'data-bin', 'data-pos'])[0];
        undeterminedJson = recursive_simplify(undeterminedJson);
        listid += "," + undeterminedJson.attr.id;
    }
    
    if (listid != "") {
        //alert(JSON.stringify(result));
        var pData = { "ids": listid, "group": _ddUserGroup.val() };
        $.ajax({
            type: "POST",
            url: _parentUrl + "adminws.asmx/PerformCRUDPermissionAction",
            data: JSON.stringify(pData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data, textStatus) {
                //location.reload();
                alert('Save data successful!');
            },
            error: function(data, textStatus) {
                alert('An error has occured retrieving data!');
            }
        });
    }
    else
        alert('Please choose the item that is complete process!');
}

function getAbsolutePath() {
    var loc = window.location;
    var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
    //return loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pathName.length));
    var newUrl = loc.href.substring(0, loc.href.length - (loc.pathname + loc.search + loc.hash).length);
    if (pathName != "/") {
        var p = pathName.split('/');
        var pn = '/' + p[1] + '/';
        if (pn.indexOf("Web") != -1)
            newUrl = loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pn.length));
    }
    //return loc.href.substring(0, loc.href.length - (loc.pathname + loc.search + loc.hash).length);
    return newUrl;
}