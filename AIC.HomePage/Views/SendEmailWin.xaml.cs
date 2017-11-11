using AIC.Core.Models;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    public partial class SendEmailWin : MetroWindow
    {        
        public SendEmailWin()
        {            
            InitializeComponent();            
        }

        private async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(
                    txtEmail.Text, txtName.Text, Encoding.UTF8);
                mail.To.Add("80267720@qq.com");
                mail.Subject = txtSubject.Text;
                mail.SubjectEncoding = Encoding.Default;
                mail.Body = txtContent.Text;
                mail.BodyEncoding = Encoding.Default;
                mail.IsBodyHtml = false;
                mail.Priority = MailPriority.Normal;
                //添加附件
                Attachment attachment = null;
                if (txtAccessory.Items.Count > 0)
                {
                    for (int i = 0; i < txtAccessory.Items.Count; i++)
                    {
                        string pathFileName = txtAccessory.Items[i].ToString();
                        string extName = System.IO.Path.GetFullPath(pathFileName).ToLower();
                        //判断附件类型
                        if (extName == ".rar" || extName == ".zip")
                        {
                            attachment = new Attachment(pathFileName, MediaTypeNames.Application.Zip);
                        }
                        else
                        {
                            attachment = new Attachment(pathFileName, MediaTypeNames.Application.Octet);
                        }
                        ContentDisposition cd = attachment.ContentDisposition;
                        cd.CreationDate = System.IO.File.GetCreationTime(pathFileName);
                        cd.ModificationDate = System.IO.File.GetLastWriteTime(pathFileName);
                        cd.ReadDate = System.IO.File.GetLastAccessTime(pathFileName);
                        mail.Attachments.Add(attachment);

                    }
                }
                SmtpClient client = new SmtpClient();
                if (txtEmail.Text.Contains("qq.com"))
                {
                    client.Host = "smtp.qq.com";
                }
                else if (txtEmail.Text.Contains("126.com"))
                {
                    client.Host = "smtp.126.com";
                }
                else if (txtEmail.Text.Contains("163.com"))
                {
                    client.Host = "smtp.163.com";
                }
                else if (txtEmail.Text.Contains("sina.com"))
                {
                    client.Host = "smtp.sina.com";
                }
                else if (txtEmail.Text.Contains("china.com"))
                {
                    client.Host = "smtp.china.com";
                }
                else if (txtEmail.Text.Contains("sohu.com"))
                {
                    client.Host = "smtp.sohu.com";
                }
                else if (txtEmail.Text.Contains("foxmail.com"))
                {
                    client.Host = "smtp.foxmail.com";
                }
                else
                {
#if XBAP
                    MessageBox.Show("暂不支持该邮箱", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("暂不支持该邮箱", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                }
                client.Port = 25;
                //是否使用安全套接字层加密连接
                client.EnableSsl = true;
                //不使用默认凭证，注意此句必须放在 client.Credentials 的上面
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(txtEmail.Text, txtPwd.Password);
                //邮件通过网络直接发送到服务器
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                try
                {
                    var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                    var token = cts.Token;
                    //await Task.Delay(TimeSpan.FromSeconds(20), token);测试超时用
                    await Task.Run(() =>
                    {
                        client.Send(mail);
                    }, token);
#if XBAP
                    MessageBox.Show("发送成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);     
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("发送成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
#endif
                }
                catch (SmtpException ex)
                {
#if XBAP
                    MessageBox.Show("发送失败：" + ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("发送失败：" + ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                }
                catch (Exception ex)
                {
#if XBAP
                    MessageBox.Show("发送失败：" + ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                    Xceed.Wpf.Toolkit.MessageBox.Show("发送失败：" + ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif
                }
                finally
                {
                    mail.Dispose();
                    client = null;
                    this.Cursor = Cursors.Arrow;
                }
            }
            catch (Exception ex)
            {
#if XBAP
                MessageBox.Show("信息填写有误：" + ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#else
                Xceed.Wpf.Toolkit.MessageBox.Show("信息填写有误：" + ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
#endif

                this.Cursor = Cursors.Arrow;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {           
            this.Close();         
        }       

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog odlg = new OpenFileDialog();
            odlg.CheckFileExists = true;
            //只接收有效的文件名
            odlg.ValidateNames = true;
            //允许一次选择多个文件作为附件
            odlg.Multiselect = true;
            if (odlg.ShowDialog() == true)
            {
                foreach (var file in odlg.FileNames)
                {
                    txtAccessory.Items.Add(file);
                }
            }
        }
    }
}
