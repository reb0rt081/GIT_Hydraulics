using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using ScienceAndMaths.Client.Modules.Canal;
using ScienceAndMaths.Client.Modules.Canal.Views;
using ScienceAndMaths.Client.Shared;

using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

using Unity;

namespace ScienceAndMaths.Client.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected MainMenuView MainMenu { get; set; }

        public App()
        {
            MainMenu = new MainMenuView();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<CanalModule>();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();

            IRegionManager regionManager = Container.Resolve<IRegionManager>();
            var module = Container.Resolve<CanalModule>();
            
            if(module is ScienceAndMathsModule scienceAndMathsModule)
            {
                var moduleInfo = scienceAndMathsModule.GetModuleInfo();

                Button canalButton = GetModuleMenuButton(regionManager, moduleInfo);

                MainMenu.menuPanel.Children.Add(canalButton);
            }

            regionManager.Regions[Shared.Constants.MainRegion].Add(MainMenu, Shared.Constants.MainMenuView);

            regionManager.RequestNavigate(Shared.Constants.MainRegion, typeof(MainMenuView).Name);
        }

        protected Button GetModuleMenuButton(IRegionManager regionManager, ScienceAndMathsModuleInfo scienceAndMathsModuleInfo)
        {
            Button canalButton = new Button();
            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Vertical
            };

            canalButton.Content = stackPanel;
            stackPanel.Children.Add(new Label()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Content = scienceAndMathsModuleInfo.Name
            });

            stackPanel.Children.Add(new Image() { Width = 200, Height = 200, Source = new BitmapImage(new Uri(scienceAndMathsModuleInfo.ImageUri)) });

            canalButton.HorizontalAlignment = HorizontalAlignment.Stretch;
            canalButton.Style = MainMenu.mainGrid.Resources["MenuButtonStyle"] as Style;
            canalButton.Command = new DelegateCommand(() => regionManager.RequestNavigate(Shared.Constants.MainRegion, scienceAndMathsModuleInfo.MainViewUri));

            return canalButton;
        }

    }
}
