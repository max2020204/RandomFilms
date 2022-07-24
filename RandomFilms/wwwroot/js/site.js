$('.set-bg').each(function () {
    var bg = $(this).data('setbg');
    $(this).css('background-image', 'url(' + bg + ')');
});
$(function () {
    $('select').selectpicker();
});
let selectBox_1 = document.getElementById('GenreIn');
let selectBox_2 = document.getElementById('GenreOut');

function f1() {
    for (i = 0; i < selectBox_1.length; i++) {
        if (selectBox_1.options[i].selected == false) {
            selectBox_2.options[i].disabled = false;
        }
        else {
            selectBox_2.options[i].disabled = true;
        }
    }
    $('#GenreOut').selectpicker('refresh');
}
function f2() {
    for (i = 0; i < selectBox_2.length; i++) {
        if (selectBox_2.options[i].selected == false) {
            selectBox_1.options[i].disabled = false;
        }
        else {
            selectBox_1.options[i].disabled = true;
        }
    }
    $('#GenreIn').selectpicker('refresh');
}
