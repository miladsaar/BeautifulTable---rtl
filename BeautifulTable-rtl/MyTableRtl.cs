using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeautifulTable_rtl
{
    public static class MyTableRtl
    {
        private static bool IconsSet { get; set; }

        private static string HeaderIcon { get; set; }

        private static string Excel { get; set; }

        private static string Pdf { get; set; }

        private static string Reload { get; set; }

        private static string Search { get; set; }

        private static string Printer { get; set; }

        private static bool ShowHeader { get; set; }

        private static bool ShowFooter { get; set; }

        private static bool ShowChanged { get; set; }

        private static bool Searcher { get; set; }

        private static bool Button { get; set; }

        private static bool RowCount { get; set; }

        private static string TableId { get; set; }

        private static int CountItemsInPage { get; set; }

        private static int MaxPageInline { get; set; }

        private static int CurrentPage { get; set; }

        private static bool PrimaryDataChanged { get; set; }

        private static string FooterColor { get; set; }

        private static string BodyColor { get; set; }

        private static string HeaderColor { get; set; }

        public static MvcHtmlString MakeTable(this HtmlHelper helper, string tableId, string tableName, string[] colNames, IEnumerable<object> model, TableColor color = TableColor.Black, TableBorder border = TableBorder.None, bool tableFadeInDown = true, bool tableRowsBounceIn = true)
        {
            if (!ShowChanged)
            {
                ShowHeader = true;
                ShowFooter = true;
                Searcher = true;
                Button = true;
                RowCount = true;
            }

            if (!PrimaryDataChanged)
            {
                CurrentPage = 1;
                CountItemsInPage = 10;
                MaxPageInline = 5;
            }

            if (!IconsSet)
            {
                Excel = "../Content/MyTable/Images/excel.ico";
                Reload = "../Content/MyTable/Images/SoftwareUpdate.png";
                Pdf = "../Content/MyTable/Images/pdf.ico";
                Printer = "../Content/MyTable/Images/Imprimante2.png";
                Search = "../Content/MyTable/Images/Search.png";
                HeaderIcon = "../Content/MyTable/Images/Information.png";
            }

            TableId = tableId;

            var str = string.Empty;

            str += MakeHiddenInputs(model.Count() + 2);

            var returnToDic = ReturnToDic(model, colNames);

            var firstItem = returnToDic.ElementAt(0);

            var tablefadeInDown = string.Empty;

            if (tableFadeInDown) tablefadeInDown = "table-fadeInDown";

            var tablerowsbounceIn = string.Empty;

            if (tableRowsBounceIn) tablerowsbounceIn = "table-rows-bounceIn";

            str += $"<table id='{tableId}' class='new-table {color.ToString().ToLower()}-table {border.ToString().ToLower()}-bordered {tablefadeInDown} {tablerowsbounceIn}'>";

            if (ShowHeader) str += MakeHeader(firstItem.Count, tableName);

            str += "<tbody><tr>";

            foreach (var item in firstItem)
            {

                str += $"<td>{item.Key}</td>";

            }

            foreach (var item in returnToDic)
            {
                str += "<tr>";
                foreach (var itemVal in item)
                {

                    str += $"<td>{itemVal.Value}</td>";

                }

                str += "</tr>";
            }


            str += "</tbody>";

            if (ShowFooter) str += MakeFooter(firstItem.Count, colNames);

            str += "</table>";

            return MvcHtmlString.Create(str);

        }



        public static void IconsPath(string headerIcon, string excel, string pdf, string reload, string search, string printer)
        {
            HeaderIcon = headerIcon;
            Excel = excel;
            Pdf = pdf;
            Reload = reload;
            Search = search;
            Printer = printer;
            IconsSet = true;
        }


        public static void Show(bool footer = true, bool header = true, bool searcher = true, bool button = true, bool rowCount = true)
        {
            ShowHeader = header;
            ShowFooter = footer;
            Searcher = searcher;
            Button = button;
            RowCount = rowCount;
            ShowChanged = true;
        }


        public static void PrimaryData(int countRowsInPage = 10, int maxPageInline = 5, int currentPage = 1)
        {
            CurrentPage = currentPage;
            CountItemsInPage = countRowsInPage;
            MaxPageInline = maxPageInline;
            PrimaryDataChanged = true;
        }

        public static void FontColor(string headerColor, string bodyColor, string footerColor)
        {
            HeaderColor = headerColor;
            BodyColor = bodyColor;
            FooterColor = footerColor;
        }



        private static string MakeHeader(int colNumber, string tableName)
        {
            return "<thead><tr>" +
                   $"<td colspan = '{colNumber}'>" +
                   "<div class='table-header col-md-1' style='float: right; display: inline-block;'>" +
                   $"<img class='icons' src='{HeaderIcon}' id='pdf' alt=''/>" +
                   $"<p style = 'display: inline-block' class='text-lg-left'>{tableName}</p>" +
                   "</div></td ></tr ></thead>";

        }

        private static string MakeHiddenInputs(int rowNumber)
        {
            var font = $"{HeaderColor},{BodyColor},{FooterColor}";
            return
                $"<input type='hidden' id='{TableId}currentPage' value='{CurrentPage}' />" +
                $"<input type='hidden' id='{TableId}countItemsInPage' value='{CountItemsInPage}' />" +
                $"<input type='hidden' id='{TableId}maxPageInline' value='{MaxPageInline}' />" +
                $"<input type='hidden' id='{TableId}footerRow' value={rowNumber} />" +
                $"<input type='hidden' id='{TableId}fontColor' value={font} />";


        }

        private static string MakeFooter(int colNumber, string[] colNames)
        {
            var str = string.Empty;
            var btn = string.Empty;
            var search = string.Empty;
            var rowsNumber = string.Empty;
            if (Button)
            {
                btn =
                    "<div class='col-md-2'>" +
                    $"<img class='icons' src='{Excel}' id='excel' alt='' title='دانلود به صورت اکسل' data-toggle='tooltip'/>" +
                    $"<img class='icons' src='{Pdf}' id='pdf' alt='' title='دانلود بصورت پی دی اف' data-toggle='tooltip'/>" +
                    $"<img class='icons' src='{Printer}' id='printer' alt='' title='پرینت' data-toggle='tooltip'/>" +
                    $"<img class='icons' src='{Reload}' id='reload' alt='' title='بارگزاری مجدد' data-toggle='tooltip'/>" +

                    "</div>";

            }

            if (Searcher)
            {
                foreach (var item in colNames)
                {
                    str += $"<option value = '{item}' > {item} </option >";
                }

                search = "<div class='col-md-3'>" +
                         $"<select class='input' name='search'>" +
                         str +
                         "</select>" +

                         $"<img class='col-md-3 icons' src='{Search}' id='SearchIcon' alt='' title='Search' data-toggle='tooltip' />" +
                         "<input class='col-md-3 input' type='text' style='width: 100px;' placeholder='جستجو' onkeyup='FilterCol(this)' />" +
                         "<lable id = '#dataInfo' class='col-form-label-lg'></lable>" +
                         " </div>";
            }

            if (RowCount)
            {
                rowsNumber = "<div class='col-md-3'>" +

                             $"<label for='rowsNumber' class='col-form-label-lg'>تعداد ردیف ها : </label>" +
                             $"<input class='input' name='rowsNumber' type='number' style='width: 100px;' value='{CountItemsInPage}' max='{CountItemsInPage}' min='1' />" +
                             $"<input name='btnDone' type='button' class='input btn btn-lg btn-outline-danger' value='انجام' onclick='ReloadPager(this)' />" +
                             "</div>";
            }

            return
            "<tfoot>" +
            "<tr>" +
                $"<td colspan = '{colNumber}' >" +

                    " <div class='table-footer row'>" +
                         search +
                         btn +
                         rowsNumber +

                " <div class='col-md-3'>" +
                $" <div class='btn-group btn-group-toggle ' role='toolbar' name='pages'></div>" +
                " </div>" +

                " </div>" +
                 "</td>" +
                "</tr>" +
                "</tfoot>";
        }
        //add all dics to list
        private static List<Dictionary<string, string>> ReturnToDic(IEnumerable<object> model, string[] colneed)
        {
            var list = new List<Dictionary<string, string>>();

            foreach (var item in model)
            {

                list.Add(ReadClassProp(item, colneed));
            }

            return list;


        }

        //add all properties to dictionary
        private static Dictionary<string, string> ReadClassProp(object model, string[] colneed)
        {
            var dic = new Dictionary<string, string>();
            foreach (var item in model.GetType().GetProperties())
            {

                if (colneed.Contains(item.Name))
                {
                    dic.Add(item.Name, item.GetValue(model, null).ToString());

                }
            }

            return dic;
        }

    }

    public enum TableColor
    {
        None = 0,
        Black = 1,
        Blue = 2,
        Dark = 3,
        Orange = 4,
        White = 5

    }

    public enum TableBorder
    {
        None = 0,
        Black = 1,
        Blue = 2,
        Dark = 3,
        Orange = 4,
        Shine = 5,
        White = 6
    }

}
