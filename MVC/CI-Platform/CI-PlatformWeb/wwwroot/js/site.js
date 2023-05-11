function IncrementView(id) {
    $.ajax({
        type: 'POST',
        url: "/Volunteer/Story/IncrementStoryView",
        data: { storyId: id },
        success: function (res) {
            location.href = "/Volunteer/Story/StoryDetails/" + id;
        },
        error: function (data) {

        }
    });
}

$("#notification-btn").on("click", function (e) {
    $("#notification").toggleClass("d-none");
    GetAllNotification();
    e.stopPropagation()
});
$(document).on("click", function (e) {
    if ($(e.target).is("#notification, #notification *") === false) {
        $("#notification").addClass("d-none");
    }
});
$("#open-notify-setting").on("click", function () {
    $("#notification-body, #notification-spinner").addClass("d-none");
    $("#notification-setting-body").removeClass("d-none");
});
function notificationBack() {
    $("#notification-setting-body").addClass('d-none');
    $("#notification-body").removeClass('d-none');
}


function GetAllNotification() {
    $.ajax({
        type: 'POST',
        url: "/Volunteer/Notification/GetNotificationofUser",
        beforeSend: function () {
            $("#notification-spinner").removeClass("d-none");
        },
        success: function (res) {
            $('#notification-setting-body').html($($.parseHTML(res)).filter("#notification-setting-body-partial")[0].innerHTML);
            $('#notification-body').html($($.parseHTML(res)).filter("#notification-body-partial")[0].innerHTML);
            //$('#notification-count').html($($.parseHTML(res)).filter("#notification-count-partial")[0].innerHTML);
        },
        error: function (data) {
            alert("some Error from Notification Partial");
        },
        complete: function () {
            $("#notification-spinner").addClass("d-none");
        }
    });
}


function SaveNotificationSettings() {
    var settingsarray = [];
    $("input:checkbox[name=settings]:checked").each(function () {
        settingsarray.push($(this).val());
    });
    $.ajax({
        type: 'POST',
        url: "/Volunteer/Notification/SaveNotificationSettings",
        data: { 'settingsarray': settingsarray },
        beforeSend: function () {
            $("#notification-spinner").removeClass("d-none");
        },
        success: function (res) {
            $("#notification-setting-body").addClass('d-none');
            $("#notification-body").removeClass('d-none');
            toastr.success("Changes Saved Successfully", "Success Message", { timeOut: 5000, "positionClass": "toast-top-right", "closeButton": true, "progressBar": true });
        },
        error: function (data) {
            alert("some error from SaveNotificationSettings.");
        },
        complete: function () {
            $("#notification-spinner").addClass("d-none");
        }
    });
    console.log(settingsarray);
}

function ClearAllNotification() {
    $.ajax({
        type: 'POST',
        url: "/Volunteer/Notification/ClearAllNotification",
        success: function (res) {
            $('#notification-body').html("");
            GetTotalNotifications();
        },
        error: function (data) {
            alert("some Error from Notification Partial");
        }
    });
}

function GetTotalNotifications() {
    $.ajax({
        type: 'POST',
        url: "/Volunteer/Notification/GetTotalNotifications",
        data: {},
        success: function (res) {
            if(res > 0)
                $("#notification-count").html(res);
            else
                $("#notification-count").addClass("d-none");
        },
        error: function (data) {
            
        }
    });
}

function MakeReadedNotification(usernotificationid) {
    $.ajax({
        url: "/Volunteer/Notification/MakeReadedNotification",
        type: 'POST',
        data: { 'usernotificationid': usernotificationid },
        success: function (res) {
            $('.unreaded-' + res).addClass('d-none');
            $('.readed-' + res).removeClass('d-none');
            GetTotalNotifications();
            toastr.success("Notification marked as read", "EVPP Says", { timeOut: 5000, "positionClass": "toast-top-right", "closeButton": true, "progressBar": true });
        },
        error: function (data) {
            alert("some error from read-unread.");
        }
    });
}