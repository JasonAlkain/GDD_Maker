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
        // 
        private string currentFilePath = string.Empty;
        private GameDesignDocument gdd;

        private string m_ImportFilter = "JSON Files (*.json)|*.json";
        private string m_ExportFilter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt|CSV Files (*.csv)|*.csv|Markdown (*.md)|*.md";
        private bool m_IsDirty = false;
        string m_FilledFields = string.Empty;

        Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "TargetAudience", "Target Audience" },
            { "WorldSetting", "World Setting" },
            { "DesignPillars", "Design Pillars" },
            { "KeyFeatures", "Key Features" },
            { "CoreMechanics", "Core Mechanics" },
            { "ArtStyle", "Art Style" },
            { "CharacterDesigns", "Character Designs" },
            { "SoundAndMusic", "Sound And Music" },
            { "BusinessModel", "Business Model" }
        };

        // 
        public MainWindow()
        {
            InitializeComponent();

            gdd = new GameDesignDocument();
            gdd.PropertyChanged += Gdd_PropertyChanged; // Subscribe to the PropertyChanged event
            
            this.DataContext = gdd;
            this.Closing += MainWindow_Closing;

        }

        //
        public void Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Executing the Open command");
        }

        // Event handler for when a property in the GDD changes
        private void Gdd_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            m_IsDirty = true; // Set the isDirty flag to true whenever a property changes
            if(!m_FilledFields.Contains(e.PropertyName))
                m_FilledFields += $"\n- {e.PropertyName}";

        }

        // 
        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            //
            if (m_IsDirty)
            {
                var result = MessageBox.Show(
                    $"Would you like to save your changes?\nThe following fields are filled:{m_FilledFields}", 
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
            //MessageBox.Show("Please note that this program only supports loading JSON files at this time.", "NOTE", MessageBoxButton.OK);

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = m_ImportFilter
            };

            if (openFileDialog.ShowDialog() == true)
            {
                LoadFromFile(openFileDialog.FileName);
            }
        }

        // Save As Dialog
        private void SaveAs()
        {
            m_IsDirty = false;
            m_FilledFields = string.Empty;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = m_ExportFilter
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
            gdd.Music = MusicTextBox.Text;
            gdd.SFX = SFXTextBox.Text;
            gdd.Team = TeamTextBox.Text;
            gdd.CharacterDesigns = CharacterDesignsTextBox.Text;
            gdd.WorldSetting = WorldSettingTextBox.Text;
            gdd.Tone = ToneTextBox.Text;
            gdd.BusinessModel = BusinessModelTextBox.Text;
            gdd.DesignPillars = DesignPillarsTextBox.Text;
            gdd.Narrative = NarrativeTextBox.Text;
        }

        // Save Data to a Object
        private void Menu_New_Click(object sender, RoutedEventArgs e)
        {
            gdd = new();
            LoadDataIntoFields(gdd);
            m_IsDirty = false;
        }

        // Save Data to a File
        private void SaveToFile(string filePath)
        {

            if (string.IsNullOrEmpty(filePath)) return;

            SaveToObject();

            string fileExtension = Path.GetExtension(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);

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
                case ".md":
                    ExportToMarkdown(gdd, filePath);
                    sb.AppendLine($"Exported to Game Design Document to {fileName}");
                    break;
                default:
                    sb.AppendLine("That file type is not supported yet.");
                    break;
            }

            //MessageBox.Show($"{sb}");

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
                    
                    //MessageBox.Show($"{sb}","Successfully Loaded");
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
            MusicTextBox.Text = GDD.Music;
            SFXTextBox.Text = GDD.SFX;
            TeamTextBox.Text = GDD.Team;
            CharacterDesignsTextBox.Text = GDD.CharacterDesigns;
            WorldSettingTextBox.Text = GDD.WorldSetting;
            ToneTextBox.Text = GDD.Tone;
            BusinessModelTextBox.Text = GDD.BusinessModel;
            DesignPillarsTextBox.Text = GDD.DesignPillars;
            NarrativeTextBox.Text = GDD.Narrative;

            if (GDD.SoundAndMusic != null) {
                MusicTextBox.Text += "\n"+ GDD.SoundAndMusic;
            }
        }

        /// <summary>
        /// Export to JSON
        /// </summary>
        /// <param _="GDD">GDD Object.</param>
        /// <param _="filePath">Path to save location.</param>
        private void ExportToJson(GameDesignDocument GDD, string filePath)
        {
            var jsonPOST = JsonConvert.SerializeObject(GDD, Formatting.Indented);
            File.WriteAllText(filePath, jsonPOST);
        }

        /// <summary>
        /// Export to TXT
        /// </summary>
        /// <param _="GDD">GDD Object.</param>
        /// <param _="filePath">Path to save location.</param>
        private void ExportToTxt(GameDesignDocument GDD, string filePath)
        {
            StringBuilder sb = new StringBuilder();
                        
            foreach (var item in gdd.GetType().GetProperties())
            {
                sb.AppendLine(item.Name+":\n"+item.GetValue(gdd)?.ToString());
            }

            File.WriteAllText(filePath, $"{sb}");
        }

        /// <summary>
        /// Export to CSV
        /// </summary>
        /// <param _="GDD">GDD Object.</param>
        /// <param _="filePath">Path to save location.</param>
        private void ExportToCsv(GameDesignDocument GDD, string filePath)
        {
            StringBuilder sbKeys = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();

            foreach (var item in GDD.GetType().GetProperties())
            {
                var name = item.Name+";";
                sbKeys.Append(name);
                var value = item.GetValue(GDD)?.ToString() + ";";
                sbValues.Append(value);
            }

            string csv = $"{sbKeys}\n{sbValues}";
            
            File.WriteAllText(filePath, csv);
        }

        /// <summary>
        /// Export to TXT
        /// </summary>
        /// <param _="GDD">GDD Object.</param>
        /// <param _="filePath">Path to save location.</param>
        private void ExportToMarkdown(GameDesignDocument GDD, string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            foreach (var item in gdd.GetType().GetProperties())
            {

                string _ = "## " + (headers.TryGetValue(item.Name, out var value) ? value : item.Name);
                sb.AppendLine(_ + ":\n" + item.GetValue(gdd)?.ToString());
            }

            File.WriteAllText(filePath, $"{sb}");
        }
    }
}