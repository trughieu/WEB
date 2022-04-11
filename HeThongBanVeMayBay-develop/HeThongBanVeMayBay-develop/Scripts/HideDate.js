$('.ItineraryRadio').on('ifChanged', function (event) { ToggleDatepicker(); });
function ToggleDatepicker() {
    var check_value = $('.ItineraryRadio:checked').val();
    if (check_value == 1) {
        $("#divtxtReturnDate").attr('style', 'visibility: hidden;');
        $("#ngayvetieude").attr('style', 'visibility: hidden;');
        $("#txtReturnDate").removeAttr("required");
    }
    else {
        $("#divtxtReturnDate").attr('style', 'visibility: visible;');
        $("#ngayvetieude").attr('style', 'visibility: visible;');
        $("#divtxtReturnDate").datepicker("option", "minDate", $("#txtDepartureDate").datepicker("getDate"));
        $("#txtReturnDate").attr("required", "");
    }
}