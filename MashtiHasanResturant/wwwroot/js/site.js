function DoSearch() {
    var action = $("#frmSearch").attr("data-action");
    var controller = $("#frmSearch").attr("data-controller");
    var formid = "#" + $().attr("data-form-id");
    var url = `/${controller}/${action}`;
    var data = $(formid).serialize();

    $.get(url, data, function (ListItem) {
        $("#DvContent").html(ListItem);
    })

}
document.on("change", ".drp", function () {
    DoSearch();
})

document.on("keyup", ".inp", function () {
    DoSearch();
})