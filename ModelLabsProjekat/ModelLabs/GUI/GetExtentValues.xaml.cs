using FTN.Common;
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
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for GetExtentValues.xaml
    /// </summary>
    public partial class GetExtentValues : Window
    {
        private static MyTestGDA testGda;
        private static ModelResourcesDesc rd;

        private static List<long> extentValuesGIDs;
        private static List<ModelCode> codes;
        private static ModelCode code;
        public static Dictionary<ModelCode, List<ModelCode>> propertyIDsByModelCode;

        public GetExtentValues()
        {
            InitializeComponent();
            extentValuesGIDs = new List<long>();
            testGda = new MyTestGDA();

            propertyIDsByModelCode = new Dictionary<ModelCode, List<ModelCode>>();
           
            codes = new List<ModelCode>();
            codes.Add(ModelCode.ACLINESEGMENT);
            codes.Add(ModelCode.DCLINESEGMENT);
            codes.Add(ModelCode.SERIESCOMPENSATOR);
            codes.Add(ModelCode.CONNECTIVITYNODE);
            codes.Add(ModelCode.TERMINAL);
            codes.Add(ModelCode.PERLENGTHSEQUENCEIMPEDANCE);

            List<string> hexModelCodes = new List<string>();
            foreach (ModelCode mc in codes)
            {
                string hex = mc.ToString() + String.Format(" - 0x{0:x16}\n", (long)mc);
                hexModelCodes.Add(hex);
            }

            ModelCodeComboBox.ItemsSource = hexModelCodes;
            
            rd = new ModelResourcesDesc();

            foreach (ModelCode code in Enum.GetValues(typeof(ModelCode)))
            {
                List<ModelCode> propertyIDs = rd.GetAllPropertyIds(code);
                propertyIDsByModelCode.Add(code, propertyIDs);
            }
        }

        private void ButtonGetValues_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }

        private void ButtonGetExtentValues_Click(object sender, RoutedEventArgs e)
        {
            GetExtentValues window = new GetExtentValues();
            this.Close();
            window.Show();
        }

        private void ButtonGetRelatedValues_Click(object sender, RoutedEventArgs e)
        {
            GetRelatedValues window = new GetRelatedValues();
            this.Close();
            window.Show();
        }

        private void ModelCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            code = codes[ModelCodeComboBox.SelectedIndex];
            CreateCheckBoxes();
        }

        private void CreateCheckBoxes()
        {
            int endIndex = 0;

            GetExtentValuesButton.Visibility = Visibility.Visible;
            SelectAllCheckBox.Visibility = Visibility.Visible;
            SelectAllCheckBox.IsChecked = false;
            for (int i = 0; i < propertyIDsByModelCode[code].Count; i++)
            {
                endIndex = i;
                ModelCode c = propertyIDsByModelCode[code][i];
                switch (i)
                {
                    case 0:
                        Item1CheckBox.Visibility = Visibility.Visible;
                        Item1CheckBox.Content = c.ToString();
                        Item1CheckBox.IsChecked = false;
                        break;
                    case 1:
                        Item2CheckBox.Visibility = Visibility.Visible;
                        Item2CheckBox.Content = c.ToString();
                        Item2CheckBox.IsChecked = false;
                        break;
                    case 2:
                        Item3CheckBox.Visibility = Visibility.Visible;
                        Item3CheckBox.Content = c.ToString();
                        Item3CheckBox.IsChecked = false;
                        break;
                    case 3:
                        Item4CheckBox.Visibility = Visibility.Visible;
                        Item4CheckBox.Content = c.ToString();
                        Item4CheckBox.IsChecked = false;
                        break;
                    case 4:
                        Item5CheckBox.Visibility = Visibility.Visible;
                        Item5CheckBox.Content = c.ToString();
                        Item5CheckBox.IsChecked = false;
                        break;
                    case 5:
                        Item6CheckBox.Visibility = Visibility.Visible;
                        Item6CheckBox.Content = c.ToString();
                        Item6CheckBox.IsChecked = false;
                        break;
                    case 6:
                        Item7CheckBox.Visibility = Visibility.Visible;
                        Item7CheckBox.Content = c.ToString();
                        Item7CheckBox.IsChecked = false;
                        break;
                    case 7:
                        Item8CheckBox.Visibility = Visibility.Visible;
                        Item8CheckBox.Content = c.ToString();
                        Item8CheckBox.IsChecked = false;
                        break;
                    case 8:
                        Item9CheckBox.Visibility = Visibility.Visible;
                        Item9CheckBox.Content = c.ToString();
                        Item9CheckBox.IsChecked = false;
                        break;
                    default:
                        Item1CheckBox.Visibility = Visibility.Visible;
                        Item1CheckBox.IsChecked = false;
                        Item1CheckBox.Content = "ERROR";
                        break;
                }
            }

            for (int i = endIndex; i < 9; i++)
            {
                switch (i)
                {
                    case 0:
                        Item1CheckBox.Visibility = Visibility.Hidden;
                        Item1CheckBox.IsChecked = false;
                        break;
                    case 1:
                        Item2CheckBox.Visibility = Visibility.Hidden;
                        Item2CheckBox.IsChecked = false;
                        break;
                    case 2:
                        Item3CheckBox.Visibility = Visibility.Hidden;
                        Item3CheckBox.IsChecked = false;
                        break;
                    case 3:
                        Item4CheckBox.Visibility = Visibility.Hidden;
                        Item4CheckBox.IsChecked = false;
                        break;
                    case 4:
                        Item5CheckBox.Visibility = Visibility.Hidden;
                        Item5CheckBox.IsChecked = false;
                        break;
                    case 5:
                        Item6CheckBox.Visibility = Visibility.Hidden;
                        Item6CheckBox.IsChecked = false;
                        break;
                    case 6:
                        Item7CheckBox.Visibility = Visibility.Hidden;
                        Item7CheckBox.IsChecked = false;
                        break;
                    case 7:
                        Item8CheckBox.Visibility = Visibility.Hidden;
                        Item8CheckBox.IsChecked = false;
                        break;
                    case 8:
                        Item9CheckBox.Visibility = Visibility.Hidden;
                        Item9CheckBox.IsChecked = false;
                        break;
                    default:
                        Item1CheckBox.Content = "ERROR";
                        break;
                }
            }
        }

        private void GetExtentValuesButton_Click(object sender, RoutedEventArgs e)
        {
            List<ModelCode> properties = GetPropertiesForCheckedBoxes();
            extentValuesGIDs = testGda.GetExtentValues(code, properties);

            ResultsTextBox.Text = "ResourceDescriptions for " + code.ToString() + ": \n";
            foreach (long gid in extentValuesGIDs)
            {
                ResourceDescription rd = testGda.GetValues(gid, properties);
                WriteResultsToTextBox(rd);
            }
        }

        private List<ModelCode> GetPropertiesForCheckedBoxes()
        {
            if (SelectAllCheckBox.IsChecked == true)
                return propertyIDsByModelCode[code];

            List<ModelCode> properties = new List<ModelCode>();

            if (Item1CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[code][0]);
            if (Item2CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[code][1]);
            if (Item3CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[code][2]);
            if (Item4CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[code][3]);
            if (Item5CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[code][4]);
            if (Item6CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[code][5]);
            if (Item7CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[code][6]);
            if (Item8CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[code][7]);
            if (Item9CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[code][8]);

            return properties;
        }

        private void WriteResultsToTextBox(ResourceDescription rd)
        {
            StringBuilder sb;

            ResultsTextBox.Text += "\nGid = ";
            ResultsTextBox.Text += String.Format("0x{0:x16}\n", rd.Id);
            ResultsTextBox.Text += "Type = ";
            ResultsTextBox.Text += ((DMSType)ModelCodeHelper.ExtractTypeFromGlobalId(rd.Id)).ToString() + "\n";
            ResultsTextBox.Text += "Properties: \n";

            List<Property> Properties = rd.Properties;

            for (int i = 0; i < rd.Properties.Count; i++)
            {
                ResultsTextBox.Text += "\t" + rd.Properties[i].Id.ToString() + " = ";
                switch (Properties[i].Type)
                {
                    case PropertyType.Float:
                        ResultsTextBox.Text += rd.Properties[i].AsFloat();
                        break;
                    case PropertyType.Bool:
                        if (Properties[i].PropertyValue.FloatValue == 1)
                            ResultsTextBox.Text += "True";
                        else
                            ResultsTextBox.Text += "False";
                        break;
                    case PropertyType.Byte:
                    case PropertyType.Int32:
                    case PropertyType.Int64:
                    case PropertyType.TimeSpan:
                    case PropertyType.DateTime:
                        if (rd.Properties[i].Id == ModelCode.IDOBJ_GID)
                        {
                            ResultsTextBox.Text += String.Format("0x{0:x16}", rd.Properties[i].AsLong());
                        }
                        else
                        {
                            ResultsTextBox.Text += rd.Properties[i].AsLong();
                        }

                        break;
                    case PropertyType.Enum:
                        try
                        {
                            EnumDescs enumDescs = new EnumDescs();
                            ResultsTextBox.Text += enumDescs.GetStringFromEnum(rd.Properties[i].Id, rd.Properties[i].AsEnum());
                        }
                        catch (Exception)
                        {
                            ResultsTextBox.Text += rd.Properties[i].AsEnum();
                        }

                        break;
                    case PropertyType.Reference:
                        ResultsTextBox.Text += String.Format("0x{0:x16}", rd.Properties[i].AsReference());
                        break;
                    case PropertyType.String:
                        if (rd.Properties[i].PropertyValue.StringValue == null)
                        {
                            rd.Properties[i].PropertyValue.StringValue = String.Empty;
                        }
                        ResultsTextBox.Text += rd.Properties[i].AsString();
                        break;

                    case PropertyType.Int64Vector:
                    case PropertyType.ReferenceVector:
                        if (rd.Properties[i].AsLongs().Count > 0)
                        {
                            sb = new StringBuilder(100);
                            for (int j = 0; j < rd.Properties[i].AsLongs().Count; j++)
                            {
                                sb.Append(String.Format("0x{0:x16}", rd.Properties[i].AsLongs()[j])).Append(", ");
                            }

                            ResultsTextBox.Text += sb.ToString(0, sb.Length - 2);
                        }
                        else
                        {
                            ResultsTextBox.Text += "empty long/reference vector";
                        }

                        break;
                    default:
                        throw new Exception("Failed to export Resource Description as XML. Invalid property type.");
                }
                ResultsTextBox.Text += "\n";
            }
        }

        private void SelectAllCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (SelectAllCheckBox.IsChecked == true)
            {
                if (Item1CheckBox.IsVisible)
                    Item1CheckBox.IsChecked = true;
                if (Item2CheckBox.IsVisible)
                    Item2CheckBox.IsChecked = true;
                if (Item3CheckBox.IsVisible)
                    Item3CheckBox.IsChecked = true;
                if (Item4CheckBox.IsVisible)
                    Item4CheckBox.IsChecked = true;
                if (Item5CheckBox.IsVisible)
                    Item5CheckBox.IsChecked = true;
                if (Item6CheckBox.IsVisible)
                    Item6CheckBox.IsChecked = true;
                if (Item7CheckBox.IsVisible)
                    Item7CheckBox.IsChecked = true;
                if (Item8CheckBox.IsVisible)
                    Item8CheckBox.IsChecked = true;
                if (Item9CheckBox.IsVisible)
                    Item9CheckBox.IsChecked = true;
            }
            else
            {
                if (Item1CheckBox.IsVisible)
                    Item1CheckBox.IsChecked = false;
                if (Item2CheckBox.IsVisible)
                    Item2CheckBox.IsChecked = false;
                if (Item3CheckBox.IsVisible)
                    Item3CheckBox.IsChecked = false;
                if (Item4CheckBox.IsVisible)
                    Item4CheckBox.IsChecked = false;
                if (Item5CheckBox.IsVisible)
                    Item5CheckBox.IsChecked = false;
                if (Item6CheckBox.IsVisible)
                    Item6CheckBox.IsChecked = false;
                if (Item7CheckBox.IsVisible)
                    Item7CheckBox.IsChecked = false;
                if (Item8CheckBox.IsVisible)
                    Item8CheckBox.IsChecked = false;
                if (Item9CheckBox.IsVisible)
                    Item9CheckBox.IsChecked = false;
            }
        }
    }
}
