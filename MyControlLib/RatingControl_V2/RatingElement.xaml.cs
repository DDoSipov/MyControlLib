using System.Windows;
using System.Windows.Controls;

namespace MyControlLib.RatingControl_V2
{
    /// <summary>
    /// Interaction logic for RatingElement.xaml
    /// </summary>
    public partial class RatingElement : UserControl
    {
        public RatingElement()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        #region DPs
        public static readonly DependencyProperty Data_Property = DependencyProperty.Register
        (
            "Data",
            typeof(RE_Type),
            typeof(RatingElement),
            new FrameworkPropertyMetadata(RE_Type.Star_5)
        );

        public static readonly DependencyProperty Value_Property = DependencyProperty.Register
        (
            "Value",
            typeof(double),
            typeof(RatingElement),
            new FrameworkPropertyMetadata(0.0)
        );
        #endregion


        #region Props
        public RE_Type Data
        {
            get { return (RE_Type)GetValue(Data_Property); }
            set { SetValue(Data_Property, value); }
        }

        public double Value
        {
            get { return (double)GetValue(Value_Property); }
            set { SetValue(Value_Property, value); }
        }
        #endregion
    }
}
