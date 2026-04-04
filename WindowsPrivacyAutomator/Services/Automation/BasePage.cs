
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using System;
using System.Diagnostics;


namespace WindowsPrivacyAutomator.Services.Automation;


public abstract class BasePage : IDisposable
{
    protected readonly Window _window;
    protected readonly UIA3Automation _automation;

    protected BasePage(string pageUri)
    {
        Process.Start(new ProcessStartInfo(pageUri) { UseShellExecute = true });

        _automation = new UIA3Automation();
        var desktop = _automation.GetDesktop();

        Retry.DefaultTimeout = TimeSpan.FromSeconds(5);
        Retry.DefaultInterval = TimeSpan.FromMilliseconds(250);


        var window = Retry.WhileNull(() =>
            desktop.FindFirstChild(cf => cf.ByText("Settings"))?.AsWindow(), timeout: TimeSpan.FromSeconds(10)).Result;

        _window = window ?? throw new Exception("Failed to find Settings window within timeout.");

        _window.SetForeground();

        // Anchor wait for any Group element to appear to signal that the window is ready.
        Retry.WhileFalse(() =>
        {
            var anyGroup = _window.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.Group));
            return anyGroup != null;
        }, timeout: TimeSpan.FromSeconds(5));
    }

    public void Dispose()
    {
        _window?.Close();
        _automation?.Dispose();
    }

    protected AutomationElement GetRequiredElement(AutomationElement parent, string autoId)
    {
        var element = Retry.WhileNull(() => parent.FindFirstDescendant(cf => cf.ByAutomationId(autoId))).Result;
        return element ?? throw new Exception($"Required element with ID '{autoId}' not found.");
    }

    protected void ExpandGroup(AutomationElement group, string targetAutoId)
    {
        var expansionAction = Retry.WhileTrue(() =>
        {
            var target = group.FindFirstDescendant(cf => cf.ByAutomationId(targetAutoId));
            if (target != null && !target.IsOffscreen)
            {
                return false;
            }

            var expandButton = group.FindFirstDescendant(cf => cf.ByText("Show more settings"))?.AsButton();

            if (expandButton != null && !expandButton.IsOffscreen)
            {
                expandButton.Click();
            }

            return true;
        });

        if (expansionAction.Success == false)
        {
            throw new Exception($"Failed to expand group or find target: {targetAutoId}");
        }
    }

    protected void PressButton(AutomationElement parent, string autoId)
    {
        var button = GetRequiredElement(parent, autoId).AsButton();
        button?.Patterns.Invoke.Pattern.Invoke();
    }

    protected AutomationElement GetGroup(string title)
    {
        var element = Retry.WhileNull(() =>
            _window.FindFirstDescendant(cf => cf.ByText(title).And(cf.ByControlType(ControlType.Group)))).Result;

        return element ?? throw new Exception($"Required UI Group `{title}` was not found.");
    }

    protected bool GetToggleState(AutomationElement parent, string autoId)
    {
        var toggle = GetRequiredElement(parent, autoId).AsToggleButton();
        return toggle?.Patterns.Toggle.Pattern.ToggleState.Value == ToggleState.On;
    }

    protected bool GetToggleStateFromCollapsableGroup(AutomationElement parent, string autoId)
    {
        ExpandGroup(parent, autoId);
        return GetToggleState(parent, autoId);
    }

    protected void SetToggleState(AutomationElement parent, string autoId, bool newState)
    {
        var toggle = GetRequiredElement(parent, autoId).AsToggleButton();
        var currentState = toggle?.Patterns.Toggle.Pattern.ToggleState.Value == ToggleState.On;

        if (currentState != newState)
        {
            toggle?.Patterns.Toggle.Pattern.Toggle();
        }
    }

    protected void SetToggleStateFromCollapsableGroup(AutomationElement parent, string autoId, bool newState)
    {
        ExpandGroup(parent, autoId);
        SetToggleState(parent, autoId, newState);
    }

    protected string GetComboBoxSelection(AutomationElement parent, string autoId)
    {
        var combobox = GetRequiredElement(parent, autoId).AsComboBox();
        return combobox.SelectedItem?.Name
            ?? throw new Exception($"Selection for Combo Box with ID '{autoId}' not found.");
    }

    protected void SetComboBoxSelection(AutomationElement parent, string autoId, string selectionText)
    {
        var combobox = GetRequiredElement(parent, autoId).AsComboBox();
        combobox.Expand();
        combobox.Select(selectionText);
        combobox.Collapse();
    }
}
