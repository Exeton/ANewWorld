using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.MapLoading
{
	class FileMapLoader : IMapLoader
	{
		string worldsFolder;

		public FileMapLoader(string worldsFolder)
		{
			this.worldsFolder = worldsFolder;

			if (!Directory.Exists(worldsFolder))
				Directory.CreateDirectory(worldsFolder);

		}

		public string[] getMapNames()
		{
			string[] filesPaths = Directory.GetFiles(worldsFolder);
			string[] mapNames = new string[filesPaths.Length];

			for (int i = 0; i < filesPaths.Length; i++)
				mapNames[i] = Path.GetFileNameWithoutExtension(filesPaths[i]);

			return mapNames;
		}

		public Map load(string name)
		{
			string path = getPathFromName(name);
			using (FileStream stream = File.OpenRead(path))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				Map map = (Map)formatter.Deserialize(stream);
				return map.onDeseralized();
			}
		}

		public void save(Map map)
		{
			string path = getPathFromName(map.name);
			if (File.Exists(path))
				File.Delete(path);

			using (StreamWriter sw = File.AppendText(path))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(sw.BaseStream, map);
				sw.Close();
			}
		}

		public string getPathFromName(string name)
		{
			return worldsFolder + name;
		}
	}
}
