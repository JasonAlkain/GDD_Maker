using System.ComponentModel;
using System.Diagnostics;

namespace GDD_Maker
{
    public class GameDesignDocument : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _gameTitle = "";
        private string _genre = "";
        private string _targetAudience = "";
        private string _description = ""    ;
        private string _keyFeatures = "";
        private string _coreMechanics = "";
        private string _artStyle = "";
        private string _soundAndMusic = "";
        private string _team = "";

        public string GameTitle
        {
            get => _gameTitle;
            set
            {
                if (_gameTitle != value)
                {
                    _gameTitle = value;
                    OnPropertyChanged(nameof(GameTitle));
                }
            }
        }

        public string Genre
        {
            get => _genre;
            set
            {
                if (_genre != value)
                {
                    _genre = value;
                    OnPropertyChanged(nameof(Genre));
                }
            }
        }

        public string TargetAudience
        {
            get => _targetAudience;
            set
            {
                if (_targetAudience != value)
                {
                    _targetAudience = value;
                    OnPropertyChanged(nameof(TargetAudience));
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public string KeyFeatures
        {
            get => _keyFeatures;
            set
            {
                if (_keyFeatures != value)
                {
                    _keyFeatures = value;
                    OnPropertyChanged(nameof(KeyFeatures));
                }
            }
        }

        public string CoreMechanics
        {
            get => _coreMechanics;
            set
            {
                if (_coreMechanics != value)
                {
                    _coreMechanics = value;
                    OnPropertyChanged(nameof(CoreMechanics));
                }
            }
        }

        public string ArtStyle
        {
            get => _artStyle;
            set
            {
                if (_artStyle != value)
                {
                    _artStyle = value;
                    OnPropertyChanged(nameof(ArtStyle));
                }
            }
        }

        public string SoundAndMusic
        {
            get => _soundAndMusic;
            set
            {
                if (_soundAndMusic != value)
                {
                    _soundAndMusic = value;
                    OnPropertyChanged(nameof(SoundAndMusic));
                }
            }
        }

        public string Team
        {
            get => _team;
            set
            {
                if (_team != value) 
                { 
                    _team = value;
                    OnPropertyChanged(nameof(Team));
                }
            }
        }

        // Trigger the PropertyChanged event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameDesignDocument(
            string gameTitle = "", 
            string genre = "", 
            string targetAudience = "", 
            string description = "", 
            string keyFeatures = "", 
            string coreMechanics = "", 
            string artStyle = "", 
            string soundAndMusic = "",
            string team = "")
        {
            GameTitle = gameTitle ?? "";
            Genre = genre ?? "";
            TargetAudience = targetAudience ?? "";
            Description = description ?? "";
            KeyFeatures = keyFeatures ?? "";
            CoreMechanics = coreMechanics ?? "";
            ArtStyle = artStyle ?? "";
            SoundAndMusic = soundAndMusic ?? "";
            Team = team ?? "";
            PropertyChanged = new PropertyChangedEventHandler(PrintPropertyChanged);
        }

        private void PrintPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
        }
    }
}
