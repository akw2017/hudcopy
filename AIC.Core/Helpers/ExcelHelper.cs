using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;

namespace AIC.Core.Helpers
{
    public class ExcelHelper
    {
        private Excel.Application _excelApp = null;
        private Excel.Workbooks _books = null;
        private Excel._Workbook _book = null;
        private Excel.Sheets _sheets = null;
        //private Excel._Worksheet _sheet = null;
        private Excel.Range _range = null;
        private Excel.Font _font = null;
        // Optional argument variable
        private object _optionalValue = Missing.Value;

        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public DataTable LoadExcel(string pPath)
        {
            //Driver={Driver do Microsoft Excel(*.xls)} 这种连接写法不需要创建一个数据源DSN，DRIVERID表示驱动ID，Excel2003后都使用790，FIL表示Excel文件类型，Excel2007用excel 8.0，MaxBufferSize表示缓存大小，DBQ表示读取Excel的文件名（全路径）

            string connString = "Driver={Driver do Microsoft Excel(*.xls)};DriverId=790;SafeTransactions=0;ReadOnly=1;MaxScanRows=16;Threads=3;MaxBufferSize=2024;UserCommitSync=Yes;FIL=excel 8.0;PageTimeout=5;";
            connString += "DBQ=" + pPath;
            OdbcConnection conn = new OdbcConnection(connString);
            OdbcCommand cmd = new OdbcCommand();
            cmd.Connection = conn;
            //获取Excel中第一个Sheet名称，作为查询时的表名
            string sheetName = this.GetExcelSheetName(pPath);
            string sql = "select * from [" + sheetName.Replace('.', '#') + "$]";
            cmd.CommandText = sql;
            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                ds = null;
                throw new Exception("从Excel文件中获取数据时发生错误:" + ex.ToString());
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                da.Dispose();
                da = null;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn = null;
            }
        }
        private string GetExcelSheetName(string pPath)
        {
            //打开一个Excel应用

            _excelApp = new Excel.Application();
            if (_excelApp == null)
            {
                throw new Exception("打开Excel应用时发生错误！");
            }
            _books = _excelApp.Workbooks;
            //打开一个现有的工作薄
            _book = _books.Add(pPath);
            _sheets = _book.Sheets;
            //选择第一个Sheet页
            Excel._Worksheet sheet = (Excel._Worksheet)_sheets.get_Item(1);
            string sheetName = sheet.Name;

            ReleaseCOM(sheet);
            ReleaseCOM(_sheets);
            ReleaseCOM(_book);
            ReleaseCOM(_books);
            _excelApp.Quit();
            ReleaseCOM(_excelApp);
            return sheetName;
        }

        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public List<DataTable> LoadExcelSheets(string pPath)
        {
            //Driver={Driver do Microsoft Excel(*.xls)} 这种连接写法不需要创建一个数据源DSN，DRIVERID表示驱动ID，Excel2003后都使用790，FIL表示Excel文件类型，Excel2007用excel 8.0，MaxBufferSize表示缓存大小，DBQ表示读取Excel的文件名（全路径）

            string connString = "Driver={Driver do Microsoft Excel(*.xls)};DriverId=790;SafeTransactions=0;ReadOnly=1;MaxScanRows=16;Threads=3;MaxBufferSize=2024;UserCommitSync=Yes;FIL=excel 8.0;PageTimeout=5;Extended Properties=Excel 8.0;HDR=YES;IMEX=1;"; 
            connString += "DBQ=" + pPath;          
            OdbcConnection conn = new OdbcConnection(connString);
            OdbcCommand cmd = new OdbcCommand();
            cmd.Connection = conn;
            //获取Excel中第一个Sheet名称，作为查询时的表名
            List<string> sheetNames = this.GetExcelSheetNames(pPath);
            List<DataTable> tables = new List<DataTable>();

            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                foreach (var sheetName in sheetNames)
                {
                    string sql = "select * from [" + sheetName.Replace('.', '#') + "$]";
                    cmd.CommandText = sql;
                    da = new OdbcDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    ds.Tables[0].TableName = sheetName;
                    tables.Add(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                ds = null;
                throw new Exception("从Excel文件中获取数据时发生错误:" + ex.ToString());
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                da.Dispose();
                da = null;              
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn = null;                
            }
            return tables;
        }

        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public List<DataTable> OleDbLoadExcelSheet(string pPath)
        {
            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = " + pPath + ";Extended Properties =Excel 8.0";//导入时包含Excel中的第一行数据，并且将数字和字符混合的单元格视为文本进行导入
            OleDbConnection conn = new OleDbConnection(connString);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            //获取Excel中第一个Sheet名称，作为查询时的表名
            List<string> sheetNames = this.GetExcelSheetNames(pPath);
            List<DataTable> tables = new List<DataTable>();

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                foreach (var sheetName in sheetNames)
                {
                    string sql = "select * from [" + sheetName.Replace('.', '#') + "$]";
                    cmd.CommandText = sql;
                    da = new OleDbDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    ds.Tables[0].TableName = sheetName;
                    tables.Add(ds.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                ds = null;
                throw new Exception("从Excel文件中获取数据时发生错误:" + ex.ToString());
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                da.Dispose();
                da = null;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn = null;
            }
            return tables;
        }

        private List<string> GetExcelSheetNames(string pPath)
        {
            //打开一个Excel应用
            List<string> names = new List<string>();
            _excelApp = new Excel.Application();
            if (_excelApp == null)
            {
                throw new Exception("打开Excel应用时发生错误！");
            }
            _books = _excelApp.Workbooks;
            //打开一个现有的工作薄
            _book = _books.Add(pPath);
            _sheets = _book.Sheets;

            foreach (var sheet in _sheets)
            {
                //选择第一个Sheet页
                string sheetname = ((Excel._Worksheet)sheet).Name;
                names.Add(sheetname);
            }

            foreach (var sheet in _sheets)
            {
                ReleaseCOM(sheet);
            }
            ReleaseCOM(_sheets);
            ReleaseCOM(_book);
            ReleaseCOM(_books);
            _excelApp.Quit();
            ReleaseCOM(_excelApp);
            return names;
        }
        /// <summary>
        /// 释放COM对象
        /// </summary>
        /// <param name="pObj"></param>
        private void ReleaseCOM(object pObj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pObj);
            }
            catch
            {
                throw new Exception("释放资源时发生错误！");
            }
            finally
            {
                pObj = null;
            }
        }

