using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml;

public class LevelLoader
{
    /// <summary>
    /// Struct for a LevelAssets position and AssetInfo
    /// </summary>
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

    /// <summary>
    /// Struct containing information of the assets texture and type
    /// </summary>
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

    
    /// <summary>
    /// Class for loading levels
    /// Create Map file in Tiled (.tmx) and load in assets found in \Engine\LevelLoader\Levels\Tiles
    /// Call requestLevel with the name of the file as a string to load in
    /// </summary>
    public LevelLoader()
    {
    }

    /// <summary>
    /// Takes the requested level, parses the file and returns a list of LevelAssets to add
    /// </summary>
    /// <param name="level">The file name of the level to load</param>
    /// <returns></returns>
    public List<LevelAsset> requestLevel(string level)
    {
        return parseLevel(level);
    }


    /// <summary>
    /// Parses level file and creates a list of assets to be added
    /// </summary>
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



    /// <summary>
    /// Generates dictionairy of corresponding asset file to asset
    /// </summary>
    /// <param name="tileset">The tileset parsed from the level file</param>
    /// <returns>Dictionairy of each tileset uid and info</returns>
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