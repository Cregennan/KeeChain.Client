using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace KeeChain.Application.Client;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;
using MsBox.Avalonia;
using Salavat;
using Warlin.Exceptions;

public partial class MainWindow : Window
{
    private const int FirstTokenTop = 160;
    private const int RowGap = 20;
    private const int RowHeight = 60;
    private const int RowWidth = 340;
    private const int RowLeft = 60;
    
    private const int NameRowTop = 18;
    private const int NameGlobalLeft = 75;

    private const int CodeRowTop = 15;
    private const int CodeGlobalLeft = 295;

    private const int ProgressRowTop = 54;

    private CancellationTokenSource _source = new();

    private OTPDescription[] _codes = [];

    private RowDescription[] _rows = [];

    private object _lockObject = new();
    
    public MainWindow()
    {
        InitializeComponent();
        if (!Design.IsDesignMode)
        {
            Listen();
            Redraw();
        }

        AddBtn.Click += async (_, _) =>
        {
            var window = new AddToken();
            var result = false;
            window.OnSubmitSuccess += (_, _) => result = true;
            await window.ShowDialog(this);
            if (result)
            {
                Redraw();
            }
        };

    }

    private async Task Redraw()
    {
        try
        {
            _codes = await SalavatService.GetCodes();
        }
        catch (WarlinException e)
        {
            var box = MessageBoxManager.GetMessageBoxStandard(
                title: "Ошибка",
                text: e.Message,
                icon: MsBox.Avalonia.Enums.Icon.Forbidden);
            await box.ShowAsync();
            return;
        }

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            
            foreach (var row in _rows)
            {
                MainCanvas.Children.Remove(row.Code);
                MainCanvas.Children.Remove(row.Progress);
                MainCanvas.Children.Remove(row.Name);
                MainCanvas.Children.Remove(row.Rectangle);
            }
            
            lock (_lockObject)
            {
                _rows = _codes.Select((codeDescr, index) => new RowDescription()
                    {
                        Name = GetNameBlock(codeDescr.Name, index),
                        Code = GetCodeBlock(codeDescr.Code, index),
                        Progress = GetProgressBar(index),
                        Rectangle = GetRectangle(index)
                    })
                    .ToArray();
            }
        
            foreach (var row in _rows)
            {
                MainCanvas.Children.Add(row.Code);
                MainCanvas.Children.Add(row.Progress);
                MainCanvas.Children.Add(row.Name);
                MainCanvas.Children.Add(row.Rectangle);
            }
                
            AddBtn.IsVisible = _rows.Length < 6;
            RemoveBtn.IsVisible = _rows.Length > 0;
            KeysLabel.Content = _rows.Length > 0 ? "Сохраненные ключи" : "Нет сохраненных ключей";

        }, DispatcherPriority.Render, _source.Token);
    }
    
#region BlockGen
    
private static TextBlock GetNameBlock(string text, int index)
{
    var element = new TextBlock() { Text = text, FontSize = 20 };
    Canvas.SetLeft(element, NameGlobalLeft);
    Canvas.SetTop(element, FirstTokenTop + NameRowTop + (RowHeight + RowGap) * index);

    return element;
}

private static TextBlock GetCodeBlock(string code, int index)
{
    var element = new TextBlock() { Text = code, FontSize = 25 };
    Canvas.SetLeft(element, CodeGlobalLeft);
    Canvas.SetTop(element, FirstTokenTop + CodeRowTop + (RowHeight + RowGap) * index);

    return element;
}

private static Rectangle GetRectangle(int index)
{
    var element = new Rectangle()
    {
        Fill = new SolidColorBrush(Colors.Transparent),
        Stroke = new SolidColorBrush(Colors.Gray),
        RadiusX = 4,
        RadiusY = 4,
        Height = RowHeight,
        Width = RowWidth,
        StrokeThickness = 1
    };
    Canvas.SetTop(element, FirstTokenTop + index * (RowHeight + RowGap));
    Canvas.SetLeft(element, RowLeft);

    return element;
}

private static ProgressBar GetProgressBar(int index)
{
    var element = new ProgressBar()
    {
        Minimum = 0,
        Maximum = 30,
        Value = 25,
        Width = RowWidth,
        Height = 2
    };
    
    Canvas.SetTop(element, FirstTokenTop + ProgressRowTop + index * (RowHeight + RowGap));
    Canvas.SetLeft(element, RowLeft);

    return element;
}
    
#endregion

    private void Listen()
    {
        Task.Run(async () =>
        {
            while (!_source.Token.IsCancellationRequested)
            {
                var time = DateTimeOffset.UtcNow.ToUnixTimeSeconds() % 30;

                if (time == 0)
                {
                    Redraw();
                }
                
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    lock (_lockObject)
                    {
                        foreach (var row in _rows)
                        {
                            row.Progress.Value = 30 - time;
                        }
                    }
                }, DispatcherPriority.Render, _source.Token);
                
                await Task.Delay(TimeSpan.FromMilliseconds(1000));
            }
        }, cancellationToken: _source.Token);
    }

    private class RowDescription
    {
        public Rectangle Rectangle;
        public TextBlock Name;
        public TextBlock Code;
        public ProgressBar Progress;
    }
}