using AIC.PDAPage.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIC.PDAPage.ViewModels
{
    public class CardCopyViewModel : BindableBase
    {
        private ObservableCollection<CardParaCopyModel> paras;
        public ObservableCollection<CardParaCopyModel> Paras
        {
            get { return paras; }
            set
            {
                paras = value;
                OnPropertyChanged("Paras");
            }
        }

        public DelegateCommand<object> testCommand;
        public DelegateCommand<object> TestCommand
        {
            get
            {
                if (testCommand == null)
                {
                    testCommand = new DelegateCommand<object>(
                        para => test(para)
                        );
                }
                return testCommand;
            }
        }

        public CardCopyViewModel(List<CardParaCopyModel> parameters)
        {
            Paras = new ObservableCollection<CardParaCopyModel>(parameters);
        }

        private void test(object value)
        {
            var p = Paras;
        }
    }
}
