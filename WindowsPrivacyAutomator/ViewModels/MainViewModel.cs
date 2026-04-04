using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WindowsPrivacyAutomator.Models;
using WindowsPrivacyAutomator.Services.Automation;

namespace WindowsPrivacyAutomator.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly DispatcherQueue _dispatcherQueue = DispatcherQueue.GetForCurrentThread();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(OptimizeCommand))]
    public partial bool IsBusy { get; set; }

    public ObservableCollection<LogEntry> Logs { get; } = new();

    [RelayCommand(CanExecute = nameof(CanOptimize))]
    private async Task Optimize()
    {
        IsBusy = true;
        Logs.Clear();

        await Task.Run(() =>
        {
            try
            {
                using (var settings = new DiagnosticsAndFeedbackPage())
                {
                    Log("Diagnostics & feedback", LogType.Header);

                    settings.EnableSendOptionalDiagnostics = false;
                    Log("Send optional diagnostic data. (Disabled)");

                    settings.EnableImproveLanguageRecognition = false;
                    Log("Improve the language recognition and suggestion capabilities of Microsoft apps and services. (Disabled)");

                    settings.EnableDiagnosticsDataViewer = false;
                    Log("Turn on the Diagnostic Data Viewer. (Disabled)");

                    settings.DeleteDiagnosticsData();
                    Log("Deleted diagnostic data.");

                    settings.FeedbackFrequency = "Never";
                    Log("Feedback frequency set to 'Never'.");
                }

                using (var settings = new RecommendationsAndOffersPage())
                {
                    Log("Personalized offers", LogType.Header);

                    settings.EnablePersonalizedOffers = false;
                    Log("Personalized offers. (Disabled)");

                    settings.EnableAllowLanguageListAccess = false;
                    Log("Allow websites to access my language list. (Disabled)");

                    settings.EnableImproveStartAndSearchResults = false;
                    Log("Improve Start and search results. (Disabled)");

                    settings.EnableShowNotificationsInSettings = false;
                    Log("Show notifications in Settings. (Disabled)");

                    settings.EnableRecommendationsAndOffersInSettings = false;
                    Log("Recommendations and offers in Settings. (Disabled)");

                    settings.EnableAdvertisingId = false;
                    Log("Advertising ID. (Disabled)");
                }

                using (var settings = new SearchPage())
                {
                    Log("Search", LogType.Header);

                    settings.EnableSearchHistory = false;
                    Log("Search history. (Disabled)");

                    settings.EnableShowSearchHighlights = false;
                    Log("Show search highlights. (Disabled)");

                    settings.EnableSearchMicrosoftAccount = false;
                    Log("Search my Microsoft account. (Disabled)");

                    settings.EnableSearchWorkOrHomeAccount = false;
                    Log("Search my Work or School account. (Disabled)");
                }

                using (var settings = new SpeechPage())
                {
                    Log("Speech", LogType.Header);

                    settings.EnablOnlineSpeechRecognition = false;
                    Log("Online speech recognition. (Disabled)");
                }
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}", LogType.Error);
            }
            finally
            {
                _dispatcherQueue.TryEnqueue(() =>
                {
                    IsBusy = false;
                });
            }

        });
    }

    private bool CanOptimize() => !IsBusy;

    private void Log(string message, LogType type = LogType.Setting)
    {
        _dispatcherQueue.TryEnqueue(() => Logs.Add(new LogEntry(message, type)));
    }
}
