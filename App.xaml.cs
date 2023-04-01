using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;

namespace WpfWebRTCPlayer
{
    public static class GlobalValues
    {
        public static PlayerWindow PlayerWindow { set; get; }

        private static bool playerWindowEnable = true;
        public static bool PlayerWindowEnable
        {
            get
            {
                return playerWindowEnable;
            }
            set
            {
                playerWindowEnable = value;
                RaiseStaticPropertyChanged(() => playerWindowEnable);
            }
        }


        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        public static void RaiseStaticPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (StaticPropertyChanged != null)
            {
                var propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
                StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
}
