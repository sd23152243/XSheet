using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XSheet.Util
{
    class SheetUtil
    {
        public static Worksheet getSheetByName(String name,WorksheetCollection sheets)
        {
            Worksheet sheet = null;
            try
            {
                sheet = sheets[name];
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Sheet："+name+"不存在!请检查配置！");
            }
            return sheet;
        }

        public static DefinedName getNameinNames(String str, DefinedNameCollection names)
        {
            DefinedName name = null;
            try
            {
                name= names.GetDefinedName(str);
            }
            catch (Exception)
            {
                MessageBox.Show("未找到名称为" + str + "的EXCEL命名区域，请确认配置");

            }
            return name;
        }

        public static DataTable SimpleDataExport(Range range,bool rangeHasHeaders)
        {
            #region #SimpleDataExport
            Worksheet worksheet = range.Worksheet;

            // Create a data table with column names obtained from the first row in a range if it has headers.
            // Column data types are obtained from cell value types of cells in the first data row of the worksheet range.
            DataTable dataTable = worksheet.CreateDataTable(range, rangeHasHeaders);

            //Validate cell value types. If cell value types in a column are different, the column values are exported as text.
            for (int col = 0; col < range.ColumnCount; col++)
            {
                CellValueType cellType = range[0, col].Value.Type;
                for (int r = 1; r < range.RowCount; r++)
                {
                    if (cellType != range[r, col].Value.Type)
                    {
                        dataTable.Columns[col].DataType = typeof(string);
                        break;
                    }
                }
            }

            // Create the exporter that obtains data from the specified range, 
            // skips the header row (if required) and populates the previously created data table. 
            DataTableExporter exporter = worksheet.CreateDataTableExporter(range, dataTable, rangeHasHeaders);
            // Handle value conversion errors.
            exporter.CellValueConversionError += exporter_CellValueConversionError;

            // Perform the export.
            exporter.Export();
            #endregion #SimpleDataExport
            // A custom method that displays the resulting data table.
            return  dataTable;
        }

        public static DataTable ExportRangeStopOnEmptyRow(Range range,bool rangeHasHeaders,bool stopOnEmptyRow)
        {
            // Create a data table with column names obtained from the first row in a range if it has headers.
            // Column data types are obtained from cell value types of cells in the first data row of the worksheet range.
            DataTable dataTable = range.Worksheet.CreateDataTable(range, rangeHasHeaders);
            // Create the exporter that obtains data from the specified range, 
            // skips the header row (if required) and populates the previously created data table. 
            DataTableExporter exporter = range.Worksheet.CreateDataTableExporter(range, dataTable, rangeHasHeaders);
            // Handle value conversion errors.
            exporter.CellValueConversionError += (sender, args) => { args.Action = DataTableExporterAction.Continue; };
            if (stopOnEmptyRow)
            {
                exporter.Options.SkipEmptyRows = false;
                // Handle empty row.
                exporter.ProcessEmptyRow += (sender, args) => { args.Action = DataTableExporterAction.Stop; };
            }
            // Perform the export.
            exporter.Export();
            // A custom method that displays the resulting data table.
            return dataTable;
        }

        public static DataTable ExportUseExporterOptions(Range range,bool rangeHasHeaders, bool  skipErrorValues )
        {
            #region #DataExportWithOptions
            Worksheet worksheet = range.Worksheet;

            // Create a data table with column names obtained from the first row in a range.
            // Column data types are obtained from cell value types of cells in the first data row of the worksheet range.
            DataTable dataTable = worksheet.CreateDataTable(range, rangeHasHeaders);

            // Create the exporter that obtains data from the specified range which has a header row and populates the previously created data table. 
            DataTableExporter exporter = worksheet.CreateDataTableExporter(range, dataTable, rangeHasHeaders);
            // Handle value conversion errors.
            exporter.CellValueConversionError += exporter_CellValueConversionError;

            // Specify exporter options.
            exporter.Options.ConvertEmptyCells = true;
            exporter.Options.DefaultCellValueToColumnTypeConverter.EmptyCellValue = 0;
            exporter.Options.DefaultCellValueToColumnTypeConverter.SkipErrorValues = skipErrorValues;

            // Perform the export.
            exporter.Export();
            #endregion #DataExportWithOptions
            // A custom method that displays the resulting data table.
            return dataTable;
        }

        #region #DataExportWithCustomConverter
        private void barButtonItem1_ItemClick(Range range)
        {
            Worksheet worksheet = range.Worksheet ;

            // Create a data table with column names obtained from the first row in a range.
            // Column data types are obtained from cell value types of cells in the first data row of the worksheet range.
            DataTable dataTable = worksheet.CreateDataTable(range, true);
            // Change the data type of the "As Of" column to text.
            dataTable.Columns["As Of"].DataType = System.Type.GetType("System.String");
            // Create the exporter that obtains data from the specified range and populates the specified data table. 
            DataTableExporter exporter = worksheet.CreateDataTableExporter(range, dataTable, true);
            // Handle value conversion errors.
            exporter.CellValueConversionError += exporter_CellValueConversionError;

            // Specify a custom converter for the "As Of" column.
            DateTimeToStringConverter toDateStringConverter = new DateTimeToStringConverter();
            exporter.Options.CustomConverters.Add("As Of", toDateStringConverter);
            // Set the export value for empty cell.
            toDateStringConverter.EmptyCellValue = "N/A";
            // Specify that empty cells and cells with errors should be processed.
            exporter.Options.ConvertEmptyCells = true;
            exporter.Options.DefaultCellValueToColumnTypeConverter.SkipErrorValues = false;

            // Perform the export.
            exporter.Export();

            // A custom method that displays the resulting data table.
            //ShowResult(dataTable);
        }

        // A custom converter that converts DateTime values to "Month-Year" text strings.
        class DateTimeToStringConverter : ICellValueToColumnTypeConverter
        {
            public bool SkipErrorValues { get; set; }
            public CellValue EmptyCellValue { get; set; }

            public ConversionResult Convert(Cell readOnlyCell, CellValue cellValue, Type dataColumnType, out object result)
            {
                result = DBNull.Value;
                ConversionResult converted = ConversionResult.Success;
                if (cellValue.IsEmpty)
                {
                    result = EmptyCellValue;
                    return converted;
                }
                if (cellValue.IsError)
                {
                    // You can return an error, subsequently the exporter throws an exception if the CellValueConversionError event is unhandled.
                    //return SkipErrorValues ? ConversionResult.Success : ConversionResult.Error;
                    result = "N/A";
                    return ConversionResult.Success;
                }
                result = String.Format("{0:MMMM-yyyy}", cellValue.DateTimeValue);
                return converted;
            }
        }
        #endregion #DataExportWithCustomConverter

        #region #CellValueConversionErrorHandler
        static void exporter_CellValueConversionError(object sender, CellValueConversionErrorEventArgs e)
        {
            MessageBox.Show("Error in cell " + e.Cell.GetReferenceA1());
            e.DataTableValue = null;
            e.Action = DataTableExporterAction.Continue;
        }
        #endregion #CellValueConversionErrorHandler
    }
}
