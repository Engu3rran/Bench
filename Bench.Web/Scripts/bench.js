$(document).ready(function() {
    function mettreAJourLeCompte() {
        $('body').css('cursor', 'progress');
        $.getJSON('/Api/Bench/CompterVoirie', function (réponse) {
            var données = JSON.parse(réponse);
            $('#nb-lignes-commune-persistance').html(données.Persistance.Communes);
            $('#nb-lignes-voie-persistance').html(données.Persistance.Voies);
            $('#nb-lignes-commune-reporting').html(données.Reporting.Communes);
            $('#nb-lignes-voie-reporting').html(données.Reporting.Voies);
            $('body').css('cursor', 'auto');
        });
    }

    $('#requete-persistance').on('click', function () {
        $('body').css('cursor', 'progress');
        $.get('/Api/Bench/RechercherVoiePersistance', function (réponse) {
            $('.console').append('<div>' + réponse + '</div>');
            mettreAJourLeCompte();
            $('body').css('cursor', 'auto');
        });
    })

    $('#requete-reporting').on('click', function () {
        $('body').css('cursor', 'progress');
        $.get('/Api/Bench/RechercherVoieReporting', function (réponse) {
            $('.console').append('<div>' + réponse + '</div>');
            mettreAJourLeCompte();
            $('body').css('cursor', 'auto');
        });
    })

    $('#insert-communes').on('click', function () {
        $('body').css('cursor', 'progress');
        $.post('/Api/Bench/InsererCommunes/' + $('#nb-insert-communes').val(), function (réponse) {
            $('.console').append('<div>' + réponse + '</div>');
            mettreAJourLeCompte();
            $('body').css('cursor', 'auto');
        });
    })

    $('#insert-persistance').on('click', function () {
        $('body').css('cursor', 'progress');
        $.post('/Api/Bench/InsererVoiePersistance/' + $('#nb-insert-persistance').val(), function (réponse) {
            $('.console').append('<div>' + réponse + '</div>');
            mettreAJourLeCompte();
            $('body').css('cursor', 'auto');
        });
    })

    $('#insert-reporting').on('click', function () {
        $('body').css('cursor', 'progress');
        $.post('/Api/Bench/InsererVoieReporting/' + $('#nb-insert-reporting').val(), function (réponse) {
            $('.console').append('<div>' + réponse + '</div>');
            mettreAJourLeCompte();
            $('body').css('cursor', 'auto');
        });
    })

    $('#insert-cohabitation').on('click', function () {
        $('body').css('cursor', 'progress');
        $.post('/Api/Bench/InsererVoie/' + $('#nb-insert-cohabitation').val(), function (réponse) {
            $('.console').append('<div>' + réponse + '</div>');
            mettreAJourLeCompte();
            $('body').css('cursor', 'auto');
        });
    })

    $('#delete-bases').on('click', function () {
        $('body').css('cursor', 'progress');
        $.post('/Api/Bench/SupprimerVoirie', function (réponse) {
            $('.console').append('<div>' + réponse + '</div>');
            mettreAJourLeCompte();
            $('body').css('cursor', 'auto');
        });
    })

    mettreAJourLeCompte();
})