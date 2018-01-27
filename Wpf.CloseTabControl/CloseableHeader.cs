using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Wpf.CloseTabControl
{
    public class CloseableHeader : BindableBase
    {
        public CloseableHeader(string titleResource, string title, bool canClose, string path= "/AIC.Resources;component/Images/application.png")
        {
            this.TitleResourceName = titleResource;
            this.Title = title;
            this.canClose = canClose;
            this.CloseCommand = new DelegateCommand(Close);
            this.ImagePath = path;
        }

        public event Action RequestClose;

        public ICommand CloseCommand { get; private set; }
        private void Close()
        {
            if (this.RequestClose != null)
            {
                this.RequestClose();
            }
        }

        public string TitleResourceName { get; set; }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public string ImagePath { get; set; }

        private bool canClose;
        public Visibility Visibility
        {
            get { return canClose ? Visibility.Visible : Visibility.Collapsed; }
        }
        private Visibility lockVisibility = Visibility.Collapsed;
        public Visibility LockVisibility
        {
            get { return lockVisibility; }
            set
            {
                lockVisibility = value;
                OnPropertyChanged("LockVisibility");
            }
        }
    }
}
