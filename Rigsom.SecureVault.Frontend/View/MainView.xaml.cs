using Rigsom.SecureVault.Frontend.Model;
using Rigsom.SecureVault.Frontend.ViewModel;
using Rigsom.SecureVault.Model.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Rigsom.SecureVault.Frontend.View
{
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        public MainView()
        {
            InitializeComponent();

            //TODO: Save path in configuration
            string configurationPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "SecureVault",
                "settings",
                "configuration.dat");

            ConfigurationHelper configHelper = new ConfigurationHelper(configurationPath);

            if (!configHelper.CheckConfiguration())
            {
                ConfigurationPage configPage = new ConfigurationPage();
                configPage.DataContext = new ConfigurationViewModel(new Configuration(), this.MainFrame);

                this.MainFrame.Navigate(configPage);

            }
            else
            {
                PasswordPage passwordPage = new PasswordPage();
                passwordPage.DataContext = new PasswordViewModel(new Vault(), this.MainFrame);

                this.MainFrame.Navigate(passwordPage);
            }
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(500);
            growAnimation.From = -800;
            growAnimation.To = 0;
            storyboard.Children.Add(growAnimation);

            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.X"));
            Storyboard.SetTarget(growAnimation, this.MainFrame);
            
            //Show Main Frame
            this.MainFrame.Visibility = System.Windows.Visibility.Visible;
            
            //Start animation
            storyboard.Begin();
        }

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            //Hide the Main Frame
            this.MainFrame.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
