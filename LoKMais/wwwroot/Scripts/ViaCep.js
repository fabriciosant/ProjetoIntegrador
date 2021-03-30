$(document).ready(function () {
    mascaraCep()
})

function mascaraCep() {
    $('#cep').setMask('cep')
}

$('#cep').blur(function () {
    var cep = $(this).val().replace(/\D/g, '')
    if (cep != "") {
        var validarCep = /^[0-9]{8}$/
        if (validarCep.test(cep)) {
            $('#logradouro').val("Aguarde ...")
            $('#bairro').val("Aguarde ...")
            $('#cidade').val("Aguarde ...")
            $('#uf').val("Aguarde ...")
            $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {
                if (!("erro" in dados)) {
                    $('#logradouro').val(dados.logradouro)
                    $('#bairro').val(dados.bairro)
                    $('#cidade').val(dados.localidade)
                    $('#uf').val(dados.uf)
                }
                else {
                    limparCampos()
                    alert("CEP não encontrado!")
                }
            })
        }
        else {
            limparCampos()
            alert("Formato de CEP inválido")
        }
    }
    else {
        limparCampos()
    }
})

function limparCampos() {
    $('#logradouro').val('')
    $('#bairro').val('')
    $('#cidade').val('')
    $('#uf').val('')
}