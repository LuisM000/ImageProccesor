using Autofac;
using ImageProcessor.Services;
using ImageProcessor.Services.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor.ViewModels.Base
{
    public class VMLocator
    {
        IContainer container;

        public VMLocator()
        {
            var builder = new ContainerBuilder();

            //VMPROCESSWINDOW
            builder.RegisterType<VMProcessWindow>();
            builder.RegisterType<NDVI>().As<INDVI>();
            builder.RegisterType<VI>().As<IVI>();
            builder.RegisterType<ProgressWindow>().As<IProgressWindow>();
            builder.RegisterType<NavigationWindow>().As<INavigationWindow>();
            builder.RegisterType<CloseApp>().As<ICloseApp>();
            builder.RegisterType<NavigationImage>().As<INavigationImage>();
            builder.RegisterType<MouseActions>().As<IMouseActions>();
            builder.RegisterType<KmeansBands>().As<IKmeansBands>();
            builder.RegisterType<KmodesBands>().As<IKmodesBands>();
            builder.RegisterType<Notepad>().As<INotepad>();

            //VMProcessWindow
            builder.RegisterType<VMImageWindow>();
            builder.RegisterType<Kmeans>().As<IKMeans>();
            builder.RegisterType<Kmodes>().As<IKModes>();
            builder.RegisterType<Meanshift>().As<IMeanShift>();
            builder.RegisterType<ChangeColor>().As<IChangeColor>();
            builder.RegisterType<Colordialog>().As<IColordialog>();
            builder.RegisterType<Message>().As<IMessage>();
            builder.RegisterType<SaveImage>().As<ISaveImage>(); 
            builder.RegisterType<Browser>().As<IBrowser>();
            builder.RegisterType<AlterBands>().As<IAlterBands>();

            container = builder.Build();
        }

        public VMProcessWindow ProcessWindowViewModel
        {
            get
            {
                return container.Resolve<VMProcessWindow>();
            }
        }

        public VMImageWindow ImageWindowViewModel
        {
            get
            {
                return container.Resolve<VMImageWindow>();
            }
        }
    }
}
