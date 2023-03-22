CKEDITOR.ClassicEditor.create(document.getElementById("editor"), {
    toolbar: {
        items: [
            'bold', 'italic', 'strikethrough', 'underline', '|', 'subscript', 'superscript', 'removeFormat',
        ],
        shouldNotGroupWhenFull: true
    },
    list: {
        properties: {
            styles: true,
            startIndex: true,
            reversed: true
        }
    },
    placeholder: 'Write Your Story Here!..',
    htmlEmbed: {
        showPreviews: true
    },
    removePlugins: [
        'CKBox',
        'CKFinder',
        'EasyImage',
        'RealTimeCollaborativeComments',
        'RealTimeCollaborativeTrackChanges',
        'RealTimeCollaborativeRevisionHistory',
        'PresenceList',
        'Comments',
        'TrackChanges',
        'TrackChangesData',
        'RevisionHistory',
        'Pagination',
        'WProofreader',
        'MathType'
    ]
});