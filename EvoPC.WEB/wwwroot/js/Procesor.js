window.Procesor = (function ($) {

    let editUrl = "/Procesor/Edit/{0}";
    let deleteUrl = "/Procesor/Delete/{0}"

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
            error: function (context, status, meessage) {
                alert(context, status, meessage);
            }
        });

    }

    function _onCHangeFile(event) {
        if (!event || !event.target || !event.target.files)
            return;

        let file = event.target.files[0];

        $(jid("fileUploadlabel")).text(file.name);
    }

    function _onEditBtnPress() {
        let selectedId = getSelectedId();
        let url = stringFormat(editUrl, selectedId);
        window.open(url, "_self");
    }

    return {
        onDelete: _onDelete,
        onEditBtnPress: _onEditBtnPress,
        onCHangeFile: _onCHangeFile
    }

}($));