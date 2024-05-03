using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace KeeChain.Application.Client;

using System;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var current = Random.Shared.Next(0, 100);
        
        Random1.Text = Random.Shared.Next(100000, 999999).ToString();
        Random2.Text = Random.Shared.Next(100000, 999999).ToString();
        Random3.Text = Random.Shared.Next(100000, 999999).ToString();
        Random4.Text = Random.Shared.Next(100000, 999999).ToString();
        Bar1.Value = current;
        Bar2.Value = current;
        Bar3.Value = current;
        Bar4.Value = current;
        
        AddBtn.Click += (_, _) => new AddToken().Show(this);
    }
}