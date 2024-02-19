function ajaxPainel()
{
    $(document).ready(function ()
    {
        $(".opcao_nave-bar").click(function (event) {
            event.preventDefault();

            var url = $(this).attr("href");

            $.ajax({
                url: url,
                type: "GET",
                success: function (result) {
                    $(".ajaxDiv").html(result);
                }
            });
        });
    });
}

function ajaxCadastro()
{
    $(document).ready(function () {
        var $botaoEnviar = $('#botaoEnviar');
        var tempo;

        $("#email").blur(async function () {
            var email = $(this).val();
            if (email.trim() !== '') {
                try {
                    var response = await $.ajax({
                        url: '/Participante/VerificarSeOEmailJaExiste',
                        method: 'GET',
                        data: { email: email }
                    });

                    if (response) {
                        $("#mensagemEmail").text("Este email já está cadastrado. Por favor, informe outro ou entre em contato com o suporte técnico do café com formação.");
                        $botaoEnviar.prop("disabled", true);
                    } else {
                        $("#mensagemEmail").text("");
                        $botaoEnviar.prop("disabled", false);
                    }
                } catch (error) {
                    console.error("Ocorreu um erro ao verificar este email! ");
                }
            }
        });

        $("#celular").blur(async function () {
            var celular = $(this).val();
            if (celular.trim() !== '') {
                try {
                    var response = await $.ajax({
                        url: '/Participante/VerificarSeOCelularJaExiste',
                        method: 'GET',
                        data: { celular: celular }
                    });

                    if (response) {
                        $("#mensagemCelular").text("Este número de contato já está cadastrado. Por favor, informe outro ou entre em contato com o suporte técnico do café com formação.");
                        $botaoEnviar.prop("disabled", true);
                    } else {
                        $("#mensagemCelular").text("");
                        $botaoEnviar.prop("disabled", false);
                    }
                } catch (error) {
                    console.error("Ocorreu um erro ao verificar este celular!");
                }
            }
        });
    });
}
