using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace CarTCOCalculator.Android
{
	[Activity(Label = "Car Total Cost of Ownership Calculator", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.AddCar);

            //button.Click += (object sender, EventArgs e) =>
            //{
            //    var carDialog = new AlertDialog.Builder(this);
            //    carDialog.SetMessage("New Car");
            //    carDialog.Show();
            //};
		}
	}
}
