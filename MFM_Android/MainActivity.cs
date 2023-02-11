namespace MFM_Android
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
       
        public TextView TimeDisplay { get; private set; }
        public int HourOffset { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var clockRefresh = new Timer(dueTime: 0, period: 1000, callback: UpdateTimeLabel, state: null);

            Button upButton = FindViewById<Button>(Resource.Id.upButton);
            upButton.Click += OnUpButton_Click;

            Button downButton = FindViewById<Button>(Resource.Id.downButton);
            downButton.Click += OnDownButton_Click;

            TimeDisplay = FindViewById<TextView>(Resource.Id.timeDisplay);
        }

        private void UpdateTimeLabel(object state = null)
        {
            // Timer callbacks run on a background thread, but UI updates must run on the UI thread.
            RunOnUiThread(() =>
            {
                TimeDisplay.Text = DateTime.Now.AddHours(HourOffset).ToLongTimeString();
            });
        }

        public void OnUpButton_Click(object sender, System.EventArgs e)
        {
            HourOffset++;
            UpdateTimeLabel();
        }

        public void OnDownButton_Click(object sender, System.EventArgs e)
        {
            HourOffset--;
            UpdateTimeLabel();
        }
    }
}
