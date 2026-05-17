using Extensions.Components.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Zonit.Extensions.Website;

namespace Extensions.Components.Pages;

[Route(Route)]
public sealed partial class AutoSave : PageEditBase<TaskViewModel>
{
    public const string Route = "/components/auto-save";

    private readonly List<string> _log = [];

    private void OnFieldInput(string fieldName, string? value)
    {
        switch (fieldName)
        {
            case nameof(TaskViewModel.Title): Model.Title = value ?? string.Empty; break;
            case nameof(TaskViewModel.Notes): Model.Notes = value ?? string.Empty; break;
        }

        EditContext?.NotifyFieldChanged(new FieldIdentifier(Model, fieldName));
    }

    private void OnPriorityInput(string? raw)
    {
        if (int.TryParse(raw, out var v))
            Model.Priority = v;

        EditContext?.NotifyFieldChanged(new FieldIdentifier(Model, nameof(TaskViewModel.Priority)));
    }

    protected override Task AutoSaveAsync(string fieldName, object? oldValue, object? newValue, CancellationToken cancellationToken)
    {
        _log.Insert(0, $"{DateTime.Now:HH:mm:ss.fff}  {fieldName}: {oldValue ?? "∅"} → {newValue ?? "∅"}");
        if (_log.Count > 10) _log.RemoveRange(10, _log.Count - 10);
        Toast.AddInfo("Auto-saved {0}", fieldName);
        return InvokeAsync(StateHasChanged);
    }

    protected override Task SubmitAsync(CancellationToken cancellationToken)
    {
        Toast.AddSuccess("Submitted form");
        return Task.CompletedTask;
    }
}
