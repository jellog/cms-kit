(function ($) {
    var l = jellog.localization.getResource("CmsKit");

    jellog.widgets.CmsContact = function ($widget) {
        var widgetManager = $widget.data("jellog-widget-manager");

        function init() {
            $widget.find(".contact-form").on('submit', '', function (e) {
                e.preventDefault();

                var formAsObject = $(this).serializeFormToObject();

                dataGap.cmsKit.public.contact.contactPublic.sendMessage(
                    {
                        name: formAsObject.name,
                        subject: formAsObject.subject,
                        email: formAsObject.emailAddress,
                        message: formAsObject.message,
                        recaptchaToken: formAsObject.recaptchaToken
                    }
                ).then(function () {
                    jellog.message.success(l("ContactSuccess"))
                        .then(function () {
                            widgetManager.refresh($widget);
                        });
                })
            });
        }

        return {
            init: init
        }
    };
})(jQuery);
