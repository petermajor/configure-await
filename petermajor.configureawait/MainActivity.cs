using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;

namespace petermajor.configureawait
{
	[Activity (Label = "Await", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		readonly PhotoService _photoService = new PhotoService();

		ListView _listView;
		SwipeRefreshLayout _swipeRefreshLayout;

		protected async override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Main);

			_listView = FindViewById<ListView>(Resource.Id.listView);

			_swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
			_swipeRefreshLayout.Refresh += async delegate
			{
				await BindPhotos();
				_swipeRefreshLayout.Refreshing = false;
			};

			await BindPhotos();
		}

		#pragma warning disable 4014
		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item.ItemId == Resource.Id.menu_refresh)
			{
				BindPhotos();
			}

			return base.OnOptionsItemSelected(item);
		}
		#pragma warning restore 4014

		async Task BindPhotos()
		{
			var photos = await _photoService.GetPhotos ();

			_listView.Adapter = new PhotoAdapter(this, photos);
		}
	}
}