        /// <summary>
        /// 保存到Excel
        /// </summary>
        /// <param name="excelName"></param>
        public string SaveToExcel(string excelName, DataTable dataTable)
        {
            string error = null;
            Excel._Worksheet sheet = null;
            try
            {
                if (dataTable != null)
                {
                    if (dataTable.Rows.Count != 0)
                    {
                        Mouse.SetCursor(Cursors.Wait);
                        sheet = CreateExcelRef(new List<DataTable>() { dataTable }).FirstOrDefault();
                        FillSheet(dataTable, sheet);
                        SaveExcel(excelName);
                        Mouse.SetCursor(Cursors.Arrow);
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                ReleaseCOM(sheet);
                ReleaseCOM(_sheets);
                ReleaseCOM(_book);
                ReleaseCOM(_books);
                ReleaseCOM(_excelApp);
            }
            return error;
        }

        /// <summary>
        /// 保存到Excel
        /// </summary>
        /// <param name="excelName"></param>
        public string SaveToExcel(string excelName, List<DataTable> dataTables)
        {
            string error = null;
            List<Excel._Worksheet> sheets = null;
            try
            {
                if (dataTables != null)
                {
                    Mouse.SetCursor(Cursors.Wait);
                    sheets = CreateExcelRef(dataTables);
                    foreach (var dataTable in dataTables)
                    {
                        var sheet = sheets.Where(p => p.Name == dataTable.TableName).FirstOrDefault();
                        FillSheet(dataTable, sheet);
                    }
                    SaveExcel(excelName);
                    Mouse.SetCursor(Cursors.Arrow);
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            finally
            {
                sheets.ForEach(p => ReleaseCOM(p));
                ReleaseCOM(_sheets);
                ReleaseCOM(_book);
                ReleaseCOM(_books);
                ReleaseCOM(_excelApp);
            }
            return error;
        }

        /// <summary>
        /// 将内存中Excel保存到本地路径
        /// </summary>
        /// <param name="excelName"></param>
        private void SaveExcel(string excelName)
        {
            _excelApp.Visible = false;
            //保存为Office2003和Office2007都兼容的格式
            _book.SaveAs(excelName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            _excelApp.Quit();
        }

        /// <summary>
        /// 将数据填充到内存Excel的工作表
        /// </summary>
        /// <param name="dataTable"></param>
        private void FillSheet(DataTable dataTable, Excel._Worksheet sheet)
        {
            object[] header = CreateHeader(dataTable, sheet);
            WriteData(header, dataTable, sheet);
        }


        private void WriteData(object[] header, DataTable dataTable, Excel._Worksheet sheet)
        {
            object[,] objData = new object[dataTable.Rows.Count, header.Length];

            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                var item = dataTable.Rows[j];
                for (int i = 0; i < header.Length; i++)
                {
                    var y = dataTable.Rows[j][i];
                    objData[j, i] = (y == null) ? "" : y.ToString();
                }
            }
            AddExcelRows("A2", dataTable.Rows.Count, header.Length, objData, sheet);
            AutoFitColumns("A1", dataTable.Rows.Count + 1, header.Length, sheet);
        }


        private void AutoFitColumns(string startRange, int rowCount, int colCount, Excel._Worksheet sheet)
        {
            _range = sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.Columns.AutoFit();
        }


        private object[] CreateHeader(DataTable dataTable, Excel._Worksheet sheet)
        {
            List<object> objHeaders = new List<object>();
            for (int n = 0; n < dataTable.Columns.Count; n++)
            {
                objHeaders.Add(dataTable.Columns[n].ColumnName);
            }

            var headerToAdd = objHeaders.ToArray();
            //工作表的单元是从“A1”开始
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd, sheet);
            SetHeaderStyle();

            return headerToAdd;
        }

        /// <summary>
        /// 将表头加粗显示
        /// </summary>
        private void SetHeaderStyle()
        {
            _font = _range.Font;
            _font.Bold = true;
        }

        /// <summary>
        /// 将数据填充到Excel工作表的单元格中
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="values"></param>
        private void AddExcelRows(string startRange, int rowCount, int colCount, object values, Excel._Worksheet sheet)
        {
            _range = sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.set_Value(_optionalValue, values);
        }
        /// <summary>
        /// 创建一个Excel程序实例
        /// </summary>
        private List<Excel._Worksheet> CreateExcelRef(List<DataTable> tables)
        {
            List<Excel._Worksheet> sheets = new List<Excel._Worksheet>();
            _excelApp = new Excel.Application();
            _books = (Excel.Workbooks)_excelApp.Workbooks;
            _book = (Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Excel.Sheets)_book.Worksheets;
            Excel._Worksheet sheet1 = (Excel._Worksheet)(_sheets.get_Item(1));//删除Sheet1
            Excel._Worksheet sheet2 = (Excel._Worksheet)(_sheets.get_Item(2));//删除Sheet2
            Excel._Worksheet sheet3 = (Excel._Worksheet)(_sheets.get_Item(3));//删除Sheet3
            foreach (var table in tables)
            {
                Excel._Worksheet sheet = _sheets.Add();
                sheet.Name = table.TableName;
                sheets.Add(sheet);
            }        
            sheet1.Delete();           
            sheet2.Delete();           
            sheet3.Delete();
            return sheets;
        }

        #region 删除Excel文件中指定的Sheet
        public bool DeleteExcelSheet(string pExcelPath, string pSheetName, out string pDeleteSheet)
        {
            try
            {
                object objOpt = Missing.Value;
                //打开一个Excel应用
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                if (app == null)
                {
                    throw new Exception("打开Excel应用时发生错误！");
                }
                app.Visible = false;
                Microsoft.Office.Interop.Excel.Workbooks wbs = app.Workbooks;
                Microsoft.Office.Interop.Excel._Workbook wb = wbs.Open(pExcelPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                wb.EnableAutoRecover = false;
                Microsoft.Office.Interop.Excel.Sheets shs = wb.Sheets;
                Microsoft.Office.Interop.Excel._Worksheet sh = (Microsoft.Office.Interop.Excel._Worksheet)shs.get_Item(pSheetName);
                app.DisplayAlerts = false;
                sh.Delete();
                wb.Save();
                pDeleteSheet = string.Empty;
                return true;
            }
            catch (Exception vErr)
            {
                pDeleteSheet = vErr.Message;
                return false;
            }
            finally
            {
                KillProcess();
            }
        }
        //强制清除Excel系统进程
        public void KillProcess()
        {
            try
            {
                foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName("Excel"))
                {
                    if (!p.CloseMainWindow())
                    {
                        p.Kill();
                    }
                }
                GC.Collect();
            }
            catch (Exception vErr)
            {
                throw new Exception("", vErr);
            }
        }
        #endregion
    }
}
