using System.Windows;
//using System.Xml;
using Newtonsoft.Json; // Make sure to install Newtonsoft.Json via NuGet
using Newtonsoft.Json.Linq;
using System.IO;
using Microsoft.Win32;
using System.Text;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Input;

namespace GDD_Maker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentFilePath = string.Empty;
        private GameDesignDocument gdd;

        private string importFilter = "JSON Files (*.json)|*.json";
        private string exportFilter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv";
        private bool isDirty = false;
        string filledFields = string.Empty;


        public MainWindow()
        {
            InitializeComponent();

            gdd = new GameDesignDocument();
            gdd.PropertyChanged += Gdd_PropertyChanged; // Subscribe to the PropertyChanged event
            
            this.DataContext = gdd;
            this.Closing += MainWindow_Closing;

            
        }

        public void Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Executing the Open command");
        }

        // Event handler for when a property in the GDD changes
        private void Gdd_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            isDirty = true; // Set the isDirty flag to true whenever a property changes
            if(!filledFields.Contains(e.PropertyName))
                filledFields += $"\n- {e.PropertyName}";

        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            //
            if (isDirty)
            {
                var result = MessageBox.Show(
                    $"Would you like to save your changes?\nThe following fields are filled:{filledFields}", 
                    "Unsaved Changes",
                    MessageBoxButton.YesNoCancel, 
                    MessageBoxImage.Warning);

                // Handle user's choice
                if (result == MessageBoxResult.Yes)
                {
                    SaveAs(); // Call your save method
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    // Cancel the closing event
                    e.Cancel = true;
                }
                // If No, allow the window to close without saving
            }
        }

        // 
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveToObject();
        }

        // Save Button Event Handler
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveToFile(currentFilePath);
        }

        // Menu 'Save' Click Handler
        private void Menu_Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveAs();
            }
            else
            {
                SaveToFile(currentFilePath);
            }
        }

        // Menu 'Save As' Click Handler
        private void Menu_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        // Menu 'Open' Click Handler
        private void Menu_Open_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "Please note that this program only supports loading JSON files at this time.", 
                "NOTE", 
                MessageBoxButton.OK
                );

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = importFilter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                LoadFromFile(openFileDialog.FileName);
            }
        }

        // Save As Dialog
        private void SaveAs()
        {
            isDirty = false;
            filledFields = string.Empty;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = exportFilter
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                currentFilePath = saveFileDialog.FileName;
                SaveToFile(currentFilePath);
            }
        }

        // Save Data to a Object
        private void SaveToObject()
        {
            gdd.GameTitle = GameTitleTextBox.Text;
            gdd.Genre = GenreTextBox.Text;
            gdd.TargetAudience = TargetAudienceTextBox.Text;
            gdd.Description = DescriptionTextBox.Text;
            gdd.KeyFeatures = FeaturesTextBox.Text;
            gdd.CoreMechanics = MechanicsTextBox.Text;
            gdd.ArtStyle = ArtStyleTextBox.Text;
            gdd.SoundAndMusic = SoundMusicTextBox.Text;
        }

        // Save Data to a File
        private void SaveToFile(string filePath)
        {

            if (string.IsNullOrEmpty(filePath)) return;

            SaveToObject();

            string fileExtension = Path.GetExtension(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string msg = "";
            StringBuilder sb = new StringBuilder();

            switch (fileExtension)
            {
                case ".json":
                    ExportToJson(gdd, filePath);
                    sb.AppendLine($"Exported to Game Design Document to {fileName}");
                    break;
                case ".txt":
                    ExportToTxt(gdd, filePath);
                    sb.AppendLine($"Exported to Game Design Document to {fileName}");
                    break;
                case ".csv":
                    ExportToCsv(gdd, filePath);
                    sb.AppendLine($"Exported to Game Design Document to {fileName}");
                    sb.AppendLine("This progam exports to CSV using ';' as the separator.");
                    break;
                default:
                    sb.AppendLine("That file type is not supported yet.");
                    break;
            }

            MessageBox.Show($"{sb}");

        }

        // Load Data from a File
        private void LoadFromFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            StringBuilder sb = new StringBuilder();
            
            if (fileExtension == ".json")
            {
                try
                {
                    var jsonGET = JsonConvert.DeserializeObject<GameDesignDocument>(File.ReadAllText(filePath));
                    if (jsonGET == null)
                        throw new JsonSerializationException();
                    
                    LoadDataIntoFields(jsonGET);
                    SaveToObject();
                    
                    sb.AppendLine($"Loaded file: {fileName}");
                    sb.AppendLine("If you notice anything missing please let me know via the comments of the project.");
                    
                    MessageBox.Show($"{sb}","Successfully Loaded");
                }
                catch (JsonSerializationException ex) 
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (fileExtension == ".txt" || fileExtension == ".csv")
            {
                // Parsing CSV or plain text loading logic goes here
                MessageBox.Show("Text/CSV loading not yet implemented.");
            }
        }

        // Helper Method to Load Data into Form Fields
        private void LoadDataIntoFields(GameDesignDocument GDD)
        {
            GameTitleTextBox.Text = GDD.GameTitle;
            GenreTextBox.Text = GDD.Genre;
            TargetAudienceTextBox.Text = GDD.TargetAudience;
            DescriptionTextBox.Text = GDD.Description;
            FeaturesTextBox.Text = GDD.KeyFeatures;
            MechanicsTextBox.Text = GDD.CoreMechanics;
            ArtStyleTextBox.Text = GDD.ArtStyle;
            SoundMusicTextBox.Text = GDD.SoundAndMusic;
        }

        // Export to JSON
        private void ExportToJson(GameDesignDocument GDD, string filePath)
        {
            var jsonPOST = JsonConvert.SerializeObject(GDD, Formatting.Indented);
            File.WriteAllText(filePath, jsonPOST);
        }

        // Export to TXT
        private void ExportToTxt(GameDesignDocument GDD, string filePath)
        {
            //string fileContent = $"Game Title: {gdd.GameTitle}\n" +
            //                     $"Genre: {gdd.Genre}\n" +
            //                     $"Target Audience: {gdd.TargetAudience}\n" +
            //                     $"Game Description: {gdd.Description}\n" +
            //                     $"Key Features: {gdd.KeyFeatures}\n" +
            //                     $"Core Mechanics: {gdd.CoreMechanics}\n" +
            //                     $"Art Style: {gdd.ArtStyle}\n" +
            //                     $"Sound and Music: {gdd.SoundAndMusic}";

            StringBuilder sb = new StringBuilder();
                        
            foreach (var item in gdd.GetType().GetProperties())
            {
                sb.AppendLine(item.Name+":\n"+item.GetValue(gdd)?.ToString());
            }

            File.WriteAllText(filePath, $"{sb}");
        }

        // Export to CSV
        private void ExportToCsv(GameDesignDocument gdd, string filePath)
        {
            StringBuilder sbKeys = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();

            foreach (var item in gdd.GetType().GetProperties())
            {
                var name = item.Name+";";
                sbKeys.Append(name);
                var value = item.GetValue(gdd)?.ToString() + ";";
                sbValues.Append(value);
            }

            string csv = $"{sbKeys}\n{sbValues}";
            
            File.WriteAllText(filePath, csv);
        }

    }
}