using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RTFTDIF.Metro.Controls
{
    /// <summary>
    /// Interaction logic for CategoryItemControl.xaml
    /// </summary>
    public partial class CategoryItemControl : UserControl
    {
        #region Constructor
        public CategoryItemControl()
        {
            InitializeComponent();
        }
        #endregion


        #region Dependancy Properties
        
        public String Id
        {
            get { return (String)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register(nameof(Id), typeof(String), typeof(CategoryItemControl), new PropertyMetadata("-1"));

        #endregion

        #region Events

        public delegate void ClickHandler(String categoryId);
        public event ClickHandler Click;

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if(Click != null)
            {
                Click(Id);
            }
        }

        #endregion
        //public static readonly RoutedEvent CategorySelectedEvent = EventManager.RegisterRoutedEvent(nameof(CategorySelectedEventHandler),
        //    RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CategoryItemControl));

        //public event RoutedEventHandler CategorySelectedEventHandler
        //{
        //    add { AddHandler(CategorySelectedEvent, value); }
        //    remove { RemoveHandler(CategorySelectedEvent, value); }
        //}

        //void RaiseCategorySelectedEvent()
        //{
        //    RoutedEventArgs eventArgs = new RoutedEventArgs(CategoryItemControl.CategorySelectedEvent);
        //    RaiseEvent(eventArgs);
        //}
    }
}
