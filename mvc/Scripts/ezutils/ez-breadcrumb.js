EzHeader = {
    breadcrumbItem: undefined,
    collapse: undefined,
    buttons: undefined,
    show: function () {
        /**
         BREADCRUMB
         */
        let $breadcrumb = $("#ez-breadcrumb");
        let $breadcrumb_container = $breadcrumb.find(".ez_breadcrumb");
        $breadcrumb_container.html('');

        if (this.breadcrumbItem) {
            (function appendItem(item, $container) {
                let $item;

                if (item.url) {
                    $item = $('<a class="ez_breadcrumb_item">').attr("href", item.url).text(item.label);
                }
                else {
                    $item = $('<span class="ez_breadcrumb_item">').text(item.label);
                }

                $container.append($item);

                if (item.child) {
                    $container.append($('<span class="ez_fade"> / </span>'));
                    appendItem(item.child, $container)
                }
            })(this.breadcrumbItem, $breadcrumb_container);

            $("#ez-breadcrumb").show();
        }
        else {
            $("#ez-breadcrumb").hide();
        }

        /**
        BUTTONS
        */
        let $buttons_container = $("#ez-header-buttons");
        if (this.buttons && this.buttons.length > 0) {
            let flagFirstButtonElement = true;
            //<button class="ez_highlight" type="button">Guardar</button>
            //    <span class="ez_fade">o</span>
            //    <a class="ez_bold">Descartar</a>
            this.buttons.forEach(function (item) {
                let $element;

                if (flagFirstButtonElement) {
                    flagFirstButtonElement = false;
                }
                else {
                    $buttons_container.append(" ");
                }

                switch (item.type) {
                    case "buttonHighlight":
                        $element = $('<button class="ez_highlight" type="button">').text(item.label);
                        $buttons_container.append($element);
                        $element.on("click", item.action)
                        break;
                    case "text":
                        $element = $('<span class="ez_fade">').text(item.label);
                        $buttons_container.append($element);
                        break;
                    case "link":
                        $element = $('<a class="ez_bold">').text(item.label);
                        $buttons_container.append($element);
                        $element.on("click", item.action)
                        break;
                }
            });
        }

        if (this.collapse != undefined) {
            $(this.collapse.id).addClass("in");
        }

        $("#ez-header").show();
    }
};

$(function () {
    $("#ez-header").hide();
})