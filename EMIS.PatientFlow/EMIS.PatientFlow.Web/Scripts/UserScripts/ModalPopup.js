function showDialog(options) {
    options = $.extend({
        id: 'orrsDiag',
        title: null,
        text: null,
        negative: false,
        positive: false,
        cancelable: true,
        contentStyle: null,
        onLoaded: false,
        anchorClick: false,
        liElement: false
    }, options);

    // remove existing dialogs
    $(document).unbind("keyup.dialog");

    if (options.liElement === "Delete") {
        $('.dialog-containerpopup').remove();
        $('<div id="' + options.id + '" class="dialog-containerpopup"><div class="mdl-card mdl-shadow--16dp modalPopup"></div></div>').appendTo("body");
    }
    else if (options.liElement === "KioskKey") {
        $('.dialog-container').remove();
        $('<div id="' + options.id + '" class="dialog-container"><div class="mdl-card mdl-shadow--16dp"></div></div>').appendTo("body");
    }

    var dialog = $('#orrsDiag');
    var content = dialog.find('.mdl-card');
    if (options.contentStyle !== null) content.css(options.contentStyle);
    if (options.title !== null) {
        if (options.liElement === "Delete") {
            $('<label class="modalPopUp_heading h6">' + options.title + '</label>').appendTo(content);
        }
        else {
            $('<label class="modalPopUp_heading h6 KioskKeyHeading">' + options.title + '</label>').appendTo(content);
        }
    }
    if (options.text !== null) {
        if (options.liElement !== "KioskKey") {
            $('<p class="modalPopUp_body">' + options.text + '</p>').appendTo(content);
        }
        else {
            $(options.text).appendTo(content);
        }
    }
    if (options.negative || options.positive || options.anchorClick) {
        var buttonBar = $('<div></div>');
       
        if (options.positive) {
            options.positive = $.extend({
                id: 'positive',
                title: 'OK',
                onClick: function () {
                    return false;
                }
            }, options.positive);
            var posButton = $('<button class="mdl-button  mdl-js-button mdl-js-ripple-effect modalPopUp_buttons" id="' + options.positive.id + '" >' + options.positive.title + '</button>');
            posButton.click(function (e) {
                e.preventDefault();
                if (!options.positive.onClick(e))
                    hideDialog(dialog);
            });
            posButton.appendTo(buttonBar);
        }

        if (options.negative) {
            options.negative = $.extend({
                id: 'negative',
                title: 'Cancel',
                onClick: function () {
                    return false;
                }
            }, options.negative);
            var negButton = $('<button class="mdl-button  mdl-js-button mdl-js-ripple-effect modalPopUp_buttons" id="' + options.negative.id + '" >' + options.negative.title + '</button>');
            negButton.click(function (e) {
                e.preventDefault();
                if (!options.negative.onClick(e))
                    hideDialog(dialog);
            });

            negButton.appendTo(buttonBar);
        }

        buttonBar.appendTo(content);
    }
    componentHandler.upgradeDom();
    if (options.cancelable) {
        content.click(function (e) {
            e.stopPropagation();
        });
    }
    setTimeout(function () {
        dialog.css({ opacity: 1 });
        if (options.onLoaded)
            options.onLoaded();
    }, 1);

    $('.KioskKeyCopy').click(function () {
        var kioskGuid = $.trim($(this).closest("tr").find('.kioskGuid').text());
        var kiosk = $('<input id="KioksGuidinput">').val(kioskGuid).appendTo('body').select();
        var success = document.execCommand("copy");
        document.getElementById("KioksGuidinput").remove();
        if (success) {
            createSnackbar('Kiosk registration key copied');    
        }
        else {
            createSnackbar('Kiosk registration key not copied');
        }
    });
    $('.syncKeyCopy').click(function () {
        var syncGuid = $.trim($(this).closest("tr").find('.SyncProductKey').text());
        var sync = $('<input id="syncinput">').val(syncGuid).appendTo('body').select();
        var success = document.execCommand("copy");
        document.getElementById("syncinput").remove();
        if (success) {
            createSnackbar('Sync service key copied');    
        }
        else {
            createSnackbar('Sync service key not copied');
        }
    });
}


function hideDialog(dialog) {
    $(document).unbind("keyup.dialog");
    dialog.css({ opacity: 0 });
    setTimeout(function () {
        dialog.remove();
    }, 400);
}

function showLoadingLoginPage() {
    // remove existing loaders
    $('.loading-container').remove();
    $('<div id="orrsLoader" class="loading-container"><div><div class="mdl-spinner mdl-js-spinner mdl-spinner--single-color is-active"></div></div></div>').appendTo("body");
        
    setTimeout(function () {
        $('#orrsLoader').css({ opacity: 1 });
    }, 1);
}

