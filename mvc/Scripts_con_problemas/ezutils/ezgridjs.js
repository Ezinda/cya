/*
 * Librerías para el manejo de grillas en plataformas Web
 * desarrolladas por EZiNDA S.A.
*/

/*
 * ORIGINAL SOURCE:
 *
 * Gijgo JavaScript Library v0.4.5
 * http://gijgo.com/
 *
 * Copyright 2014, 2015 gijgo.com
 * Released under the MIT license
 */

if (typeof (ez) === "undefined") {
    ez = {};
}
if (typeof (ez.ezGrid) === "undefined") {
    ez.ezGrid = {};
}

ez.ezGrid.configuration = {
    base: {
        /** The data source that is going to be used for data in rows during rendering.<br />
          * If set to string, then the grid is going to use this string as a url for ajax requests to the server.<br />
          * If set to object, then the grid is going to use this object as settings for the <a href="http://api.jquery.com/jquery.ajax/" target="_new">jquery ajax</a> function.<br />
          * If set to array, then the grid is going to use the array as data for rows.
          * @memberof Grid
          * @type (string|object|array)
          * @default undefined
          * @example <table id="grid"></table>
          * <script>
          *     var grid = $("#grid").grid({
          *         dataSource: "/version_0_4/Demos/GetPlayers",
          *         columns: [ { field: "Name" }, { field: "PlaceOfBirth" } ]
          *     });
          * </script>
          * @example <table id="grid" data-source="/version_0_4/Demos/GetPlayers">
          *     <thead>
          *         <tr>
          *             <th width="20">ID</th>
          *             <th>Name</th>
          *             <th>PlaceOfBirth</th>
          *         </tr>
          *     </thead>
          * </table>
          * <script>
          *     $("#grid").grid();
          * </script>
          * @example <table id="grid"></table>
          * <script>
          *     var grid, onSuccessFunc = function (response) { 
          *         alert("The result contains " + response.records.length + " records.");
          *         grid.render(response);
          *     };
          *     grid = $("#grid").grid({
          *         dataSource: { url: "/version_0_4/Demos/GetPlayers", data: {}, success: onSuccessFunc },
          *         columns: [ { field: "Name" }, { field: "PlaceOfBirth" } ]
          *     });
          * </script>
          * @example <table id="grid"></table>
          * <script>
          *     var data = [
          *         { "ID": 1, "Name": "Hristo Stoichkov", "PlaceOfBirth": "Plovdiv, Bulgaria" },
          *         { "ID": 2, "Name": "Ronaldo Luis Nazario de Lima", "PlaceOfBirth": "Rio de Janeiro, Brazil" },
          *         { "ID": 3, "Name": "David Platt", "PlaceOfBirth": "Chadderton, Lancashire, England" }
          *     ];
          *     $("#grid").grid({
          *         dataSource: data,
          *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
          *     });
          * </script>
          */
        dataSource: undefined,
        /** An array that holds the configurations of each column from the grid.
          * @memberof Grid
          * @type array
          * @example <table id="grid"></table>
          * <script>
          *     $("#grid").grid({
          *         dataSource: "/version_0_4/Demos/GetPlayers",
          *         columns: [ { field: "ID", width: 30 }, { field: "Name" }, { field: "PlaceOfBirth", name: "Birth Place" } ]
          *     });
          * </script>
          */
        columns: [
            {
                /** If set to true the column will not be displayed in the grid. By default all columns are displayed.
                  * @alias columns.hidden
                  * @memberof Grid
                  * @type boolean
                  * @default false
                  */
                hidden: false,
                /** The width of the column. Numeric values are treated as pixels.
                  * If the width is undefined the width of the column is not set and depends on the with of the table(grid).
                  * @alias columns.width
                  * @memberof Grid
                  * @type int|string
                  * @default undefined
                  * @example <table id="grid"></table>
                  * <script>
                  *     $("#grid").grid({
                  *         dataSource: "/version_0_4/Demos/GetPlayers",
                  *         columns: [
                  *             { field: "ID", width: 20 },
                  *             { field: "Name", width: 120 },
                  *             { field: "PlaceOfBirth" }
                  *         ]
                  *     });
                  * </script>
                  */
                width: undefined,
                /** Indicates if the column is sortable.
                  * If set to true the user can click the column header and sort the grid by the column source field.
                  * @alias columns.sortable
                  * @memberof Grid
                  * @type boolean
                  * @default false
                  * @example <table id="grid"></table>
                  * <script>
                  *     $("#grid").grid({
                  *         dataSource: "/version_0_4/Demos/GetPlayers",
                  *         columns: [
                  *             { field: "ID" },
                  *             { field: "Name", sortable: true },
                  *             { field: "PlaceOfBirth", sortable: false },
                  *             { field: "DateOfBirth", type: "date", title: "Birth Date" }
                  *         ]
                  *     });
                  * </script>
                  */
                sortable: false,
                /** Indicates the type of the column.
                  * @alias columns.type
                  * @memberof Grid
                  * @type undefined|checkbox|icon|date
                  * @default undefined
                  */
                type: undefined, //checkbox
                /** The caption that is going to be displayed in the header of the grid.
                  * @alias columns.title
                  * @memberof Grid
                  * @type string
                  * @default undefined
                  * @example <table id="grid"></table>
                  * <script>
                  *     $("#grid").grid({
                  *         dataSource: "/version_0_4/Demos/GetPlayers",
                  *         columns: [
                  *             { field: "ID" },
                  *             { field: "Name", title: "Player" },
                  *             { field: "PlaceOfBirth", title: "Place of Birth" },
                  *             { field: "DateOfBirth", type: "date", title: "Birth Date" }
                  *         ]
                  *     });
                  * </script>
                  */
                title: undefined, //TODO: rename to title
                /** The field name to which the column is bound.
                 * If the columns.name is not defined this value is used as columns.name.
                 * @alias columns.field
                 * @memberof Grid
                 * @type string
                 * @default undefined
                 * @example <table id="grid"></table>
                 * <script>
                 *     $("#grid").grid({
                 *         dataSource: "/version_0_4/Demos/GetPlayers",
                 *         columns: [
                 *             { field: "ID" },
                 *             { field: "Name" },
                 *             { field: "PlaceOfBirth", title: "Place of Birth" },
                 *             { field: "DateOfBirth", type: "date" }
                 *         ]
                 *     });
                 * </script>
                 */
                field: undefined,
                /** This setting control the alignment of the text in the cell.
                  * @alias columns.align
                  * @memberof Grid
                  * @type left|right|center|justify|initial|inherit
                  * @default "left"
                  * @example <table id="grid"></table>
                  * <script>
                  *     $("#grid").grid({
                  *         dataSource: "/version_0_4/Demos/GetPlayers",
                  *         columns: [
                  *             { field: "ID", align: "center" },
                  *             { field: "Name", align: "right" },
                  *             { field: "PlaceOfBirth", align: "left" }
                  *         ]
                  *     });
                  * </script>
                  */
                align: "left",
                /** The name(s) of css class(es) that are going to be applied to the text wrapper inside the cell.
                  * @alias columns.cssClass
                  * @memberof Grid
                  * @type string
                  * @default undefined
                  * @example <table id="grid"></table>
                  * <style>
                  * .nowrap { white-space: nowrap }
                  * .bold { font-weight: bold }
                  * </style>
                  * <script>
                  *     $("#grid").grid({
                  *         dataSource: "/version_0_4/Demos/GetPlayers",
                  *         columns: [
                  *             { field: "ID", width: 20 },
                  *             { field: "Name", width: 100, cssClass: "nowrap bold" },
                  *             { field: "PlaceOfBirth" }
                  *         ]
                  *     });
                  * </script>
                  */
                cssClass: undefined,
                /** The text for the cell tooltip.
                  * @alias columns.tooltip
                  * @memberof Grid
                  * @type string
                  * @default undefined
                  * @example <table id="grid"></table>
                  * <script>
                  *     $("#grid").grid({
                  *         dataSource: "/version_0_4/Demos/GetPlayers",
                  *         columns: [
                  *             { field: "ID", tooltip: "This is my tooltip 1." },
                  *             { field: "Name", tooltip: "This is my tooltip 2." },
                  *             { field: "PlaceOfBirth", tooltip: "This is my tooltip 3." }
                  *         ]
                  *     });
                  * </script>
                  */
                tooltip: undefined,
                /** Css class for icon that is going to be in use for the cell.
                  * This setting can be in use only with combination of type icon.
                  * @alias columns.icon
                  * @memberof Grid
                  * @type string
                  * @default undefined
                  * @example <table id="grid"></table>
                  * <script>
                  *     $("#grid").grid({
                  *         dataSource: "/version_0_4/Demos/GetPlayers",
                  *         columns: [
                  *             { field: "ID" },
                  *             { field: "Name" },
                  *             { field: "PlaceOfBirth" },
                  *             { title: "", field: "Edit", width: 20, type: "icon", icon: "ui-icon-pencil", events: { "click": function (e) { alert("name=" + e.data.record.Name); } } }
                  *         ]
                  *     });
                  * </script>
                  */
                icon: undefined,
                /** Configuration object with event names as keys and functions as values that are going to be bind to each cell from the column.
                  * Each function is going to receive event information as a parameter with info in the "data" field for id, field name and record data.
                  * @alias columns.events
                  * @memberof Grid
                  * @type function
                  * @default undefined
                  * @example <table id="grid"></table>
                  * <script>
                  *     $("#grid").grid({
                  *         dataSource: "/version_0_4/Demos/GetPlayers",
                  *         columns: [
                  *             { field: "ID" },
                  *             { 
                  *               field: "Name", 
                  *               events: {
                  *                 "mouseenter": function (e) {
                  *                     e.stopPropagation();
                  *                     $(e.currentTarget).css("background-color", "red");
                  *                 },
                  *                 "mouseleave": function (e) {
                  *                     e.stopPropagation();
                  *                     $(e.currentTarget).css("background-color", ""); 
                  *                 }
                  *               }
                  *             },
                  *             { field: "PlaceOfBirth" },
                  *             { 
                  *               title: "", field: "Info", width: 20, type: "icon", icon: "ui-icon-info", 
                  *               events: { 
                  *                 "click": function (e) { 
                  *                     alert("record with id=" + e.data.id + " is clicked."); } 
                  *                 }
                  *             }
                  *         ]
                  *     });
                  * </script>
                  */
                events: undefined,
                /** Format the date when the type of the column is date. 
                  * This configuration setting is going to work only if you have implementation of format method for the Date object.
                  * You can use external libraries like http://blog.stevenlevithan.com/archives/date-time-format for that.
                  * @alias columns.format
                  * @memberof Grid
                  * @type string
                  * @default undefined
                  * @example <table id="grid"></table>
                  * <script src="http://stevenlevithan.com/assets/misc/date.format.js"></script>
                  * <script>
                  *     $("#grid").grid({
                  *         dataSource: "/version_0_4/Demos/GetPlayers",
                  *         columns: [
                  *             { field: "ID" },
                  *             { field: "Name" },
                  *             { field: "DateOfBirth", type: 'date', format: 'HH:MM:ss mm/dd/yyyy' }
                  *         ]
                  *     });
                  * </script>
                  */
                format: undefined,
                /** Number of decimal digits after the decimal point.
                  * @alias columns.decimalDigits
                  * @memberof Grid
                  * @type int
                  * @default undefined
                  */
                decimalDigits: undefined,
                /** Template for the content in the column.
                  * Use curly brackets "{}" to wrap the names of data source columns from server response.
                  * @alias columns.tmpl
                  * @memberof Grid
                  * @type string
                  * @default undefined
                  * @example <table id="grid"></table>
                  * <script>
                  *     $("#grid").grid({
                  *         dataSource: "/version_0_4/Demos/GetPlayers",
                  *         columns: [
                  *             { field: "ID" },
                  *             { field: "Name" },
                  *             { title: "Info", tmpl: "{Name} is born in {PlaceOfBirth}." }
                  *         ]
                  *     });
                  * </script>
                  */
                tmpl: undefined,
                /** Provides a way to specify a custom editing UI for the column.
                  * @alias columns.editor
                  * @memberof Grid
                  * @type function|boolean
                  * @default undefined
                  * @example <table id="grid"></table>
                  * <script>
                  *     function edit($container, currentValue) {
                  *         $container.append("<input type=\"text\" value=\"" + currentValue + "\"/>");
                  *     }
                  *     $("#grid").grid({
                  *         dataSource: "/version_0_4/Demos/GetPlayers",
                  *         columns: [
                  *             { field: "ID" },
                  *             { field: "Name", editor: edit },
                  *             { field: "PlaceOfBirth", editor: true }
                  *         ]
                  *     });
                  * </script>
                  */
                editor: undefined
            }
        ],
        mapping: {
            /** The name of the object in the server response, that contains array with records, that needs to be display in the grid.
                * @alias mapping.dataField
                * @memberof Grid
                * @type string
                * @default "records"
                */
            dataField: "records",
            /** The name of the object in the server response, that contains the number of all records on the server.
                * @alias mapping.totalRecordsField
                * @memberof Grid
                * @type string
                * @default "total"
                */
            totalRecordsField: "total"
        },
        params: {},
        defaultParams: {
            /** The name of the parameter that is going to send the name of the column for sorting.
                * The "sortable" setting for at least one column should be enabled in order this parameter to be in use.
                * @alias defaultParams.sortBy
                * @memberof Grid
                * @type string
                * @default "sortBy"
                */
            sortBy: "sortBy",
            /** The name of the parameter that is going to send the direction for sorting.
                * The "sortable" setting for at least one column should be enabled in order this parameter to be in use.
                * @alias defaultParams.direction
                * @memberof Grid
                * @type string
                * @default "direction"
                */
            direction: "direction",
            /** The name of the parameter that is going to send the number of the page.
                * The pager should be enabled in order this parameter to be in use.
                * @alias defaultParams.page
                * @memberof Grid
                * @type string
                * @default "page"
                */
            page: "page",
            /** The name of the parameter that is going to send the maximum number of records per page.
                * The pager should be enabled in order this parameter to be in use.
                * @alias defaultParams.limit
                * @memberof Grid
                * @type string
                * @default "limit"
                */
            limit: "limit"
        },
        /** The name of the UI library that is going to be in use. 
            * Currently we support only jQuery UI and bootstrap. jQuery UI or Bootstrap should be manually included to the page where the grid is in use.
            * @memberof Grid
            * @type (jqueryui|bootstrap)
            * @default "jqueryui"
            * @example <table id="grid"></table>
            * <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css" rel="stylesheet">
            * <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
            * <script>
            *     $("#grid").grid({
            *         dataSource: "/version_0_4/Demos/GetPlayers",
            *         uiLibrary: "bootstrap",
            *         columns: [
            *             { field: "ID" },
            *             { field: "Name" },
            *             { field: "PlaceOfBirth" }
            *         ],
            *         pager: { enable: true, limit: 2, sizes: [2, 5, 10, 20] }
            *     });
            * </script>
            */
        uiLibrary: "jqueryui",
        style: {
            wrapper: "ez-grid-wrapper",
            table: "ez-grid-table",
            loadingCover: "ez-grid-loading-cover",
            loadingText: "ez-grid-loading-text",
            header: {
                cell: "",
                sortable: "gj-grid-thead-sortable",
                sortAscIcon: "gj-grid-ui-thead-th-sort-icon ui-icon ui-icon-arrowthick-1-s",
                sortDescIcon: "gj-grid-ui-thead-th-sort-icon ui-icon ui-icon-arrowthick-1-n"
            },
            content: {
                rowHover: "ui-state-hover",
                rowSelected: "ui-state-active"
            },
            pager: {
                cell: "ez-grid-pager-cell",
                stateDisabled: "ui-state-disabled"
            }
        },

        /** The type of the row selection.<br/>
            * If the type is set to multiple the user will be able to select more then one row from the grid.
            * @memberof Grid
            * @type (single|multiple)
            * @default "single"
            * @example $("table").grid({  });
            * @example <table id="grid"></table>
            * <script>
            *     $("#grid").grid({
            *         dataSource: "/version_0_4/Demos/GetPlayers",
            *         selectionType: "multiple",
            *         selectionMethod: "checkbox",
            *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
            *     });
            * </script>
            */
        selectionType: 'single',

        /** The type of the row selection method.<br/>
            * If this setting is set to "basic" when the user select a row, then this row will be highlighted.<br/>
            * If this setting is set to "checkbox" a column with checkboxes will appear as first row of the grid and when the user select a row, then this row will be highlighted and the checkbox selected.
            * @memberof Grid
            * @type (basic|checkbox)
            * @default "basic"
            * @example <table id="grid"></table>
            * <script>
            *     $("#grid").grid({
            *         dataSource: "/version_0_4/Demos/GetPlayers",
            *         selectionType: "single",
            *         selectionMethod: "checkbox",
            *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
            *     });
            * </script>
            */
        selectionMethod: 'basic',

        /** When this setting is enabled the content of the grid will be loaded automatically after the creation of the grid.
            * @memberof Grid
            * @type boolean
            * @default true
            * @example <table id="grid"></table>
            * <script>
            *     var grid = $("#grid").grid({ 
            *         dataSource: "/version_0_4/Demos/GetPlayers", 
            *         autoLoad: false,
            *         columns: [ { field: "ID" }, { field: "Name" } ]
            *     });
            *     grid.reload(); //call .reload() explicitly in order to load the data in the grid
            * </script>
            * @example <table id="grid"></table>
            * <script>
            *     $("#grid").grid({ 
            *         dataSource: "/version_0_4/Demos/GetPlayers",
            *         autoLoad: true,
            *         columns: [ { field: "ID" }, { field: "Name" } ]
            *     });
            * </script>
            */
        autoLoad: true,

        /** The text that is going to be displayed if the grid is empty.
            * @memberof Grid
            * @type string
            * @default "No records found."
            * @example <table id="grid"></table>
            * <script>
            *     $("#grid").grid({
            *         dataSource: { url: "/version_0_4/Demos/GetPlayers", data: { searchString: "sadasd" } },
            *         notFoundText: "No records found custom message",
            *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
            *     });
            * </script>
            */
        notFoundText: "No se han encontrado registros.",

        /** Width of the grid.
          * @memberof Grid
          * @type int
          * @default undefined
          * @example <table id="grid"></table>
          * <script>
          *     $("#grid").grid({
          *         dataSource: "/version_0_4/Demos/GetPlayers",
          *         width: 400,
          *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
          *     });
          * </script>
          */
        width: undefined,

        /** Minimum width of the grid.
          * @memberof Grid
          * @type int
          * @default undefined
          */
        minWidth: undefined,

        /** The size of the font in the grid.
          * @memberof Grid
          * @type string
          * @default undefined
          * @example <table id="grid"></table>
          * <script>
          *     $("#grid").grid({
          *         dataSource: "/version_0_4/Demos/GetPlayers",
          *         fontSize: "14px",
          *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
          *     });
          * </script>
          */
        fontSize: undefined,

        pager: {
            /** This setting control the visualization of the pager. If this setting is enabled the pager would show.
                * @alias pager.enable
                * @memberof Grid
                * @type boolean
                * @default false
                */
            enable: false,

            /** The maximum number of records that can be show by page.
                * @alias pager.limit
                * @memberof Grid
                * @type int
                * @default 10
                */
            limit: 10,

            /** Array that contains the possible page sizes of the grid.
                * When this setting is set, then a drop down with the options for each page size is visualized in the pager.
                * @alias pager.sizes
                * @memberof Grid
                * @type array
                * @default undefined
                */
            sizes: undefined,

            /** Array that contains a list with jquery objects that are going to be used on the left side of the pager.
                * @alias pager.leftControls
                * @memberof Grid
                * @type array
                * @default array
                */
            leftControls: [
                $('<span title="Primera" data-role="page-first" class="glyphicon glyphicon-fast-backward ez_button" style="vertical-align: middle;top: 0px;">'),
                $('<span title="Anterior" data-role="page-previous" class="glyphicon glyphicon-backward ez_button" style="vertical-align: middle;top: 0px;margin-left: 5px;">'),
                $('<span style="vertical-align: middle;margin-left: 15px;margin-right: 5px;">Página</span>'),
                $('<div></div>').append($('<input type="text" data-role="page-number" class="ui-grid-pager" value="0">')),
                $('<span style="vertical-align: middle;margin-left: 5px;">de&nbsp;</span>'),
                $('<span data-role="page-label-last" style="vertical-align: middle;margin-right: 15px;">0</span>'),
                $('<span title="Siguiente" data-role="page-next" class="glyphicon glyphicon-forward ez_button" style="vertical-align: middle;top: 0px;">'),
                $('<span title="Última" data-role="page-last" class="glyphicon glyphicon-fast-forward ez_button" style="vertical-align: middle;top: 0px;margin-left: 5px;">'),
                $('<span title="Recargar" data-role="page-refresh" class="glyphicon glyphicon-refresh ez_button" style="vertical-align: middle;top: 0px;margin-left: 10px;margin-right: 10px;">'),
                $('<div></div>').append($('<select data-role="page-size"></select>'))
                // $('<div title="Primera" data-role="page-first" class="ui-icon ui-icon-seek-first ui-grid-icon"></div>'),
                // $('<div title="Anterior" data-role="page-previous" class="ui-icon ui-icon-seek-prev ui-grid-icon"></div>'),
                // $('<div>Página</div>'),
                // $('<div></div>').append($('<input type="text" data-role="page-number" class="ui-grid-pager" value="0">')),
                // $('<div>de&nbsp;</div>'),
                // $('<div data-role="page-label-last">0</div>'),
                // $('<div title="Siguiente" data-role="page-next" class="ui-icon ui-icon-seek-next ui-grid-icon"></div>'),
                // $('<div title="Última" data-role="page-last" class="ui-icon ui-icon-seek-end ui-grid-icon"></div>'),
                // $('<div title="Recargar" data-role="page-refresh" class="ui-icon ui-icon-refresh ui-grid-icon"></div>'),
                // $('<div></div>').append($('<select data-role="page-size" class="ui-grid-page-sizer"></select>'))
            ],

            /** Array that contains a list with jquery objects that are going to be used on the right side of the pager.
                * @alias pager.rightControls
                * @memberof Grid
                * @type array
                * @default array
                */
            rightControls: [
                $('<div>Registros&nbsp;</div>'),
                $('<div data-role="record-first">0</div>'),
                $('<div>&nbsp;-&nbsp;</div>'),
                $('<div data-role="record-last">0</div>'),
                $('<div>&nbsp;de&nbsp;</div>'),
                $('<div data-role="record-total">0</div>').css({ "margin-right": "5px" })
            ]
        }
    },

    bootstrap: {
        style: {
            wrapper: "gj-grid-wrapper",
            table: "gj-grid-table table table-bordered table-hover",
            header: {
                cell: "gj-grid-bootstrap-thead-cell",
                sortable: "gj-grid-thead-sortable",
                sortAscIcon: "glyphicon glyphicon-sort-by-alphabet",
                sortDescIcon: "glyphicon glyphicon-sort-by-alphabet-alt"
            },
            content: {
                rowHover: "",
                rowSelected: "active"
            },
            pager: {
                cell: "gj-grid-bootstrap-tfoot-cell",
                stateDisabled: "ui-state-disabled"
            }
        },
        pager: {
            leftControls: [
                $('<button type="button" data-role="page-first" title="First Page" class="btn btn-default btn-sm"><span class="glyphicon glyphicon-step-backward"></span></button>'),
                $('<div>&nbsp;</div>'),
                $('<button type="button" data-role="page-previous" title="Previous Page" class="btn btn-default btn-sm"><span class="glyphicon glyphicon-backward"></span></button>'),
                $('<div>&nbsp;</div>'),
                $('<div>Page</div>'),
                $('<div>&nbsp;</div>'),
                $('<div></div>').append($('<input data-role="page-number" class="form-control input-sm" style="width: 40px; text-align: right;" type="text" value="0">')),
                $('<div>&nbsp;</div>'),
                $('<div>of&nbsp;</div>'),
                $('<div data-role="page-label-last">0</div>'),
                $('<div>&nbsp;</div>'),
                $('<button type="button" data-role="page-next" title="Next Page" class="btn btn-default btn-sm"><span class="glyphicon glyphicon-forward"></span></button>'),
                $('<div>&nbsp;</div>'),
                $('<button type="button" data-role="page-last" title="Last Page" class="btn btn-default btn-sm"><span class="glyphicon glyphicon-step-forward"></span></button>'),
                $('<div>&nbsp;</div>'),
                $('<button type="button" data-role="page-refresh" title="Reload" class="btn btn-default btn-sm"><span class="glyphicon glyphicon-refresh"></span></button>'),
                $('<div>&nbsp;</div>'),
                $('<div></div>').append($('<select data-role="page-size" class="form-control input-sm"></select></div>'))
            ],
            rightControls: [
                $('<div>Displaying records&nbsp;</div>'),
                $('<div data-role="record-first">0</div>'),
                $('<div>&nbsp;-&nbsp;</div>'),
                $('<div data-role="record-last">0</div>'),
                $('<div>&nbsp;of&nbsp;</div>'),
                $('<div data-role="record-total">0</div>').css({ "margin-right": "5px" })
            ]
        }
    }
};
ez.ezGrid.events = {
    beforeEmptyRowInsert: function ($grid, $row) {
        /**
         * Event fires before addition of an empty row to the grid.
         *
         * @event beforeEmptyRowInsert
         * @memberof Grid
         * @property {object} e - event data
         * @property {object} $row - The empty row as jquery object
         * @example <table id="grid"></table>
         * <script>
         *     var grid = $("#grid").grid({
         *         dataSource: { 
         *             url: "/version_0_4/Demos/GetPlayers",
         *             data: { searchString: "not existing data" } //search for not existing data in order to fire the event
         *         },
         *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
         *     });
         *     grid.on("beforeEmptyRowInsert", function (e, $row) {
         *         alert("beforeEmptyRowInsert is fired.");
         *     });
         * </script>
         */
        $grid.trigger("beforeEmptyRowInsert", [$row]);
    },
    dataBinding: function ($grid, records) {
        /**
         * Event fired before data binding takes place.
         *
         * @event dataBinding
         * @memberof Grid
         * @property {object} e - event data
         * @property {array} records - the list of records received from the server
         * @example <table id="grid"></table>
         * <script>
         *     var grid = $("#grid").grid({
         *         dataSource: "/version_0_4/Demos/GetPlayers",
         *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
         *     });
         *     grid.on("dataBinding", function (e, records) {
         *         alert("dataBinding is fired. " + records.length + " records will be loaded in the grid.");
         *     });
         * </script>
         */
        $grid.trigger("dataBinding", [records]);
    },
    dataBound: function ($grid, records) {
        /**
         * Event fires after the loading of the data in the grid.
         *
         * @event dataBound
         * @memberof Grid
         * @property {object} e - event data
         * @property {array} records - the list of records received from the server
         * @example <table id="grid"></table>
         * <script>
         *     var grid = $("#grid").grid({
         *         dataSource: "/version_0_4/Demos/GetPlayers",
         *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
         *     });
         *     grid.on("dataBound", function (e, records) {
         *         alert("dataBound is fired. " + records.length + " records are bound to the grid.");
         *     });
         * </script>
         */
        $grid.trigger("dataBound", [records]);
    },
    rowDataBound: function ($grid, $row, id, record) {
        /**
         * Event fires after insert of a row in the grid during the loading of the data
         *
         * @event rowDataBound
         * @memberof Grid
         * @property {object} e - event data
         * @property {object} $row - the row presented as jquery object
         * @property {object} id - the id of the record
         * @property {object} record - the data of the row record
         * @example <table id="grid"></table>
         * <script>
         *     var grid = $("#grid").grid({
         *         dataSource: "/version_0_4/Demos/GetPlayers",
         *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
         *     });
         *     grid.on("rowDataBound", function (e, $row, id, record) {
         *         alert("rowDataBound is fired for row with id=" + id + ".");
         *     });
         * </script>
         */
        $grid.trigger("rowDataBound", [$row, id, record]);
    },
    cellDataBound: function ($grid, $wrapper, id, index, record) {
        /**
         * Event fires after insert of a cell in the grid during the loading of the data
         *
         * @event cellDataBound
         * @memberof Grid
         * @property {object} e - event data
         * @property {object} $wrapper - the cell wrapper presented as jquery object 
         * @property {string} id - the id of the record
         * @property {int} index - the index number of the column
         * @property {object} record - the data of the row record
         * @example <table id="grid"></table>
         * <script>
         *     var grid = $("#grid").grid({
         *         dataSource: "/version_0_4/Demos/GetPlayers",
         *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" }, { field: "IsBulgarian", title: "Is Bulgarian" } ]
         *     });
         *     grid.on("cellDataBound", function (e, $wrapper, id, index, record) {
         *         if ("IsBulgarian" === index) {
         *             $wrapper.text(record.PlaceOfBirth.indexOf("Bulgaria") > -1 ? "Bulgarian" : "");
         *         }
         *     });
         * </script>
         */
        $grid.trigger("cellDataBound", [$wrapper, id, index, record]);
    },
    rowOpen: function ($grid, $row, id, record) {
        $grid.trigger("rowOpen", [$row, id, record]);
    },
    rowSelect: function ($grid, $row, id, record) {
        /**
         * Event fires on selection of row
         *
         * @event rowSelect
         * @memberof Grid
         * @property {object} e - event data
         * @property {object} $row - the row presented as jquery object 
         * @property {string} id - the id of the record
         * @property {object} record - the data of the row record
         * @example <table id="grid"></table>
         * <script>
         *     var grid = $("#grid").grid({
         *         dataSource: "/version_0_4/Demos/GetPlayers",
         *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ],
         *         selectionMethod: "checkbox"
         *     });
         *     grid.on("rowSelect", function (e, $row, id, record) {
         *         alert('Row with id=' + id + ' is selected.');
         *     });
         * </script>
         */
        $grid.trigger("rowSelect", [$row, id, record]);
    },
    rowUnselect: function ($grid, $row, id, record) {

        /**
         * Event fires on un selection of row
         *
         * @event rowUnselect
         * @memberof Grid
         * @property {object} e - event data
         * @property {object} $row - the row presented as jquery object 
         * @property {string} id - the id of the record
         * @property {object} record - the data of the row record
         * @example <table id="grid"></table>
         * <script>
         *     var grid = $("#grid").grid({
         *         dataSource: "/version_0_4/Demos/GetPlayers",
         *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ],
         *         selectionMethod: "checkbox"
         *     });
         *     grid.on("rowUnselect", function (e, $row, id, record) {
         *         alert('Row with id=' + id + ' is unselected.');
         *     });
         * </script>
         */
        $grid.trigger("rowUnselect", [$row, id, record]);

    },
    pageSizeChange: function ($grid, newSize) {

        /**
         * Event fires on change of the page size
         *
         * @event pageSizeChange
         * @memberof Grid
         * @property {object} e - event data
         * @property {int} newSize - The new page size
         * @example <table id="grid"></table>
         * <script>
         *     var grid = $("#grid").grid({
         *         dataSource: "/version_0_4/Demos/GetPlayers",
         *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ],
         *         pager: { enable: true, limit: 2, sizes: [2, 5, 10, 20] }
         *     });
         *     grid.on("pageSizeChange", function (e, newSize) {
         *         alert('The new page size is ' + newSize + '.');
         *     });
         * </script>
         */
        $grid.trigger("pageSizeChange", [newSize]);

    }
};
ez.ezGrid.private = {

    init: function (jsConfiguration) {
        var options;
        if (!this.data('grid')) {
            options = ez.ezGrid.private.SetOptions(this, jsConfiguration || {});
            ez.ezGrid.private.InitGrid(this);
            ez.ezGrid.private.HeaderRenderer(this);
            ez.ezGrid.private.AppendEmptyRow(this, "&nbsp;");
            ez.ezGrid.private.CreatePager(this);
            if (options.autoLoad) {
                this.reload();
            }
        }
        return this;
    },

    SetOptions: function ($grid, jsConfiguration) {
        var options, htmlConfiguration;
        options = $.extend(true, {}, ez.ezGrid.configuration.base);
        if (jsConfiguration.uiLibrary && jsConfiguration.uiLibrary === "bootstrap") {
            $.extend(true, options, ez.ezGrid.configuration.bootstrap);
        }
        htmlConfiguration = ez.ezGrid.private.GetHTMLConfiguration($grid);
        $.extend(true, options, htmlConfiguration);
        $.extend(true, options, jsConfiguration);
        $grid.data('grid', options);
        return options;
    },

    GetHTMLConfiguration: function ($grid) {
        var result = ez.ezGrid.private.GetAttributes($grid);
        result.columns = [];
        $grid.find("thead>tr>th").each(function () {
            var $el = $(this),
                config = ez.ezGrid.private.GetAttributes($el),
                title = $el.text();
            if (title) {
                config.title = title;
                if (!config.field) {
                    config.field = title;
                }
            }
            result.columns.push(config);
        });
        return result;
    },

    GetAttributes: function ($el) {
        var result;
        result = $el.data();
        if ($el.attr('width')) {
            result['width'] = $el.attr('width');
        }
        if (result.source) {
            result["dataSource"] = result.source;
        }
        return result;
    },

    LoaderSuccessHandler: function ($grid) {
        return function (response) {
            $grid.render(response);
        };
    },

    InitGrid: function ($grid) {
        var data = $grid.data('grid');
        if (!$grid.parent().hasClass(data.style.wrapper)) {
            $grid.wrapAll('<div class="' + data.style.wrapper + '" />');
        }
        if (data.width) {
            $grid.parent().css("width", data.width);
        }
        if (data.minWidth) {
            $grid.css("min-width", data.minWidth);
        }
        if (data.fontSize) {
            $grid.css("font-size", data.fontSize);
        }
        $grid.addClass(data.style.table);
        if ("checkbox" === data.selectionMethod) {
            data.columns = [{ title: '', field: data.dataKey, width: (data.uiLibrary === 'jqueryui' ? 24 : 30), align: 'center', type: 'checkbox' }].concat(data.columns);
        }
        if (data.pager.enable) {
            data.params[data.defaultParams.page] = 1;
            data.params[data.defaultParams.limit] = data.pager.limit;
            $grid.data('grid', data);
        }
        $grid.append($("<tbody/>"));
    },

    HeaderRenderer: function ($grid) {
        var data, $row, $cell, columns, style, i, sortBy, direction;

        data = $grid.data("grid");
        columns = data.columns;
        style = data.style.header;
        sortBy = data.params[data.defaultParams.sortBy];
        direction = data.params[data.defaultParams.direction];

        if ($grid.children("thead").length === 0) {
            $grid.prepend($("<thead />"));
        }

        $row = $("<tr/>");
        for (i = 0; i < columns.length; i += 1) {
            $cell = $("<th/>");
            if (columns[i].width) {
                $cell.css("width", columns[i].width);
            }
            $cell.addClass(style.cell);
            $cell.css("text-align", columns[i].align || "left");
            if (columns[i].sortable) {
                $cell.addClass(style.sortable);
                $cell.bind("click", ez.ezGrid.private.CreateSortHandler($grid, $cell));
            }
            if ("checkbox" === data.selectionMethod && "multiple" === data.selectionType && "checkbox" === columns[i].type) {
                $cell.append($("<input type='checkbox' id='checkAllBoxes' />").hide().click(function () {
                    if (this.checked) {
                        $grid.selectAll();
                    } else {
                        $grid.unSelectAll();
                    }
                }));
            } else {
                $cell.append($("<div style='float: left'/>").text(typeof (columns[i].title) === "undefined" ? columns[i].field : columns[i].title));
                if (sortBy && direction && columns[i].field === sortBy) {
                    $cell.append($("<span style='float: left; margin-left:5px;'/>").addClass("desc" === direction ? style.sortDescIcon : style.sortAscIcon));
                }
            }
            if (columns[i].hidden) {
                $cell.hide();
            }

            $cell.data("cell", columns[i]);
            $row.append($cell);
        }

        $grid.children("thead").empty().append($row);
    },

    CreateSortHandler: function ($grid, $cell) {
        return function () {
            var $icon, data, cellData, params = {};
            if ($grid.count() > 1) {
                data = $grid.data('grid');
                cellData = $cell.data("cell");
                cellData.direction = (cellData.direction === "asc" ? "desc" : "asc");
                params[data.defaultParams.sortBy] = cellData.field;
                params[data.defaultParams.direction] = cellData.direction;
                $grid.reload(params);
            }
        };
    },

    StartLoading: function ($grid) {
        var $tbody, $cover, $loading, width, height, top, data;
        ez.ezGrid.private.StopLoading($grid);
        data = $grid.data('grid');
        if (0 === $grid.outerHeight()) {
            return;
        }
        $tbody = $grid.children("tbody");
        width = $tbody.outerWidth(false);
        height = $tbody.outerHeight(false);
        top = $tbody.prev().outerHeight(false) + parseInt($grid.parent().css("padding-top").replace('px', ''), 10);
        $cover = $("<div data-role='loading-cover' />").addClass(data.style.loadingCover).css({
            width: width,
            height: height,
            top: top
        });
        $loading = $("<div data-role='loading-text'>Cargando...</div>").addClass(data.style.loadingText);
        $loading.insertAfter($grid);
        $cover.insertAfter($grid);
        $loading.css({
            top: top + (height / 2) - ($loading.outerHeight(false) / 2),
            left: (width / 2) - ($loading.outerWidth(false) / 2)
        });
    },

    StopLoading: function ($grid) {
        $grid.parent().find("div[data-role='loading-cover']").remove();
        $grid.parent().find("div[data-role='loading-text']").remove();
    },

    CreateAddRowHoverHandler: function ($row, cssClass) {
        return function () {
            $row.addClass(cssClass);
        };
    },

    CreateRemoveRowHoverHandler: function ($row, cssClass) {
        return function () {
            $row.removeClass(cssClass);
        };
    },

    AppendEmptyRow: function ($grid, caption) {
        var data, $row, $cell, $wrapper;
        data = $grid.data('grid');
        $row = $("<tr data-role='emptyRow'/>");
        $cell = $("<td/>").css({ "width": "100%", "text-align": "center" });
        $cell.attr("colspan", $grid.find("thead > tr > th").length);
        $wrapper = $("<div />").html(caption);
        $cell.append($wrapper);
        $row.append($cell);

        ez.ezGrid.events.beforeEmptyRowInsert($grid, $row);

        $grid.append($row);
    },

    LoadData: function ($grid, data, records) {
        var data, records, i, j, recLen, rowCount,
            $tbody, $rows, $row, $checkAllBoxes;

        ez.ezGrid.events.dataBinding($grid, records);
        recLen = records.length;
        ez.ezGrid.private.StopLoading($grid);
        $tbody = $grid.find("tbody");
        if ("checkbox" === data.selectionMethod && "multiple" === data.selectionType) {
            $checkAllBoxes = $grid.find("input#checkAllBoxes");
            $checkAllBoxes.prop("checked", false);
            if (0 === recLen) {
                $checkAllBoxes.hide();
            } else {
                $checkAllBoxes.show();
            }
        }
        $tbody.find("tr[data-role='emptyRow']").remove(); //Remove empty row
        if (0 === recLen) {
            $tbody.empty();
            ez.ezGrid.private.AppendEmptyRow($grid, data.notFoundText);
        }

        $rows = $tbody.children("tr");
        rowCount = $rows.length;

        for (i = 0; i < rowCount; i++) {
            if (i < recLen) {
                $row = $rows.eq(i);
                ez.ezGrid.private.RowRenderer($grid, $row, records[i], i);
            } else {
                $tbody.find("tr:gt(" + (i - 1) + ")").remove();
                break;
            }
        }

        for (i = rowCount; i < recLen; i++) {
            ez.ezGrid.private.RowRenderer($grid, null, records[i], i);
        }
        ez.ezGrid.events.dataBound($grid, records);
    },

    RowRenderer: function ($grid, $row, record, position) {
        var id, $cell, i, data, mode;
        data = $grid.data('grid');
        if (!$row || $row.length === 0) {
            mode = "create";
            $row = $($grid.find("tbody")[0].insertRow(position));
            $row.bind({
                "mouseenter.grid": ez.ezGrid.private.CreateAddRowHoverHandler($row, data.style.content.rowHover),
                "mouseleave.grid": ez.ezGrid.private.CreateRemoveRowHoverHandler($row, data.style.content.rowHover)
            });
        } else {
            mode = "update";
            $row.removeClass(data.style.content.rowSelected).off("click");
        }
        id = (data.dataKey && record[data.dataKey]) ? record[data.dataKey] : (position + 1);
        $row.data("row", { id: id, record: record });
        $row.on("click", ez.ezGrid.private.CreateRowClickHandler($grid, id, record));
        $row.on("dblclick", function (e) {
            ez.ezGrid.events.rowOpen($grid, $row, $row.data("row").id, $row.data("row").record);
        });
        for (i = 0; i < data.columns.length; i++) {
            if (mode === "update") {
                $cell = $row.find("td:eq(" + i + ")");
                ez.ezGrid.private.CellRenderer($grid, $cell, data.columns[i], record, id);
            } else {
                $cell = ez.ezGrid.private.CellRenderer($grid, null, data.columns[i], record, id);
                $row.append($cell);
            }
        }
        ez.ezGrid.events.rowDataBound($grid, $row, id, record);
    },

    CellRenderer: function ($grid, $cell, column, record, id, mode) {
        var text, $wrapper, mode;

        if (!$cell || $cell.length === 0) {
            $cell = $("<td/>").css("text-align", column.align || "left");
            $wrapper = $("<div data-role='display' />");
            if (column.cssClass) {
                $wrapper.addClass(column.cssClass);
            }
            $cell.append($wrapper);
            mode = "create";
        } else {
            $wrapper = $cell.find("div");
            mode = "update";
        }

        if ("checkbox" === column.type) {
            if ("create" === mode) {
                $wrapper.append($("<input />").attr("type", "checkbox").val(id));
            } else {
                $wrapper.find("input[type='checkbox']").val(id).prop("checked", false);
            }
        } else if ("icon" === column.type) {
            if ("create" === mode) {
                $wrapper.append($("<span/>")
                    .addClass($grid.data('grid').uiLibrary === "bootstrap" ? "glyphicon" : "ui-icon")
                    .addClass(column.icon).css({ "cursor": "pointer" }));
            }
        } else if (column.tmpl) {
            text = column.tmpl;
            column.tmpl.replace(/\{(.+?)\}/g, function ($0, $1) {
                text = text.replace($0, record[$1]);
            });
            $wrapper.text(text);
        } else {
            ez.ezGrid.private.SetCellText($wrapper, column, record[column.field]);
        }
        if (column.tooltip && "create" === mode) {
            $wrapper.attr("title", column.tooltip);
        }
        //remove all event handlers
        if ("update" === mode) {
            $cell.off();
            $wrapper.off();
        }
        if (column.events) {
            for (var key in column.events) {
                if (column.events.hasOwnProperty(key)) {
                    $cell.on(key, { id: id, field: column.field, record: record }, column.events[key]);
                }
            }
        }
        if (column.editor) {
            $cell.on("click", function () {
                ez.ezGrid.private.OnCellEdit($cell, column, record);
            });
        }
        if (column.hidden) {
            $cell.hide();
        }

        ez.ezGrid.events.cellDataBound($grid, $wrapper, id, column.field, record);

        return $cell;
    },

    OnCellEdit: function ($cell, column, record) {
        var $editorContainer, $editorField;
        if ($cell.attr("data-mode") !== "edit" && column.editor) {
            $cell.find("div[data-role='display']").hide();
            $editorContainer = $cell.find("div[data-role='edit']");
            if ($editorContainer && $editorContainer.length) {
                $editorContainer.show();
                $editorField = $editorContainer.find("input, select, textarea").first();
            } else {
                $editorContainer = $("<div data-role='edit' />");
                $cell.append($editorContainer);
                if (typeof (column.editor) === "function") {
                    column.editor($editorContainer, record[column.field]);
                } else if (typeof (column.editor) === "boolean") {
                    $editorContainer.append("<input type=\"text\" value=\"" + record[column.field] + "\"/>");
                }
                $editorField = $editorContainer.find("input, select, textarea").first();
                $editorField.on("blur", function () { ez.ezGrid.private.OnCellDisplay($cell, column); });
                $editorField.on("keypress", function (e) {
                    if (e.which === 13) {
                        ez.ezGrid.private.OnCellDisplay($cell, column);
                    }
                });
            }
            $editorField.focus().select();
            $cell.attr("data-mode", "edit");
        }
    },

    OnCellDisplay: function ($cell, column) {
        var value, style = "";
        if ($cell.attr("data-mode") === "edit") {
            $editorContainer = $cell.find("div[data-role='edit']");
            $displayContainer = $cell.find("div[data-role='display']");
            value = $editorContainer.find("input, select, textarea").first().val();
            ez.ezGrid.private.SetCellText($displayContainer, column, value);
            $cell.parent().data("row").record[column.field] = value;
            $editorContainer.hide();
            $displayContainer.show();
            if ($cell.find("span.gj-dirty").length === 0) {
                if ($cell.css("padding-top") !== "0px") {
                    style += "margin-top: -" + $cell.css("padding-top") + ";";
                }
                if ($cell.css("padding-left") !== "0px") {
                    style += "margin-left: -" + $cell.css("padding-left") + ";";
                }
                style = style ? " style=\"" + style + "\"" : "";
                $cell.prepend($("<span class=\"gj-dirty\"" + style + "></span>"));
            }
            $cell.attr("data-mode", "display");
        }
    },

    SetCellText: function ($wrapper, column, value) {
        var text = ez.ezGrid.private.FormatText(value, column.type, column.format);
        if (column.decimalDigits && text) {
            text = parseFloat(text).toFixed(column.decimalDigits);
        }
        if (!column.tooltip) {
            $wrapper.attr("title", text);
        }
        $wrapper.text(text);
    },

    FormatText: function (text, type, format) {
        var dt, day, month;
        if (text && type) {
            switch (type) {
                case "date":
                    if (text.indexOf("/Date(") > -1) {
                        dt = new Date(parseInt(text.substr(6), 10));
                    } else {
                        var parts = text.match(/(\d+)/g);
                        // new Date(year, month, date, hours, minutes, seconds);
                        dt = new Date(parts[0], parts[1] - 1, parts[2], parts[3], parts[4], parts[5]); // months are 0-based
                    }

                    if (dt.format && format) {
                        text = dt.format(format); //using 3rd party plugin "Date Format 1.2.3 by (c) 2007-2009 Steven Levithan <stevenlevithan.com>"
                    } else {
                        day = dt.getDate().toString().length === 2 ? dt.getDate() : "0" + dt.getDate();
                        month = (dt.getMonth() + 1).toString();
                        month = month.length === 2 ? month : "0" + month;
                        text = month + "/" + day + "/" + dt.getFullYear();
                    }
                    break;
                case "money":
                    text = parseFloat(text).toFixed(2);
                    break;
            }
        } else {
            text = (typeof (text) === "undefined" || text === null) ? "" : text.toString();
        }
        return text;
    },

    GetRecords: function (data, response) {
        var records = [];
        if ($.isArray(response)) {
            records = response;
        } else if (data && data.mapping && $.isArray(response[data.mapping.dataField])) {
            records = response[data.mapping.dataField];
        }
        return records;
    },

    CreateRowClickHandler: function ($grid, id, record) {
        return function (e) {
            ez.ezGrid.private._SetSelected($grid, $(this), id);
        };
    },

    SelectRow: function ($grid, data, $row) {
        $row.addClass(data.style.content.rowSelected);

        ez.ezGrid.events.rowSelect($grid, $row, $row.data("row").id, $row.data("row").record);

        if ("checkbox" === data.selectionMethod) {
            $row.find("td:nth-child(1) input[type='checkbox']").prop("checked", true);
        }

        $row.siblings().find("td[data-mode='edit']").each(function () {
            var $cell = $(this),
                column = data.columns[$cell.parent().children().index(this)];
            ez.ezGrid.private.OnCellDisplay($cell, column);
        });
    },

    UnselectRow: function ($grid, data, $row) {
        if ($row.hasClass(data.style.content.rowSelected)) {
            $row.removeClass(data.style.content.rowSelected);

            ez.ezGrid.events.rowUnselect($grid, $row, $row.data("row").id, $row.data("row").record)

            if ("checkbox" === data.selectionMethod) {
                $row.find("td:nth-child(1) input[type='checkbox']").prop("checked", false);
            }
        }
    },

    _SetSelected: function ($grid, $row, id) {
        var data = $grid.data('grid');
        if ($row.hasClass(data.style.content.rowSelected)) {
            ez.ezGrid.private.UnselectRow($grid, data, $row);
        } else {
            if ("single" === data.selectionType) {
                $row.siblings().each(function () {
                    ez.ezGrid.private.UnselectRow($grid, data, $(this));
                });
            }
            ez.ezGrid.private.SelectRow($grid, data, $row);
        }
    },

    _GetSelected: function ($grid) {
        var result, data, selections;
        data = $grid.data("grid");
        selections = $grid.find("tbody > tr." + data.style.content.rowSelected);
        if (selections.length > 0) {
            result = $(selections[0]).data("row").id;
        }
        return result;
    },

    GetSelectedRows: function ($grid) {
        var data = $grid.data("grid");
        return $grid.find("tbody > tr." + data.style.content.rowSelected);
    },

    _GetSelections: function ($grid) {
        var result = [],
            $selections = ez.ezGrid.private.GetSelectedRows($grid);
        if (0 < $selections.length) {
            $selections.each(function () {
                result.push($(this).data("row").id);
            });
        }
        return result;
    },

    getRecordById: function ($grid, id) {
        var result = {}, rows, i, rowData;
        rows = $grid.find("tbody > tr");
        for (i = 0; i < rows.length; i++) {
            rowData = $(rows[i]).data("row");
            if (rowData.id === id) {
                result = rowData.record;
                break;
            }
        }
        return result;
    },

    getRowById: function ($grid, id) {
        var result = null, rows, i, rowData;
        rows = $grid.find("tbody > tr");
        for (i = 0; i < rows.length; i++) {
            rowData = $(rows[i]).data("row");
            if (rowData.id === id) {
                result = $(rows[i]);
                break;
            }
        }
        return result;
    },

    getByPosition: function ($grid, position) {
        var result = {}, $rows, data;
        $rows = $grid.find("tbody > tr");
        if ($rows.length >= position) {
            data = $rows.eq(position - 1).data("row");
            if (data && data.record) {
                result = data.record;
            }
        }
        return result;
    },

    GetColumnPosition: function (columns, field) {
        var position = -1, i;
        for (i = 0; i < columns.length; i++) {
            if (columns[i].field === field) {
                position = i;
                break;
            }
        }
        return position;
    },

    GetColumnInfo: function ($grid, index) {
        var i, result = {}, data = $grid.data("grid");
        for (i = 0; i < data.columns.length; i += 1) {
            if (data.columns[i].field === index) {
                result = data.columns[i];
                break;
            }
        }
        return result;
    },

    GetCell: function ($grid, id, index) {
        var result = {}, rows, i, rowData, position;
        position = ez.ezGrid.private.GetColumnPosition($grid, index);
        rows = $grid.find("tbody > tr");
        for (i = 0; i < rows.length; i += 1) {
            rowData = $(rows[i]).data("row");
            if (rowData.id === id) {
                result = $(rows[i].cells[position]).find("div");
                break;
            }
        }
        return result;
    },

    SetCellContent: function ($grid, id, index, value) {
        var column, $cellWrapper = ez.ezGrid.private.GetCell($grid, id, index);
        $cellWrapper.empty();
        if (typeof (value) === "object") {
            $cellWrapper.append(value);
        } else {
            column = ez.ezGrid.private.GetColumnInfo($grid, index);
            ez.ezGrid.private.SetCellText($cellWrapper, column, value);
        }
    },

    CreatePager: function ($grid) {
        var $row, $cell, data, controls, $leftPanel, $rightPanel, $tfoot, leftControls, rightControls;

        data = $grid.data('grid');

        if (data.pager && data.pager.enable) {
            $row = $("<tr/>");
            $cell = $("<th/>").addClass(data.style.pager.cell);
            $row.append($cell);

            $leftPanel = $("<div />").css({ "float": "left" });
            $rightPanel = $("<div />").css({ "float": "right" });
            if (/msie/.test(navigator.userAgent.toLowerCase())) {
                $rightPanel.css({ "padding-top": "3px" });
            }

            $cell.append($leftPanel).append($rightPanel);

            $tfoot = $("<tfoot />").append($row);
            $grid.append($tfoot);
            ez.ezGrid.private.updatePagerColSpan($grid);

            leftControls = ez.ezGrid.private.clone(data.pager.leftControls); //clone array
            $.each(leftControls, function () {
                $leftPanel.append(this);
            });

            rightControls = ez.ezGrid.private.clone(data.pager.rightControls); //clone array
            $.each(rightControls, function () {
                $rightPanel.append(this);
            });

            controls = $grid.find("TFOOT [data-role]");
            for (i = 0; i < controls.length; i++) {
                ez.ezGrid.private.InitPagerControl($(controls[i]), $grid);
            }
        }
    },

    updatePagerColSpan: function ($grid) {
        var i, columns, count, $cell = $grid.find('tfoot > tr > th');
        if ($cell && $cell.length) {
            columns = $grid.data('grid').columns;
            count = 0;
            for (i = 0; i < columns.length; i++) {
                if (columns[i].hidden !== true) {
                    count++;
                }
            }
            $cell.attr('colspan', count);
        }
    },

    clone: function (source) {
        var target = [];
        $.each(source, function () {
            target.push(this.clone());
        });
        return target;
    },

    InitPagerControl: function ($control, $grid) {
        var data = $grid.data('grid')
        switch ($control.data("role")) {
            case "page-number":
                $control.bind("keypress", function (e) {
                    if (e.keyCode === 13) {
                        $(this).trigger("change");
                    }
                });
                break;
            case "page-size":
                if (data.pager.sizes && 0 < data.pager.sizes.length) {
                    $control.show();
                    $.each(data.pager.sizes, function () {
                        $control.append($("<option/>").attr("value", this.toString()).text(this.toString()));
                    });
                    $control.change(function () {
                        var newSize = parseInt(this.value, 10);
                        data.params[data.defaultParams.page] = 1;
                        ez.ezGrid.private.SetPageNumber($grid, 1);
                        data.params[data.defaultParams.limit] = newSize;
                        $grid.reload();
                        ez.ezGrid.events.pageSizeChange($grid, newSize);
                    });
                    $control.val(data.params[data.defaultParams.limit]);
                } else {
                    $control.hide();
                }
                break;
            case "page-refresh":
                $control.bind("click", function () { $grid.reload(); });
                break;
        }

    },

    ReloadPager: function ($grid, data, response) {
        var totalRecords, page, limit, lastPage, firstRecord, lastRecord;

        totalRecords = response[data.mapping.totalRecordsField];
        if (!totalRecords || isNaN(totalRecords)) {
            totalRecords = 0;
        }
        if (data.pager.enable) {
            page = (0 === totalRecords) ? 0 : data.params[data.defaultParams.page];
            limit = parseInt(data.params[data.defaultParams.limit], 10);
            lastPage = Math.ceil(totalRecords / limit);
            firstRecord = (0 === page) ? 0 : (limit * (page - 1)) + 1;
            lastRecord = (firstRecord + limit) > totalRecords ? totalRecords : (firstRecord + limit) - 1;

            controls = $grid.find("TFOOT [data-role]");
            for (i = 0; i < controls.length; i++) {
                ez.ezGrid.private.ReloadPagerControl($(controls[i]), $grid, page, lastPage, firstRecord, lastRecord, totalRecords);
            }

            ez.ezGrid.private.updatePagerColSpan($grid);
        }
    },

    ReloadPagerControl: function ($control, $grid, page, lastPage, firstRecord, lastRecord, totalRecords) {
        var data = $grid.data('grid')
        switch ($control.data("role")) {
            case "page-first":
                if (page < 2) {
                    $control.addClass(data.style.pager.stateDisabled).unbind("click");
                } else {
                    $control.removeClass(data.style.pager.stateDisabled).unbind("click").bind("click", ez.ezGrid.private.CreateFirstPageHandler($grid));
                }
                break;
            case "page-previous":
                if (page < 2) {
                    $control.addClass(data.style.pager.stateDisabled).unbind("click");
                } else {
                    $control.removeClass(data.style.pager.stateDisabled).unbind("click").bind("click", ez.ezGrid.private.CreatePrevPageHandler($grid));
                }
                break;
            case "page-number":
                $control.val(page).unbind("change").bind("change", ez.ezGrid.private.CreateChangePageHandler($grid, page, lastPage));
                break;
            case "page-label-last":
                $control.text(lastPage);
                break;
            case "page-next":
                if (lastPage === page) {
                    $control.addClass(data.style.pager.stateDisabled).unbind("click");
                } else {
                    $control.removeClass(data.style.pager.stateDisabled).unbind("click").bind("click", ez.ezGrid.private.CreateNextPageHandler($grid));
                }
                break;
            case "page-last":
                if (lastPage === page) {
                    $control.addClass(data.style.pager.stateDisabled).unbind("click");
                } else {
                    $control.removeClass(data.style.pager.stateDisabled).unbind("click").bind("click", ez.ezGrid.private.CreateLastPageHandler($grid, lastPage));
                }
                break;
            case "record-first":
                $control.text(firstRecord);
                break;
            case "record-last":
                $control.text(lastRecord);
                break;
            case "record-total":
                $control.text(totalRecords);
                break;
        }
    },

    CreateFirstPageHandler: function ($grid) {
        return function () {
            var data = $grid.data('grid');
            data.params[data.defaultParams.page] = 1;
            ez.ezGrid.private.SetPageNumber($grid, 1);
            $grid.reload();
        };
    },

    CreatePrevPageHandler: function ($grid) {
        return function () {
            var data, currentPage, lastPage, newPage;
            data = $grid.data('grid');
            currentPage = data.params[data.defaultParams.page];
            newPage = (currentPage && currentPage > 1) ? currentPage - 1 : 1;
            data.params[data.defaultParams.page] = newPage;
            ez.ezGrid.private.SetPageNumber($grid, newPage);
            $grid.reload();
        };
    },

    CreateNextPageHandler: function ($grid) {
        return function () {
            var data, currentPage;
            data = $grid.data('grid');
            currentPage = data.params[data.defaultParams.page];
            data.params[data.defaultParams.page] = currentPage + 1;
            ez.ezGrid.private.SetPageNumber($grid, currentPage + 1);
            $grid.reload();
        };
    },

    CreateLastPageHandler: function ($grid, lastPage) {
        return function () {
            var data = $grid.data('grid');
            data.params[data.defaultParams.page] = lastPage;
            ez.ezGrid.private.SetPageNumber($grid, lastPage);
            $grid.reload();
        };
    },

    CreateChangePageHandler: function ($grid, currentPage, lastPage) {
        return function (e) {
            var data = $grid.data('grid'),
                newPage = parseInt(this.value, 10);
            if (newPage && !isNaN(newPage) && newPage <= lastPage) {
                data.params[data.defaultParams.page] = newPage;
                $grid.reload();
            } else {
                this.value = currentPage;
                alert("Enter a valid page.");
            }
        };
    },

    SetPageNumber: function ($grid, value) {
        $grid.find("TFOOT [data-role='page-number']").val(value);
    },

    GetAll: function ($grid) {
        var result = [],
            rows = $grid.find("tbody > tr"),
            i, record;

        for (i = 0; i < rows.length; i++) {
            record = $(rows[i]).data("row");
            if (record) {
                result.push(record);
            }
        }
        return result;
    }
};
/** @class Grid */
ez.ezGrid.public = {
    xhr: null,

    /**
     * Reload the data in the grid from a data source.
     * @method
     * @memberof Grid
     * @param {object} params - An object that contains a list with parameters that are going to be send to the server.
     * @fires Grid#beforeEmptyRowInsert, Grid#dataBinding, Grid#dataBound, Grid#cellDataBound
     * @return void
     * @example <input type="text" id="txtSearch">
     * <button id="btnSearch">Search</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
     *     });
     *     $("#btnSearch").on("click", function () {
     *         grid.reload({ searchString: $("#txtSearch").val() });
     *     });
     * </script>
     */
    reload: function (params) {
        var data, ajaxOptions, records;
        data = this.data('grid');
        $.extend(data.params, params);
        ez.ezGrid.private.StartLoading(this);
        ez.ezGrid.private.HeaderRenderer(this);
        if ($.isArray(data.dataSource)) {
            records = ez.ezGrid.private.GetRecords(data, data.dataSource);
            ez.ezGrid.private.LoadData(this, data, records);
        } else if (typeof (data.dataSource) === "string") {
            ajaxOptions = { url: data.dataSource, data: data.params, success: ez.ezGrid.private.LoaderSuccessHandler(this) };
            if (this.xhr) {
                this.xhr.abort();
            }
            this.xhr = $.ajax(ajaxOptions);
        } else if (typeof (data.dataSource) === "object") {
            if (!data.dataSource.data) {
                data.dataSource.data = {};
            }
            $.extend(data.dataSource.data, data.params);
            ajaxOptions = $.extend(true, {}, data.dataSource); //clone dataSource object
            if (ajaxOptions.dataType === "json" && typeof (ajaxOptions.data) === "object") {
                ajaxOptions.data = JSON.stringify(ajaxOptions.data);
            }
            if (!ajaxOptions.success) {
                ajaxOptions.success = ez.ezGrid.private.LoaderSuccessHandler(this);
            }
            if (this.xhr) {
                this.xhr.abort();
            }
            this.xhr = $.ajax(ajaxOptions);
        }
        return this;
    },

    /**
     * Clear the content in the grid.
     * @method
     * @memberof Grid
     * @return void
     * @example <button id="btnClear">Clear</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
     *     });
     *     $("#btnClear").on("click", function () {
     *         grid.clear();
     *     });
     * </script>
     */
    clear: function () {
        var emptyResponse = {}, data = this.data('grid');
        if ("checkbox" === data.selectionMethod) {
            this.find("input#checkAllBoxes").hide();
        }
        emptyResponse[data.mapping.totalRecordsField] = 0;
        this.children("tbody").empty();
        ez.ezGrid.private.StopLoading(this);
        ez.ezGrid.private.AppendEmptyRow(this, "&nbsp;");
        ez.ezGrid.private.ReloadPager(this, data, emptyResponse);
        return this;
    },

    /**
     * Return the number of records presented on the screen.
     * @method
     * @memberof Grid
     * @return int
     * @example <button id="btnShowCount">Show Count</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
     *     });
     *     $("#btnShowCount").on("click", function () {
     *         alert(grid.count());
     *     });
     * </script>
     */
    count: function () {
        return $(this).find("tbody tr").length;
    },

    /**
     * Render data in the grid
     * @method
     * @memberof Grid
     * @param {object} response - An object that contains the data that needs to be loaded in the grid.
     * @fires Grid#beforeEmptyRowInsert, Grid#dataBinding, Grid#dataBound, Grid#cellDataBound
     * @return void
     * @example <table id="grid"></table>
     * <script>
     *     var grid, onSuccessFunc; 
     *     onSuccessFunc = function (response) { 
     *         grid.render(response);
     *     };
     *     grid = $("#grid").grid({
     *         dataSource: { url: "/version_0_4/Demos/GetPlayers", success: onSuccessFunc },
     *         columns: [ { field: "Name" }, { field: "PlaceOfBirth" } ]
     *     });
     * </script>
     */
    render: function (response) {
        var data, records;
        if (!response) {
            return;
        }
        data = this.data('grid');
        if (data) {
            records = ez.ezGrid.private.GetRecords(data, response);
            ez.ezGrid.private.LoadData(this, data, records);
            ez.ezGrid.private.ReloadPager(this, data, response);
        }
    },

    /**
     * Remove the grid from the HTML dom tree.
     * @method
     * @memberof Grid
     * @return void
     * @example <button id="btnRemove">Remove</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
     *     });
     *     $("#btnRemove").on("click", function () {
     *         grid.remove();
     *     });
     * </script>
     */
    remove: function () {
        var data = this.data('grid');
        $(window).unbind('.grid');
        if (this.parent().hasClass(data.style.wrapper)) {
            this.unwrap();
        }
        this.removeData();
        this.remove();
    },


    /**
     * Destroy the grid from the HTML dom tree and leave just the "table" tag in the HTML dom tree.
     * @method
     * @memberof Grid
     * @return void
     * @example <button id="btnDestroy">Destroy</button>
     * <button id="btnCreate">Create</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid, createFunc;
     *     createFunc = function() {
     *         grid = $("#grid").grid({
     *             dataSource: "/version_0_4/Demos/GetPlayers",
     *             columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
     *         });
     *     };
     *     createFunc();
     *     $("#btnDestroy").on("click", function () {
     *         grid.destroy();
     *     });
     *     $("#btnCreate").on("click", function () {
     *         createFunc();
     *     });
     * </script>
     */
    destroy: function () {
        $(window).unbind('.grid');
        this.removeClass().empty();
        if (this.parent().hasClass("ui-grid-wrapper")) {
            this.unwrap();
        }
        this.removeData();
        this.off();
    },

    /**
     * Select a row from the grid based on id parameter.
     * @method
     * @memberof Grid
     * @param {string} id - The id of the row that needs to be selected
     * @return void
     * @example <input type="text" id="txtNumber" value="1" />
     * <button id="btnSelect">Select</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ],
     *         selectionMethod: "checkbox"
     *     });
     *     $("#btnSelect").on("click", function () {
     *         grid.setSelected(parseInt($("#txtNumber").val(), 10));
     *     });
     * </script>
     */
    setSelected: function (id) {
        var $row = ez.ezGrid.private.getRowById(this, id);
        if ($row) {
            ez.ezGrid.private._SetSelected(this, $row, id);
        }
    },

    /**
     * Return the id of the selected record.
     * If the multiple selection method is one this method is going to return only the id of the first selected record.
     * @method
     * @memberof Grid
     * @return string
     * @example <button id="btnShowSelection">Show Selection</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ],
     *         selectionMethod: "checkbox"
     *     });
     *     $("#btnShowSelection").on("click", function () {
     *         alert(grid.getSelected());
     *     });
     * </script>
     */
    getSelected: function () {
        return ez.ezGrid.private._GetSelected(this);
    },

    /**
     * Return an array with the ids of the selected record.
     * @method
     * @memberof Grid
     * @return array
     * @example <button id="btnShowSelection">Show Selections</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ],
     *         selectionMethod: "checkbox",
     *         selectionType: "multiple"
     *     });
     *     $("#btnShowSelection").on("click", function () {
     *         var selections = grid.getSelections();
     *         $.each(selections, function() {
     *             alert(this);
     *         });
     *     });
     * </script>
     */
    getSelections: function () {
        return ez.ezGrid.private._GetSelections(this);
    },

    /**
     * Select all records from the grid.
     * @method
     * @memberof Grid
     * @return void
     * @example <button id="btnSelectAll">Select All</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ],
     *         selectionMethod: "checkbox",
     *         selectionType: "multiple"
     *     });
     *     $("#btnSelectAll").on("click", function () {
     *         grid.selectAll();
     *     });
     * </script>
     */
    selectAll: function () {
        var $grid = this,
            data = this.data('grid');
        $grid.find("thead input#checkAllBoxes").prop("checked", true);
        $grid.find("tbody tr").each(function () {
            ez.ezGrid.private.SelectRow($grid, data, $(this));
        });
    },

    /**
     * Unselect all records from the grid.
     * @method
     * @memberof Grid
     * @return void
     * @example <button id="btnSelectAll">Select All</button>
     * <button id="btnUnSelectAll">UnSelect All</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ],
     *         selectionMethod: "checkbox",
     *         selectionType: "multiple"
     *     });
     *     $("#btnSelectAll").on("click", function () {
     *         grid.selectAll();
     *     });
     *     $("#btnUnSelectAll").on("click", function () {
     *         grid.unSelectAll();
     *     });
     * </script>
     */
    unSelectAll: function () {
        var $grid = $(this),
            data = this.data('grid');
        this.find("thead input#checkAllBoxes").prop("checked", false);
        this.find("tbody tr").each(function () {
            ez.ezGrid.private.UnselectRow($grid, data, $(this));
        });
    },

    /**
     * Return record by id of the record.
     * @method
     * @memberof Grid
     * @param {string} id - The id of the row that needs to be returned.
     * @return object
     * @example <button id="btnGetData">Get Data</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ],
     *         dataKey: "ID" //define the name of the column that you want to use as ID here.
     *     });
     *     $("#btnGetData").on("click", function () {
     *         var data = grid.getById("2");
     *         alert(data.Name + " born in " + data.PlaceOfBirth);
     *     });
     * </script>
     */
    getById: function (id) {
        return ez.ezGrid.private.getRecordById(this, id);
    },

    /**
     * Return record from the grid based on position.
     * @method
     * @memberof Grid
     * @param {int} position - The position of the row that needs to be return.
     * @return object
     * @example <button id="btnGetData">Get Data</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
     *     });
     *     $("#btnGetData").on("click", function () {
     *         var data = grid.get(3);
     *         alert(data.Name + " born in " + data.PlaceOfBirth);
     *     });
     * </script>
     */
    get: function (position) {
        return ez.ezGrid.private.getByPosition(this, position);
    },

    /**
     * Return an array with all records presented in the grid.
     * @method
     * @memberof Grid
     * @return array
     * @example <button id="btnGetAllName">Get All Names</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
     *     });
     *     $("#btnGetAllName").on("click", function () {
     *         var records = grid.getAll(), names = "";
     *         $.each(records, function () { 
     *             names += this.record.Name + "(id=" + this.id + "),";
     *         });
     *         alert(names);
     *     });
     * </script>
     */
    getAll: function () {
        return ez.ezGrid.private.GetAll(this);
    },

    /**
     * Show hidden column.
     * @method
     * @memberof Grid
     * @param {string} field - The name of the field bound to the column.
     * @return grid
     * @example <button id="btnShowColumn">Show Column</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth", hidden: true } ]
     *     });
     *     $("#btnShowColumn").on("click", function () {
     *         grid.showColumn("PlaceOfBirth");
     *     });
     * </script>
     */
    showColumn: function (field) {
        var data = this.data('grid'),
            position = ez.ezGrid.private.GetColumnPosition(data.columns, field);

        if (position > -1) {
            this.find("thead>tr>th:eq(" + position + ")").show();
            $.each(this.find("tbody>tr"), function () {
                $(this).find("td:eq(" + position + ")").show();
            });
            data.columns[position].hidden = false;
            ez.ezGrid.private.updatePagerColSpan(this);
        }

        return this;
    },

    /**
     * Hide column from the grid.
     * @method
     * @memberof Grid
     * @param {string} field - The name of the field bound to the column.
     * @return grid
     * @example <button id="btnHideColumn">Hide Column</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
     *     });
     *     $("#btnHideColumn").on("click", function () {
     *         grid.hideColumn("PlaceOfBirth");
     *     });
     * </script>
     */
    hideColumn: function (field) {
        var data = this.data('grid'),
            position = ez.ezGrid.private.GetColumnPosition(data.columns, field);

        if (position > -1) {
            this.find("thead>tr>th:eq(" + position + ")").hide();
            $.each(this.find("tbody>tr"), function () {
                $(this).find("td:eq(" + position + ")").hide();
            });
            data.columns[position].hidden = true;
            ez.ezGrid.private.updatePagerColSpan(this);
        }

        return this;
    },

    /**
     * Add new row to the grid.
     * @method
     * @memberof Grid
     * @param {object} record - Object with data for the new record.
     * @return grid
     * @example <button id="btnAdd">Add Row</button>
     * <br/><br/>
     * <table id="grid"></table>
     * <script>
     *     var grid = $("#grid").grid({
     *         dataSource: "/version_0_4/Demos/GetPlayers",
     *         columns: [ { field: "ID" }, { field: "Name" }, { field: "PlaceOfBirth" } ]
     *     });
     *     $("#btnAdd").on("click", function () {
     *         grid.addRow({ "ID": grid.count() + 1, "Name": "Test Player", "PlaceOfBirth": "Test City, Test Country" });
     *     });
     * </script>
     */
    addRow: function (record) {
        var position = this.count();
        ez.ezGrid.private.RowRenderer(this, null, record, position);
        return this;
    },

    /**
     * Update row data.
     * @method
     * @memberof Grid
     * @param {string} id - The id of the row that needs to be updated
     * @param {object} record - Object with data for the new record.
     * @return grid
     * @example <table id="grid"></table>
     * <script>
     *     var grid, data;
     *     function Edit(e) {
     *         grid.updateRow(e.data.id, { "ID": e.data.id, "Name": "Ronaldo", "PlaceOfBirth": "Rio, Brazil" });
     *     }
     *     grid = $("#grid").grid({
     *         dataSource: [
     *             { "ID": 1, "Name": "Hristo Stoichkov", "PlaceOfBirth": "Plovdiv, Bulgaria" },
     *             { "ID": 2, "Name": "Ronaldo Luis Nazario de Lima", "PlaceOfBirth": "Rio de Janeiro, Brazil" },
     *             { "ID": 3, "Name": "David Platt", "PlaceOfBirth": "Chadderton, Lancashire, England" }
     *         ],
     *         columns: [ 
     *             { field: "ID" },
     *             { field: "Name" },
     *             { field: "PlaceOfBirth" },
     *             { title: "", width: 20, type: "icon", icon: "ui-icon-pencil", events: { "click": Edit } }
     *         ]
     *     });
     * </script>
     */
    updateRow: function (id, record) {
        var $row = ez.ezGrid.private.getRowById(this, id);
        ez.ezGrid.private.RowRenderer(this, $row, record, $row.index());
        return this;
    },

    //TODO: needs to be removed
    setCellContent: function (id, index, value) {
        ez.ezGrid.private.SetCellContent(this, id, index, value);
    },

    /**
     * Remove row from the grid
     * @method
     * @memberof Grid
     * @param {string} id - Id of the record that needs to be removed.
     * @return grid
     * @example <table id="grid"></table>
     * <script>
     *     var grid;
     *     function Delete(e) {
     *         if (confirm("Are you sure?")) {
     *             grid.removeRow(e.data.id);
     *         }
     *     }
     *     grid = $("#grid").grid({
     *         dataSource: [
     *             { "ID": 1, "Name": "Hristo Stoichkov", "PlaceOfBirth": "Plovdiv, Bulgaria" },
     *             { "ID": 2, "Name": "Ronaldo Luís Nazário de Lima", "PlaceOfBirth": "Rio de Janeiro, Brazil" },
     *             { "ID": 3, "Name": "David Platt", "PlaceOfBirth": "Chadderton, Lancashire, England" }
     *         ],
     *         columns: [ 
     *             { field: "ID" },
     *             { field: "Name" },
     *             { field: "PlaceOfBirth" },
     *             { title: "", width: 20, type: "icon", icon: "ui-icon-close", events: { "click": Delete } }
     *         ]
     *     });
     * </script>
     */
    removeRow: function (id) {
        var $row = ez.ezGrid.private.getRowById(this, id);
        if ($row) {
            $row.remove();
        }
        return this;
    }
};

(function ($) {
    $.fn.ezGrid = function (method) {
        if (typeof method === 'object' || !method) {
            function ezGrid() {
                var self = this;
                $.extend(self, ez.ezGrid.public);
            };
            var ezgrid = new ezGrid();
            $.extend(this, ezgrid);
            return ez.ezGrid.private.init.apply(this, arguments);
        } else if (ez.ezGrid.public[method]) {
            return ez.ezGrid.public[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else {
            throw "Method " + method + " does not exist.";
        }
    };
})(jQuery);
