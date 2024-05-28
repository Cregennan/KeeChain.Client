using Avalonia.Controls;

namespace KeeChain.Application.Client
{
    using System;
    using MsBox.Avalonia;
    using Warlin.Exceptions;

    public partial class RemoveTokenWindow : Window
    {
        
        public event EventHandler OnDeleteSuccess;
        
        public RemoveTokenWindow(string[] names)
        {
            InitializeComponent();
            foreach (var name in names)
            {
                Selector.Items.Add(new ComboBoxItem { Content = name });
            }

            Selector.SelectedIndex = 0;

            Delete.Click += async (_, _) =>
            {
                try
                {
                    await SalavatService.RemoveEntry(Selector.SelectedIndex);
                    OnDeleteSuccess?.Invoke(this, EventArgs.Empty);
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
            };
        }
    }
}