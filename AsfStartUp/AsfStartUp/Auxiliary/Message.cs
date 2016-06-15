using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Collections;

namespace AsfStartUp.Auxiliary
{
    #region RootPathMessage
    public class RootPathSetter
    {
        public static void SetRootPath(RootPathMessage _rootPathMessage)
        {
            Messenger.Default.Send<RootPathMessage>(_rootPathMessage);
        }
    }
    public class RootPathMessage
    {
        public string RootPath
        { get; set; }
        public RootPathMessage(string _rootPath)
        {
            RootPath = _rootPath;
        }
    }
    #endregion
    #region SequenceSetter
    public class SequenceSelectedMessage
    {
        public string SequenceEnvFilePath
        { get; set; }
        public string TemplateFilePath
        { get; set; }
        public SequenceSelectedMessage(string _filePath,string _templateFilePath)
        {
            SequenceEnvFilePath = _filePath;
            TemplateFilePath = _templateFilePath;
        }
    }
    public class SequenceSelectedMessageSetter
    {
        public static void SetSequenceSelected(SequenceSelectedMessage _ssm)
        {
            Messenger.Default.Send<SequenceSelectedMessage>(_ssm);
        }
    }

    #endregion

    #region reflesh UIPropertiry
    public class PropertyMessage
    {
        public string Btn_Text
        { get; set; }
        public PropertyMessage(string _btnTxt)
        {
            Btn_Text = _btnTxt;
        }
    }
    public class PropertyMessageSetter
    {
        public static void RefleshUI(PropertyMessage pm)
        {
            Messenger.Default.Send<PropertyMessage>(pm);
        }
    }
    #endregion

<<<<<<< HEAD
    #region ProgressMessage
    public class ProgressMessage
    {
        public int CurrentValue
        { get; set; }
        public ProgressMessage(int _cv)
        {
            CurrentValue = _cv;
        }
    }

    public class ProgressMessageSetter
    {
        public static void SendProgressMessage(ProgressMessage pm)
        {
            Messenger.Default.Send<ProgressMessage>(pm);
        }
    }
    #endregion


=======
  
>>>>>>> origin/master

}
