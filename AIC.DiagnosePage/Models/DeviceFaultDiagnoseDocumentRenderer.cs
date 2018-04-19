using AIC.Core.DiagnosticBaseModels;
using AIC.Core.FlowDoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace AIC.DiagnosePage.Models
{
    class DeviceFaultDiagnoseDocumentRenderer : IDocumentRenderer
    {
        public void Render(FlowDocument doc, object data)
        {
            TableRowGroup groupDetails = doc.FindName("rowsDetails") as TableRowGroup;
            //TableRowGroup groupOverview = doc.FindName("rowsOverview") as TableRowGroup;

            Style styleCell = doc.Resources["BorderedCell"] as Style;
            foreach (DiagnoseResult item in (DiagnoseResult[])data)
            {
                TableRow row = new TableRow();

                TableCell cell = new TableCell(new Paragraph(new Run(item.Name)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.Result)));
                cell.Style = styleCell;
                row.Cells.Add(cell);

                cell = new TableCell(new Paragraph(new Run(item.DescriptionString)));
                cell.Style = styleCell;
                row.Cells.Add(cell);              

                groupDetails.Rows.Add(row);
            }
        }
    }
}
