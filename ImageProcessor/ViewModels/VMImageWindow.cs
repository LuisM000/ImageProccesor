using ImageProcessor.Models;
using ImageProcessor.Services;
using ImageProcessor.Services.UI;
using ImageProcessor.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageProcessor.ViewModels
{
    public class VMImageWindow:VMBase
    {
        private BitmapImage bitmap;
        private BitmapImage bitmapProcessed;
        private int clustersKmeans;
        private double toleranceKMeans;
        private List<string> typeDistanceKMeans;
        private string typeDistanceSelectedKMeans;
        private int clustersKModes;
        private double toleranceKmodes;
        private double constKernelMeanS;
        private double sigmaMeanS;
        private double toleranceMeanS;
        private int iterationsMeanS;
        private List<string> typeKernelMeanS;
        private string typeKernelSelectedMeanS;
        private ObservableCollection<ItemProportions> proportionsResult;
        private Visibility visibilityProcessedBitmap;
        private string status;
        

      
        private IKMeans kmeansService;
        private IKModes kmodesService;
        private IMeanShift meanshiftService;
        private IChangeColor changeColService;
        private IColordialog colorDialogService;
        private ISaveImage saveImageService;
        private IProgressWindow pwService;
        private IMessage messageService;
        private IMouseActions mouseActionsService;


        
        private Lazy<DelegateCommand> kmeansCommand;
        private Lazy<DelegateCommand> kmodesCommand;
        private Lazy<DelegateCommand> meanShiftCommand;
        private Lazy<DelegateCommandParam<SolidColorBrush>> changeColorCommand;
        private Lazy<DelegateCommand> visibiliyProcessedImageCommand;
        private Lazy<DelegateCommand> mouseEnterCommand;
        private Lazy<DelegateCommand> mouseLeaveCommand;
        private Lazy<DelegateCommand> saveImageCommand;


        public VMImageWindow(IProgressWindow pwService, IColordialog colorDialogService,IMessage messageService,
                            IKMeans kmeansService, IKModes kmodesService,
                            IMeanShift meanshiftService, IChangeColor changeColService, ISaveImage saveImageService,
                             IMouseActions mouseActionsService)
        {
            visibilityProcessedBitmap = Visibility.Collapsed;
            this.ClustersKMeans = 5;
            this.ToleranceKMeans = 0.05;
            this.ClustersKModes = 5;
            this.ToleranceKmodes = 0.05;
            this.TypeDistanceKMeans = new List<string>()
            {
                "BrayCurtis",
                "Chessboard",
                "Correlation",
                "Cosine",
                "Euclidean",
                "Manhattan",
                "SquareEuclidean",
            };
            this.TypeDistanceSelectedKMeans = "Cosine";
            this.constKernelMeanS = 1.0;
            this.SigmaMeanS = 20;
            this.ToleranceMeanS = 0.05;
            this.IterationsMeanS = 100;
            this.TypeKernelMeanS = new List<string>()
            {
                "EpanechnikovKernel",
                "GaussianKernel",
                "UniformKernel",
            };
            this.TypeKernelSelectedMeanS = "GaussianKernel";
           
            this.colorDialogService = colorDialogService;
            this.pwService = pwService;
            this.messageService = messageService;
            this.kmeansService = kmeansService;
            this.kmodesService = kmodesService;
            this.meanshiftService = meanshiftService;
            this.changeColService = changeColService;
            this.saveImageService = saveImageService;
            this.mouseActionsService = mouseActionsService;


            this.kmeansCommand = new Lazy<DelegateCommand>(
                  () =>
                  new DelegateCommand(KMeansExecute, KMeansCanExecute));
            this.kmodesCommand = new Lazy<DelegateCommand>(
                  () =>
                  new DelegateCommand(KModesExecute, KModesCanExecute));
            this.meanShiftCommand = new Lazy<DelegateCommand>(
             () =>
             new DelegateCommand(MeanShiftExecute, MeanShiftCanExecute));
            this.changeColorCommand = new Lazy<DelegateCommandParam<SolidColorBrush>>(
            () =>
            new DelegateCommandParam<SolidColorBrush>(ChangeColorExecute, ChangeColorCanExecute));
            this.saveImageCommand = new Lazy<DelegateCommand>(
            () =>
            new DelegateCommand(SaveImageCommandExecute, SaveImageCommandCanExecute));
            this.visibiliyProcessedImageCommand = new Lazy<DelegateCommand>(
             () =>
             new DelegateCommand(VisibilityProcessedImageExecute, VisibilityProcessedImageCanExecute));
            this.mouseEnterCommand = new Lazy<DelegateCommand>(
            () =>
             new DelegateCommand(MouseEnterCommandExecute, MouseEnterCommandCanExecute));
                this.mouseLeaveCommand = new Lazy<DelegateCommand>(
             () =>
             new DelegateCommand(MouseLeaveCommandExecute, MouseLeaveCommandCanExecute));
        }


      
        public BitmapImage Bitmap
        {
            get { return bitmap; }
            set { bitmap = value; RaisePropertyChanged(); kmeansCommand.Value.RaiseCanExecuteChanged();
            kmodesCommand.Value.RaiseCanExecuteChanged(); meanShiftCommand.Value.RaiseCanExecuteChanged();
            }
        }
        public BitmapImage BitmapProcessed
        {
            get { return bitmapProcessed; }
            set { bitmapProcessed = value; RaisePropertyChanged(); VisibilityProcessedBitmap = Visibility.Visible; 
                }
        }
        public int ClustersKMeans
        {
            get { return clustersKmeans; }
            set { clustersKmeans = value; RaisePropertyChanged(); }
        }
        public double ToleranceKMeans
        {
            get { return toleranceKMeans; }
            set { toleranceKMeans = value; RaisePropertyChanged(); }
        }
        public List<string> TypeDistanceKMeans
        {
            get { return typeDistanceKMeans; }
            set { typeDistanceKMeans = value; RaisePropertyChanged(); }
        }
        public string TypeDistanceSelectedKMeans
        {
            get { return typeDistanceSelectedKMeans; }
            set { typeDistanceSelectedKMeans = value; RaisePropertyChanged(); }
        }
        public int ClustersKModes
        {
            get { return clustersKModes; }
            set { clustersKModes = value; RaisePropertyChanged(); }
        }
        public double ToleranceKmodes
        {
            get { return toleranceKmodes; }
            set { toleranceKmodes = value; RaisePropertyChanged(); }
        }
        public double ConstKernelMeanS
        {
            get { return constKernelMeanS; }
            set { constKernelMeanS = value; RaisePropertyChanged(); }
        }
        public double SigmaMeanS
        {
            get { return sigmaMeanS; }
            set { sigmaMeanS = value; RaisePropertyChanged(); }
        }
        public double ToleranceMeanS
        {
            get { return toleranceMeanS; }
            set { toleranceMeanS = value; RaisePropertyChanged(); }
        }
        public int IterationsMeanS
        {
            get { return iterationsMeanS; }
            set { iterationsMeanS = value; RaisePropertyChanged(); }
        }
        public List<string> TypeKernelMeanS
        {
            get { return typeKernelMeanS; }
            set { typeKernelMeanS = value; RaisePropertyChanged(); }
        }
        public string TypeKernelSelectedMeanS
        {
            get { return typeKernelSelectedMeanS; }
            set { typeKernelSelectedMeanS = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<ItemProportions> ProportionsResult
        {
            get { return proportionsResult; }
            set { proportionsResult = value; RaisePropertyChanged(); }
        }

        public string Status
        {
            get { return status; }
            set { status = value; RaisePropertyChanged(); }
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
       


        public ICommand KMeansCommand
        {
            get
            {
                return kmeansCommand.Value;
            }
        }
        public void KMeansExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            Bitmap.Freeze();
            var task = Task.Run(() =>
            {
                BitmapProcessed = this.kmeansService.getImage(Bitmap,ClustersKMeans,ToleranceKMeans,typeDistanceSelectedKMeans);
                ProportionsResult = new ObservableCollection<ItemProportions>(this.kmeansService.Results);
                this.pwService.closeProgress();
            });
            this.pwService.showProgress(Status,"Procesando imagen... (puede tardar unos minutos)");
        }
        private bool KMeansCanExecute()
        {
            return Bitmap != null;
        }

        public ICommand KModesCommand
        {
            get
            {
                return kmodesCommand.Value;
            }
        }
        public void KModesExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            Bitmap.Freeze();
            var task = Task.Run(() =>
            {
                BitmapProcessed = this.kmodesService.getImage(Bitmap, clustersKModes, toleranceKmodes);
                ProportionsResult = new ObservableCollection<ItemProportions>(this.kmodesService.Results);
                this.pwService.closeProgress();
            });
            this.pwService.showProgress(Status, "Procesando imagen... (puede tardar unos minutos)");
        }
        private bool KModesCanExecute()
        {
            return Bitmap != null;
        }

        public ICommand MeanShiftCommand
        {
            get
            {
                return meanShiftCommand.Value;
            }
        }
        public void MeanShiftExecute()
        {
            Status = System.Reflection.MethodBase.GetCurrentMethod().Name;
            Bitmap.Freeze();
            var task = Task.Run(() =>
            {
                BitmapProcessed = this.meanshiftService.getImage(Bitmap, ConstKernelMeanS, TypeKernelSelectedMeanS, SigmaMeanS,
                    ToleranceMeanS, IterationsMeanS);
                ProportionsResult = new ObservableCollection<ItemProportions>(this.meanshiftService.Results);
                this.pwService.closeProgress();
            });
            this.pwService.showProgress(Status, "Procesando imagen... (puede tardar unos minutos)");
        }
        private bool MeanShiftCanExecute()
        {
            return Bitmap != null;
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
       
    }
}
