$.ajax({
    type: "GET",
    url: 'Volunteer/Home/Index',
    data: { Id = id },
    success: function (data) {

    },
    failure: function (e) {
        errorMessage(e.responseText);
    },
    error: function (e) {
        errorMessage(e.responseText);
    }
});