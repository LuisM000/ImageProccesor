using ImageProcessor.Models;
using ImageProcessor.Services;
using ImageProcessor.Services.UI;
using ImageProcessor.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessor.ViewModels
{
    public class VMProcessWindow:VMBase
    {
        private BitmapImage bitmapOriginal;
        private BitmapImage bitmapNIR;
        private BitmapImage bitmapProcessed;
        private Segmentation segmentation;
        private List<string> typeSegmentation;
        private string typeSegmentationSelected;
        private int clusters;
        private double tolerance;
        private List<string> typeDistance;
        private string typeDistanceSelected;
        private bool enabledCluster;
        private bool enabledTolerance;
        private bool enabledDistance;
        private int widthProportionsResults;

        private bool channel1Segmentation;
        private bool channel2Segmentation;
        private bool channel3Segmentation;
        private List<string> typeSegmentationSg;
        private string typeSegmentationSgSelected;
        private int clustersSegmentation;
        private double toleranceSegmentation;
        private List<string> typeDistanceSegmentation;
        private string typeDistanceSegmentationSelected;
        private List<string> typeAlterBands;
        private string selectedTypeAlter10;
        private string selectedTypeAlter11;
        private string selectedTypeAlter12;
        private string selectedTypeAlter13;
        private string selectedTypeAlter20;
        private string selectedTypeAlter21;
        private string selectedTypeAlter22;
        private string selectedTypeAlter23;


        private bool enabledClusterSegmentation;
        private bool enabledToleranceSegmentation;
        private bool enabledDistanceSegmentation;

        private ObservableCollection<ItemChannel> listChanelsReturn;
        private bool channel1MultipleSelected = true;
        private bool channel2MultipleSelected = true;
        private bool channel3MultipleSelected = true;
        private bool channel4MultipleSelected = true;
        private bool channel5MultipleSelected = true;
        private bool channel6MultipleSelected = true;

       
        private ItemChannel channel1ReturnSelected;
        private ItemChannel channel2ReturnSelected;
        private ItemChannel channel3ReturnSelected;
        

        private string status;
        private ObservableCollection<ItemProportions> proportionsResult;
        private ObservableCollection<ItemExternLibrary> listExternResources;
        private RampsNDVI rampNDVI;
        private RampsVI rampVI;
        private ObservableCollection<ItemRampColor> typeRampNDVIColors;
        private ItemRampColor typeRampNDVIColorsSelected;
        private ObservableCollection<ItemRampColor> typeRampVIColors;
        private ItemRampColor typeRampVIColorsSelected;
        private ItemRampColor rampSelected;
        private double minimumRamp;
        private double maximunRamp;
        private bool enabledAdjustRampNDVI;
        private bool enabledAdjustRampVI;
        private byte valueAdjustmentVI;
        private string urlQuiz;
     
                    
        private Visibility visibilityOptionsRampNDVI;
        private Visibility visibilityOptionsRampVI;
        private Visibility visibilityProcessedBitmap;
        private Visibility visibilityProportionsResults;
        private Visibility visibilityOptionsSegmentation;
        private Visibility visibilityQuiz;
        private Visibility visibilityContact;
        private Visibility visibilityAlterBands;



        private Lazy<DelegateCommand> ndviCommand;
        private Lazy<DelegateCommand> viCommand;
        private Lazy<DelegateCommand> ndviNIRCommand;
        private Lazy<DelegateCommand> viNIRCommand;
        private Lazy<DelegateCommand> withoutIRCommand;
        private Lazy<DelegateCommand> segmentationBandsCommand;
        private Lazy<DelegateCommand> segmentationBandsIRCommand;
        private Lazy<DelegateCommand> segmentationMultipleBandsCommand;
        private Lazy<DelegateCommand> setOriginalImageCommand;
        private Lazy<DelegateCommand> setIRImageCommand;

        private Lazy<DelegateCommandParam<SolidColorBrush>> changeColorCommand;
        private Lazy<DelegateCommand> expandImageCommand;
        private Lazy<DelegateCommandParam<Window>> returnWindowCommand;
        private Lazy<DelegateCommand> closeCommand;
        private Lazy<DelegateCommand> saveImageCommand;
        private Lazy<DelegateCommand> mouseEnterCommand;
        private Lazy<DelegateCommand> mouseLeaveCommand;
        private Lazy<DelegateCommand> exchangeImagesCommand;
        private Lazy<DelegateCommand> alterBandsCommand;
        private Lazy<DelegateCommand> alterBandsIRCommand;
        private Lazy<DelegateCommandParam<string>> browserCommand;
        private Lazy<DelegateCommandParam<string>> openURL;
        private Lazy<DelegateCommandParam<string>> openNotepad;
        private Lazy<DelegateCommand> visibiliyProcessedImageCommand;
        private Lazy<DelegateCommand> visibiliyOptionsSegmentationCommand;
        private Lazy<DelegateCommand> visibiliyOptionsRampNDVICommand;
        private Lazy<DelegateCommand> visibiliyOptionsRampVICommand;
        private Lazy<DelegateCommand> visibiliyQuizCommand;
        private Lazy<DelegateCommand> visibiliyContactCommand;
        private Lazy<DelegateCommand> visibiliyAlterBandsCommand;

        private INDVI ndviService;
        private IVI viService;
        private IKmeansBands kmeansBandsService;
        private IKmodesBands kmodesBandsService;
        private IChangeColor changeColService;
        private IProgressWindow pwService;
        private IColordialog colorDialogService;
        private IMessage messageService;
        private INavigationWindow navigationService;
        private INavigationImage navigationImageService;
        private ICloseApp closeService;
        private ISaveImage saveImageService;
        private IMouseActions mouseActionsService;
        private IBrowser browserService;
        private INotepad notepadService;
        private IAlterBands alterBandsService;

        public VMProcessWindow(INDVI ndviService,IVI viService,IKmeansBands kmeansBandsService, IKmodesBands kmodesBandsService, IChangeColor changeColService, IProgressWindow pwService, 
                                IColordialog colorDialogService, INavigationWindow navigationService,ICloseApp closeService, ISaveImage saveImageService,  
                                IMessage messageService, INavigationImage navigationImageService, IMouseActions mouseActionsService, IBrowser browserService, INotepad notepadService,
                                IAlterBands alterBandsService)
                                 
        {
            this.ndviService = ndviService;
            this.viService = viService;
            this.kmeansBandsService = kmeansBandsService;
            this.kmodesBandsService = kmodesBandsService;
            this.changeColService = changeColService;
            this.pwService = pwService;
            this.colorDialogService = colorDialogService;
            this.navigationService = navigationService;
            this.navigationImageService = navigationImageService;
            this.messageService = messageService;
            this.closeService = closeService;
            this.saveImageService = saveImageService;
            this.mouseActionsService = mouseActionsService;
            this.browserService = browserService;
            this.notepadService = notepadService;
            this.alterBandsService = alterBandsService;

            visibilityProcessedBitmap = Visibility.Collapsed;
            visibilityOptionsSegmentation = Visibility.Collapsed;
            visibilityOptionsRampNDVI = Visibility.Collapsed;
            visibilityOptionsRampVI = Visibility.Collapsed;
            visibilityQuiz = Visibility.Collapsed;
            visibilityContact = Visibility.Collapsed;
            visibilityAlterBands = Visibility.Collapsed;

            this.Clusters = 5;
            this.Tolerance = 0.05;
            this.TypeSegmentation = new List<string>()
            {
                "Progresiva",
                "Kmeans",
                "Kmodes",
            };
            this.typeSegmentationSelected = "Progresiva";
            this.TypeDistance = new List<string>()
            {
                    "BrayCurtis",
                "Chessboard",
                "Euclidean",
                "Manhattan",
                "SquareEuclidean",
            };
            this.TypeDistanceSelected = "SquareEuclidean";


            this.Channel1Segmentation = true; this.Channel2Segmentation = true; this.Channel3Segmentation = true;
            this.ClustersSegmentation = 5;
            this.ToleranceSegmentation = 0.05;
            this.TypeSegmentationSg = new List<string>()
            {
                "Kmeans",
                "Kmodes",
            };
            this.TypeSegmentationSgSelected = "Kmeans";
            this.typeDistanceSegmentation = new List<string>()
            {
                "BrayCurtis",
                "Chessboard",
                "Euclidean",
                "Manhattan",
                "SquareEuclidean",
            };
            this.TypeDistanceSegmentationSelected = "SquareEuclidean";

             this.ListChanelsReturn = new ObservableCollection<ItemChannel>()
            {
                new ItemChannel(){Name="Verde IR",Index=0},
                new ItemChannel(){Name="Rojo IR",Index=1},
                new ItemChannel(){Name="Infrarrojo IR",Index=2},
                new ItemChannel(){Name="Azul Orig.",Index=3},
                new ItemChannel(){Name="Verde Orig.",Index=4},
                new ItemChannel(){Name="Rojo Orig.",Index=5},
                new ItemChannel(){Name="Vacío",Index=-1}
            };
             this.Channel1ReturnSelected = this.ListChanelsReturn.Where(C => C.Index == 3).First();
             this.Channel2ReturnSelected = this.ListChanelsReturn.Where(C => C.Index == 4).First();
             this.Channel3ReturnSelected = this.ListChanelsReturn.Where(C => C.Index == 5).First();


            this.ListExternResources = new ObservableCollection<ItemExternLibrary>()
            {
               new ItemExternLibrary(){Title="Accord.NET", Url="http://www.accord-framework.net/"},
               new ItemExternLibrary(){Title="AForge.NET", Url="http://www.aforgenet.com/"},
               new ItemExternLibrary(){Title="Autofac", Url="http://www.autofac.org/"},
               new ItemExternLibrary(){Title="Progress dialog", Url="http://www.parago.de/"}
            };
            this.TypeAlterBands = new List<string>()
            {
                "Banda 1","Banda 2","Banda 3","Banda 4"
            };
            this.rampNDVI = new RampsNDVI();
            this.TypeRampNDVIColors = this.rampNDVI.Ramps;
            this.TypeRampNDVIColorsSelected = this.rampNDVI.Ramps[0];
            this.rampVI= new RampsVI();
            this.TypeRampVIColors = this.rampVI.Ramps;
            this.TypeRampVIColorsSelected = this.rampVI.Ramps[0];
            this.ValueAdjustmentVI = 5;
            



            this.ndviCommand = new Lazy<DelegateCommand>(
                () =>
                new DelegateCommand(NDVIExecute, NDVICanExecute));
            this.viCommand = new Lazy<DelegateCommand>(
               () =>
               new DelegateCommand(VIExecute, VICanExecute));

            this.ndviNIRCommand = new Lazy<DelegateCommand>(
              () =>
              new DelegateCommand(NDVINIRExecute, NDVINIRCanExecute));
            this.viNIRCommand = new Lazy<DelegateCommand>(
             () =>
             new DelegateCommand(VINIRExecute, VINIRCanExecute));
            this.segmentationBandsCommand = new Lazy<DelegateCommand>(
               () =>
               new DelegateCommand(SegmentationBandsExecute, SegmentationBandsCanExecute));
            this.segmentationBandsIRCommand = new Lazy<DelegateCommand>(
               () =>
               new DelegateCommand(SegmentationBandsIRExecute, SegmentationBandsIRCanExecute));
            this.segmentationMultipleBandsCommand = new Lazy<DelegateCommand>(
               () =>
               new DelegateCommand(SegmentationMultipleBandsExecute, SegmentationMultipleBandsCanExecute));
            this.withoutIRCommand = new Lazy<DelegateCommand>(
            () =>
            new DelegateCommand(WithoutIRExecute, WithoutIRCanExecute));
            this.changeColorCommand = new Lazy<DelegateCommandParam<SolidColorBrush>>(
           () =>
           new DelegateCommandParam<SolidColorBrush>(ChangeColorExecute, ChangeColorCanExecute));
            this.expandImageCommand = new Lazy<DelegateCommand>(
            () =>
            new DelegateCommand(ExpandImageCommandExecute, ExpandImageCommandCanExecute));
            this.alterBandsCommand = new Lazy<DelegateCommand>(
                 () =>
                 new DelegateCommand(AlterBandsCommandExecute, AlterBandsCommandCanExecute));
            this.alterBandsIRCommand = new Lazy<DelegateCommand>(
                () =>
                new DelegateCommand(AlterBandsIRCommandExecute, AlterBandsIRCommandCanExecute));
            this.setOriginalImageCommand = new Lazy<DelegateCommand>(
                 () =>
                 new DelegateCommand(SetOriginalImageCommandExecute, SetOriginalImageCommandCanExecute));
            this.setIRImageCommand = new Lazy<DelegateCommand>(
                 () =>
                 new DelegateCommand(SetIRImageCommandExecute, SetIRImageCommandCanExecute));
            this.returnWindowCommand = new Lazy<DelegateCommandParam<Window>>(
           () =>
           new DelegateCommandParam<Window>(ReturnWindowCommandExecute, ReturnWindowCommandCanExecute));

            this.closeCommand = new Lazy<DelegateCommand>(
           () =>
           new DelegateCommand(CloseCommandExecute, CloseCommandCanExecute));

            this.saveImageCommand = new Lazy<DelegateCommand>(
          () =>
          new DelegateCommand(SaveImageCommandExecute, SaveImageCommandCanExecute));

            this.mouseEnterCommand = new Lazy<DelegateCommand>(
          () =>
          new DelegateCommand(MouseEnterCommandExecute, MouseEnterCommandCanExecute));
            this.mouseLeaveCommand = new Lazy<DelegateCommand>(
         () =>
         new DelegateCommand(MouseLeaveCommandExecute, MouseLeaveCommandCanExecute));
            this.exchangeImagesCommand = new Lazy<DelegateCommand>(
         () =>
         new DelegateCommand(ExchangeImagesCommandExecute, ExchangeImagesCommandCanExecute));
            this.browserCommand = new Lazy<DelegateCommandParam<string>>(
           () =>
           new DelegateCommandParam<string>(BrowserCommandCommandExecute, BrowserCommandCanExecute));
            this.openURL = new Lazy<DelegateCommandParam<string>>(
           () =>
           new DelegateCommandParam<string>(OpenURLCommandExecute, OpenURLCommandCanExecute));
            this.openNotepad = new Lazy<DelegateCommandParam<string>>(
           () =>
           new DelegateCommandParam<string>(OpenNotepadCommandExecute, OpenNotepadCommandCanExecute));

            this.visibiliyProcessedImageCommand = new Lazy<DelegateCommand>(
              () =>
              new DelegateCommand(VisibilityProcessedImageExecute, VisibilityProcessedImageCanExecute));
            this.visibiliyOptionsSegmentationCommand = new Lazy<DelegateCommand>(
              () =>
              new DelegateCommand(VisibilityOptionsSegmentationExecute, VisibilityOptionsSegmentationCanExecute));
            this.visibiliyOptionsRampNDVICommand = new Lazy<DelegateCommand>(
              () =>
              new DelegateCommand(VisibilityOptionsRampNDVIExecute, VisibilityOptionsRampNDVICanExecute));
            this.visibiliyOptionsRampVICommand = new Lazy<DelegateCommand>(
              () =>
              new DelegateCommand(VisibilityOptionsRampVIExecute, VisibilityOptionsRampVICanExecute));
            this.visibiliyQuizCommand = new Lazy<DelegateCommand>(
             () =>
             new DelegateCommand(VisibilityQuizCommandExecute, VisibilityQuizCommandCanExecute));
            this.visibiliyContactCommand = new Lazy<DelegateCommand>(
             () =>
             new DelegateCommand(VisibilityContactCommandExecute, VisibilityContactCommandCanExecute));
            this.visibiliyAlterBandsCommand = new Lazy<DelegateCommand>(
            () =>
            new DelegateCommand(VisibilityAlterBandsCommandExecute, VisibilityAlterBandsCommandCanExecute));

        }


        public BitmapImage BitmapOriginal
        {
            get { return bitmapOriginal; }
            set 
            { 
                if(value!=null)
                {
                    bitmapOriginal = value;
                    RaisePropertyChanged();
                    ndviCommand.Value.RaiseCanExecuteChanged();
                    viCommand.Value.RaiseCanExecuteChanged();
                    segmentationBandsCommand.Value.RaiseCanExecuteChanged();
                    exchangeImagesCommand.Value.RaiseCanExecuteChanged();
                    segmentationMultipleBandsCommand.Value.RaiseCanExecuteChanged();
                    alterBandsCommand.Value.RaiseCanExecuteChanged();
                }
            }
        }
        public BitmapImage BitmapNIR
        {
            get {return bitmapNIR; }
            set
            {
                if (value != null)
                {
                    bitmapNIR = value;
                    RaisePropertyChanged();
                    ndviCommand.Value.RaiseCanExecuteChanged();
                    viCommand.Value.RaiseCanExecuteChanged();
                    ndviNIRCommand.Value.RaiseCanExecuteChanged();
                    viNIRCommand.Value.RaiseCanExecuteChanged();
                    withoutIRCommand.Value.RaiseCanExecuteChanged();
                    segmentationBandsIRCommand.Value.RaiseCanExecuteChanged();
                    exchangeImagesCommand.Value.RaiseCanExecuteChanged();
                    segmentationMultipleBandsCommand.Value.RaiseCanExecuteChanged();
                    alterBandsIRCommand.Value.RaiseCanExecuteChanged();
                }
            }
        }
        public BitmapImage BitmapProcessed
        {
            get {return bitmapProcessed; }
            set
            {
                if (value != null)
                {
                    bitmapProcessed = value;
                    RaisePropertyChanged();
                    VisibilityProcessedBitmap = Visibility.Visible;
                 }
            }
        }
        public Segmentation Segmentation
        {
            get { return segmentation; }
            set { segmentation = value; RaisePropertyChanged(); }
        }
        public List<string> TypeSegmentation
        {
            get { return typeSegmentation; }
            set { typeSegmentation = value; RaisePropertyChanged();
            }
        }
        public string TypeSegmentationSelected
        {
            get { return typeSegmentationSelected; }
            set { 
                typeSegmentationSelected = value; 
                RaisePropertyChanged();
                if (value == "Progresiva") { EnabledCluster = false; EnabledDistance = false; EnabledTolerance = false; }
                if (value == "Kmeans") { EnabledCluster = true; EnabledDistance = true; EnabledTolerance = true; }
                if (value == "Kmodes") { EnabledCluster = true; EnabledDistance = false; EnabledTolerance = true; }
            }
        }
        public int Clusters
        {
            get { return clusters; }
            set { clusters = value; RaisePropertyChanged(); }
        }
        public double Tolerance
        {
            get { return tolerance; }
            set { tolerance = value; RaisePropertyChanged(); }
        }
        public List<string> TypeDistance
        {
            get { return typeDistance; }
            set { typeDistance = value; RaisePropertyChanged(); }
        }
        public string TypeDistanceSelected
        {
            get { return typeDistanceSelected; }
            set { typeDistanceSelected = value; RaisePropertyChanged(); }
        }
        public List<string> TypeAlterBands
        {
            get { return typeAlterBands; }
            set { typeAlterBands = value; RaisePropertyChanged(); }
        }
        public bool EnabledTolerance
        {
            get { return enabledTolerance; }
            set { enabledTolerance = value; RaisePropertyChanged(); }
        }
        public bool EnabledDistance
        {
            get { return enabledDistance; }
            set { enabledDistance = value; RaisePropertyChanged(); }
        }
        public bool EnabledCluster
        {
            get { return enabledCluster; }
            set { enabledCluster = value; RaisePropertyChanged(); }
        }

        public bool Channel1Segmentation
        {
            get { return channel1Segmentation; }
            set { channel1Segmentation = value; RaisePropertyChanged(); 
                if (segmentationBandsIRCommand != null) { segmentationBandsIRCommand.Value.RaiseCanExecuteChanged(); }
                if (segmentationBandsCommand != null) { segmentationBandsCommand.Value.RaiseCanExecuteChanged(); }
            }
        }
        public bool Channel2Segmentation
        {
            get { return channel2Segmentation; }
            set { channel2Segmentation = value; RaisePropertyChanged();
            if (segmentationBandsIRCommand != null) { segmentationBandsIRCommand.Value.RaiseCanExecuteChanged();
            if (segmentationBandsCommand != null) { segmentationBandsCommand.Value.RaiseCanExecuteChanged(); }
            }
            }
        }
        public bool Channel3Segmentation
        {
            get { return channel3Segmentation; }
            set { channel3Segmentation = value; RaisePropertyChanged();
            if (segmentationBandsIRCommand != null) { segmentationBandsIRCommand.Value.RaiseCanExecuteChanged();
            if (segmentationBandsCommand != null) { segmentationBandsCommand.Value.RaiseCanExecuteChanged(); }
            }
            }
        }
        public List<string> TypeSegmentationSg
        {
            get { return typeSegmentationSg; }
            set { typeSegmentationSg = value; RaisePropertyChanged(); }
        }
        public string TypeSegmentationSgSelected
        {
            get { return typeSegmentationSgSelected; }
            set { typeSegmentationSgSelected = value; RaisePropertyChanged();
             if (value == "Kmeans") { EnabledClusterSegmentation = true; EnabledDistanceSegmentation = true; EnabledToleranceSegmentation = true; }
             if (value == "Kmodes") { EnabledClusterSegmentation = true; EnabledDistanceSegmentation = false; EnabledToleranceSegmentation = true; }
            }
        }
        public int ClustersSegmentation
        {
            get { return clustersSegmentation; }
            set { clustersSegmentation = value; RaisePropertyChanged();
            this.kmeansBandsService.Clusters = value; this.kmodesBandsService.Clusters = value;
            }
        }
        public double ToleranceSegmentation
        {
            get { return toleranceSegmentation; }
            set { toleranceSegmentation = value; RaisePropertyChanged();
            this.kmeansBandsService.Tolerance = value; this.kmodesBandsService.Tolerance = value;
            }
        }
        public List<string> TypeDistanceSegmentation
        {
            get { return typeDistanceSegmentation; }
            set { typeDistanceSegmentation = value; RaisePropertyChanged();
            
            }
        }
        public string TypeDistanceSegmentationSelected
        {
            get { return typeDistanceSegmentationSelected; }
            set { typeDistanceSegmentationSelected = value; RaisePropertyChanged();
            this.kmeansBandsService.TypeDistance = value; 
            }
        }
        public bool EnabledClusterSegmentation
        {
            get { return enabledClusterSegmentation; }
            set { enabledClusterSegmentation = value; RaisePropertyChanged(); }
        }
        public bool EnabledToleranceSegmentation
        {
            get { return enabledToleranceSegmentation; }
            set { enabledToleranceSegmentation = value; RaisePropertyChanged(); }
        }
        public bool EnabledDistanceSegmentation
        {
            get { return enabledDistanceSegmentation; }
            set { enabledDistanceSegmentation = value; RaisePropertyChanged(); }
        }


        public ObservableCollection<ItemChannel> ListChanelsReturn
        {
            get { return listChanelsReturn; }
            set { listChanelsReturn = value; RaisePropertyChanged(); }
        }
        public bool Channel1MultipleSelected
        {
            get { return channel1MultipleSelected; }
            set { channel1MultipleSelected = value; RaisePropertyChanged();
            if (segmentationMultipleBandsCommand != null) { segmentationMultipleBandsCommand.Value.RaiseCanExecuteChanged(); }
            }
        }
        public bool Channel2MultipleSelected
        {
            get { return channel2MultipleSelected; }
            set { channel2MultipleSelected = value; RaisePropertyChanged();
            if (segmentationMultipleBandsCommand != null) { segmentationMultipleBandsCommand.Value.RaiseCanExecuteChanged(); }
            }
        }
        public bool Channel3MultipleSelected
        {
            get { return channel3MultipleSelected; }
            set { channel3MultipleSelected = value; RaisePropertyChanged();
            if (segmentationMultipleBandsCommand != null) { segmentationMultipleBandsCommand.Value.RaiseCanExecuteChanged(); }
            }
        }
        public bool Channel4MultipleSelected
        {
            get { return channel4MultipleSelected; }
            set { channel4MultipleSelected = value; RaisePropertyChanged();
            if (segmentationMultipleBandsCommand != null) { segmentationMultipleBandsCommand.Value.RaiseCanExecuteChanged(); }
            }
        }
        public bool Channel5MultipleSelected
        {
            get { return channel5MultipleSelected; }
            set { channel5MultipleSelected = value; RaisePropertyChanged();
            if (segmentationMultipleBandsCommand != null) { segmentationMultipleBandsCommand.Value.RaiseCanExecuteChanged(); }
            }
        }
        public bool Channel6MultipleSelected
        {
            get { return channel6MultipleSelected; }
            set { channel6MultipleSelected = value; RaisePropertyChanged();
            if (segmentationMultipleBandsCommand != null) { segmentationMultipleBandsCommand.Value.RaiseCanExecuteChanged(); }
            }
        }
        public ItemChannel Channel1ReturnSelected
        {
            get { return channel1ReturnSelected; }
            set { channel1ReturnSelected = value; RaisePropertyChanged(); }
        }
        public ItemChannel Channel2ReturnSelected
        {
            get { return channel2ReturnSelected; }
            set { channel2ReturnSelected = value; RaisePropertyChanged(); }
        }
        public ItemChannel Channel3ReturnSelected
        {
            get { return channel3ReturnSelected; }
            set { channel3ReturnSelected = value; RaisePropertyChanged(); }
        }


        public string Status
        {
            get { return status; }
            set { status = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<ItemProportions> ProportionsResult
        {
            get { return proportionsResult; }
            set { 
                proportionsResult = value; RaisePropertyChanged();
                if (value == null) { VisibilityProportionsResults = Visibility.Collapsed; WidthProportionsResults = 0; } else { VisibilityProportionsResults = Visibility.Visible; WidthProportionsResults = 300; }
            }
        }
        public int WidthProportionsResults
        {
            get { return widthProportionsResults; }
            set { widthProportionsResults = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<ItemExternLibrary> ListExternResources
        {
            get { return listExternResources; }
            set { listExternResources = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<ItemRampColor> TypeRampNDVIColors
        {
            get { return typeRampNDVIColors; }
            set { typeRampNDVIColors = value; RaisePropertyChanged(); }
        }
        public ItemRampColor TypeRampNDVIColorsSelected
        {
            get { return typeRampNDVIColorsSelected; }
            set
            {
                typeRampNDVIColorsSelected = value; this.ndviService.ColorRamp = value.FunctionRamp;
                RaisePropertyChanged(); }
        }
        public ObservableCollection<ItemRampColor> TypeRampVIColors
        {
            get { return typeRampVIColors; }
            set { typeRampVIColors = value; RaisePropertyChanged(); }
        }
        public ItemRampColor TypeRampVIColorsSelected
        {
            get { return typeRampVIColorsSelected; }
            set
            {
                typeRampVIColorsSelected = value; this.viService.ColorRamp = value.FunctionRamp;
                RaisePropertyChanged();
            }
        }
        public ItemRampColor RampSelected
        {
            get { return rampSelected; }
            set { rampSelected = value; RaisePropertyChanged(); }
        }
        public double MinimumRamp
        {
            get { return minimumRamp; }
            set { minimumRamp = value; RaisePropertyChanged(); }
        }
        public double MaximunRamp
        {
            get { return maximunRamp; }
            set { maximunRamp = value; RaisePropertyChanged(); }
        }
        public byte ValueAdjustmentVI
        {
            get { return valueAdjustmentVI; }
            set { valueAdjustmentVI = value; this.rampVI.MaxValueScale = value; RaisePropertyChanged(); }
        }
        public bool EnabledAdjustRampNDVI
        {
            get { return enabledAdjustRampNDVI; }
            set
            {
                enabledAdjustRampNDVI = value; this.ndviService.RampAdjustment = value;
                RaisePropertyChanged(); }
        }
        public bool EnabledAdjustRampVI
        {
            get { return enabledAdjustRampVI; }
            set
            {
                enabledAdjustRampVI = value; this.viService.RampAdjustment = value;
                RaisePropertyChanged();
            }
        }
        public string UrlQuiz
        {
            get { return urlQuiz; }
            set { if (value != this.urlQuiz) { urlQuiz = value; RaisePropertyChanged(); } }
        }

        public string SelectedTypeAlter10
        {
            get { return selectedTypeAlter10; }
            set { selectedTypeAlter10 = value; RaisePropertyChanged(); alterBandsCommand.Value.RaiseCanExecuteChanged(); }
        }
        public string SelectedTypeAlter11
        {
            get { return selectedTypeAlter11; }
            set { selectedTypeAlter11 = value; RaisePropertyChanged(); alterBandsCommand.Value.RaiseCanExecuteChanged(); }
        }
        public string SelectedTypeAlter12
        {
            get { return selectedTypeAlter12; }
            set { selectedTypeAlter12 = value; RaisePropertyChanged(); alterBandsCommand.Value.RaiseCanExecuteChanged(); }
        }
        public string SelectedTypeAlter13
        {
            get { return selectedTypeAlter13; }
            set { selectedTypeAlter13 = value; RaisePropertyChanged(); alterBandsCommand.Value.RaiseCanExecuteChanged(); }
        }
        public string SelectedTypeAlter20
        {
            get { return selectedTypeAlter20; }
            set { selectedTypeAlter20 = value; RaisePropertyChanged(); alterBandsIRCommand.Value.RaiseCanExecuteChanged(); }
        }
        public string SelectedTypeAlter21
        {
            get { return selectedTypeAlter21; }
            set { selectedTypeAlter21 = value; RaisePropertyChanged(); alterBandsIRCommand.Value.RaiseCanExecuteChanged(); }
        }
        public string SelectedTypeAlter22
        {
            get { return selectedTypeAlter22; }
            set { selectedTypeAlter22 = value; RaisePropertyChanged(); alterBandsIRCommand.Value.RaiseCanExecuteChanged(); }
        }
            public string SelectedTypeAlter23
        {
            get { return selectedTypeAlter23; }
            set { selectedTypeAlter23 = value; RaisePropertyChanged(); alterBandsIRCommand.Value.RaiseCanExecuteChanged(); }
        }
 
        

        public Visibility VisibilityProportionsResults
        {
            get { return visibilityProportionsResults; }
            set { visibilityProportionsResults = value; RaisePropertyChanged(); }
        }
        public Visibility VisibilityProcessedBitmap
        {
            get { return visibilityProcessedBitmap; }
            set
            {
                if (BitmapProcessed != null)
                {
                    visibilityProcessedBitmap = value;
                    RaisePropertyChanged();
                }
            }
        }
        public Visibility VisibilityOptionsSegmentation
        {
            get { return visibilityOptionsSegmentation; }
            set {collapseExceptSegmentation();visibilityOptionsSegmentation = value; RaisePropertyChanged();}
        }
        public Visibility VisibilityOptionsRampNDVI
        {
            get { return visibilityOptionsRampNDVI; }
            set { collapseExceptRampNDVI(); visibilityOptionsRampNDVI = value; RaisePropertyChanged(); }
        }
        public Visibility VisibilityOptionsRampVI
        {
            get { return visibilityOptionsRampVI; }
            set { collapseExceptRampVI(); visibilityOptionsRampVI = value; RaisePropertyChanged(); }
        }
        public Visibility VisibilityQuiz
        {
            get { return visibilityQuiz; }
            set { visibilityQuiz = value; RaisePropertyChanged(); }
        }
        public Visibility VisibilityContact
        {
            get { return visibilityContact; }
            set { visibilityContact = value; RaisePropertyChanged(); }
        }
        public Visibility VisibilityAlterBands
        {
            get { return visibilityAlterBands; }
            set { visibilityAlterBands = value; RaisePropertyChanged(); }
        }

        private void collapseExceptSegmentation()
        {
            if (this.VisibilityOptionsRampNDVI == Visibility.Visible) { this.VisibilityOptionsRampNDVI = Visibility.Collapsed; }
            if (this.VisibilityOptionsRampVI == Visibility.Visible) { this.VisibilityOptionsRampVI = Visibility.Collapsed; }
        }
        private void collapseExceptRampNDVI()
        {
            if (this.VisibilityOptionsSegmentation == Visibility.Visible) { this.VisibilityOptionsSegmentation = Visibility.Collapsed; }
            if (this.VisibilityOptionsRampVI == Visibility.Visible) { this.VisibilityOptionsRampVI = Visibility.Collapsed; }
        }
        private void collapseExceptRampVI()
        {
            if (this.VisibilityOptionsRampNDVI == Visibility.Visible) { this.VisibilityOptionsRampNDVI = Visibility.Collapsed; }
            if (this.VisibilityOptionsSegmentation == Visibility.Visible) { this.VisibilityOptionsSegmentation = Visibility.Collapsed; }
        }
        


        public ICommand NDVICommand
        {
            get
            {
                return ndviCommand.Value;
            }
        }
        public void NDVIExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ProportionsResult = null;
            bitmapNIR.Freeze();
            bitmapOriginal.Freeze();
            var task = Task.Run(() =>
            {
                if (typeSegmentationSelected == "Progresiva")
                {
                    BitmapProcessed = this.ndviService.getNDVI(bitmapOriginal, bitmapNIR);
                }
                else
                {
                    BitmapProcessed = this.ndviService.getNDVI(bitmapOriginal, bitmapNIR, new Segmentation()
                    {
                        Clusters = this.clusters,
                        Tolerance = this.tolerance,
                        TypeSegmentation = (ImageProcessor.Models.Segmentation.Segmentations)Enum.Parse(typeof(ImageProcessor.Models.Segmentation.Segmentations), this.typeSegmentationSelected),
                        TypeDistance = this.typeDistanceSelected
                    });
                    ProportionsResult = new ObservableCollection<ItemProportions>(this.ndviService.Results);

                }
                this.RampSelected = this.typeRampNDVIColorsSelected;
                MinimumRamp = this.ndviService.MinValue; MaximunRamp = this.ndviService.MaxValue;
                this.pwService.closeProgress();

            });

            this.pwService.showProgress(Status);
         }
        private bool NDVICanExecute()
        {
            return bitmapNIR!=null && bitmapOriginal!=null;
        }

        public ICommand VICommand
        {
            get
            {
                return viCommand.Value;
            }
        }
        public void VIExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ProportionsResult = null;
            bitmapNIR.Freeze();
            bitmapOriginal.Freeze();
            var task = Task.Run(() =>
            {
                if (typeSegmentationSelected == "Progresiva")
                {
                    BitmapProcessed = this.viService.getVI(bitmapOriginal, bitmapNIR);
                }
                else
                {
                    BitmapProcessed = this.viService.getVI(bitmapOriginal, bitmapNIR, new Segmentation()
                    {
                        Clusters = this.clusters,
                        Tolerance = this.tolerance,
                        TypeSegmentation = (ImageProcessor.Models.Segmentation.Segmentations)Enum.Parse(typeof(ImageProcessor.Models.Segmentation.Segmentations), this.typeSegmentationSelected),
                        TypeDistance = this.typeDistanceSelected
                    });
                    ProportionsResult = new ObservableCollection<ItemProportions>(this.viService.Results);

                }
                this.RampSelected = this.typeRampVIColorsSelected;
                MinimumRamp = this.viService.MinValue; MaximunRamp = this.viService.MaxValue;
                 this.pwService.closeProgress();
            });
            this.pwService.showProgress(Status);
         }
        private bool VICanExecute()
        {
            return bitmapNIR != null && bitmapOriginal != null;
        }


        public ICommand NDVINIRCommand
        {
            get
            {
                return ndviNIRCommand.Value;
            }
        }
        public void NDVINIRExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ProportionsResult = null;
            bitmapNIR.Freeze();
            var task = Task.Run(() =>
            {
                if (typeSegmentationSelected == "Progresiva")
                {
                    BitmapProcessed = this.ndviService.getNDVI(bitmapNIR);
                }
                else
                {
                    BitmapProcessed = this.ndviService.getNDVI(bitmapNIR, new Segmentation()
                    {
                        Clusters = this.clusters,
                        Tolerance = this.tolerance,
                        TypeSegmentation = (ImageProcessor.Models.Segmentation.Segmentations)Enum.Parse(typeof(ImageProcessor.Models.Segmentation.Segmentations), this.typeSegmentationSelected),
                        TypeDistance = this.typeDistanceSelected
                    });
                    ProportionsResult = new ObservableCollection<ItemProportions>(this.ndviService.Results);
                }
                this.RampSelected = this.typeRampNDVIColorsSelected;
                MinimumRamp = this.ndviService.MinValue; MaximunRamp = this.ndviService.MaxValue;
                this.pwService.closeProgress();

            });
            this.pwService.showProgress(Status);
        }
        private bool NDVINIRCanExecute()
        {
            return bitmapNIR != null;
        }


        public ICommand VINIRCommand
        {
            get
            {
                return viNIRCommand.Value;
            }
        }
        public void  VINIRExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ProportionsResult = null;
            bitmapNIR.Freeze();
             var task = Task.Run(() =>
            {
                if (typeSegmentationSelected == "Progresiva")
                {
                    BitmapProcessed = this.viService.getVI(bitmapNIR);
                }
                else
                {
                    BitmapProcessed = this.viService.getVI(bitmapNIR, new Segmentation() { 
                        Clusters = this.clusters, 
                        Tolerance = this.tolerance,
                        TypeSegmentation = (ImageProcessor.Models.Segmentation.Segmentations)Enum.Parse(typeof(ImageProcessor.Models.Segmentation.Segmentations), this.typeSegmentationSelected),
                        TypeDistance=this.typeDistanceSelected
                    });
                    ProportionsResult = new ObservableCollection<ItemProportions>(this.viService.Results);

                }
                this.RampSelected = this.typeRampVIColorsSelected;
                MinimumRamp = this.viService.MinValue; MaximunRamp = this.viService.MaxValue;
                this.pwService.closeProgress();
            });
             this.pwService.showProgress(Status);
        }
        private bool VINIRCanExecute()
        {
            return bitmapNIR != null;
        }

        public ICommand WithoutIRCommand
        {
            get
            {
                return withoutIRCommand.Value;
            }
        }
        public void WithoutIRExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ProportionsResult = null;
            bitmapNIR.Freeze();
            BitmapProcessed = bitmapNIR;

            this.RampSelected = null;
            MinimumRamp = 0; MaximunRamp = 0;
        }
        private bool WithoutIRCanExecute()
        {
            return bitmapNIR != null;
        }

        public ICommand SegmentationBandsCommand
        {
            get
            {
                return segmentationBandsCommand.Value;
            }
        }
        public void SegmentationBandsExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ProportionsResult = null;
            
            bitmapOriginal.Freeze();
            var task = Task.Run(() =>
            {

                if (this.TypeSegmentationSgSelected.ToLower() == "kmeans")
                {
                    BitmapProcessed = this.kmeansBandsService.getImage(bitmapOriginal, this.channel1Segmentation, this.channel2Segmentation, this.channel3Segmentation);
                    ProportionsResult = new ObservableCollection<ItemProportions>(this.kmeansBandsService.Results);
                }
                else
                {
                    BitmapProcessed = this.kmodesBandsService.getImage(bitmapOriginal, this.channel1Segmentation, this.channel2Segmentation, this.channel3Segmentation);
                    ProportionsResult = new ObservableCollection<ItemProportions>(this.kmodesBandsService.Results);
                }
                this.pwService.closeProgress();
            });
            this.RampSelected = null;
            MinimumRamp = 0; MaximunRamp = 0;
            this.pwService.showProgress(Status);

        }
        private bool SegmentationBandsCanExecute()
        {
            return bitmapOriginal != null &&
                (Channel1Segmentation != false || Channel2Segmentation != false || Channel3Segmentation != false);
        }

        public ICommand SegmentationBandsIRCommand
        {
            get
            {
                return segmentationBandsIRCommand.Value;
            }
        }
        public void SegmentationBandsIRExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ProportionsResult = null;
            bitmapNIR.Freeze();
            var task = Task.Run(() =>
            {
                if (this.TypeSegmentationSgSelected.ToLower() == "kmeans")
                {
                    BitmapProcessed = this.kmeansBandsService.getImage(bitmapNIR, this.channel1Segmentation, this.channel2Segmentation, this.channel3Segmentation);
                    ProportionsResult = new ObservableCollection<ItemProportions>(this.kmeansBandsService.Results);
                }
                else
                {
                    BitmapProcessed = this.kmodesBandsService.getImage(bitmapNIR, this.channel1Segmentation, this.channel2Segmentation, this.channel3Segmentation);
                    ProportionsResult = new ObservableCollection<ItemProportions>(this.kmodesBandsService.Results);
                }
                this.pwService.closeProgress();

            });
             this.RampSelected = null;
            MinimumRamp = 0; MaximunRamp = 0;
            this.pwService.showProgress(Status);

        }
        private bool SegmentationBandsIRCanExecute()
        {
            return bitmapNIR != null &&
                (Channel1Segmentation != false || Channel2Segmentation != false || Channel3Segmentation != false );
        }

        public ICommand SegmentationMultipleBandsCommand
        {
            get
            {
                return segmentationMultipleBandsCommand.Value;
            }
        }
        public void SegmentationMultipleBandsExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ProportionsResult = null;

            bitmapOriginal.Freeze();
            bitmapNIR.Freeze();
            var task = Task.Run(() =>
            {

                if (this.TypeSegmentationSgSelected.ToLower() == "kmeans")
                {
                    BitmapProcessed = this.kmeansBandsService.getImage(bitmapNIR, bitmapOriginal, this.Channel1MultipleSelected, this.Channel2MultipleSelected, this.Channel3MultipleSelected,
                                                                        this.Channel4MultipleSelected, this.Channel5MultipleSelected, this.Channel6MultipleSelected,
                        new List<int>() { this.Channel1ReturnSelected.Index, this.Channel2ReturnSelected.Index, this.Channel3ReturnSelected.Index });
                    ProportionsResult = new ObservableCollection<ItemProportions>(this.kmeansBandsService.Results);
                }
                else
                {
                    BitmapProcessed = this.kmodesBandsService.getImage(bitmapNIR, bitmapOriginal, this.Channel1MultipleSelected, this.Channel2MultipleSelected, this.Channel3MultipleSelected,
                                                                        this.Channel4MultipleSelected, this.Channel5MultipleSelected, this.Channel6MultipleSelected,
                        new List<int>() { this.Channel1ReturnSelected.Index, this.Channel2ReturnSelected.Index, this.Channel3ReturnSelected.Index });
                    ProportionsResult = new ObservableCollection<ItemProportions>(this.kmodesBandsService.Results);
                }
                this.pwService.closeProgress();
            });

            this.RampSelected = null;
            MinimumRamp = 0; MaximunRamp = 0;
            this.pwService.showProgress(Status);

        }
        private bool SegmentationMultipleBandsCanExecute()
        {
            return bitmapOriginal != null && bitmapNIR!=null
                && (Channel1MultipleSelected != false || Channel2MultipleSelected != false || Channel3MultipleSelected != false ||
                Channel4MultipleSelected != false || Channel5MultipleSelected != false || Channel6MultipleSelected != false);
        }

        public ICommand ChangeColorCommand
        {
            get
            {
                return changeColorCommand.Value;
            }
        }
        public void ChangeColorExecute(SolidColorBrush parameter)
        {
            var returnColor = colorDialogService.showDialog(parameter.Color);
            if (returnColor == null)
                return;

            for (int i = 0; i < proportionsResult.Count; i++)
            {
                if ((int)proportionsResult[i].Colors[2] == returnColor.Value.R &&
                  (int)proportionsResult[i].Colors[1] == returnColor.Value.G && (int)proportionsResult[i].Colors[0] == returnColor.Value.B)
                {
                    messageService.showMessage("Selección color", "No puede haber dos colores iguales. Por favor, seleccione otro color.");
                    return;
                }
            }

            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            parameter.Freeze();
            BitmapProcessed.Freeze();
            var task = Task.Run(() =>
            {
                BitmapProcessed = this.changeColService.changeColor(BitmapProcessed, parameter.Color, (Color)returnColor, proportionsResult.ToList<ItemProportions>());
                ProportionsResult = new ObservableCollection<ItemProportions>(this.changeColService.Results);
                this.pwService.closeProgress();
            });
            this.pwService.showProgress(Status, "Procesando imagen...");

        }
        private bool ChangeColorCanExecute(SolidColorBrush parameter)
        {
            return true;
        }


        public ICommand SaveImageCommand
        {
            get
            {
                return saveImageCommand.Value;
            }
        }
        public void SaveImageCommandExecute()
        {
            this.saveImageService.saveImage(bitmapProcessed, Status);
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
        }
        private bool SaveImageCommandCanExecute()
        {
            return true;
        }


        public ICommand ExpandImageCommand
        {
            get
            {
                return expandImageCommand.Value;
            }
        }
        public void ExpandImageCommandExecute()
        {
            this.navigationImageService.navigationTo(bitmapProcessed);
        }
        private bool ExpandImageCommandCanExecute()
        {
            return bitmapProcessed!=null;
        }

        public ICommand ExchangeImagesCommand
        {
            get
            {
                return exchangeImagesCommand.Value;
            }
        }
        public void ExchangeImagesCommandExecute()
        {
            var imageAux = BitmapOriginal;
            BitmapOriginal = BitmapNIR;
            BitmapNIR = imageAux;
            imageAux = null;

        }
        private bool ExchangeImagesCommandCanExecute()
        {
            return bitmapNIR != null && bitmapOriginal != null;
        }

        public ICommand AlterBandsCommand
        {
            get
            {
                return alterBandsCommand.Value;
            }
        }
        public void AlterBandsCommandExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ProportionsResult = null;

            bitmapOriginal.Freeze();
            var task = Task.Run(() =>
            {
                BitmapProcessed = this.alterBandsService.getImage(bitmapOriginal,new int[] { getBand(selectedTypeAlter10),
                getBand(selectedTypeAlter11),getBand(selectedTypeAlter12),getBand(selectedTypeAlter13)});
                this.pwService.closeProgress();
            });

            this.pwService.showProgress(Status);

        }
        private bool AlterBandsCommandCanExecute()
        {
            return bitmapOriginal!=null && !String.IsNullOrEmpty(selectedTypeAlter10) && !String.IsNullOrEmpty(selectedTypeAlter11)
                 && !String.IsNullOrEmpty(selectedTypeAlter12) && !String.IsNullOrEmpty(selectedTypeAlter13); ;
        }

        public ICommand AlterBandsIRCommand
        {
            get
            {
                return alterBandsIRCommand.Value;
            }
        }
        public void AlterBandsIRCommandExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            ProportionsResult = null;

            bitmapNIR.Freeze();
            var task = Task.Run(() =>
            {
                BitmapProcessed = this.alterBandsService.getImage(bitmapNIR, new int[] { getBand(selectedTypeAlter20),
                getBand(selectedTypeAlter21),getBand(selectedTypeAlter22),getBand(selectedTypeAlter23)});
                this.pwService.closeProgress();
            });

            this.pwService.showProgress(Status);

        }
        private bool AlterBandsIRCommandCanExecute()
        {
            return bitmapNIR != null && !String.IsNullOrEmpty(selectedTypeAlter20) && !String.IsNullOrEmpty(selectedTypeAlter21)
                 && !String.IsNullOrEmpty(selectedTypeAlter22) && !String.IsNullOrEmpty(selectedTypeAlter23);
        }

        private int getBand(string bandText)
        {
            if(bandText=="Banda 1")
            {
                return 0;
            }
            else if(bandText == "Banda 2")
            {
                return 1;
            }
            else if (bandText == "Banda 3")
            {
                return 2;
            }
            else if (bandText == "Banda 4")
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }


        public ICommand SetOriginalImageCommand
        {
            get
            {
                return setOriginalImageCommand.Value;
            }
        }
        public void SetOriginalImageCommandExecute()
        {
            BitmapOriginal = BitmapProcessed;
        }
        private bool SetOriginalImageCommandCanExecute()
        {
            return true;
        }

        public ICommand SetIRImageCommand
        {
            get
            {
                return setIRImageCommand.Value;
            }
        }
        public void SetIRImageCommandExecute()
        {
            BitmapNIR = BitmapProcessed;
        }
        private bool SetIRImageCommandCanExecute()
        {
            return true;
        }

        public ICommand ReturnWindowCommand
        {
            get
            {
                return returnWindowCommand.Value;
            }
        }
        public void ReturnWindowCommandExecute(Window parameter)
        {
            navigationService.NavigateTo(parameter, new MainWindow(bitmapOriginal,bitmapNIR));

            
        }
        private bool ReturnWindowCommandCanExecute(Window parameter)
        {
            return true;
        }

        public ICommand CloseCommand
        {
            get
            {
                return closeCommand.Value;
            }
        }
        public void CloseCommandExecute()
        {
            this.closeService.close();
        }
        private bool CloseCommandCanExecute()
        {
            return true;
        }


        public ICommand MouseEnterCommand
        {
            get
            {
                return mouseEnterCommand.Value;
            }
        }
        public void MouseEnterCommandExecute()
        {
            this.mouseActionsService.mouseEnter();
        }
        private bool MouseEnterCommandCanExecute()
        {
            return true;
        }

        public ICommand MouseLeaveCommand
        {
            get
            {
                return mouseLeaveCommand.Value;
            }
        }
        public void MouseLeaveCommandExecute()
        {
            this.mouseActionsService.mouseLeave();
        }
        private bool MouseLeaveCommandCanExecute()
        {
            return true;
        }

        public ICommand BrowserCommand
        {
            get
            {
                return browserCommand.Value;
            }
        }
        public void BrowserCommandCommandExecute(string parameter)
        {
            this.browserService.openUrl(this.listExternResources.Where(C=>C.Title.Contains(parameter)).First<ItemExternLibrary>().Url);
        }
        private bool BrowserCommandCanExecute(string parameter)
        {
            return !String.IsNullOrEmpty(parameter);
        }

        public ICommand OpenURLCommand
        {
            get
            {
                return openURL.Value;
            }
        }
        public void OpenURLCommandExecute(string parameter)
        {
            this.browserService.openUrl(parameter);
        }
        private bool OpenURLCommandCanExecute(string parameter)
        {
            return !String.IsNullOrEmpty(parameter);
        }


        public ICommand OpenNotepadCommand
        {
            get
            {
                return openNotepad.Value;
            }
        }
        public void OpenNotepadCommandExecute(string parameter)
        {
            var opened=this.notepadService.openFile(parameter);
            if (!opened) { this.messageService.showMessage("Error", "No se encontró el archivo"); }
        }
        private bool OpenNotepadCommandCanExecute(string parameter)
        {
            return !String.IsNullOrEmpty(parameter);
        }



        public ICommand VisibilityProcessedImageCommand
        {
            get
            {
                return visibiliyProcessedImageCommand.Value;
            }
        }
        public void VisibilityProcessedImageExecute()
        {
            VisibilityProcessedBitmap = Visibility.Collapsed;
        }
        private bool VisibilityProcessedImageCanExecute()
        {
            return true;
        }

        public ICommand VisibilityOptionsSegmentationCommand
        {
            get
            {
                return visibiliyOptionsSegmentationCommand.Value;
            }
        }
        public void VisibilityOptionsSegmentationExecute()
        {
            VisibilityOptionsSegmentation = (visibilityOptionsSegmentation == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }
        private bool VisibilityOptionsSegmentationCanExecute()
        {
            return true;
        }

        public ICommand VisibilityOptionsRampNDVICommand
        {
            get
            {
                return visibiliyOptionsRampNDVICommand.Value;
            }
        }
        public void VisibilityOptionsRampNDVIExecute()
        {
            VisibilityOptionsRampNDVI = (VisibilityOptionsRampNDVI == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }
        private bool VisibilityOptionsRampNDVICanExecute()
        {
            return true;
        }

        public ICommand VisibilityOptionsRampVICommand
        {
            get
            {
                return visibiliyOptionsRampVICommand.Value;
            }
        }
        public void VisibilityOptionsRampVIExecute()
        {
            VisibilityOptionsRampVI = (VisibilityOptionsRampVI == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }
        private bool VisibilityOptionsRampVICanExecute()
        {
            return true;
        }

        public ICommand VisibilityQuizCommand
        {
            get
            {
                return visibiliyQuizCommand.Value;
            }
        }
        public void VisibilityQuizCommandExecute()
        {
            if (VisibilityQuiz==Visibility.Collapsed)
            {
                VisibilityQuiz = Visibility.Visible;
                this.UrlQuiz = "http://1drv.ms/1H15oYC";
            }
            else
            {
                VisibilityQuiz = Visibility.Collapsed;
            }
         }
        private bool VisibilityQuizCommandCanExecute()
        {
            return true;
        }

        public ICommand VisibilityContactCommand
        {
            get
            {
                return visibiliyContactCommand.Value;
            }
        }
        public void VisibilityContactCommandExecute()
        {
            VisibilityContact=(VisibilityContact == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }
        private bool VisibilityContactCommandCanExecute()
        {
            return true;
        }


        public ICommand VisibilityAlterBandsCommand
        {
            get
            {
                return visibiliyAlterBandsCommand.Value;
            }
        }
        public void VisibilityAlterBandsCommandExecute()
        {
            VisibilityAlterBands = (VisibilityAlterBands == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
        }
        private bool VisibilityAlterBandsCommandCanExecute()
        {
            return true;
        }


    }
}
