using Android.Content;
using Android.Util;
using Microsoft.Maui.ApplicationModel;

namespace AsyncTaskApplication
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/AppStyle")]
    public class MainActivity : Activity
    {
        private const string TAG = nameof(MainActivity);

        private bool taskIsRunning = false;

        private string? loading;
        private string? taskCompleted;
        private string? startConsistentButton;

        private Button? parallelTasklButton;
        private TextView? parallelTaskText;
        private Button? consistentTaskButton;
        private TextView? consistentTaskText;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            loading = GetString(Resource.String.loading);
            taskCompleted = GetString(Resource.String.task_completed);
            startConsistentButton = GetString(Resource.String.consistent_task_button_text);

            parallelTasklButton = FindViewById<Button>(Resource.Id.parallelTaskButton);
            parallelTaskText = FindViewById<TextView>(Resource.Id.parallelTaskTextView);

            consistentTaskButton = FindViewById<Button>(Resource.Id.consistentTaskButton);
            consistentTaskText = FindViewById<TextView>(Resource.Id.consistentTaskTextView);

            parallelTasklButton!.Click += ParallelButtonTask;
            consistentTaskButton!.Click += ConsistentButtonTask;
        }

        private async void ParallelButtonTask(object? sender, EventArgs e)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            Log.Debug(TAG, "The task completed.");
            parallelTaskText!.Text += "The task completed.\n";
        }

        private async void ConsistentButtonTask(object? sender, EventArgs e)
        {
            consistentTaskButton!.Enabled = false;
            consistentTaskButton.Text = loading;
            await Task.Delay(TimeSpan.FromSeconds(5));
            consistentTaskText!.Text += $"{taskCompleted}\n";
            consistentTaskButton.Text = startConsistentButton;
            consistentTaskButton.Enabled = true;
        }
    }
}