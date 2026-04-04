using FlaUI.Core.AutomationElements;


namespace WindowsPrivacyAutomator.Services.Automation;

internal class RecommendationsAndOffersPage : BasePage
{
    #region Auto IDs
    private const string EnablePersonalizedOffersAutoId = "SystemSettings_Privacy_TailoredExperiences2_ToggleSwitch";
    private const string EnableAllowLanguageListAccessAutoId = "SystemSettings_Language_Web_Content_Control_ToggleSwitch";
    private const string EnableImproveStartAndSearchResultsAutoId = "SystemSettings_Privacy_StoreAppUsage_ToggleSwitch";
    private const string EnableShowNotificationsInSettingsAutoId = "SystemSettings_Privacy_EnableAccountNotificationsInSettings_ToggleSwitch";
    private const string EnableRecommendationsAndOffersInSettingsAutoId = "SystemSettings_Privacy_EnableSuggestionsInSettings_ToggleSwitch";
    private const string EnableAdvertisingIdAutoId = "SystemSettings_Privacy_AdvertisingIdEnabled_ToggleSwitch";
    #endregion

    #region Group Elements
    private AutomationElement PersonalizedOffersGroup;
    #endregion

    public RecommendationsAndOffersPage() : base("ms-settings:privacy-general")
    {
        PersonalizedOffersGroup = GetGroup("Personalized offers");
    }

    public bool EnablePersonalizedOffers
    {
        get => GetToggleStateFromCollapsableGroup(PersonalizedOffersGroup, EnablePersonalizedOffersAutoId);
        set => SetToggleStateFromCollapsableGroup(PersonalizedOffersGroup, EnablePersonalizedOffersAutoId, value);
    }

    public bool EnableAllowLanguageListAccess
    {
        get => GetToggleState(_window, EnableAllowLanguageListAccessAutoId);
        set => SetToggleState(_window, EnableAllowLanguageListAccessAutoId, value);
    }

    public bool EnableImproveStartAndSearchResults
    {
        get => GetToggleState(_window, EnableImproveStartAndSearchResultsAutoId);
        set => SetToggleState(_window, EnableImproveStartAndSearchResultsAutoId, value);
    }

    public bool EnableShowNotificationsInSettings
    {
        get => GetToggleState(_window, EnableShowNotificationsInSettingsAutoId);
        set => SetToggleState(_window, EnableShowNotificationsInSettingsAutoId, value);
    }

    public bool EnableRecommendationsAndOffersInSettings
    {
        get => GetToggleState(_window, EnableRecommendationsAndOffersInSettingsAutoId);
        set => SetToggleState(_window, EnableRecommendationsAndOffersInSettingsAutoId, value);
    }

    public bool EnableAdvertisingId
    {
        get => GetToggleState(_window, EnableAdvertisingIdAutoId);
        set => SetToggleState(_window, EnableAdvertisingIdAutoId, value);
    }
}
