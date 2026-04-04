namespace WindowsPrivacyAutomator.Services.Automation;


public class DiagnosticsAndFeedbackPage : BasePage
{
    #region Auto IDs
    private const string EnableSendOptionalDiagnosticDataAutoId = "SystemSettings_Privacy_Diagnostic_Data_Toggle_2_ToggleSwitch";
    private const string EnableImproveLanguageRecognitionAutoId = "SystemSettings_Privacy_ImproveInkType_Toggle_2_ToggleSwitch";
    private const string EnableDiagnosticsDataViewerAutoId = "SystemSettings_Privacy_Telemetry_Viewer_Toggle_2_ToggleSwitch";
    private const string DeleteDiagnosticsDataAutoId = "SystemSettings_Privacy_Device_Delete_2_Button";
    private const string FeedbackFrequencyAutoId = "SystemSettings_Privacy_Change_Feedback_Frequency_ComboBox";
    #endregion

    public DiagnosticsAndFeedbackPage() : base("ms-settings:privacy-feedback")
    {
    }

    public bool EnableSendOptionalDiagnostics
    {
        get
        {
            var group = GetGroup("Diagnostic data");
            return GetToggleStateFromCollapsableGroup(group, EnableSendOptionalDiagnosticDataAutoId);
        }
        set
        {
            var group = GetGroup("Diagnostic data");
            SetToggleStateFromCollapsableGroup(group, EnableSendOptionalDiagnosticDataAutoId, value);
        }
    }

    public bool EnableImproveLanguageRecognition
    {
        get
        {
            if (!EnableSendOptionalDiagnostics)
            {
                return false;
            }

            var group = GetGroup("Improve inking and typing");
            return GetToggleStateFromCollapsableGroup(group, EnableImproveLanguageRecognitionAutoId);
        }
        set
        {
            if (EnableSendOptionalDiagnostics)
            {
                var group = GetGroup("Improve inking and typing");
                SetToggleStateFromCollapsableGroup(group, EnableImproveLanguageRecognitionAutoId, value);
            }
        }
    }

    public bool EnableDiagnosticsDataViewer
    {
        get
        {
            var group = GetGroup("View diagnostic data");
            return GetToggleStateFromCollapsableGroup(group, EnableDiagnosticsDataViewerAutoId);
        }
        set
        {
            var group = GetGroup("View diagnostic data");
            SetToggleStateFromCollapsableGroup(group, EnableDiagnosticsDataViewerAutoId, value);
        }
    }

    public string FeedbackFrequency
    {
        get
        {
            var group = GetGroup("Feedback");
            return GetComboBoxSelection(group, FeedbackFrequencyAutoId);
        }
        set
        {
            var group = GetGroup("Feedback");
            SetComboBoxSelection(group, FeedbackFrequencyAutoId, value);
        }
    }


    public void DeleteDiagnosticsData()
    {
        var group = GetGroup("Delete diagnostic data");
        ExpandGroup(group, DeleteDiagnosticsDataAutoId);
        PressButton(group, DeleteDiagnosticsDataAutoId);
    }

}
