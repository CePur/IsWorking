let context = null;

const beep = (freq = 520, duration = 200, vol = 100) => {
    const oscillator = context.createOscillator();
    const gain = context.createGain();
    oscillator.connect(gain);
    oscillator.frequency.value = freq;
    oscillator.type = "square";
    gain.connect(context.destination);
    gain.gain.value = vol * 0.01;
    oscillator.start(context.currentTime);
    oscillator.stop(context.currentTime + duration * 0.001);
};


var isSoundOn = false;

if (!localStorage.getItem("isSoundOn")) {
    populateStorage(isSoundOn);
}

isSoundOn = ((localStorage.getItem("isSoundOn")) === "true");

function populateStorage(isSoundOn) {
    localStorage.setItem("isSoundOn", isSoundOn);

}


window.onload = function() {

    this.soundOn(isSoundOn);

    $("#clickableAwesomeFont").click(function() {

        if (isSoundOn) {
            isSoundOn = false;
            soundOn(isSoundOn);
        } else {
            isSoundOn = true;
            soundOn(isSoundOn);
            NotifyPermisson();
        }
    });

};

function soundOn(bool) {
    if (bool) {
        $("#clickableAwesomeFont").html('<i class="fas fa-volume-up fa-2x" style="color: rgb(165, 137, 26);"></i>');
    } else {
        $("#clickableAwesomeFont").html('<i class="fas fa-volume-mute fa-2x"></i>');
    }

    populateStorage(bool);
}


function NotifyPermisson() {
    if (("Notification" in window)) {
        if (Notification.permission !== "granted") {
            Notification.requestPermission();
        }
    }
}


function showNotify() {
    if (Notification.permission === "granted") {

        var notifyOptions = {
            body: "Müsait",
            silent: false
        };

        var notification = new Notification("Çalışıyor Mu ?", notifyOptions);

        document.addEventListener("visibilitychange",
            function() {
                if (document.visibilityState === "visible") {
                    // The tab has become visible so clear the now-stale Notification.                                                                                                                                      
                    notification.close();
                }
            });
    }
}