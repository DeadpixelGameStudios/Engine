using Game1;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class LevelLoader
{

    public struct LevelAsset
    {
        public LevelAsset(Vector2 pos, AssetInfo inf)
        {
            position = pos;
            info = inf;
        }

        public Vector2 position;
        public AssetInfo info;
    }

    public struct AssetInfo
    {
        public AssetInfo(string tex, Type ty)
        {
            texture = tex;
            type = ty;
        }

        public string texture;
        public Type type;
    }

    private const string LevelPath = "Engine/Level/LevelFiles/";


    //Class for loading levels
    //Create Map file in Tiled (.tmx) and load in assets found in \Engine\LevelLoader\Levels\Tiles
    //Call requestLevel with the name of the file as a string to load in
    public LevelLoader()
    {
    }


    public List<LevelAsset> requestLevel(string level)
    {
        return parseLevel(level);
    }


    //Parses level file and creates a list of assets to be added
    private List<LevelAsset> parseLevel(string level)
    {
        XmlDocument parser = new XmlDocument();
        parser.Load(level.Insert(0, LevelPath));


        Dictionary<int, AssetInfo> assetDictionary = createAssetDictionary(parser.DocumentElement.SelectNodes("tileset"));


        int tileHeight = int.Parse(parser.DocumentElement.Attributes["tileheight"].Value);
        int tileWidth = int.Parse(parser.DocumentElement.Attributes["tilewidth"].Value);

        string levelData = parser.DocumentElement.SelectNodes("layer")[0].SelectSingleNode("data").InnerText;
        string[] lines = levelData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        List<LevelAsset> levelAssetList = new List<LevelAsset>();

        int rowNumber = 0;
        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                string[] splitLine = line.Split(new[] { "," }, StringSplitOptions.None);

                int columnNumber = 0;
                foreach (string asset in splitLine)
                {
                    if (asset != "0" && !string.IsNullOrWhiteSpace(asset))
                    {
                        levelAssetList.Add(new LevelAsset(new Vector2(columnNumber * tileWidth, rowNumber * tileHeight), assetDictionary[int.Parse(asset)]));
                    }
                    columnNumber++;
                }
                rowNumber++;
            }
        }

        return levelAssetList;
    }


    //Generates dictionairy of corresponding asset file to asset
    private Dictionary<int, AssetInfo> createAssetDictionary(XmlNodeList tileset)
    {
        Dictionary<int, AssetInfo> textureDict = new Dictionary<int, AssetInfo>();

        foreach (XmlNode node in tileset)
        {
            int uid = int.Parse(node.Attributes["firstgid"].Value);

            string asset = node.Attributes["source"].Value;

            XmlDocument parser = new XmlDocument();
            parser.Load(asset.Insert(0, LevelPath));
            asset = parser.DocumentElement.Attributes["name"].Value.Insert(0, "Walls/");

            string propertyName = parser.DocumentElement.SelectNodes("properties")[0].SelectSingleNode("property").Attributes["name"].Value;

            if (propertyName == "class")
            {
                Type type = Type.GetType("Game1." + parser.DocumentElement.SelectNodes("properties")[0].SelectSingleNode("property").Attributes["value"].Value);

                AssetInfo newAsset = new AssetInfo(asset, type);

                textureDict.Add(uid, newAsset);
            }
        }

        return textureDict;
    }

}