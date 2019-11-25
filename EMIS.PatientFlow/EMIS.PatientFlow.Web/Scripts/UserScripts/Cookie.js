$(document).ready(function () {
    var isVisible = readFromLocalStorage("isVisible");
    if (isVisible !== null && !isVisible) {
        document.getElementsByClassName('cookie-footer')[0].style.display = 'none';
    }
    else {
        const cookieAcceptance = getAcceptanceCookie();
        writeToLocalStorage("isVisible", !cookieAcceptance);
        document.getElementsByClassName('cookie-footer')[0].style.display = 'block';
    }
    document.getElementById("footer-cookie-close").onclick = footerCookieClose;
    function footerCookieClose(event) {
        event.preventDefault();
        writeToLocalStorage("isVisible", false);
        document.cookie = 'CookieAcceptance=true; path=/';
        document.getElementsByClassName('cookie-footer')[0].style.display = 'none';
    }
});

function readFromLocalStorage(key) {
    if (typeof (Storage) === 'undefined') {
        return null;
    }
    return JSON.parse(localStorage.getItem(key));
}

function getAcceptanceCookie() {
    const cookie = document.cookie.split('; ');
    const cookieAcceptance = (cookie.indexOf('CookieAcceptance=true') === -1) ? false : true;
    return cookieAcceptance;
}

function writeToLocalStorage(key, value) {
    if (typeof (Storage) === 'undefined') {
        alert("Your browser doesn't support HTML5 LocalStorage which this site make use of. Some features may not be available. Consider upgrading your browser to the latest version");
        return false;
    }
    value = JSON.stringify(value);

    try {
        window.localStorage.setItem(key, value);
    }
    catch (e) {
        if (e === QUOTA_EXCEEDED_ERR) {
            alert('Local storage Quota exceeded! .Clearing localStorage');
            localStorage.clear();
            window.localStorage.setItem(key, value);
        }
    }
    return true;
}