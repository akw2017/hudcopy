using AIC.DeviceDataPage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace AIC.DeviceDataPage.Views
{
    /// <summary>
    /// PrintPreviewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPreviewWindow : Window
    {
        public PrintPreviewWindow()
        {
            InitializeComponent();
        }

        private delegate void LoadXpsMethod();
        private readonly FlowDocument m_doc;
        private readonly List<FlowDocument> m_doclist;


        public static FlowDocument LoadDocumentAndRender(string strTmplName, Object data, IDocumentRenderer renderer = null)
        {
            FlowDocument doc = (FlowDocument)Application.LoadComponent(new Uri(strTmplName, UriKind.RelativeOrAbsolute));
            doc.PagePadding = new Thickness(50);
            doc.DataContext = data;
            if (renderer != null)
            {
                renderer.Render(doc, data);
                DocumentPaginator paginator = ((IDocumentPaginatorSource)doc).DocumentPaginator;
                paginator.PageSize = new Size(1188, 840);
                //doc.PagePadding = new Thickness(50, 50, 50, 50);
                doc.ColumnWidth = double.PositiveInfinity;
            }
            return doc;
        }


        public PrintPreviewWindow(string strTmplName, Object data, IDocumentRenderer renderer = null)
        {
            InitializeComponent();
            if (data is List<PrintResult>)
            {
                List<PrintResult> resultList = data as List<PrintResult>;
                m_doclist = new List<FlowDocument>();
                foreach (var result in resultList)
                {
                    m_doclist.Add(LoadDocumentAndRender(strTmplName, result, renderer));
                }
                Dispatcher.BeginInvoke(new LoadXpsMethod(LoadManyXps), DispatcherPriority.ApplicationIdle);
            }
            else
            {
                m_doc = LoadDocumentAndRender(strTmplName, data, renderer);
                Dispatcher.BeginInvoke(new LoadXpsMethod(LoadXps), DispatcherPriority.ApplicationIdle);
            }
           
        }

        public void LoadXps()
        {
            //构造一个基于内存的xps document
            MemoryStream ms = new MemoryStream();
            Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
            Uri DocumentUri = new Uri("pack://InMemoryDocument.xps");
            PackageStore.RemovePackage(DocumentUri);
            PackageStore.AddPackage(DocumentUri, package);
            XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, DocumentUri.AbsoluteUri);

            //将flow document写入基于内存的xps document中去
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(((IDocumentPaginatorSource)m_doc).DocumentPaginator);

            //获取这个基于内存的xps document的fixed document
            docViewer.Document = xpsDocument.GetFixedDocumentSequence();

            //关闭基于内存的xps document
            xpsDocument.Close();
        }

        public void LoadManyXps()
        {
            if (m_doclist == null || m_doclist.Count == 0)
            {
                return;
            }
            //------------------定义新文档的结构
            FixedDocumentSequence newFds = new FixedDocumentSequence();//创建一个新的文档

            for(int i = 0; i < m_doclist.Count; i++)
            {
                //构造一个基于内存的xps document
                MemoryStream ms = new MemoryStream();
                Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
                Uri DocumentUri = new Uri("pack://InMemoryDocument"+ i.ToString() + ".xps");
                PackageStore.RemovePackage(DocumentUri);
                PackageStore.AddPackage(DocumentUri, package);
                XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.Fast, DocumentUri.AbsoluteUri);

                //将flow document写入基于内存的xps document中去
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                writer.Write(((IDocumentPaginatorSource)m_doclist[i]).DocumentPaginator);

                DocumentReference newDocRef = AddPage(xpsDocument);//加入第一个文件
                newFds.References.Add(newDocRef);

                //关闭基于内存的xps document
                xpsDocument.Close();
            }            

            string newFile = "xpsShow.xps";
            File.Delete(newFile);
            //xps写入新文件
            XpsDocument NewXpsDocument = new XpsDocument("xpsShow.xps", System.IO.FileAccess.ReadWrite);
            XpsDocumentWriter xpsDocumentWriter = XpsDocument.CreateXpsDocumentWriter(NewXpsDocument);
            xpsDocumentWriter.Write(newFds);

            //获取这个基于内存的xps document的fixed document
            docViewer.Document = NewXpsDocument.GetFixedDocumentSequence();           
            NewXpsDocument.Close();
        }

        public DocumentReference AddPage(XpsDocument xpsDocument)
        {
            DocumentReference newDocRef = new DocumentReference();
            FixedDocument newFd = new FixedDocument();

            FixedDocumentSequence docSeq = xpsDocument.GetFixedDocumentSequence();

            foreach (DocumentReference docRef in docSeq.References)
            {
                FixedDocument fd = docRef.GetDocument(false);

                foreach (PageContent oldPC in fd.Pages)
                {
                    Uri uSource = oldPC.Source;//读取源地址
                    Uri uBase = (oldPC as IUriContext).BaseUri;//读取目标页面地址

                    PageContent newPageContent = new PageContent();
                    newPageContent.GetPageRoot(false);
                    newPageContent.Source = uSource;
                    (newPageContent as IUriContext).BaseUri = uBase;
                    newFd.Pages.Add(newPageContent);//将新文档追加到新的documentRefences中
                }
            }
            newDocRef.SetDocument(newFd);
            xpsDocument.Close();
            return newDocRef;
        }

        public DocumentReference AddPage(string fileName)
        {
            DocumentReference newDocRef = new DocumentReference();
            FixedDocument newFd = new FixedDocument();

            XpsDocument xpsDocument = new XpsDocument(fileName, FileAccess.Read);
            FixedDocumentSequence docSeq = xpsDocument.GetFixedDocumentSequence();

            foreach (DocumentReference docRef in docSeq.References)
            {
                FixedDocument fd = docRef.GetDocument(false);

                foreach (PageContent oldPC in fd.Pages)
                {
                    Uri uSource = oldPC.Source;//读取源地址
                    Uri uBase = (oldPC as IUriContext).BaseUri;//读取目标页面地址

                    PageContent newPageContent = new PageContent();
                    newPageContent.GetPageRoot(false);
                    newPageContent.Source = uSource;
                    (newPageContent as IUriContext).BaseUri = uBase;
                    newFd.Pages.Add(newPageContent);//将新文档追加到新的documentRefences中
                }
            }
            newDocRef.SetDocument(newFd);
            xpsDocument.Close();
            return newDocRef;
        }
    }
}
