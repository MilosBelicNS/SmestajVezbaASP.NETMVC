function ValidateForm() {
    let forma = document.querySelector("#f1");
    let isValid = true;

    let brojSobe = forma["BrojSobe"].value;
    let brojSobeSpan = document.querySelector("#brojSobeSpan");

    let brojKreveta = forma["BrojKreveta"].value;
    let brojKrevetaSpan = document.querySelector("#brojKrevetaSpan");

    let cenaNoc = forma["CenaNoc"].value;
    let cenaNocSpan = document.querySelector("#cenaNocSpan");

    let nazivSmestaja = forma["NazivSmestaja"].value;
    let nazivSmestajaSpan = document.querySelector("#nazivSmestajaSpan");

    let imePrezime = forma["ImePrezime"].value;
    let imePrezimeSpan = document.querySelector("#imePrezimeSpan");

    let ocena = forma["Ocena"].value;
    let ocenaSpan = document.querySelector("#ocenSpan");

    let opis = forma["Opis"].value;
    let opisSpan = document.querySelector("#opisSpan");



    if (!opis) {
        opisSpan.innerHTML = "Ovo polje je obavezno!";
    } else {
        opisSpan.innerHTML = "";
    }
    if (!ocena) {
        ocenaSpan.innerHTML = "Ovo polje je obavezno!";
        isValid = false;
    }
    else if (ocena < 1) {
        ocenaSpan.innerHTML = "Vrednost polja mora biti izmedju 1 i 5";
        isValid = false;
    }
    else {
        ocenaSpan.innerHTML = "";
    }
    if (!brojSobe) {
        brojSobeSpan.innerHTML = "Ovo polje je obavezno!";
        isValid = false;
    }
    else if (brojSobe < 1) {
        brojSobeSpan.innerHTML = "Vrednost polja mora biti veca od 0";
        isValid = false;
    }
    else {
        brojSobeSpan.innerHTML = "";
    }

    if (!brojKreveta) {
        brojKrevetaSpan.innerHTML = "Ovo polje je obavezno!";
        isValid = false;
    }
    else if (brojKreveta < 1) {
        brojKrevetaSpan.innerHTML = "Vrednost polja mora biti veca od 1!";
        isValid = false;
    }
    else {
        brojKrevetaSpan.innerHTML = "";
    }
    if (!cenaNoc) {
        cenaNocSpan.innerHTML = "Ovo polje je obavezno!";
        isValid = false;
    }
    else if (cenaNoc < 1999 || cenaNoc >8000) {
        cenaNocSpan.innerHTML = "Vrednost polja mora biti izmedju 1999rsd i 8000rsd!";
        isValid = false;
    }
    else {
        cenaNocSpan.innerHTML = "";
    }
    if (nazivSmestaja.length < 3 || nazivSmestaja.length > 50) {
        nazivSmestajaSpan.innerHTML = "Ovo polje mora imati vrednost izmedju 3 i 50 karaktera!";
        isValid = false;
    }
    else {
        nazivSmestajaSpan.innerHTML = "";
    }
    if (imePrezime.length < 3 || imePrezime.length > 50) {
        imePrezimeSpan.innerHTML = "Ovo polje mora imati vrednost izmedju 3 i 50 karaktera!";
        isValid = false;
    } else {
        imePrezimeSpan = "";
    }
    var a = Date.UTC;
    if (datumPocetka > a) {
        imePrezimeSpan.innerHTML = "Ne mozete rezervisati sobu u proslom vremenu!";
        isValid = false;
    } else {
        imePrezimeSpan = "";
    }






      
  

    return isValid;
}



