$(function () {
    $("#CmsKitProContactSettingsForm").on("submit", function (e) {
        e.preventDefault();
        
        var form = $(this).serializeFormToObject();
        
        dataGap.cmsKit.admin.contact.contactSetting.update(form).then(function (result) {
            $(document).trigger("JellogSettingSaved");
        });
    });
});