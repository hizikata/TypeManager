using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using XuxzLib;
using TypeManager.DAL;

namespace TypeManager.ViewModel
{
    public class LoginViewModel
    {
        #region Properties

        #endregion
        #region Commands
        public RelayCommand<object> LoginCommand
        {
            get
            {
                return new RelayCommand<object>(obj => ExecuteLoginCommand(obj));
            }
        }
        
        #endregion
        #region CommandMethods
        void ExecuteLoginCommand(object obj)
        {
            Messenger.Default.Send<object>("", "Login");
        }
        #endregion
    }
}
