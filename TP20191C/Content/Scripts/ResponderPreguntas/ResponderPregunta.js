
tinymce.init({
    selector: 'textarea#Repuesta',
    height: 400,
    plugins: [
        'advlist autolink lists link image charmap print preview anchor textcolor',
        'searchreplace visualblocks code fullscreen',
        'insertdatetime media table paste code help wordcount'
    ],
    toolbar: 'undo redo| styleselect | bold underline italic fontsizeselect | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent |table| link image',
    fontsize_formats: '8pt 9pt 10pt 11pt 12pt 14pt 16pt 18pt 24pt 36pt 48pt 72pt'
});

