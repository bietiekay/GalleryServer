using System.Collections.Generic;
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using com.drew.metadata;
using com.drew.imaging.jpg;
using com.drew.metadata.jpeg;
using com.drew.metadata.exif;

namespace GalleryServer
{
	public class FileManagement
	{

		public FileManagement ()
		{
		}

		public void ResizeJPG(String fileName)
		{
			Metadata metadata = JpegMetadataReader.ReadMetadata(new FileInfo(fileName));

			foreach(ExifDirectory _dir in metadata)
			{
				_dir.WriteThumbnail(fileName+"_thumb.jpg");
				break;
			}

/*			Bitmap SourceBitmap = new Bitmap(fileName);
			
			if (SourceBitmap.PhysicalDimension.Width >= SourceBitmap.PhysicalDimension.Height)
			{ // landscape
				Bitmap newimage = new Bitmap(296, 196);
				using (Graphics g = Graphics.FromImage(newimage))
					g.DrawImage(SourceBitmap, 0, 0, 296, 196);
				newimage.Save(fileName+"_landscape.jpg", ImageFormat.Jpeg);
			} // portrait
			else
			{
				Bitmap newimage = new Bitmap(196, 296);
				using (Graphics g = Graphics.FromImage(newimage))
					g.DrawImage(SourceBitmap, 0, 0, 196, 296);
				newimage.Save(fileName+"_portrait.jpg", ImageFormat.Jpeg);
			}*/
		}

		public List<FileInfo> EnumerateAllJPGs(String Path)
		{
			List<FileInfo> Output = new List<FileInfo>();
			#region enumerate all .jpg in ./pics
			DirectoryInfo diTop = new DirectoryInfo(Path);
			foreach (var di in diTop.EnumerateDirectories("*"))
			{
				try
				{
					foreach (var fi in di.EnumerateFiles("*", SearchOption.AllDirectories))
					{
						try
						{
							if (fi.Extension.ToUpper() == ".JPG")
								Output.Add(fi);
						}
						catch (UnauthorizedAccessException UnAuthFile)
						{
						}
					}
				}
				catch (UnauthorizedAccessException UnAuthSubDir)
				{
				}
			}
			#endregion
			return Output;
		}

		public String AllJpegsByDay(List<FileInfo> AllFiles)
		{
			foreach(FileInfo jpg in AllFiles)
			{
				//Output.AppendLine(jpg.Name);
			}

			return "";
		}

	}
}

