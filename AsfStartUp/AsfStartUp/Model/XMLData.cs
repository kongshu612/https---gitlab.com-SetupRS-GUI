using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AsfStartUp.Model
{
    public class OSInfo
    {
        public string DisplayName
        {
            get; set;
        }
        public string TemplateName
        { get; set; }
        public int Priority
        { get; set; }
        public OperatingSystemType OSType
        {
            get;set;
        }
        public OSInfo(string displayName, string templateName,int priority, OperatingSystemType ostype)
        {
            DisplayName = displayName;
            TemplateName = templateName;
            Priority = priority;
            OSType = ostype;
        }
        public OSInfo()
        { }

        public enum OperatingSystemType
        { win_desktop,win_server,linux}
    }

    public class AsfRoleInfo
    {
        public string RoleName
        { get; set; }
        public ObservableCollection<string> TemplateBlackList
        { get; set; }
        public string TemplateName
        { get; set; }
        public string HostName
        { get; set; }
        public InstallMode Mode
        { get; set; }
        public ASFHostType HostType
        { get; set; }
        public string ProductVersion
        { get; set; }
        public bool IsInCloud
        { get; set; }
        public ObservableCollection<string> AliasNameCollection
        { get; set; }
        public ObservableCollection<string> SupportedBuildCollection
        { get; set; }
        public AsfRoleInfo()
        {
            TemplateBlackList = new ObservableCollection<string>();
            Mode = InstallMode.Auto;
            HostType = ASFHostType.AsfManaged;
            IsInCloud = false;
            AliasNameCollection = new ObservableCollection<string>();
            SupportedBuildCollection = new ObservableCollection<string>();
        }

        public AsfRoleInfo(string roleName,string productVersion,ObservableCollection<string> _supportedBuildCollection) : this()
        {
            RoleName = roleName;
            SupportedBuildCollection = _supportedBuildCollection;
            ProductVersion = productVersion;
        }
        public AsfRoleInfo(string roleName, ObservableCollection<string> _supportedBuildCollection) : this()
        {
            RoleName = roleName;
            SupportedBuildCollection = _supportedBuildCollection;
        }

        public AsfRoleInfo(string roleName,string templateName,string productVersion, ObservableCollection<string> _supportedBuildCollection) :this()
        {
            RoleName = roleName;
            TemplateName = templateName;
            ProductVersion = productVersion;
            SupportedBuildCollection = _supportedBuildCollection;
        }
        public AsfRoleInfo(string roleName, ObservableCollection<string> templateBlackList,string productVersion, ObservableCollection<string> _supportedBuildCollection) :this()
        {
            RoleName = roleName;
            TemplateBlackList = templateBlackList;
            ProductVersion = productVersion;
            SupportedBuildCollection = _supportedBuildCollection;
        }
        public static ASFHostType ConvertToASFHostType(string hostType)
        {
            switch(hostType)
            {
                case "AsfManaged": return ASFHostType.AsfManaged;
                case "Unmanaged": return ASFHostType.Unmanaged;
                case "TestManaged": return ASFHostType.TestManaged;
                case "AsfPowerManaged": return ASFHostType.AsfPowerManaged;
                default:throw new NotSupportedException( $"{hostType} not support in Current Version");
            }
        }


    }


    public enum InstallMode
    { Auto, Manual };
    public enum ASFHostType
    { AsfManaged, Unmanaged, TestManaged, AsfPowerManaged };
   
}
