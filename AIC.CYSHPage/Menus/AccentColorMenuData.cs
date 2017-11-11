using MahApps.Metro;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace AIC.CYSHPage.Menus
{
    public class AccentColorMenuData
    {
        public string Name { get; set; }
        public Brush BorderColorBrush { get; set; }
        public Brush ColorBrush { get; set; }

        private ICommand changeAccentCommand;

        public ICommand ChangeAccentCommand
        {
            get { return this.changeAccentCommand ?? (changeAccentCommand = new DelegateCommand<object>(x => this.DoChangeTheme(x))); }
        }


        protected virtual void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
            DoChangeAddition(theme.Item2.Name,this.Name);
        }       

        private void DoChangeAddition(string oldname, string newname)
        {          
            string requestedStyle = "/AIC.Resources;component/Styles/" + oldname + "Addition.xaml";
            ResourceDictionary resourceDictionary = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source.OriginalString.Equals(@requestedStyle));
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            requestedStyle = "/AIC.Resources;component/Styles/" + newname + "Addition.xaml";
            resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri(@requestedStyle, UriKind.RelativeOrAbsolute);
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
        }
    }
}
