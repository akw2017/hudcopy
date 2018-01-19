using AIC.Core.Models;
using AIC.ServiceInterface;
using MahApps.Metro.Controls;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Deployment.Application;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AIC.HomePage.Views
{
    /// <summary>
    /// Interaction logic for LoginWin.xaml
    /// </summary>
    public partial class ShowAbnormalWin : MetroWindow
    {
        private ObservableCollection<ExceptionModel> exceptionModelCollection;
        public IEnumerable<ExceptionModel> ExceptionModels { get { return exceptionModelCollection; } }

        public ShowAbnormalWin(ObservableCollection<ExceptionModel> list)
        {
            exceptionModelCollection = list;
            InitializeComponent();
            this.DataContext = this;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private static ILoginUserService _loginUserService;
        private void ClearExceptionButton_Click(object sender, RoutedEventArgs e)
        {
            _loginUserService = ServiceLocator.Current.GetInstance<ILoginUserService>();
            _loginUserService.ClearException();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var model = e.AddedItems[0] as ExceptionModel;
                if (model != null)
                {
                    var inlines = new List<Inline>();
                    Inline inline = new Bold(new Run(model.ExceptionType));
                    inline.FontSize = 14;
                    inlines.Add(inline);

                    AddProperty(inlines, "Message", model.Message);
                    AddProperty(inlines, "Stack Trace", model.StackTrace);
                    AddProperty(inlines, "Data", model.Data);
                    AddProperty(inlines, "TargetSite", model.TargetSite);
                    AddProperty(inlines, "Source", model.Source);

                    var doc = new FlowDocument();

                    doc.FontSize = 12.0;
                    doc.FontFamily = new FontFamily("Microsoft YaHei");
                    doc.TextAlignment = TextAlignment.Left;
                    doc.Background = docViewer.Background;

                    var para = new Paragraph();
                    para.Inlines.AddRange(inlines);
                    doc.Blocks.Add(para);
                    docViewer.Document = doc;
                }
            }
        }

        private void AddProperty(List<Inline> inlines, string propName, string propVal)
        {
            inlines.Add(new LineBreak());
            inlines.Add(new LineBreak());
            var inline = new Bold(new Run(propName + ":"));
            inline.FontSize = 13;
            inlines.Add(inline);
            inlines.Add(new LineBreak());
            // Might have embedded newlines.
            AddLines(inlines, propVal as string);
        }

        void AddLines(List<Inline> inlines, string str)
        {
            string[] lines = str.Split('\n');

            inlines.Add(new Run(lines[0].Trim('\r')));

            foreach (string line in lines.Skip(1))
            {
                inlines.Add(new LineBreak());
                inlines.Add(new Run(line.Trim('\r')));
            }
        }
    }
}
