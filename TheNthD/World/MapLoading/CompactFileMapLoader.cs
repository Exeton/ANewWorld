using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.Model;
using The_Nth_D.World;

namespace The_Nth_D.MapLoading
{
	//More 7 times more space efficent than, but less stable than FileMapLoader
	//ToDo: Split this (and FileMapLoader) into two classes
	class CompactFileMapLoader : IMapLoader
	{
		string worldsFolder;

		public CompactFileMapLoader(string worldsFolder)
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
			using (FileStream sr = File.OpenRead(path))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				MapInfo mapInfo = (MapInfo)formatter.Deserialize(sr);

				BinaryReader binaryReader = new BinaryReader(sr, Encoding.Default, true);

				int xLen = binaryReader.ReadInt32();
				int yLen = binaryReader.ReadInt32();
				Block[,] blocks = new Block[xLen, yLen];

				for (int i = 0; i < xLen; i++)
					for (int j = 0; j < yLen; j++)
					{
						bool filled = binaryReader.ReadBoolean();
						blocks[i, j] = new Block(filled, binaryReader.ReadInt32());
					}
				return new Map(blocks, mapInfo).onDeseralized();
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
				formatter.Serialize(sw.BaseStream, map.getMapInfo());

				BinaryWriter binaryWriter = new BinaryWriter(sw.BaseStream, Encoding.Default, true);

				Block[,] blocks = map.map;
				int xLen = blocks.GetLength(0);
				int yLen = blocks.GetLength(1);

				binaryWriter.Write(xLen);
				binaryWriter.Write(yLen);

				for (int i = 0; i < xLen; i++)
					for (int j = 0; j< yLen; j++)
					{
						Block block = blocks[i, j];
						binaryWriter.Write(block.filled);
						binaryWriter.Write(block.type);
					}
			}
		}

		public string getPathFromName(string name)
		{
			return worldsFolder + name;
		}
	}
}
