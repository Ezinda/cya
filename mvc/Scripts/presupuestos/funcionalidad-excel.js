$("#file").on("change", function (e) {

    //getting form into Jquery Wrapper Instance to enable JQuery Functions on form
    var form = $("#UploadExcel");

    //Serializing all For Input Values (not files!) in an Array Collection so that we can iterate this collection later.
    var params = form.serializeArray();

    //Getting Files Collection
    var files = $("#file")[0].files;

    //Declaring new Form Data Instance
    var formData = new FormData();
    //Looping through uploaded files collection in case there is a Multi File Upload. This also works for single i.e simply remove MULTIPLE attribute from file control in HTML.
    for (var i = 0; i < files.length; i++) {
        formData.append(files[i].name, files[i]);
    }
    //Now Looping the parameters for all form input fields and assigning them as Name Value pairs.
    $(params).each(function (index, element) {
        formData.append(element.name, element.value);
    });

    //disabling Submit Button so that user cannot press Submit Multiple times
    var btn = $(this);
    btn.text("Procesando ...");
    btn.prop("disabled", true);

    $.ajax({
        url: form.attr("action"), //You can replace this with MVC/WebAPI/PHP/Java etc
        method: "post",
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data);
            //alert("pase");
            for (var i = 0; i < data.Items.length; i++) {
                var itemData = data.Items[i];
                let mapping = data.Items.map(x => x.NumeroPosicion);
                mapping.push(0);
                let lastPosicion = Math.max.apply(Math, mapping);              

                let item = {
                    id: itemData.PresupuestoId,

                    numeroPosicion: lastPosicion + 1,// ITEMS_COUNTERNUEVOS,

                    tipologiaId: "",
                    tipologiaThumbnailUrl: "",
                    tipologiaUrl: "",

                    posicion: "",
                    descripcion: "",

                    unidades: 0,
                    preciounitario: 0,
                    importe: 0,

                    ancho: 0,
                    alto: 0,
                    carpinteria: 0.0,
                    tapajuntas: 0.0,

                    vidriosId: $("#VidrioId").val(),
                    vidriosDescripcion: $("#vidrioId_autocomplete").val(),//'',
                    vidriosPrecio: $("#VidrioId").attr('data-item-preciounitario-value'),
                    vidriosCalculado: 0.0,

                    colocacionId: $("#ColocacionId").val(),//null,
                    colocacionDescripcion: $("#colocacionId_autocomplete").val(),//'',
                    colocacionPrecio: $("#ColocacionId").attr('data-item-preciounitario-value'),//0.0,
                    colocacionCalculado: 0.0,
                    detalle: "",

                    estado: "NUEVO"
                };

                var cotizacion = parseFloat($("#CotizacionId").val().replace(/\./g, "").replace(/,/g, ".") || 0.0);

                item.tipologiaId = itemData.ArchivoTipologiaId;
                item.tipologiaThumbnailUrl = "/Archivo/GetThumbnail" + "?" + "Id=" + itemData.ArchivoTipologiaId + "&" + "sizeClass=" + 3;
                item.tipologiaUrl = "/Archivo/Get" + "?" + "Id=" + itemData.ArchivoTipologiaId;

                item.posicion = itemData.Posicion;
                item.descripcion = itemData.Descripcion;
                item.unidades = itemData.Unidades;
                item.preciounitario = parseFloat(itemData.PrecioUnitario) *cotizacion;
                item.importe = parseFloat(itemData.Importe) *cotizacion;

                item.ancho = parseFloat(itemData.Ancho);
                item.alto = parseFloat(itemData.Alto);
                item.carpinteria = parseFloat(itemData.Carpinteria) * cotizacion;
                item.tapajuntas = parseFloat(itemData.Tapajuntas) * cotizacion;           
                item.detalle = itemData.Detalle;

                ITEMS_STORE.push(item);
                addRow(item, $GRID, handleOnAfterEmbeddedRow);

                ITEMS_COUNTERNUEVOS++;
            }

            btn.prop("disabled", false);
            btn.text("Importar Excel ...");
            $("#file").val("");
        },
        error: function (error) { alert("Error"); }
    });

});


function addRow(data, $grid, onAfterEmbeddedRow, onBeforeShowDetail) {
    template = $grid.find("*[data-grid-row-template]").html();

    $tbody = $grid.find("tbody");
    $rowItem = $(template);
    $rowItem.appendTo($tbody);

    $grid.find("tr[data-row-default]").hide();

    /*
    BIND TOGGLER
    */
    $rowItem.find("*[data-headercol='toggler']").on("click", function () {
        toggleRowDetail($(this), onBeforeShowDetail);
    });

    if (onAfterEmbeddedRow) { onAfterEmbeddedRow($rowItem, data); }
}