﻿namespace Aida64Display
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Xamarin.Forms;

    public class StartPage : ContentPage
    {
        public StartPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }
    }
}