function showLoading() {
    // remove existing loaders
    $('.loading-container').remove();
    $('<div id="orrsLoader" class="loading-container"><div><div class="mdl-spinner mdl-js-spinner mdl-spinner--single-color is-active"></div></div></div>').appendTo("body");

    componentHandler.upgradeElements($('.mdl-spinner').get());
    setTimeout(function () {
        $('#orrsLoader').css({ opacity: 1 });
    }, 1);
}

function hideLoading() {
    $('#orrsLoader').css({ opacity: 0 });
    setTimeout(function () {
        $('#orrsLoader').remove();
    }, 400);
}


var createSnackbar = (function () {
    var previous = [];
    return function (message, actionText,action) {
        var snackbar = document.createElement('div');
        snackbar.className = 'paper-snackbar body2 onPrimary';

        var text = document.createTextNode(message);
        snackbar.appendChild(text);

        previous.push(snackbar);

        document.body.appendChild(snackbar);

        if (actionText) {
            var actionButton = document.createElement('button');
            actionButton.className = 'action';
            actionButton.innerHTML = actionText;
            actionButton.addEventListener('click', action);
            snackbar.appendChild(actionButton);

        }
        snackbar.dismiss = function () {
            this.style.opacity = 0;
        };

        getComputedStyle(snackbar).bottom;
        if (previous.length > 0) {
            var border = 0;
            previous.forEach(function (element, index) {
                element.setAttribute("id", index + 1);
                if (element.getAttribute("id") <= previous.length) {
                    var el = document.getElementById(index);
                    snackbar.style.bottom = border + "px";
                    snackbar.style.zIndex = 99999;
                    snackbar.style.opacity = 1;
                    border += 75;
                }
            });
        }
        snackbar.style.opacity = 1;

        setTimeout(function (e) {
            snackbar.dismiss();
            previous = [];
        }.bind(snackbar), 5000);

    };
})();

function SetAlertImageStyleForSmallScreen() {
    $('#KioskAlertImages').css({ 'width': '140%' });
    $('#KioskAlertImages').parent().css({ 'float': 'unset', 'margin-top': '0%', 'margin-right': '0%' });
}
function SetAlertImageStyleForMediumScreen() {
    $('#KioskAlertImages').css({ 'width': '' });
    $('#KioskAlertImages').parent().css({ 'float': 'unset', 'margin-top': '0%', 'margin-right': '0%' });
}
function SetAlertImageStyleForLargeSceen() {
    $('#KioskAlertImages').css({ 'width': '' });
    $('#KioskAlertImages').parent().css({ 'float': 'right', 'margin-top': '-15%', 'margin-right': '30%' });
}
function SetAlertImageStyleForScreen() {
    $('#KioskAlertImages').css({ 'width': '' });
    $('#KioskAlertImages').parent().css({ 'float': 'right', 'margin-top': '-20%', 'margin-right': '0%' });
}

function onfocusEvent(val) {
    setActiveOutline($(val));
    onfocusEditEvent(val);
}

function onfocusEditEvent(val) {
    var fs = val.closest('fieldset').attr('name');
    //var legendData = '<text>' + fs + '</text>';
    val.closest('fieldset').children().eq(2).hide();
    val.closest('fieldset').children().eq(0).html(fs);
    //val.closest('fieldset').children().eq(0).html(legendData);
}

function outFocusEvent(val) {
    val.closest('fieldset').children().eq(2).show();
    val.closest('fieldset').children().eq(0).html(' ');
}

var format = function (str, col) {
    col = typeof col === 'object' ? col : Array.prototype.slice.call(arguments, 1);
    return str.replace(/\{\{|\}\}|\{(\w+)\}/g, function (m, n) {
        if (m === "{{") { return "{"; }
        if (m === "}}") { return "}"; }
        return col[n];
    });
};

function setActiveOutline(val) {
    $(val).closest('fieldset').addClass("Active-outline");
    $(val).closest('fieldset').removeClass("Normal-outline");
}
function setNormalOutline(val) {
    $(val).closest('fieldset').removeClass("Active-outline");
    $(val).closest('fieldset').addClass("Normal-outline");
}

function CheckboxFunctionalityForIE(thisObj, checked) {
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))  // If Internet Explorer, return version number
    {
        thisObj.checked = false; // uncheck the box every time
        if (checked) {
            thisObj.checked = checked;
            $(thisObj).parent().addClass("is-checked");
        }
        else {
            thisObj.checked = false;
            $(thisObj).parent().removeClass("is-checked");
        }
    }
}