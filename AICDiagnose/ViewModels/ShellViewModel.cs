using AIC.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace AICDiagnose.ViewModels
{
    public class ShellViewModel : BindableBase
    {
      
        public ShellViewModel()
        {
                       
        }

        private string title = LocalSetting.Title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private ICommand settingCommand;
        public ICommand SettingCommand
        {
            get
            {
                return this.settingCommand ?? (this.settingCommand = new DelegateCommand(() => this.Setting()));
            }
        }

        private void Setting()
        {
#if XBAP
            MessageBox.Show("测试快捷键！！！");
#else
            Xceed.Wpf.Toolkit.MessageBox.Show("测试快捷键！！！");
#endif
        }
    }
}
