(function (window, document, $, undefined) {
    "use strict";
    setCalendarConfiguration();
    $("#btnNovoAgendamento").click(showModalAgendamento);
})(window, document, window.jQuery);

function setCalendarConfiguration() {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        locale: 'pt-br',
        initialView: 'dayGridMonth'
      });
    calendar.render();
}

function showModalAgendamento(){
    setFormConfiguration();
    $("#modalAgendamento").modal("show");
}