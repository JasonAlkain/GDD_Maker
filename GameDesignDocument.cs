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
        private string _description = "";
        private string _designPillars = "";
        private string _narrative = "";
        private string _worldSetting = "";
        private string _tone = "";
        private string _keyFeatures = "";
        private string _coreMechanics = "";
        private string _artStyle = "";
        private string _characterDesigns = "";
        private string _soundAndMusic = "";
        private string _music = "";
        private string _sfx = "";
        private string _team = "";
        private string _businessModel = "";

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

        public string Tone
        {
            get => _tone;
            set
            {
                if (_tone != value)
                {
                    _tone = value;
                    OnPropertyChanged(nameof(Tone));
                }
            }
        }

        public string WorldSetting
        {
            get => _worldSetting;
            set
            {
                if (_worldSetting != value)
                {
                    _worldSetting = value;
                    OnPropertyChanged(nameof(WorldSetting));
                }
            }
        }

        public string Narrative
        {
            get => _narrative;
            set
            {
                if (_narrative != value)
                {
                    _narrative = value;
                    OnPropertyChanged(nameof(Narrative));
                }
            }
        }

        public string DesignPillars
        {
            get => _designPillars;
            set
            {
                if (_designPillars != value)
                {
                    _designPillars = value;
                    OnPropertyChanged(nameof(DesignPillars));
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

        public string CharacterDesigns
        {
            get => _characterDesigns;
            set
            {
                if (_characterDesigns != value)
                {
                    _characterDesigns = value;
                    OnPropertyChanged(nameof(CharacterDesigns));
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

        public string Music
        {
            get => _music;
            set
            {
                if (_music != value)
                {
                    _music = value;
                    OnPropertyChanged(nameof(Music));
                }
            }
        }

        public string SFX
        {
            get => _sfx;
            set
            {
                if (_sfx != value)
                {
                    _sfx = value;
                    OnPropertyChanged(nameof(SFX));
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

        public string BusinessModel 
        { 
            get => _businessModel;
            set
            {
                if(_businessModel != value)
                {
                    _businessModel = value;
                    OnPropertyChanged(nameof(BusinessModel));
                }
            } 
        }

        // Trigger the PropertyChanged event
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameDesignDocument(
            string gameTitle="", 
            string genre = "", 
            string targetAudience = "", 
            string description = "",
            string worldSetting = "",
            string tone = "",
            string designPillars = "",
            string keyFeatures = "", 
            string coreMechanics = "", 
            string artStyle = "", 
            string characterDesigns = "",
            string soundAndMusic = "",
            string team = "",
            string bussinessModel = "")
        {
            GameTitle = gameTitle ?? "";
            Genre = genre ?? "";
            TargetAudience = targetAudience ?? "";
            Description = description ?? "";
            WorldSetting = worldSetting ?? "";
            Tone = tone ?? "";
            DesignPillars = designPillars ?? "";
            KeyFeatures = keyFeatures ?? "";
            CoreMechanics = coreMechanics ?? "";
            ArtStyle = artStyle ?? "";
            CharacterDesigns = characterDesigns ?? "";
            SoundAndMusic = soundAndMusic ?? "";
            Team = team ?? "";
            BusinessModel = bussinessModel ?? "";
        }
    }
}
