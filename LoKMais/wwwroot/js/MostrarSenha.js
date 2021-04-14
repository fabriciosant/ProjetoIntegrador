function mostrarOcutarSenha() {
    var senha = document.getElementById("senha")
    if (senha.type == "password") {
        senha.type = "text"
    } else {
        senha.type = "password"
    }

    var confirmasenha = document.getElementById("confirmasenha")
    if (confirmasenha.type == "password") {
        confirmasenha.type = "text"
    } else {
        confirmasenha.type = "password"
    }
}