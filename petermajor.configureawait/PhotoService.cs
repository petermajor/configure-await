using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace petermajor.configureawait
{
	public class PhotoService
	{
		public async Task<List<Photo>> GetPhotos()
		{
			var client = new WebClient ();

			var photosAsJson = await client.DownloadStringTaskAsync ("http://jsonplaceholder.typicode.com/photos");
//				.ConfigureAwait (false);

			var photos = JsonConvert.DeserializeObject<List<Photo>>(photosAsJson);

			return photos.OrderBy (c => c.Title).ToList ();
		}
	}
}