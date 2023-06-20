var eventos = [];

(async function (window, document, $, undefined) {
    "use strict";
    setCalendarConfiguration();
    await verificaUsuarioLogado();
    await getEvents();
    $(".especialidade").change(getEvents);

    if (userRole == "Paciente") {
      $("#btnNovoAgendamento").click(() => showModalAgendamento());
      $("#btnNovoAgendamento").css('display', 'flex');
    }
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

function addDays(date, days) {
    const newDate = new Date(date);
    newDate.setDate(newDate.getDate() + days);

    return newDate;
}

function addWeeks(date, weeks) {
    const newDate = new Date(date);
    newDate.setDate(newDate.getDate() + 7 * weeks);

    return newDate;
}

function addMonths(date, months) {
    const newDate = new Date(date);
    newDate.setMonth(newDate.getMonth() + months);

    return newDate;
}

function addYears(date, years) {
    const newDate = new Date(date);
    newDate.setFullYear(newDate.getFullYear() + years);

    return newDate;
}

async function getEvents() {
    var especialidades = []
    for (var i = 0; i < $(".especialidade").length; i++) {
        var especialidade = $(".especialidade")[i];
        if (especialidade.checked) {
            especialidades.push(especialidade.id);
        }
    }
    let data = {
        UserId: userId,
        UserRole: userRole,
        Especialidades: especialidades
    };
    
    await $.ajax({
        url: 'https://localhost:5000/Agendamento/GetAgendamentos',
        type: 'POST',
        data: JSON.stringify(data),
        contentType: 'application/json',
        dataType: 'json',
        success: function (resposta) {
            if (resposta.statusCode == 400) {
                let erro = resposta.value[0].errorMessage || resposta.value;
                toastError(erro);              
                console.error(resposta)                                 
            } else if (resposta.statusCode == 200) {   
                eventos = [];
                $.each(resposta.value, function (r, item) {
                    eventos.push({
                        id: item.id,
                        title: item.nomeEspecialidade,
                        start: item.dataHoraInicio,
                        end: item.dataHoraFim,
                        color: "#0dcaf0",
                        display: "block",
                    });

                    if (item.frequencia != 1) {
                        let endDate = new Date(item.dataFinal);
                        endDate.setHours(23, 59, 59);
                        
                        let currentEventTimeStart = new Date(item.dataHoraInicio);
                        let currentEventTimeEnd = new Date(item.dataHoraFim)
                        let addFunction = item.frequencia == 2 ? addDays : item.frequencia == 3 ? addWeeks : item.frequencia == 4 ? addMonths : addYears;

                        currentEventTimeStart = addFunction(currentEventTimeStart, 1);
                        currentEventTimeEnd = addFunction(currentEventTimeEnd, 1);
                        while(currentEventTimeStart.getTime() <= endDate.getTime()) {
                            eventos.push({
                                id: item.id,
                                title: item.nomeEspecialidade,
                                start: currentEventTimeStart.toISOString(),
                                end: currentEventTimeEnd.toISOString(),
                                color: "#0dcaf0",
                                display: "block",
                            });

                            currentEventTimeStart = addFunction(currentEventTimeStart, 1);
                            currentEventTimeEnd = addFunction(currentEventTimeEnd, 1);
                        }
                    }
                });
            }            
            setCalendarConfiguration();
        }
    });
}