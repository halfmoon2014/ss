//刷新树
function reloadTree(treeId) {
    var node = $('#' + treeId).tree('getSelected');
    if (node) {
        $('#' + treeId).tree('reload');
    } else {
        $('#' + treeId).tree('reload');
    }
}
//折叠树
function collapseAllTree(treeId) {
    var node = $('#' + treeId).tree('getSelected');
    if (node) {
        $('#' + treeId).tree('collapseAll', node.target);
    } else {
        $('#' + treeId).tree('collapseAll');
    }
}
//展开树
function expandAllTree(treeId) {
    var node = $('#' + treeId).tree('getSelected');
    if (node) {
        $('#' + treeId).tree('expandAll', node.target);
    } else {
        $('#' + treeId).tree('expandAll');
    }
}

function loadTree(treeId,sqlCommand) {
    var bz = mySysDate(sqlCommand);
    $('#' + treeId).tree({
        url: "lss_tree.ashx?bz=" + bz + " ",
        onClick: function (node) {
            if (typeof (onClickTree) == "function") {
                onClickTree(node,treeId)
            }
        },
        onLoadSuccess: function (node, data) {
            if (typeof (onLoadSuccessTree) == "function") {
                onLoadSuccessTree(node, data, treeId);
            }
        }
    });

};                  