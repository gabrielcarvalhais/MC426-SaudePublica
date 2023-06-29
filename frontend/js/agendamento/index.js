var eventos = [];
var especialidades = [
    { id: 1, nome: 'Clínico geral' },
    { id: 2, nome: 'Cardiologia' },
    { id: 3, nome: 'Dermatologia' },
    { id: 4, nome: 'Ginecologia' },
    { id: 5, nome: 'Ortopedia' },
    { id: 6, nome: 'Pediatria' },
    { id: 7, nome: 'Oftalmologia' },
    { id: 8, nome: 'Psiquiatria' },
    { id: 9, nome: 'Endocrinologia' },
    { id: 10, nome: 'Neurologia' },
    { id: 11, nome: 'Radiologia' },
    { id: 12, nome: 'Fisioterapia' }
  ];

(async function (window, document, $, undefined) {
    "use strict";
    setCalendarConfiguration();
    setEspecialidadesSelect();    
    await verificaUsuarioLogado();
    await getEvents();
    $(".especialidade").change(getEvents);
    $(".tipo-agenda").change(tipoAgendaChange);
    if (userRole == "Paciente") {
      $("#btnNovoAgendamento").click(() => showModalAgendamento());
      $("#btnNovoAgendamento").css('display', 'flex');    
      $("#filtro-tipo-agenda").hide();
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

function setEspecialidadesSelect(){
      var container = $('#especialidades-container');
      var select = $('#especialidade');

      $.each(especialidades, function(index, especialidade) {
        var div = $('<div>').addClass('d-flex align-items-center mb-2 form-check-primary');
        var input = $('<input>').addClass('form-check-input especialidade').attr('id', especialidade.id).attr('type', 'checkbox');
        var label = $('<label>').text(especialidade.nome);
        
        div.append(input);
        div.append(label);
        container.append(div);

        var option = $('<option>').attr('value', especialidade.id).text(especialidade.nome);
        select.append(option);
      });
}

function showModalAgendamento(id){
    setFormConfiguration(id);
    $("#modalAgendamento").modal("show");
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
        Especialidades: especialidades,
        Todos: $(".tipo-agenda")[0].checked
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
            } else if (resposta.statusCode == 200) {   
                eventos = [];
                $.each(resposta.value, function (r, item) {
                    eventos.push({
                        id: item.id,
                        title: item.nomeEspecialidade,
                        start: item.dataHoraInicio,
                        end: item.dataHoraFim,
                        color: item.color,
                        display: "block",
                    });
                });
            }            
            setCalendarConfiguration();
        }
    });
}

function tipoAgendaChange(){
    let itemClicado = $(this)[0];
    if (itemClicado.id == 'all'){
        if (itemClicado.checked == true) 
            $(".tipo-agenda")[1].checked = false;        
        else 
            $(".tipo-agenda")[1].checked = true;
    }
    else{
        if (itemClicado.checked == true) 
            $(".tipo-agenda")[0].checked = false;        
        else 
            $(".tipo-agenda")[0].checked = true;
    }
    getEvents();
}