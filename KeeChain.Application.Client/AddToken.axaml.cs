using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace KeeChain.Application.Client;

using System;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using Warlin.Exceptions;

public partial class AddToken : Window
{
    public event EventHandler OnSubmitSuccess;
    
    public AddToken()
    {
        InitializeComponent();
    }

    private async void OnSubmitClick(object o, RoutedEventArgs a)
    {
        try
        {
            await SalavatService.AddEntry(Name?.Text ?? string.Empty, Secret?.Text ?? string.Empty);
            OnSubmitSuccess?.Invoke(this, EventArgs.Empty);
            this.Close();
        }
        catch (WarlinException e)
        {
            var box = MessageBoxManager.GetMessageBoxStandard(
                title: "Ошибка",
                text: e.Message,
                icon: MsBox.Avalonia.Enums.Icon.Forbidden);
            await box.ShowAsync();
        }
    }
}