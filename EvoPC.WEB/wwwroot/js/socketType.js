﻿window.SocketType = (function ($) {

    let editUrl = "/SocketType/Edit/{0}";
    let deleteUrl="/SocketType/Delete/{0}"

    function _onDelete() {

        let selectedId = getSelectedId();

        $.ajax({
            url: stringFormat(deleteUrl, selectedId),
            data: { "id": selectedId },
            type: "delete",
            success: function (data) {
                if (data.succes)
                    $(jid(selectedId)).remove();
            },
            error: function (context, status, message) {
                alert(context, status, message);
            }
        });

    }

    function _onEditBtnPress() {
        let selectedId = getSelectedId();
        let url = stringFormat(editUrl, selectedId);
        window.open(url, "_self");
    }

    return {
        onDelete: _onDelete,
        onEditBtnPress: _onEditBtnPress
    }

}($));