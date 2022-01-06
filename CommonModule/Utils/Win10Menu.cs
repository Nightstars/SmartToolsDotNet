﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CommonModule.Utils
{
    public class Win10Menu : ContentControl
    {
        public new List Content
        {
            get { return (List)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public new static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(List<Win10MenuItem>), typeof(Win10Menu), new FrameworkPropertyMetadata(null));

        static Win10Menu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Win10Menu), new FrameworkPropertyMetadata(typeof(Win10Menu)));
        }

        public override void BeginInit()
        {
            Content = new List<Win10MenuItem>();
            base.BeginInit();
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(Win10Menu), new PropertyMetadata(true));

        public System.Windows.Media.Brush MenuIconColor
        {
            get { return (System.Windows.Media.Brush)GetValue(MenuIconColorProperty); }
            set { SetValue(MenuIconColorProperty, value); }
        }

        public static readonly DependencyProperty MenuIconColorProperty =
            DependencyProperty.Register("MenuIconColor", typeof(System.Windows.Media.Brush), typeof(Win10Menu), new PropertyMetadata(Brushes.White));

        public Brush SelectionIndicatorColor
        {
            get { return (Brush)GetValue(SelectionIndicatorColorProperty); }
            set { SetValue(SelectionIndicatorColorProperty, value); }
        }

        public static readonly DependencyProperty SelectionIndicatorColorProperty =
            DependencyProperty.Register("SelectionIndicatorColor", typeof(Brush), typeof(Win10Menu), new PropertyMetadata(Brushes.Red));

        public Brush MenuItemForeground
        {
            get { return (Brush)GetValue(MenuItemForegroundProperty); }
            set { SetValue(MenuItemForegroundProperty, value); }
        }

        public static readonly DependencyProperty MenuItemForegroundProperty =
            DependencyProperty.Register("MenuItemForeground", typeof(Brush), typeof(Win10Menu), new PropertyMetadata(Brushes.Transparent));
    }
}
}
