/**
UTILS
*/
function roundNumber(num, scale) {
    if (!("" + num).includes("e")) {
        return +(Math.round(num + "e+" + scale) + "e-" + scale);
    } else {
        var arr = ("" + num).split("e");
        var sig = ""
        if (+arr[1] + scale > 0) {
            sig = "+";
        }
        return +(Math.round(+arr[0] + "e" + sig + (+arr[1] + scale)) + "e-" + scale);
    }
}

function checkIfArray(obj) {
    let result = false;
    if (Object.prototype.toString.call(obj) === '[object Array]') {
        result = true;
    }
    return result;
}

function checkIfString(obj) {
    let result = false;
    if (typeof obj === 'string') {
        result = true;
    }
    return result;
}

function checkIfFunction(functionToCheck) {
    var getType = {};
    return functionToCheck && getType.toString.call(functionToCheck) === '[object Function]';
}

function initAutocomplete($hiddenInput, $autocompleteInput, $autocompleteDropdown, sourceOfData, onSelectCallback) {
    let _this = this;
    let wasOpen = false;

    $autocompleteInput.autocomplete({
        delay: 0,
        minLength: 0,
        // position: { collision: "flip flip" },
        // source: availableTags,
        // source: $.proxy(this, '_source')
        source: function (request, response) {
            // request.term es el texto escrito en el input.
            // Una vez que se obtengan los datos, llamar a response(data);.
            // data es un array de objetos.
            // Los objetos pueden tener cualquier numero de propiedades
            // pero por defecto jequery ui espera que sean objetos de tipo { label: '', value: '' }.
            // En el evento select se puede acceder a la propiedad value de un objeto
            // mediante ui.item.value.
            // En la extención _renderItem se puede acceder a la propiedad value de un objeto
            // mediante item.value.


            // AJAX para obtener datos o una lista simple
            if (checkIfArray(sourceOfData)) {
                let resultData = sourceOfData.filter(function (x) { return (new RegExp(request.term, 'i')).test(x.value); });
                response(resultData);
            }
            else if (checkIfString(sourceOfData)) {
                $.ajax({
                    dataType: "json",
                    url: sourceOfData,
                    data: { valor: request.term },
                    success: function (data, textStatus, jqXHR) {
                        response(data);
                    }
                });
            }
            else if (checkIfFunction(sourceOfData)) {
                var sourceUrl = sourceOfData();
                $.ajax({
                    dataType: "json",
                    url: sourceUrl,
                    data: { valor: request.term },
                    success: function (data, textStatus, jqXHR) {
                        response(data);
                    }
                });
            }
            else {
                console.error('No se ha podido determinar un tipo de fuente valida para el autocomplete');
            }
        },
        create: function (event, ui) {
            $(this).data('ui-autocomplete')._renderMenu = function (ul, items) {
                let that = this;
                $.each(items, function (index, item) {
                    that._renderItemData(ul, item);
                });
            };

            $(this).data('ui-autocomplete')._renderItemData = function (ul, item) {
                return this._renderItem(ul, item).data('ui-autocomplete-item', item);
            };

            $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
                if (item.showSearch === true) {
                    return $('<li>').addClass('autocomplete_option').append($('<a>').text('Buscar...')).appendTo(ul);
                } else if (item.value) {
                    return $('<li>').append($('<a>').text(item.value)).appendTo(ul);
                } else {
                    return $('<li>');
                }
            }
        },
        open: function () {
            // jqueryui configura un with que no es necesario
            // $('.ui-autocomplete').css('width', '');
        },
        select: function (event, ui) {
            // Actualizar model.clienteid
            // el objeto ui tiene el objeto item.
            // item por defecto tiene las propiedades label y value.
            // console.log('autocompleteselect');
            // console.log(ui);
            // console.log(ui.item);
            // this.value es el valor del input
            // ui.item.value es el valor del item seleccionado
            // evitar que jQuery ui actualice automaticamente el valor del input

            if (ui.item.showSearch) {
                updateAutocompleteBox('', '', $hiddenInput, $autocompleteInput);
                // _this.onAutocompleteSearch.emit();
            } else {
                updateAutocompleteBox(ui.item.key, ui.item.value, $hiddenInput, $autocompleteInput);
                if (onSelectCallback) { onSelectCallback($hiddenInput, $autocompleteInput, ui.item) };
            }

            return false;
        }
    });

    // $autocompleteInput.autocomplete('option', 'appendTo', '#' + $autocompleteInput.attr('id') + ' ~ ' + '*.autocomplete-list-container');
    // autocompleteElement.autocomplete('option', 'classes.ui-autocomplete');
    $autocompleteInput.autocomplete('option', 'classes.ui-autocomplete', 'ez_autocomplete ez_border_radius');

    $autocompleteDropdown.on('mousedown', function () {
        if ($autocompleteInput.attr('disabled')) { return; }
        wasOpen = $autocompleteInput.autocomplete('widget').is(':visible');
    });
    $autocompleteDropdown.on('click', function () {
        if ($autocompleteInput.attr('disabled')) { return; }
        $autocompleteInput.trigger('focus');

        // Close if already visible
        if (wasOpen) {
            return;
        }

        // Pass empty string as value to search for, displaying all results
        $autocompleteInput.autocomplete('search', '');
    });
}

function updateAutocompleteBox(key, value, $hiddenInput, $autocompleteInput) {
    $hiddenInput.val(key);
    $autocompleteInput.val(value);
    // TODO: disparar un evento cuando se seleccione un valor
}

function initModal($modal, $disparador, contentUrl, onAfterEmbeddedModalContent, onBeforeShow, onHide) {
    $.ajax({
        dataType: "html",
        url: contentUrl,
        success: function (data, textStatus, jqXHR) {
            $modalBodyContainer = $modal.find("*[data-modal-body-container]");
            $modalBody = $(data);
            $modalBody.appendTo($modalBodyContainer);
            $modal.appendTo("body");

            if (onAfterEmbeddedModalContent) { onAfterEmbeddedModalContent($modal, $modalBodyContainer); }

            $modal.modal({ show: false })

            if (onHide) {
                $modal.on('hidden.bs.modal', function (e) {
                    onHide($modal, $modalBodyContainer);
                })
            }

            // TODO: detectar el tipo de elemento para saber con que evento actuar
            $disparador.click(function () {
                if (onBeforeShow) { onBeforeShow($modal, $modalBodyContainer); }
                $modal.modal('show')
            });
        }
    });
}