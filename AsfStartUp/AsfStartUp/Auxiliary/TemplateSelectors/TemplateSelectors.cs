using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AsfStartUp.ViewModel;
using System.Windows.Controls;

namespace AsfStartUp.Auxiliary.TemplateSelectors
{

    public class TemplateSelectors:DataTemplateSelector
    {
        public DataTemplate RadioTemplate
        { get; set; }
        public DataTemplate TextTemplate
        { get; set; }
        public DataTemplate ComboBoxTemplate
        { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            GeneralDisplayData gdd = item as GeneralDisplayData;
            switch(gdd.CType)
            {
                case CustomerType.Bool:return RadioTemplate;
                case CustomerType.Combo:return ComboBoxTemplate;
                case CustomerType.Text:return TextTemplate;
                default:return null;
            }
        }
    }
}
