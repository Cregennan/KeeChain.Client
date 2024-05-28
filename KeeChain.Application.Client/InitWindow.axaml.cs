using Avalonia.Controls;

namespace KeeChain.Application.Client;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Warlin.Exceptions;

public partial class InitWindow : Window
{
    public InitWindow()
    {
        InitializeComponent();
        if (!Design.IsDesignMode)
        {
            Task.Run(async () => await TryConnectToBoard());
        }
    }

    public async Task TryConnectToBoard()
    {
        using var source = new CancellationTokenSource();
        Task.Run(async () =>
        {
            var i = 0;
            while (true)
            {
                source.Token.ThrowIfCancellationRequested();
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    DeviceStatus.Text = $"Подключаемся к плате{string.Concat(Enumerable.Repeat('.', i))}";
                }, DispatcherPriority.Render, source.Token);
                
                i = (i + 1) % 5;
                await Task.Delay(TimeSpan.FromMilliseconds(200), source.Token);
            }
        }, source.Token);
        
        while (true)
        {
            try
            {
                await SalavatService.Initialize();
                await source.CancelAsync();
                
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    DeviceStatus.Text = "Плата найдена";
                    DeviceStatus.Foreground = new SolidColorBrush(Colors.Green);
                    PasswordField.Watermark = SalavatService.EntriesCount > 0 ? "Введите пароль" : "Придумайте пароль";
                });
                
                await UnlockPasswordFields();
                return;
            }
            catch (WarlinException e)
            {
                
            }
        }
    }
    
    private async Task UnlockPasswordFields()
    {
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            Submit.IsVisible = true;
            PasswordField.IsVisible = true;
        });
    }

    private async void OnSubmitClick(object obj, RoutedEventArgs args)
    {
        var text = PasswordField.Text;
        try
        {
            await SalavatService.Unlock(text);
            
            new MainWindow().Show();
            this.Close();
        }
        catch (WarlinException e)
        {
            var msg = MessageBoxManager.GetMessageBoxStandard(
                title: "Ошибка",
                text: e.Message,
                @enum: ButtonEnum.Ok,
                icon: MsBox.Avalonia.Enums.Icon.Forbidden);

            await msg.ShowAsync();
        }
    }
}