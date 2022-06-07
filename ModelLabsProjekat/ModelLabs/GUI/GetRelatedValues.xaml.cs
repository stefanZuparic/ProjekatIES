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
    /// Interaction logic for GetRelatedValues.xaml
    /// </summary>
    public partial class GetRelatedValues : Window
    {
        public static MyTestGDA testGda;
        public static ModelResourcesDesc resourcesDesc;
        public static Dictionary<ModelCode, List<ModelCode>> propertyIDsByModelCode;
        public static List<long> allGids;
        public static List<DMSType> dmsTypes;
        public static List<ModelCode> associations;
        public static long gid;
        public static ModelCode code;
        public static DMSType dms;
        public static Association association;
        public static ModelCode assocCode;
        public static ModelCode dmsCode;

        public GetRelatedValues()
        {
            InitializeComponent();

            testGda = new MyTestGDA();
            resourcesDesc = new ModelResourcesDesc();
            propertyIDsByModelCode = new Dictionary<ModelCode, List<ModelCode>>();
            
            allGids = GetAllDecimalGids();

            dmsTypes = new List<DMSType>();

            associations = new List<ModelCode>();

            List<string> hexGids = GetAllStringGids();
            this.GIDComboBox.ItemsSource = hexGids;

            foreach (ModelCode code in Enum.GetValues(typeof(ModelCode)))
            {
                List<ModelCode> propertyIDs = resourcesDesc.GetAllPropertyIds(code);
                propertyIDsByModelCode.Add(code, propertyIDs);
            }

            foreach (DMSType dms in Enum.GetValues(typeof(DMSType)))
            {
                if (dms == DMSType.MASK_TYPE)
                    continue;
                dmsTypes.Add(dms);
            }
        }

        private List<long> GetAllDecimalGids()
        {
            return testGda.TestGetExtentValuesAllTypes();
        }

        private List<string> GetAllStringGids()
        {
            List<long> gids = GetAllDecimalGids();

            List<string> hexGids = new List<string>();
            foreach (long gid in gids)
            {
                string hexValue = GetStringGidFromDecimal(gid);
                hexGids.Add(hexValue);
            }
            return hexGids;
        }

        private string GetStringGidFromDecimal(long gid)
        {
            return "0x0000000" + gid.ToString("X");
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

        private void GIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gid = allGids[GIDComboBox.SelectedIndex];
            associations = new List<ModelCode>();
            foreach (ModelCode mc in resourcesDesc.GetAllPropertyIdsForEntityId(gid))
            {
                string hexValue = ((long)mc).ToString("X");
                string isReference = hexValue.Substring(hexValue.Length - 1);
                
                if (isReference == "9")
                {
                    associations.Add(mc);
                }
            }
            AssociationComboBox.ItemsSource = associations;
            AssociationComboBox.SelectedIndex = -1;
            DMSTypeComboBox.SelectedIndex = -1;
            code = resourcesDesc.GetModelCodeFromId(gid);
        }

        private void GetRelatedValuesButton_Click(object sender, RoutedEventArgs e)
        {
            List<ModelCode> properties = GetPropertiesForCheckedBoxes();
            association = new Association();
            association.Inverse = false;
            association.PropertyId = assocCode;
            association.Type = resourcesDesc.GetModelCodeFromType(dms);

            ResultsTextBox.Text = "ResourceDescriptions for " + assocCode.ToString() + ": \n";
            List<long> gids = testGda.GetRelatedValues(gid, association, properties);
            foreach (long g in gids)
            {
                ResourceDescription rd = testGda.GetValues(g, properties);
                WriteResultsToTextBox(rd);
            }
        }

        private List<ModelCode> GetPropertiesForCheckedBoxes()
        {
            if (SelectAllCheckBox.IsChecked == true)
                return propertyIDsByModelCode[dmsCode];

            List<ModelCode> properties = new List<ModelCode>();

            if (Item1CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[dmsCode][0]);
            if (Item2CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[dmsCode][1]);
            if (Item3CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[dmsCode][2]);
            if (Item4CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[dmsCode][3]);
            if (Item5CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[dmsCode][4]);
            if (Item6CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[dmsCode][5]);
            if (Item7CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[dmsCode][6]);
            if (Item8CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[dmsCode][7]);
            if (Item9CheckBox.IsChecked == true)
                properties.Add(propertyIDsByModelCode[dmsCode][8]);

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

            private void AssociationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AssociationComboBox.SelectedIndex != -1)
            {
                assocCode = associations[AssociationComboBox.SelectedIndex];
                DMSTypeComboBox.ItemsSource = dmsTypes;
            }
        }

        private void DMSTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DMSTypeComboBox.SelectedIndex != -1)
            {
                dms = dmsTypes[DMSTypeComboBox.SelectedIndex];
                CreateCheckBoxes();
            }
        }

        private void CreateCheckBoxes()
        {
            int endIndex = 0;

            GetRelatedValuesButton.Visibility = Visibility.Visible;
            SelectAllCheckBox.Visibility = Visibility.Visible;
            SelectAllCheckBox.IsChecked = false;
            dmsCode = resourcesDesc.GetModelCodeFromType(dms);
            for (int i = 0; i < propertyIDsByModelCode[dmsCode].Count; i++)
            {
                endIndex = i;
                ModelCode c = propertyIDsByModelCode[dmsCode][i];
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
