using System;
using com.drew.metadata;
using com.drew.imaging.jpg;
using com.drew.metadata.exif;
using System.IO;
using System.Collections.Generic;

namespace GalleryServer
{
	public class ThumbnailCache
	{
		private Int32 MaximumCacheSize = 200;
		private String CachePath;
		private List<String> IDs; // the list of ids, where we add to the end and remove from the beginning
		private Dictionary<String,byte[]> Cache;	// strict in-memory...

		public ThumbnailCache (String CacheDirectory, Int32 MaximumCachedFiles)
		{
			CachePath = CacheDirectory;
			MaximumCacheSize = MaximumCachedFiles;
			Cache = new Dictionary<string, byte[]>();
		}

		#region add to cache
		private void AddToCache(String ID, byte[] data)
		{
			if (IDs.Count == MaximumCacheSize)
			{
				// remove from IDs and Cache
				String RemoveID = IDs[0];
				IDs.RemoveAt(0);
				Cache.Remove(RemoveID);
			}

			Cache.Add(ID,data);
			IDs.Add(ID);
			// done!
		}
		#endregion

		#region GenerateThumbnail and store in cache
		private String GenerateThumbnail(FileInfo OriginalJpegFile)
		{
			Metadata metadata = JpegMetadataReader.ReadMetadata(OriginalJpegFile);

			try
			{
				foreach(ExifDirectory _dir in metadata)
				{
					// generate thumbnail and cache it...
					// first we need the ID...
					String ID = OriginalJpegFile.Name+OriginalJpegFile.LastWriteTime.Ticks;
					byte[] data = _dir.GetThumbnailData();
					AddToCache(ID,data);
					return ID;
				}
			}
			catch(Exception)
			{

			}
			return null;
		}
		#endregion

		#region RetrieveThumbnail
		public byte[] RetrieveThumbnail(FileInfo OriginalJpegFile)
		{
			String Key = OriginalJpegFile.Name+OriginalJpegFile.LastWriteTime.Ticks;
			// first determine if it's in cache or not...
			if (Cache.ContainsKey(Key))
			{
				// yes it is
				return Cache[Key];
			}
			else
			{
				return Cache[GenerateThumbnail(OriginalJpegFile)];
			}
		}
		#endregion
	}
}

