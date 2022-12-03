using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace App.Reactive;

public class MainWindowViewModel : ReactiveObject
{
    public MainWindowViewModel()
    {
        this.Command = ReactiveCommand.CreateFromTask(this.CommandExecutedAsync);
        this.result = this.Command
            .ObserveOn(RxApp.MainThreadScheduler)
            .ToProperty(this, x => x.Result);
    }

    public ReactiveCommand<Unit, string> Command { get; }

    private ObservableAsPropertyHelper<string> result;

    public string Result => this.result.Value;

    private async Task<string> CommandExecutedAsync()
    {
        await Task.Delay(1000);

        return DateTimeOffset.UtcNow.ToString();
    }
}