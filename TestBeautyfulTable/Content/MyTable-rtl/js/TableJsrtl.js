
var trVisibleList;
var countPages;
var btnSelect = "";
var btn = "";

function TableJs(id) {
 
    MakeTableList(id);
    LoadPager(id);
    SetFonts(id);
}

$(window).load(function () {

    $("#lodingOverlay").hide();
});

function HasHeader(id) {
    var c = "#" + id;
    var hasthead = $(c).has("thead").html();
    if (hasthead == undefined) {
        return false;
    }
    return true;
}

function MakeTableList(id) {
    var mId = "#" + id + " tr";
    var tr = $(mId);
    trVisibleList = new Array();
   
    var hm = 3;
    var tm = 2;
    if (!HasHeader(id)) {
        hm = 0;
        tm = 0;
    }
    for (var i = 0; i < tr.length - hm; i++) {
        trVisibleList[i] = i + tm;
    }
   
}

function FindCountPages(tableId) {
    var countRows = trVisibleList.length;
    var countItemsInPageId = "#" + tableId + "countItemsInPage";
    var countItemsInPage = parseInt($(countItemsInPageId).val());
    countPages = Math.ceil(countRows / countItemsInPage);
}

function FilterCol(x) {

    var td, i;
    var filter = $(x).val().toUpperCase();

    var tableId = "#" + $(x).parents("table").attr("id") + " tr";
    var tr = $(tableId);
    var select = "#" + $(x).parents("table").attr("id") + " select[name ='search'] option:selected";
  
    var index = $(select).index();
   
    trVisibleList = new Array();
    var k = -1;
    for (i = 1; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[index];
        if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                //tr[i].style.display = "";
                k++;
                trVisibleList[k] = i;
            } else {
                //tr[i].style.display = "none";
            }
        }
    }

    $("#currentPage").val(1);

    var id = $(x).parents("table").attr("id");

    LoadPager(id);


}

function GoPrevious(x) {
    var currentPageId = "#" + $(x).parents("table").attr("id") + "currentPage";
    var currentPage = $(currentPageId).val();
    if (currentPage > 1) {
        currentPage--;
        GoToPage(currentPage, x);
    }
}

function GoNext(x) {
    var currentPageId = "#" + $(x).parents("table").attr("id") + "currentPage";
    var currentPage = parseInt($(currentPageId).val());
    if (currentPage < countPages) {
        currentPage++;
        GoToPage(currentPage, x);
    }
}

function GoToPage(page, x) {
    var currentPageId = "#" + $(x).parents("table").attr("id") + "currentPage";
    var lastPageId = "#page" + $(currentPageId).val();
    $(lastPageId).removeClass().addClass(btnSelect);
    var thisPageId = "#page" + page;
    $(thisPageId).removeClass().addClass(btn);
    $(currentPageId).val(page);
    page--;
    var countItemsInPageId = "#" + $(x).parents("table").attr("id") + "countItemsInPage";
    var rowsNumber = parseInt($(countItemsInPageId).val());
    var firstRow = page * rowsNumber;
    var maxPageInlineId = "#" + $(x).parents("table").attr("id") + "maxPageInline";
    var maxPage = parseInt($(maxPageInlineId).val());
    var tableId = $(x).parents("table").attr("id");
    if (maxPage < countPages) {
        LoadPager(tableId);
    }

    ShowRowsTable(firstRow, tableId);

}

function ShowRowsTable(lfirstRow, tableId) {
    var hasH = HasHeader(tableId);
    var id = "#" + tableId + " tr";
    $(id).hide();
    var firstRow = parseInt(lfirstRow);
    var countItemsInPageId = "#" + tableId + "countItemsInPage";
    var rowsNumber = parseInt($(countItemsInPageId).val());
    var lastRow = firstRow + rowsNumber;
    var j;
    for (var i = firstRow; i < lastRow; i++) {

        j = trVisibleList[i];
        $(id).eq(j).show();
    }
    $(id).eq(0).show();
    var fR = 1;
    if (hasH) {
        $(id).eq(1).show();
        fR = 0;
    }
   

    var footerRowId = "#" + tableId + "footerRow";
    var endRow = $(footerRowId).val()-fR;
    $(id).eq(endRow).show();
}

function ReloadPager(x) {
   
  
    var tableId = $(x).parents("table").attr("id");
    var btnS = "#" + tableId + " input[name='rowsNumber']";
    var rowsNumber = parseInt($(btnS).val());
    var countItemsInPageId = "#" + $(x).parents("table").attr("id") + "countItemsInPage";
    $(countItemsInPageId).val(rowsNumber);
    var currentPageId = "#" + $(x).parents("table").attr("id") + "currentPage";
    $(currentPageId).val("1");
   
   
    LoadPager(tableId);
}

