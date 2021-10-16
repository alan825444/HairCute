$(document).ready(function () {
    $("#fCity").change(function () { ChangeCity(); })
});

function ChangeCity() {
    var selectedCity = $("#fCity option:selected").val();
    if ($.trim(selectedCity).length > 0) {
        GetArea(selectedCity)
    }
}

function GetArea(CityID) {
    $.ajax({
        url: "Area",
        data: { CityID: CityID },
        type: 'post',
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            if (data.length > 0) {
                $('#fArea').empty();
                $('#fArea').append($('<option></option>').val('').text('請選擇'));
                $.each(data, function (i, item) {
                    $('#fArea').append($('<option></option>').val(item.Key).text(item.Value))
                })
            }
        }
    })
}