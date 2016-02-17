using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace petermajor.configureawait
{
	public class PhotoAdapter : BaseAdapter<Photo>
	{
		readonly List<Photo> _photos;

		readonly Context _context;

		public PhotoAdapter(Context context, List<Photo> photos)
		{
			_context = context;
			_photos = photos;
		}

		public override int Count
		{
			get { return _photos.Count; }
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override Photo this[int index]
		{
			get { return _photos[index]; }
		}

		class PhotoViewHolder : Java.Lang.Object
		{
			readonly TextView _titleView;
			readonly TextView _urlView;

			public PhotoViewHolder(View view)
			{
				_titleView = view.FindViewById<TextView>(Resource.Id.titleText);
				_urlView = view.FindViewById<TextView>(Resource.Id.urlText);
			}

			public void SetPhoto(Photo photo)
			{
				_titleView.Text = photo.Title;
				_urlView.Text = photo.Url;
			}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			PhotoViewHolder holder;

			View view = convertView;
			if (view == null)
			{
				view = LayoutInflater.FromContext(_context).Inflate(Resource.Layout.PhotoCell, parent, false);
				holder = new PhotoViewHolder(view);
				view.Tag = holder;
			}
			else
			{
				holder = (PhotoViewHolder) view.Tag;
			}

			var photo = _photos [position];
			holder.SetPhoto(photo);

			return view;
		}
	}
}