function LoadPager(id) {

    var startNum = 0;
    var appendDate = "";
    var currentPageId = "#" + id + "currentPage";
 
    var currentPage = parseInt($(currentPageId).val());

    FindCountPages(id);
    var prv = currentPage - 1;
    var next = prv + 2;
    var pagesNumber = countPages;
    var maxPageInlineId = "#" + id + "maxPageInline";
    var maxPage = parseInt($(maxPageInlineId).val());
    var pages = "#" + id + " div[name='pages']";

    if (countPages >= maxPage) {

        if ((countPages - currentPage) + 1 > maxPage) {
            pagesNumber = prv + maxPage;
            startNum = currentPage - 1;
        } else {
            pagesNumber = countPages;
            startNum = countPages - (maxPage);
        }

    }

    $(pages).empty();


    if (countPages > 1) {
        $(pages).append("<a class='btn btn-warning prvButton' href='#'" +
            "onclick='GoPrevious(this)'>" +
            "<span> قبلی</span>" +
            "</a>");
    }
    var btnClass = "";

    for (var i = startNum; i < pagesNumber; i++) {
        var num = i + 1;

        btnClass = SetBtn(currentPage, i, id);

        appendDate = "<a class='btn " +
            btnClass +
            "' href='#'" +
            "onclick='GoToPage(" +
            num +
            ",this)'" +
            "Id='" +
            id+
            "page" +
            num +
            "'>" +
            num +
            "</a>";

        $(pages).append(appendDate);
    }

    var thisPageId = "#page" + 1;

    $(thisPageId).removeClass().addClass(btnSelect);

    if (countPages > 1) {
        $(pages).append("<a class='btn btn-warning nextButton' href='#' " +
            "onclick='GoNext(this)'>" +
            "<span>بعدی</span>" +
            "</a>");
    }

    ShowRowsTable(0, id);

}

function SetBtn(currentPage, i, tableId) {
    var btnClass = "";
    var id = "#" + tableId;
    var btnS = "#" +tableId+" input[name='btnDone']";
    
    if ($(id).hasClass("orange-table")) {
        if (currentPage - 1 == i) {
            btnClass = "btn-danger";
        } else {
            btnClass = "btn-outline-danger";
        }
        btnSelect = "btn btn-danger";
        btn = "btn-outline-danger";

        $(btnS).removeClass().addClass("input btn btn-lg btn-outline-danger");
    } else if ($(id).hasClass("blue-table")) {
        if (currentPage - 1 == i) {
            btnClass = "btn-primary";
        } else {
            btnClass = "btn-outline-primary";
        }
        btnSelect = "btn btn-primary";
        btn = "btn-outline-primary";
        $(btnS).removeClass().addClass("input btn btn-lg btn-outline-primary");
    } else if ($(id).hasClass("dark-table")) {
        if (currentPage - 1 == i) {
            btnClass = "btn-dark";
        } else {
            btnClass = "btn-outline-dark";
        }
        btnSelect = "btn btn-dark";
        btn = "btn-outline-dark";
        $(btnS).removeClass().addClass("input btn btn-lg btn-outline-dark");
    } else if ($(id).hasClass("black-table")) {
        if (currentPage - 1 == i) {
            btnClass = "btn-warning";
        } else {
            btnClass = "btn-outline-warning";
        }
        btnSelect = "btn btn-warning";
        btn = "btn-outline-warning";
        $(btnS).removeClass().addClass("input btn btn-lg btn-outline-dark");
    } else if ($(id).hasClass("white-table")) {
        if (currentPage - 1 == i) {
            btnClass = "btn-dark";
        } else {
            btnClass = "btn-outline-dark";
        }
        btnSelect = "btn btn-dark";
        btn = "btn-outline-dark";
        $(btnS).removeClass().addClass("input btn btn-lg btn-outline-dark");
    } else if ($(id).hasClass("none-table")) {
        if (currentPage - 1 == i) {
            btnClass = "btn-light";
        } else {
            btnClass = "btn-outline-light";
        }
        btnSelect = "btn btn-light";
        btn = "btn-outline-light";
        $(btnS).removeClass().addClass("input btn btn-lg btn-outline-light");
    }


    return btnClass;
}

function SetFonts(id) {
    var headerId = "#" + id + " thead";
    var footerId = "#" + id + " tfoot";
    //var btnColor = "#" + id + " tfoot a";
    var bodyId = "#" + id + " tbody tr td";
    var colors = "#" + id + "fontColor";
    var colorsFonts = $(colors).val();
    if (colorsFonts.length > 0) {
        var a = colorsFonts.split(",");
        $(headerId).css("color", a[0]);
        $(bodyId).css("color", a[1]);
        $(footerId).css("color", a[2]);
        //$(btnColor).css("color", a[2]);
    }
   

}
