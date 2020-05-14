$(function () {
    $("#tbl").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        }
    });
    $("#tbl").on("click", ".btnsil", function () {
        var btn = $(this);
        bootbox.confirm("Departmanı silmek istediğinize emin misiniz!!!", function (result) {
            if (result) {
                var id = btn.data("id");
                var x = location.href;
                var res = x.split("/");
                $.ajax({
                    type: "GET",
                    url: "/"+res[3]+"/Sil/" + id,
                    success: function () {
                        btn.parent().parent().remove();
                    },
                    error: function () {
                        console.log("Silme başarısız");
                    }
                });
            }
        });
    });
});



function CheckDateTypeIsValid(dateElement)
{
    var value = $(dateElement).val();
    if (value == null) {
        $(dateElement).valid("false");
    } else {
        $(dateElement).valid();
    }
}