﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LearningCardsApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardPage : ContentPage
    {
        public CardPage()
        {
            InitializeComponent();
            TurnButton.Clicked += ButtonClicked;
        }


        private void ButtonClicked (object sender, EventArgs e)
        {
            BackTextLabel.IsVisible = !BackTextLabel.IsVisible;
        }
    }
}