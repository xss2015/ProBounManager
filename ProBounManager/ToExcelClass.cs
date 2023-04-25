using ProBounManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ProBounManager
{
    class ToExcelClass
    {
        public static void ExportDataGrid(object sender)
        {
            DataGrid currentGrid = sender as DataGrid;
            if (currentGrid != null)
            {
                StringBuilder sbGridData = new StringBuilder();
                List<string> listColumns = new List<string>();

                List<DataGridColumn> listVisibleDataGridColumns = new List<DataGridColumn>();

                List<string> listHeaders = new List<string>();

                Microsoft.Office.Interop.Excel.Application application = null;

                Microsoft.Office.Interop.Excel.Workbook workbook = null;

                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

                int rowCount = 1;

                int colCount = 1;

                try
                {
                    application = new Microsoft.Office.Interop.Excel.Application();
                    workbook = application.Workbooks.Add(Type.Missing);
                    worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

                    if (currentGrid.HeadersVisibility == DataGridHeadersVisibility.Column || currentGrid.HeadersVisibility == DataGridHeadersVisibility.All)
                    {
                        foreach (DataGridColumn dataGridColumn in currentGrid.Columns.Where(dataGridColumn => dataGridColumn.Visibility == Visibility.Visible))
                        {
                            listVisibleDataGridColumns.Add(dataGridColumn);
                            if (dataGridColumn.Header != null)
                            {
                                listHeaders.Add(dataGridColumn.Header.ToString());
                            }
                            worksheet.Cells[rowCount, colCount] = dataGridColumn.Header;
                            colCount++;
                        }

                        // IEnumerable collection = currentGrid.ItemsSource;

                        foreach (object data in currentGrid.ItemsSource)
                        {
                            listColumns.Clear();

                            colCount = 1;

                            rowCount++;

                            foreach (DataGridColumn dataGridColumn in listVisibleDataGridColumns)
                            {
                                string strValue = string.Empty;
                                Binding objBinding = null;
                                DataGridBoundColumn dataGridBoundColumn = dataGridColumn as DataGridBoundColumn;

                                if (dataGridBoundColumn != null)
                                {
                                    objBinding = dataGridBoundColumn.Binding as Binding;
                                }

                                DataGridTemplateColumn dataGridTemplateColumn = dataGridColumn as DataGridTemplateColumn;

                                if (dataGridTemplateColumn != null)
                                {
                                    // This is a template column...let us see the underlying dependency object

                                    DependencyObject dependencyObject = dataGridTemplateColumn.CellTemplate.LoadContent();

                                    FrameworkElement frameworkElement = dependencyObject as FrameworkElement;
                                    if (frameworkElement != null)
                                    {
                                        FieldInfo fieldInfo = frameworkElement.GetType().GetField("ContentProperty", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                                        if (fieldInfo == null)
                                        {
                                            if (frameworkElement is System.Windows.Controls.TextBox || frameworkElement is TextBlock || frameworkElement is ComboBox)
                                            {
                                                fieldInfo = frameworkElement.GetType().GetField("TextProperty");
                                            }
                                            else if (frameworkElement is DatePicker)
                                            {
                                                fieldInfo = frameworkElement.GetType().GetField("SelectedDateProperty");
                                            }
                                        }

                                        if (fieldInfo != null)
                                        {
                                            DependencyProperty dependencyProperty = fieldInfo.GetValue(null) as DependencyProperty;
                                            if (dependencyProperty != null)
                                            {
                                                BindingExpression bindingExpression = frameworkElement.GetBindingExpression(dependencyProperty);
                                                if (bindingExpression != null)
                                                {
                                                    objBinding = bindingExpression.ParentBinding;
                                                }
                                            }
                                        }
                                    }
                                }

                                if (objBinding != null)
                                {

                                    if (!String.IsNullOrEmpty(objBinding.Path.Path))
                                    {

                                        PropertyInfo pi = data.GetType().GetProperty(objBinding.Path.Path);

                                        if (pi != null)
                                        {

                                            object propValue = pi.GetValue(data, null);

                                            if (propValue != null)
                                            {
                                                strValue = Convert.ToString(propValue);
                                            }

                                            else
                                            {
                                                strValue = string.Empty;
                                            }
                                        }
                                    }

                                    if (objBinding.Converter != null)
                                    {
                                        if (!String.IsNullOrEmpty(strValue))
                                        {
                                            strValue = objBinding.Converter.Convert(strValue, typeof(string), objBinding.ConverterParameter, objBinding.ConverterCulture).ToString();
                                        }

                                        else
                                        {
                                            strValue = objBinding.Converter.Convert(data, typeof(string), objBinding.ConverterParameter, objBinding.ConverterCulture).ToString();
                                        }
                                    }
                                }

                                listColumns.Add(strValue);

                                worksheet.Cells[rowCount, colCount] = strValue;

                                colCount++;
                            }
                        }
                    }

                }

                catch (System.Runtime.InteropServices.COMException)
                {
                }

                finally
                {
                    workbook.Close();
                    application.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
                }

            }
        }
        public static void ExportDataPromo(DataGrid mygrid)
        {
            try
            {
                var data = (List<Result>)mygrid.ItemsSource;
                Microsoft.Office.Interop.Excel.Application application = null;

                Microsoft.Office.Interop.Excel.Workbook workbook = null;

                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

                application = new Microsoft.Office.Interop.Excel.Application();
                workbook = application.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

                int rowI = 2;
                foreach (var item in data)
                {


                    worksheet.Cells[1, 1] = mygrid.Columns[1].Header.ToString();
                    worksheet.Cells[1, 2] = mygrid.Columns[2].Header.ToString();
                    worksheet.Cells[1, 3] = mygrid.Columns[3].Header.ToString();
                    worksheet.Cells[1, 4] = mygrid.Columns[4].Header.ToString();
                    worksheet.Cells[1, 5] = mygrid.Columns[5].Header.ToString();
                    worksheet.Cells[1, 6] = mygrid.Columns[6].Header.ToString();
                    worksheet.Cells[1, 7] = mygrid.Columns[7].Header.ToString();
                    worksheet.Cells[1, 8] = mygrid.Columns[8].Header.ToString();
                    worksheet.Cells[1, 9] = mygrid.Columns[9].Header.ToString();
                    //worksheet.Cells[1, 10] = mygrid.Columns[10].Header.ToString();
                    //worksheet.Cells[1, 11] = mygrid.Columns[11].Header.ToString();

                    worksheet.Cells[rowI, 1] = item.Emp.EmpName; // Emp Name
                    worksheet.Cells[rowI, 2] = item.PromoHistory.Rank.RankName; // Crnt Rank
                    worksheet.Cells[rowI, 3] = item.BouncHistory.Stage.StageName; //crnt Stage
                    worksheet.Cells[rowI, 4] = item.PromoHistory.PromoCrntCareer; //crnt Career
                    worksheet.Cells[rowI, 5] = item.PromoHistory.PromoNextRank; // next rank
                    worksheet.Cells[rowI, 6] = item.PromoHistory.PromoNextRankDate; //next rank date
                    worksheet.Cells[rowI, 7] = item.BouncHistory.BouncNextStage; // next stage
                    worksheet.Cells[rowI, 8] = item.PromoHistory.PromoNextCareer; // next career
                    worksheet.Cells[rowI, 9] = item.Emp.EmpNote; // Emp note
                                                                 //worksheet.Cells[rowI, 4] = item.Emp.PrivilageMonth.PrivilageMonthName;
                                                                 //worksheet.Cells[rowI, 5] = item.Emp.PrivilageYear.PrivilageYearName;

                    //worksheet.Cells[rowI, 7] = item.PromoHistory.PromoNextRankDate;

                    //worksheet.Cells[rowI, 9] = item.Emp.EmpNote;

                    rowI += 1;

                }

                workbook.Close();
                application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);


            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
        public static void ExportDataBounc(DataGrid mygrid)
        {
            try
            {
                var data = (List<Result>)mygrid.ItemsSource;
                Microsoft.Office.Interop.Excel.Application application = null;

                Microsoft.Office.Interop.Excel.Workbook workbook = null;

                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

                application = new Microsoft.Office.Interop.Excel.Application();
                workbook = application.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

                int rowI = 2;
                foreach (var item in data)
                {


                    worksheet.Cells[1, 1] = mygrid.Columns[1].Header.ToString();
                    worksheet.Cells[1, 2] = mygrid.Columns[2].Header.ToString();
                    worksheet.Cells[1, 3] = mygrid.Columns[3].Header.ToString();
                    worksheet.Cells[1, 4] = mygrid.Columns[4].Header.ToString();
                    worksheet.Cells[1, 5] = mygrid.Columns[5].Header.ToString();
                    worksheet.Cells[1, 6] = mygrid.Columns[6].Header.ToString();
                    worksheet.Cells[1, 7] = mygrid.Columns[7].Header.ToString();
                    //worksheet.Cells[1, 8] = mygrid.Columns[7].Header.ToString();



                    worksheet.Cells[rowI, 1] = item.Emp.EmpName;
                    worksheet.Cells[rowI, 2] = item.PromoHistory.PromoCrntCareer;
                    worksheet.Cells[rowI, 3] = item.PromoHistory.Rank.RankName;
                    worksheet.Cells[rowI, 4] = item.BouncHistory.Stage.StageName;
                    worksheet.Cells[rowI, 5] = item.BouncHistory.BouncNextStage;
                    worksheet.Cells[rowI, 6] = item.BouncHistory.BouncNextStageDate;
                    //worksheet.Cells[rowI, 7] = item.BouncHistory.BouncNextStageDate;
                    worksheet.Cells[rowI, 7] = item.Emp.EmpNote;

                    rowI += 1;

                }

                workbook.Close();
                application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }
        public static void ExportPromoHistory(DataGrid mygrid)
        {
            try
            {
                var data = (List<PromoHistory>)mygrid.ItemsSource;
                Microsoft.Office.Interop.Excel.Application application = null;

                Microsoft.Office.Interop.Excel.Workbook workbook = null;

                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

                application = new Microsoft.Office.Interop.Excel.Application();
                workbook = application.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

                int rowI = 3;
                foreach (var item in data)
                {
                    worksheet.Cells[1, 1] = item.Emp.EmpName;

                    worksheet.Cells[2, 1] = mygrid.Columns[0].Header.ToString();
                    worksheet.Cells[2, 2] = mygrid.Columns[1].Header.ToString();
                    worksheet.Cells[2, 3] = mygrid.Columns[2].Header.ToString();
                    worksheet.Cells[2, 4] = mygrid.Columns[3].Header.ToString();

                    //worksheet.Cells[1, 5] = mygrid.Columns[5].Header.ToString();
                    //worksheet.Cells[1, 6] = mygrid.Columns[6].Header.ToString();

                    //worksheet.Cells[1, 8] = mygrid.Columns[7].Header.ToString();



                    worksheet.Cells[rowI, 1] = item.Rank.RankName;
                    worksheet.Cells[rowI, 2] = item.PromoCrntCareer;
                    worksheet.Cells[rowI, 3] = item.PromoCrntRankDate;

                    //worksheet.Cells[rowI, 4] = item.BouncHistory.Stage.StageName;
                    //worksheet.Cells[rowI, 5] = item.BouncHistory.BouncNextStage;
                    //worksheet.Cells[rowI, 6] = item.BouncHistory.BouncNextStageDate;
                    //worksheet.Cells[rowI, 7] = item.BouncHistory.BouncNextStageDate;
                    worksheet.Cells[rowI, 4] = item.Emp.EmpNote;

                    rowI += 1;

                }

                workbook.Close();
                application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }
        public static void ExportBouncHistory(DataGrid mygrid)
        {
            try
            {
                var data = (List<BouncHistory>)mygrid.ItemsSource;
                Microsoft.Office.Interop.Excel.Application application = null;

                Microsoft.Office.Interop.Excel.Workbook workbook = null;

                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

                application = new Microsoft.Office.Interop.Excel.Application();
                workbook = application.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

                int rowI = 3;
                foreach (var item in data)
                {
                    worksheet.Cells[1, 1] = item.PromoHistory.Emp.EmpName;
                    worksheet.Cells[1, 2] = item.PromoHistory.Rank.RankName;

                    worksheet.Cells[2, 1] = mygrid.Columns[0].Header.ToString();
                    worksheet.Cells[2, 2] = mygrid.Columns[1].Header.ToString();
                    worksheet.Cells[2, 3] = mygrid.Columns[2].Header.ToString();
                    //worksheet.Cells[2, 4] = mygrid.Columns[3].Header.ToString();

                    //worksheet.Cells[1, 5] = mygrid.Columns[5].Header.ToString();
                    //worksheet.Cells[1, 6] = mygrid.Columns[6].Header.ToString();

                    //worksheet.Cells[1, 8] = mygrid.Columns[7].Header.ToString();



                    worksheet.Cells[rowI, 1] = item.Stage.StageName;
                    worksheet.Cells[rowI, 2] = item.BouncCrntStageDate;
                    worksheet.Cells[rowI, 3] = item.PromoHistory.Emp.EmpNote;

                    //worksheet.Cells[rowI, 4] = item.BouncHistory.Stage.StageName;
                    //worksheet.Cells[rowI, 5] = item.BouncHistory.BouncNextStage;
                    //worksheet.Cells[rowI, 6] = item.BouncHistory.BouncNextStageDate;
                    //worksheet.Cells[rowI, 7] = item.BouncHistory.BouncNextStageDate;
                    //worksheet.Cells[rowI, 4] = item.Emp.EmpNote;

                    rowI += 1;

                }

                workbook.Close();
                application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }
        public static void ExportEntry(DataGrid mygrid)
        {
            try
            {
                var data = (List<Result>)mygrid.ItemsSource;
                Microsoft.Office.Interop.Excel.Application application = null;

                Microsoft.Office.Interop.Excel.Workbook workbook = null;

                Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

                application = new Microsoft.Office.Interop.Excel.Application();
                workbook = application.Workbooks.Add(Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];

                int rowI = 2;
                foreach (var item in data)
                {
                    worksheet.Cells[1, 1] = mygrid.Columns[1].Header.ToString();
                    worksheet.Cells[1, 2] = mygrid.Columns[2].Header.ToString();
                    worksheet.Cells[1, 3] = mygrid.Columns[3].Header.ToString();
                    worksheet.Cells[1, 4] = mygrid.Columns[4].Header.ToString();
                    worksheet.Cells[1, 5] = mygrid.Columns[5].Header.ToString();
                    worksheet.Cells[1, 6] = mygrid.Columns[6].Header.ToString();
                    worksheet.Cells[1, 7] = mygrid.Columns[7].Header.ToString();
                    worksheet.Cells[1, 8] = mygrid.Columns[8].Header.ToString();
                    worksheet.Cells[1, 9] = mygrid.Columns[9].Header.ToString();
                    worksheet.Cells[1, 10] = mygrid.Columns[10].Header.ToString();
                    worksheet.Cells[1, 11] = mygrid.Columns[11].Header.ToString();
                    worksheet.Cells[1, 12] = mygrid.Columns[12].Header.ToString();
                    worksheet.Cells[1, 13] = mygrid.Columns[13].Header.ToString();
                    worksheet.Cells[1, 14] = mygrid.Columns[14].Header.ToString();
                    worksheet.Cells[1, 15] = mygrid.Columns[15].Header.ToString();
                    worksheet.Cells[1, 16] = mygrid.Columns[16].Header.ToString();
                    worksheet.Cells[1, 17] = mygrid.Columns[17].Header.ToString();




                    worksheet.Cells[rowI, 1] = item.Emp.EmpName;
                    worksheet.Cells[rowI, 2] = item.Emp.EmpDateHiring;
                    worksheet.Cells[rowI, 3] = item.Emp.EmpNO;

                    worksheet.Cells[rowI, 4] = item.PromoHistory.Rank.RankName;
                    worksheet.Cells[rowI, 5] = item.PromoHistory.PromoCrntRankDate;
                    worksheet.Cells[rowI, 6] = item.BouncHistory.Stage.StageName;
                    worksheet.Cells[rowI, 7] = item.BouncHistory.BouncCrntStageDate;
                    worksheet.Cells[rowI, 8] = item.PromoHistory.PromoCrntCareer;
                    worksheet.Cells[rowI, 9] = item.Emp.PrivilageYear.PrivilageYearName;
                    worksheet.Cells[rowI, 10] = item.Emp.PrivilageMonth.PrivilageMonthName;
                    worksheet.Cells[rowI, 11] = item.Emp.Penalty.PenaltyName;
                    worksheet.Cells[rowI, 12] = item.PromoHistory.PromoNextRank;
                    worksheet.Cells[rowI, 13] = item.PromoHistory.PromoNextRankDate;
                    worksheet.Cells[rowI, 14] = item.BouncHistory.BouncNextStage;
                    worksheet.Cells[rowI, 15] = item.BouncHistory.BouncNextStageDate;
                    worksheet.Cells[rowI, 16] = item.PromoHistory.PromoNextCareer;
                    worksheet.Cells[rowI, 17] = item.Emp.EmpNote;




                    rowI += 1;

                }

                workbook.Close();
                application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }


    }
}
