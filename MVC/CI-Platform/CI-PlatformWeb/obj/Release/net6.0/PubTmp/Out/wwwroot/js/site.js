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