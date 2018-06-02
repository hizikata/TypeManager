/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TypeManager"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace TypeManager.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Register<TestViewModel>();
            SimpleIoc.Default.Register<FrmMainViewModel>();
            SimpleIoc.Default.Register<TypeParaViewModel>();
            SimpleIoc.Default.Register<StationSetViewModel>();
            SimpleIoc.Default.Register<CommonParaViewModel>();
            SimpleIoc.Default.Register<ThreeInOneParaViewModel>();
            SimpleIoc.Default.Register<LabelSetViewModel>();
            SimpleIoc.Default.Register<ReportExportViewModel>();
            SimpleIoc.Default.Register<MaterialSetViewModel>();
            SimpleIoc.Default.Register<MaterialRegisterViewModel>();
            SimpleIoc.Default.Register<LivParaSetViewModel>();
            SimpleIoc.Default.Register<SenParaSetViewModel>();
        }
        public TestViewModel Test
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TestViewModel>();
            }
        }
        public FrmMainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FrmMainViewModel>();
            }
        }
        public TypeParaViewModel TypePara
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TypeParaViewModel>();
            }
        }
        public StationSetViewModel StationSet
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StationSetViewModel>();
            }
        }
        public CommonParaViewModel CommonPara
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CommonParaViewModel>();
            }
        }
        public ThreeInOneParaViewModel ThreeInOne
        {
            get
            {
                return ServiceLocator.Current.GetInstance < ThreeInOneParaViewModel>();
            }
        }
        public LabelSetViewModel LabelSet
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LabelSetViewModel>();
            }
        }
        public ReportExportViewModel ReportExport
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ReportExportViewModel>();
            }
        }
        public MaterialRegisterViewModel MaterialRegister
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MaterialRegisterViewModel>();
            }
        }
        public MaterialSetViewModel MaterialSet
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MaterialSetViewModel>();
            }
        }
        public LivParaSetViewModel LivParaSet
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LivParaSetViewModel>();
            }
        }
        public SenParaSetViewModel SenParaSet
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SenParaSetViewModel>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}