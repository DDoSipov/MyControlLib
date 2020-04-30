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

using System.ComponentModel;

namespace MyControlLib.RatingControl
{
    public delegate void ValueUpdatedHandler(double value);

    /// <summary>
    /// Mouse Control View
    /// </summary>
    public partial class MC_View : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty
            .Register("Value", typeof(double), typeof(MC_View),
                    new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }


        public static readonly DependencyProperty DebugProperty = DependencyProperty
            .Register("Debug", typeof(bool), typeof(MC_View),
                    new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public bool Debug
        {
            get { return (bool)GetValue(DebugProperty); }
            set { SetValue(DebugProperty, value); }
        }

        public static readonly DependencyProperty RatingCollectionProperty = DependencyProperty
            .Register("RatingCollection", typeof(RatingCollection.RC_VM), typeof(MC_View),
                    new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public RatingCollection.RC_VM RatingCollection
        {
            get { return (RatingCollection.RC_VM)GetValue(DebugProperty); }
            set { SetValue(RatingCollectionProperty, value); }
        }

        //public static readonly DependencyProperty CmdProp = DependencyProperty.Register
        //    ("Cmd", typeof(ICommand), typeof(MC_View), new UIPropertyMetadata(null));
        //public ICommand Cmd
        //{
        //    get { return (ICommand)GetValue(CmdProp); }
        //    set { SetValue(CmdProp, value); }
        //}


        private readonly MouseControl.MC_VM MC_VM;
        public MC_View()
        {
            InitializeComponent();
            MC_VM = new MouseControl.MC_VM(5.0);


            //List<RatingElement.RE_Model> models = new List<RatingElement.RE_Model>
            //{
            //    new RatingElement.RE_Model(new RatingElement.String_Type.RE_String_Model()),
            //    new RatingElement.RE_Model(new RatingElement.String_Type.RE_String_Model()),
            //    new RatingElement.RE_Model(new RatingElement.Drawing_Type.RE_Drawing_Model(RatingElement.Drawing_Type.RE_Type.Star)),
            //    new RatingElement.RE_Model(new RatingElement.Drawing_Type.RE_Drawing_Model(RatingElement.Drawing_Type.RE_Type.Square)),
            //    new RatingElement.RE_Model(new RatingElement.String_Type.RE_String_Model())
            //};

            List<RatingElement.RE_Model> models = new List<RatingElement.RE_Model>
            {
                new RatingElement.RE_Model(new RatingElement.Drawing_Type.RE_Drawing_Model(RatingElement.Drawing_Type.RE_Type.Star)),
                new RatingElement.RE_Model(new RatingElement.Drawing_Type.RE_Drawing_Model(RatingElement.Drawing_Type.RE_Type.Star)),
                new RatingElement.RE_Model(new RatingElement.Drawing_Type.RE_Drawing_Model(RatingElement.Drawing_Type.RE_Type.Star)),
                new RatingElement.RE_Model(new RatingElement.Drawing_Type.RE_Drawing_Model(RatingElement.Drawing_Type.RE_Type.Star)),
                new RatingElement.RE_Model(new RatingElement.Drawing_Type.RE_Drawing_Model(RatingElement.Drawing_Type.RE_Type.Star)),
            };
            MC_VM.RC_VM = new RatingCollection.RC_VM(new RatingCollection.RC_Model(models));

            DataContext = MC_VM;

            Binding valueBinding = new Binding
            {
                Source = MC_VM,
                Path = new PropertyPath("Value"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            SetBinding(ValueProperty, valueBinding);

            Binding debugBinding = new Binding
            {
                Source = MC_VM,
                Path = new PropertyPath("Debug"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            SetBinding(DebugProperty, debugBinding);
            
            Binding rcBinding = new Binding
            {
                Source = MC_VM,
                Path = new PropertyPath("RC_VM"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            SetBinding(RatingCollectionProperty, rcBinding);
        }



        private void Spek_MouseMove(object sender, MouseEventArgs e)
        {
            Point EndPoint = e.GetPosition(spek);
            double lPart = EndPoint.X / spek.RenderSize.Width;

            MC_VM.EditValue = lPart;
        }

        private void Spek_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MC_VM.AllowSave();
        }
        private void Spek_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MC_VM.FinishEdit();
        }

        private void Spek_MouseEnter(object sender, MouseEventArgs e)
        {
            MC_VM.StartEdit();
        }
        private void Spek_MouseLeave(object sender, MouseEventArgs e)
        {
            MC_VM.FinishEdit();
        }
    }


    public class VisibilityConverter : IValueConverter
    {
        /// <summary>
        /// The data binding engine calls this method when it propagates a value from the binding source to the binding target.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == false)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }
    public class FillConverter : IValueConverter
    {
        /// <summary>
        /// The data binding engine calls this method when it propagates a value from the binding source to the binding target.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == false)
                    return Brushes.Red;
                else
                    return Brushes.Black;
            }
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
    }
}
