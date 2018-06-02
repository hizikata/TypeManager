using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace TypeManager.ViewModel
{
    public class FrmMainViewModel:ViewModelBase
    {
        #region TypeModifyCommand 参数更改  
        public ReadOnlyCollection<CommandViewModel> TypeModifyCommand
        {
            get
            {
                List<CommandViewModel> command = new List<CommandViewModel>()
                {
                    new CommandViewModel(new RelayCommand(ShowTypeModifuSubmenu),"参数更改")
                };
                return new ReadOnlyCollection<CommandViewModel>(command);           
            }
        }
        void ShowTypeModifuSubmenu()
        {

        }
        public ReadOnlyCollection<CommandViewModel> TypeModifyCommands
        {
            get
            {
                List<CommandViewModel> commands = new List<CommandViewModel>()
                {
                    new CommandViewModel(new RelayCommand(ShowTypePara),"参数更改")
                };
                return new ReadOnlyCollection<CommandViewModel>(commands);
            }
        }
        void ShowTypePara()
        {

        }

        #endregion
        #region Command 树形菜单命令
        public RelayCommand<object> TreeViewItemCommand
        {
            get
            {
                return new RelayCommand<object>((obj) =>
                {
                    if (obj != null)
                    {
                        var treeViewItem = obj as TreeViewItem;
                        if (treeViewItem != null)
                        {
                            if (treeViewItem.Items.Count == 0)
                                TreeViemItemChanged(treeViewItem.Tag);
                        }
                    }
                    else
                    {
                        MessageBox.Show("convert is defeated");
                    }
                });
            }
        }
        public void TreeViemItemChanged(object tag)
        {
            if (tag != null && !string.IsNullOrEmpty(tag.ToString()))
            {
                Messenger.Default.Send<object>("", tag.ToString());
            }
            else
            {
                MessageBox.Show("TreeView 的Tag标签不能为空", "系统提示");
            }
            
        }
        #endregion
    }
}
