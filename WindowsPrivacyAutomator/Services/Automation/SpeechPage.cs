using FlaUI.Core.AutomationElements;

namespace WindowsPrivacyAutomator.Services.Automation;

public class SpeechPage : BasePage
{
    #region Auto IDs
    private const string EnablOnlineSpeechRecognitionAutoId = "SystemSettings_Privacy_Speech_AllowSpeechServices_ToggleSwitch";
    #endregion

    #region Group Elements
    private AutomationElement OnlineSpeechRecognitionGroup;
    #endregion

    public SpeechPage() : base("ms-settings:privacy-speech")
    {
        OnlineSpeechRecognitionGroup = GetGroup("Online speech recognition");
    }

    public bool EnablOnlineSpeechRecognition
    {
        get => GetToggleStateFromCollapsableGroup(OnlineSpeechRecognitionGroup, EnablOnlineSpeechRecognitionAutoId);
        set => SetToggleStateFromCollapsableGroup(OnlineSpeechRecognitionGroup, EnablOnlineSpeechRecognitionAutoId, value);
    }
}
