var eventos = [];

(async function (window, document, $, undefined) {
    "use strict";
    setCalendarConfiguration();
    await getEvents();
    $("#btnNovoAgendamento").click(showModalAgendamento);
})(window, document, window.jQuery);

function setCalendarConfiguration() {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        locale: 'pt-br',
        initialView: 'dayGridMonth',
        events: eventos,
        eventClick: function (info) {
            showModalAgendamento(info.event.id)
        }
      });
    calendar.render();
}

function showModalAgendamento(id){
    setFormConfiguration(id);
    $("#modalAgendamento").modal("show");
}

async function getEvents() {

    await $.ajax({
        url: 'https://localhost:5000/Agendamento/GetAgendamentos',
        type: 'GET',
        dataType: 'json',
        success: function (resposta) {
            if (resposta.statusCode == 400) {
                let erro = resposta.value[0].errorMessage || resposta.value;
                alert(erro);                                               
            } else if (resposta.statusCode == 200) {   
                eventos = [];
                $.each(resposta.value, function (r, item) {
                    eventos.push({
                        id: item.id,
                        title: item.nomeEspecialidade,
                        start: item.dataHoraInicio,
                        end: item.dataHoraFim,
                        display: "block",
                    });

                });
            }            
            setCalendarConfiguration();;
        }
    });
}