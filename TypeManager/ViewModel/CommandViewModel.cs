using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TypeManager.ViewModel
{
    public class CommandViewModel:ViewModelBase
    {
        #region Fields
        public RelayCommand Command { get; private set; }
        public string DisplayName { get; private set; }
        public string Remark { get; private set; }
        public ImageSource Image { get; private set; }
        #endregion
        #region Constructors
        public CommandViewModel(RelayCommand command,string displayName,string image,string remark)
        {
            this.Command = command;
            this.DisplayName = displayName;
            if (!string.IsNullOrEmpty(image))
                this.Image = new BitmapImage(new Uri("pack://application:,,,/TypeManager;component/Resources/" + image, UriKind.Absolute));
            this.Remark = remark;
        }
        public CommandViewModel(RelayCommand command,string displayName,string image) : this(command, displayName, image, null)
        {

        }
        public CommandViewModel(RelayCommand command,string displayName) : this(command, displayName, null, null)
        {

        }
        #endregion
    }
}
