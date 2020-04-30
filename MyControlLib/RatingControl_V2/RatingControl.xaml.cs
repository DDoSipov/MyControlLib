using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyControlLib.RatingControl_V2
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RatingControl : UserControl
    {
        public RatingControl()
        {
            RE_Coll = new RE_VM[]
            {
                new RE_VM() {Debug = Debug },
                new RE_VM() {Debug = Debug },
                new RE_VM() {Debug = Debug },
                new RE_VM() {Debug = Debug },
                new RE_VM() {Debug = Debug }
            };
            

            InitializeComponent();
            LayoutRoot.DataContext = this;
        }


        #region DPs
        public static readonly DependencyProperty Debug_Property = DependencyProperty.Register
        (
            nameof(Debug),
            typeof(bool),
            typeof(RatingControl),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(Debug_Property_CB))
        );

        public static readonly DependencyProperty RE_Coll_Property = DependencyProperty.Register
        (
            nameof(RE_Coll),
            typeof(RE_VM[]),
            typeof(RatingControl),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(RE_Coll_Property_CB))
        );

        public static readonly DependencyProperty MaxValue_Property = DependencyProperty.Register
        (
            nameof(MaxValue),
            typeof(double),
            typeof(RatingControl),
            new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(Update_View_CB))
        );



        public static readonly DependencyProperty Value_Property = DependencyProperty.Register
        (
            nameof(Value),
            typeof(double),
            typeof(RatingControl),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(Value_Property_CB))
        );

        public static readonly DependencyProperty DisplayValue_Property = DependencyProperty.Register
        (
            nameof(DisplayValue),
            typeof(double),
            typeof(RatingControl),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(Update_View_CB))
        );



        public static readonly DependencyProperty CanSave_Property = DependencyProperty.Register
        (
            nameof(CanSave),
            typeof(bool),
            typeof(RatingControl),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );

        public static readonly DependencyProperty IsEditing_Property = DependencyProperty.Register
        (
            nameof(IsEditing),
            typeof(bool),
            typeof(RatingControl),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );
        #endregion


        #region Props
        public bool Debug
        {
            get { return (bool)GetValue(Debug_Property); }
            set { SetValue(Debug_Property, value); }
        }

        public RE_VM[] RE_Coll
        {
            get { return (RE_VM[])GetValue(RE_Coll_Property); }
            set { SetValue(RE_Coll_Property, value); }
        }

        public double MaxValue
        {
            get { return (double)GetValue(MaxValue_Property); }
            set { SetValue(MaxValue_Property, value); }
        }



        public double Value
        {
            get { return (double)GetValue(Value_Property); }
            set { SetValue(Value_Property, value); }
        }

        public double DisplayValue
        {
            get { return (double)GetValue(DisplayValue_Property); }
            set { SetValue(DisplayValue_Property, value); }
        }
        


        public bool CanSave
        {
            get { return (bool)GetValue(CanSave_Property); }
            set { SetValue(CanSave_Property, value); }
        }

        public bool IsEditing
        {
            get { return (bool)GetValue(IsEditing_Property); }
            set { SetValue(IsEditing_Property, value); }
        }
        #endregion



        private static void Debug_Property_CB(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var RC = (RatingControl)obj;
            foreach (var p in RC.RE_Coll)
                p.Debug = (bool)e.NewValue;
        }

        private static void RE_Coll_Property_CB(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var RC = (RatingControl)obj;
            RC.RE_Coll = (RE_VM[])e.NewValue;

            foreach (var p in RC.RE_Coll)
                p.Debug = RC.Debug;

            RC.UpdateView();
        }

        private static void Value_Property_CB(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var RC = (RatingControl)obj;
            if (!RC.IsEditing)
                RC.DisplayValue = (double)e.NewValue;
        }

        private static void Update_View_CB(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ((RatingControl)obj).UpdateView();
        }


        private void UpdateView()
        {
            int count = RE_Coll.Count();
            double Step = MaxValue / count;
            double ival = 0;


            for (int i = 0; i < count; i++)
            {
                if (ival + Step <= DisplayValue)
                    RE_Coll[i].Value = 1;
                else
                {
                    RE_Coll[i].Value = (DisplayValue - ival) / Step;
                    for (int j = i + 1; j < count; j++)
                        RE_Coll[j].Value = 0;
                    break;
                }

                ival += Step;
            }
        }


        private void FinishEdit()
        {
            IsEditing = false;
            CanSave = false;

            DisplayValue = Value;
        }


        #region Events
        /// <summary>
        /// Рассчитывается lPart - доля рейтинга слева от мышки.
        ///     В идеале она от 0 до 1 (double), для удобства выделения (чтобы совсем на края не жать) - она может быть чуть меньше и чуть больше
        ///     Т.к. Presenter - сам рейтинг, по которому считается размер, расположен внутри LayoutRoot, вызывающего ивент.
        ///     Следовательно потом загоняем в границы 0..1
        ///     
        /// После этого считаем value
        /// 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Spek_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsEditing)
            {
                Point EndPoint = e.GetPosition(Presenteer);
                double lPart = EndPoint.X / Presenteer.RenderSize.Width;

                if (lPart < 0)
                    lPart = 0;
                if (lPart > 1)
                    lPart = 1;



                double value = lPart * MaxValue;

                DisplayValue = value;
                if (CanSave)
                    Value = value;
            }
        }

        private void Spek_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CanSave = true;
            Value = DisplayValue;
        }
        private void Spek_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FinishEdit();
        }

        private void Spek_MouseEnter(object sender, MouseEventArgs e)
        {
            IsEditing = true;
        }
        private void Spek_MouseLeave(object sender, MouseEventArgs e)
        {
            FinishEdit();
        }
        #endregion
    }
}
