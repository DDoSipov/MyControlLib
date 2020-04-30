using System.Windows;
using System.Windows.Controls;

namespace MyControlLib
{
    public partial class MyLabeledTextBox : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty
            .Register("Label",
                    typeof(string),
                    typeof(MyLabeledTextBox),
                    new FrameworkPropertyMetadata("LTB"));

        public static readonly DependencyProperty TextProperty = DependencyProperty
            .Register("Text",
                    typeof(string),
                    typeof(MyLabeledTextBox),
                    new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty LabelWidthProperty = DependencyProperty
            .Register("LabelWidth",
                    typeof(string),
                    typeof(MyLabeledTextBox),
                    new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




        
        public MyLabeledTextBox()
        {
            InitializeComponent();
            Root.DataContext = this;
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string LabelWidth
        {
            get { return (string)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }
    }
}
