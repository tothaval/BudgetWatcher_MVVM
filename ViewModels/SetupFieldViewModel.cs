/*  BudgetWatcher (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  SetupFieldViewModel  : BaseViewModel
 * 
 *  viewmodel for SetupField component
 */
using BudgetWatcher.ViewModels.ViewLess;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows;
using BudgetWatcher.Commands;

namespace BudgetWatcher.ViewModels
{
    public class SetupFieldViewModel : BaseViewModel
    {

        // properties & fields
        #region properties

        private Brush _background;
        public Brush Background
        {
            get { return _background; }
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
            }
        }


        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;

                Background = new SolidColorBrush(BackgroundColor);
                Application.Current.Resources["BackgroundBrush"] = Background;

                OnPropertyChanged(nameof(BackgroundColor));
            }
        }


        private double _ButtonCornerRadius;
        public double ButtonCornerRadius
        {
            get { return _ButtonCornerRadius; }
            set
            {
                _ButtonCornerRadius = value;
                Application.Current.Resources["Button_CornerRadius"] = new CornerRadius(_ButtonCornerRadius);
                OnPropertyChanged(nameof(ButtonCornerRadius));
            }
        }


        private Brush _ExpenseBrush;
        public Brush ExpenseBrush
        {
            get { return _ExpenseBrush; }
            set
            {
                _ExpenseBrush = value;
                OnPropertyChanged(nameof(ExpenseBrush));
            }
        }


        private Color _ExpenseColor;
        public Color ExpenseColor
        {
            get { return _ExpenseColor; }
            set
            {
                _ExpenseColor = value;

                ExpenseBrush = new SolidColorBrush(ExpenseColor);
                Application.Current.Resources["ExpenseBrush"] = ExpenseBrush;

                GainExpenseColorChange?.Invoke(this, EventArgs.Empty);

                OnPropertyChanged(nameof(ExpenseColor));
            }
        }


        private FontFamily _fontFamiliy;
        public FontFamily FontFamily
        {
            get { return _fontFamiliy; }
            set
            {
                _fontFamiliy = value;

                Application.Current.Resources["FF"] = FontFamily;
                OnPropertyChanged(nameof(FontFamily));
            }
        }


        private double _fontSize;
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;

                OnPropertyChanged(nameof(FontSize));
            }
        }


        private Brush _foreground;
        public Brush Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                OnPropertyChanged(nameof(Foreground));
            }
        }


        private Color _foregroundColor;
        public Color ForegroundColor
        {
            get { return _foregroundColor; }
            set
            {
                _foregroundColor = value;

                Foreground = new SolidColorBrush(ForegroundColor);
                Application.Current.Resources["TextBrush"] = Foreground;

                OnPropertyChanged(nameof(ForegroundColor));
            }
        }


        private Brush _GainBrush;
        public Brush GainBrush
        {
            get { return _GainBrush; }
            set
            {
                _GainBrush = value;
                OnPropertyChanged(nameof(GainBrush));
            }
        }


        private Color _GainColor;
        public Color GainColor
        {
            get { return _GainColor; }
            set
            {
                _GainColor = value;

                GainBrush = new SolidColorBrush(GainColor);
                Application.Current.Resources["GainBrush"] = GainBrush;
                GainExpenseColorChange?.Invoke(this, EventArgs.Empty);

                OnPropertyChanged(nameof(GainColor));
            }
        }


        private Brush _headerText;
        public Brush HeaderText
        {
            get { return _headerText; }
            set
            {
                _headerText = value;
                OnPropertyChanged(nameof(Foreground));
            }
        }


        private Color _headerTextColor;
        public Color HeaderTextColor
        {
            get { return _headerTextColor; }
            set
            {
                _headerTextColor = value;

                HeaderText = new SolidColorBrush(HeaderTextColor);
                Application.Current.Resources["HeaderBrush"] = HeaderText;

                OnPropertyChanged(nameof(HeaderTextColor));
            }
        }


        private string _Language;
        public string Language
        {
            get { return _Language; }
            set
            {
                _Language = value;

                Application.Current.Resources["Language"] = _Language;

                OnPropertyChanged(nameof(Language));
            }
        }


        private Brush _selection;
        public Brush Selection
        {
            get { return _selection; }
            set
            {
                _selection = value;
                OnPropertyChanged(nameof(Selection));
            }
        }


        private Color _selectionColor;
        public Color SelectionColor
        {
            get { return _selectionColor; }
            set
            {
                _selectionColor = value;

                Selection = new SolidColorBrush(SelectionColor);
                Application.Current.Resources["SelectionBrush"] = Selection;

                OnPropertyChanged(nameof(SelectionColor));
            }
        }


        private CultureInfo _SelectedCulture;
        public CultureInfo SelectedCulture
        {
            get { return _SelectedCulture; }
            set
            {
                _SelectedCulture = value;

                Application.Current.Resources["Culture"] = XmlLanguage.GetLanguage(_SelectedCulture.IetfLanguageTag);

                OnPropertyChanged(nameof(SelectedCulture));
            }
        }


        private string _SelectedLanguage;
        public string SelectedLanguage
        {
            get { return _SelectedLanguage; }
            set
            {
                _SelectedLanguage = value;
                Language = value;


                //new LanguageResources(_SelectedLanguage);

                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }


        private double _VisibilityFieldCornerRadius;
        public double VisibilityFieldCornerRadius
        {
            get { return _VisibilityFieldCornerRadius; }
            set
            {
                _VisibilityFieldCornerRadius = value;
                Application.Current.Resources["VisibilityField_CornerRadius"] = new CornerRadius(_VisibilityFieldCornerRadius);

                if (_VisibilityFieldCornerRadius < 40)
                {
                    Application.Current.Resources["VisibilityFieldBorderPadding"] = new Thickness(10);
                }
                else
                {
                    Application.Current.Resources["VisibilityFieldBorderPadding"] = new Thickness(_VisibilityFieldCornerRadius / 4);
                }

                OnPropertyChanged(nameof(VisibilityFieldCornerRadius));
            }
        }

        #endregion properties


        // Event Properties
        #region Event Properties

        public EventHandler GainExpenseColorChange;

        #endregion


        // collections
        #region collections

        private ObservableCollection<CultureInfo> _Currency;
        public ObservableCollection<CultureInfo> Currency
        {
            get { return _Currency; }
            set
            {
                _Currency = value;

                OnPropertyChanged(nameof(Currency));
            }
        }


        private ObservableCollection<string> _Languages;
        public ObservableCollection<string> Languages
        {
            get { return _Languages; }
            set
            {
                _Languages = value;
                OnPropertyChanged(nameof(Languages));
            }
        }

        #endregion collections


        // commands
        #region commands

        public ICommand ApplyFontSizeCommand { get; }

        #endregion commands


        // constructors
        #region constructors

        public SetupFieldViewModel()
        {
            FontSize = (double)Application.Current.Resources["FS"];
            FontFamily = (FontFamily)Application.Current.Resources["FF"];

            BackgroundColor = ((SolidColorBrush)Application.Current.Resources["BackgroundBrush"]).Color;
            ForegroundColor = ((SolidColorBrush)Application.Current.Resources["TextBrush"]).Color;
            HeaderTextColor = ((SolidColorBrush)Application.Current.Resources["HeaderBrush"]).Color;
            SelectionColor = ((SolidColorBrush)Application.Current.Resources["SelectionBrush"]).Color;
            GainColor = ((SolidColorBrush)Application.Current.Resources["GainBrush"]).Color;
            ExpenseColor = ((SolidColorBrush)Application.Current.Resources["ExpenseBrush"]).Color;

            ButtonCornerRadius = ((CornerRadius)Application.Current.Resources["Button_CornerRadius"]).TopLeft;

            VisibilityFieldCornerRadius = ((CornerRadius)Application.Current.Resources["VisibilityField_CornerRadius"]).TopLeft;

            //Languages = new LanguageResources().LoadLanguages();

            OnPropertyChanged(nameof(Languages));


            if (Application.Current.Resources["Language"] != null)
            {
                SelectedLanguage = Application.Current.Resources["Language"].ToString();
            }
            else
            {
                SelectedLanguage = "English.xml";
            }


            Currency = new ObservableCollection<CultureInfo>(CultureInfo.GetCultures(CultureTypes.SpecificCultures).ToList());

            ApplyFontSizeCommand = new RelayCommand((s) => ApplyFontSize(s), (s) => true);

        }

        #endregion constructors


        // methods
        #region methods
        private void ApplyFontSize(object s)
        {
            Application.Current.Resources["FS"] = FontSize;
            Application.Current.Resources["HFS"] = FontSize * 1.25;
        }

        #endregion methods


    }
}
// EOF