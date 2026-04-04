using FlaUI.Core.AutomationElements;
using System;


namespace WindowsPrivacyAutomator.Services.Automation;

public class SearchPage : BasePage
{
    #region Auto IDs
    private const string EnableSearchHistoryAutoId = "SystemSettings_Search_MyDeviceHistory_ToggleSwitch";
    private const string EnableShowSearchHighlightsAutoId = "SystemSettings_Search_DynamicSearchBox_ToggleSwitch";
    private const string EnableSearchMicrosoftAccountAutoId = "SystemSettings_Search_CloudSearchMSA_ToggleSwitch";
    private const string EnableSearchWorkOrHomeAccountAutoId = "SystemSettings_Search_CloudSearchAAD_ToggleSwitch";
    #endregion

    #region Group Elements
    private AutomationElement SearchHistoryGroup;
    private AutomationElement ClearDeviceSearchHistoryGroup;
    private AutomationElement SearchMyAccountsGroup;
    #endregion

    public SearchPage() : base("ms-settings:search")
    {
        SearchHistoryGroup = GetGroup("Search history");
        ClearDeviceSearchHistoryGroup = GetGroup("Clear device search history");
        SearchMyAccountsGroup = GetGroup("Search my accounts");
    }

    public bool EnableSearchHistory
    {
        get => GetToggleStateFromCollapsableGroup(SearchHistoryGroup, EnableSearchHistoryAutoId);
        set => SetToggleStateFromCollapsableGroup(SearchHistoryGroup, EnableSearchHistoryAutoId, value);
    }

    public bool EnableShowSearchHighlights
    {
        get => GetToggleState(_window, EnableShowSearchHighlightsAutoId);
        set => SetToggleState(_window, EnableShowSearchHighlightsAutoId, value);
    }

    public bool EnableSearchMicrosoftAccount
    {
        get => GetToggleStateFromCollapsableGroup(SearchMyAccountsGroup, EnableSearchMicrosoftAccountAutoId);
        set => SetToggleStateFromCollapsableGroup(SearchMyAccountsGroup, EnableSearchMicrosoftAccountAutoId, value);
    }

    public bool EnableSearchWorkOrHomeAccount
    {
        get => GetToggleStateFromCollapsableGroup(SearchMyAccountsGroup, EnableSearchWorkOrHomeAccountAutoId);
        set => SetToggleStateFromCollapsableGroup(SearchMyAccountsGroup, EnableSearchWorkOrHomeAccountAutoId, value);
    }

    public void ClearDeviceSearchHistory()
    {
        // TODO: Fix this
        ExpandGroup(SearchHistoryGroup, EnableSearchWorkOrHomeAccountAutoId);
        var button = ClearDeviceSearchHistoryGroup.FindFirstDescendant(cf => cf.ByText("Clear"))
            ?? throw new Exception($"Required element with text 'Clear' not found.");
        button?.Patterns.Invoke.Pattern.Invoke();
    }
}
